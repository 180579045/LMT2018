using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CommonUility;
using LogManager;
using SCMTOperationCore.Message.SNMP;

namespace FileManager.FileHandler
{
	public sealed class DtzFileHandler : BaseFileHandler
	{
		public DtzFileHandler(string ip) : base(ip)
		{

		}

		/// <summary>
		/// DTZ文件的处理操作
		/// </summary>
		/// <param name="srcFileFullName"></param>
		/// <param name="dstFilePath"></param>
		/// <returns></returns>
		public override ExecuteResult DoHandle(string srcFileFullName, string dstFilePath)
		{
			if (!IsValidPath(srcFileFullName) || !IsValidPath(dstFilePath))
			{
				throw new CustomException("传入的路径错误");
			}

			TswPackInfo head = new TswPackInfo();
			if (!GetDtzFileDetailInfo(srcFileFullName, ref head))
			{
				throw new CustomException("解析压缩包头出现错误");
			}

			_bDetailFlag = IsExistVerDetailNode(boardAddr);

			//查询所有的软件包详细信息
			var runningSwPackVer = GetRunningSwPackVer(".1");		// 软件包版本
			var runningSwPackVerCP = GetRunningSwPackVer(".2");		// 冷补丁版本
			var runningSwPackVerHP = GetRunningSwPackVer(".3");		// 热补丁版本
			var PPRunningVer = GetRunningPeripheralVer(".1.1");		// 外设版本

			List<string> nbArray = new List<string>();
			for (var i = 1; i < 5; i++)
			{
				nbArray.Add(GetSwPackVersion($".1.{i}"));
			}

			List<string> nbArrayCP = new List<string>();
			for (var i = 1; i < 5; i++)
			{
				nbArrayCP.Add(GetSwPackVersion($".2.{i}"));
			}

			List<string> nbArrayHP = new List<string>();
			for (var i = 1; i < 5; i++)
			{
				nbArrayHP.Add(GetSwPackVersion($".3.{i}"));
			}

			List<string> wsArray = new List<string>();
			for (var i = 1; i < 3; i++)
			{
				wsArray.Add(GetPeripheralVersion($".1.1.{i}"));
			}

			var bTipForceFlag = false;         //是否提醒强制下载

			CompressFileHead zipFileHeader = new CompressFileHead();
			var nRezCode = DtzFileHelper.Aom_Zip_GetFileHead_OupPut(srcFileFullName, ref zipFileHeader);
			if (0 != nRezCode)
			{
				throw new CustomException("获取文件头信息失败");
			}

			var csRelayVersion = new string(zipFileHeader.u8ZipFileRelayVersion).Trim();

			// 比对基站中的文件和本地文件的版本
			if (FileTransMacro.SWPACK_ENB_TYPE == head.nSWEqpType)
			{
				if (FileTransMacro.INVALID_RelayVersion == csRelayVersion)
				{
					foreach (var itemVer in nbArray)
					{
						if (itemVer.Equals(head.csSWPackVersion))		// 判断基站中的软件版本和本地的升级包版本
						{
							bTipForceFlag = true;
							break;
						}
					}

					if (runningSwPackVer.Equals(head.csSWPackVersion))
					{
						bTipForceFlag = true;
					}
				}
				else if (FileTransMacro.EQUIP_SWPACK_BBU_COLDPATCH == head.nSWPackType)
				{
					//判断冷补丁的依赖与running目录下主设备版本是否一致
					if (!runningSwPackVer.Equals(head.csSWPackRelayVersion))
					{
						throw new CustomException("冷补丁依赖的版本与running目录下运行的主设备版本不符合，请选择正确的冷补丁包！");
					}

					foreach (var itemVer in nbArrayCP)
					{
						if (itemVer.Equals(head.csSWPackVersion))
						{
							bTipForceFlag = true;
							break;
						}
					}

					if (runningSwPackVerCP.Equals(head.csSWPackVersion))
					{
						bTipForceFlag = true;
					}
				}
				else if (FileTransMacro.EQUIP_SWPACK_HOTPATCH == head.nSWPackType)
				{
					//判断热补丁的依赖与running目录下主设备版本是否一致
					if (!runningSwPackVer.Equals(head.csSWPackRelayVersion))
					{
						throw new CustomException("热补丁依赖的版本与running目录下运行的主设备版本不符合，请选择正确的热补丁包！");
					}

					foreach (var itemVer in nbArrayHP)
					{
						if (itemVer.Equals(head.csSWPackVersion))
						{
							bTipForceFlag = true;
							break;
						}
					}

					if (runningSwPackVerHP.Equals(head.csSWPackVersion))
					{
						bTipForceFlag = true;
					}
				}

				if (bTipForceFlag)
				{
					if (DialogResult.OK != MessageBox.Show("要升级软件包版本与目前基站内的软件包版本相同，是否强制下载？", "强制下载确认",
							MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
					{
						bTipForceFlag = false;
					}
				}
			}
			else if (head.csSWPackRelayVersion.ToLower().Equals("null"))
			{
				if (FileTransMacro.INVALID_RelayVersion == csRelayVersion)
				{
					foreach (var itemVer in wsArray)
					{
						if (itemVer.Equals(head.csSWPackVersion))
						{
							bTipForceFlag = true;
							break;
						}
					}

					if (PPRunningVer.Equals(head.csSWPackVersion))
					{
						bTipForceFlag = true;
					}

					if (bTipForceFlag)
					{
						if ( DialogResult.OK != MessageBox.Show("要升级外设软件包版本与目前基站内外设的软件包版本相同，是否强制下载？", "强制下载确认",
								MessageBoxButtons.OKCancel, MessageBoxIcon.Question) )
						{
							bTipForceFlag = false;
						}
					}
				}
			}

			// 弹出对话框，确定一些信息后组装命令并下发
			C5216SwPackDlConfirmDlg confirmDlg = new C5216SwPackDlConfirmDlg
			{
				DtzFilePath = Path.GetDirectoryName(srcFileFullName),
				ForceDlFlag = bTipForceFlag,
				TargetIp = boardAddr
			};

			var dlgRet = confirmDlg.ShowDialog();
			if (DialogResult.OK == dlgRet)
			{
				if (!confirmDlg.CmdSucceedFlag)
				{
					throw new CustomException("命令执行失败");
					//return ExecuteResult.UpgradeFailed;
				}

				// TODO 进度条
			}

			return ExecuteResult.UpgradeFinish;
		}


		#region 私有方法

		// 获取DTZ文件的相关属性
		private bool GetDtzFileDetailInfo(string dtzFilePath, ref TswPackInfo swPackInfo)
		{
			CompressFileHead headinfo = new CompressFileHead();
			var result = DtzFileHelper.Aom_Zip_GetFileHead_OupPut(dtzFilePath, ref headinfo);
			if (0 != result)
			{
				Log.Error("获取压缩文件头信息失败");
				return false;
			}

			swPackInfo.csSWPackName = Path.GetFileName(dtzFilePath);
			swPackInfo.csSWPackRelayVersion = new string(headinfo.u8ZipFileRelayVersion).Trim('\0').ToUpper();
			swPackInfo.csSWPackVersion = new string(headinfo.u8ZipFileBigVersion).Trim('\0');

			if (swPackInfo.csSWPackRelayVersion.Equals(FileTransMacro.INVALID_RelayVersion))     //非补丁文件
			{
				swPackInfo.csSWPackRelayVersion = "null";
				swPackInfo.nSWEqpType = FileTransMacro.SWPACK_ENB_PERIPHERAL_TYPE;   //大部分是外设软件包的类型

				var zipFileType = headinfo.u8ZipFileType;
				if ((FileTransMacro.SI_ZIPFILETYPE_SOFT == zipFileType) || (FileTransMacro.SI_ZIPFILETYPE_FIRM == zipFileType))
				{
					swPackInfo.nSWEqpType = FileTransMacro.SWPACK_ENB_TYPE;		//基站软件包
					swPackInfo.nSWPackType = FileTransMacro.EQUIP_SWPACK;		//主设备软件
					swPackInfo.csSWPackTypeName = "主设备软件";
				}
				else if (FileTransMacro.SI_ZIPFILETYPE_RRU == zipFileType)
				{
					swPackInfo.nSWPackType = FileTransMacro.PERIP_SWPACK_RRU;   //RRU软件
					swPackInfo.csSWPackTypeName = "RRU软件";
				}
				else if (FileTransMacro.SI_ZIPFILETYPE_RETANT == zipFileType)
				{
					swPackInfo.nSWPackType = FileTransMacro.PERIP_SWPACK_RETANT;   //电调天线软件
					swPackInfo.csSWPackTypeName = "电调天线";
				}
				else if (FileTransMacro.SI_ZIPFILETYPE_EM == zipFileType)
				{
					swPackInfo.nSWPackType = FileTransMacro.PERIP_SWPACK_EM;   //环境监控软件
					swPackInfo.csSWPackTypeName = "环境监控软件";
				}
				else if (FileTransMacro.SI_ZIPFILETYPE_GPS == zipFileType)
				{
					swPackInfo.nSWPackType = FileTransMacro.PERIP_SWPACK_GPS;   //GSP软件
					swPackInfo.csSWPackTypeName = "GSP软件";
				}
				else if (FileTransMacro.SI_ZIPFILETYPE_1588 == zipFileType)
				{
					swPackInfo.nSWPackType = FileTransMacro.PERIP_SWPACK_1588;   //1588软件
					swPackInfo.csSWPackTypeName = "1588软件";
				}
				else if (FileTransMacro.SI_ZIPFILETYPE_CNSS == zipFileType)
				{
					swPackInfo.nSWPackType = FileTransMacro.PERIP_SWPACK_CNSS;   //北斗软件
					swPackInfo.csSWPackTypeName = "北斗软件";
				}
				else if (FileTransMacro.SI_ZIPFILETYPE_OCU == zipFileType)
				{
					swPackInfo.nSWPackType = FileTransMacro.PERIP_SWPACK_OCU;   //时钟拉远单元
					swPackInfo.csSWPackTypeName = "时钟拉远单元";
				}
				else
				{
					Log.Error($"文件管理模块GetFileInfo，解文件头获得的软件类型不可知swPackInfo.nSWPackType = {zipFileType}!");
					return false;
				}
			}
			else
			{
				//冷热补丁都属于基站软件包
				swPackInfo.nSWEqpType = FileTransMacro.SWPACK_ENB_TYPE;	//基站软件包
				swPackInfo.nSWPackType = FileTransMacro.EQUIP_SWPACK;	//主设备软件

				if (FileTransMacro.SI_ZIPPATCHTYPE_RRUCOLD == headinfo.u8ZipPatchType)
				{
					swPackInfo.nSWPackType = FileTransMacro.PERIP_SWPACK_RRU;			//冷补丁
					swPackInfo.nSWEqpType = FileTransMacro.SWPACK_ENB_PERIPHERAL_TYPE;
					swPackInfo.csSWPackTypeName = "RRU冷补丁";
				}
				else if (FileTransMacro.SI_ZIPPATCHTYPE_HOT == headinfo.u8ZipPatchType)
				{
					//增加对主设备冷热补丁的支持
					swPackInfo.nSWPackType = FileTransMacro.EQUIP_SWPACK_HOTPATCH;		//热补丁
					swPackInfo.nSWEqpType = FileTransMacro.SWPACK_ENB_TYPE;
					swPackInfo.csSWPackTypeName = "主设备热补丁";
				}
				else if (FileTransMacro.SI_ZIPPATCHTYPE_COMMON == headinfo.u8ZipPatchType)
				{
					swPackInfo.nSWPackType = FileTransMacro.EQUIP_SWPACK_BBU_COLDPATCH;	//bbu冷补丁
					swPackInfo.nSWEqpType = FileTransMacro.SWPACK_ENB_TYPE;				//软件包大类型设为主设备软件
					swPackInfo.csSWPackTypeName = "BBU冷补丁";
				}
			}

			return true;
		}

		// 验证路径的有效性
		private bool IsValidPath(string path)
		{
			return !string.IsNullOrWhiteSpace(path) && !string.IsNullOrEmpty(path);
		}

		// 判断是否存在详细信息节点
		private bool IsExistVerDetailNode(string ipAddr)
		{
			throw new NotImplementedException();        //ling:3712
		}

		//查询running sw pack version。index=.1;.2;.3
		private string GetRunningSwPackVer(string index)
		{
			var cmdName = "GetRunningSWPack";
			var mibName = "swPackRunningVersion";

			if (_bDetailFlag)
			{
				cmdName = "GetRunningSWPackDetailVer";
				mibName = "swPackRunningDetailVersion";
			}

			return SnmpToDatabase.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
		}

		//查询running外设详细版本号。index = .1.1
		private string GetRunningPeripheralVer(string index)
		{
			var cmdName = "GetRunningperipheralPack";
			var mibName = "peripheralPackRunningVersion";

			if (_bDetailFlag)
			{
				cmdName = "GetRunningperipheralPackDetailVer";
				mibName = "peripheralPackRunningDetailVersion";
			}

			return SnmpToDatabase.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
		}

		//获取软件包的版本。.1.1~.1.5；.2.1~.2.5[冷补丁]；.3.1~.3.5[热补丁]
		private string GetSwPackVersion(string index)
		{
			var cmdName = "GetSWPack";
			var mibName = "swPackVersion";

			if (_bDetailFlag)
			{
				cmdName = "GetSWPackDetailVer";
				mibName = "swPackDetailVersion";
			}

			return SnmpToDatabase.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
		}

		//获取外设的版本。.1.1.1~.1.1.3
		private string GetPeripheralVersion(string index)
		{
			var cmdName = "GetperipheralPack";
			var mibName = "peripheralPackVersion";

			if (_bDetailFlag)
			{
				cmdName = "GetPeripheralPackDetailVer";
				mibName = "peripheralPackDetailVersion";
			}

			return SnmpToDatabase.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
		}

		#endregion

		#region 私有属性

		private bool _bDetailFlag;

		#endregion
	}


	#region 和dtz相关的struct定义

	public struct TswPackInfo
	{
		public int nSWEqpType;					//软件包大类型，是外设还是基站软件包
		public int nSWPackType;					//软件包类型
		public string csSWPackName;				//软件包名称
		public string csSWPackVersion;			//软件包版本号
		public string csSWPackRelayVersion;		//软件包依赖版本
		public string csSWPackTypeName;			//RRU软件
	}

	public struct TswPackDlProcInfo
	{
		public string FileTypeName;			//软件包类型名
		public string Index;                //命令下发的索引号
		public long SetReqId;                //流水号，对外提供，在探出进度条时使用。
		public string m_csGetProcCmdName;       //进度获取命令名
		public ulong FileSize;            //下载的文件大小
		public bool IsSwPack;                //是否是基站软件包
		public string ActiveIndValue;       //激活标志
		public string FileName;				//文件名称
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct CompressFileHead
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] u8FileCheckHead;								/*必须是固定的数字*/

		public byte u8FileVersion;									/*文件版本，只能压缩，解压缩比自己版本小的*/

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public byte[] u8Reserve;									/*保留字节*/

		public byte u8SuitableType;									/*适用网元类型*/
		public ushort u16SuitableVersion;							/*适用网元型号*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
		public char[] u8ZipFileBigVersion;							/*文件大版本30字节*/

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
		public char[] u8ZipFileRelayVersion;						/*文件大版本依赖关系30字节*/

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
		public char[] u8FileDateTime;								/*文件生成日期30字节*/

		public byte u8ZipFileType;									/*压缩包类型，新增加，占用了一个保留字节*/
		public byte u8ZipPatchType;									/*补丁包类型，占用了一个保留字节*/
		public byte u8SubDtzNum;									/*拆分后小包的个数，0和1为未差分，大于1为小包个数，占用了一个保留字节*/
		public byte u8SubFileNameType;								/*小文件名长度类型，0为40字节，1为240字节。占用了一个保留字节*/

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
		public byte[] u8Reserve2;									/*保留字节，剩下36个字节*/

		public ushort u16FileNum;									/*子文件个数*/
	};
	
	
	#endregion

}

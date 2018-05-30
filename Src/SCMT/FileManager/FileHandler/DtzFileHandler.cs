using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonUility;
using LogManager;
using SCMTOperationCore.Message.SNMP;

namespace FileManager.FileHandler
{
	public sealed class DtzFileHandler : BaseFileHandler
	{
		public DtzFileHandler(string ip)
		: base(ip)
		{

		}

		/// <summary>
		/// DTZ文件的处理操作
		/// </summary>
		/// <param name="srcFileFullName"></param>
		/// <param name="dstFilePath"></param>
		/// <returns></returns>
		public override bool DoHandle(string srcFileFullName, string dstFilePath)
		{
			if (!IsValidPath(srcFileFullName) || !IsValidPath(dstFilePath))
			{
				throw new CustomException("传入的路径错误");
			}

			TSWPackInfo head = new TSWPackInfo();
			if (!GetDtzFileDetailInfo(srcFileFullName, ref head))
			{
				throw new CustomException("解析压缩包头出现错误");
			}

			_bDetailFlag = IsExistVerDetailNode(boardAddr);

			//查询所有的软件包详细信息
			string runningSwPackVer = GetRunningSwPackVer(".1");			// 软件包版本
			string runningSwPackVerCP = GetRunningSwPackVer(".2");		// 冷补丁版本
			string runningSwPackVerHP = GetRunningSwPackVer(".3");		// 热补丁版本
			string PPRunningVer = GetRunningPeripheralVer(".1.1");	// 外设版本

			List<string> nbArray = new List<string>();
			for (int i = 1; i < 5; i++)
			{
				nbArray.Add(GetSwPackVersion($".1.{i}"));
			}

			List<string> nbArrayCP = new List<string>();
			for (int i = 1; i < 5; i++)
			{
				nbArrayCP.Add(GetSwPackVersion($".2.{i}"));
			}

			List<string> nbArrayHP = new List<string>();
			for (int i = 1; i < 5; i++)
			{
				nbArrayHP.Add(GetSwPackVersion($".3.{i}"));
			}

			List<string> wsArray = new List<string>();
			for (int i = 1; i < 3; i++)
			{
				wsArray.Add(GetPeripheralVersion($".1.1.{i}"));
			}

			bool bTipForceFlag = false;         //是否提醒强制下载

			CompressFileHead TZipFileHeader = new CompressFileHead();
			long nRezCode = DtzFileHelper.Aom_Zip_GetFileHead_OupPut(srcFileFullName, ref TZipFileHeader);
			if (0 != nRezCode)
			{
				throw new CustomException("获取文件头信息失败");
			}

			string csRelayVersion = new string(TZipFileHeader.u8ZipFileRelayVersion).Trim();

			// 比对基站中的文件和本地文件的版本
			if (Macro.SWPACK_ENB_TYPE == head.nSWEqpType)
			{
				if (Macro.INVALID_RelayVersion == csRelayVersion)
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
				else if (Macro.EQUIP_SWPACK_BBU_COLDPATCH == head.nSWPackType)
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
				else if (Macro.EQUIP_SWPACK_HOTPATCH == head.nSWPackType)
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
				if (Macro.INVALID_RelayVersion == csRelayVersion)
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

			// 下发命令
			C5216SWPackDownLoad dlg(boardAddr, Path.GetDirectoryName(srcFileFullName), head, bTipForceFlag);
			if (IDOK == dlg.DoModal())
			{
				// 判断命令是否执行成功
				if (!dlg.bCmdSucceed)
				{
					// TODO 提示错误消息
					return false;
				}
				else
				{
					// 弹出进度条等操作 Line:4071
				}
			}

			return true;
		}


		#region 私有方法

		// 获取DTZ文件的相关属性
		private bool GetDtzFileDetailInfo(string dtzFilePath, ref TSWPackInfo swPackInfo)
		{
			CompressFileHead headinfo = new CompressFileHead();
			long result = DtzFileHelper.Aom_Zip_GetFileHead_OupPut(dtzFilePath, ref headinfo);
			if (0 != result)
			{
				Log.Error("获取压缩文件头信息失败");
				return false;
			}

			swPackInfo.csSWPackName = Path.GetFileName(dtzFilePath);
			swPackInfo.csSWPackRelayVersion = new string(headinfo.u8ZipFileRelayVersion).Trim('\0').ToUpper();
			swPackInfo.csSWPackVersion = new string(headinfo.u8ZipFileBigVersion).Trim('\0');

			if (swPackInfo.csSWPackRelayVersion.Equals(Macro.INVALID_RelayVersion))     //非补丁文件
			{
				swPackInfo.csSWPackRelayVersion = "null";
				swPackInfo.nSWEqpType = Macro.SWPACK_ENB_PERIPHERAL_TYPE;   //大部分是外设软件包的类型

				var zipFileType = headinfo.u8ZipFileType;
				if ((Macro.SI_ZIPFILETYPE_SOFT == zipFileType) || (Macro.SI_ZIPFILETYPE_FIRM == zipFileType))
				{
					swPackInfo.nSWEqpType = Macro.SWPACK_ENB_TYPE;		//基站软件包
					swPackInfo.nSWPackType = Macro.EQUIP_SWPACK;		//主设备软件
					swPackInfo.csSWPackTypeName = "主设备软件";
				}
				else if (Macro.SI_ZIPFILETYPE_RRU == zipFileType)
				{
					swPackInfo.nSWPackType = Macro.PERIP_SWPACK_RRU;   //RRU软件
					swPackInfo.csSWPackTypeName = "RRU软件";
				}
				else if (Macro.SI_ZIPFILETYPE_RETANT == zipFileType)
				{
					swPackInfo.nSWPackType = Macro.PERIP_SWPACK_RETANT;   //电调天线软件
					swPackInfo.csSWPackTypeName = "电调天线";
				}
				else if (Macro.SI_ZIPFILETYPE_EM == zipFileType)
				{
					swPackInfo.nSWPackType = Macro.PERIP_SWPACK_EM;   //环境监控软件
					swPackInfo.csSWPackTypeName = "环境监控软件";
				}
				else if (Macro.SI_ZIPFILETYPE_GPS == zipFileType)
				{
					swPackInfo.nSWPackType = Macro.PERIP_SWPACK_GPS;   //GSP软件
					swPackInfo.csSWPackTypeName = "GSP软件";
				}
				else if (Macro.SI_ZIPFILETYPE_1588 == zipFileType)
				{
					swPackInfo.nSWPackType = Macro.PERIP_SWPACK_1588;   //1588软件
					swPackInfo.csSWPackTypeName = "1588软件";
				}
				else if (Macro.SI_ZIPFILETYPE_CNSS == zipFileType)
				{
					swPackInfo.nSWPackType = Macro.PERIP_SWPACK_CNSS;   //北斗软件
					swPackInfo.csSWPackTypeName = "北斗软件";
				}
				else if (Macro.SI_ZIPFILETYPE_OCU == zipFileType)
				{
					swPackInfo.nSWPackType = Macro.PERIP_SWPACK_OCU;   //时钟拉远单元
					swPackInfo.csSWPackTypeName = "时钟拉远单元";
				}
				else
				{
					Log.Error("文件管理模块GetFileInfo，解文件头获得的软件类型不可知swPackInfo.nSWPackType = %d!", zipFileType);
					return false;
				}
			}
			else
			{
				//冷热补丁都属于基站软件包
				swPackInfo.nSWEqpType = Macro.SWPACK_ENB_TYPE;	//基站软件包
				swPackInfo.nSWPackType = Macro.EQUIP_SWPACK;	//主设备软件

				if (Macro.SI_ZIPPATCHTYPE_RRUCOLD == headinfo.u8ZipPatchType)
				{
					swPackInfo.nSWPackType = Macro.PERIP_SWPACK_RRU;			//冷补丁
					swPackInfo.nSWEqpType = Macro.SWPACK_ENB_PERIPHERAL_TYPE;
					swPackInfo.csSWPackTypeName = "RRU冷补丁";
				}
				else if (Macro.SI_ZIPPATCHTYPE_HOT == headinfo.u8ZipPatchType)
				{
					//增加对主设备冷热补丁的支持
					swPackInfo.nSWPackType = Macro.EQUIP_SWPACK_HOTPATCH;		//热补丁
					swPackInfo.nSWEqpType = Macro.SWPACK_ENB_TYPE;
					swPackInfo.csSWPackTypeName = "主设备热补丁";
				}
				else if (Macro.SI_ZIPPATCHTYPE_COMMON == headinfo.u8ZipPatchType)
				{
					swPackInfo.nSWPackType = Macro.EQUIP_SWPACK_BBU_COLDPATCH;	//bbu冷补丁
					swPackInfo.nSWEqpType = Macro.SWPACK_ENB_TYPE;				//软件包大类型设为主设备软件
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
			CDTLmtbPdu InOutPduRunning = new CDTLmtbPdu();
			string csSWRunVerCP = "";
			long lrequestId = 0;

			if (_bDetailFlag)
			{
				DirectCmdGet_Sync("GetRunningSWPackDetailVer", lrequestId, index, boardAddr, InOutPduRunning, false);
				InOutPduRunning.GetValueByMibName(boardAddr, "swPackRunningDetailVersion", out csSWRunVerCP);
			}
			else
			{
				DirectCmdGet_Sync("GetRunningSWPack", lrequestId, index, boardAddr, InOutPduRunning, false);
				InOutPduRunning.GetValueByMibName(boardAddr, "swPackRunningVersion", out csSWRunVerCP);
			}

			return csSWRunVerCP;
		}

		//查询running外设详细版本号。index = .1.1
		private string GetRunningPeripheralVer(string index)
		{
			string csSWRunVerCP = "";
			long lrequestId = 0;

			CDTLmtbPdu InOutPduRunning = new CDTLmtbPdu();

			if (_bDetailFlag)
			{
				DirectCmdGet_Sync("GetRunningperipheralPackDetailVer", lrequestId, index, boardAddr, InOutPduRunning, false);
				InOutPduRunning.GetValueByMibName(boardAddr, "peripheralPackRunningDetailVersion", out csSWRunVerCP);
			}
			else
			{
				DirectCmdGet_Sync("GetRunningperipheralPack", lrequestId, index, boardAddr, InOutPduRunning, false);
				InOutPduRunning.GetValueByMibName(boardAddr, "peripheralPackRunningVersion", out csSWRunVerCP);
			}

			return csSWRunVerCP;
		}

		//获取软件包的版本。.1.1~.1.5；.2.1~.2.5[冷补丁]；.3.1~.3.5[热补丁]
		private string GetSwPackVersion(string index)
		{
			long lrequestId = 0;
			string csCmdValueTemp = "";
			CDTLmtbPdu InOutPdu = new CDTLmtbPdu();

			if (_bDetailFlag)
			{
				DirectCmdGet_Sync("GetSWPackDetailVer", lrequestId, index, boardAddr, InOutPdu, false);
				InOutPdu.GetValueByMibName(boardAddr, "swPackDetailVersion", out csCmdValueTemp);
			}
			else
			{
				DirectCmdGet_Sync("GetSWPack", lrequestId, index, boardAddr, InOutPdu, false);
				InOutPdu.GetValueByMibName(boardAddr, "swPackVersion", out csCmdValueTemp);
			}

			return csCmdValueTemp;
		}

		//获取外设的版本。.1.1.1~.1.1.3
		private string GetPeripheralVersion(string index)
		{
			long lrequestId = 0;
			string csCmdValueTemp = "";
			CDTLmtbPdu InOutPdu = new CDTLmtbPdu();

			if (_bDetailFlag)
			{
				DirectCmdGet_Sync("GetPeripheralPackDetailVer", lrequestId, index, boardAddr, InOutPdu, false);
				InOutPdu.GetValueByMibName(boardAddr, "peripheralPackDetailVersion", out csCmdValueTemp);
			}
			else
			{
				DirectCmdGet_Sync("GetperipheralPack", lrequestId, index, boardAddr, InOutPdu, false);
				InOutPdu.GetValueByMibName(boardAddr, "peripheralPackVersion", out csCmdValueTemp);
			}

			return csCmdValueTemp;
		}

		#endregion

		#region 私有属性

		private bool _bDetailFlag;

		#endregion
	}


	#region 和dtz相关的struct定义

	public struct TSWPackInfo
	{
		public int nSWEqpType;					//软件包大类型，是外设还是基站软件包
		public int nSWPackType;					//软件包类型
		public string csSWPackName;				//软件包名称
		public string csSWPackVersion;			//软件包版本号
		public string csSWPackRelayVersion;		//软件包依赖版本
		public string csSWPackTypeName;			//RRU软件
	}

	public struct TSWPackDLProcInfo
	{
		public string m_csFileTypeName;     //软件包类型名
		public string m_csIndex;                //命令下发的索引号
		public long m_lSetReqId;                //流水号，对外提供，在探出进度条时使用。
		public string m_csGetProcCmdName;       //进度获取命令名
		public UInt64 m_u64FileSize;            //下载的文件大小
		public bool m_bIsswPack;                //是否是基站软件包
		public string m_csActiveIndValue;       //激活标志
		public string m_csFileName;			//文件名称
	}

	internal class Macro
	{
		public const byte SWPACK_ENB_TYPE = 0;				//基站软件包
		public const byte SWPACK_ENB_PERIPHERAL_TYPE = 1;	//基站外设软件包

		public static string INVALID_RelayVersion = "NULL";

		public const byte EQUIP_SWPACK = 1;					//主设备软件
		public const byte EQUIP_SWPACK_BBU_COLDPATCH = 0;	//BBU冷补丁
		public const byte EQUIP_SWPACK_HOTPATCH = 1;		//热补丁
		public const byte EQUIP_SWPACK_RRU_COLDPATCH = 2;	//RRU冷补丁

		// 外设软件类型
		public const byte PERIP_SWPACK_RRU = 1;			//RRU软件
		public const byte PERIP_SWPACK_RETANT = 2;		//电调天线
		public const byte PERIP_SWPACK_EM = 3;			//环境监控单元
		public const byte PERIP_SWPACK_GPS = 4;			//GPS软件
		public const byte PERIP_SWPACK_1588 = 5;		//1588
		public const byte PERIP_SWPACK_CNSS = 6;		//北斗
		public const byte PERIP_SWPACK_OCU = 7;			//时钟拉远单元

		/*压缩文件类型的宏定义*/
		public const byte SI_ZIPFILETYPE_SOFT = 1;			/* 软件压缩包*/
		public const byte SI_ZIPFILETYPE_FIRM = 2;			/* 固件压缩包*/
		public const byte SI_ZIPFILETYPE_GPS = 3;			/* GPS压缩包*/
		public const byte SI_ZIPFILETYPE_ERU = 4;			/* ERU压缩包 */
		public const byte SI_ZIPFILETYPE_EM = 5;			/* 环境监控单元压缩包*/
		public const byte SI_ZIPFILETYPE_RRU = 7;			/* RRU压缩包*/
		public const byte SI_ZIPFILETYPE_RETANT = 8;		/* 电调天线压缩包*/
		public const byte SI_ZIPFILETYPE_1588 = 9;			/* 1588压缩包*/
		public const byte SI_ZIPFILETYPE_CNSS = 10;			/* 北斗压缩包*/
		public const byte SI_ZIPFILETYPE_OCU = 11;			/* 时钟拉远单元压缩包*/

		/*补丁包类型的宏定义*/
		public const byte SI_ZIPPATCHTYPE_COMMON = 0;		/* 普通补丁包*/
		public const byte SI_ZIPPATCHTYPE_HOT = 1;			/* 热补丁包*/
		public const byte SI_ZIPPATCHTYPE_RRUCOLD = 2;      /* rru冷补丁包*/

		#region 公共方法



		#endregion

		#region 私有属性

		#endregion
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

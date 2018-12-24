﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CommonUtility;
using LinkPath;
using LmtbSnmp;
using LogManager;

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
		public override ExecuteResult DoPutFile(string srcFileFullName, string dstFilePath)
		{
			if (!IsValidPath(srcFileFullName) || !IsValidPath(dstFilePath))
			{
				Log.Error("传入的路径错误");
				return ExecuteResult.UpgradeFailed;
			}

			TswPackInfo head = new TswPackInfo();
			if (!GetDtzFileDetailInfo(srcFileFullName, ref head))
			{
				Log.Error("解析压缩包头出现错误");
				return ExecuteResult.UpgradeFailed;
			}

			_bDetailFlag = IsExistVerDetailNode(boardAddr);

			//查询所有的软件包详细信息
			var runningSwPackVer = GetRunningSwPackVer(".1");       // 软件包版本
			if (_bDetailFlag)
			{
				var runningSwPackVerDetail = GetRunningSwPackVerDetail(".1");
			}

			var runningSwPackVerCP = GetRunningSwPackVer(".2");     // 冷补丁版本
			if (_bDetailFlag)
			{
				var runningSwPackVerCPDetail = GetRunningSwPackVerDetail(".2");
			}

			var runningSwPackVerHP = GetRunningSwPackVer(".3");     // 热补丁版本
			if (_bDetailFlag)
			{
				var runningSwPackVerHPDetail = GetRunningSwPackVerDetail(".3");
			}
			var PPRunningVer = GetRunningPeripheralVer(".1.1");     // 外设版本
			if (_bDetailFlag)
			{
				var PPRunningVerDetail = GetRunningPeripheraVerDetail(".1.1");
			}

			List<string> nbArray = new List<string>();
			for (var i = 1; i < 5; i++)
			{
				string ver = GetSwPackVersion($".1.{i}");
				if (!string.IsNullOrEmpty(ver))
				{
					nbArray.Add(GetSwPackVersion($".1.{i}"));
				}
				if (_bDetailFlag)
				{
					ver = GetSwPackVersionDetail($".1.{i}");
					if (!string.IsNullOrEmpty(ver))
					{
						nbArray.Add(ver);
					}
				}
			}

			List<string> nbArrayCP = new List<string>();
			for (var i = 1; i < 5; i++)
			{
				string ver = GetSwPackVersion($".2.{i}");
				if (!string.IsNullOrEmpty(ver))
				{
					nbArrayCP.Add(ver);
				}

				if (_bDetailFlag)
				{
					ver = GetSwPackVersionDetail($".2.{i}");
					if (!string.IsNullOrEmpty(ver))
					{
						nbArray.Add(ver);
					}
				}
			}

			List<string> nbArrayHP = new List<string>();
			for (var i = 1; i < 5; i++)
			{
				string ver = GetSwPackVersion($".3.{i}");
				if (!string.IsNullOrEmpty(ver))
				{
					nbArrayHP.Add(ver);
				}
				if (_bDetailFlag)
				{
					ver = GetSwPackVersionDetail($".3.{i}");
					if (!string.IsNullOrEmpty(ver))
					{
						nbArray.Add(ver);
					}
				}
			}

			List<string> wsArray = new List<string>();
			for (var i = 1; i < 3; i++)
			{
				string ver = GetPeripheralVersion($".1.1.{i}");
				if (!string.IsNullOrEmpty(ver))
				{
					wsArray.Add(ver);
				}
				if (_bDetailFlag)
				{
					ver = GetPeripheralVersionDetail($".1.1.{i}");
					if (!string.IsNullOrEmpty(ver))
					{
						nbArray.Add(ver);
					}
				}
			}

			var bTipForceFlag = false;         //是否提醒强制下载

			CompressFileHead zipFileHeader = new CompressFileHead();
			var nRezCode = DtzFileHelper.Aom_Zip_GetFileHead_OupPut(srcFileFullName, ref zipFileHeader);
			//if (0 != nRezCode)
			//{
			//	Log.Error("获取文件头信息失败");
				//return ExecuteResult.UpgradeFailed;
			//}

			var csRelayVersion = new string(zipFileHeader.u8ZipFileRelayVersion).Trim('\0');

			// 比对基站中的文件和本地文件的版本
			if (FileTransMacro.SWPACK_ENB_TYPE == head.nSWEqpType)
			{
				if (FileTransMacro.INVALID_RelayVersion == csRelayVersion)
				{
					if (nbArray.Any(itemVer => itemVer.Equals(head.csSWPackVersion)))
					{
						bTipForceFlag = true;
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
						Log.Error($"冷补丁依赖的版本 {head.csSWPackRelayVersion} 与running目录下运行的主设备版本 {runningSwPackVer} 不符合，请选择正确的冷补丁包！");
						return ExecuteResult.UpgradeFailed;
					}

					if (nbArrayCP.Any(itemVer => itemVer.Equals(head.csSWPackVersion)))
					{
						bTipForceFlag = true;
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
						Log.Error($"热补丁依赖的版本 {head.csSWPackRelayVersion} 与running目录下运行的主设备版本 {runningSwPackVer} 不符合，请选择正确的热补丁包！");
						return ExecuteResult.UpgradeFailed;
					}

					if (nbArrayHP.Any(itemVer => itemVer.Equals(head.csSWPackVersion)))
					{
						bTipForceFlag = true;
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
						return ExecuteResult.UserCancel;
					}
					bTipForceFlag = true;
				}
			}
			else if (head.csSWPackRelayVersion.ToLower().Equals("null"))
			{
				if (FileTransMacro.INVALID_RelayVersion == csRelayVersion)
				{
					if (wsArray.Any(itemVer => itemVer.Equals(head.csSWPackVersion)))
					{
						bTipForceFlag = true;
					}

					if (PPRunningVer.Equals(head.csSWPackVersion))
					{
						bTipForceFlag = true;
					}

					if (bTipForceFlag)
					{
						if (DialogResult.OK != MessageBox.Show("要升级外设软件包版本与目前基站内外设的软件包版本相同，是否强制下载？", "强制下载确认",
								MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
						{
							return ExecuteResult.UserCancel;
						}
						bTipForceFlag = true;
					}
				}
			}

			// 弹出对话框，确定一些信息后组装命令并下发
			var confirmDlg = new C5216SwPackDlConfirmDlg
			{
				DtzFilePath = Path.GetDirectoryName(srcFileFullName),
				ForceDlFlag = bTipForceFlag,
				TargetIp = boardAddr
			};

			confirmDlg.SetSwPackInfo(head);
			var dlgRet = confirmDlg.ShowDialog();
			if (DialogResult.OK == dlgRet)
			{
				if (!confirmDlg.CmdSucceedFlag)
				{
					Log.Error("命令执行失败");
					return ExecuteResult.UpgradeFailed;
				}
			}
			else
			{
				return ExecuteResult.UserCancel;
			}

			var swPackInfo = new CSWPackPlanProcInfoMgr();
			swPackInfo.SetInfo(confirmDlg.GetDlProcInfo());
			WorkingForUpgrade = true;
			UFO = swPackInfo;

			return ExecuteResult.UpgradeFinish;
		}

		#region 私有方法

		// 获取DTZ文件的相关属性
		private bool GetDtzFileDetailInfo(string dtzFilePath, ref TswPackInfo swPackInfo)
		{
			CompressFileHead headinfo = new CompressFileHead();
			try
			{
				var result = DtzFileHelper.Aom_Zip_GetFileHead_OupPut(dtzFilePath, ref headinfo);
				//if (0 != result)
				//{
				//	Log.Error("获取压缩文件头信息失败");
				//	return false;
				//}
			}
			catch (Exception e)
			{
				Log.Error($"获取压缩文件头信息失败，{e.Message}");
				return false;
			}

			Func<string, string> getRealString = (originValue) =>
			{
				var pos = originValue.IndexOf("\0", StringComparison.Ordinal);
				return -1 == pos ? originValue : originValue.Substring(0, pos);
			};

			swPackInfo.csSWPackName = Path.GetFileName(dtzFilePath);
			swPackInfo.csSWPackRelayVersion = getRealString(new string(headinfo.u8ZipFileRelayVersion)).ToUpper();
			swPackInfo.csSWPackVersion = getRealString(new string(headinfo.u8ZipFileBigVersion));

			if (swPackInfo.csSWPackRelayVersion.Equals(FileTransMacro.INVALID_RelayVersion))     //非补丁文件
			{
				swPackInfo.csSWPackRelayVersion = "null";
				swPackInfo.nSWEqpType = FileTransMacro.SWPACK_ENB_PERIPHERAL_TYPE;   //大部分是外设软件包的类型

				var zipFileType = headinfo.u8ZipFileType;
				if ((FileTransMacro.SI_ZIPFILETYPE_SOFT == zipFileType) || (FileTransMacro.SI_ZIPFILETYPE_FIRM == zipFileType))
				{
					swPackInfo.nSWEqpType = FileTransMacro.SWPACK_ENB_TYPE;     //基站软件包
					swPackInfo.nSWPackType = FileTransMacro.EQUIP_SWPACK;       //主设备软件
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
				swPackInfo.nSWEqpType = FileTransMacro.SWPACK_ENB_TYPE; //基站软件包
				swPackInfo.nSWPackType = FileTransMacro.EQUIP_SWPACK;   //主设备软件

				if (FileTransMacro.SI_ZIPPATCHTYPE_RRUCOLD == headinfo.u8ZipPatchType)
				{
					swPackInfo.nSWPackType = FileTransMacro.PERIP_SWPACK_RRU;           //冷补丁
					swPackInfo.nSWEqpType = FileTransMacro.SWPACK_ENB_PERIPHERAL_TYPE;
					swPackInfo.csSWPackTypeName = "RRU冷补丁";
				}
				else if (FileTransMacro.SI_ZIPPATCHTYPE_HOT == headinfo.u8ZipPatchType)
				{
					//增加对主设备冷热补丁的支持
					swPackInfo.nSWPackType = FileTransMacro.EQUIP_SWPACK_HOTPATCH;      //热补丁
					swPackInfo.nSWEqpType = FileTransMacro.SWPACK_ENB_TYPE;
					swPackInfo.csSWPackTypeName = "主设备热补丁";
				}
				else if (FileTransMacro.SI_ZIPPATCHTYPE_COMMON == headinfo.u8ZipPatchType)
				{
					swPackInfo.nSWPackType = FileTransMacro.EQUIP_SWPACK_BBU_COLDPATCH; //bbu冷补丁
					swPackInfo.nSWEqpType = FileTransMacro.SWPACK_ENB_TYPE;             //软件包大类型设为主设备软件
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
			var mibNode = SnmpToDatabase.GetMibNodeInfoByName("swPackDetailVersion", ipAddr);
			if (null == mibNode)
			{
				return false;
			}

			return (!string.IsNullOrWhiteSpace(mibNode.childOid) && !string.IsNullOrEmpty(mibNode.childOid));
		}

		//查询running sw pack version。index=.1;.2;.3
		private string GetRunningSwPackVer(string index)
		{
			var cmdName = "GetRunningSWPack";
			var mibName = "swPackRunningVersion";

			return CommLinkPath.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
		}

		/// <summary>
		/// 查询running sw pack version。index=.1;.2;.3  详细版本信息
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		private string GetRunningSwPackVerDetail(string index)
		{
			var cmdName = "GetRunningSWPackDetailVer";
			var mibName = "swPackRunningDetailVersion";

			return CommLinkPath.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
		}

		//查询running外设详细版本号。index = .1.1
		private string GetRunningPeripheralVer(string index)
		{
			var cmdName = "GetRunningperipheralPack";
			var mibName = "peripheralPackRunningVersion";

			return CommLinkPath.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
		}

		/// <summary>
		/// 查询running 外设的详细版本号  index = 1.1
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		private string GetRunningPeripheraVerDetail(string index)
		{
			var cmdName = "GetRunningperipheralPackDetailVer";
			var mibName = "peripheralPackRunningDetailVersion";

			return CommLinkPath.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
		}

		//获取软件包的版本。.1.1~.1.5；.2.1~.2.5[冷补丁]；.3.1~.3.5[热补丁]
		private string GetSwPackVersion(string index)
		{
			var cmdName = "GetSWPack";
			var mibName = "swPackVersion";

			return CommLinkPath.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
		}

		//获取详细软件包的版本。.1.1~.1.5；.2.1~.2.5[冷补丁]；.3.1~.3.5[热补丁]
		private string GetSwPackVersionDetail(string index)
		{
			var cmdName = "GetSWPackDetailVer";
			var mibName = "swPackDetailVersion";

			return CommLinkPath.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
		}

		//获取外设的版本。.1.1.1~.1.1.3
		private string GetPeripheralVersion(string index)
		{
			var cmdName = "GetperipheralPack";
			var mibName = "peripheralPackVersion";

			return CommLinkPath.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
		}

		//获取详细外设的版本。.1.1.1~.1.1.3
		private string GetPeripheralVersionDetail(string index)
		{
			var cmdName = "GetPeripheralPackDetailVer";
			var mibName = "peripheralPackDetailVersion";

			return CommLinkPath.GetMibValueFromCmdExeResult(index, cmdName, mibName, boardAddr);
		}

		#endregion 私有方法

		#region 私有属性

		private bool _bDetailFlag;

		#endregion 私有属性
	}
}
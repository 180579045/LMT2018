using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CommonUtility;
using FileManager.FileHandler;
using LogManager;
using MIBDataParser;
using SCMTOperationCore.Message.SNMP;
using MIBDataParser.JSONDataMgr;
using SCMT.Base.FileTransTaskMgr;

namespace FileManager
{
	/// <summary>
	/// DTZ压缩包文件下载确认窗口
	/// </summary>
	public partial class C5216SwPackDlConfirmDlg : Form
	{
		public C5216SwPackDlConfirmDlg()
		{
			InitializeComponent();
		}

		#region 公共方法、属性

		public string TargetIp { get; set; }

		public string DtzFilePath { get; set; }

		public bool ForceDlFlag { get; set; }

		/// <summary>
		/// 命令执行成功与否
		/// </summary>
		public bool CmdSucceedFlag { get; private set; }

		// 设置升级包文件的信息
		public void SetSwPackInfo(TswPackInfo packInfo)
		{
			_mTSwPackInfo = packInfo;

			_mTInfo = new TswPackDlProcInfo();

			_mTInfo.FileTypeName = _mTSwPackInfo.csSWPackTypeName;
			_mTInfo.FileName = _mTSwPackInfo.csSWPackName;

			var fileInfo = new FileInfo($"{DtzFilePath.TrimEnd('\\', '/')}/{_mTInfo.FileName}");
			_mTInfo.FileSize = (ulong)fileInfo.Length;
		}

		public TswPackDlProcInfo GetDlProcInfo()
		{
			return _mTInfo;
		}

		#endregion

		#region 系统事件处理

		// 激活类型修改事件处理
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetTimeCtrlStatus();
		}

		// 点击确定按钮响应函数
		private void IDOK_Click(object sender, EventArgs e)
		{
			CmdSucceedFlag = InitCmdInfoAndSend();
			this.Close();
		}

		private void IDCANCEL_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		// 加载窗体事件处理。在此事件中控件的初始化。
		private void form_Load(object sender, EventArgs e)
		{
			var mibname = "swPackPlanActivateIndicator";

			var bIsPeripheral = (FileTransMacro.SWPACK_ENB_PERIPHERAL_TYPE == _mTSwPackInfo.nSWEqpType) ;
			if (bIsPeripheral)
			{
				mibname = "peripheralPackPlanActivateIndicator";
			}

			// 激活类型
			var activeTypeMap = SnmpToDatabase.GetValueRangeByMibName(mibname, TargetIp);
			if (null == activeTypeMap)
			{
				throw new CustomException("获取软件包激活类型失败");
			}

			var bHasRelay = _mTSwPackInfo.csSWPackRelayVersion.Equals("null");
			var nSelIndex = 0;

			foreach (var at in activeTypeMap)
			{
				if (!bHasRelay)
				{
					if (at.Value.Equals("去激活"))
					{
						continue;       //只有补订包才有去激活
					}
				}

				IDC_COMBO_ACTIVEFLAG.Items.Add(at.Value);

				if (at.Value.Equals("立即激活"))
				{
					nSelIndex = at.Key;
				}
			}

			if (IDC_COMBO_ACTIVEFLAG.Items.Count > 0)
			{
				IDC_COMBO_ACTIVEFLAG.SelectedIndex = nSelIndex;
			}

			// 根据激活类型设置激活时间控件
			SetTimeCtrlStatus();

			// 设置固件激活指示的显示状态
			IDC_COMBO_FWACTIVEFLAG.Enabled = !bIsPeripheral;

			// 判断是否是冷热不定
			var bIsPatch = ((FileTransMacro.EQUIP_SWPACK_BBU_COLDPATCH == _mTSwPackInfo.nSWPackType) ||
							 (FileTransMacro.EQUIP_SWPACK_HOTPATCH == _mTSwPackInfo.nSWPackType));

			if (!bHasRelay && bIsPatch)		//是冷热补丁的
			{
				IDC_COMBO_FWACTIVEFLAG.Enabled = false;	//固件激活
			}

			var fwActive = SnmpToDatabase.GetValueRangeByMibName("swPackPlanFwActiveIndicator", TargetIp);
			if (null == fwActive)
			{
				throw new CustomException("获取软件包固件激活指示失败");
			}

			foreach (var mv in fwActive)
			{
				IDC_COMBO_FWACTIVEFLAG.Items.Add(mv.Value);
				if (mv.Value.Equals("激活"))
				{
					nSelIndex = mv.Key;
				}
			}

			if (IDC_COMBO_FWACTIVEFLAG.Items.Count > 0)
			{
				IDC_COMBO_FWACTIVEFLAG.SelectedIndex = nSelIndex;
			}

			if (ForceDlFlag)
			{
				IDC_EDIT_SOFTPACKFACINFO.Enabled = false;
				IDC_COMBO_ACTIVEFLAG.Enabled = false;
				dateTimePicker1.Enabled = false;
				dateTimePicker2.Enabled = false;
			}

			//Add By Mayi   给控件添加变量
			IDC_EDIT_SOFTPACKTYPE.Text = _mTSwPackInfo.csSWPackTypeName;
			IDC_EDIT_SOFTPACKNAME.Text = _mTSwPackInfo.csSWPackName;
			IDC_EDIT_SOFTPACKFACINFO.Text = _mTSwPackInfo.csSWPackRelayVersion;
			textBox4.Text = _mTSwPackInfo.csSWPackVersion;
		}

		#endregion


		#region 私有方法

		// 组合命令并发送
		private bool InitCmdInfoAndSend()
		{
			string csCmdName;
			string csRowStatus;
			string csIndexToJudgeRowStatus;		//用来判断是否有效，用以区分命令类型

			var bHasDelay = _mTSwPackInfo.csSWPackRelayVersion.ToLower().Equals("null");
			var bIsColdPatch = (FileTransMacro.EQUIP_SWPACK_BBU_COLDPATCH == _mTSwPackInfo.nSWPackType);
			var bIsHotPatch = (FileTransMacro.EQUIP_SWPACK_HOTPATCH == _mTSwPackInfo.nSWPackType);

			if (FileTransMacro.SWPACK_ENB_PERIPHERAL_TYPE == _mTSwPackInfo.nSWEqpType)
			{
				csCmdName = "GetPeripheralPackPlan";
				_mTInfo.IsSwPack = false;
				csRowStatus = "peripheralPackPlanRowStatus";
				csIndexToJudgeRowStatus = $".{_mTSwPackInfo.nSWPackType}.1";
			}
			else
			{
				csCmdName = "GetSWPackPlan";
				_mTInfo.IsSwPack = true;
				csRowStatus = "swPackPlanRowStatus";
				csIndexToJudgeRowStatus = $".{_mTSwPackInfo.nSWPackType}";
				if (!bHasDelay)
				{
					if (bIsColdPatch)
					{
						csIndexToJudgeRowStatus = ".2";
					}

					if (bIsHotPatch)
					{
						csIndexToJudgeRowStatus = ".3";
					}
				}
			}

			var bAddCmdFalg = false;
			var csRowStatusValue = SnmpToDatabase.GetMibValueFromCmdExeResult(csIndexToJudgeRowStatus, csCmdName, csRowStatus, TargetIp);
			if (string.IsNullOrEmpty(csRowStatusValue) || csRowStatusValue.Equals(FileTransMacro.STR_DESTROY))
			{
				bAddCmdFalg = true;
			}

			var filePath = $"{DtzFilePath}/{_mTSwPackInfo.csSWPackName}";
			var dstPath = DtzFilePath;

			var subFileNum = 0;
			var unpackRet = DtzFileHelper.UnpackZipPackageSplitForDTFile(filePath, dstPath, ref subFileNum);
			//if (0 != unpackRet)
			//{
   //             //throw new CustomException("解压缩失败，请检查磁盘空间和压缩文件！");
   //             return true;
			//}

			var subFileCount = Convert.ToString(subFileNum);

			var pSwPackMibNode = SnmpToDatabase.GetMibNodeInfoByName("swPackPlanSubPackNumber", TargetIp);
			var pPeriMibNode = SnmpToDatabase.GetMibNodeInfoByName("peripheralPackPlanSubPackNumber", TargetIp);

			// 外设的
			if (FileTransMacro.SWPACK_ENB_PERIPHERAL_TYPE == _mTSwPackInfo.nSWEqpType)
			{
				var index = $".{_mTSwPackInfo.nSWPackType}.1";		// 第二维是厂家索引，固定填1
				_mTInfo.Index = index;

				csCmdName = "SetPeripheralPackPlan";
				Dictionary<string, string> mapName2Value = new Dictionary<string, string>();

				//如果不是添加命令就不需要行状态字段
				if (bAddCmdFalg)
				{
					mapName2Value.Add(csRowStatus, FileTransMacro.STR_CREATANDGO);
					csCmdName = "AddPeripheralPackPlan";
				}

				mapName2Value.Add("peripheralPackPlanPackName", _mTSwPackInfo.csSWPackName);
				mapName2Value.Add("peripheralPackPlanVendor", IDC_EDIT_SOFTPACKFACINFO.Text);		//厂家信息
				mapName2Value.Add("peripheralPackPlanVersion", _mTSwPackInfo.csSWPackVersion);		//软件包版本

				// 3:强制下载；1：立即下载
				mapName2Value.Add("peripheralPackPlanDownloadIndicator", ForceDlFlag ? "3" : "1");

				var csCurTime = TimeHelper.GetCurrentTime();
				mapName2Value.Add("peripheralPackPlanScheduleDownloadTime", csCurTime);		//使用目前的时间
				mapName2Value.Add("peripheralPackPlanDownloadDirectory", DtzFilePath);

				var actFlagIndex = Convert.ToString(IDC_COMBO_ACTIVEFLAG.SelectedIndex);
				_mTInfo.ActiveIndValue = actFlagIndex;			//激活标志
				mapName2Value.Add("peripheralPackPlanActivateIndicator", actFlagIndex);
				mapName2Value.Add("peripheralPackPlanScheduleActivateTime", GetActiveTimeString(ForceDlFlag));

				//冷不丁包加入依赖版本节点
				mapName2Value.Add("peripheralPackPlanRelyVesion", _mTSwPackInfo.csSWPackRelayVersion);
				if (pPeriMibNode != null)
				{
					mapName2Value.Add("peripheralPackPlanSubPackNumber", subFileCount);
				}

				var ret = CDTCmdExecuteMgr.CmdSetAsync(csCmdName, out _mTInfo.SetReqId, mapName2Value, index, TargetIp);
				if (0 == ret)
				{
					Log.Info("外设软件规划命令下发成功!");
					return true;
				}

				Log.Error("外设软件规划命令下发失败!\n");
				return false;
			}
			else
			{
				Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
				string csswPackPlanTypeIndex;

				if (bHasDelay)
				{
					long lReqId;
					csswPackPlanTypeIndex = ".2";
					csCmdName = "DelSWPackPlan";

					mapName2Value.Add("swPackPlanRowStatus", "6"); //6是无效
					lReqId = 0;
					var ret = CDTCmdExecuteMgr.CmdSetAsync(csCmdName, out lReqId, mapName2Value, csswPackPlanTypeIndex, TargetIp);
					if (0 == ret)
					{
						Log.Info("删除冷补丁软件规划命令下发成功!");
					}
					else
					{
						Log.Error("删除冷补丁软件规划命令下发失败!");
					}

					csswPackPlanTypeIndex = ".3";
					lReqId = 1;
					ret = CDTCmdExecuteMgr.CmdSetAsync(csCmdName, out lReqId, mapName2Value, csswPackPlanTypeIndex, TargetIp);
					if (0 == ret)
					{
						Log.Info("删除热补丁软件规划命令下发成功!");
					}
					else
					{
						Log.Error("删除热补丁软件规划命令下发失败!");
					}
				}

				mapName2Value.Clear();
				csswPackPlanTypeIndex = $".{_mTSwPackInfo.nSWPackType}";

				if (!bHasDelay && bIsColdPatch)
				{
					csswPackPlanTypeIndex = ".2";
				}

				if (!bHasDelay && bIsHotPatch)
				{
					csswPackPlanTypeIndex = ".3";
				}
				_mTInfo.Index = csswPackPlanTypeIndex;

				csCmdName = "SetSWPackPlan";

				//如果不是添加命令就不需要行状态字段
				if (bAddCmdFalg)
				{
					csCmdName = "AddSWPackPlan";
				}

				mapName2Value.Add(csRowStatus, FileTransMacro.STR_CREATANDGO);
				mapName2Value.Add("swPackPlanPackName", _mTSwPackInfo.csSWPackName);
				mapName2Value.Add("swPackPlanVendor", IDC_EDIT_SOFTPACKFACINFO.Text);		//厂家信息
				mapName2Value.Add("swPackPlanVersion", _mTSwPackInfo.csSWPackVersion);		//软件包版本

				// 3:强制下载;1:立即下载
				mapName2Value.Add("swPackPlanDownloadIndicator", ForceDlFlag ? "3" : "1");

				var csCurTime = TimeHelper.GetCurrentTime();
				mapName2Value.Add("swPackPlanScheduleDownloadTime", csCurTime); //使用目前的时间
				mapName2Value.Add("swPackPlanDownloadDirectory", DtzFilePath);

				var actFlagIndex = Convert.ToString(IDC_COMBO_ACTIVEFLAG.SelectedIndex);
				_mTInfo.ActiveIndValue = actFlagIndex;						//激活标志
				mapName2Value.Add("swPackPlanActivateIndicator", actFlagIndex);
				mapName2Value.Add("swPackPlanScheduleActivateTime", GetActiveTimeString(ForceDlFlag));
				mapName2Value.Add("swPackPlanRelyVesion", _mTSwPackInfo.csSWPackRelayVersion);

				var fwActFlagIndex = Convert.ToString(IDC_COMBO_FWACTIVEFLAG.SelectedIndex);
				mapName2Value.Add("swPackPlanFwActiveIndicator", fwActFlagIndex);

				if (pSwPackMibNode != null)
				{
					mapName2Value.Add("swPackPlanSubPackNumber", subFileCount);
				}

				var setRet = CDTCmdExecuteMgr.CmdSetAsync(csCmdName, out _mTInfo.SetReqId, mapName2Value, csswPackPlanTypeIndex, TargetIp);

				if (0 == setRet)
				{
					Log.Info("软件规划命令下发成功!");
					return true;

				}

				Log.Error("软件规划命令下发失败!\n");
				return false;
			}
		}

		// 设置激活时间控件状态
		private void SetTimeCtrlStatus()
		{
			var activeChoice = IDC_COMBO_ACTIVEFLAG.SelectedText;
			dateTimePicker1.Enabled = activeChoice.Equals("定时激活");
			dateTimePicker2.Enabled = activeChoice.Equals("定时激活");
		}

		// 获取激活时间字符串
		private string GetActiveTimeString(bool bForceDownload)
		{
			if (bForceDownload)
			{
				return TimeHelper.GetCurrentTime();
			}

			return $"{dateTimePicker1.Text} {dateTimePicker2.Text}";
		}

		#endregion

		#region 私有属性

		private TswPackInfo _mTSwPackInfo;
		private TswPackDlProcInfo _mTInfo;

		#endregion
	}
}

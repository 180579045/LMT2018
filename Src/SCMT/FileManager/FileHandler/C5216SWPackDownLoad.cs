using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonUility;
using FileManager.FileHandler;
using LogManager;
using SCMTOperationCore.Message.SNMP;

namespace FileManager
{
	public partial class C5216SWPackDownLoad : Form
	{
		public C5216SWPackDownLoad()
		{
			InitializeComponent();
		}

		#region 公共方法、属性

		public string m_csIpAddr { get; set; }

		public string m_csFilePath { get; set; }

		public bool m_bForceFlag { get; set; }

		public void SetSwPackInfo(TSWPackInfo packInfo)
		{
			//m_TSwPackInfo = new TSWPackInfo();
			m_TSwPackInfo = packInfo;
		}

		#endregion


		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		// 点击确定按钮响应函数
		private void IDOK_Click(object sender, EventArgs e)
		{

		}

		private void IDCANCEL_Click(object sender, EventArgs e)
		{

		}


		#region 私有方法

		// 组合命令并发送
		private bool InitCmdInfoAndSend()
		{
			string csCmdName = "";
			string csRowStatus = "";
			string csIndexTOJudgeRowStatus = "";//用来判断是否有效，用以区分命令类型

			if (Macro.SWPACK_ENB_PERIPHERAL_TYPE == m_TSwPackInfo.nSWEqpType)
			{
				csCmdName = "GetPeripheralPackPlan";
				m_TInfo.m_bIsswPack = false;
				csRowStatus = "peripheralPackPlanRowStatus";
				csIndexTOJudgeRowStatus = $".{m_TSwPackInfo.nSWPackType}.1";
			}
			else
			{
				csCmdName = "GetSWPackPlan";
				m_TInfo.m_bIsswPack = true;
				csRowStatus = "swPackPlanRowStatus";
				csIndexTOJudgeRowStatus = $".{m_TSwPackInfo.nSWPackType}";
				if ("null" != m_TSwPackInfo.csSWPackRelayVersion.ToLower())
				{
					if (Macro.EQUIP_SWPACK_BBU_COLDPATCH == m_TSwPackInfo.nSWPackType)
					{
						csIndexTOJudgeRowStatus = ".2";
					}
					if (Macro.EQUIP_SWPACK_HOTPATCH == m_TSwPackInfo.nSWPackType)
					{
						csIndexTOJudgeRowStatus = ".3";
					}
				}
			}

			bool bAddCmdFalg = false;
			string csRowStatusValue = GetValueByMibNameAndIndex_SYN(csCmdName, csCmdName, csIndexTOJudgeRowStatus);
			if (string.IsNullOrEmpty(csRowStatusValue) || csRowStatusValue.Equals(FileTransMacro.STR_DESTROY))
			{
				bAddCmdFalg = true;
			}

			string filePath = $"{m_csFilePath}/{m_TSwPackInfo.csSWPackName}";
			string dstPath = m_csFilePath;

			int subFileNum = 0;
			DtzFileHelper.UnpackZipPackageSplitForDTFile(filePath, dstPath, ref subFileNum);
			string subFileCount = Convert.ToString(subFileNum);

			GetMibNodeInfoByName();
			GetMibNodeInfoByName();

			// 外设的
			if (Macro.SWPACK_ENB_PERIPHERAL_TYPE == m_TSwPackInfo.nSWEqpType)
			{
				string index = $".{m_TSwPackInfo.nSWPackType}.1";
				m_TInfo.m_csIndex = index;

				csCmdName = "SetPeripheralPackPlan";
				Dictionary<string, string> mapName2Value = new Dictionary<string, string>();

				//如果不是添加命令就不需要行状态字段
				if (bAddCmdFalg)
				{
					mapName2Value.Add(csRowStatus, FileTransMacro.STR_CREATANDGO);
					csCmdName = "AddPeripheralPackPlan";
				}

				mapName2Value.Add("peripheralPackPlanPackName", m_TSwPackInfo.csSWPackName);
				mapName2Value.Add("peripheralPackPlanVendor", m_csSWPackFacInfo);					//厂家信息
				mapName2Value.Add("peripheralPackPlanVersion", m_TSwPackInfo.csSWPackVersion);		//软件包版本

				if (m_bForceFlag)
				{
					mapName2Value.Add("peripheralPackPlanDownloadIndicator", "3");     // 设置为强制下载
				}
				else
				{
					mapName2Value.Add("peripheralPackPlanDownloadIndicator", "1");     //设置为立即下载
				}

				string csCurTime = TimeHelper.GetCurrentTime();
				mapName2Value.Add("peripheralPackPlanScheduleDownloadTime", csCurTime);		//使用目前的时间
				mapName2Value.Add("peripheralPackPlanDownloadDirectory", m_csFilePath);

				csValue.Format("%d", m_nActiveFlag);
				m_TInfo.m_csActiveIndValue = csValue;//激活标志
				mapName2Value.Add("peripheralPackPlanActivateIndicator", csValue);
				mapName2Value.Add("peripheralPackPlanScheduleActivateTime", m_csTime);

				//冷不丁包加入依赖版本节点
				mapName2Value.Add("peripheralPackPlanRelyVesion", m_TSwPackInfo.csSWPackRelayVersion);
				if (pPeriMibNode != null)
				{
					mapName2Value.Add("peripheralPackPlanSubPackNumber", subFileCount);
				}

				if (DirectCmdSet(csCmdName, m_TInfo.m_lSetReqId, mapName2Value, index, m_csIpAddr))
				{
					Log.Info("外设软件规划命令下发成功!");
					return true;
				}
				else
				{
					Log.Error("外设软件规划命令下发失败!\n");
					return false;
				}
			}
			else
			{
				string csswPackPlanTypeIndex = "";
				Dictionary<string, string> mapName2Value = new Dictionary<string, string>();

				if ("null" == m_TSwPackInfo.csSWPackRelayVersion.ToLower())
				{
					long m_lReqId = 0;
					csswPackPlanTypeIndex = ".2";
					csCmdName = "DelSWPackPlan";

					mapName2Value.Add("swPackPlanRowStatus", "6"); //6是无效
					if (DirectCmdSet(csCmdName, m_lReqId, mapName2Value, csswPackPlanTypeIndex, m_csIpAddr))
					{
						Log.Info("删除冷补丁软件规划命令下发成功!");
					}
					else
					{
						Log.Error("删除冷补丁软件规划命令下发失败!\n");
					}

					m_lReqId = 1;
					csswPackPlanTypeIndex = ".3";
					if (DirectCmdSet(csCmdName, m_lReqId, mapName2Value, csswPackPlanTypeIndex, m_csIpAddr))
					{
						L_NOTICE("删除热补丁软件规划命令下发成功!");
					}
					else
					{
						Log.Error("删除热补丁软件规划命令下发失败!\n");
					}

					mapName2Value.Clear();
					csswPackPlanTypeIndex = $".{m_TSwPackInfo.nSWPackType}";
					m_TInfo.m_csIndex = csswPackPlanTypeIndex;

					if (("null" != m_TSwPackInfo.csSWPackRelayVersion) &&
					    (Macro.EQUIP_SWPACK_BBU_COLDPATCH == m_TSwPackInfo.nSWPackType))
					{
						csswPackPlanTypeIndex = ".2";
						m_TInfo.m_csIndex = csswPackPlanTypeIndex;
					}

					if (("null" != m_TSwPackInfo.csSWPackRelayVersion) &&
					    (Macro.EQUIP_SWPACK_HOTPATCH == m_TSwPackInfo.nSWPackType))
					{
						csswPackPlanTypeIndex = ".3";
						m_TInfo.m_csIndex = csswPackPlanTypeIndex;
					}

					csCmdName = "SetSWPackPlan";

					//如果不是添加命令就不需要行状态字段
					if (bAddCmdFalg)
					{
						csCmdName = "AddSWPackPlan";
					}

					mapName2Value.Add(csRowStatus, FileTransMacro.STR_CREATANDGO);
					mapName2Value.Add("swPackPlanPackName", m_TSwPackInfo.csSWPackName);
					mapName2Value.Add("swPackPlanVendor", m_csSWPackFacInfo);				//厂家信息
					mapName2Value.Add("swPackPlanVersion", m_TSwPackInfo.csSWPackVersion);	//软件包版本

					if (m_bForceFlag)
					{
						mapName2Value.Add("swPackPlanDownloadIndicator", "3"); //设置为强制下载
					}
					else
					{
						mapName2Value.Add("swPackPlanDownloadIndicator", "1"); //设置为立即下载
					}

					string csCurTime = TimeHelper.GetCurrentTime();
					mapName2Value.Add("swPackPlanScheduleDownloadTime", csCurTime); //使用目前的时间
					mapName2Value.Add("swPackPlanDownloadDirectory", m_csFilePath);

					csValue.Format("%d", m_nActiveFlag);
					m_TInfo.m_csActiveIndValue = csValue; //激活标志
					mapName2Value.Add("swPackPlanActivateIndicator", csValue);
					mapName2Value.Add("swPackPlanScheduleActivateTime", m_csTime);
					mapName2Value.Add("swPackPlanRelyVesion", m_TSwPackInfo.csSWPackRelayVersion);

					csValue.Format("%d", m_nFWActiveFlag);
					mapName2Value.Add("swPackPlanFwActiveIndicator", csValue);

					if (pSwPackMibNode != null)
					{
						mapName2Value.Add("swPackPlanSubPackNumber", subFileCount);
					}

					if (DirectCmdSet(csCmdName, m_TInfo.m_lSetReqId, mapName2Value, csswPackPlanTypeIndex, m_csIpAddr))
					{
						Log.Info("软件规划命令下发成功!");
						return true;

					}
					else
					{
						Log.Error("软件规划命令下发失败!\n");
						return false;
					}
				}
			}

			return true;
		}

		private string GetValueByMibNameAndIndex_SYN(string csCmdName, string strMibName, string csIndex)
		{
			CDTLmtbPdu InOutPdu = new CDTLmtbPdu();
			string RowStatusValue;
			long lrequestId = 0;

			int nGetCmdRezlt = DirectCmdGet_Sync(csCmdName, lrequestId, csIndex, m_csIpAddr, ref InOutPdu, false);
			InOutPdu.GetValueByMibName(m_csIpAddr,strMibName, out RowStatusValue);
			//L_NOTICE("现查版本号：cscmdNam=%s,%s=%s", csCmdName, strMibName, RowStatusValue);
			return RowStatusValue;
		}


	#endregion

		#region 私有属性

		private TSWPackInfo m_TSwPackInfo;
		private TSWPackDLProcInfo m_TInfo;

		#endregion
	}
}

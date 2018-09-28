using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using CommonUtility;
using LogManager;
using SCMT.Base.FileTransTaskMgr;
using SCMTOperationCore.Message.SNMP;

namespace FileManager.FileHandler
{
	using MIBValMAP = Dictionary<string, string>;

	public sealed class CfgFileHandler : BaseFileHandler
	{
		public CfgFileHandler(string ip) : base(ip)
		{

		}

		#region 虚函数区

		public override ExecuteResult DoPutFile(string srcFileFullName, string dstFilePath)
		{
			CfgFileSelDlg dlg = new CfgFileSelDlg();
			var ret = dlg.ShowDialog();
			if (DialogResult.OK != ret)
			{
				return ExecuteResult.UserCancel;
			}

			// 非RNC容灾文件需要进行校验
			if (!dlg.FileTypeString.Equals("RNC容灾数据文件"))
			{
				var errorInfo = "";
				if (!CheckCfgFile(srcFileFullName, ref errorInfo))
				{
					errorInfo += "\r\n\r\n";
					var errorDlg = new CfgFileCheckErrorDlg(errorInfo);
					if (DialogResult.OK != errorDlg.ShowDialog())
					{
						return ExecuteResult.UserCancel;
					}
				}

				//提示信息
				const string tip = "该操作可能触发版本升级，请核对基站 / 外设软件包配置信息。\n确认后请单击“确定”，否则单击“取消”";
				ret = MessageBox.Show(tip, "升级确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (DialogResult.Cancel == ret)
				{
					return ExecuteResult.UserCancel;
				}
			}

			var dstPath = "";
			if (dlg.FileTypeString.Equals("配置文件"))
			{
				fileType = Transfiletype5216.TRANSFILE_cfgPackFile;
				dstPath = FileTransMacro.STR_EMPTYPATH;
			}

			if (dlg.FileTypeString.Equals("一般文件"))
			{
				fileType = Transfiletype5216.TRANSFILE_generalFile;
				dstPath = FileTransMacro.STR_FILEPATH;
			}

			if (dlg.FileTypeString.Equals("RNC容灾数据文件"))
			{
				fileType = Transfiletype5216.TRANSFILE_rncDisa;
				dstPath = FileTransMacro.STR_FILEPATH_RNC;
			}

			return base.DoPutFile(srcFileFullName, dstPath);
		}

		protected override Transfiletype5216 GetTransFileType()
		{
			return fileType;
		}

		#endregion

		#region 私有函数区

		public bool CheckCfgFile(string fileFullPath, ref string errorInfo)
		{
			var mapCheckNode = GetNeedCheckNodeFromXml();
			if (mapCheckNode.Count == 0)
			{
				return true;
			}

			var mapFileNodeValue = new MIBValMAP();		//当前文件校验节点，值
			var mapFileNodeValue2 = new MIBValMAP();       //当前文件校验节点，值

			var ret = GetCfgNodeValue(fileFullPath, "omLinkEntry", mapCheckNode, mapFileNodeValue, mapFileNodeValue2);
			if (!ret)
			{
				throw new CustomException("校验失败");
			}

			errorInfo = "传输参数不同节点值核对如下:\r\n";

			var nReportLine = 0;
			var bResult = false;
			var FileNodeMap = new MIBValMAP[2] {mapFileNodeValue, mapFileNodeValue};
			for (var i = 0; i < 2; i++)
			{
				CDTLmtbPdu pdu = new CDTLmtbPdu();
				var FileValueMap = FileNodeMap[i];
				var index = $".{i}";
				long reqId = 0;
				var cmdRet = CDTCmdExecuteMgr.GetInstance().CmdGetSync("GetOmLinkInfo", out reqId, index, boardAddr, ref pdu);
				if (0 != cmdRet)
				{
					Log.Error($"GetOmLinkInfo命令下发失败");
					if (FileValueMap.Count > 0)
					{
						bResult = false;
						var reportLine = ($"------OM链路--实例-----{i}----文件中存在，而基站侧不存在\r\n");
						errorInfo += reportLine;
					}
				}
				else
				{
					bResult = FindDifferInMapandPdu(mapCheckNode, FileValueMap, pdu, ref errorInfo, i, ref nReportLine);
				}
			}

			return bResult;
		}

		// 从config/LeafCFGCheck.xml文件中解析需要校验的信息
		// key:english name，value:中文描述
		private MIBValMAP GetNeedCheckNodeFromXml()
		{
			var xmlFilePath = FilePathHelper.GetConfigPath() + "LeafCFGCheck.xml";
			if (!FilePathHelper.FileExists(xmlFilePath))
			{
				throw new CustomException($"文件{xmlFilePath}不存在");
			}

			var checkNodeDic = new MIBValMAP();
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlFilePath);

			var countNode = doc.SelectSingleNode("/root/totalNum");
			if (countNode != null)
			{
				var count  = int.Parse(countNode.InnerText);
				for (var i = 0; i < count; i++)
				{
					var label = $"/root/Task{i}";
					var taskNode = doc.SelectSingleNode(label) as XmlElement;
					if (taskNode != null)
					{
						checkNodeDic[taskNode.GetAttribute("name")] = taskNode.GetAttribute("key");
					}
				}
			}

			return checkNodeDic;
		}

		private bool GetCfgNodeValue(string cfgFilePath, string checkTblName, MIBValMAP mapNodeName,
			MIBValMAP mapNodeValue, MIBValMAP mapNodeValue2)
		{
			OM_STRU_CfgFile_FieldInfo OmLinkIndexNode = new OM_STRU_CfgFile_FieldInfo();

			FileStream fs = new FileStream(cfgFilePath, FileMode.Open);
			fs.Seek(0, SeekOrigin.Begin);

			var headBytes = new byte[OMIC_STRU_ICFile_Header.SizeOf()];
			var readLen = fs.Read(headBytes, 0, OMIC_STRU_ICFile_Header.SizeOf());
			if (readLen != headBytes.Length)
			{
				fs.Close();
				throw new CustomException("读取文件失败");
			}

			OMIC_STRU_ICFile_Header header = (OMIC_STRU_ICFile_Header) SerializeHelper.BytesToStruct(headBytes, typeof(OMIC_STRU_ICFile_Header));
			var dataBlockPos = SerializeHelper.ReverseFourBytesData(header.u32DataBlk_Location);
			fs.Seek(dataBlockPos, SeekOrigin.Begin);

			var dataHeadBytes = new byte[DataHead.SizeOf()];
			readLen = fs.Read(dataHeadBytes, 0, DataHead.SizeOf());
			if (readLen != dataHeadBytes.Length)
			{
				fs.Close();
				throw new CustomException("读取文件失败");
			}

			var dataHeader = (DataHead)SerializeHelper.BytesToStruct(dataHeadBytes, typeof(DataHead));
			var dataHeadVerify = Encoding.UTF8.GetString(dataHeader.u8VerifyStr).Replace("\0", "");
			if (!dataHeadVerify.Equals("BEG"))
			{
				fs.Close();
				throw new CustomException("该配置文件的数据头部信息校验失败");
			}

			var tableCount = SerializeHelper.ReverseFourBytesData(dataHeader.u32TableCnt);
			for (var i = 0; i < tableCount; i++)
			{
				fs.Seek(dataBlockPos + DataHead.SizeOf(), SeekOrigin.Begin);
				var tableItemIdxBytes = new byte[OM_STRU_IcfIdxTableItem.SizeOf()];
				for (var j = 0; j <= i; j++)
				{
					readLen = fs.Read(tableItemIdxBytes, 0, OM_STRU_IcfIdxTableItem.SizeOf());
					if (readLen != tableItemIdxBytes.Length)
					{
						fs.Close();
						throw new CustomException($"读取头文件第{j}个表的偏移量失败");
					}
				}

				var tableItemIdx = (OM_STRU_IcfIdxTableItem)SerializeHelper.BytesToStruct(tableItemIdxBytes, typeof(OM_STRU_IcfIdxTableItem));
				var tableInitPos = SerializeHelper.ReverseFourBytesData(tableItemIdx.u32CurTblOffset);
				fs.Seek(tableInitPos, SeekOrigin.Begin);

				var cfgTableInfoBytes = new byte[OM_STRU_CfgFile_TblInfo.SizeOf()];
				readLen = fs.Read(cfgTableInfoBytes, 0, OM_STRU_CfgFile_TblInfo.SizeOf());
				if (readLen != cfgTableInfoBytes.Length)
				{
					fs.Close();
					throw new CustomException($"读取头文件第{i}个表信息失败");
				}

				var cfgTableInfo = (OM_STRU_CfgFile_TblInfo)SerializeHelper.BytesToStruct(cfgTableInfoBytes, typeof(OM_STRU_CfgFile_TblInfo));
				var tableName = Encoding.UTF8.GetString(cfgTableInfo.s8TblName).Replace("\0", "");
				if (!tableName.Equals(checkTblName))
				{
					continue;
				}
				else
				{
					var nFieldCount = SerializeHelper.ReverseUshort(cfgTableInfo.u16FieldNum);
					var nRecordCount = SerializeHelper.ReverseFourBytesData(cfgTableInfo.u32RecNum);
					var nRecordLen = SerializeHelper.ReverseUshort(cfgTableInfo.u16RecLen);
					for (var k = 0; k < nFieldCount; k++)
					{
						var offset = tableInitPos + OM_STRU_CfgFile_TblInfo.SizeOf() +
						             OM_STRU_CfgFile_FieldInfo.SizeOf() * k;
						fs.Seek(offset, SeekOrigin.Begin);
						var fieldInfoBytes = new byte[OM_STRU_CfgFile_FieldInfo.SizeOf()];
						readLen = fs.Read(fieldInfoBytes, 0, OM_STRU_CfgFile_FieldInfo.SizeOf());
						var fieldInfo =
							(OM_STRU_CfgFile_FieldInfo) SerializeHelper.BytesToStruct(fieldInfoBytes, typeof(OM_STRU_CfgFile_FieldInfo));
						var mibNodeName = Encoding.UTF8.GetString(fieldInfo.s8FieldName).Replace("\0", "");
						if (mibNodeName.Equals("omLinkIndex"))
						{
							OmLinkIndexNode = fieldInfo;
						}

						if (mapNodeName.ContainsKey(mibNodeName))
						{
							for (var recordNo = 0; recordNo < nRecordCount; recordNo++)
							{
								var pInstMem = new byte[nRecordLen];
								var offset2 = tableInitPos + OM_STRU_CfgFile_TblInfo.SizeOf() +
								              OM_STRU_CfgFile_FieldInfo.SizeOf() * nFieldCount +
								              recordNo * nRecordLen;
								fs.Seek(offset2, SeekOrigin.Begin);
								fs.Read(pInstMem, 0, nRecordLen);

								var value = GetFiledValue_ByType(fieldInfo, pInstMem);
								var indexValue = GetFiledValue_ByType(OmLinkIndexNode, pInstMem);
								if (indexValue.Equals("0"))
								{
									mapNodeValue[mibNodeName] = value;
								}
								else if (indexValue.Equals("1"))
								{
									mapNodeValue2[mibNodeName] = value;
								}
							}
						}
					}
				}
				break;
			}
			fs.Close();
			return true;
		}

		private string GetFiledValue_ByType(OM_STRU_CfgFile_FieldInfo fieldInfo, byte[] bufBytes)
		{
			var strValue = "";
			int fieldLength = SerializeHelper.ReverseUshort(fieldInfo.u16FieldLen);
			int fieldOff = SerializeHelper.ReverseUshort(fieldInfo.u16FieldOffset);

			switch (fieldInfo.u8FieldType)
			{
				case 5:
					var temp_u32 = BitConverter.ToUInt64(bufBytes, fieldOff);
					var s32Value = SerializeHelper.ReverseEightBytesData(temp_u32);
					strValue = $"{s32Value}";
					break;

				case 13:
					var tmp = new byte[fieldLength + 1];
					Buffer.BlockCopy(bufBytes, fieldOff, tmp, 0, fieldLength);
					//strValue = BitConverter.ToString(tmp);
					SetInetAddress(tmp, fieldLength, ref strValue, 0);
					break;
			}
			return strValue;
		}

		// 把IP格式化成点分十进制字符串
		private void SetInetAddress(byte[] inputBytes, int len, ref string value, int type)
		{
			if (type == 0)		// ipv4
			{
				var ipValue = new byte[4];
				Buffer.BlockCopy(inputBytes, 0, ipValue, 0, 4);
				value = $"{ipValue[0]}.{ipValue[1]}.{ipValue[2]}.{ipValue[3]}";
			}
			else				// ipv6
			{
				throw new CustomException("ipv6正在建设中...");
			}
		}

		private bool FindDifferInMapandPdu(MIBValMAP mapCheckNode, MIBValMAP FileNodeValueMap, CDTLmtbPdu InOutPdu2,
			ref string csCheckError, int index, ref int nReportLine)
		{
			var reportErr = "";
			if (FileNodeValueMap.Count == 0)
			{
				Log.Error($"文件中实例{index}为空,enb不为空");

				reportErr = ($"------OM链路--实例-----{index}----基站侧存在，而文件不存在\r\n");
				csCheckError += reportErr;
				return false;
			}

			var bResult = true;
			reportErr = ($"------OM链路--实例-----{index}----\r\n");
			csCheckError += reportErr;
			foreach (var checkNodePair in mapCheckNode)
			{
				var nodeName = checkNodePair.Key;
				var cname = checkNodePair.Value;
				string value;
				var ret = InOutPdu2.GetValueByMibName(boardAddr, nodeName, out value);
				if (ret && !string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value))
				{
					if (FileNodeValueMap.ContainsKey(nodeName))
					{
						var csFileValue = FileNodeValueMap[nodeName];
						//只显示不同节点值
						if (!csFileValue.Equals(value))
						{
							bResult = false;
							reportErr = $"{nReportLine++} ){nodeName}----{cname} \r\n";
							csCheckError += reportErr;
							reportErr = $"    文件值：{csFileValue} \r\n     基站值：{value} \r\n";
							csCheckError += reportErr;
						}
					}
				}
			}

			if (bResult)
			{
				reportErr = $"------OM链路--实例-----{index}----相同---- \r\n";
				csCheckError += reportErr;
			}

			return bResult;
		}

		private Transfiletype5216 fileType { get; set; }

		#endregion
	}
}

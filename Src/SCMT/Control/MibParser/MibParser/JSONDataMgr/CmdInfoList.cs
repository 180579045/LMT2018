using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MIBDataParser.JSONDataMgr
{
	public class CmdInfoList
	{
		/// <summary>
		/// key (string) : cmd English name
		/// value (Dictionary): {
		///     "TableName":.
		///     "CmdType":
		///     "CmdDesc":命令描述
		///     "leafOIdList" (List):[oid_1,oid_2,...,oid_x]}
		/// </summary>
		private Dictionary<string, CmdMibInfo> cmdInfoNew;

		public bool GeneratedCmdInfoList()
		{
			dynamic cmdJson = new JsonFile().ReadJsonFileForJObject(getCmdJsonFilePath());
			if (null == cmdJson)
			{
				return false;
			}

			cmdInfoNew = new Dictionary<string, CmdMibInfo>();
			foreach (var table in cmdJson)
			{
				cmdInfoNew.Add(table.Name.ToString(), new CmdMibInfo(table.Value, table.Name.ToString()));
			}

			return true;
		}

		public bool getCmdInfoByName(string cmdNameEn, out CmdMibInfo cmdInfo, out string err)
		{
			cmdInfo = null;
			err = "";
			//判断键存在
			if (!cmdInfoNew.ContainsKey(cmdNameEn))
			{
				err = $"cmd db with Key = ({cmdNameEn}) not exists.";
				return false;
			}

			cmdInfo = cmdInfoNew[cmdNameEn];
			return true;
		}

		public bool getCmdInfoByName(Dictionary<string, CmdMibInfo> reData, out string err)
		{
			err = "";
			//判断键存在

			string[] dtKeys = new string[reData.Keys.Count];
			reData.Keys.CopyTo(dtKeys, 0);

			// 查询
			foreach (var cmdNameEn in dtKeys)
			{
				if (!cmdInfoNew.ContainsKey(cmdNameEn))
				{
					err = $"cmd db with Key = ({cmdNameEn}) not exists.";
					return false;
				}

				reData[cmdNameEn] = cmdInfoNew[cmdNameEn];
			}

			return true;
		}

		/// <summary>
		/// 根据表名查询所有的命令信息
		/// </summary>
		/// <param name="strTblName"></param>
		/// <param name="strTargetIp"></param>
		public List<CmdMibInfo> GetCmdsByTblName(string strTblName)
		{
			if (string.IsNullOrEmpty(strTblName))
			{
				throw new ArgumentNullException();
			}

			var cmdsValueList = cmdInfoNew.Values.ToList();
			var cmdInfoList = cmdsValueList.Where(item => item.m_tableName == strTblName).ToList();

			return cmdInfoList;
		}


		private string getCmdJsonFilePath()
		{
			string iniFilePath = ReadIniFile.GetIniFilePath("JsonDataMgr.ini");
			if (string.IsNullOrEmpty(iniFilePath))
			{
				return null;
			}

			string jsonfilepath = ReadIniFile.IniReadValue(iniFilePath, "JsonFileInfo", "jsonfilepath");
			return jsonfilepath + "cmd.json";
		}
	}
}

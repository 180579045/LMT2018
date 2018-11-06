using System;
using System.Collections.Generic;
using System.Text;

// config目录下文件路径整理。全部是相对路径

namespace CommonUtility
{
	public static class ConfigFileHelper
	{
		public const string ObjTreeReferenceJson = @"Data/Tree_Reference.json";
		public const string JsonDataFilePathMgrIni = @"config/JsonDataMgr.ini";
		public const string NodebListJson = @"Data/BaseStationConnectInfo.Json";
		public const string MainConfigJson = @"config/AppConfig.json";
		public const string NetPlanShelfJson = @"config/NetPlanElement_Board.json";
		public const string NetPlanRruJson = @"config/NetPlanElement_RruType.json";
		public const string NetPlanCmdJson = @"config/NetPlanElement_MibCmd.json";
		public const string NetPlanAntJson = @"config/NetPlanElement_AntennaInfo.json";
		public const string DisabledEditMibJson = @"config/DisableEditMib.json";

		/// <summary>
		/// 获取配置文件AppConfig.json中Ftp的路径
		/// 如果获取失败，会返回默认值tools\DTFtpServer.exe
		/// </summary>
		/// <returns></returns>
		public static string GetFtpPath()
		{
			try
			{
				if (null == configObj)
				{
					ParseAppConfig();
					return configObj?.FtpName;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return "tools\\DTFtpServer.exe";
		}

		#region 私有属性和方法

		private static void ParseAppConfig()
		{
			try
			{
				var filePath = FilePathHelper.GetAppPath() + MainConfigJson;
				var fileContent = FileRdWrHelper.GetFileContent(filePath);
				if (!string.IsNullOrEmpty(fileContent))
				{
					configObj = JsonHelper.SerializeJsonToObject<AppConfig>(fileContent);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		private static AppConfig configObj = null;

		#endregion


	}

	// TODO 后面需要加什么配置信息，再进行扩展
	internal class AppConfig
	{
		public string FtpName { get; set; }
	}
}

﻿/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ JsonDataManager $
* 机器名称：       $ machinename $
* 命名空间：       $ MIBDataParser.JSONDataMgr $
* 文 件 名：       $ JsonDataManager.cs $
* 创建时间：       $ 2018.04.XX $
* 作    者：       $ TangYun $
* 说   明 ：
*     JsonDataManager。
* 修改时间     修 改 人    修改内容：
* 2018.04.xx   唐 芸       创建文件并实现类  JsonDataManager
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Threading;
using System.Data;
using CommonUtility;
using System.IO;
using System.Threading.Tasks;
using LogManager;

namespace MIBDataParser.JSONDataMgr
{
	class JsonDataManager
	{
		string mibInfo;
		string objTreeInfo;
		string cmdTreeInfo;
		string mibVersion;
		
		string mdbFile;
		string _jsonFilePath;

		bool isMibJsonOK = false;
		bool isObjJsonOK = false;
		bool isObjJson2OK = false;
		bool isCmdJsonOK = false;
		bool isJsonProtect = false;

		public JsonDataManager(string mibVersion)
		{
            // 配置文件获取
            string err = "";
			string iniFilePath = ReadIniFile.GetIniFilePath("JsonDataMgr.ini", out err);
			if (string.IsNullOrEmpty(iniFilePath))
			{
                Log.Error("JsonDataMgr.ini不存在" + err);
                Console.WriteLine("JsonDataMgr.ini不存在");
				return;
			}

			try
			{
				string appPath = FilePathHelper.GetAppPath();
				string mdbfilePath = appPath + ReadIniFile.IniReadValue(iniFilePath, "ZipFileInfo", "mdbfilePath");
				mdbFile = mdbfilePath + "lm.mdb";
				_jsonFilePath = appPath + ReadIniFile.IniReadValue(iniFilePath, "JsonFileInfo", "jsonfilepath");
				this.mibVersion = mibVersion;
			}
			catch (Exception ex)
			{
				Console.WriteLine("读取ini文件({0})时,{1}", iniFilePath, ex.Message);
			}
		}

		/// <summary>
		/// lm.dtz解析生成json文件，保存当前基站的对象树及MIB信息
		/// </summary>
		/// <param name="fileName"></param>
		public void ConvertAccessDbToJson()//string fileName,string mibJsonPath, string ojbJsonPath)
		{
			Console.WriteLine("begin to parse mdb file, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));

			ConvertAccessDbToJsonMibTree();// 查询MibTree database
			ConvertAccessDbToJsonObjTree();// 对象树转换生成json文件
			ConvertAccessDbToJsonCmdTree();// cmdTree命令树 转换生成json文件

			Console.WriteLine("end to parse mdb file, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
		}
		public async Task<bool> ConvertAccessDbToJsonForThread()//string fileName,string mibJsonPath, string ojbJsonPath)
		{
			isMibJsonOK = false;
			isObjJsonOK = false;
			isObjJson2OK = false;
			isCmdJsonOK = false;
			isJsonProtect = false;

            Log.Debug("Db init : Start to parse mdb file, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

            Console.WriteLine("begin to parse mdb file, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
			Thread[] threads = {
				 new Thread(ConvertAccessDbToJsonMibTree),// "MibTree"
				 //new Thread(new ThreadStart(ConvertAccessDbToJsonObjTree)),// "ObjTree"
				 new Thread(ConvertAccessDbToJsonCmdTree),// "CmdTree"
				 new Thread(ConvertAccessDbToJsonTreeReference),//与"ObjTree"类似
				 new Thread(ConvertAccessDbToJsonProtect),//线程保护机制
			};

			//var taskLists = new List<Task>
			//{
			//	new Task(ConvertAccessDbToJsonMibTree),
			//	new Task(ConvertAccessDbToJsonCmdTree),
			//	new Task(ConvertAccessDbToJsonTreeReference)
			//};

			//try
			//{
			//	await Task.WhenAll(taskLists);
			//}
			//catch (Exception e)
			//{
			//	Console.WriteLine(e);
			//	return false;
			//}
			//return true;
			foreach (var t in threads)
			{
				t.Start();
			}

			while (true)
			{
				//if (isJsonProtect)
				//{
					foreach (var t in threads)
					{
						if (t.IsAlive)
						{
							try
							{
								t.Join();
							}
							catch (ThreadStateException e)
							{
                                Log.Error("Db init err: " + e);

                                Console.WriteLine(e);
							}
						}
					}
				return true;
				//}
			}

			return true;
		}

		private void JsonFileWrite(string fileName, string content)
		{
			new JsonFile().WriteFile(fileName, content);
		}

		/// <summary>
		/// 解析lm.dtz 写 mib.json
		/// </summary>
		private void ConvertAccessDbToJsonMibTree()
		{
			//Console.WriteLine("DbToJsonMibTree start " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
			string sqlContent = "select * from MibTree order by OID asc";
			DataSet dataSet = GetRecordByAccessDb(this.mdbFile, sqlContent);
			MibJsonData mibJsonDatat = new MibJsonData(this.mibVersion);
			mibJsonDatat.MibParseDataSet(dataSet);//按格式解析

			// 把解析内容写成json文件
			JsonFileWrite(_jsonFilePath + "mib.json", mibJsonDatat.GetStringMibJson());
			this.mibInfo = mibJsonDatat.GetStringMibJson();

			isMibJsonOK = true;
			//Console.WriteLine("DbToJson : MibTree end " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
		}
		
		/// <summary>
		/// 解析lm.dtz 写 obj.json
		/// </summary>
		public void ConvertAccessDbToJsonObjTree()
		{
			//Console.WriteLine("DbToJsonObjTree start " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
			string sqlContent = "select * from ObjTree order by ObjExcelLine";
			DataSet dataSet = GetRecordByAccessDb(mdbFile, sqlContent);
			ObjTressJsonData objTreeJson = new ObjTressJsonData();
			objTreeJson.ObjParseDataSet(dataSet);

			JsonFileWrite(_jsonFilePath + "obj.json", objTreeJson.GetStringObjTreeJson());
			this.objTreeInfo = objTreeJson.GetStringObjTreeJson();

			isObjJsonOK = true;
			//Console.WriteLine("DbToJsonObjTree end " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
		}

		/// <summary>
		/// 解析lm.dtz 写 TreeReference.json
		/// </summary>
		public void ConvertAccessDbToJsonTreeReference()
		{
			string sqlContent = "select * from ObjTree order by ObjExcelLine";
			DataSet dataSet = GetRecordByAccessDb(mdbFile, sqlContent);
			ObjTressJsonData objTreeJson = new ObjTressJsonData(this.mibVersion);
			objTreeJson.TreeReferenceParseDataSet(dataSet);

			JsonFileWrite(_jsonFilePath + "Tree_Reference.json", objTreeJson.GetStringTreeReference());

			isObjJson2OK = true;
			//Console.WriteLine("DbToJson : TreeReference end " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
			ConvertFileEncoding(_jsonFilePath + "Tree_Reference.json", _jsonFilePath + "Tree_Collect.json", System.Text.Encoding.UTF8);
		}
		private void FileCopy(string path, string path2)
		{
			if (File.Exists(path2))
			{
				File.Delete(path2);
			}
			File.Copy(path, path2, true);
		}
		static void ConvertFileEncoding(string sourceFile, string destFile, System.Text.Encoding targetEncoding)
		{
			destFile = string.IsNullOrEmpty(destFile) ? sourceFile : destFile;
			System.IO.File.WriteAllText(destFile,
			System.IO.File.ReadAllText(sourceFile, System.Text.Encoding.Default),
			targetEncoding);
		}
		/// <summary>
		/// 解析lm.dtz 写 cmd.json
		/// </summary>
		public void ConvertAccessDbToJsonCmdTree()
		{
			//Console.WriteLine("DbToJsonCmdTree start " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
			//生产 cmdTree 命令树文件
			string sqlContent = "select * from CmdTree order by CmdID";
			DataSet dataSet = GetRecordByAccessDb(mdbFile, sqlContent);
			CmdTreeJsonData cmdJsonDatat = new CmdTreeJsonData();
            //cmdJsonDatat.CmdParseDataSet(dataSet);
            cmdJsonDatat.CmdParseDataSetVersion2(dataSet);

            JsonFile jsonObjFile = new JsonFile();
			//jsonObjFile.WriteFile("D:\\C#\\SCMT\\obj.json", objTreeJson.GetStringObjTreeJson());
			jsonObjFile.WriteFile(_jsonFilePath + "cmd.json", cmdJsonDatat.GetStringObjTreeJson());
			this.cmdTreeInfo = cmdJsonDatat.GetStringObjTreeJson();

			isCmdJsonOK = true;
			//Console.WriteLine("DbToJson : CmdTree end " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
			return;
		}

		/// <summary>
		/// 线程保护机制，默认3秒可以全部处理完写文件，超过3秒自己退出
		/// </summary>
		private void ConvertAccessDbToJsonProtect()
		{
			//Thread.Sleep(3000);
			isJsonProtect = true;
			Console.WriteLine("DbToJson : Thread Protect Timer over, err.");
		}

		/// <summary>
		/// 打开数据库,进行查询
		/// </summary>
		/// <param name="fileName">完整文件名</param>
		/// <param name="sqlContent">sql查询语句</param>
		/// <returns>DataSet结果</returns>
		private DataSet GetRecordByAccessDb(string fileName, string sqlContent)
		{
			//fileName = "D:\\C#\\SCMT\\lm.mdb";
			DataSet dateSet;
			//To do:将lm.dtz改名lm.rar,再解压缩,再改名成为mdb
			var mdbData = new AccessDBManager(fileName);
			try
			{
				mdbData.Open();
				dateSet = mdbData.GetDataSet(sqlContent);
			}
			finally
			{
				mdbData.Close();
			}
			return dateSet;
		}
	}
}

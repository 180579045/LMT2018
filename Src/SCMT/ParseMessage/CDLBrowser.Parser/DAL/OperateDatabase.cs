// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperateDatabase.cs" company="dtmobile">
//   dtmobile
// </copyright>
// <summary>
//   Defines the OperateDatabase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CDLBrowser.Parser.DatabaseMgr;
using System.Data;

namespace CDLBrowser.Parser.DAL
{
    using System;
    using System.Data.SQLite;
    using System.IO;
    using System.Windows;

    using CDLBrowser.Parser.Document.Event;

    using Common.Logging;

    using DevExpress.Xpo;
    using DevExpress.Xpo.DB;

    using Microsoft.Win32;

    using SuperLMT.Utils;

    /// <summary>
    /// The operate database.
    /// </summary>
    public class OperateDatabase
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(OperateDatabase));
		private DbConn dbConn;
		public OperateDatabase(DbConn dbc)
		{
			if(dbc == null)
			   return;
			   
			dbConn = dbc.Clone();
			if(dbConn == null)
			{
				Log.Error("OperateDatabase.icor, dbConn is null");
			}
		
		}
        #region 离线导出日志

		

        /// <summary>
        /// The cdl export log.
        /// </summary>
        /// <param name="filterString">
        /// The filter String.
        /// </param>
        public  void CdlExportLog(string filterString)
        {
            var saveDialog = new SaveFileDialog();
            if (filterString != null)
            {
                filterString = "WHERE " + filterString;
            }
            /*
            try
            {
                saveDialog.Title = "Log Save";
                saveDialog.Filter = "文件类型(*.db3)|*.db3";

                string strTempFilePath = GetTempDb3FilePath();
                var save = (bool)saveDialog.ShowDialog();
                if (save)
                {
                    
                    string strExecute = string.Format("SELECT OID,RawData,EventIdentifier,DisplayIndex,Version,TimeStamp,Protocol,EventName,MessageSource,MessageDestination,HalfSubFrameNo,HostNeid,LocalCellId,CellId,CellUeId,TickTime,OptimisticLockField,GCRecord FROM Event {0}", filterString);
                    SelectedData seleData = new DALManager().GetSession().ExecuteQuery(strExecute);

                   
                    string strFilePath = string.Format("Data Source={0};Version=3;", strTempFilePath);
                    var sqLiteConnection = new SQLiteConnection(strFilePath);
                    SQLiteCommand cdlCmd;
                    if (!File.Exists(strTempFilePath))
                    {
                       
                        SQLiteConnection.CreateFile(strTempFilePath);
                        
                        sqLiteConnection.Open();
                        cdlCmd = new SQLiteCommand("CREATE TABLE CDLEvent(OID INTEGER,RawData image,EventIdentifier INTEGER,DisplayIndex INTEGER,Version nvarchar(100),TimeStamp nvarchar(100),Protocol nvarchar(100),EventName nvarchar(100),MessageSource nvarchar(100),MessageDestination nvarchar(100),HalfSubFrameNo INTEGER,HostNeid INTEGER,LocalCellId INTEGER,CellId INTEGER,CellUeId INTEGER,TickTime INTEGER,OptimisticLockField nvarchar(100),GCRecord nvarchar(100),LogName nvarchar(100))", sqLiteConnection);
                    }
                    else
                    {
                        sqLiteConnection.Open();
                        cdlCmd = new SQLiteCommand("DELETE FROM CDLEvent", sqLiteConnection);
                    }

                    cdlCmd.ExecuteNonQuery();
                    SQLiteTransaction tran = sqLiteConnection.BeginTransaction();
                    cdlCmd.Transaction = tran;

                    int num = saveDialog.FileName.LastIndexOf('\\');
                    string logName = saveDialog.FileName.Substring(num, saveDialog.FileName.Length - num);
                    logName = logName.Replace("\\", string.Empty);

                    foreach (SelectStatementResult selectResult in seleData.ResultSet)
                    {
                        foreach (SelectStatementResultRow rowItem in selectResult.Rows)
                        {
                            cdlCmd.CommandText =
                                "insert into CDLEvent values(@OID,@RawData,@EventIdentifier,@DisplayIndex,@Version,@TimeStamp,@Protocol,@EventName,@MessageSource,@MessageDestination,@HalfSubFrameNo,@HostNeid,@LocalCellId,@CellId,@CellUeId,@TickTime,@OptimisticLockField,@GCRecord,@LogName)";
                            cdlCmd.Parameters.AddWithValue("@OID", Convert.ToInt64(rowItem.Values[0]));
                            cdlCmd.Parameters.AddWithValue("@RawData", rowItem.Values[1]);
                            cdlCmd.Parameters.AddWithValue("@EventIdentifier", Convert.ToUInt16(rowItem.Values[2]));
                            cdlCmd.Parameters.AddWithValue("@DisplayIndex", Convert.ToUInt16(rowItem.Values[3]));
                            cdlCmd.Parameters.AddWithValue("@Version", rowItem.Values[4].ToString());
                            cdlCmd.Parameters.AddWithValue("@TimeStamp", rowItem.Values[5].ToString());
                            cdlCmd.Parameters.AddWithValue("@Protocol", rowItem.Values[6].ToString());
                            cdlCmd.Parameters.AddWithValue("@EventName", rowItem.Values[7].ToString());
                            cdlCmd.Parameters.AddWithValue("@MessageSource", rowItem.Values[8].ToString());
                            cdlCmd.Parameters.AddWithValue("@MessageDestination", rowItem.Values[9].ToString());
                            cdlCmd.Parameters.AddWithValue("@HalfSubFrameNo", Convert.ToInt32(rowItem.Values[10]));
                            cdlCmd.Parameters.AddWithValue("@HostNeid", Convert.ToInt32(rowItem.Values[11]));
                            cdlCmd.Parameters.AddWithValue("@LocalCellId", Convert.ToInt32(rowItem.Values[12]));
                            cdlCmd.Parameters.AddWithValue("@CellId", Convert.ToInt32(rowItem.Values[13]));
                            cdlCmd.Parameters.AddWithValue("@CellUeId", Convert.ToInt32(rowItem.Values[14]));
                            cdlCmd.Parameters.AddWithValue("@TickTime", Convert.ToUInt64(rowItem.Values[15]));
                            cdlCmd.Parameters.AddWithValue("@OptimisticLockField", Convert.ToInt32(rowItem.Values[16]));
                            cdlCmd.Parameters.AddWithValue("@GCRecord", Convert.ToInt32(rowItem.Values[17]));
                            cdlCmd.Parameters.AddWithValue("@LogName", logName);
                            cdlCmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    sqLiteConnection.Close();

                    File.Copy(strTempFilePath, saveDialog.FileName, true);
                    MessageBox.Show("CDL离线日志导出成功！", "Export Log");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }*/
        }

        #endregion

        #region   离线导入数据库

        /// <summary>
        /// The cdl database parser.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public  void CdlDatabaseParser(string fileName)
        {
            if (!CheckLogList(fileName, "CDL"))
            {
                return;
            }
            /* --yanjiewa 2014/12/11 
           // string filePath = string.Format("Data Source={0};Version=3;", fileName);
            //var connection = new SQLiteConnection(filePath);
            //connection.Open();

            
            string strCommand = string.Format("SELECT * FROM CDLEvent");
            var cmd = new SQLiteCommand(strCommand, connection);
            int i = 0;

            using (var databaseSession = new UnitOfWork(XpoDefault.DataLayer))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var curUeInfo = new Event()
                        {
                            Oid = Convert.ToInt32(reader["OID"]),
                            RawData = (byte[])reader["RawData"],

                            // TODO：将获取目前parseId和LogId的值加一后赋给新导入的db文件
                            //ParsingId = ,
                            //LogFileId = ,

                            EventIdentifier = Convert.ToInt32(reader["EventIdentifier"]),
                            DisplayIndex = Convert.ToInt32(reader["DisplayIndex"]),
                            Version = reader["Version"].ToString(),
                            TimeStamp = reader["TimeStamp"].ToString(),
                            Protocol = reader["Protocol"].ToString(),
                            EventName = reader["EventName"].ToString(),
                            MessageSource = reader["MessageSource"].ToString(),
                            MessageDestination = reader["MessageDestination"].ToString(),
                            HalfSubFrameNo = Convert.ToInt32(reader["HalfSubFrameNo"]),
                            HostNeid = Convert.ToUInt32(reader["HostNeid"]),
                            LocalCellId = Convert.ToUInt16(reader["LocalCellId"]),
                            CellId = Convert.ToUInt16(reader["CellId"]),
                            CellUeId = Convert.ToUInt16(reader["CellUeId"]),
                            TickTime = Convert.ToUInt64(reader["TickTime"]),
                            LogName = Convert.ToString(reader["LogName"])
                        };
                        i++;
                    }
                }

                int eventNum = i;
                Log.Info(string.Format("共导入：{0}条信息", eventNum));
                databaseSession.CommitChanges();
            }*/
        }

        #endregion 
        
        #region  在线导出日志

        /// <summary>
        /// The LST export log.
        /// </summary>
        public  void LstExportLog()
        {
            /*
            var saveDialog = new SaveFileDialog();
            try
            {
                saveDialog.Title = "Log Save";
                saveDialog.Filter = "文件类型(*.db3)|*.db3";

                string strTempFilePath = GetTempDb3FilePath();
                var save = (bool)saveDialog.ShowDialog();
                if (save)
                {
                    
                    string strExecute = string.Format("SELECT OID,IsMarked,DeviceType,TrcNEGlobalId,RemoteIpAddress,TaskID,TaskSetId,TraceType,SequenceID,TimeStamp,TrcMsgInterfaceType,TrcMsgDirect,CellId,SctpIndex,MessageName,SourceNet,DestinationNet,MessageData,OptimisticLockField,GCRecord FROM SignalingEvent WHERE TaskID={0}", 64);
                    DataTable seleData = dbConn.QueryGetDataTable(strExecute);

                   
                    string strFilePath = string.Format("Data Source={0};Version=3;", strTempFilePath);
                    var cn = new SQLiteConnection(strFilePath);
                    SQLiteCommand cmd;
                    if (!File.Exists(strTempFilePath))
                    {
                        
                        SQLiteConnection.CreateFile(strTempFilePath);
                        
                        cn.Open();
                        cmd = new SQLiteCommand("CREATE TABLE OnLineEvent(LogName, OID INTEGER,IsMarked INTEGER,DeviceType nvarchar(100),TrcNEGlobalId INTEGER,RemoteIpAddress nvarchar(100),TaskID INTEGER,TaskSetId INTEGER,TraceType nvarchar(100),SequenceID INTEGER,TimeStamp nvarchar(100),TrcMsgInterfaceType INTEGER,TrcMsgDirect nvarchar(100),CellId INTEGER,SctpIndex INTEGER,MessageName nvarchar(100),SourceNet nvarchar(100),DestinationNet nvarchar(100),MessageData image,OptimisticLockField nvarchar(100),GCRecord nvarchar(100))", cn);
                    }
                    else
                    {
                        cn.Open();
                        cmd = new SQLiteCommand("DELETE FROM OnLineEvent", cn);
                    }

                    cmd.ExecuteNonQuery();
                    SQLiteTransaction tran = cn.BeginTransaction();
                    cmd.Transaction = tran;

                    int num = saveDialog.FileName.LastIndexOf('\\');
                    string logName = saveDialog.FileName.Substring(num, saveDialog.FileName.Length - num);
                    logName = logName.Replace("\\", string.Empty);

                    // 将TaskSetID设置为100，避免重复
                    foreach (SelectStatementResult selectResult in seleData.ResultSet)
                    {
                        foreach (SelectStatementResultRow rowItem in selectResult.Rows)
                        {
                            cmd.CommandText =
                                "insert into OnLineEvent values(@LogName,@OID,@IsMarked,@DeviceType,@TrcNEGlobalId,@RemoteIpAddress,@TaskID,@TaskSetId,@TraceType,@SequenceID,@TimeStamp,@TrcMsgInterfaceType,@TrcMsgDirect,@CellId,@SctpIndex,@MessageName,@SourceNet,@DestinationNet,@MessageData,@OptimisticLockField,@GCRecord)";
                            cmd.Parameters.AddWithValue("@LogName", logName);
                            cmd.Parameters.AddWithValue("@OID", Convert.ToInt64(rowItem.Values[0]));
                            cmd.Parameters.AddWithValue("@IsMarked", Convert.ToInt64(rowItem.Values[1]));
                            cmd.Parameters.AddWithValue("@DeviceType", rowItem.Values[2].ToString());
                            cmd.Parameters.AddWithValue("@TrcNEGlobalId", Convert.ToUInt16(rowItem.Values[3]));
                            cmd.Parameters.AddWithValue("@RemoteIpAddress", rowItem.Values[4].ToString());
                            cmd.Parameters.AddWithValue("@TaskID", Convert.ToUInt16(rowItem.Values[5]));
                            cmd.Parameters.AddWithValue("@TaskSetId", 100);
                            cmd.Parameters.AddWithValue("@TraceType", rowItem.Values[7].ToString());
                            cmd.Parameters.AddWithValue("@SequenceID", Convert.ToUInt16(rowItem.Values[8]));
                            cmd.Parameters.AddWithValue("@TimeStamp", rowItem.Values[9].ToString());
                            cmd.Parameters.AddWithValue("@TrcMsgInterfaceType", Convert.ToUInt16(rowItem.Values[10]));
                            cmd.Parameters.AddWithValue("@TrcMsgDirect", rowItem.Values[11].ToString());
                            cmd.Parameters.AddWithValue("@CellId", Convert.ToInt32(rowItem.Values[12]));
                            cmd.Parameters.AddWithValue("@SctpIndex", Convert.ToInt32(rowItem.Values[13]));
                            cmd.Parameters.AddWithValue("@MessageName", rowItem.Values[14].ToString());
                            cmd.Parameters.AddWithValue("@SourceNet", rowItem.Values[15].ToString());
                            cmd.Parameters.AddWithValue("@DestinationNet", rowItem.Values[16].ToString());
                            cmd.Parameters.AddWithValue("@MessageData", rowItem.Values[17]);
                            cmd.Parameters.AddWithValue("@OptimisticLockField", Convert.ToInt32(rowItem.Values[18]));
                            cmd.Parameters.AddWithValue("@GCRecord", rowItem.Values[19]);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    cn.Close();

                    File.Copy(strTempFilePath, saveDialog.FileName, true);
                    MessageBox.Show("日志导出成功！", "Export Log");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
*/
        }

        #endregion

        #region   在线导入数据库

        /// <summary>
        /// The LST database 3 file parser.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public  void LstDb3FileParser(string fileName)
        {
            if (!CheckLogList(fileName, "LST"))
            {
                return;
            }
            /*
            string filePath = string.Format("Data Source={0};Version=3;", fileName);
            var connection = new SQLiteConnection(filePath);
            connection.Open();

            
            string strCommand = string.Format("SELECT * FROM OnLineEvent");
            var cmd = new SQLiteCommand(strCommand, connection);
            int i = 0;

            using (var databaseSession = new UnitOfWork(XpoDefault.DataLayer))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var curUeInfo = new SignalingEvent(databaseSession)
                        {
                            LogName = Convert.ToString(reader["LogName"]),
                            Oid = Convert.ToUInt16(reader["OID"]),
                            IsMarked = Convert.ToBoolean(reader["IsMarked"]),
                            DeviceType = reader["DeviceType"].ToString(),
                            TrcNEGlobalId = Convert.ToUInt16(reader["TrcNEGlobalId"]),
                            RemoteIpAddress = reader["RemoteIpAddress"].ToString(),
                            TaskId = Convert.ToUInt16(reader["TaskID"]),
                            TaskSetId = Convert.ToUInt16(reader["TaskSetId"]),
                            TraceType = reader["TraceType"].ToString(),
                            SequenceId = Convert.ToUInt16(reader["SequenceID"]),
                            TimeStamp = reader["TimeStamp"].ToString(),
                            TrcMsgInterfaceType = Convert.ToUInt16(reader["TrcMsgInterfaceType"]),
                            TrcMsgDirect = reader["TrcMsgDirect"].ToString(),
                            CellId = Convert.ToUInt16(reader["CellId"]),
                            SctpIndex = Convert.ToUInt16(reader["CellId"]),
                            MessageName = reader["MessageName"].ToString(),
                            SourceNet = reader["SourceNet"].ToString(),
                            DestinationNet = reader["DestinationNet"].ToString(),
                            MessageData = (byte[])reader["MessageData"]
                        };
                        i++;
                    }
                }

                int eventNum = i;
                Log.Info(string.Format("共导入：{0}条信息", eventNum));
                databaseSession.CommitChanges();
            }*/
        }

        #endregion 

        /// <summary>
        /// The get temp database file path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private  string GetTempDb3FilePath()
        {
            string strFilePath = @"temp.db3";
            strFilePath = AppPathUtiliy.Singleton.GetAppPath() + strFilePath;
            return strFilePath;
        }

        /// <summary>
        /// The add log list.
        /// </summary>
        /// <param name="logName">
        /// The log name.
        /// </param>
        /// <param name="tableName">
        /// The table Name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// 判断SignalingEvent表中是否已保存要导入的log
        private  bool CheckLogList(string logName, string tableName)
        {
            /* 2014/12/11 yanjiewa removed
            try
            {
                string sqlString;
                int num = logName.LastIndexOf('\\');
                string log = logName.Substring(num, logName.Length - num);
                log = log.Replace("\\", string.Empty);
                log = "'" + log + "'";
                sqlString = string.Format(tableName == "LST" ? "SELECT COUNT(*) FROM SignalingEvent WHERE LogName = {0}" : "SELECT COUNT(*) FROM Event WHERE LogName = {0}", log);

                object result = new DALManager().GetSession().ExecuteScalar(sqlString);
                if (Convert.ToInt32(result) == 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.Error("ChcekTable error,table name = " + logName + "error info = " + ex.Message);
                return false;
            }*/

            return false;
        }
    }
}


#define USE_MYSQL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDLBrowser.Parser.DatabaseMgr
{
    public class DbConnProvider
    {
        //默认sql数据库名称
        public const string DefaultSqlDatabaseName = "cdl";
        public const string DefaultSqliteDatabaseName = "cdl.db";
        public const string DefaultSqliteMemDatabaseName = ":memory:";
        public static string [] AllTableNames = new string[]
                                              {
                                                  "ApHungUpRecord",
                                                  "CrntiHungUpRecord",
                                                  "DropCallRecord",
                                                  "Event",
                                                  "ErabAccessRecord",
                                                  "HandOverRecord",
                                                  "ReEstablishRecord",
                                                  "RRCAccessRecord",
                                                  "RRCRRMHungUpRecord",
                                                  "RandomAccessRecord",
                                                  "S1HandoverRecord",
                                                  "UeAccessRecord",
                                                  "X2HandoverRecord"
                                              };
        public static string GetDefaultMysqlConnString()
        {
            string connstr;
            //connstr = DbConnMysql.GetConnectString("localhost", "root", "123456", "cdl");
            connstr = DbConnMysql.GetConnectString("localhost", "root", "", "cdl");
            //connstr = DbConnMysql.GetConnectString("192.168.0.254", "Test", "123456", "cdl");
            //connstr = DbConnMysql.GetConnectString("192.168.0.188", "remote", "123456", "cdl");
            return connstr;
        }

        public static string GetDefaultSqliteConnString()
        {
            string connstr;
            //connstr = DbConnMysql.GetConnectString("localhost", "root", "123456", "cdl");
            connstr = DbConnSqlite.GetConnectString(DefaultSqliteDatabaseName);
            //connstr = DbConnMysql.GetConnectString("192.168.0.254", "Test", "123456", "cdl");
            //connstr = DbConnMysql.GetConnectString("192.168.0.188", "remote", "123456", "cdl");
            return connstr;
        }

        public static DbConn CreateNewDbConn()
        {
            DbConn dbconn;

#if USE_SQLITE
                string strTempFilePath = "cdl.db";
                //写入内存
                //strTempFilePath = ":memory:";
                string connstr = string.Format("Data Source={0};Version=3;", strTempFilePath);
                try{
                    dbconn = new DbConn(DbType.SQLite);
                    dbconn.Connect(connstr);
                }
                catch(Exception err)
                {
                    MyLog.Log("err = " + err.ToString());
                    dbconn = null;
                }
                return dbconn;

#elif USE_MYSQL
            string connstr = GetDefaultMysqlConnString();
           
            try
            {
                dbconn = new DbConn(DbType.Mysql,connstr);
            }
            catch (Exception err)
            {
                MyLog.Log("err = " + err.ToString());
                dbconn = null;
            }

            return dbconn;
#endif
        }

        //for sqlite
        public static string GetCreateDBEventTableSQLForSqlite()
        {
            string sql = "CREATE TABLE [Event] ( " +
                "[OID] INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "[IsMarked] bit, " +
                "[RawData] image NULL, " +
                "[ParsingId] int NULL, " +
                "[LogFileId] int NULL, " +
                "[EventIdentifier] int NULL, " +
                "[DisplayIndex] int NULL, " +
                "[Version] nvarchar(100) NULL, " +
                "[TimeStamp]nvarchar(100) NULL, " +
                "[Protocol] nvarchar(100) NULL, " +
                "[EventName] nvarchar(100) NULL, " +
                "[MessageSource] nvarchar(100) NULL, " +
                "[MessageDestination] nvarchar(100) NULL," +
                "[HalfSubFrameNo] int NULL, " +
                "[HostNeid] numeric(10,0) NULL, " +
                "[LocalCellId] numeric(5,0) NULL, " +
                "[CellId] numeric(5,0) NULL, " +
                "[CellUeId] numeric(5,0) NULL, " +
                "[TickTime]numeric(20,0) NULL, " +
                "[LogName] nvarchar(100) NULL, " +
                "[UeType] nvarchar(100) NULL, " +
                "[OptimisticLockField] int NULL, " +
                "[GCRecord] int NULL," +
                "[IsDeleted] bit DEFAULT 0" +
                ");";

            return sql;
        }
		
		//no use
        public static string GetCreateDBEventTableSQLForMysql()
        {
            string sql = "CREATE TABLE [Event] ([OID] INTEGER PRIMARY KEY AUTOINCREMENT, [IsMarked] bit, " +
                "[RawData] image NULL, [ParsingId] int NULL, [LogFileId] int NULL, [EventIdentifier] int NULL, [DisplayIndex] int NULL, [Version] nvarchar(100) NULL, " +
                "[TimeStamp] nvarchar(100) NULL, [Protocol] nvarchar(100) NULL, [EventName] nvarchar(100) NULL, " +
                "MessageSource] nvarchar(100) NULL, [MessageDestination] nvarchar(100) NULL, " +
                "[HalfSubFrameNo] int NULL, [HostNeid] numeric(10,0) NULL, [LocalCellId] numeric(5,0) NULL, " +
                "[CellId] numeric(5,0) NULL, [CellUeId] numeric(5,0) NULL, [TickTime] numeric(20,0) NULL, " +
                "[LogName] nvarchar(100) NULL, [UeType] nvarchar(100) NULL, [OptimisticLockField] int NULL, [GCRecord] int NULL,[IsMarked] bit DEFAULT 0)";

            return sql;
        }

    }
}

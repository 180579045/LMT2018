 
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DALManager.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the DataAccessLayer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.DAL
{
    using System;
    using System.Data;
    using System.Reflection;
    using MySql.Data.MySqlClient;
    using Common.Logging;
    using Parser.Document.Event;
    using DevExpress.Xpo;
    using DevExpress.Xpo.DB;
	using System.Data.SQLite;
	using System.Diagnostics;
	using System.Threading;
    using CDLBrowser.Parser.DatabaseMgr;

    /// <summary>
    /// The data access layer.
    /// </summary>
    public class DALManager 
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(DALManager));

        /// <summary>
        /// The DataAccessLayer instance.
        /// </summary>
       // private static DALManager instance;
       // private IDataLayer datalayer;

        /// <summary>
        /// The db operation
        /// </summary>
        /// //yanjiewa delete 2014/11/28
        //private IDbConnection cn;

        //private IDbTransaction tran;

        //private IDbCommand command;
        private DbConn dbConn;

        /// <summary>
        /// The persist session.
        /// </summary>
        private Session persistSession;

        /// <summary>
        /// The db name 数据库名称
        /// </summary>
        private static string dbname = "cdl";

        //数据库是否OK
        public bool IsInit = false;

        /// <summary>
        /// The db type 数据库类型, "mysql", "sqlite"，当初始化之前设置，会按照指定类型进行初始化
        /// </summary>
        
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DbConnectString { get; set; }

        
        /// <summary>
        /// Set the db name
        /// </summary>
        public static string DbName
        {
            set
            {
                dbname = value;
            }
            get
            {
                return dbname.Trim();
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DALManager"/> class from being created.
        /// </summary>
        public DALManager(DbConn dbc)
        {
            //remove by deyu 2014-10-13 new 的时候不能调用初始化，需要设置其必须的参数值
            //Initialize();
            if (dbc == null)
            {
                return;
            }
			dbConn = dbc.Clone();
			if(dbConn == null)
			{
				Log.Error("Error:DALManager.icor. dbconn is null");
			}
			
        }
		
	   public DALManager()
        {
            //remove by deyu 2014-10-13 new 的时候不能调用初始化，需要设置其必须的参数值
            //Initialize();

			
        }
		
        /// <summary>
        /// 
        /// </summary>
         ~DALManager()
        {
            try
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("~DALManager error,error info = " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the persist session.
        /// </summary>
        public Session PersistSession
        {
            get
            {
                if (this.persistSession == null)
                {
                    this.persistSession = new Session(XpoDefault.DataLayer);
                }

                return this.persistSession;
            }
        }

        /// <summary>
        /// The get singleton.
        /// </summary>
        /// <returns>
        /// The <see cref="DALManager"/>.
        /// </returns>
        //public static DALManager GetSingleton()
        //{
        //        if (ReferenceEquals(instance, null))
        //        {
        //            instance = new DALManager();
        //            //instance.Initialize();
        //        }

        //        return instance;
        //}

        /// <summary>
        /// The get session.
        /// </summary>
        /// <returns>
        /// The <see cref="Session"/>.
        /// </returns>
       // public Session GetSession()
       // {
       //     return new Session(datalayer);
       // }

        /// <summary>
        /// The get unit of work.
        /// </summary>
        /// <returns>
        /// The <see cref="UnitOfWork"/>.
        /// </returns>
        public UnitOfWork GetUnitOfWork()
        {
            //return new UnitOfWork(datalayer);
            return null;
        }

        /// <summary>
        /// The delete table.
        /// </summary>
        /// <param name="tableName">
        /// The table name.
        /// </param>
        public void DeleteTable(string tableName)
        {
            if (Thread.CurrentThread.IsBackground == true)
            {
                try
                {
                    if (!IsInit)
                    {
                        Debug.Assert(false, "数据库未初始化");
                        return;
                    }
                    if (dbConn == null)
                    {
                        return;
                    }

                   // if (cn.State != System.Data.ConnectionState.Open)
                    //    this.Initialize();
                    string sql = string.Format("DELETE FROM {0}", tableName);
                   // command.CommandText = string.Format("DELETE FROM {0}", tableName);
                   // command.ExecuteNonQuery();

                    dbConn.ExcuteNonQuery(sql);
                }
                catch (Exception ex)
                {
                    Log.Error("DeleteTable error,table name = " + tableName + "error info = " + ex.Message);
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbCommand BeginTransaction()
        {
            if (dbConn != null)
            {
               return dbConn.BeginTransaction();
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        public void CommitChanges(IDbCommand dbcmd)
        {
            if (dbConn != null)
            {
                dbConn.CommitChanges(dbcmd);
                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        public void AddEvent(Event evt,IDbCommand sqlcmd)
        {
            Type classinfo = typeof(Event);
            //创建对应表 --hubing 2014-10-31
           // if (CreateTable(classinfo) != 1)
             //   return;
            //[] for debug
            string tableName = "Event";
            try
            {
                
                //string sql = string.Format("INSERT INTO  {0}(" +
                //    "IsMarked, RawData, ParsingId, LogFileId, EventIdentifier," +
                //    "DisplayIndex,"+ "Version, TimeStamp, Protocol, EventName, " +
                //    "MessageSource, MessageDestination," +
                //    "HalfSubFrameNo, HostNeid, LocalCellId, CellId," + "CellUeId," +
                //    "TickTime,LogName,UeType,OptimisticLockField,GCRecord) VALUES(" + 
                //    (evt.IsMarked ? 1 : 0) + ",@RawData," +evt.ParsingId + "," + evt.LogFileId + "," + evt.EventIdentifier + "," + 
                //    evt.DisplayIndex + ",'" + evt.Version + "','" + evt.TimeStamp + "','" + evt.Protocol + "','" + evt.EventName + "','" +
                //    evt.MessageSource + "','" + evt.MessageDestination + "'," + 
                //    evt.HalfSubFrameNo +"," + evt.HostNeid + "," + evt.LocalCellId + "," + evt.CellId + "," + evt.CellUeId + "," + 
                //    evt.TickTime + ",'" + evt.LogName + "','"+ evt.UeType + "'," + 0 + "," + 0 + ")", tableName);
                
                string sql = dbConn.CreateInsertSqlFromObject(typeof (Event), evt, "Event", true);
                IDbDataParameter dbparameter = null;
                if (dbConn.mDbType == DatabaseMgr.DbType.Mysql)
                {
                    dbparameter = new MySqlParameter("@RawData", evt.RawData);
                }
                else
                {
                    dbparameter = new SQLiteParameter("@RawData", evt.RawData);
                }

                IDbDataParameter[] paramsArray = new IDbDataParameter[1];
                paramsArray[0] = dbparameter;
                if (dbConn != null)
                {
                    dbConn.ExecuteWithParamtersByTrans(sql,paramsArray,sqlcmd);
                }
              
            }
            catch (Exception ex)
            {
                Log.Error("INSERT error,table name = " + tableName + "error info = " + ex.Message);
            }
        }

        public void CommitToDbByTrans(string sql,IDbCommand sqlcmd)
        {
            if (dbConn != null)
            {
                dbConn.ExcuteByTrans(sql, sqlcmd);
            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable LoadEvent(string filter)
        {/* 2017-09-05 delete
            if (dbConn == null)
            {
                Log.Error("ERROR:LoadEvent, dbconn is null");
                return null;
            }
            DataTable dt = null;
            try
            {
                string sql = "select * from event where " + filter;
                dt = dbConn.QueryGetDataTable(sql);
            }
            catch (Exception ex)
            {
                Log.Error("SELECT error,table name = event ,error info = " + ex.Message);
                return null;
            }
            return dt;
            */
            return null;

        }
       
        #region 离线导出日志

        /// <summary>
        /// The cdl export log.
        /// </summary>
        /// <param name="filterString">
        /// The filter String.
        /// </param>
        public void CdlExportLog(string filterString)
        {
            new OperateDatabase(dbConn).CdlExportLog(filterString);
        }

        #endregion

        #region   离线导入数据库

        /// <summary>
        /// The cdl database parser.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public void CdlDatabaseParser(string fileName)
        {
            new OperateDatabase(dbConn).CdlDatabaseParser(fileName);
        }

        #endregion 

        #region  在线导出日志

        /// <summary>
        /// The export log.
        /// </summary>
        public void ExportLog()
        {
            new OperateDatabase(dbConn).LstExportLog();
        }
    
        #endregion

        #region   在线导入数据库

        /// <summary>
        /// The database 3 file parser.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public void Db3FileParser(string fileName)
        {
            new OperateDatabase(dbConn).LstDb3FileParser(fileName);
        }

        #endregion 
        public object ExecuteScalar(string sql)
        {
            try
            {

                return dbConn.ExecuteScalar(sql);
            }
            catch (Exception ex)
            {
                Log.Error("ExecuteScalar err:" + ex.ToString() + ", sql=" + sql);
                return null;
            }
        }
        public int ExecuteNonQuery(string sql)
        {
            try {
                if (dbConn != null)
                {
                    dbConn.ExcuteNonQuery(sql);
                }
            }
            catch(Exception err)
            {
                Log.Error("ExecuteNonQuery err:" + err.ToString() + ", sql=" + sql);
                return -1;
            }
            
            return 0;
        }
        //add by deyu end

        //add by deyu for special use
        public int SetDBConnect(DbConn dbc)
        {
			if(dbc == null)
				return -1;
            try
            {
                dbConn = dbc.Clone();
            }
            catch(Exception exp)
            {
                MyLog.Log("EXP: SetDBConnect, "+ exp.Message);
                return -1;
            }

            IsInit = true;
            return 0;
        }

        public void CloseConnection()
        {
            if (dbConn != null)
            {
                dbConn.Close();
            }
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        public void Initialize()
        {//add by deyu 2014-10-13 如果已经初始化，避免再次初始化

            //wangyanjie 2014-11-2 skip main thread init process.
            IsInit = true;
        }
        //检查数据库，自动根据解析选项创建数据库连接，并检查数据库中是否存在必须表，如果不存在，自动创建
        public int CheckDatabaseTable(Type type)
        {
            if (dbConn == null || !dbConn.IsConnect())
            {
                return -1;
            }
            bool bExist = CheckDatabaseTableExist(dbConn, type.Name);
            if (!bExist)
            {
                //如果不存在，自动创建数据库表
                string sql = GetCreateTableSQL(type);
                if (!string.IsNullOrEmpty(sql))
                {
                    int ret = dbConn.ExcuteNonQuery(sql);
                    if (ret < 0)
                    {
                        Log.Error("dbconn create table err sql=" + sql);
                        return -1;
                    }
                }

                //创建表后，再次检验表是否存在
                bExist = CheckDatabaseTableExist(dbConn, type.Name);
                if (!bExist)
                {
                    //创建失败
                    return -2;
                }
            }

            return 0;
        }
        //根据解析参数, 创建数据库连接对象
        

        //检查数据库表是否存在，for sqlite
        public  bool CheckDatabaseTableExist(DbConn dbconn, string tableName)
        {
            if (dbconn == null || !dbconn.IsConnect())
            {
                return false;
            }
            string sql = string.Format("SELECT COUNT(OID) FROM sqlite_master where type='table' and name='{0}'", tableName);
            object obj = dbconn.ExecuteScalar(sql);
            if (obj == null)
            {
                return false;
            }
            if (0 == Convert.ToInt32(obj))
            {
                return false;
            }
            return true;
        }

        private string GetCreateTableSQL(Type t)
        {
            return CreateTable(t);
        }
        /// <summary>
        /// Create the table which needed automatically
        /// </summary>
        private string CreateTable(Type classinfo)
        {
            try
            {
                if ( dbConn == null)
                    return "";

                string CreateSql = string.Format("Create Table IF NOT EXISTS {0} (", classinfo.Name);
                PropertyInfo[] properties = classinfo.GetProperties();
                //获得类型的所有公开属性
                int count = properties.Length;
                for (int i = 0; i < count; i++)
                {
                    string _type_in_db;
                    string _default_describe = "DEFAULT NULL";
                    switch (properties[i].PropertyType.Name.ToLower())
                    {
                        case "string":
                            _type_in_db = "varchar(100)";
                            break;
                        case "boolean":
                            _type_in_db = "bit(1)";
                            break;
                        case "int32":
                            _type_in_db = "int";
                            break;
                        case "uint32":

                        case "uint16":

                        case "uint64":
                            _type_in_db = "numeric";
                            break;
                        case "byte[]":
                            _type_in_db = "longblob";
                            break;
                        default:
                            _type_in_db = "varchar";
                            break;
                    }
                    //将属性的类型装换为数据库中的类型
                    if (string.Equals(properties[i].Name, "Oid"))
                    {
                        _type_in_db = "INTEGER";
                        if (dbConn.mDbType == DatabaseMgr.DbType.Mysql)
                            _default_describe = "NOT NULL PRIMARY KEY AUTO_INCREMENT";
                        else if (dbConn.mDbType == DatabaseMgr.DbType.SQLite || dbConn.mDbType == DatabaseMgr.DbType.Memory)
                        {

                            _default_describe = "NOT NULL PRIMARY KEY AUTOINCREMENT";
                        }
                        else
                        {
                            throw new Exception("不支持的数据库类型！");
                        }
                    }
                    CreateSql += properties[i].Name + " " + _type_in_db + " " + _default_describe + ",";
                }
                CreateSql += "OptimisticLockField int NULL,GCRecord int(11) DEFAULT NULL)";

                dbConn.ExcuteNonQuery(CreateSql);

                return CreateSql;
            }
            catch (Exception ex)
            {
                Log.Error("CREATE TABLE error,table name = " + classinfo.Name + "error info = " + ex.Message);
                return string.Empty;
            }
        }
        
    }
}

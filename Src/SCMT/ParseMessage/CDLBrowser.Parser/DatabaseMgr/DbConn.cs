using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Data.Common;
using CDLBrowser.Parser.Document.Event;
using Common.Logging;

namespace CDLBrowser.Parser.DatabaseMgr
{
    //数据库类型
    public enum DbType
    {
        SQLite,
        Mysql,
        Memory,
        None,
    };

    public struct DbOptions
    {
        public DbType ConnType;
        public  string ConnStr;
    };
    public class DbConn
    {
        private static readonly ILog Log = Common.Logging.LogManager.GetLogger(typeof(DbConn));
        public DbType mDbType;
        private DbconnBase mIDbconn;
        public string strConnString;
        //public string ConnString { set; get; }

        public DbConn(DbType type, string connstring)
        {
            mDbType = type;
            strConnString = connstring;
            Connect();
        }

        public DbConn(DbOptions opts)
        {
            this.mDbType = opts.ConnType;
            this.strConnString = opts.ConnStr;
            Connect();
        }

        public DbConn(DbConn dbc)
        {
            this.mDbType = dbc.mDbType;
            this.strConnString = dbc.strConnString;
            Connect();
        }
        public DbConn Clone()
        {
            DbConn newConn = new DbConn(this.mDbType,this.strConnString);
            //newConn.Connect();
            return newConn;
        }

        public int Connect()
        {
            if (string.IsNullOrEmpty(strConnString))
            {
                return -1;
            }
            // default 
            int ret = -1;
            try
            {
                switch (mDbType)
                {
                    case DbType.Memory:
                        {
                            if (mIDbconn == null)
                            {
                                mIDbconn = DBConnMemory.Instance;
                            }
                            ret = mIDbconn.Connect(strConnString);
                        }
                        break;
                    case DbType.SQLite:
                        {
                            if (mIDbconn == null)
                            {
                                mIDbconn = new DbConnSqlite();
                            }
                            ret = mIDbconn.Connect(strConnString);
                        }
                        break;
                    case DbType.Mysql:
                        {
                            if (mIDbconn == null)
                            {
                                mIDbconn = new DbConnMysql();
                            }
                            ret = mIDbconn.Connect(strConnString);
                        }
                        break;
                    default:
                        throw new Exception("DbType not support");
                    //break;
                }
            }
            catch (Exception exp)
            {
                MyLog.Log("EXP:" + exp.Message);
                //throw;
            }
           
            return ret;
        }

        public int SetConnection(DbConn dbc)
        {
            if (dbc == null)
            {
                return -1;
            }
            this.mDbType = dbc.mDbType;
            this.strConnString = dbc.strConnString;
            this.Connect();
            return 0;
        }
        public int ExcuteNonQuery(string sql)
        {
            return mIDbconn.ExcuteNonQuery(sql);
        }
        public int ExcuteByTrans(string sql, IDbCommand sqlcmd)
        {
            return mIDbconn.ExcuteByTrans(sql, sqlcmd);
        }
        public void ExecuteWithParamtersByTrans(string sql, IDbDataParameter[] paramters, IDbCommand mysqlcmd)
        {
            mIDbconn.ExecuteWithParamtersByTrans(sql,paramters,mysqlcmd);
        }
        public bool IsConnect()
        {
            if (mIDbconn == null)
            {
                Log.Error("IsConnect, dbconnection is null");
                return false;
            }
            return mIDbconn.IsConnect();
        }

        public void Close()
        {
            if (mIDbconn == null)
            {
                return;
            }
            mIDbconn.Close();
            mIDbconn = null;
        }
        public void ClearLastErrorString()
        {
            if (mIDbconn != null)
            {
                mIDbconn.ClearLastErrorString();
            }

        }
        public string GetLastErrString()
        {
            if (mIDbconn != null)
            {
                return mIDbconn.GetLastErrString();
            }
            return "";
        }

        public object ExecuteScalar(string sql)
        {
            if (mIDbconn != null)
            {
                return mIDbconn.ExecuteScalar(sql);
            }
            return null;
        }

        public IDbCommand BeginTransaction()
        {
            if (mIDbconn != null)
            {
                return  mIDbconn.BeginTransaction();
            }
            return null;
        }

        public void CommitChanges(IDbCommand dbcmd)
        {
            if (mIDbconn != null)
            {
                mIDbconn.CommitChanges(dbcmd);
            }
           
        }
        public DataTable QueryGetDataTable(string sql)
        {
            if (mIDbconn != null)
            {
                DataTable dt = mIDbconn.QueryGetDataTable(sql);
                return dt;
            }
             return null;
        }
        public DataTable GetDataTableAndClose(string sql)
        {
            if (mIDbconn == null)
            {
                MyLog.Log("ERROR: GetDataTableQueryAndClose:mIDbconn is null.");
                return null;
            }
            return mIDbconn.QueryGetDataTable(sql);
        }
        public DataRow[] GetDataRowsArrayAndClose(string sql)
        {
            if (mIDbconn == null)
            {
                MyLog.Log("ERROR: GetDataRowsArrayQueryAndClose:mIDbconn is null.");
                return null;
            }
            return  mIDbconn.GetDataRowsArrayAndClose(sql);
        }
        public Event[] QueryGetEventsArray(string sql)
        {
            if (mIDbconn == null)
            {
                return null;
            }
            DataTable dt = mIDbconn.QueryGetDataTable(sql);
           
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            Event [] array = new Event[dt.Rows.Count];
            int index = 0;
            foreach (var row in dt.Rows)
            {
                array[index++] = CreateEventFromDataRow((DataRow)row);
            }
            return array;
        }

        public ObservableCollection<Event> QueryGetObservCollectionEvents(string sql)
        {
            //System.Diagnostics.Debug.WriteLine("### query events.." + DateTime.Now.ToString("HH:mm:ss.fff"));
            ObservableCollection<Event> obs = new ObservableCollection<Event>();
            Event[] array = QueryGetEventsArray(sql);
            //System.Diagnostics.Debug.WriteLine("### query ends.." + DateTime.Now.ToString("HH:mm:ss.fff"));
            if (array == null)
            {
                return obs;
            }
            for (int i = 0; i < array.Length; i++)
            {
                obs.Add(array[i]);
            }
            //System.Diagnostics.Debug.WriteLine("### create event array ends.." + DateTime.Now.ToString("HH:mm:ss.fff"));
            return obs;
        }
        /// <summary>
        /// Transfer datarow to event
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private Event CreateEventFromDataRow(DataRow row)
        {
            Event eventObject = new Event();
            PropertyInfo[] ppinfo = eventObject.GetType().GetProperties();
            object[] param = new object[1];
            foreach (PropertyInfo pinfo in ppinfo)
            {
                MethodInfo setminfo = pinfo.GetSetMethod(false);
                if (setminfo != null && row.Table.Columns.Contains(pinfo.Name))
                {
                    object pval = row[pinfo.Name];
                    if ("TimeStamp" == pinfo.Name)
                    {
                        DateTime dt = (DateTime)row[pinfo.Name];
                        pval = dt.ToString("yyyy-MM-dd  HH:mm:ss.fff");
                    }

                    if (pval != null)
                    {
                        if (!Convert.IsDBNull(pval))
                        {
                            if (pval.GetType() != pinfo.PropertyType)
                            {
                                param[0] = Convert.ChangeType(pval, pinfo.PropertyType);
                            }
                            else
                            {
                                param[0] = pval;
                            }
                            setminfo.Invoke(eventObject, param);
                        }
                    }
                }
            }

            //eventObject.InitOtherParams();

            return eventObject;
        }
        public void ExecuteWithParamters(string sql, IDbDataParameter[] paramters)
        {
            if (mIDbconn != null)
            {
                mIDbconn.ExecuteWithParamters(sql,paramters);
            }
        }

        public void CreateIndexForTable(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return;
            }
            switch (tableName)
            {
                case "Event":
                    CreateIndexsForEventTable();
                    break;
            }

        }
        /// <summary>
        /// Create index
        /// </summary>
        private void CreateIndexsForEventTable()
        {
            try
            {
                string index_sql = string.Format("create index parsingid_index on Event (DisplayIndex,ParsingId);");
                ExcuteNonQuery(index_sql);

                index_sql = string.Format("create index evt_cellid_cellueid_tick on Event (CellId ASC, CellUeId ASC, TickTime ASC);");
                ExcuteNonQuery(index_sql);

                index_sql = string.Format("create index parsingid_timestamp_hsfn_index on Event (TickTime,HalfSubFrameNo,ParsingId);");
                ExcuteNonQuery(index_sql);               
                
                
                
                /*index_sql = string.Format("create index timestamp_index on Event (TimeStamp);");
                ExcuteNonQuery(index_sql);
                index_sql = string.Format("create index protocol_index on Event (Protocol);");
                ExcuteNonQuery(index_sql);
                index_sql = string.Format("create index eventname_index on Event (EventName);");
                ExcuteNonQuery(index_sql);
                index_sql = string.Format("create index messagesource_index on Event (MessageSource);");
                ExcuteNonQuery(index_sql);
                index_sql = string.Format("create index messagedst_index on Event (MessageDestination);");
                ExcuteNonQuery(index_sql);
                index_sql = string.Format("create index hostneid_index on Event (HostNeid);");
                ExcuteNonQuery(index_sql);
                index_sql = string.Format("create index localcellid_index on Event (LocalCellId);");
                ExcuteNonQuery(index_sql);
                index_sql = string.Format("create index cellid_index on Event (CellId);");
                ExcuteNonQuery(index_sql);
                index_sql = string.Format("create index cellueid_index on Event (CellUeId);");
                ExcuteNonQuery(index_sql);
                index_sql = string.Format("create index halfsubfrmno_index on Event (HalfSubFrameNo);");
                ExcuteNonQuery(index_sql);*/
                
            }
            catch (Exception exp)
            {
                Log.Error("CreateIndexsForEventTable," + exp.Message);
                Close();
                //throw;
            }

        }
        /// <summary>
        /// Create tables in database automatically
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int CheckDatabaseTable(Type type, String tableName = null)
        {
            if (this.IsConnect() == false)
            {
                //创建数据库失败
                Log.Error("CheckDatabaseTable, no dbconnection!");
                return -1;
            }

            //设置解析选项数据库连接 yanjeiwa 2014/12/3, optiondatas donot save dbconn anymore.
            //this.CurrentParseOptionData.dbconn = dbconn;
           
            //add by deyu 2015-06-09 begin for 创建临时表
            if (tableName == null)
            {
                tableName = type.Name;
            }

            bool bExist = CheckDatabaseTableExist(tableName);
            //add by deyu 2015-06-09 end
            if (!bExist)
            {
                //如果不存在，自动创建数据库表
                string sql = GetCreateTableSQL(type, tableName);
                if (!string.IsNullOrEmpty(sql))
                {
                    int ret = ExcuteNonQuery(sql);
                    if (ret < 0)
                    {
                        Log.Error("dbconn create table err sql=" + sql);
                        return -1;
                    }
                    CreateIndexForTable(tableName);
                }
                             
                //创建表后，再次检验表是否存在
                bExist = CheckDatabaseTableExist(tableName);
                if (bExist)
                {
                    //创建ok
                    
                }
                else
                {
                    return -2;
                }
            }
            return 0;
        }
      
        //检查数据库表是否存在，for sqlite and mysql
        public bool CheckDatabaseTableExist(string tableName)
        {
            if (mIDbconn == null || mIDbconn.IsConnect() == false)
            {
                return false;
            }
            try
            {
                string sql;
                object obj;
                switch (mDbType)
                {
                    case DbType.Mysql:
                        sql = string.Format("SELECT COUNT(OID) FROM information_schema.tables where table_schema = 'cdl' and TABLE_NAME='{0}';", tableName);
                          obj = ExecuteScalar(sql);
                          if (obj == null || 0 == Convert.ToInt32(obj))
                          {
                             return false;
                          }
                    break;

                    case DbType.SQLite:
                    case DbType.Memory:
                          sql = string.Format("SELECT COUNT(OID) FROM sqlite_master where type='table' and name='{0}'", tableName);
                          obj = ExecuteScalar(sql);
                         if (obj == null || 0 == Convert.ToInt32(obj))
                         {
                             return false;
                         }
                    break;

                }
            }
            catch (Exception exp)
            {
                Log.Error("CheckDatabaseTableExist,EXP:" + exp.Message);
                //throw;
            }

            return true;
        }

        /// <summary>
        /// The get sql for insert to db.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string CreateInsertSqlFromObject(Type objType, Object objVal, string tablename, bool isEventTable = false)
        {
            PropertyInfo[] pinfos = objType.GetProperties();
            object[] param = new object[1];
            StringBuilder sql = new StringBuilder(string.Format("INSERT INTO  {0} (", tablename));
            foreach (PropertyInfo pinfo in pinfos)
            {
                var DbAttr = pinfo.GetCustomAttributes(typeof(DataBaseIngoredAttribute), false).FirstOrDefault() as DataBaseIngoredAttribute;
                if ( DbAttr== null || (DbAttr != null && DbAttr.IgnoredFlag))
                {
                    continue;
                }

                if (pinfo.Name.ToLower() == "oid")
                    continue;
                

                if (!pinfo.PropertyType.IsValueType)
                {
                    if (pinfo.PropertyType.Name.ToLower() != "string" && pinfo.PropertyType.Name.ToLower() != "byte[]")
                        continue;
                }
                sql.Append(pinfo.Name);
                sql.Append(",");
            }
            //删除最后一个,
            sql.Remove(sql.Length - 1, 1);
            sql.Append(") VALUES(");
            foreach (PropertyInfo pinfo in pinfos)
            {
                var DbAttr = pinfo.GetCustomAttributes(typeof(DataBaseIngoredAttribute), false).FirstOrDefault() as DataBaseIngoredAttribute;
                if (DbAttr == null || (DbAttr != null && DbAttr.IgnoredFlag))
                {
                    continue;
                }

                if (pinfo.Name.ToLower() == "oid")
                    continue;

                //if (isEventTable)
                //{
                   /* if ((pinfo.Name == "EnbUeId" || pinfo.Name == "CRNTI"))
                    {
                        continue;
                    }*/
                //}

                if (pinfo.Name == "MsgBody")
                {
                    sql.Append("@MsgBody");
                    sql.Append(",");
                    continue;
                
                }
                if (!pinfo.PropertyType.IsValueType)
                {
                    if (pinfo.PropertyType.Name.ToLower() != "string" && pinfo.PropertyType.Name.ToLower() != "byte[]")
                        continue;
                }
                MethodInfo getminfo = pinfo.GetGetMethod(false);
                if (getminfo != null)
                {
                    if (pinfo.PropertyType.FullName == typeof(string).FullName)
                    {
                        sql.Append("'");
                        sql.Append(getminfo.Invoke(objVal, null));
                        sql.Append("',");
                    }
                    else
                    {
                        if (pinfo.PropertyType.Name.ToLower() == "byte[]")
                        {
                            sql.Append("@RawData");
                            sql.Append(",");
                        }
                        else
                        {
                            object tmpval = getminfo.Invoke(objVal, null);
                            if (tmpval is bool)
                            {
                                int tmp = (bool)tmpval == true ? 1 : 0;
                                sql.Append(tmp);
                                sql.Append(",");
                            }
                            else if (tmpval is Enum)
                            {
                                int tmp = (int)tmpval;
                                sql.Append(tmp);
                                sql.Append(",");
                            }
                            else
                            {
                                sql.Append(tmpval);
                                sql.Append(",");
                            }

                        }
                    }
                }
            }
            //删除最后一个,
            sql.Remove(sql.Length - 1, 1);
            sql.Append(")");
            return sql.ToString();
        }
        private string GetCreateTableSQL(Type t, String tableName = null)
        {
            return CreateTable(t, tableName);
        }
        /// <summary>
        /// Create the table which needed automatically
        /// </summary>
        private string CreateTable(Type classinfo, String tableName = null)
        {
            try
            {
                if (tableName == null)
                {
                    tableName = classinfo.Name;
                }

                string CreateSql = string.Format("Create Table IF NOT EXISTS {0} (", tableName);
                PropertyInfo[] properties = classinfo.GetProperties();
                //获得类型的所有公开属性
                int count = properties.Length;
                for (int i = 0; i < count; i++)
                {
                    var DbAttr = properties[i].GetCustomAttributes(typeof(DataBaseIngoredAttribute), false).FirstOrDefault() as DataBaseIngoredAttribute;
                    if ( DbAttr== null || ( DbAttr != null && DbAttr.IgnoredFlag))
                    {
                        continue;
                    }

                    if (!properties[i].PropertyType.IsValueType)
                    {
                        if (properties[i].PropertyType.Name.ToLower() != "string" && properties[i].PropertyType.Name.ToLower() != "byte[]")
                            continue;
                    }
                    /*
                    if (properties[i].Name == "EnbUeId" || properties[i].Name == "CRNTI")
				    {
				         continue;
				    }	*/					
                    string _type_in_db;
                    string _default_describe = "DEFAULT NULL";
                   
                    switch (properties[i].PropertyType.Name.ToLower() )
                    {
                        case "string":
                            _type_in_db = "varchar(100)";
                            break;
                        case "boolean":
                            _type_in_db = "bit(1)";
                            break;
                        case "int16":
                        case "uint16":
                            _type_in_db = "INTEGER";
                            break;
                        case "int32":
                        case "int64":
                        case "uint32":
                        case "uint64":
                            _type_in_db = "INTEGER";    
                            break;
                        case "byte[]":
                            _type_in_db = "BLOB";
                            break;
                        default:
                            if (properties[i].PropertyType.BaseType.Name  == "Enum")
                            {
                                _type_in_db = "int";
                            }
                            else
                            {
                                _type_in_db = "varchar(200)";
                            }
                            break;
                    }
                    //将属性的类型装换为数据库中的类型
                    switch (properties[i].Name)
                    {
                        case  "Oid":
                            _type_in_db = "INTEGER";
                            if (mDbType == DatabaseMgr.DbType.Mysql)
                                _default_describe = "NOT NULL PRIMARY KEY AUTO_INCREMENT";
                            else if (mDbType == DatabaseMgr.DbType.SQLite || mDbType == DatabaseMgr.DbType.Memory)
                            {

                                _default_describe = "NOT NULL PRIMARY KEY AUTOINCREMENT";
                            }
                            else
                            {
                                throw new Exception("不支持的数据库类型！");
                            }
                            break;
                        case "IsDeleted":
                            _default_describe = "default 0";
                            break;

                        case "TimeStamp":
                            _type_in_db = "DATETIME";//"DATETIME"
                            _default_describe = "default '0000-00-00 00:00:00.000'";//"default '0000-00-00 00:00:00.000'"
                            break;

                    }
                    CreateSql += properties[i].Name + " " + _type_in_db + " " + _default_describe + ",";
                }
                CreateSql = CreateSql.Remove(CreateSql.Length - 1);
                CreateSql += ")";


                //ExcuteNonQuery(CreateSql);

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

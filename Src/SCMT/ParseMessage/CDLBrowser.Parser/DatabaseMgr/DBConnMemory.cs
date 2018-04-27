using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using CDLBrowser.Parser.Document.Event;
using System.Threading;
namespace CDLBrowser.Parser.DatabaseMgr
{
    public class DBConnMemory : DbconnBase
    {

        //"database=" + dbname.Trim() + ";DataSource=localhost;UserId=root;Password=";
        public static string GetConnectString(string dbname)
        {
            return string.Format("Data Source={0};Version=3;", dbname);
        }

       public  static DBConnMemory Instance = new DBConnMemory();

        private SQLiteConnection m_SQLiteConnection;
        //private SQLiteCommand m_SQLiteCmd;
        //private SQLiteDataReader m_SQLiteReader;
        //private SQLiteTransaction m_SQLiteTransaction;
        //private SQLiteDataAdapter m_SQLiteDataAdapter;
        //private SQLiteDataAdapter m_SQLiteDataAdapter;
        private Mutex mutex = new Mutex();

        public string mLastQuerySql;
        public string mLastErrString;

        public override IDbCommand BeginTransaction()
        {

            if (m_SQLiteConnection == null)
            {
                MyLog.Log("Error:BeginTransaction, connection is null..");
                return null;
            }
            try
            {
                mutex.WaitOne();
                SQLiteCommand sqlcmd = m_SQLiteConnection.CreateCommand();
                SQLiteTransaction trans = m_SQLiteConnection.BeginTransaction();
                sqlcmd.Transaction = trans;
                return sqlcmd;
            }
            catch (Exception exp)
            {
			   mutex.ReleaseMutex();
                MyLog.Log("BeginTransactioin:EXP" + exp.Message);
                mLastErrString = "异常:" + exp.Message;
                Close();
                //throw;
            }
            return null;
        }

        public override int Connect(string connectString)
        {
            if (string.IsNullOrEmpty(connectString))
            {
                return -1;
            }
            try
            {
                if (m_SQLiteConnection != null)
                {
                    return 0;
                }
                m_SQLiteConnection = new SQLiteConnection(connectString);
                m_SQLiteConnection.Open();
               /* m_SQLiteCmd = new SQLiteCommand(m_SQLiteConnection);*/
            }
            catch (Exception exp)
            {
                MyLog.Log("DBConnMemory:Connect,EXP: " + exp.ToString());
                mLastErrString = "异常:" + exp.Message;
                //m_SQLiteConnection = null;
                Close();
                return -1;
            }

            return 0;
        }

        public override DbconnBase Clone()
        {
            return this;
        }
        public override void Close()
        {
            /*
            if (m_SQLiteConnection != null)
            {
                m_SQLiteConnection.Close();
                m_SQLiteConnection.Dispose();
            }*/
        }
        public override void CommitChanges(IDbCommand dbcmd)
        {
            try
            {
                if (dbcmd.Transaction != null)
                {
                    dbcmd.Transaction.Commit();
                }
            }
            catch (Exception exp)
            {
                MyLog.Log("EXP:" + exp.Message);
                mLastErrString = "异常:" + exp.Message;
                Close();
                //throw;
            }
            finally
            {
                mutex.ReleaseMutex();
            }

        }

        public override bool IsConnect()
        {
            if (m_SQLiteConnection == null)
            {
                return false;
            }
            if (m_SQLiteConnection.State == ConnectionState.Open)
            {
                return true;
            }
            return false;
        }

        public override void ClearLastErrorString()
        {
            this.mLastErrString = "";
            //base.ClearLastErrorString();
        }


        public override int ExcuteNonQuery(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return -1;
            }
            try
            {
                mutex.WaitOne();
                SQLiteCommand sqlitecmd = m_SQLiteConnection.CreateCommand();
                sqlitecmd.CommandText = sql;
                mLastQuerySql = sql;
                sqlitecmd.ExecuteNonQuery();
                
            }
            catch (Exception exp)
            {
                MyLog.Log("ExcuteNonQuery: EXP:" + exp + ", sql=" + sql);
                mLastErrString = "异常:" + exp.Message;
                //Close();
                return -1;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            return 0;
        }

        public override int ExcuteByTrans(string sql, IDbCommand mysqlcmd)
        {
            if (string.IsNullOrEmpty(sql))
            {
                mLastErrString = "sql is null error";
                return -1;
            }
            try
            {
		        mutex.WaitOne();
                mysqlcmd.CommandText = sql;
                mLastQuerySql = sql;
                mysqlcmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                MyLog.Log("Query err = " + exp.Message + ", sql=" + sql);
                mLastErrString = "异常:" + exp.Message;
                return -1;
            }
			finally
			{
			 mutex.ReleaseMutex();
			}

            return 0;
        }
     public override object ExecuteScalar(string sql)
        {
            object retObj = null;
            try
            {
                mutex.WaitOne();
                SQLiteCommand sqlitecmd = m_SQLiteConnection.CreateCommand();
                sqlitecmd.CommandText = sql;
                mLastQuerySql = sql;
                retObj = sqlitecmd.ExecuteScalar();
            }
            catch (Exception exp)
            {
                MyLog.Log("ExecuteScalar:EXP: " + exp.Message + "\n");
                //MyLog.Log("sql=" + sql);
                mLastErrString = "异常:" + exp.Message;
                return null;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            return retObj;
        }

 
        public override void ExecuteWithParamtersByTrans(string sql, IDbDataParameter[] paramters, IDbCommand mysqlcmd)
        {
            try
            {
                if (m_SQLiteConnection == null || m_SQLiteConnection.State != ConnectionState.Open || mysqlcmd == null)
                {
                    return;
                }
                mutex.WaitOne();
                foreach (var param in paramters)
                {
                    mysqlcmd.Parameters.Add(param);
                }

                mysqlcmd.CommandText = sql;
                mysqlcmd.ExecuteNonQuery();
                if (mysqlcmd.Parameters != null)
                {
                    mysqlcmd.Parameters.Clear();
                }
            }
            catch (Exception exp)
            {
                MyLog.Log("EXP:ExecuteWithParamters: " + exp.Message);
                mLastErrString = "异常:" + exp.Message;
                //throw;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
        public override void ExecuteWithParamters(string sql, IDbDataParameter[] paramters)
        {
            try
            {
                if (m_SQLiteConnection == null || m_SQLiteConnection.State != ConnectionState.Open)
                {
                    return;
                }
                mutex.WaitOne();

                SQLiteCommand sqlitecmd = m_SQLiteConnection.CreateCommand();
                sqlitecmd.CommandText = sql;
                foreach (var param in paramters)
                {
                    sqlitecmd.Parameters.Add(param);
                }
                
                sqlitecmd.ExecuteNonQuery();

            }
            catch (Exception exp)
            {
                MyLog.Log("ExecuteWithParamters:EXP:" + exp.Message);
                mLastErrString = "异常:" + exp.Message;
                //throw;
            }
            finally
            {
                mutex.ReleaseMutex();
            }

        }
        public override DataTable QueryGetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(sql))
            {
                return null;

            }
            sql = PreProcessSql(sql);
            if (m_SQLiteConnection == null || m_SQLiteConnection.State != ConnectionState.Open)
            {
                MyLog.Log("Error:QueryGetDataTable, connection is null.");
                return null;
            }
            try
            {
                mutex.WaitOne();
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, m_SQLiteConnection);
                da.Fill(dt);
            }
            catch (Exception exp)
            {
                MyLog.Log("QueryGetDataTable:EXP:" + exp.Message);
                mLastErrString ="异常:" + exp.Message;
                //throw;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            return dt;
        }
        public override DataRow[] GetDataRowsArrayAndClose(string sql)
        {
            DataRow[] rows = null;
            try
            {
                DataTable dt = GetDataTableAndClose(sql);
                if (dt.Rows.Count == 0)
                {
                    MyLog.Log("Warn:QueryGetDataRowsArray: query result is empty,Sql=" + sql);
                    return null;
                }
                rows = new DataRow[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rows[i] = dt.Rows[i];
                }
            }
            catch (Exception exp)
            {
                MyLog.Log("EXP:QueryGetDataRowsArray: " + exp.Message);
                mLastErrString = "异常:" + exp.Message;
                //throw;
            }

            return rows;
        }
        public override DataTable GetDataTableAndClose(string sql)
        {
            DataTable dt = null;
            if (m_SQLiteConnection == null)
            {
                MyLog.Log("Error:QueryGetDataTable, connection is null.");
                return null;
            }
            try
            {
                dt = QueryGetDataTable(sql);
            }
            catch (Exception exp)
            {
                MyLog.Log("Exp:QueryGetDataTable:" + exp.Message);
                //throw;
            }
            // yanjiewa 2014/12/3 For Memory DB, We cannot close the connection,otherwise ,DB will be lost.;
            //
            Close();
            return dt;
        }
        public override string GetLastErrString()
        {
            return mLastErrString;
        }

        public override DataRow[] QueryGetDataRowsArray(string sql)
        {
            DataRow[] rows = null;

            try
            {
                DataTable dt = QueryGetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    MyLog.Log("Warn:QueryGetDataRowsArray: query result is empty,Sql=" + sql);
                    return null;
                }
                rows = new DataRow[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rows[i] = dt.Rows[i];
                }
            }
            catch (Exception exp)
            {
                MyLog.Log("EXP:QueryGetDataRowsArray: " + exp.Message);
                mLastErrString = "异常:" + exp.Message;
                //throw;
            }

            return rows;
        }

  
    }
}

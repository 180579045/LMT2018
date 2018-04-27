using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using CDLBrowser.Parser.Document.Event;
using SuperLMT.Utils;
using System.Threading;
namespace CDLBrowser.Parser.DatabaseMgr
{
    public class DbConnSqlite : DbconnBase
    {
        //"database=" + dbname.Trim() + ";DataSource=localhost;UserId=root;Password=";
        public static string GetConnectString(string dbname)
        {
            if (string.Equals(dbname, DbConnProvider.DefaultSqliteMemDatabaseName))
                return string.Format("Data Source={0};Version=3;",dbname);
            else
                return string.Format("Data Source={0};Version=3;", AppPathUtiliy.Singleton.GetAppPath() + dbname);
                
        }

        private SQLiteConnection m_SQLiteConnection;
        //private SQLiteDataAdapter m_SQLiteDataAdapter;
        //private SQLiteDataAdapter m_SQLiteDataAdapter;
        private static Mutex mutex = new Mutex();

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
                MyLog.Log("BeginTransaction, EXP:" + exp.Message);
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
                    MyLog.Log("Warn:Connect, Already connected!!");
                    return 0;
                }
                m_SQLiteConnection = new SQLiteConnection(connectString);
                m_SQLiteConnection.Open();
            }
            catch (Exception exp)
            {
                MyLog.Log("err = " + exp.ToString());
                mLastErrString = "异常:" + exp.Message;
                m_SQLiteConnection = null;
                return -1;
            }

            return 0;
        }

        public override DbconnBase Clone()
        {
            if (m_SQLiteConnection!= null)
            {
                DbConnSqlite conn = new DbConnSqlite();
                conn.m_SQLiteConnection = (SQLiteConnection)this.m_SQLiteConnection.Clone();
               return conn;
            }
            return null;
        }
		
        public override void Close()
        {
            if (m_SQLiteConnection != null)
            {
                m_SQLiteConnection.Close();
                m_SQLiteConnection.Dispose();
                m_SQLiteConnection = null;
            }
        }
        public override void ClearLastErrorString()
        {
            this.mLastErrString = "";
            //base.ClearLastErrorString();
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
                MyLog.Log("CommitChanges:Exceptioin:" + exp.Message);
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
        
        public override string GetLastErrString()
        {
            return mLastErrString;
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
                SQLiteCommand sqlcmd = m_SQLiteConnection.CreateCommand();
                sqlcmd.CommandText = sql;
                mLastQuerySql = sql;
                sqlcmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                MyLog.Log("Query err = " + exp + ", sql=" + sql);
                mLastErrString = "异常:" + exp.Message;
                Close();
                return -1;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            return 0;
        }
        public override int ExcuteByTrans(string sql, IDbCommand sqlcmd)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return -1;
            }
            try
            {
		        mutex.WaitOne();
                sqlcmd.CommandText = sql;
                mLastQuerySql = sql;
                sqlcmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                MyLog.Log("Query err = " + exp.Message + ", sql=" + sql);
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
        public override object ExecuteScalar(string sql)
        {
            object retObj = null;
            try
            {
                mutex.WaitOne();
                sql = PreProcessSql(sql);
                SQLiteCommand sqlcmd = m_SQLiteConnection.CreateCommand();
                sqlcmd.CommandText = sql;
	            mLastQuerySql = sql;
                retObj = sqlcmd.ExecuteScalar();
            }
            catch (Exception exp)
            {
                MyLog.Log("ExecuteScalar:EXP: " + exp.Message + "\n");
                //MyLog.Log("sql=" + sql);
                mLastErrString = "异常:" + exp.Message;
                //Close();
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
                    if (param == null)
                    {
                        continue;
                    }
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
                //Close();
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
                SQLiteCommand sqlcmd = m_SQLiteConnection.CreateCommand();
                sqlcmd.CommandText = sql;
                foreach (var param in paramters)
                {
                    sqlcmd.Parameters.Add(param);
                }
                sqlcmd.ExecuteNonQuery();
                
            }
            catch (Exception exp)
            {
                MyLog.Log("ExecuteWithParamters,EXP:" + exp.Message);
                mLastErrString = "异常:" + exp.Message;
                //Close();
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

            //sql = "select distinct * from event ORDER BY DisplayIndex DESC , HalfSubFrameNo ASC limit 500, 1000";

            if (m_SQLiteConnection == null || m_SQLiteConnection.State != ConnectionState.Open)
            {
                MyLog.Log("Error:QueryGetDataTable, connection is null.");
                return null;
            }
            try
            {
                mutex.WaitOne();
                string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                System.Diagnostics.Debug.WriteLine(string.Format("begin:QueryGetDataTable----{0}----\n{1}", strDateTime, sql));
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, m_SQLiteConnection);
                da.Fill(dt);

                strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                System.Diagnostics.Debug.WriteLine(string.Format("end:QueryGetDataTable----{0}----{1}", strDateTime, sql));
            }
            catch (Exception exp)
            {
                MyLog.Log("Exp:QueryGetDataTable:" + exp.Message + " sql = " + sql);
                mLastErrString = "异常:" + exp.Message;
                //Close();
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
                string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                System.Diagnostics.Debug.WriteLine(string.Format("begin:GetDataRowsArrayAndClose----{0}----\n{1}", strDateTime, sql));
                DataTable dt = GetDataTableAndClose(sql);
                if (dt.Rows.Count == 0)
                {
                    MyLog.Log("Warn:QueryGetDataRowsArray: query result is empty,Sql=" + sql);
                    return null;
                }
                rows = new DataRow[dt.Rows.Count];
                /*for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rows[i] = dt.Rows[i];
                }*/

                dt.Rows.CopyTo(rows, dt.Rows.Count);
                strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                System.Diagnostics.Debug.WriteLine(string.Format("end:GetDataRowsArrayAndClose----{0}----{1}", strDateTime, sql));
            }
            catch (Exception exp)
            {
                MyLog.Log("EXP:QueryGetDataRowsArray: " + exp.Message);
                mLastErrString = "异常:" + exp.Message;
                //Close();
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
                mLastErrString = "异常:" + exp.Message;
                //throw;
            }
            finally
            {
                //m_MySqlConnection.Close();
                Close();
            }
            return dt;
        }
	   public override DataRow[] QueryGetDataRowsArray(string sql)
        {
            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            System.Diagnostics.Debug.WriteLine(string.Format("begin:QueryGetDataRowsArray----{0}----\n{1}", strDateTime, sql));

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
                /*for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rows[i] = dt.Rows[i];
                }*/
                dt.Rows.CopyTo(rows, dt.Rows.Count);

                strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                System.Diagnostics.Debug.WriteLine(string.Format("end:QueryGetDataRowsArray----{0}----{1}", strDateTime, sql));
            }
            catch (Exception exp)
            {
                MyLog.Log("EXP:QueryGetDataRowsArray: "+ exp.Message);
                mLastErrString = "异常:" + exp.Message;
                //Close();
                //throw;
            }

            return rows;
        }

    }
}

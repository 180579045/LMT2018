using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDLBrowser.Parser.Document.Event;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Reflection;
namespace CDLBrowser.Parser.DatabaseMgr
{
    public class DbConnMysql : DbconnBase
    {
        //"database=" + dbname.Trim() + ";DataSource=localhost;UserId=root;Password=";
        public static string GetConnectString(string address, string user, string pass, string dbname)
        {
            return "DataSource=" + address + ";UserId=" + user + ";Password=" + pass + ";database=" + dbname;
        }

        private MySqlConnection m_MySqlConnection;
        public string mLastQuerySql;
        public string mLastErrString;

        public override IDbCommand BeginTransaction()
        {
            if (m_MySqlConnection == null)
            {
                return null;
            }
            try
            {
                MySqlCommand mysqlcmd = m_MySqlConnection.CreateCommand();
                MySqlTransaction mysqltrans = m_MySqlConnection.BeginTransaction();
                mysqlcmd.Transaction = mysqltrans;
                return mysqlcmd;
            }
            catch (Exception exp)
            {
                MyLog.Log("BeginTransaction:EXP" + exp.Message);
                mLastErrString = "异常:" + exp.Message;
                Close();
                //throw;
            }
            return null;

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
                MyLog.Log("CommitChangs:Exception:"+ exp.Message);
                mLastErrString = "异常:" + exp.Message;
                Close();
                //throw;
            }

        }
        public override int Connect(string connectString)
        {
            try {
                if (IsConnect())
                {
                    MyLog.Log("Warn:Connect, already connected!!!");
                    return 0;
                }
                m_MySqlConnection = new MySqlConnection(connectString);
                m_MySqlConnection.Open();
               }
            catch (Exception exp)
            {
                MyLog.Log("err = " + exp.ToString());
                mLastErrString = "异常:" + exp.Message;
                Close();
                return -1;
            }
            return 0;
        }
        public override void Close()
        {
            if (m_MySqlConnection != null)
            {
                m_MySqlConnection.Close();
                m_MySqlConnection.Dispose();
            }
        }
        public override DbconnBase Clone()
        {
            try
            {
                if (m_MySqlConnection != null)
                {
                    DbConnMysql conn = new DbConnMysql();
                    conn.m_MySqlConnection = this.m_MySqlConnection.Clone();
                    return conn;
                }
            }
            catch (Exception exp)
            {
                MyLog.Log("Clone:Exception:" + exp.Message);
                Close();
                //throw;
            }

            return null;
        }
        public override void ClearLastErrorString()
        {
            this.mLastErrString = "";
            //base.ClearLastErrorString();
        }
        public override void ExecuteWithParamters(string sql,IDbDataParameter[] paramters)
        {
            try
            {
                if (m_MySqlConnection == null || m_MySqlConnection.State != ConnectionState.Open)
                {
                    return;
                }
                MySqlCommand mysqlcmd = m_MySqlConnection.CreateCommand();
                foreach (var param in paramters)
                {
                    mysqlcmd.Parameters.Add(param);
                }

                mysqlcmd.CommandText = sql;
                mysqlcmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                MyLog.Log("EXP:ExecuteWithParamters: " + exp.Message + " Sql=" + sql);
                mLastErrString = "异常:" + exp.Message;
               
                //throw;
            }
        }

        public override void ExecuteWithParamtersByTrans(string sql, IDbDataParameter[] paramters, IDbCommand mysqlcmd)
        {
            if (m_MySqlConnection == null || m_MySqlConnection.State != ConnectionState.Open || mysqlcmd == null)
            {
                return;
            }
            try
            {
                foreach (var param in paramters)
                {
                    mysqlcmd.Parameters.Add(param);
                }

                mysqlcmd.CommandText = sql;
                mysqlcmd.ExecuteNonQuery();
                //clear all paramters
                if (mysqlcmd.Parameters != null)
                {
                    mysqlcmd.Parameters.Clear();
                }
            }
            catch (Exception exp)
            {
                MyLog.Log("EXP:ExecuteWithParamters: " + exp.Message + " Sql=" + sql);
                mLastErrString = "异常:" + exp.Message;
                if (mysqlcmd.Parameters != null)
                {
                    mysqlcmd.Parameters.Clear();
                }
                //throw;
            }
        }

        public override bool IsConnect()
        {
            if (m_MySqlConnection == null)
            {
                return false;
            }
            if (m_MySqlConnection.State == ConnectionState.Open)
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
                mLastErrString = "sql is null error";
                return -1;
            }
            try
            {
                MySqlCommand mysqlcmd = m_MySqlConnection.CreateCommand();
                mysqlcmd.CommandText = sql;
                mLastQuerySql = sql;
                return mysqlcmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                MyLog.Log("Query err = " + exp.Message + "\n" + " Sql=" + sql);
                //MyLog.Log("sql=" + sql);
                mLastErrString = "异常:" + exp.Message;
               
                return -1;
            }
            
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
                mysqlcmd.CommandText = sql;
                mLastQuerySql = sql;
                return mysqlcmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                MyLog.Log("Query err = " + exp.Message + "\n" + " Sql=" + sql);
                //MyLog.Log("sql=" + sql);
                mLastErrString = "异常:" + exp.Message;
                
                return -1;
            }
            
        }
        public override object ExecuteScalar(string sql)
        {
            object retObj = null;
            try
            {
                MySqlCommand mysqlcmd = m_MySqlConnection.CreateCommand();
                mysqlcmd.CommandText = sql;
	            mLastQuerySql = sql;
                retObj = mysqlcmd.ExecuteScalar();
            }
            catch (Exception exp)
            {
                MyLog.Log("Query err = " + exp.Message + "\n" + " Sql=" + sql);
                //MyLog.Log("sql=" + sql);
                mLastErrString = "异常:" + exp.Message;
               
                return null;
            }

            return retObj;
        }

        public override DataTable QueryGetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(sql))
            {
                return null;

            }
            sql = PreProcessSql(sql);

            if (m_MySqlConnection == null || m_MySqlConnection.State != ConnectionState.Open)
            {
                MyLog.Log("Error:QueryGetDataTable, connection is null." + " Sql=" + sql);
                return null;
            }
            
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sql, m_MySqlConnection);
                da.Fill(dt);
            }
            catch (Exception exp)
            {
                MyLog.Log("Exp:QueryGetDataTable:" + exp.Message + " Sql=" + sql);
                mLastErrString = "异常:" + exp.Message;
                //Close();
                //throw;
            }
           /* DataTable dt = MakeEventsDataTable();
            MySqlCommand sqlcmd = m_MySqlConnection.CreateCommand();
            sqlcmd.CommandText = sql;
            MySqlDataReader reader = sqlcmd.ExecuteReader();

            while (reader.Read())
            {
                Event ent = CreateEventObjectByReader(reader);
                if (ent != null)
                {
                    DataRow row = dt.NewRow();
                    foreach ( PropertyInfo pinfo in ent.GetType().GetProperties())
                    {
                        MethodInfo getminfo = pinfo.GetGetMethod(false);
                        if (getminfo != null)
                        {
                            row[pinfo.Name] = getminfo.Invoke(ent, null);
                        }
                         
                    }
                    dt.Rows.Add(row);
                }
            }
            */
            return dt;
        }

        public override DataTable GetDataTableAndClose(string sql)
        {
            DataTable dt = null;
            if (m_MySqlConnection == null)
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
            finally
            {
                Close();
            }

            return rows;
        }

    }
}

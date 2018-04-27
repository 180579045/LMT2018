

using System.Reflection;

namespace CDLBrowser.Parser.DatabaseMgr
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Data.Common;
    using CDLBrowser.Parser.Document.Event;
    //数据库接口
    public class DbconnBase
    {
        public virtual IDbCommand BeginTransaction()
        {
            return null;
        }
        public virtual int Connect(string connectString)
        {
            return 0;
        }
        public virtual void Close()
        {

        }
        public virtual DbconnBase Clone()
        {

            return null;
        }
        public virtual void ClearLastErrorString() { }
        public virtual void CommitChanges(IDbCommand dbcmd)
        {

        }

        public virtual object ExecuteScalar(string sql)
        {
            return null;
        }
        public virtual void ExecuteWithParamters(string sql, IDbDataParameter[] paramters) { }
        public virtual int ExcuteNonQuery(string sql)
        {
            return 0;
        }
        public virtual void ExecuteWithParamtersByTrans(string sql, IDbDataParameter[] paramters, IDbCommand mysqlcmd)
        {

        }
        public virtual int ExcuteByTrans(string sql, IDbCommand sqlcmd)
        {
            return 0;
        }
        public virtual DataTable GetDataTableAndClose(string sql)
        {
            return null;
        }
        public virtual string GetLastErrString()
        {
            return null;
        }
        public virtual DataRow[] GetDataRowsArrayAndClose(string sql)
        {
            return null;
        }
        public virtual bool IsConnect()
        {
            return false;
        }
        public virtual string PreProcessSql(string sql)
        {
            // 2014/12/24 yanjiewa
            // As we will mark the deleted row with flag IsDeleted = 1, but currently, we dont specify this in the sql str, so 
            // add this filter here for all selecting.
            string sqlbk = sql;
            if (string.IsNullOrEmpty(sql))
            {
                return sql;
            }
            sql = sql.ToUpper();
           
            if (sql.Contains(" EVENT ") == false)
            {
                return sqlbk;
            }
            if (sql.Contains("SELECT") == false)
            {
                return sqlbk;
            }
            if (sql.Contains("ISDELETED") == true)
            {
                return sqlbk;
            }

            int poswhere = sql.IndexOf("WHERE ");
            if (poswhere > 0)
            {
                int mpos;
                if (sql.Contains("ORDER"))
                {
                    mpos = sql.LastIndexOf("ORDER");
                }
                else
                {
                    mpos = sql.LastIndexOf("LIMIT");
                }
                //int poslimit = sql.LastIndexOf("LIMIT ");

                int istart = sqlbk.IndexOf("WHERE ", StringComparison.OrdinalIgnoreCase);
                string strtmp = sqlbk.Substring(istart, "WHERE".Length);
                sqlbk = sqlbk.Replace(strtmp, strtmp + "(");

                //if (poslimit > 0)
                if (mpos > 0)
                {
                    if (sql.Contains("ORDER"))
                    {
                        istart = sqlbk.IndexOf("ORDER", StringComparison.OrdinalIgnoreCase);
                        strtmp = sqlbk.Substring(istart, "ORDER".Length); // modified by lixiang 2015-04-23
                    }
                    else
                    {
                        istart = sqlbk.IndexOf("LIMIT", StringComparison.OrdinalIgnoreCase);
                        strtmp = sqlbk.Substring(istart, "LIMIT".Length);

                    }
                    sqlbk = sqlbk.Replace(strtmp, ") and (IsDeleted = 0) " + strtmp);
                    sqlbk = sqlbk.TrimEnd(new char[] { ' ', ';' });
                    sqlbk = sqlbk + ";";
                }
                else
                {
                    sqlbk = sqlbk.TrimEnd(new char[] { ' ', ';' });
                    sqlbk = sqlbk + ") and (IsDeleted = 0);";
                }
            }
            return sqlbk;
        }
        public virtual Event[] QueryGetEventsArray(string sql)
        {
            return null;
        }
        public virtual DataRow[] QueryGetDataRowsArray(string sql)
        {
            return null;
        }
        public virtual DataTable QueryGetDataTable(string sql)
        {
            return null;
        }

        /*
        public virtual DataTable MakeEventsDataTable()
        {
            DataTable dt = new DataTable();
            Event eventObject = new Event();
            PropertyInfo[] ppinfo = eventObject.GetType().GetProperties();
           
            int index = 0;
            foreach (PropertyInfo pinfo in ppinfo)
            {
                DataColumn col = new DataColumn(pinfo.Name);
                col.DataType = pinfo.PropertyType;
                dt.Columns.Add(col);
               
            }
            return dt;
        }

        public virtual Event CreateEventObjectByReader(IDataReader reader)
        {
            Event eventObject = new Event();
            PropertyInfo[] ppinfo = eventObject.GetType().GetProperties();
            object[] param = new object[1];
            foreach (PropertyInfo pinfo in ppinfo)
            {
                MethodInfo setminfo = pinfo.GetSetMethod(false);
                if (setminfo != null)
                {
                    object pval = reader[pinfo.Name];
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
            return eventObject;
        }*/
    }
}

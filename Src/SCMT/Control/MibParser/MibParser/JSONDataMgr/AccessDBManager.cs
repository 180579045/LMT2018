/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ AccessDBManager $
* 机器名称：       $ machinename $
* 命名空间：       $ MIBDataParser.JSONDataMgr $
* 文 件 名：       $ AccessDBManager.cs $
* 创建时间：       $ 2018.04.XX $
* 作    者：       $ TangYun $
* 说   明 ：
*     AccessDBManager。
* 修改时间     修 改 人    修改内容：
* 2018.04.xx   唐 芸       创建文件并实现类  AccessDBManager
*************************************************************************************/
using System;
using System.Data;
using System.Data.OleDb;

namespace MIBDataParser.JSONDataMgr
{
    public class AccessDBManager
    {
        private string fileName;
        private string connectionString;
        private OleDbConnection odcConnection;

        //fileName 为MDB文件（完整路径）
        public AccessDBManager(string fileName)
        {
            this.fileName = fileName;
            this.connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + @";Jet OleDb:DataBase Password=(/1ac2";
        }

        // 建立连接（打开数据库文件）  
        public void Open()
        {
            try
            {
                // 建立连接  
                this.odcConnection = new OleDbConnection(this.connectionString);
                // 打开连接  
                this.odcConnection.Open();
            }
            catch (Exception)
            {
                throw new Exception("尝试打开" + this.fileName + " 失败，请确认文件路径、文件名或者密码是否正确");
            }
        }

        // 断开连接（关闭据库文件）  
        public void Close()
        {
            if ((null != this.odcConnection)
                && (ConnectionState.Open == this.odcConnection.State))
            {
                this.odcConnection.Close();
            }
        }

        //根据sql命令返回一个DataSet  
        //以DataTable形式返回数据
        public DataSet GetDataSet(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                //OleDbDataAdapter查表返回的字段的数据类型不会掉失
                //OleDbCommand和OleDbDataReader 读取、组合出来的Table数据字段都是String型
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, this.odcConnection);
                adapter.Fill(ds);
            }
            catch (Exception)
            {
                throw new Exception("sql语句： " + sql + " 执行失败！");
            }
            return ds;
        }
    }
}

/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ SQLiteHelper $
* 机器名称：       $ machinename $
* 命名空间：       $ DataBaseUtil $
* 文 件 名：       $ SQLiteHelper.cs $
* 创建时间：       $ 2018.10.28 $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     SQLite数据库帮助类。
* 修改时间     修 改 人         修改内容：
* 2018.10.xx  XXXX            XXXXX
*************************************************************************************/
using CommonUtility;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace DataBaseUtil
{
	public class SQLiteHelper
	{
		// 主数据库名称
		public static string DB_NAME_MAIN = FilePathHelper.GetDataPath() + "LMTDBMain.sqlite";
		// 告警据库名称
		public static string DB_NAME_ALARM = FilePathHelper.GetDataPath() + "LMTDBAlarm.sqlite";
		// 主数据库连接字符串
		public static string CONN_CFG_MAIN = string.Format("Data Source={0};Version=3;", DB_NAME_MAIN);
		// 告警数据库连接字符串
		public static string CONN_CFG_ALARM = string.Format("Data Source={0};Version=3;", DB_NAME_ALARM);


		#region 数据库操作
		/// <summary>
		/// 创建数据库
		/// </summary>
		/// <param name="dBFileName">数据库文件名称</param>
		public static void CreateDatabase(string dBFileName)
		{
			SQLiteConnection.CreateFile(dBFileName);
		}

		/// <summary>
		/// 断开数据库连接
		/// </summary>
		/// <param name="conn">数据库连接对象</param>
		public static void Disconnection(SQLiteConnection conn)
		{
			if (conn != null && conn.State == ConnectionState.Open)
			{
				conn.Close();
				conn = null;
			}
		}
		#endregion

		#region ExecuteNonQuery
		/// <summary>
		/// 执行数据库操作(新增、修改、删除，创建表等)
		/// </summary>
		/// <param name="connString">数据库连接字符串</param>
		/// <param name="cmd">SqlCommand对象</param>
		/// <returns>影响的行数</returns>
		public static int ExecuteNonQuery(string connString, SQLiteCommand cmd)
		{
			int result = 0;
			if (string.IsNullOrEmpty(connString))
			{
				throw new ArgumentNullException("connString");
			}

			// 建立连接
			SQLiteConnection conn = new SQLiteConnection(connString);
			SQLiteTransaction trans = null;

			// 准备参数
			PrepareCommand(cmd, conn, ref trans, true, cmd.CommandText);

			try
			{
				result = cmd.ExecuteNonQuery();
				trans.Commit();
			}
			catch (Exception ex)
			{
				trans.Rollback();
				throw ex;
			}

			return result;
		}


		/// <summary>
		/// 执行数据库操作(新增、修改、删除，创建表等)
		/// </summary>
		/// <param name="connString">数据库连接字符串</param>
		/// <param name="strSql">执行的SQL语句</param>
		/// <returns>影响的行数</returns>
		public static int ExecuteNonQuery(string connString, string strSql)
		{
			int result = 0;
			if (string.IsNullOrEmpty(connString))
			{
				throw new ArgumentNullException("connString");
			}
			if (string.IsNullOrEmpty(strSql))
			{
				throw new ArgumentNullException("strSql");
			}

			SQLiteCommand cmd = new SQLiteCommand();
			SQLiteConnection conn = new SQLiteConnection(connString);
			SQLiteTransaction trans = null;

			PrepareCommand(cmd, conn, ref trans, true, strSql);
			try
			{
				result = cmd.ExecuteNonQuery();
				trans.Commit();
			}
			catch (Exception ex)
			{
				trans.Rollback();
				throw ex;
			}

			return result;
		}

		#endregion


		#region ExecuteScalar
		/// <summary>
		/// 执行数据库操作(新增、更新，删除，查询)同时返回执行后查询所得的第1行第1列数据
		/// 说明：如果SQL语句是Select查询，则仅仅返回查询结果集中第一行第一列，而忽略其他行和列；
		///      如果SQL语句不是Select查询，则这个返回结果没任何作用；
		/// </summary>
		/// <param name="connString">数据库连接字符串</param>
		/// <param name="cmd">SQLiteCommand对象</param>
		/// <returns>执行完语句后，查询所得的第1行第1列数据</returns>
		public static object ExecuteScalar(string connString, SQLiteCommand cmd)
		{
			object result = 0;

			if (string.IsNullOrEmpty(connString))
			{
				throw new ArgumentNullException("connString");
			}
			SQLiteConnection conn = new SQLiteConnection(connString);
			SQLiteTransaction trans = null;

			PrepareCommand(cmd, conn, ref trans, true, cmd.CommandText);

			try
			{
				result = cmd.ExecuteScalar();
				trans.Commit();
			}
			catch ( Exception ex)
			{
				trans.Rollback();
				throw ex;
			}

			return result;
		}

		/// <summary>
		/// 执行数据库操作(新增、更新、删除、查询)同时返回执行后查询所得的第1行第1列数据
		/// 说明：如果SQL语句是Select查询，则仅仅返回查询结果集中第一行第一列，而忽略其他行和列；
		///      如果SQL语句不是Select查询，则这个返回结果没任何作用；
		/// </summary>
		/// <param name="connString">数据库连接字符串</param>
		/// <param name="strSql">执行的SQL语句</param>
		/// <returns>执行完语句后，查询所得的第1行第1列数据</returns>
		public static object ExecuteScalar(string connString, string strSql)
		{
			object result = 0;
			
			if (string.IsNullOrEmpty(connString))
			{
				throw new ArgumentNullException("connString");
			}
			if (string.IsNullOrEmpty(strSql))
			{
				throw new ArgumentNullException("strSql");
			}

			SQLiteCommand cmd = new SQLiteCommand();
			SQLiteConnection conn = new SQLiteConnection(connString);
			SQLiteTransaction trans = null;

			PrepareCommand(cmd, conn, ref trans, true, strSql);
			try
			{
				result = cmd.ExecuteScalar();
				trans.Commit();
			}
			catch ( Exception ex)
			{
				trans.Rollback();
				throw ex;
			}

			return result;
		}
		#endregion

		#region ExecuteReader
		/// <summary>
		/// 执行数据库查询，返回SqlDataReader对象
		/// 说明： 适合数据量较小的查询，是基于连接的
		/// </summary>
		/// <param name="connString">数据库连接字符串</param>
		/// <param name="cmd">SQLiteCommand对象</param>
		/// <returns>SqlDataReader对象</returns>
		public static DbDataReader ExecuteReader(string connString, SQLiteCommand cmd)
		{
			DbDataReader reader = null;

			if (string.IsNullOrEmpty(connString))
			{
				throw new ArgumentNullException("connSting");
			}

			SQLiteConnection conn = new SQLiteConnection(connString);
			SQLiteTransaction trans = null;

			PrepareCommand(cmd, conn, ref trans, false, cmd.CommandText);
			try
			{
				reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return reader;
		}


		/// <summary>
		/// 执行数据库查询，返回SqlDataReader对象
		/// 说明： 适合数据量较小的查询，是基于连接的
		/// </summary>
		/// <param name="connString"></param>
		/// <param name="strSql"></param>
		/// <returns></returns>
		public static DbDataReader ExecuteReader(string connString, string strSql)
		{
			DbDataReader reader = null;

			if (string.IsNullOrEmpty(connString))
			{
				throw new ArgumentNullException("connString");
			}
			if (string.IsNullOrEmpty(strSql))
			{
				throw new ArgumentNullException("strSql");
			}

			SQLiteConnection conn = new SQLiteConnection(connString);
			SQLiteCommand cmd = new SQLiteCommand();
			SQLiteTransaction trans = null;

			PrepareCommand(cmd, conn, ref trans, false, strSql);

			try
			{
				reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch ( Exception ex)
			{
				throw ex;
			}

			return reader;
		}

		#endregion

		#region ExecuteDataSet

		/// <summary>
		/// 执行数据库查询，返回DataSet对象
		/// 说明：适合数据量较大的查询，是基于非连接的
		/// </summary>
		/// <param name="connString">数据库连接字符串</param>
		/// <param name="cmd">SQLiteCommand对象</param>
		/// <returns>DataSet对象</returns>
		public static DataSet ExecuteDataSet(string connString, SQLiteCommand cmd)
		{
			DataSet ds = new DataSet();

			SQLiteConnection conn = new SQLiteConnection(connString);
			SQLiteTransaction trans = null;

			PrepareCommand(cmd, conn, ref trans, false, cmd.CommandText);

			try
			{
				SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
				sda.Fill(ds);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (conn != null && conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}

			return ds;
		}

		/// <summary>
		/// 执行数据库查询，返回DataSet对象
		/// 说明：适合数据量较大的查询，是基于非连接的
		/// </summary>
		/// <param name="connString">数据库连接字符串</param>
		/// <param name="strSql">SQL语句</param>
		/// <returns>DataSet对象</returns>
		public static DataSet ExecuteDataSet(string connString, string strSql)
		{
			DataSet ds = new DataSet();

			if (string.IsNullOrEmpty(connString))
			{
				throw new ArgumentNullException("connString");
			}
			if (string.IsNullOrEmpty(strSql))
			{
				throw new ArgumentNullException("strSql");
			}

			SQLiteConnection conn = new SQLiteConnection(connString);
			SQLiteCommand cmd = new SQLiteCommand();
			SQLiteTransaction trans = null;

			PrepareCommand(cmd, conn, ref trans, false, strSql);

			try
			{
				SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
				sda.Fill(ds);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (conn != null && conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}

			return ds;
		}
		#endregion

		#region 通用分页查询方法
		/// <summary>
		/// 通用分页查询方法
		/// </summary>
		/// <param name="connString">数据库连接字符串</param>
		/// <param name="tableName">表名</param>
		/// <param name="strColumns">查询字段名</param>
		/// <param name="strWhere">where条件</param>
		/// <param name="strOrder">排序条件</param>
		/// <param name="pageSize">每页显示条数</param>
		/// <param name="currentIndex">当前页数</param>
		/// <param name="totalCount">数据总条数</param>
		/// <returns></returns>
		public static DataTable SelectPaging(string connString, string tableName
			, string strColumns, string strWhere, string strOrder, int pageSize
			, int currentIndex, out int totalCount)
		{
			totalCount = 0;
			DataTable dt = new DataTable();

			// SQL
			string strSql = string.Format("SELECT COUNT(*) FROM {0} ", tableName);
			totalCount = Convert.ToInt32(ExecuteScalar(connString, strSql));

			int offsetCount = (currentIndex - 1) * pageSize;
			strSql = string.Format("SELECT {0} FROM {1} WHERE {2} ORDER BY {3} LIMIT {4} OFFSET {5} ",
				strColumns, tableName, strWhere, strOrder, pageSize.ToString(), offsetCount.ToString());

			DbDataReader reader = ExecuteReader(connString, strSql);
			if (reader != null)
			{
				dt.Load(reader);
			}

			return dt;
		}
		#endregion

		#region PrepareCommand
		/// <summary>
		/// 预处理Command对象,数据库链接,事务,需要执行的对象,参数等的初始化
		/// </summary>
		/// <param name="cmd">Command对象</param>
		/// <param name="conn">Connection对象</param>
		/// <param name="trans">Transcation对象</param>
		/// <param name="useTrans">是否使用事务</param>
		/// <param name="cmdText">SQL语句</param>
		private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn
			, ref SQLiteTransaction trans, bool useTrans, string cmdText)
		{
			if (conn.State != ConnectionState.Open)
			{
				conn.Open();
			}

			cmd.Connection = conn;
			cmd.CommandText = cmdText;

			if (useTrans)
			{
				trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
				cmd.Transaction = trans;
			}

			// cmd.CommandType = cmdType;
		}
		#endregion

	}
}

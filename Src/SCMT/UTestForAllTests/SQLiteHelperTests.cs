/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ SQLiteHelperTests $
* 机器名称：       $ machinename $
* 命名空间：       $ UTestForAll.Tests $
* 文 件 名：       $ SQLiteHelperTests.cs $
* 创建时间：       $ 2018.10.28 $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     SQLite数据库帮助测试类,SQLite数据库使用实例
* 修改时间     修 改 人         修改内容：
* 2018.10.xx  XXXX            XXXXX
*************************************************************************************/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBaseUtil;
using System;
using System.Text;
using System.Diagnostics;
using System.Data.Common;
using System.Data;
using System.Data.SQLite;

namespace UTestForAll.Tests
{
	[TestClass()]
	public class SQLiteHelperTests
	{
		#region 创建数据库、创建表等操作
		/// <summary>
		/// 创建数据库
		/// </summary>
		[TestMethod()]
		public void CreateDatabaseTest()
		{
			// 创建数据库
			SQLiteHelper.CreateDatabase(SQLiteHelper.DB_NAME_MAIN);
		}

		/// <summary>
		/// 创建表
		/// </summary>
		[TestMethod()]
		public void CreateTableTest()
		{
			// 组装SQL
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("CREATE TABLE IF NOT EXISTS tblTest");
			sbSql.Append(" (");
			sbSql.Append("id INTEGER PRIMARY KEY AUTOINCREMENT"); // 主键自增,必须为INTEGER类型，INT就会出错
			sbSql.Append(", name VARCHAR(20)");
			sbSql.Append(", age INT");
			sbSql.Append(", address VARCHAR(50)");
			sbSql.Append(", create_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP"); // 默认值为记录创建时间
			sbSql.Append(", update_time TIMESTAMP");
			sbSql.Append(")");

			// 执行SQL
			int result = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONN_CFG_MAIN, sbSql.ToString());

			Debug.WriteLine("--------");
		}

		/// <summary>
		/// 删除表
		/// </summary>
		[TestMethod]
		public void DelTableTest()
		{
			// 组装SQL
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("DROP TABLE IF EXISTS tblTest");

			// 执行SQL
			int result = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONN_CFG_MAIN, sbSql.ToString());

			Debug.WriteLine("----------");
		}

		#endregion

		#region 表的增、删、改、查操作
		/// <summary>
		/// 插入操作
		/// </summary>
		[TestMethod]
		public void InsertTableTest()
		{
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("INSERT INTO tblTest (id, name, age, address, update_time)");
			sbSql.Append(" VALUES (NULL, 'tom', 20, '学院路20号', DATETIME('now', 'localtime'))");

			// 执行SQL
			int result = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONN_CFG_MAIN, sbSql.ToString());

			Debug.WriteLine("--------");
		}

		/// <summary>
		/// 检索表数据（使用基于连接的Reader）
		/// </summary>
		[TestMethod]
		public void QueryTableByReaderTest()
		{
			// 组装SQL
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("SELECT * FROM tblTest");
			sbSql.Append(" WHERE name='").Append("tom'");

			// 执行SQL
			DbDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.CONN_CFG_MAIN, sbSql.ToString());

			// 读取数据
			while (reader.Read())// 每执行一次Read()，就会实时的到数据库读取一条记录，所以Reader是基于连接的,不是一次性读出所有符合条件的记录
			{
				Debug.WriteLine(string.Format("Id:{0}, Name:{1}, Age:{2}, Address:{3}, 创建时间:{4}, 更新时间:{5}"
					, reader["id"], reader["name"], reader["age"], reader["address"], reader["create_time"], reader["update_time"]));
			}

			if (reader != null) // 读完记录必须关闭Reader
			{
				reader.Close();
			}


			Debug.WriteLine("----------");

		}

		/// <summary>
		/// 检索表数据，参数为SQLiteCommand对象，便于操作连接、事务等（使用基于连接的Reader）
		/// </summary>
		[TestMethod]
		public void QueryTableByReaderCmdTest()
		{
			// 组装SQL
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("SELECT * FROM tblTest");
			sbSql.Append(" WHERE name='").Append("tom'");

			SQLiteCommand cmd = new SQLiteCommand();
			cmd.CommandText = sbSql.ToString();

			// 执行SQL
			DbDataReader reader = SQLiteHelper.ExecuteReader(SQLiteHelper.CONN_CFG_MAIN, cmd);

			// 读取数据
			while (reader.Read())// 每执行一次Read()，就会实时的到数据库读取一条记录，所以Reader是基于连接的,不是一次性读出所有符合条件的记录
			{
				Debug.WriteLine(string.Format("Id:{0}, Name:{1}, Age:{2}, Address:{3}, 创建时间:{4}, 更新时间:{5}"
					, reader["id"], reader["name"], reader["age"], reader["address"], reader["create_time"], reader["update_time"]));
			}

			if (reader != null) // 读完记录必须关闭Reader
			{
				reader.Close();
			}


			Debug.WriteLine("----------");
		}

		/// <summary>
		/// 检索表数据（使用基于非连接的SQLiteDataAdapter + DataSet）
		/// </summary>
		[TestMethod]
		public void QueryTableByDataSetTest()
		{
			// 组装SQL
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("SELECT * FROM tblTest");
			sbSql.Append(" WHERE name='").Append("tom'");

			// 执行SQL(是基于非连接的，一次性将所有数据读取到DataSet中，然后连接自动断开，后续只需要操作DataSet即可)
			DataSet ds = SQLiteHelper.ExecuteDataSet(SQLiteHelper.CONN_CFG_MAIN, sbSql.ToString());

			// 读取数据
			foreach (DataTable dt in ds.Tables)
			{
				foreach(DataRow dr in dt.Rows)
				{
					Debug.WriteLine(string.Format("Id:{0}, Name:{1}, Age:{2}, Address:{3}, 创建时间:{4}, 更新时间:{5}"
						, dr["id"], dr["name"], dr["age"], dr["address"], dr["create_time"], dr["update_time"]));

				}
			}

			Debug.WriteLine("---------------");
		}

		/// <summary>
		/// 检索表数据,使用SQLiteCommand对象，便于操作连接、事务等（使用基于非连接的SQLiteDataAdapter + DataSet）
		/// </summary>
		[TestMethod]
		public void QueryTableByDataSetCmdTest()
		{
			// 组装SQL
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("SELECT * FROM tblTest");
			sbSql.Append(" WHERE name='").Append("tom'");

			SQLiteCommand cmd = new SQLiteCommand();
			cmd.CommandText = sbSql.ToString();

			// 执行SQL(是基于非连接的，一次性将所有数据读取到DataSet中，然后连接自动断开，后续只需要操作DataSet即可)
			DataSet ds = SQLiteHelper.ExecuteDataSet(SQLiteHelper.CONN_CFG_MAIN, cmd);

			// 读取数据
			foreach (DataTable dt in ds.Tables)
			{
				foreach (DataRow dr in dt.Rows)
				{
					Debug.WriteLine(string.Format("Id:{0}, Name:{1}, Age:{2}, Address:{3}, 创建时间:{4}, 更新时间:{5}"
						, dr["id"], dr["name"], dr["age"], dr["address"], dr["create_time"], dr["update_time"]));

				}
			}

			Debug.WriteLine("---------------");
		}


		/// <summary>
		/// 更新操作
		/// </summary>
		[TestMethod]
		public void UpdateTableTest()
		{
			// 组装SQL语句
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("UPDATE tblTest");
			sbSql.Append(" SET name='jack', age=10, address='地大附中旁边', update_time=DATETIME('now', 'localtime')");
			sbSql.Append(" WHERE id=2");

			// 执行SQL
			int result = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONN_CFG_MAIN, sbSql.ToString());

			Debug.WriteLine("-----------------");
		}

		/// <summary>
		/// 更新操作，参数为SQLiteCommand对象，便于操作连接、事务等
		/// </summary>
		[TestMethod]
		public void UpdateTableCmdTest()
		{
			// 组装SQL语句
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("UPDATE tblTest");
			sbSql.Append(" SET name='jack', age=10, address='地大附中旁边', update_time=DATETIME('now', 'localtime')");
			sbSql.Append(" WHERE id=2");

			SQLiteCommand cmd = new SQLiteCommand();
			cmd.CommandText = sbSql.ToString();

			// 执行SQL
			int result = SQLiteHelper.ExecuteNonQuery(SQLiteHelper.CONN_CFG_MAIN, cmd);

			Debug.WriteLine("-----------------");
		}

		#endregion

		#region ExecuteScalar操作
		/// <summary>
		/// ExecuteScalar测试
		/// </summary>
		[TestMethod]
		public void ScalarTest()
		{
			// 组装SQL
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("SELECT COUNT(*) FROM tblTest");
			sbSql.Append(" WHERE 1=1");

			// 执行SQL,返回第一行第一列 (ExecuteScalar()方法返回的是object类型，根据具体情况可以转换为实际的类型)
			object ObjTotal = SQLiteHelper.ExecuteScalar(SQLiteHelper.CONN_CFG_MAIN, sbSql.ToString());
			int total = Convert.ToInt32(ObjTotal);

			Debug.WriteLine("Total:{0}", total);
		}

		/// <summary>
		/// ExecuteScalar测试，参数为SQLCommand对象，可以控制连接状态、事务等
		/// </summary>
		[TestMethod]
		public void ScalarCmdTest()
		{
			// 组装SQL
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("SELECT COUNT(*) FROM tblTest");
			sbSql.Append(" WHERE 1=1");

			SQLiteCommand cmd = new SQLiteCommand();
			cmd.CommandText = sbSql.ToString();

			// 执行SQL，参数为SQLCommand对象，可以控制连接状态、事务等
			object obj = SQLiteHelper.ExecuteScalar(SQLiteHelper.CONN_CFG_MAIN, cmd);
			int total = Convert.ToInt32(obj);

			Debug.WriteLine("Total: {0}", total);

		}
		#endregion

		#region 事务操作
		/// <summary>
		/// 带事务的数据库操作
		/// </summary>
		[TestMethod]
		public void UpdateWithTransaction()
		{
			SQLiteConnection conn = new SQLiteConnection(SQLiteHelper.CONN_CFG_MAIN);
			if (conn.State != ConnectionState.Open)
			{
				conn.Open();
			}
			SQLiteCommand cmd = new SQLiteCommand();
			cmd.Connection = conn;

			// 事务开始
			SQLiteTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
			cmd.Transaction = trans;

			try
			{
				// 组装SQL语句
				StringBuilder sbSql = new StringBuilder();
				// 执行多个数据库操作
				for (int i = 1; i < 4; i++)
				{
					// SQL
					sbSql.Clear();
					sbSql.Append("UPDATE tblTest");
					sbSql.Append(" SET name='jack', age=10, address='地大附中旁边', update_time=DATETIME('now', 'localtime')");
					sbSql.Append(" WHERE id=").Append(i);
					// 设置SQL
					cmd.CommandText = sbSql.ToString();

					// 执行SQL
					int result = cmd.ExecuteNonQuery();
				}

				// 提交事务
				trans.Commit();
			}
			catch (Exception ex)
			{
				// 回滚事务
				trans.Rollback();
				throw ex;
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}

			}


			Debug.WriteLine("-----------------");

		}
		#endregion

	}
}
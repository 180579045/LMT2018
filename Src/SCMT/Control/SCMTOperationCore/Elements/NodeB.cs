using MIBDataParser.JSONDataMgr;
using System;
using System.Net;
using System.Windows;
using CommonUtility;

namespace SCMTOperationCore.Elements
{
	/// <summary>
	/// NodeB是基于SI的连接;
	/// </summary>
	public class NodeB : SiElement
	{
		public string m_ObjTreeDataPath { get; }         // 基站对象树数据库数据源;
		public string m_MibDataPath { get; }             // 基站Mib数据源;
		public Database db { get; set; }                 // 用来全局保存数据库;
		public IPAddress m_IPAddress;

		// TODO 基站类型：EMB5116，EMB6116

		public NodeB(string neIp, string friendName, ushort nePort = 5000)
		: base(friendName, IPAddress.Parse(neIp), nePort)
		{
			try
			{
				this.m_IPAddress = IPAddress.Parse(neIp);
				this.m_ObjTreeDataPath = ConfigFileHelper.ObjTreeReferenceJson;

			}
			catch(Exception e)
			{
				MessageBox.Show("加载对象树文件失败;" + e.ToString());
			}


			// 此后的动作;
			// 1、创建数据库保存路径,并保存到对应的属性成员中;
			// 2、更新用户界面，在界面中添加节点按钮;
			// 3、以管理站接入基站的流程;
		}
		// 对应的对象树模型;
		// 对应的数据库;
		// 在考虑;

		// 节点的类型。5G的还要扩充
		public void SetType(string neType)
		{
			if (!string.IsNullOrEmpty(neType) && !string.IsNullOrWhiteSpace(neType))
			{
				var type = byte.Parse(neType);
				NodeType = (EnbTypeEnum) type;
			}
		}

		#region 公共属性

		public EnbTypeEnum NodeType { get; protected set; }

		#endregion
	}

	public enum EnbTypeEnum : byte
	{
		ENB_NULL = 0,
		ENB_TLB60A = 2,
		ENB_EMB5216 = 3,
		EMB5132_DTLTE = 4,
		ENB_EMB5116 = 5
	};
}

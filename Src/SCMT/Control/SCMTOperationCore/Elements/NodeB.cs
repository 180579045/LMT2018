using System;
using System.Net;

namespace SCMTOperationCore.Elements
{
	/// <summary>
	/// NodeB是基于SI的连接;
	/// </summary>
	public class NodeB : SiElement
	{
		public string m_ObjTreeDataPath { get; }         // 基站对象树数据库数据源;

		public string m_MibDataPath { get; }             // 基站Mib数据源;

		public NodeB(string neIp, string friendName, ushort nePort = 5000)
		: base(friendName, IPAddress.Parse(neIp), nePort)
		{
			this.m_ObjTreeDataPath = @"Data\Tree_Reference.json";

			// 此后的动作;
			// 1、创建数据库保存路径,并保存到对应的属性成员中;
			// 2、更新用户界面，在界面中添加节点按钮;
			// 3、以管理站接入基站的流程;
		}
		// 对应的对象树模型;
		// 对应的数据库;
		// 在考虑;
	}
}

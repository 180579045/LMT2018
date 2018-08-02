/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：ObjTree_Control.cs
// 文件功能描述：对象树节点控制类;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2017-11-20
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using SCMTOperationCore.Elements;
using System.Threading.Tasks;
using System.Windows;
using CommonUtility;

namespace SCMTMainWindow
{
	/// <summary>
	/// 对象树管理类;
	/// </summary>
	class ObjNodeControl
	{
		public NodeB m_NodeB { get; set; }                          // 对应的基站;
		public List<ObjNode> m_NodeList { get; set; }               // 对用的对象树列表(后续会改为以基站为索引的dictionary);
		public List<ObjNode> m_RootNode { get; set; }               // Demo只有一个基站，暂时用来保存根节点;
		private string m_ObjFilePath;                               // 暂时用一下，保存JSON文件路径;

		/// <summary>
		/// 实验程序先假定一个LMT只连接一个基站，在构造函数中直接读取默认JSON文件;
		/// </summary>
		public ObjNodeControl(NodeB node)
		{
			m_ObjFilePath = node.m_ObjTreeDataPath;
			//JObject JObj = new JObject();
			try
			{
				ObjNode.nodeb = node;

				var jsonContent = FileRdWrHelper.GetFileContent(FilePathHelper.GetAppPath() + m_ObjFilePath, Encoding.Default);
				var nodeList = JsonHelper.SerializeJsonToObject<Nodes>(jsonContent);

				ParseJObject(nodeList);
			}
			catch(Exception e)
			{
				MessageBox.Show("1加载数据库失败\r\n" + e.ToString());
			}
		}

		/// <summary>
		/// 解析JObject到一个容器当中;
		/// </summary>
		/// <param name="obj">从文件中解析出来的JSON对象;</param>
		/// <returns>返回一个保存了所有节点的容器;</returns>
		private void ParseJObject(Nodes nodes)
		{
			var AllNodes = nodes.NodeList;
			m_NodeList = new List<ObjNode>();
			int TempCount = 0;

			var version = nodes.version;

			// 遍历所有JSON文件中的节点,并添加进列表中;
			foreach (var iter in AllNodes)
			{
				var ObjParentNodes = iter.ObjParentID;
				var name = iter.ObjName;
				var TableName = iter.MibTableName;

				ObjNode objNode;
				if (iter.ChildRenCount != 0)
				{
					objNode = new ObjTreeNode(iter.ObjID, ObjParentNodes, version, name, TableName);
				}
				else
				{
					objNode = new ObjLeafNode(iter.ObjID, ObjParentNodes, version, name, TableName);
				}

				m_NodeList.Add(objNode);
			}
			m_RootNode = ArrangeParentage(m_NodeList);
		}

		/// <summary>
		/// 确认亲子关系;
		/// </summary>
		/// <param name="NodeList"></param>
		/// <returns></returns>
		public static List<ObjNode> ArrangeParentage(List<ObjNode> NodeList)
		{
			List<ObjNode> RootNodeShow = new List<ObjNode>();
			ObjNode Root = new ObjTreeNode(0, 0, "1.0", "基站节点列表",@"/");

			// 遍历所有节点确认亲子关系;
			foreach (ObjNode iter in NodeList)
			{
				if (iter.ObjParentID == 0)
				{
					Root.Add(iter);
				}
				else
				{
					foreach (ObjNode iterParent in NodeList)
					{
						if (iterParent.ObjID == iter.ObjParentID)
						{
							iterParent.Add(iter);
						}
					}
				}
			}

			RootNodeShow.Add(Root);

			return RootNodeShow;
		}
	}

	#region 解析json文件所需的类

	internal class OneNode
	{
		public int ObjID;
		public int ObjParentID;
		public int ChildRenCount;
		public string ObjName;
		public string ObjNameEn;
		public string MibTableName;
		public string MIBList;
	}

	internal class Nodes
	{
		public string version;
		public List<OneNode> NodeList;
	}

	#endregion
}

using System;
using SCMTOperationCore.Elements;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using BaseStationConInfo.BSCInfoMgr;
using CommonUtility;
using LogManager;

//节点管理功能
namespace SCMTOperationCore.Control
{
	public class NodeBControl : ElementControl
	{
		#region 静态函数区
		public static NodeBControl GetInstance()
		{
			return Singleton<NodeBControl>.GetInstance();
		}

		public static bool SendSiMsg(string nodeIp, byte[] dataBytes)
		{
			return GetInstance().SendSiMsgToTarget(nodeIp, dataBytes);
		}
		#endregion

		#region 构造函数

		private NodeBControl()
		{
			mapElements = new Dictionary<string, Element>();
		}

		#endregion

		#region 公共接口

		/// <summary>
		/// 初始化已存在的节点信息
		/// </summary>
		/// <returns>节点的友好名和IP地址。key:友好名，value:连接IP</returns>
		public Dictionary<string, string> GetNodebInfo()
		{
			// 从数据库模块中读取已经添加的节点信息
			var nodesInfo = new Dictionary<string, string>();
			if (BSConInfo.GetInstance().getBaseStationConInfo(nodesInfo))
			{
				foreach (var nodeInfo in nodesInfo)
				{
					AddElement(nodeInfo.Value, nodeInfo.Key);
				}
			}

			return nodesInfo;
		}

		/// <summary>
		/// 添加NodeB网元。IP、友好名不能重复；最多20个节点
		/// </summary>
		/// <param name="ip">网元IP地址</param>
		/// <param name="friendlyName">网元友好名</param>
		/// <param name="port">连接端口，默认5000</param>
		/// <exception cref="CustomException">抛出错误信息，调用者catch到直接使用即可</exception>
		/// <returns>Element obj</returns>
		public override Element AddElement(string ip, string friendlyName, ushort port = 5000)
		{
			if (HasTwentyNodebs())
			{
				throw new CustomException("当前已存在20个基站");
			}

			string errorInfo = "";
			if (HasSameFriendlyName(friendlyName))
			{
				errorInfo = $"友好名为{friendlyName}的基站已存在";
				Log.Error(errorInfo);
				throw new CustomException(errorInfo);
			}

			if (HasSameIpAddr(ip))
			{
				errorInfo = $"地址为{ip}的基站已存在，友好名为{GetFriendlyNameByIp(ip)}";
				Log.Error(errorInfo);
				throw new CustomException(errorInfo);
			}

			Element newNodeb = new NodeB(ip, friendlyName, port);
			AddElement(ip, newNodeb);

			if (!BSConInfo.GetInstance().addBaseStationConInfo(friendlyName, ip))
			{
				Log.Error("数据库模块增加节点配置失败");
			}

			return newNodeb;
		}

        /// <summary>
        /// 修改网元的友好名，根据IP地址直接替换原来的友好名
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="friendlyName"></param>
        /// <returns></returns>
        public bool ModifyElementFriendlyName(string ip, string friendlyName)
        {
            string errorInfo = "";
            if (!HasSameIpAddr(ip))
            {
                errorInfo = $"地址为{ip}的基站不存在，友好名为{friendlyName}";
                Log.Error(errorInfo);
                throw new CustomException(errorInfo);
            }

            if (mapElements.ContainsKey(ip))
            {
                mapElements[ip].FriendlyName = friendlyName;
            }

            if (!BSConInfo.GetInstance().modifyBaseStationConInfoFriendlyName(friendlyName, ip))
            {
                Log.Error("数据库模块修改节点配置失败");
            }

            return true;

        }

        /// <summary>
        /// 修改网元的IP地址
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="friendlyName"></param>
        /// <returns></returns>
        public bool ModifyElementIPAddr(string ip, string friendlyName)
        {
            string errorInfo = "";
            if (!HasSameFriendlyName(friendlyName))
            {
                errorInfo = $"友好名为{friendlyName}的基站不存在";
                Log.Error(errorInfo);
                throw new CustomException(errorInfo);
            }
            if (HasSameIpAddr(ip))
            {
                errorInfo = $"地址为{ip}的基站已存在，友好名为{GetFriendlyNameByIp(ip)}";
                Log.Error(errorInfo);
                throw new CustomException(errorInfo);
            }

            //if (!BSConInfo.GetInstance().modifyBaseStationConInfoIP(friendlyName, ip))
            //{
            //    Log.Error("数据库模块修改节点配置失败");
            //    return false;
            //}
            if (DelElementByFriendlyName(friendlyName))
            {
                Element newNodeb = new NodeB(ip, friendlyName);
                AddElement(ip, newNodeb);
                if (!BSConInfo.GetInstance().addBaseStationConInfo(friendlyName, ip))
                {
                    Log.Error("数据库模块增加节点配置失败");
                    return false;
                }

                return true;
            }
            return false;
        }

        // 删除网元
        public override bool DelElement(string ip)
		{
			if (null == ip || ip.Trim().Equals(""))
			{
				throw new ArgumentNullException("ip is null or only space char");
			}

			var fname = GetFriendlyNameByIp(ip);
			if (null == fname)
			{
				Log.Error($"未找到{ip}对应的友好名");
				return false;
			}

			return DelElementByFriendlyName(fname);
		}

		// 删除网元，传入参数：网元友好名
		public bool DelElementByFriendlyName(string friendlyName)
		{
			if (string.IsNullOrEmpty(friendlyName))
			{
				return false;
			}

			var node = GetNodeByFName(friendlyName);
			if (null == node)
			{
				return false;
			}

			RmElement(node.NeAddress.ToString());

			if (!BSConInfo.GetInstance().delBaseStationConInfoByName(friendlyName))
			{
				Log.Error($"数据库删除友好名为{friendlyName}的节点数据失败");
				return false;
			}

			return true;
		}

		//获取网元的友好名
		public string GetFriendlyNameByIp(string ip)
		{
			if (null == ip || ip.Trim().Equals(""))
			{
				throw new ArgumentNullException("ip is null or only space char");
			}

			lock (lockObj)
			{
				if (mapElements.ContainsKey(ip))
				{
					return mapElements[ip].FriendlyName;
				}
			}

			return null;
		}

		// 根据友好名获取网元IP地址
		public string GetNodeIpByFriendlyName(string friendlyName)
		{
			if (string.IsNullOrEmpty(friendlyName))
			{
				throw new ArgumentNullException("friendlyName is null or only space char");
			}

			var node = GetNodeByFName(friendlyName) as NodeB;
			return node?.m_IPAddress.ToString();
		}

		//判断友好名是否重复
		public bool HasSameFriendlyName(string friendlyName)
		{
			if (null == friendlyName || friendlyName.Trim().Equals(""))
			{
				throw new ArgumentNullException("friendlyName is null or only space char");
			}

			lock (lockObj)
			{
				var elements = mapElements.Values;
				foreach (var item in elements)
				{
					if (Equals(item.FriendlyName, friendlyName))
					{
						return true;
					}
				}
			}

			return false;
		}

		//判断IP是否重复
		public bool HasSameIpAddr(string ip)
		{
			if (null == ip || ip.Trim().Equals(""))
			{
				throw new ArgumentNullException("ip is null or only space char");
			}

			lock (lockObj)
			{
				return mapElements.ContainsKey(ip);
			}
		}

		// 连接基站，传入参数：友好名
		public bool ConnectNodeb(string friendlyName)
		{
			var et = GetNodeByFName(friendlyName) as NodeB;
			et?.ConnectAsync();
			return true;
		}

		// 断开连接，传入参数：友好名
		public bool DisConnectNodeb(string friendlyName)
		{
			var et = GetNodeByFName(friendlyName) as NodeB;
			et?.DisConnect();
			return true;
		}

		// 设置网元的类型
		public void SetNetElementType(string ip, string neType)
		{
			var et = GetNodeByIp(ip) as NodeB;
			et?.SetType(neType);
		}

		// 根据友好名获取节点信息
		public Element GetNodeByFName(string name)
		{
			Dictionary<string, Element>.ValueCollection vc = null;
			lock (lockObj)
			{
				vc = mapElements.Values;
			}

			foreach (var element in vc)
			{
				if (element.FriendlyName.Equals(name))
				{
					return element;
				}
			}

			return null;
		}

		/// <summary>
		/// 获取基站是4G还是5G站
		/// </summary>
		/// <param name="targetIp"></param>
		public EnbTypeEnum GetEnbTypeByIp(string targetIp)
		{
			var el = GetNodeByIp(targetIp) as NodeB;
			return el?.NodeType ?? EnbTypeEnum.ENB_NULL;
		}


		public void SetNodebGridByIp(string targetIp, EnbTypeEnum st)
		{
			var el = GetNodeByIp(targetIp) as NodeB;
			el?.SetType(st);
		}

		#endregion

		#region 私有函数区

		//判断网元数量是否已经到达最大值20
		private bool HasTwentyNodebs()
		{
			lock (lockObj)
			{
				return (mapElements.Count >= 20);
			}
		}

		//往mapElements结构中增加元素
		private void AddElement(string key, Element value)
		{
			lock (lockObj)
			{
				mapElements[key] = value;
			}
		}

		//删除节点
		private void RmElement(string key)
		{
			lock (lockObj)
			{
				if (mapElements.ContainsKey(key))
				{
					mapElements.Remove(key);
				}
			}
		}

		//判断节点的连接状态
		private bool NodeHasConnected(string ip)
		{
			lock (lockObj)
			{
				var node = mapElements[ip] as NodeB;
				return node.HasConnected();
			}
		}

		private bool SendSiMsgToTarget(string nodeIp, byte[] dataBytes)
		{
			if (!HasSameIpAddr(nodeIp))
			{
				Log.Debug($"待查询的节点{nodeIp}不存在，无法发送Si消息");
				return false;
			}

			lock (lockObj)
			{
				var nodeb = mapElements[nodeIp] as NodeB;
				return nodeb.SendSiMsg(dataBytes);
			}
		}

		// 根据网元IP获取网元节点
		public Element GetNodeByIp(string ip)
		{
			Element nodeb = null;
			lock (lockObj)
			{
				if (mapElements.ContainsKey(ip))
				{
					nodeb = mapElements[ip] as NodeB;
				}
			}

			return nodeb;
		}

		#endregion



		//key:ip， value: Element obj
		private readonly Dictionary<string, Element> mapElements;
		private static readonly object lockObj = new object();
	}
}

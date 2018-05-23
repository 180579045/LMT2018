using System;
using SCMTOperationCore.Elements;
using System.Collections.Generic;
using System.Net;
using CommonUility;
using LogManager;

//节点管理功能
namespace SCMTOperationCore.Control
{
	public class NodeBControl : ElementControl
	{
		public static NodeBControl GetInstance()
		{
			return Singleton<NodeBControl>.GetInstance();
		}

		public NodeBControl()
		{
			mapElements = new Dictionary<string, Element>();
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
				errorInfo = $"友好名为：{friendlyName}的网元已存在";
				Log.Error(errorInfo);
				throw new CustomException(errorInfo);
			}

			if (HasSameIpAddr(ip))
			{
				errorInfo = $"地址为：{ip}的网元已存在，网元友好名为：{GetFriendlyNameByIp(ip)}";
				Log.Error(errorInfo);
				throw new CustomException(errorInfo);
			}

			Element newNodeb = new NodeB(ip, friendlyName, port);
			AddElement(ip, newNodeb);

			return newNodeb;
		}

		public override bool DelElement(string ip)
		{
			if (null == ip || ip.Trim().Equals(""))
			{
				throw new ArgumentNullException("ip is null or only space char");
			}

			RmElement(ip);
			return true;
		}

		//判断友好名是否重复
		private bool HasSameIpAddr(string ip)
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

		//判断IP是否重复
		private bool HasSameFriendlyName(string friendlyName)
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

		//判断网元数量是否已经到达最大值20
		private bool HasTwentyNodebs()
		{
			lock (lockObj)
			{
				return (mapElements.Count >= 20);
			}
		}

		//获取网元的友好名
		private string GetFriendlyNameByIp(string ip)
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

		//key:ip， value: Element obj
		private readonly Dictionary<string, Element> mapElements;
		private static readonly object lockObj = new object();
	}
}

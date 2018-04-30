using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace CommonUility
{
	public class NetActHelper
	{
		//保存IP对应的mac地址。first : IPV4 addr，second : mac addr
		private static readonly Dictionary<string, byte[]> IpToMacAddr = new Dictionary<string, byte[]>();

		/// <summary>
		/// ip地址转换为uint
		/// </summary>
		/// <param name="ip">ipv4 点分十进制地址</param>
		/// <returns></returns>
		public static uint inet_addr(string ip)
		{
			return (uint)BitConverter.ToInt32(IPAddress.Parse(ip).GetAddressBytes(), 0);
		}

		public static string inet_ntoa(uint ip)
		{
			return new IPAddress(BitConverter.GetBytes(ip)).ToString();
		}

		public static byte[] GetMacAddrByIp(string ipAddr)
		{
			byte[] macBytes = GetMacBytesByIp(ipAddr);
			if (null != macBytes)
			{
				return macBytes;
			}

			try
			{
				NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
				foreach (var adapter in nics)
				{
					var ipInterfaceProperties = adapter.GetIPProperties();
					var ipv4Properties = ipInterfaceProperties.UnicastAddresses;
					foreach (var itemProperty in ipv4Properties)
					{
						if (itemProperty.Address.ToString().Equals(ipAddr))
						{
							macBytes = adapter.GetPhysicalAddress().GetAddressBytes();
							SetMacBytes(ipAddr, macBytes);
							return macBytes;
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return null;
		}

		/// <summary>
		/// 根据IP地址获取mac地址
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		private static byte[] GetMacBytesByIp(string ip)
		{
			return IpToMacAddr.ContainsKey(ip) ? IpToMacAddr[ip] : null;
		}

		/// <summary>
		/// 保存ip对应的网卡mac地址
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="mac"></param>
		private static void SetMacBytes(string ip, byte[] mac)
		{
			IpToMacAddr[ip] = mac;
		}
	}
}

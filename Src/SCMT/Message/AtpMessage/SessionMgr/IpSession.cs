using System;
using System.Linq;
using MsgQueue;
using SharpPcap;
using SharpPcap.LibPcap;

namespace AtpMessage.SessionMgr
{
	/// <summary>
	/// 处理gtsa抄送的消息，抓回来的是IP层数据
	/// </summary>
	class IpSession
	{
		private LibPcapLiveDevice _deviceCapture;
		private string _pubTopic;
		private bool _stoped;

		/// <summary>
		/// 根据IP地址获取设备，用于抓包
		/// </summary>
		/// <param name="ip">本地网卡的IP地址</param>
		/// <returns>LibPcapLiveDevice obj。此处不使用ICaptureDevice类型是因为此类型设备要查询信息需要先调用open，最后还要close</returns>
		private LibPcapLiveDevice GetDeviceWithIp(string ip)
		{
			var deviceList = LibPcapLiveDeviceList.Instance;
			if (deviceList.Count < 1)
			{
				return null;
			}

			foreach (var dev in deviceList)
			{
				try
				{
					var addrs = dev.Addresses;
					if (addrs.Select(addr => addr.Addr.ipAddress.ToString()).Any(ipAddr => ip == ipAddr))
					{
						return dev;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}

			return null;
		}

		/// <summary>
		/// 启动数据包捕获
		/// </summary>
		/// <param name="localIp">本地网卡上的IP地址</param>
		/// <param name="filter">数据包过滤filter</param>
		/// <param name="pubTopic">收到数据包发布的topic TODO 是否合适设置topic</param>
		/// <returns>true:启动接收成功，false:在本地网卡上没有找到对应的本地IP地址</returns>
		public bool Init(string localIp, string filter, string pubTopic)
		{
			_deviceCapture = GetDeviceWithIp(localIp);
			if (null == _deviceCapture)
			{
				return false;
			}

			_pubTopic = pubTopic;
			_deviceCapture.OnPacketArrival += OnPacketArrive;
			_deviceCapture.OnCaptureStopped += OnStopCapture;
			_deviceCapture.Open(DeviceMode.Promiscuous);
			_deviceCapture.Filter = filter;       //filter规则语法和tcpdump的一样，不是wireshark的语法
			_deviceCapture.StartCapture();
			_stoped = false;
			return true;
		}

		/// <summary>
		/// 停止设备接收数据包
		/// </summary>
		/// <returns>true:success，false:otherwise</returns>
		public bool Stop()
		{
			if (null != _deviceCapture && _deviceCapture.Started)
			{
				try
				{
					_deviceCapture?.StopCapture();
				}
				catch (PcapException e)     //此处throw一个异常，但不会造成影响，不必处理
				{
					//Console.WriteLine(e);
					//throw;
				}
			}

			if (null != _deviceCapture && _deviceCapture.Opened)
			{
				_deviceCapture?.Close();
			}

			return true;
		}

		private void OnPacketArrive(object sender, CaptureEventArgs e)
		{
			byte[] data = e.Packet.Data;       //TODO Data转为string，需要替换-为空字符
			PublishHelper.PublishMsg("/GtsMsgParseService", data);
		}

		private void OnStopCapture(object sender, CaptureStoppedEventStatus status)
		{
			//if (status == CaptureStoppedEventStatus.CompletedWithoutError)
			//{
			//	Console.WriteLine("capture thread exit succeed");
			//}

			//if (status == CaptureStoppedEventStatus.ErrorWhileCapturing)
			//{
			//	Console.WriteLine("capture thread has aborted");
			//}
		}
	}
}

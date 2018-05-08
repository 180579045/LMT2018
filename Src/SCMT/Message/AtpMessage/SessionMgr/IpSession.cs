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
	public class IpSession : IASession
	{
		private LibPcapLiveDevice _deviceCapture;

		public IpSession(Target target)
		{

		}

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

		public void SendAsync(byte[] dataBytes)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 启动数据包捕获
		/// </summary>
		/// <param name="localIp">本地网卡上的IP地址</param>
		/// <returns>true:启动接收成功，false:在本地网卡上没有找到对应的本地IP地址</returns>
		public bool Init(string localIp)
		{
			_deviceCapture = GetDeviceWithIp(localIp);
			if (null == _deviceCapture)
			{
				return false;
			}

			_deviceCapture.OnPacketArrival += OnPacketArrive;
			_deviceCapture.OnCaptureStopped += OnStopCapture;
			_deviceCapture.Open(DeviceMode.Promiscuous);
			_deviceCapture.Filter = "udp and udp.srcport==50000";
			_deviceCapture.StartCapture();

			return true;
		}

		//停止设备接收数据包
		public bool Stop()
		{
			if (null != _deviceCapture && _deviceCapture.Started)
			{
				try
				{
					_deviceCapture?.StopCapture();
				}
				catch (PcapException)     //此处throw一个异常，但不会造成影响，不必处理
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
			byte[] data = e.Packet.Data;       //Data转为string，需要替换-为空字符
			PublishHelper.PublishMsg("/GtsMsgParseService/WinPcap", data);
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

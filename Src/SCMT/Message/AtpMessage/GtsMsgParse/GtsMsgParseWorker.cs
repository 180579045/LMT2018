using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MsgQueue;

namespace AtpMessage.GtsMsgParse
{
	public class GtsMsgParseWorker
	{
		public void ParseIpFrameData(byte[] frameDataBytes)
		{
			IP_HEADER ipHeader = ProtocolParseHelper.GetIpHeader(frameDataBytes);
			if (null == ipHeader)
			{
				return;
			}

			byte protocolType = ipHeader.protocol;
			if (ProtocolType.UDP != (ProtocolType)protocolType)
			{
				return;
			}

			int offset = ipHeader.headerLen + Marshal.SizeOf<ETHERNET_HEADER>();
			if (0 == ipHeader._flag && 0 == ipHeader._offset)   //当分片标志为0,而偏移值也为0时，表示没有分片，不进行组包处理；
			{
				ParseUdpHeader(frameDataBytes, offset, ipHeader.src_addr, ipHeader.des_addr);
			}
			else
			{
				int dataLen = frameDataBytes.Length - offset;
				var ipData = new byte[dataLen];
				Buffer.BlockCopy(frameDataBytes, offset, ipData, 0, dataLen);
				AddNewIpPacket(ipHeader.id, ipHeader._offset, ipData);      //UDP报文太大，产生了分片，需要保存数据

				if (0 == ipHeader._flag)                     //最后一个分片，所有的数据组装成UDP数据报，再进行解析
				{
					var udpData = IpBurst(ipHeader.id);
					if (null == udpData)
					{
						throw new ApplicationException();
					}

					ParseUdpHeader(udpData, 0, ipHeader.src_addr, ipHeader.des_addr);   //TODO 这里需要判断是否是不同的ipHeader，源地址和目的地址可能不同
				}
			}
		}

		private void ParseUdpHeader(byte[] udpBytes, int offset, uint srcAddr, uint dstAddr)
		{
			UDP_HEADER udpHeader = ProtocolParseHelper.GetUdpHeader(udpBytes, offset);
			if (null == udpHeader)
			{
				throw new ArgumentException();
			}

			int udpDataLen = udpBytes.Length - offset - udpHeader.Len;
			UdpDataInfo udpData = new UdpDataInfo(udpDataLen)
			{
				SrcAddr = srcAddr,
				DstAddr = dstAddr,
				SrcPort = udpHeader.src_port,
				DstPort = udpHeader.des_port
			};

			Buffer.BlockCopy(udpBytes, offset + udpHeader.Len, udpData.UdpDataBytes, 0, udpDataLen);

			ParseUdpData(udpData);
		}

		private void ParseUdpData(UdpDataInfo data)
		{
			//TODO 需要确定是否要去掉其他的数据

			//处理完成后，通过接口发送给CDL
			PublishHelper.PublishMsg("/AtpBack/CDLParse/MsgParse", data.UdpDataBytes);
		}

		public void AddNewIpPacket(ushort ipId, ushort frameOffset, byte[] frameData)
		{
			if (mapIpFrames.ContainsKey(ipId))
			{
				var framesMap = mapIpFrames[ipId];
				if (framesMap.ContainsKey(frameOffset))
				{
					throw new ApplicationException("ip id is same and frame offset is same");
				}

				framesMap.Add(frameOffset, frameData);
			}
			else
			{
				var frameMap = new SortedDictionary<ushort, byte[]>();
				frameMap.Add(frameOffset, frameData);

				mapIpFrames.Add(ipId, frameMap);
			}
		}

		//IP分片组装成UDP数据
		public byte[] IpBurst(ushort ipId)
		{
			if (!mapIpFrames.ContainsKey(ipId))
			{
				//throw new ApplicationException("ip id is invalid");
				return null;
			}

			var mapFrames = mapIpFrames[ipId];
			byte[] temp = new byte[0xffff];		//最长65535字节
			int offset = 0;
			foreach (var itemMapFrame in mapFrames)
			{
				var value = itemMapFrame.Value;
				var len = value.Length;
				value.CopyTo(temp, offset);
				Buffer.BlockCopy(value, 0, temp, offset, len);
				offset += len;
			}

			mapFrames.Clear();
			mapIpFrames.Remove(ipId);		//组包完成后，清空数据

			byte[] udpData = new byte[offset];
			Buffer.BlockCopy(temp, 0, udpData, 0, offset);

			return udpData;
		}

		public void ParseUdpData(byte[] udpDataBytes)
		{
			//TODO 需要确定UDP的数据和IP数据的解析topic是否相同
		}

		//保存IP分片数据。key:IP id，value:分片数据.value key:offset id， value value:data
		//IP分片特点：如果IP数据包产生了分片，那么这个IP包的所有分片的IP id是相同的。分片带有offset，但不能保证哪个分片先到达，因此用SortedDictionary保存
		private Dictionary< ushort, SortedDictionary<ushort, byte[]> > mapIpFrames = new Dictionary<ushort, SortedDictionary<ushort, byte[]>>();
	}

	internal class UdpDataInfo
	{
		public byte[] UdpDataBytes;     //已经去掉了所有的协议头
		public uint SrcAddr;
		public uint DstAddr;
		public ushort SrcPort;
		public ushort DstPort;

		public UdpDataInfo(int len)
		{
			UdpDataBytes = new byte[len];
		}
	}
}

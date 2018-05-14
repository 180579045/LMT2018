using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommonUility;

//IP报文头，UDP报文头的定义和解析
namespace AtpMessage.GtsMsgParse
{
	//下面的定义参见RFC 1700
	public enum ProtocolType
	{
		IP = 0x0800,
		ARP = 0x0806,
		RARP = 0x0835,
		Appletalk = 0x809B,
		SNMP = 0x814C,
		Novell0 = 0x8137,
		Novell1 = 0x8138,
		ICMP = 1,           //控制信息协议
		TCP = 6,            //传输控制协议
		EGP = 8,            //外部网关协议
		IGP = 9,            //内部网关协议
		UDP = 17            //用户数据报协议
	}


	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public class ETHERNET_HEADER : IASerialize
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public byte[] des_mac;    //接收端的MAC地址

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public byte[] src_mac;    //发送端的MAC地址

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] type;       //类型字段

		public ETHERNET_HEADER()
		{
			des_mac = new byte[6];
			src_mac = new byte[6];
			type = new byte[2];
		}

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			throw new NotImplementedException();
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			if (bytes.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += SerializeHelper.SerializeBytes(ref des_mac, 0, bytes, offset + used, des_mac.Length);
			used += SerializeHelper.SerializeBytes(ref src_mac, 0, bytes, offset + used, src_mac.Length);
			used += SerializeHelper.SerializeBytes(ref type, 0, bytes, offset + used, type.Length);

			return used;
		}

		public int Len => ContentLen;
		public ushort ContentLen => (ushort) (des_mac.Length + src_mac.Length + type.Length);
	};

	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public class IP_HEADER : IASerialize
	{
		public byte ver_len;           //版本4位,头长度4位,报头长度以32位为一个单位
		public byte type;              //服务类型8位
		public ushort length;          //总长度,16位,指出报文的以字节为单位的总长度，报文长度不能超过65536个字接，否则认为报文遭到破坏
		public ushort id;              //报文标示,用于多于一个报文
		public ushort flag_offset;     //标志,3位	数据块偏移13位
		public byte time;              //生存时间,8位
		public byte protocol;          //协议,8位
		public ushort crc_val;         //头校验和，16位
		public uint src_addr;          //源地址,32位
		public uint des_addr;          //目标地址,32位

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] options;			//选项和填充,32位

		//下面的是为了方便添加的，不是协议中的内容
		public byte headerLen;
		public byte _flag;
		public byte _offset;

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			throw new NotImplementedException();
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			if (bytes.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref ver_len);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref type);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref length);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref id);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref flag_offset);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref time);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref protocol);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref crc_val);
			used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref src_addr);
			used += SerializeHelper.DeserializeInt32(bytes, offset + used, ref des_addr);

			headerLen = (byte)(ver_len & 0x0F);
			if (used < headerLen)      //条件成立：有可选项
			{
				options = new byte[32];
				used += SerializeHelper.SerializeBytes(ref options, 0, bytes, offset + used, options.Length);
			}

			_flag = (byte)(flag_offset >> 13);          //3bit：分片标志。标识是否IP分片.第一位无用，第二位0：允许分片，1：不允许。第三位0：最后一片，1：后面还有分片
			_offset = (byte)(flag_offset & 0x1FFF);     //13bit：片偏移

			return used;
		}

		public int Len => ContentLen;

		public ushort ContentLen => (ushort)(sizeof(byte) * 20);    //TODO 选项不一定有
	};

	public class UDP_HEADER : IASerialize
	{
		public ushort src_port;   //发送端端口
		public ushort des_port;   //接收端端口
		public ushort length;     //用户数据报长度
		public ushort check_sum;  //校验码

		public int SerializeToBytes(ref byte[] ret, int offset)
		{
			throw new NotImplementedException();
		}

		public int DeserializeToStruct(byte[] bytes, int offset)
		{
			if (bytes.Length - offset < Len)
			{
				return -1;
			}

			int used = 0;
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref src_port);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref des_port);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref length);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref check_sum);

			return used;
		}

		public int Len => ContentLen;
		public ushort ContentLen => sizeof(ushort) * 4;
	};

	public class ProtocolParseHelper
	{
		public static IP_HEADER GetIpHeader(byte[] frameData)
		{
			IP_HEADER ipHeader = new IP_HEADER();
			if (-1 == ipHeader.DeserializeToStruct(frameData, Marshal.SizeOf<ETHERNET_HEADER>()))
				return null;

			return ipHeader;
		}

		public static UDP_HEADER GetUdpHeader(byte[] frameData, int offset)
		{
			UDP_HEADER udpHeader = new UDP_HEADER();
			if (-1 == udpHeader.DeserializeToStruct(frameData, offset))
			{
				return null;
			}

			return udpHeader;
		}
	}

	#region
	//public struct TCP_HEADER
	//{
	//    byte src_port[2];       //发送端端口号,16位
	//    byte des_port[2];       //接收端端口号,16位
	//    byte sequence_no[4];    //32位，标示消息端的数据位于全体数据块的某一字节的数字
	//    byte ack_no[4];         //32位，确认号,标示接收端对于发送端接收到数据块数值
	//    byte offset_reser_con[2];//数据偏移4位，预留6位，控制位6为
	//    byte window[2];         //窗口16位
	//    byte checksum[2];       //校验码,16位
	//    byte urgen_pointer[2];  //16位，紧急数据指针
	//    byte options[4];        //选祥和填充,32位
	//};

	//public struct ICMP_HEADER
	//{
	//    byte type;          //类型字节(1字节)
	//    byte code;          //代码字节(1字节)
	//    byte check_sum[2];  //校验码(2字节)
	//};

	//public struct ARP_PROTOCOL
	//{
	//    byte hw_type[2];    //硬件类型。以态网表示为1
	//    byte protocol[2];   //网络层协议的类型，例如IP协议为800
	//    byte hw_len;        //查询物理地址的字节长度。以态网时为6
	//    byte protocol_len;  //查询上层协议地址的字节长度。IPv4时为4
	//    byte opcode[2];     //表示操作内容的数值。1为ARP请求，2为ARP响应，3为RARP请求，4为RARP响应
	//    byte src_mac[6];    //发送端MAC地址
	//    byte src_addr[4];   //发送端的IP地址
	//    byte des_mac[6];    //查询对象的MAC地址
	//    byte des_addr[4];   //查询对象的IP地址
	//};
	#endregion
}

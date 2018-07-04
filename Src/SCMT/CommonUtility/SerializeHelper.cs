using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CommonUtility
{
	/// <summary>
	/// 序列化和反序列化类
	/// </summary>
	public static class SerializeHelper
	{
		/// <summary>
		/// 字节数组转换为结构体对象
		/// </summary>
		/// <param name="bytes">从网络上收到的数据包</param>
		/// <param name="structType">结构体类型</param>
		/// <returns></returns>
		public static object BytesToStruct(byte[] bytes, Type structType)
		{
			int size = Marshal.SizeOf(structType);

			if (bytes.Length < size)
			{
				return null;
			}

			IntPtr buffer = Marshal.AllocHGlobal(size);
			try
			{
				Marshal.Copy(bytes, 0, buffer, size);
				return Marshal.PtrToStructure(buffer, structType);
			}
			catch
			{
				return null;
			}
			finally
			{
				Marshal.FreeHGlobal(buffer);
			}
		}

		public static T BytesToStruct<T>(byte[] bytes) where T : IASerialize, new ()
		{
			//int size = Marshal.SizeOf(structType);
			var obj = new T() as IASerialize;
			int size = obj.Len;

			if (bytes.Length < size)
			{
				return default(T);
			}

			IntPtr buffer = Marshal.AllocHGlobal(size);
			try
			{
				var structType = typeof(T);
				Marshal.Copy(bytes, 0, buffer, size);
				return (T)Marshal.PtrToStructure(buffer, typeof(T));
			}
			catch
			{
				return default(T);
			}
			finally
			{
				Marshal.FreeHGlobal(buffer);
			}
		}

		//// <summary>
		/// 结构体转byte数组
		/// 这个方法有BUG。对于嵌套的结构体变量数组处理错误，结果byte[]长度不正确
		/// </summary>
		/// <param name="obj">要转换的结构体</param>
		/// <returns>转换后的byte数组</returns>
		//public static byte[] StructToBytes(object obj)
		//{
		//	//得到结构体的大小
		//	int size = Marshal.SizeOf(obj);
		//	//创建byte数组
		//	byte[] bytes = new byte[size];
		//	//分配结构体大小的内存空间
		//	IntPtr structPtr = Marshal.AllocHGlobal(size);
		//	//将结构体拷到分配好的内存空间
		//	Marshal.StructureToPtr(obj, structPtr, false);
		//	//从内存空间拷到byte数组
		//	Marshal.Copy(structPtr, bytes, 0, size);
		//	//释放内存空间
		//	Marshal.FreeHGlobal(structPtr);
		//	//返回byte数组
		//	return bytes;
		//}

		/// <summary>
		/// 序列化一个byte数据到字节数组中
		/// </summary>
		/// <param name="ret">保存序列化后的数据</param>
		/// <param name="offset">ret从哪里开始存</param>
		/// <param name="value">待序列化的数据</param>
		/// <returns>此次序列化了几个字节</returns>
		public static int SerializeByte(ref byte[] ret, int offset, byte value)
		{
			if (ret.Length <= offset)
			{
				return -1;
			}

			ret[offset] = value;
			return sizeof(byte);
		}

		/// <summary>
		/// 从字节数组中取出一个数据
		/// </summary>
		/// <param name="ret">原始数据</param>
		/// <param name="offset">字节数组偏移量，也就是从偏移量位置开始取数据</param>
		/// <param name="value">保存反序列化后的数据</param>
		/// <returns></returns>
		public static int DeserializeByte(byte[] ret, int offset, ref byte value)
		{
			if (ret.Length <= offset)
			{
				return -1;
			}

			value = ret[offset];
			return sizeof(byte);
		}

		/// <summary>
		/// 把一个字节数组中的所有数据copy到另外一个数组中
		/// 这个函数序列化和反序列化都会用到，只不过是src与dst交换了位置
		/// 把src中从src_offset开始的数据copy len个字节到dst中
		/// </summary>
		/// <param name="dst">保存序列化后的数据</param>
		/// <param name="offset">字节数组偏移量，也就是从偏移量位置开始存数据</param>
		/// <param name="src">待序列化的字节数组</param>
		/// <param name="src_offset">字节数组偏移量，也就是从偏移量位置开始取数据</param>
		/// <param name="len">要序列化的数据长度，也就是从src中copy多少个字节到dst中</param>
		/// <returns>copy的长度</returns>
		public static int SerializeBytes(ref byte[] dst, int offset, byte[] src, int src_offset, int len)
		{
			if(src == null)
			{
				return -1;
			}
			if ((src.Length - src_offset < len) || (dst.Length - offset < len))
			{
				return -1;
			}

			Buffer.BlockCopy(src, src_offset, dst, offset, len);
			return len;
		}

		/// <summary>
		/// 序列化双字节数组
		/// </summary>
		/// <param name="dst">保存序列化后的数据</param>
		/// <param name="offset">字节数组偏移量，也就是从偏移量位置开始存数据</param>
		/// <param name="src">待序列化的双字节数组</param>
		/// <param name="src_offset">双字节数组偏移量，也就是从偏移量位置开始取数据</param>
		/// <returns>copy的长度</returns>
		public static int SerializeUintArray(ref byte[] dst, int offset, uint[] src, int src_offset, int len)
		{
			int sizeCopy = len * sizeof(uint);

			if ((src.Length - src_offset < len) || (dst.Length - offset < sizeCopy))
			{
				return -1;
			}

			byte[] buf = new byte[sizeCopy];
			int used = 0;

			for (int i = src_offset; i < src.Length; i++)
			{
				used += SerializeInt32(ref buf, used, src[i]);
			}

			Buffer.BlockCopy(buf, 0, dst, offset, used);

			return used;
		}

		/// <summary>
		/// 翻转byte数组
		/// </summary>
		/// <param name="bytes">待处理的数组</param>
		public static void ReverseBytes(byte[] bytes)
		{
			var len = bytes.Length;

			for (var i = 0; i < len / 2; i++)
			{
				var tmp = bytes[len - 1 - i];
				bytes[len - 1 - i] = bytes[i];
				bytes[i] = tmp;
			}
		}
		
		/// <summary>
		/// 翻转byte数组，规定了起始位置和长度
		/// </summary>
		/// <param name="bytes">待处理的数组</param>
		public static void ReverseBytes(byte[] bytes, int start, int len)
		{
			int end = start + len - 1;
			byte tmp;
			int i = 0;
			for (int index = start; index < start + len / 2; index++, i++)
			{
				tmp = bytes[end - i];
				bytes[end - i] = bytes[index];
				bytes[index] = tmp;
			}
		}

		/// <summary>
		/// 双字节数据反转字节序
		/// </summary>
		/// <param name="value">原始数据</param>
		/// <returns>翻转后的数据</returns>
		public static ushort ReverseUshort(ushort value)
		{
			return (ushort) ((value & 0x00FFU) << 8 | (value & 0xFF00U) >> 8);
		}

		public static short ReverseShort(short value)
		{
			return (short)((value & 0x00FFU) << 8 | (value & 0xFF00U) >> 8);
		}

		/// <summary>
		/// 双字节数据序列化
		/// </summary>
		/// <param name="ret">保存序列化后的数据</param>
		/// <param name="offset">字节数组偏移量，也就是从偏移量位置开始存数据</param>
		/// <param name="value">原始数据</param>
		/// <returns>序列化数据的字节数</returns>
		public static int SerializeUshort(ref byte[] ret, int offset, ushort value)
		{
			if (ret.Length - offset < sizeof(ushort))
			{
				return -1;
			}

			var resultBuf = BitConverter.GetBytes(ReverseUshort(value));
			Buffer.BlockCopy(resultBuf, 0, ret, offset, resultBuf.Length);
			return resultBuf.Length;
		}

		/// <summary>
		/// 双字节数据反序列化
		/// </summary>
		/// <param name="ret">原始数据</param>
		/// <param name="offset">字节数组偏移量，也就是从偏移量位置开始取数据</param>
		/// <param name="value">保存转后的数据</param>
		/// <returns>序列化数据的字节数</returns>
		public static int DeserializeUshort(byte[] ret, int offset, ref ushort value)
		{
			if (ret.Length - offset < sizeof(ushort))
			{
				return -1;
			}

			var data = BitConverter.ToUInt16(ret, offset);
			value = ReverseUshort(data);
			return sizeof(ushort);
		}
		
		/// <summary>
		/// 四字节数据反转字节序
		/// </summary>
		/// <param name="value">原始数据</param>
		/// <returns>翻转后的数据</returns>
		public static uint ReverseFourBytesData(uint value)
		{
			return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
				   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
		}

		public static int ReverseFourBytesData(int value)
		{
			return (int)((value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
				   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24);
		}

		/// <summary>
		/// 四字节数据序列化
		/// </summary>
		/// <param name="ret">保存序列化后的数据</param>
		/// <param name="offset">字节数组偏移量，也就是从偏移量位置开始存数据</param>
		/// <param name="value">原始数据</param>
		/// <returns>序列化数据的字节数</returns>
		public static int SerializeInt32(ref byte[] ret, int offset, uint value)
		{
			if (ret.Length - offset < sizeof(uint))
			{
				return -1;
			}

			var resultBuf = BitConverter.GetBytes(ReverseFourBytesData(value));
			Buffer.BlockCopy(resultBuf, 0, ret, offset, resultBuf.Length);
			return sizeof(int);
		}

		/// <summary>
		/// 四字节数据反序列化
		/// </summary>
		/// <param name="ret">原始数据</param>
		/// <param name="offset">字节数组偏移量，也就是从偏移量位置开始取数据</param>
		/// <param name="value">保存转后的数据</param>
		/// <returns>序列化数据的字节数</returns>
		public static int DeserializeInt32(byte[] ret, int offset, ref uint value)
		{
			if (ret.Length - offset < sizeof(uint))
			{
				return -1;
			}

			var data = BitConverter.ToUInt32(ret, offset);
			value = ReverseFourBytesData(data);
			return sizeof(uint);
		}
		
		/// <summary>
		/// 八字节数据反转字节序
		/// </summary>
		/// <param name="value">原始数据</param>
		/// <returns>翻转后的数据</returns>
		public static ulong ReverseEightBytesData(ulong value)
		{
			return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
				   (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
				   (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
				   (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
		}

		/// <summary>
		/// 把给定的结构体序列化为字节数组，然后通过网络发送
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static byte[] SerializeStructToBytes<T>(T obj)
			where T : IASerialize
		{
			if (null == obj)
			{
				return null;
			}

			var sendBytes = new byte[obj.Len];

			if (-1 == obj.SerializeToBytes(ref sendBytes, 0))
			{
				throw new InternalBufferOverflowException();
			}

			return sendBytes;
		}

		//这种方法导致byte数组最少256个字节，不符合要求
		//public static bool SerializeStructToString(object obj, out byte[] resultBytes)
		//{
		//	bool succeed = false;
		//	resultBytes = null;
		//	try
		//	{
		//		MemoryStream momeryStream = new MemoryStream();
		//		BinaryFormatter binaryFormatter = new BinaryFormatter();
		//		binaryFormatter.Serialize(momeryStream, obj);
		//		resultBytes = momeryStream.ToArray();
		//		succeed = true;
		//	}
		//	catch
		//	{
		//		succeed = false;
		//	}

		//	return succeed;
		//}
	}



	#region

	///*消息模拟终止请求*/
	//struct Msg_gtsmgtsa_simulate_stop_req
	//{
	//	GTS_MsgHeader   header;                         /*GTS消息头*/
	//	byte     u8Reserved;
	//	byte     u8Padding[3];
	//};

	///*消息模拟终止响应*/
	//struct Msg_gtsmgtsa_simulate_stop_rsp
	//{
	//	GTS_MsgHeader   header;                         /*GTS消息头*/
	//	byte     u8Complete;
	//	byte     u8Padding[3];
	//};

	///*消息模拟完成指示*/
	//struct Msg_gtsmgtsa_simulate_complete_ind
	//{
	//	GTS_MsgHeader   header;                         /*GTS消息头*/
	//	byte     u8Reserved;
	//	byte     u8Padding[3];
	//};

	///*DTMUC00099515 远程登录情况下，ATP关闭跟踪，基站同步停止写29、30远程日志 start*/
	///*控制远程日志记录消息*/
	///*写日志控制请求消息*/
	//struct Msg_gtsmgtsa_writelog_ctrl_req
	//{
	//	GTS_MsgHeader   header;                         /*GTS消息头*/
	//	byte             u8Option;                       /*控制选项，0:停止写日志；1:开启写日志*/
	//	byte             u8Pad[3];                       /*保留*/
	//};

	///*写日志控制响应消息*/
	//struct Msg_gtsagtsm_writelog_ctrl_rsp
	//{
	//	GTS_MsgHeader   header;                         /*GTS消息头*/
	//	byte             u8Complete;                     /*0:成功;1:失败*/
	//	public fix byte[3]             u8Pad[3];                       /*保留*/
	//};
	///*DTMUC00099515 远程登录情况下，ATP关闭跟踪，基站同步停止写29、30远程日志 end*/

	///*模拟文件结构*/
	///*文件头*/
	//struct STRU_FileHeader
	//{
	//	byte     u8FileType ;                            /*文件类型*/
	//	byte     u8Number ;                              /*消息个数*/
	//	byte     u8Padding[2];
	//};

	///*模拟消息消息头结构*/
	//struct STRU_SimuMsg_RuleHeader
	//{
	//	uint    DelayType ;                             /*表示消息发送延迟的类型，
	//															0表示不延迟，
	//															1表示到指定的SFN发送，
	//															2表示延迟指定的时间（毫秒）发送。*/
	//	uint    u32Delay ;                              /*消息发送的延迟时间，
	//															对于DelayType ==1,其值为SFN点值，
	//															对于DelayType ==2，单位10ms*/
	//	uint    u32Repeat ;                             /*消息重复发送次数*/
	//	uint    u32RepeatPeriod ;                       /*重复发送周期，以SFN为单位*/
	//	public UInt16    u16InterfaceID ;                        /*模拟消息流的ID，如果是物理层的模拟消息，
	//															则为消息流的ID，如果为高层的模拟消息，
	//															则为特定的标志*/
	//	ushort    u16DestID;                              /*模拟消息的目标地址，当为高层的模拟消息时，
	//															设置u16DestID 为特定值*/
	//	ushort    u16SourceID ;                           /*模拟消息的源地址，当为高层的模拟消息时，
	//															设置u16SourceID为空*/
	//	ushort    u16DataLength ;                         /*模拟消息的数据长度，即u8MsgData的实际长度*/
	//	uint    u32Opcode;                              /*物理层消息为操作码，高层消息为原语ID*/
	//	uint    u32SapID;                               /*物理层消息为一特殊值，高层消息为SAP_ID*/
	//};

	///*模拟消息消息体结构*/
	//struct STRU_SimuMsg_Rule
	//{
	//	STRU_SimuMsg_RuleHeader   SimMsgHeaderStru;     /*模拟消息头*/
	//	byte     u8MsgData[MAX_SIMU_MSG_LENGTH];         /*真正的模拟消息字节流*/
	//};

	///*响应规则文件结构*/
	///*响应规则结构*/
	//struct STRU_RespRule
	//{
	//	uint    u32Opcode;                              /*需要响应的OPCODE*/
	//	uint    u32RespNum;                             /*响应消息个数*/
	//	ushort    u16RespType;                            /*响应类型*/
	//	int         s8FileName[MAX_FILE_PATH_NAME_LENGTH];	 
	//};


	///************************************
	//消息宏：O_GTSMGTSA_SET_FILEPATH_IND
	//结构名：Msg_gtsmgtsa_Set_FilePath_Ind
	//源SFU：
	//目的SFU：
	//描述：ATP发给GTSA的文件上传路径设置消息
	//**************************************/
	//struct Msg_gtsmgtsa_Set_FilePath_Ind
	//{
	//	GTS_MsgHeader    struheader;                        /*GTSA头*/
	//	int              s8FilePath[SI_FILEPATH_MAX_LEN];   /*文件上传路径*/
	//	uint             u32Pad;
	//};

	#endregion

}

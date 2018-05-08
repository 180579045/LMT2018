using System;

namespace AtpMessage.MsgDefine
{
	public class GetHeaderFromBytes
	{
		public static GtsMsgHeader GetHeader(byte[] bytes)
		{
			GtsMsgHeader header = new GtsMsgHeader();
		    if (-1 == header.DeserializeToStruct(bytes, 0))
		        return null;
			return header;
		}
	}
}

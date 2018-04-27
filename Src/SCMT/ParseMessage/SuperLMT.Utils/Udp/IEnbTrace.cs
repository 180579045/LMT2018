using System;

namespace SuperLMT.Utils.Udp
{
    public interface IEnbTrace
    {
        //消息序号
        UInt32 GetSeqNum();

        //消息名称
        UInt32 GetU32TrcMsgType();

        //消息接口类型
        UInt32 GetU32TrcMsgInterfaceType();

        //板类型
        Byte GetU8BoardType();

        //SCTPIndex
        UInt16 GetU16TrcMsgSctpIndex();

        //U8TrcMsgCellId
        Byte GetU8TrcMsgCellIdx();

        //消息方向
        Byte GetU8TrcMsgDirect();

        //消息接收时间
        StrucDateTime GetStruDateTime();

        //获取消息长度
        UInt32 GetU32TrcMsgLen();

        //获取消息
        Byte[] GetMsgBuf();
    }


    public class StrucDateTime
    {
        public ushort Year { get; set; }

        public byte Month { get; set; }

        public byte Day { get; set; }

        public byte Hout { get; set; }

        public byte Minute { get; set; }

        public byte Second { get; set; }

        public uint U32Millisecond { get; set; }
    }
}

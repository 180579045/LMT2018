using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;

namespace SCMTOperationCore.Message.SI
{
	/*************************************************************************************************    
	消息宏:  O_SILMTENB_NBPHASE_REP
	结构名: SI_NBPHASE_REP_MSG
	源SFU：  LMT-B的SFU
	目标SFU：CCU板上SCP处理器SI的LMT-B接入SFU
	描述:  NODEB所处阶段上报消息
	**************************************************************************************************/
	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
	public class SI_NBPHASE_REP_MSG : IASerialize
	{
		public SiMsgHead head;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public byte[] u48NodeBEquID;
		//public SIu48 u48NodeBEquID;						/*NodeB设备标识号*/
		public ushort u16NodeBPhase;					/*NODE B所处阶段。0x01：系统初始化LMT-B接入超时阶段；0x02：系统初始化已完成阶段；0x03：已有连接，忙。0x04：备SCM板禁止接入；0x05：系统初始化 LMT-B接入超时前阶段。*/
		public byte u8NodeBType;						/*基站类型: 0宏 1微 2超*/
		public byte u8NodeBHwVer;						/*基站硬件版本*/
		public SIDOS_DATE_TIME struNodeBSystemTime;     /* NodeB系统时间 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
		public byte[] u8VxWorksCoreVer;					/* 本板Vxworks核心版本(字符串以“\0结尾”)*/
		public ushort pad;

		public SI_NBPHASE_REP_MSG()
		{
			head = new SiMsgHead();
			u48NodeBEquID = new byte[6];
			struNodeBSystemTime = new SIDOS_DATE_TIME();
			u8VxWorksCoreVer = new byte[40];
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
			used += head.DeserializeToStruct(bytes, offset + used);
			used += SerializeHelper.SerializeBytes(ref u48NodeBEquID, 0, bytes, offset + used, 6);
			used += SerializeHelper.DeserializeUshort(bytes, offset + used, ref u16NodeBPhase);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8NodeBType);
			used += SerializeHelper.DeserializeByte(bytes, offset + used, ref u8NodeBHwVer);
			used += struNodeBSystemTime.DeserializeToStruct(bytes, offset + used);
			used += SerializeHelper.SerializeBytes(ref u8VxWorksCoreVer, 0, bytes, offset + used, 40);
			used += 2;
			return used;
		}

		public int Len => head.Len + ContentLen;

		public ushort ContentLen => (ushort)(6 + sizeof(ushort) * 2 + sizeof(byte) * 42 + struNodeBSystemTime.Len);
	}

	public enum ENODEB_PHASE
	{
		ENODEBPHASE_UNKNOWN = -1,
		ENODEBPHASE_SI_OVERTIME = 1,         /*系统初始化,-B接入超时阶段(已过15秒)*/
		ENODEBPHASE_SI_INTIME = 5,           /*系统初始化，-B接入超时前阶段(15秒之内)*/
		ENODEBPHASE_SI_FINISH = 2,           /*系统初始化完成阶段*/
		ENODEBPHASE_EXIST_CONNECT = 3,       /*已经有LMT-eNB接入*/
		ENODEBPHASE_LOGIN_BACKBOARD = 4,     /*错误的登录到备板*/
	};
}

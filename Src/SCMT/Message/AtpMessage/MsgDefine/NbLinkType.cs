namespace AtpMessage.MsgDefine
{
	public class NbLinkType
	{
		/*双向DSAP基址*/
		public const ushort NB_DSAP_BIDIRECTION_BASE = 0;
		/*单向DSAP基址*/
		public const ushort NB_DSAP_UNILATERAL_BASE = 100;
		/*主备通信DSAP*/
		public const ushort NB_DSAP_OSP_BACKUP_P2P = 200;                                    /*用于CCU板主备通信*/

		/*PSA DSAP*/
		/*按照原有LinkId编号方案，DSP内部通信使用的LinkId宏定义为NB_DSAP_OSP_P2P，数值为0，*/
		/*而去往RNC的LinkId为0～360,即当LinkId为0时，存在冲突，DSP侧的PSA驱动无法识别该链路是去往DSP的还是去往RNC需要去掉OSP消息头的。*/
		/*DSP侧内部通信链路号需要更改，大于360*/
		//#ifdef OSP_SUPPORT_PHAROS
		//public const ushort NB_DSAP_OSP_P2P = 500;          /*带OSP头的点对点通信DSAP*/
		//#else
		//public const ushort NB_DSAP_OSP_P2P = (NB_DSAP_BIDIRECTION_BASE+0);          /*带OSP头的点对点通信DSAP*/
		//#endif

		public const ushort NB_DSAP_NO_OSP_TNP_RIP0_SAAL0 = (NB_DSAP_BIDIRECTION_BASE+10);         /*不带OSP头的DSAP,用于TNP和槽位0的RIP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_TNP_RIP0_SAAL1 = (NB_DSAP_BIDIRECTION_BASE+11);         /*不带OSP头的DSAP,用于TNP和槽位0的RIP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_TNP_RIP0_SAAL2 = (NB_DSAP_BIDIRECTION_BASE+12);         /*不带OSP头的DSAP,用于TNP和槽位0的RIP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_TNP_RIP0_SAAL3 = (NB_DSAP_BIDIRECTION_BASE+13);         /*不带OSP头的DSAP,用于TNP和槽位0的RIP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_TNP_RIP1_SAAL0 = (NB_DSAP_BIDIRECTION_BASE+14);         /*不带OSP头的DSAP,用于TNP和槽位1的RIP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_TNP_RIP1_SAAL1 = (NB_DSAP_BIDIRECTION_BASE+15);         /*不带OSP头的DSAP,用于TNP和槽位1的RIP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_TNP_RIP1_SAAL2 = (NB_DSAP_BIDIRECTION_BASE+16);         /*不带OSP头的DSAP,用于TNP和槽位1的RIP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_TNP_RIP1_SAAL3 = (NB_DSAP_BIDIRECTION_BASE+17);         /*不带OSP头的DSAP,用于TNP和槽位1的RIP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_RIP0_IPOA = (NB_DSAP_BIDIRECTION_BASE+18);         /*不带OSP头的DSAP,用于SCP和槽位0的RIP的IPOA缺省路径*/
		public const ushort NB_DSAP_NO_OSP_SCP_RIP1_IPOA = (NB_DSAP_BIDIRECTION_BASE+19);         /*不带OSP头的DSAP,用于SCP和槽位1的RIP的IPOA缺省路径*/
		public const ushort NB_DSAP_NO_OSP_IFP_BCP_BROADCAST = (NB_DSAP_BIDIRECTION_BASE+20);         /*不带OSP头的DSAP,用于IFP和BCP的广播通信*/
		public const ushort NB_DSAP_NO_OSP_IFP0_BCP_P2P = (NB_DSAP_BIDIRECTION_BASE+21);         /*不带OSP头的DSAP,用于RRS0和BBU的点对点通信，动态创建*/
		public const ushort NB_DSAP_NO_OSP_IFP1_BCP_P2P = (NB_DSAP_BIDIRECTION_BASE+22);         /*不带OSP头的DSAP,用于RRS1和BBU的点对点通信，动态创建*/
		public const ushort NB_DSAP_NO_OSP_IFP_LMTRRS_BROADCAST = (NB_DSAP_BIDIRECTION_BASE+23);         /*不带OSP头的DSAP,用于RRS和LMT-RRS的广播通信*/
		public const ushort NB_DSAP_NO_OSP_IFP_LMTRRS_P2P = (NB_DSAP_BIDIRECTION_BASE+24);         /*不带OSP头的DSAP,用于RRS和LMT-RRS的点对点通信，动态创建*/
		public const ushort NB_DSAP_NO_OSP_IFP_BCP_P2P = (NB_DSAP_BIDIRECTION_BASE+25);         /*不带OSP头的DSAP,用于RRS和BBU的点对点通信，动态创建*/

		/* 36A链路规划 */ 
		/* 基站内部带OSP头的P2P链路，复用144A的宏定义NB_DSAP_OSP_P2P */
		/* BCP-DSP之间的链路，由DSI改为PSA，复用144A的宏定义NB_DSAP_OSP_P2P */
		/* CCU板主备通信，复用144A的宏定义NB_DSAP_OSP_BACKUP_P2P */
		/* 无OSP头的链路，SCP-NP, IPOA, IFP-EIP，DSP-PC定义如下*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP0_SAAL0 = (NB_DSAP_BIDIRECTION_BASE+10);         /*不带OSP头的DSAP,用于SCP和槽位0的NP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP0_SAAL1 = (NB_DSAP_BIDIRECTION_BASE+11);         /*不带OSP头的DSAP,用于SCP和槽位0的NP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP0_SAAL2 = (NB_DSAP_BIDIRECTION_BASE+12);         /*不带OSP头的DSAP,用于SCP和槽位0的NP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP0_SAAL3 = (NB_DSAP_BIDIRECTION_BASE+13);         /*不带OSP头的DSAP,用于SCP和槽位0的NP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP1_SAAL0 = (NB_DSAP_BIDIRECTION_BASE+14);         /*不带OSP头的DSAP,用于SCP和槽位1的NP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP1_SAAL1 = (NB_DSAP_BIDIRECTION_BASE+15);         /*不带OSP头的DSAP,用于SCP和槽位1的NP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP1_SAAL2 = (NB_DSAP_BIDIRECTION_BASE+16);         /*不带OSP头的DSAP,用于SCP和槽位1的NP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP1_SAAL3 = (NB_DSAP_BIDIRECTION_BASE+17);         /*不带OSP头的DSAP,用于SCP和槽位1的NP的SAAL通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP0_IPOA = (NB_DSAP_BIDIRECTION_BASE+18);         /*不带OSP头的DSAP,用于SCP和槽位0的NP的IPOA缺省路径*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP1_IPOA = (NB_DSAP_BIDIRECTION_BASE+19);         /*不带OSP头的DSAP,用于SCP和槽位1的NP的IPOA缺省路径*/
		public const ushort NB_DSAP_NO_OSP_IFP_EIP_BROADCAST = (NB_DSAP_BIDIRECTION_BASE+20);         /*不带OSP头的DSAP,用于IFP和BCP的广播通信*/
		public const ushort NB_DSAP_NO_OSP_IFP_EIP_P2P = (NB_DSAP_BIDIRECTION_BASE+21);         /*不带OSP头的DSAP,用于RRS和EIU的点对点通信，动态创建*/

		public const ushort NB_DSAP_NO_OSP_SCP_NP0_SCTP0 = (NB_DSAP_BIDIRECTION_BASE+40);         /*不带OSP头的DSAP,用于SCP和槽位0的NP的SCTP通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP0_SCTP1 = (NB_DSAP_BIDIRECTION_BASE+41);         /*不带OSP头的DSAP,用于SCP和槽位0的NP的SCTP通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP0_SCTP2 = (NB_DSAP_BIDIRECTION_BASE+42);         /*不带OSP头的DSAP,用于SCP和槽位0的NP的SCTP通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP0_SCTP3 = (NB_DSAP_BIDIRECTION_BASE+43);         /*不带OSP头的DSAP,用于SCP和槽位0的NP的SCTP通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP0_SCTP4 = (NB_DSAP_BIDIRECTION_BASE+44);         /*不带OSP头的DSAP,用于SCP和槽位0的NP的SCTP通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP0_SCTP5 = (NB_DSAP_BIDIRECTION_BASE+45);         /*不带OSP头的DSAP,用于SCP和槽位0的NP的SCTP通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP1_SCTP0 = (NB_DSAP_BIDIRECTION_BASE+46);         /*不带OSP头的DSAP,用于SCP和槽位1的NP的SCTP通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP1_SCTP1 = (NB_DSAP_BIDIRECTION_BASE+47);         /*不带OSP头的DSAP,用于SCP和槽位1的NP的SCTP通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP1_SCTP2 = (NB_DSAP_BIDIRECTION_BASE+48);         /*不带OSP头的DSAP,用于SCP和槽位1的NP的SCTP通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP1_SCTP3 = (NB_DSAP_BIDIRECTION_BASE+49);         /*不带OSP头的DSAP,用于SCP和槽位1的NP的SCTP通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP1_SCTP4 = (NB_DSAP_BIDIRECTION_BASE+50);         /*不带OSP头的DSAP,用于SCP和槽位1的NP的SCTP通信*/
		public const ushort NB_DSAP_NO_OSP_SCP_NP1_SCTP5 = (NB_DSAP_BIDIRECTION_BASE+51);         /*不带OSP头的DSAP,用于SCP和槽位1的NP的SCTP通信*/

		public const ushort NB_DSAP_NO_OSP_EIP_NP_ATMARP = (NB_DSAP_BIDIRECTION_BASE+60);         /*EIP与NP之间发送inAtmArp应答报文链路号*/

		public const ushort NB_DSAP_NO_OSP_DSP_PC_GTSA = (NB_DSAP_BIDIRECTION_BASE+65);         /*DSP与PC之间发送GTS抄送消息报文链路号*/


		/*DSI LINK ID*/
		/*DSP to BCP NB_DSAP*/
		public const ushort NB_DSAP_OSP_DSP_BCP = (NB_DSAP_UNILATERAL_BASE+0);           /*有OS的DSP core向BCP发送消息*/
		public const ushort NB_DSAP_NO_OSP_DSP_BCP = (NB_DSAP_UNILATERAL_BASE+1);           /*无OS的DSP core向BCP发送消息*/
		/*BCP to DSP NB_DSAP*/
		public const ushort NB_DSAP_OSP_BCP_DSP_FPP_C0 = (NB_DSAP_UNILATERAL_BASE+11);          /*BCP向FPP CORE0 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_FPP_C1 = (NB_DSAP_UNILATERAL_BASE+12);          /*BCP向FPP CORE1 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_FPP_C2 = (NB_DSAP_UNILATERAL_BASE+13);          /*BCP向FPP CORE2 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_FPP_C3 = (NB_DSAP_UNILATERAL_BASE+14);          /*BCP向FPP CORE3 发送消息*/

		public const ushort NB_DSAP_OSP_BCP_DSP_CCP0_C0 = (NB_DSAP_UNILATERAL_BASE+15);          /*BCP向CCP0 CORE0 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_CCP0_C1 = (NB_DSAP_UNILATERAL_BASE+16);          /*BCP向CCP0 CORE1 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_CCP0_C2 = (NB_DSAP_UNILATERAL_BASE+17);          /*BCP向CCP0 CORE2 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_CCP0_C3 = (NB_DSAP_UNILATERAL_BASE+18);          /*BCP向CCP0 CORE3 发送消息*/

		public const ushort NB_DSAP_OSP_BCP_DSP_DLCP_C0 = (NB_DSAP_UNILATERAL_BASE+19);          /*BCP向DLCP CORE0 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_DLCP_C1 = (NB_DSAP_UNILATERAL_BASE+20);          /*BCP向DLCP CORE1 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_DLCP_C2 = (NB_DSAP_UNILATERAL_BASE+21);          /*BCP向DLCP CORE2 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_DLCP_C3 = (NB_DSAP_UNILATERAL_BASE+22);          /*BCP向DLCP CORE3 发送消息*/

		public const ushort NB_DSAP_OSP_BCP_DSP_ULCP0_C0 = (NB_DSAP_UNILATERAL_BASE+23);          /*BCP向ULCP0 CORE0 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_ULCP0_C1 = (NB_DSAP_UNILATERAL_BASE+24);          /*BCP向ULCP0 CORE1 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_ULCP0_C2 = (NB_DSAP_UNILATERAL_BASE+25);          /*BCP向ULCP0 CORE2 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_ULCP0_C3 = (NB_DSAP_UNILATERAL_BASE+26);          /*BCP向ULCP0 CORE3 发送消息*/

		public const ushort NB_DSAP_OSP_BCP_DSP_ULCP1_C0 = (NB_DSAP_UNILATERAL_BASE+27);          /*BCP向ULCP1 CORE0 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_ULCP1_C1 = (NB_DSAP_UNILATERAL_BASE+28);          /*BCP向ULCP1 CORE1 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_ULCP1_C2 = (NB_DSAP_UNILATERAL_BASE+29);          /*BCP向ULCP1 CORE2 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_ULCP1_C3 = (NB_DSAP_UNILATERAL_BASE+30);          /*BCP向ULCP1 CORE3 发送消息*/

		public const ushort NB_DSAP_OSP_BCP_DSP_ULCP2_CCP1_C0 = (NB_DSAP_UNILATERAL_BASE+31);          /*BCP向ULCP2/CCP1 CORE0 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_ULCP2_CCP1_C1 = (NB_DSAP_UNILATERAL_BASE +32);          /*BCP向ULCP2/CCP1 CORE1 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_ULCP2_CCP1_C2 = (NB_DSAP_UNILATERAL_BASE +33);          /*BCP向ULCP2/CCP1 CORE2 发送消息*/
		public const ushort NB_DSAP_OSP_BCP_DSP_ULCP2_CCP1_C3 = (NB_DSAP_UNILATERAL_BASE +34);          /*BCP向ULCP2/CCP1 CORE3 发送消息*/

		public const ushort NB_IP_PORT_OSP_P2P = 20000;                               /*OSP_DSAP点对点。用于基站内部处理器间信令通信*/                              /**/
		public const ushort NB_IP_PORT_OSP_BACKUP_P2P = 22222;                               /*OSP_DSAP点对点。用于主备板卡之间的备份数据通信*/

		public const ushort NB_IP_PORT_NO_OSP_TNP_RIP_SAAL0 = 16660;                               /*SAAL TNP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_TNP_RIP_SAAL1 = 16661;                               /*SAAL TNP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_TNP_RIP_SAAL2 = 16662;                               /*SAAL TNP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_TNP_RIP_SAAL3 = 16663;                               /*SAAL TNP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_SCP_RIP_IPOA = 11111;                               /*IPOA缺省*/
		public const ushort NB_IP_PORT_NO_OSP_IFP_BCP_BROADCAST = 33333;                               /*NO_OSP_DSAP 广播*/
		public const ushort NB_IP_PORT_NO_OSP_IFP_BCP_P2P = 30000;                               /*RRS与BBU之间的点对点通信*/
		public const ushort NB_IP_PORT_NO_OSP_IFP_LMTRRS_BROADCAST = 34567;                               /*LMT-RRS与RRS之间的广播通信*/
		public const ushort NB_IP_PORT_NO_OSP_IFP_LMTRRS_P2P = 31000;                               /*LMT-RRS与RRS之间的点对点通信*/  

	/* 36A端口规划 */
	/* 基站内部带OSP头的P2P链路，端口号复用144A的宏定义NB_IP_PORT_OSP_P2P */
	/* BCP-DSP之间的链路，由DSI改为PSA，端口号复用144A的宏定义NB_IP_PORT_OSP_P2P */
	/* CCU板主备通信，端口号复用144A的宏定义NB_IP_PORT_OSP_BACKUP_P2P */
	/* 无OSP头的链路，SCP-NP, IPOA, IFP-EIP， DSP-PC端口号定义如下*/
		public const ushort NB_IP_PORT_NO_OSP_SCP_NP_SAAL0 = 16660;                               /*SAAL SCP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_SCP_NP_SAAL1 = 16661;                               /*SAAL SCP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_SCP_NP_SAAL2 = 16662;                               /*SAAL SCP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_SCP_NP_SAAL3 = 16663;                               /*SAAL SCP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_SCP_NP_SCTP0 = 16670;                               /*SCTP SCP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_SCP_NP_SCTP1 = 16671;                               /*SCTP SCP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_SCP_NP_SCTP2 = 16672;                               /*SCTP SCP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_SCP_NP_SCTP3 = 16673;                               /*SCTP SCP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_SCP_NP_SCTP4 = 16674;                               /*SCTP SCP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_SCP_NP_SCTP5 = 16675;                               /*SCTP SCP<->WINPATH*/
		public const ushort NB_IP_PORT_NO_OSP_SCP_SCTP_SERVER = 16676;                               /*SCTP SERVER*/

		public const ushort NB_IP_PORT_NO_OSP_SCP_NP_IPOA = 11111;                               /*IPOA缺省*/

		public const ushort NB_IP_PORT_NO_OSP_IFP_EIP_BROADCAST = 33333;                               /*NO_OSP_DSAP 广播*/
		public const ushort NB_IP_PORT_NO_OSP_IFP_EIP_P2P = 30000;                               /*RRS与BBU之间的点对点通信*/

		public const ushort NB_IP_PORT_NO_OSP_DSP_PC_GTSA = 50000;                               /*DSP与PC之间发送GTS抄送消息报文链路端口号*/


		//TODO 不知道什么时候用到
///*0号DSAP通信可用以下的宏通过SFUID获取IP地址*/
//public const ushort NB_OSP_GET_IP_FROM_SUFID(sfuId);      \
//    (10<<24 | 0 <<16 | sfuId.SubrackId<<12 | sfuId.SlotId<<8 | 192 | sfuId.ProcId); 

///*第一个RRS 的IP地址*/
//public const ushort NB_NO_OSP_RRS1_GET_IP_FROM_SUFID(sfuId);        \
//    (10<<24 | 2 <<16 | sfuId.SubrackId<<12 | sfuId.SlotId<<8 | 64 | sfuId.ProcId);
///*第二个RRS 的IP地址*/
//public const ushort NB_NO_OSP_RRS2_GET_IP_FROM_SUFID(sfuId);        \
//    (10<<24 | 2 <<16 | sfuId.SubrackId<<12 | sfuId.SlotId<<8 | 128 | sfuId.ProcId);

///* 根据子网ID生成IP: 10.subnet.xx.xx */
//public const ushort NB_GET_IP_BY_SUBNET(subnet, subrack, slot, proc);      \
//    (10 << 24 | subnet <<16 | subrack << 12 | slot << 8 | 192 | proc);

		public const string NB_DEBUG_ETH_DEV_NAME = "dtm_eth0";                             /*调试网口设备名*/
		public const string NB_TRAFFIC_ETH_DEV_NAME = "dtm_eth1";                             /*业务网口设备名*/
		public const string NB_CCU_BACKUP_ETH_DEV_NAME = "dtm_eth2";                             /*CCU主备网口设备名*/
		public const string NB_BBU_DDP_ETH_DEV_NAME = "dtm_eth2";                             /*BBU接RRS的网口设备名*/
		public const string NB_EIU_EMP_ETH_DEV_NAME = "dtm_eth2"; /*EIU接RRS的网口设备名*/

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsgQueue;
using SCMTOperationCore.Message.SI;

namespace SCMTOperationCore.Elements.BaseElement
{

	//处理SI消息的数据包。每个NodeB对象持有一个
	public class SiMsgDealer
	{
		private string BoardIp { get; set; }

		//--------------------接口-------------------------
		public SiMsgDealer(string ip)
		{
			m_siMsgBuffer = new byte[BUFF_LEN];
			BoardIp = ip;
		}

		//处理si消息。newData是新收到的数据
		public bool DealSiMsg(byte[] newData)
		{
			if (null == newData)
			{
				return false;
			}

			int used = 0;
			int nNewDataRest = 0;
			while ( (nNewDataRest = newData.Length - used) > 0 )
			{
				var restLen = BUFF_LEN - m_nMsgLen;
				var minLen = Math.Min(restLen, nNewDataRest);
				Buffer.BlockCopy(newData, 0, m_siMsgBuffer, m_nMsgLen, minLen);
				m_nMsgLen += minLen;
				DealMsgs();
				used += minLen;
			}

			return true;
		}


		private void DealMsgs()
		{
			//判断数据长度是否>head的长度
			if (m_nMsgLen < 4)
			{
				//已经保存的数据长度连报文头长度都不够，继续攒
				return;
			}

			var head = SiMsgHelper.GetSiMsgHead(m_siMsgBuffer);
			if (head.u16MsgLength <= 0)		//消息长度设置错误
			{
				//按照原来的处理流程是清空掉所有的数据
				var newBuf = new byte[BUFF_LEN];      //构造一个新的byte[],旧的gc
				m_siMsgBuffer = newBuf;
				m_nMsgLen = 0;
				return;
			}

			if (m_nMsgLen < head.u16MsgLength)		//确保已经保存的数据可以解析出一个完整的消息
			{
				//已经保存的数据不够长，继续攒
				return ;
			}

			var msgData = m_siMsgBuffer.Take(head.u16MsgLength).ToArray();  //取出一条消息所需的数据
			ProcessOneMsg(msgData);

			m_nMsgLen -= head.u16MsgLength;		//剩余的数据长度

			//剩余的数据前移。在这里直接new一个新的数组，旧的gc回收。
			var temp = new byte[BUFF_LEN];
			Buffer.BlockCopy(m_siMsgBuffer, head.u16MsgLength, temp, 0, m_nMsgLen);
			m_siMsgBuffer = temp;

			DealMsgs();		//递归处理
		}


		private bool ProcessOneMsg(byte[] msgBytes)
		{
			var head = SiMsgHelper.GetSiMsgHead(msgBytes);
			switch (head.u16MsgType)
			{
				case SiMacroDef.O_SILMTENB_GETFILEINFO_RES:
					PublishHelper.PublishMsg($"/{BoardIp}/O_SILMTENB_GETFILEINFO_RES", msgBytes);
					break;
				case SiMacroDef.O_SILMTENB_GETFILEATTRIB_RES:
					break;
				case SiMacroDef.O_SILMTENB_SETRDWRATTRIB_RES:
					break;
				case SiMacroDef.O_SILMTENB_DELFILE_RES:
					break;
				case SiMacroDef.O_SILMTENB_GETCAPACITY_RES:
					break;
				default:
					//TODO 不知道是什么啊，老铁
					break;
			}
			return true;
		}


		//--------------------属性--------------------------
		//保存未处理的数据。有两种情况：1.收到的数据分片了，还不够长；2.数据够长，处理后剩余的数据
		//之所以用byte[]，是为了使用Buffer.BlockCopy函数快速填充数据。
		private byte[] m_siMsgBuffer;
		private int m_nMsgLen;				//未处理的消息长度
		private int m_nOffset;				//下一次从哪里开始处理
		private const int BUFF_LEN = 65536;	//head中限制了一条完整消息最大长度是ushort的最大值65535。
	}
}

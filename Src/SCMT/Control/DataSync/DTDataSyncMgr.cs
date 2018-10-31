using CommonUtility;
using LmtbSnmp;
using LogManager;
using MsgQueue;

namespace DataSync
{
	/// <summary>
	/// DTDataSyncMgr数据同步管理器
	/// </summary>
	public class DTDataSyncMgr : Singleton<DTDataSyncMgr>
	{
		/// <summary>
		/// 构造方法
		/// </summary>
		private DTDataSyncMgr()
		{
		}

		/// <summary>
		/// 把变更数据存储到数据库中
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <returns></returns>
		public bool DealAlteration(CDTLmtbPdu lmtPdu)
		{
			if (null == lmtPdu)
			{
				Log.Error("参数lmtPdu为空！");
				return false;
			}

			// 发布消息
			byte[] bytes = SerializeHelper.Serialize2Binary(lmtPdu);
			PublishHelper.PublishMsg(TopicHelper.SnmpMsgDispose_CfgChgTrap, bytes);

			return true;
		}
	}
}

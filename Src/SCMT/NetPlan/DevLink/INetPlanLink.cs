using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan
{
	/// <summary>
	/// 网规中设备间连接的抽象接口
	/// </summary>
	public interface INetPlanLink
	{
		bool DelLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo);

		bool AddLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo);

		DevAttributeInfo GetRecord(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo);

		//	bool CheckLinkIsValid(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo);
	}
}
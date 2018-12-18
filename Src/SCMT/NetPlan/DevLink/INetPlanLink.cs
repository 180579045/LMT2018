namespace NetPlan
{
	/// <summary>
	/// 网规中设备间连接的抽象接口
	/// </summary>
	public interface INetPlanLink
	{
		bool DelLink(WholeLink wholeLink, NPDictionary mapMibInfo);

		bool AddLink(WholeLink wholeLink, NPDictionary mapMibInfo);

		DevAttributeInfo GetRecord(WholeLink wholeLink, NPDictionary mapMibInfo);

		//	bool CheckLinkIsValid(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo);
	}
}
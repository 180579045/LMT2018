namespace NetPlan
{
	internal static class NetDevFactory
	{
		internal static NetDevBase GetDevHandler(string strTargetIp, EnumDevType devType, NPDictionary mapOriginData)
		{
			NetDevBase handler;
			switch (devType)
			{
				case EnumDevType.rru:
					handler = new NetDevRru(strTargetIp, mapOriginData);
					break;

				case EnumDevType.ant:
					handler = new NetDevAnt(strTargetIp, mapOriginData);
					break;
				case EnumDevType.rhub:
					handler = new NetDevRhub(strTargetIp, mapOriginData);
					break;
				case EnumDevType.nrNetLc:
					handler = new NetDevLc(strTargetIp, mapOriginData);
					break;
				case EnumDevType.board:
					handler = new NetDevBoard(strTargetIp, mapOriginData);
					break;
				default:
					handler = new NetDevBase(strTargetIp, mapOriginData);
					break;
			}

			return handler;
		}
	}
}
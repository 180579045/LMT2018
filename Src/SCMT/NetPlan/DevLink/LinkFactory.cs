using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPlan.DevLink
{
	public static class LinkFactory
	{
		public static INetPlanLink CreateLinkHandler(EnumDevType linkType)
		{
			INetPlanLink linkHandler = null;
			switch (linkType)
			{
				case EnumDevType.board_rru:
					linkHandler = new LinkBoardRru();
					break;
				case EnumDevType.board_rhub:
					linkHandler = new LinkBoardRhub();
					break;
				case EnumDevType.rru_ant:
				case EnumDevType.prru_ant:
					linkHandler = new LinkRruAnt();
					break;
				case EnumDevType.rhub_prru:
					linkHandler = new LinkRhubPico();
					break;
				case EnumDevType.rru_rru:
					break;
				case EnumDevType.rhub_rhub:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(linkType), linkType, null);
			}

			return linkHandler;
		}
	}
}

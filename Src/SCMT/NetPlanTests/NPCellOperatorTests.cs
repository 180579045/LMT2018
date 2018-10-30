using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPlan;

namespace NetPlanTests
{
	[TestClass()]
	public class NPCellOperatorTests
	{
		[TestMethod()]
		public void SetCellActiveTriggerTest()
		{
			var ret = NPCellOperator.SetCellActiveTrigger(0, "172.27.245.92", CellOperType.active, 200);
			Assert.IsTrue(ret);
		}
	}
}
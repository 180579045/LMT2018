using System;
using System.Collections.Generic;
using CommonUility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonUilityTest
{
	public class TraceSwitch
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public TraceSwitch(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}

	public class TracesSwitchs
	{
		public int Count { get; set; }
		public List<TraceSwitch> Switches;
	}

	[TestClass]
	public class JsonHelperTests
	{
		[TestMethod()]
		public void SerializeObjectToStringTest()
		{
			TraceSwitch switch1 = new TraceSwitch(1, "test");

			string expect = "{\"Id\":1,\"Name\":\"test\"}";
			string actual = JsonHelper.SerializeObjectToString(switch1);
			Assert.AreEqual(expect, actual);

			TraceSwitch switch2 = new TraceSwitch(2, "OK");
			TracesSwitchs switchs = new TracesSwitchs
			{
				Count = 2,
				Switches = new List<TraceSwitch> {switch1, switch2}
			};

			expect = "{\"Switches\":[{\"Id\":1,\"Name\":\"test\"},{\"Id\":2,\"Name\":\"OK\"}],\"Count\":2}";
			actual = JsonHelper.SerializeObjectToString(switchs);
			Assert.AreEqual(expect, actual);
		}

		[TestMethod()]
		public void SerializeJsonToObjectTest()
		{
			string expect = "{\"Switches\":[{\"Id\":1,\"Name\":\"test\"},{\"Id\":2,\"Name\":\"OK\"}],\"Count\":2}";
			TracesSwitchs actual = JsonHelper.SerializeJsonToObject<TracesSwitchs>(expect);

			Assert.AreEqual(2, actual.Count);
			Assert.AreEqual(1, actual.Switches[0].Id);
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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

	public class Ts
	{
		public string ip;
		public byte[] ts;
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

		[TestMethod]
		public void SerializeJsonBytesToObjectTest()
		{
			Ts tt = new Ts()
			{
				ip = "172.27.245.92",
				ts = new byte[5] {1, 0, 1, 1, 1}
			};
			string json = JsonHelper.SerializeObjectToString(tt);

			byte[] btemp = Encoding.Default.GetBytes(json);

			Ts actual = JsonHelper.SerializeJsonToObject<Ts>(btemp);
			Assert.AreEqual("172.27.245.92", actual.ip);
			Assert.AreEqual(1, actual.ts[0]);
			Assert.AreEqual(0, actual.ts[1]);
		}
	}
}

using System;
using CommonUility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonUilityTest
{
	class BFTest : BitField
	{
		[BitInfo(5)]
		public bool val0;
		[BitInfo(5)]
		public byte val1;
		[BitInfo(15)]
		public uint val2;
		[BitInfo(15)]
		public float val3;
		[BitInfo(15)]
		public int val4;
		[BitInfo(15)]
		public int val5;
		[BitInfo(15)]
		public int val6;

		public BFTest(bool v0, byte v1, uint v2, float v3, int v4, int v5, int v6)
		{
			val0 = v0;
			val1 = v1;
			val2 = v2;
			val3 = v3;
			val4 = v4;
			val5 = v5;
			val6 = v6;
		}

		public BFTest(byte[] datas)
		{
			parse(datas);
		}

		public new string ToString()
		{
			return $"val0: {val0}, val1: {val1}, val2: {val2}, val3: {val3}, val4: {val4}, val5: {val5}, val6: {val6}\r\nbinary => {ArrayConverter.toBinary(toArray())}";
		}
	}

	[TestClass]
	public class BitFieldTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			BFTest t = new BFTest(true, 1, 12, -1.3f, 4, -5, 100);
			Assert.IsTrue(t.val0);
			string aa = t.ToString();

			BFTest t2 = new BFTest(t.toArray());


		}
	}
}

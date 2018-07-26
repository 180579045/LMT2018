using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{


	public static class GetHeaderHelper
	{
		public static T GetHeader<T>(byte[] bytes, int offset = 0) where T : IASerialize, new ()
		{
			var o = new T() as IASerialize;
			var dataLen = bytes.Length - offset;
			if (dataLen < o.Len)
			{
				return default(T);
			}

			if (-1 == o.DeserializeToStruct(bytes, offset))
				return default(T);

			return (T)o;
		}
	}
}

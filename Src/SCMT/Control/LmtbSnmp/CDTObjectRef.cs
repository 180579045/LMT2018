using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LmtbSnmp
{
	[Serializable]
	public class CDTObjectRef : IDTObjectRefInterface, IDisposable
	{
		public CDTObjectRef()
		{
			m_nRefValue = 0;
		}

		public void AddRef()
		{
			Interlocked.Increment(ref m_nRefValue);
		}

		public void ReleaseRef()
		{
			Interlocked.Decrement(ref m_nRefValue);
			if (m_nRefValue <= 0)
			{
				Dispose();
			}
		}

		private volatile int m_nRefValue;

		public void Dispose()
		{
			
		}
	}
}

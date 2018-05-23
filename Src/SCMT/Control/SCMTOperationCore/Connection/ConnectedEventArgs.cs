using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore.Connection
{
	public class ConnectedEventArgs : EventArgs, IRecyclable
	{
		static readonly ObjectPool<ConnectedEventArgs> objectPool = new ObjectPool<ConnectedEventArgs>(() => new ConnectedEventArgs());

		internal static ConnectedEventArgs GetObject()
		{
			return objectPool.GetObject();
		}

		public Exception Exception { get; private set; }

		ConnectedEventArgs()
		{

		}

		internal void Set(Exception e)
		{
			this.Exception = e;
		}

		public void Recycle()
		{
			objectPool.PutObject(this);
		}
	}
}

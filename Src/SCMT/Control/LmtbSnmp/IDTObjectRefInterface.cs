using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmtbSnmp
{
	interface IDTObjectRefInterface
	{
		void AddRef();

		void ReleaseRef();
	}
}

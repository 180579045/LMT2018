using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore.Elements
{
    /// <summary>
    /// 所有网元类型接口;
    /// </summary>
    interface IElement
    {
        Task ConnectAsync();

	    Task DisConnect();
    }
}

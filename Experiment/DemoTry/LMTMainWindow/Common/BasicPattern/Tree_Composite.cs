using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMTMainWindow
{
    interface Component
    {
        void Add(Component obj);
        void Remove(Component obj);
        void TraversetoList(int depth);
    }

    class Composite : Component
    {
        public List<Component> Tree_Lsit = new List<Component>();
        public void Add(Component obj)
        {
            Tree_Lsit.Add(obj);
        }

        public void Remove(Component obj)
        {
            Tree_Lsit.Remove(obj);
        }

        public void TraversetoList(int depth)
        {
            throw new NotImplementedException();
        }
    }

}

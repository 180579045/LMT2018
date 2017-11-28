using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace LMTMainWindow.View
{
    class ObjTreeNode : Component
    {
        public List<Component> Obj_Lsit = new List<Component>();
        private static JObject ObjJson { get; set; }

        public void Add(Component obj)
        {
            Obj_Lsit.Add(obj);
        }

        public void Remove(Component obj)
        {
            Obj_Lsit.Remove(obj);
        }

        public void LoadFromFile()
        {
            ObjJson = new JObject();
            using (StreamReader reader = File.OpenText(@"Data\Tree_Reference2.json"))
            {
                ObjJson = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            }
        }

        public void TraversetoList(int depth)
        {
            throw new NotImplementedException();
        }
    }
    
}

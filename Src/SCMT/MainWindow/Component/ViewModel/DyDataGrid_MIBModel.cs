using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using MIBDataParser;

namespace SCMTMainWindow.Component.ViewModel
{
    /// <summary>
    /// 一个动态MIB表模型,在主界面生成DataGrid的时候,通过该类型生成对应的模型;
    /// </summary>
    public class DyDataGrid_MIBModel : DynamicObject
    {
        /// <summary>
        /// 用来保存这个动态类型的所有属性;
        /// Key:为属性的名字;
        /// Value:为属性的值（同时也包含了类型）;
        /// </summary>
        public Dictionary<string, object> Properties = new Dictionary<string, object>();

        /// <summary>
        /// 用来保存中文列名与属性的对应关系;
        /// </summary>
        Dictionary<string, string> ColName_Property = new Dictionary<string, string>();

        /// <summary>
        /// 优化结构:使用元组保存所有的动态属性;
        /// Item1:属性名;
        /// Item2:列名;
        /// Item3:属性实例;
        /// </summary>
        public List<Tuple<string, string, object>> PropertyList { get; set; }
        /// <summary>
        /// 用于保存当前点击节点对应MibTable，便于根据表明进行添加，查询，修改等操作
        /// </summary>

        public object TableProperty = new object();

        // 为动态类型动态添加成员;
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!Properties.Keys.Contains(binder.Name))
            {
                Properties.Add(binder.Name, value);
            }
            return true;
        }

        // 为动态类型动态添加方法;
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            // 可以通过调用方法的手段添加属性;
            if (binder.Name == "AddProperty" && binder.CallInfo.ArgumentCount == 3)
            {
                string name = args[0] as string;
                if (name == null || Properties.ContainsKey(name))
                {
                    //throw new ArgumentException("name");  
                    result = null;
                    return false;
                }

	            if (Properties.Count > 0 && Properties.ContainsKey(name))
	            {
		            result = null;
		            return true;
	            }

                // 向属性列表添加属性及其值;
                object value = args[1];
                Properties.Add(name, value);

                // 添加列名与属性列表的映射关系;
                string column_name = args[2] as string;
                ColName_Property.Add(column_name, name);

                PropertyList.Add(new Tuple<string, string, object>(name, column_name, value));

                result = value;
                 return true;
            }
            // StartEditing事件，判断应该使用那个属性操作;
            if (binder.Name == "JudgePropertyName_StartEditing" && binder.CallInfo.ArgumentCount == 1)
            {
                string columnname = args[0] as string;
                if (columnname == null)
                {
                    result = null;
                    return false;
                }

                // 在当前列名于属性列表中查找，看是否有匹配项;
                if (ColName_Property.ContainsKey(columnname))
                {
                    string key = ColName_Property[columnname];
                    // 如果存在对应得属性;
                    if (Properties.ContainsKey(key))
                    {
                        object property = Properties[key];
                        (property as GridCell).EditingCallback();
                        result = property;
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("Can not find the right property");
                    result = null;
                    return false;
                }

            }

            if (binder.Name == "AddTableProperty" && binder.CallInfo.ArgumentCount == 1)
            {
                string name = (args[0] as MibTable).nameCh;

                if (name == null)
                {
                    result = null;
                    return false;
                }
                // 向属性列表添加属性及其值;
                object value = args[0];
                TableProperty = value;

                result = value;
                return true;
            }

            return base.TryInvokeMember(binder, args, out result);
        }

        /// <summary>
        /// 获取属性;
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return Properties.TryGetValue(binder.Name, out result);
        }

        /// <summary>
        /// 当单元格失去焦点之后，统一调用单元格类的对应函数;
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool JudgePropertyName_ChangeSelection(string name, object SelectionObj)
        {
            bool ret = false;

            // 在当前列名于属性列表中查找，看是否有匹配项;
            if (ColName_Property.ContainsKey(name))
            {
                string key = ColName_Property[name];
                if (Properties.ContainsKey(key))
                {
                    object property = Properties[key];
                    (property as GridCell).SelectionCellChanged(SelectionObj);
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Can not find the right property");
                return false;
            }

            return ret;
        }

        /// <summary>
        /// 初始化;
        /// </summary>
        public DyDataGrid_MIBModel()
        {
            PropertyList = new List<Tuple<string, string, object>>();
        }

    }
}

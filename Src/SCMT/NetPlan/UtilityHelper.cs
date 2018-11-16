using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LogManager;

// todo 后期已到CommonUtility中
namespace NetPlan
{
	public static class UtilityHelper
	{
		/// 使用反射获取指定对象指定字段的值
		public static bool GetFieldValueOfType<T>(object obj, string fieldName, out dynamic fieldValue)
		{
			fieldValue = new object();

			if (obj is T)
			{
				var newObj = (T)Convert.ChangeType(obj, typeof(T));
				if (newObj == null)
					return false;

				var info = newObj.GetType().GetProperty(fieldName);		// fieldName对应的必须是属性，且设置了public get
				if (null == info)
				{
					Log.Error(typeof(T).Name + " not have property " + fieldName);
					return false;
				}

				fieldValue = info.GetValue(newObj, null);
				return true;
			}

			return false;
		}
	}
}

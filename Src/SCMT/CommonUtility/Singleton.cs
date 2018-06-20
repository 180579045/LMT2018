using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUtility
{
	/// <summary>
	/// 泛化单例模式
	/// </summary>
	public class Singleton<T> where T : class
	{
		private static T _instance;
		private static readonly object syslock = new object();

		public static T GetInstance()
		{
			if (null == _instance)
			{
				lock (syslock)
				{
					if (null == _instance)
					{
						_instance = (T) Activator.CreateInstance(typeof(T), true);
					}
				}
			}

			return _instance;
		}
	}
}

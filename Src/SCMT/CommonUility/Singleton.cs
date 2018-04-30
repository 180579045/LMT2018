using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUility
{
    /// <summary>
    /// 泛化单例模式，使用方法：
    /// public static PubSubServer GetInstance()
    /// {
    ///    return Singleton<PubSubServer>.GetInstance();
    /// }
    /// </summary>
public class Singleton<T> where T : class, new()
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
                        _instance = new T();
                    }
                }
            }

            return _instance;
        }
    }
}

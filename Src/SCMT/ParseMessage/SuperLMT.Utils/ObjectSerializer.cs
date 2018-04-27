//===============================================================================
// Serializer 
//      --对象实例的序列化工具类
//===============================================================================
// Copyright ? Datang Mobile Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// Author:  pengqiang
// History: 
//   2012/2/16 created by pengqiang
//===============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Common.Logging;

namespace SuperLMT.Utils
{
    public class ObjectSerializer
    {
        /// <summary>
        /// The shared <see cref="Common.Logging.ILog"/> instance for this class (and derived classes).
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(ObjectSerializer));

        #region Binary Serializers

        /// <summary>
        /// 将对象实例序列化为二进制流
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static byte[] SerializeBinary(object obj) //将对象流转换成二进制流
        {
            Check.Require(null != obj, "待序列化的对象为空！");

            byte[] binaryObject = null;
            using(MemoryStream memStream = new MemoryStream())
            {  
                BinaryFormatter serializer = new BinaryFormatter();
                try
                {
                    serializer.Serialize(memStream, obj);
                    binaryObject = memStream.ToArray();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    throw ex;
                }   
            }

            return binaryObject;
        }

        /// <summary>
        /// 将二进制流反序列化为对象实例
        /// </summary>
        /// <param name="binaryObject"></param>
        /// <returns></returns>
        public static object DeSerializeBinary(byte[] binaryObject) //将二进制流转换成对象
        {
            Check.Require( null != binaryObject, "待反序列化的二进制流为空！");

            object newObj = null;
            using(MemoryStream memStream = new MemoryStream(binaryObject))
            {
                BinaryFormatter deSerializer = new BinaryFormatter();
                try
                {
                    newObj = deSerializer.Deserialize(memStream);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    throw ex;
                }    
            }
            
            return newObj;
        }
        #endregion
    }
}

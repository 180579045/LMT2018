/*----------------------------------------------------------------
// Copyright (C) 2004 大唐移动通信设备有限公司
// 版权所有。 
//
// 文件名：DataOperationHelper.cs
// 文件功能描述：一些数据转换操作
//
// 版本：
// 作者：于晓伟 2012/05/17
// 
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SuperLMT.Utils
{
    
    /// <summary>
    /// 没用使用BitConverter,可能会提升一些效率
    /// </summary>
    public class DataOperHelper
    {
        /// <summary>
        /// Byte数组到Int
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static int ByteArrray2Int(byte[] buffer, int pos)
        {
            return (buffer[pos] << 24) + (buffer[pos + 1] << 16) + (buffer[pos + 2] << 8) + buffer[pos + 3];
        }

        /// <summary>
        /// Int到ByteArray
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] Int2ByteArray(int value)
        {
            byte[] buffer = new byte[4];
            buffer[0] = (byte)((value >> 24));
            buffer[1] = (byte)((value >> 16));
            buffer[2] = (byte)((value >> 8));
            buffer[3] = (byte)(value );
            return buffer;
        }

        /// <summary>
        /// Byte数组到UInt16
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static UInt16 ByteArray2UInt16(byte[] buffer, int pos)
        {
            int high = buffer[pos] << 8;
            int low = buffer[pos + 1];
            int value = high + low;
            return (UInt16)value;
        }

        /// <summary>
        /// 移位操作，都以Int来存放
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static int ByteArray2Char(byte[] buffer, int pos)
        {
            return ((buffer[pos] << 8) +(buffer[pos + 1]));
        }

        /// <summary>
        /// 获取char类型数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static char ByteArrayToChar(byte[] buffer, int pos)
        {
            return Convert.ToChar(buffer[pos]);
        }

        /// <summary>
        /// 获取8bit的整形数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static UInt16 Byte8bitToInt(byte[] buffer, int pos)
        {
            return Convert.ToUInt16(buffer[pos]);
        }
        

        /// <summary>
        /// string类型到RGB
        /// </summary>
        /// <param name="color"></param>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool StringTORGB(string color, out byte R, out byte G, out byte B)
        {
            R = 0;
            G = 0;
            B = 0;
            try
            {
	            int colorValue = System.Convert.ToInt32(color);
	            R = (byte)(colorValue >> 16);
	            G = (byte)(colorValue >> 8);
	            B =  (byte)colorValue;
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }


        //==============================================================================================
        //位操作
        /// <summary>
        /// 屏蔽某一比特位，将某比特位变0
        /// </summary>
        /// <param name="toMaskValue"></param>
        /// <param name="bitPos"></param>
        /// <returns></returns>
        public static int MaskABit(int toMaskValue,int bitPos)
        {
            return toMaskValue &(~(0x00000001 << bitPos));
        }

        /// <summary>
        /// 使能某一比特位
        /// </summary>
        /// <param name="value"></param>
        /// <param name="bitPos"></param>
        /// <returns></returns>
        public static int EnableABit(int value,int bitPos)
        {
            return value | (0x00000001 << bitPos);
        }

        /// <summary>
        /// 设置某一位，0，或者1
        /// </summary>
        /// <param name="srcValue"></param>
        /// <param name="bitPos"></param>
        /// <param name="setValue"></param>
        /// <returns></returns>
        public static int SetBitValue(int srcValue, int bitPos,int setValue)
        {
           return (srcValue - (0x00000001 << bitPos)) | (setValue&0x00000001);
        }

        /// <summary>
        /// 将结构转换为字节数组 (小端排序)
        /// </summary>
        /// <param name="obj">结构对象</param>
        /// <returns>字节数组</returns>
        public static byte[] StructToBytes(object obj)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(obj);
            //创建byte数组
            byte[] bytes = new byte[size];
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(obj, structPtr, false);
            //从内存空间拷到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回byte数组
            return bytes;

        }
        /// <summary>
        /// 结构体转字节数组（按大端模式）      
        /// </summary> 
        /// <param name="obj">struct type</param> 
        /// <returns></returns>   
        public static byte[] StructureToByteArrayEndian(object obj)
        {
            try
            {
                object thisBoxed = obj;//copy ，将 struct 装箱 
                Type test = thisBoxed.GetType();
                int offset = 0;
                byte[] data = new byte[Marshal.SizeOf(thisBoxed)];
                object fieldValue;
                TypeCode typeCode;
                byte[] temp;
                // 列举结构体的每个成员，并Reverse 
                System.Reflection.FieldInfo[] infos = test.GetFields();
                foreach (var field in test.GetFields())
                {
                    fieldValue = field.GetValue(thisBoxed); // Get value 
                    typeCode = Type.GetTypeCode(fieldValue.GetType());  // get type 
                    #region
                    switch (typeCode)
                    {
                        case TypeCode.Single: // float
                            {
                                temp = BitConverter.GetBytes((Single)fieldValue);
                                Array.Reverse(temp);
                                Array.Copy(temp, 0, data, offset, sizeof(Single));
                                break;
                            }
                        case TypeCode.Int32:
                            {
                                temp = BitConverter.GetBytes((Int32)fieldValue);
                                Array.Reverse(temp);
                                Array.Copy(temp, 0, data, offset, sizeof(Int32));
                                break;
                            }
                        case TypeCode.UInt32:
                            {
                                temp = BitConverter.GetBytes((UInt32)fieldValue);
                                Array.Reverse(temp);
                                Array.Copy(temp, 0, data, offset, sizeof(UInt32));
                                break;
                            }
                        case TypeCode.Int16:
                            {
                                temp = BitConverter.GetBytes((Int16)fieldValue);
                                Array.Reverse(temp);
                                Array.Copy(temp, 0, data, offset, sizeof(Int16));
                                break;
                            }
                        case TypeCode.UInt16:
                            {
                                temp = BitConverter.GetBytes((UInt16)fieldValue);
                                Array.Reverse(temp);
                                Array.Copy(temp, 0, data, offset, sizeof(UInt16));
                                break;
                            }
                        case TypeCode.Int64:
                            {
                                temp = BitConverter.GetBytes((Int64)fieldValue);
                                Array.Reverse(temp);
                                Array.Copy(temp, 0, data, offset, sizeof(Int64));
                                break;
                            }
                        case TypeCode.UInt64:
                            {
                                temp = BitConverter.GetBytes((UInt64)fieldValue);
                                Array.Reverse(temp);
                                Array.Copy(temp, 0, data, offset, sizeof(UInt64));
                                break;
                            }
                        case TypeCode.Double:
                            {
                                temp = BitConverter.GetBytes((Double)fieldValue);
                                Array.Reverse(temp);
                                Array.Copy(temp, 0, data, offset, sizeof(Double));
                                break;
                            }
                        case TypeCode.Byte:
                            {
                                data[offset] = (Byte)fieldValue;
                                break;
                            }
                        default:
                            {
                                //System.Diagnostics.Debug.Fail("No conversion provided for this type : " + typeCode.ToString());
                                break;
                            }
                    } // switch
                    #endregion
                    if (typeCode == TypeCode.Object)
                    {
                        //int length = ((byte[])fieldValue).Length;
                        //Array.Copy(((byte[])fieldValue), 0, data, offset, length);
                        try
                        {
                            
                            Type type = fieldValue.GetType();
                            if (type.IsArray)
                            {
                                Array temp_array = (Array)fieldValue;
                                for (int index = 0; index < temp_array.Length; index++ )
                                {
                                    object sub_o = temp_array.GetValue(index);
                                    int length = Marshal.SizeOf(sub_o);
                                    byte[] subdata = DataOperHelper.StructureToByteArrayEndian(sub_o);
                                    Array.Copy(subdata, 0, data, offset, length);
                                    offset += length;  
                                }

                            }
                            else
                            {
                                int length = Marshal.SizeOf(fieldValue);
                                byte[] subdata = DataOperHelper.StructureToByteArrayEndian(fieldValue);
                                Array.Copy(subdata, 0, data, offset, length);
                                offset += length;  
                            }                                                      
                        }
                        catch
                        {
                            
                            //type. fileds = (type)fieldValue;
                            //for (int index = 0; index < fileds.Length; index++)
                            //{
                            //    int length = Marshal.SizeOf(fileds[index]);
                            //    byte[] subdata = DataOperHelper.StructureToByteArrayEndian(fieldValue);
                            //    Array.Copy(subdata, 0, data, offset, length);
                            //    offset += length;
                            //}
                        }
                        
                    }
                    else
                    {
                        offset += Marshal.SizeOf(fieldValue);
                        if (offset >= data.Length)
                        {
                            offset -= Marshal.SizeOf(fieldValue);
                        }
                    }
                } // foreach
                return data;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// byte数组转结构 (小端模式)
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="type">结构类型</param>
        /// <returns>转换后的结构</returns>
        public static void BytesToStruct(byte[] bytes, int startIndex, ref object obj)
        {
            try
            {
                //得到结构的大小
                int size = Marshal.SizeOf(obj.GetType());
                // Log(size.ToString(), 1);
                //byte数组长度小于结构的大小 不能转换则返回 null
                if (size > bytes.Length)
                {
                    //返回空
                    return;
                }
                //分配结构大小的内存空间
                IntPtr structPtr = Marshal.AllocHGlobal(size);
                //将byte数组拷到分配好的内存空间
                Marshal.Copy(bytes, startIndex, structPtr, size);
                //将内存空间转换为目标结构
                obj = Marshal.PtrToStructure(structPtr, obj.GetType());
                //释放内存空间
                Marshal.FreeHGlobal(structPtr);
            }
            catch
            {                 
            }
           
        }
        /// <summary>       
        /// 字节数组转结构体(按大端模式) 
        /// </summary>        
        /// <param name="bytearray">字节数组</param>        
        /// <param name="obj">目标结构体</param>        
        /// <param name="startoffset">bytearray内的起始位置</param>       
        public static void ByteArrayToStructureEndian(byte[] bytearray, ref object obj, int startoffset)
        {
            int len = Marshal.SizeOf(obj);
            IntPtr i = Marshal.AllocHGlobal(len);
            byte[] temparray = (byte[])bytearray.Clone();
            // 从结构体指针构造结构体           
            obj = Marshal.PtrToStructure(i, obj.GetType());
            // 做大端转换  
            object thisBoxed = obj;
            Type typeRoot = thisBoxed.GetType();
            int reversestartoffset = startoffset;
            // 列举结构体的每个成员，并Reverse     
            reversestartoffset = ReverseBytes(ref temparray, reversestartoffset, thisBoxed);
            try
            {
                //将字节数组复制到结构体指针   
                Marshal.Copy(temparray, startoffset, i, len);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ByteArrayToStructure FAIL: error " + ex.ToString());
            }
            obj = Marshal.PtrToStructure(i, obj.GetType());
            Marshal.FreeHGlobal(i);  //释放内存    
        }
        /// <summary>
        /// 反转字节数组
        /// </summary>
        /// <param name="temparray">字节数组</param>
        /// <param name="offset">起始位置</param>
        /// <param name="fieldValue">反转区域</param>
        /// <returns></returns>
        private static int ReverseBytes(ref byte[] temparray, int offset, object fieldValue)
        {
            int reversestartoffset = offset;
            Type rootType = fieldValue.GetType();
            int length = rootType.GetFields().Length;
            TypeCode typeCode = Type.GetTypeCode(fieldValue.GetType());  //Get Type   
            if (length > 0 && typeCode == TypeCode.Object)
            {
                foreach (var field in rootType.GetFields())
                {
                    object subfieldValue = field.GetValue(fieldValue);
                    Type subType = subfieldValue.GetType();
                    int length2 = subType.GetFields().Length;
                    if (length2 > 0)
                    {
                        reversestartoffset = ReverseBytes(ref temparray, reversestartoffset, subfieldValue);
                    }
                    if (subType.IsArray)
                    {
                        Array temp_array = (Array)subfieldValue;
                        for (int index = 0; index < temp_array.Length; index++)
                        {
                            object sub_o = temp_array.GetValue(index);
                            reversestartoffset = ReverseBytes(ref temparray, reversestartoffset, sub_o);
                        }

                    }
                }
            }
            else
            {
                if (typeCode != TypeCode.Object)  //如果为值类型    
                {
                    Array.Reverse(temparray, reversestartoffset, Marshal.SizeOf(fieldValue));
                    reversestartoffset += Marshal.SizeOf(fieldValue);
                }
                else  //如果为引用类型         
                {
                    reversestartoffset += ((byte[])fieldValue).Length;
                }
            }

            return reversestartoffset;
        }
    }
}

       

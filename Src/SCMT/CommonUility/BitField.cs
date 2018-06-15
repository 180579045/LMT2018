//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.IO.IsolatedStorage;
//using System.Net;
//using System.Reflection;
//using System.Text;

//namespace CommonUility
//{
//	[System.AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
//	public sealed class BitInfoAttribute : Attribute
//	{
//		public BitInfoAttribute(byte length)
//		{
//			Length = length;
//		}

//		public byte Length { get; private set; }
//	}

//	public abstract class BitField
//	{
//		public void parse<T>(T[] vals)
//		{
//			analysis().parse(this, ArrayConverter.convert<T, uint>(vals));
//		}

//		public byte[] toArray()
//		{
//			return ArrayConverter.convert<uint, byte>(analysis().toArray(this));
//		}

//		public T[] toArray<T>()
//		{
//			return ArrayConverter.convert<uint, T>(analysis().toArray(this));
//		}

//		static Dictionary<Type, BitTypeInfo> bitInfoMap = new Dictionary<Type, BitTypeInfo>();

//		private BitTypeInfo analysis()
//		{
//			Type type = this.GetType();
//			if (!bitInfoMap.ContainsKey(type))
//			{
//				List<BitInfo> infos = new List<BitInfo>();

//				byte dataIdx = 0, offset = 0;
//				foreach (FieldInfo f in type.GetFields())
//				{
//					object[] attrs = f.GetCustomAttributes(typeof(BitInfoAttribute), false);
//					if (1 == attrs.Length)
//					{
//						byte bitLen = ((BitInfoAttribute) attrs[0]).Length;
//						if (offset + bitLen > 32)
//						{
//							dataIdx++;
//							offset = 0;
//						}

//						infos.Add(new BitInfo(f, bitLen, dataIdx, offset));
//						offset += bitLen;
//					}
//				}

//				bitInfoMap.Add(type, new BitTypeInfo(dataIdx + 1, infos.ToArray()));
//			}

//			return bitInfoMap[type];
//		}
//	}

//	public class BitTypeInfo
//	{
//		public int dataLen { get; private set; }

//		public BitInfo[] bitInfos { get; private set; }

//		public BitTypeInfo(int _dataLen, BitInfo[] _bitInfos)
//		{
//			dataLen = _dataLen;
//			bitInfos = _bitInfos;
//		}

//		public uint[] toArray<T>(T obj)
//		{
//			uint[] data = new uint[dataLen];
//			foreach (BitInfo bif in bitInfos)
//			{
//				bif.encode(obj, data);
//			}

//			return data;
//		}

//		public void parse<T>(T obj, uint[] vals)
//		{
//			foreach (BitInfo bif in bitInfos)
//			{
//				bif.decode(obj, vals);
//			}
//		}
//	}

//	public class BitInfo
//	{
//		private FieldInfo field;
//		private uint mask;
//		private byte idx, offset, shiftA, shiftB;
//		private bool isUnsigned = false;

//		public BitInfo(FieldInfo _field, byte _bitLen, byte _idx, byte _offset)
//		{
//			field = _field;
//			mask = (uint)( ( ( 1 << _bitLen ) - 1 ) << _offset);
//			idx = _idx;
//			offset = _offset;
//			shiftA = (byte) (32 - _offset - _bitLen);
//			shiftB = (byte) (32 - _bitLen);

//			if (_field.FieldType == typeof(bool) ||
//				_field.FieldType == typeof(byte) ||
//				_field.FieldType == typeof(char) ||
//				_field.FieldType == typeof(uint) ||
//				_field.FieldType == typeof(ulong) ||
//				_field.FieldType == typeof(ushort) )
//			{
//				isUnsigned = true;
//			}
//		}

//		public void encode(Object obj, uint[] data)
//		{
//			if (isUnsigned)
//			{
//				uint val = (uint)Convert.ChangeType(field.GetValue(obj), typeof(uint));
//				data[idx] |= (val << offset & mask);
//			}
//			else
//			{
//				int val = (int) Convert.ChangeType(field.GetValue(obj), typeof(int));
//				data[idx] |= ((uint)(val << offset) & mask);
//			}
//		}

//		public void decode(Object obj, uint[] data)
//		{
//			if (isUnsigned)
//			{
//				uint val = ((( (data[idx] & mask) ) << shiftA) >> shiftB);
//				field.SetValue(obj, Convert.ChangeType(val, field.FieldType));
//			}
//			else
//			{
//				int val = ((((int)(data[idx] & mask)) << shiftA) >> shiftB);
//				field.SetValue(obj, Convert.ChangeType(val, field.FieldType));
//			}
//		}
//	}

//	public class ArrayConverter
//	{
//		public static T[] convert<T>(uint[] val)
//		{
//			return convert<uint, T>(val);
//		}

//		public static T1[] convert<T0, T1>(T0[] val)
//		{
//			T1[] rt = null;

//			if (typeof(T0) == typeof(T1))
//			{
//				rt = (T1[]) (Array) val;
//			}
//			else
//			{
//				int len = Buffer.ByteLength(val);
//				int w = typeWidth<T1>();

//				switch (w)
//				{
//					case 1:
//						rt = new T1[len * 8];
//						break;
//					case 8:
//						rt = new T1[len];
//						break;
//					default:
//						int nn = w / 8;
//						int len2 = (len / nn) + ((len % nn) > 0 ? 1 : 0);
//						rt = new T1[len2];
//						break;
//				}

//				Buffer.BlockCopy(val, 0, rt, 0, len);
//			}

//			return rt;
//		}

//		public static string toBinary<T>(T[] vals)
//		{
//			StringBuilder sb = new StringBuilder();
//			int width = typeWidth<T>();
//			int len = Buffer.ByteLength(vals);
//			for (int i = len - 1; i > 0; i--)
//			{
//				sb.Append(Convert.ToString(Buffer.GetByte(vals, i), 2).PadLeft(8, '0')).Append(" ");
//			}

//			return sb.ToString();
//		}

//		private static int typeWidth<T>()
//		{
//			int rt = 0;
//			if (typeof(T) == typeof(bool))
//			{
//				rt = 1;
//			}
//			else if (typeof(T) == typeof(byte) ||
//				typeof(T) == typeof(sbyte))
//			{
//				rt = 8;
//			}
//			else if (typeof(T) == typeof(ushort) ||
//				typeof(T) == typeof(short) ||
//				typeof(T) == typeof(char))		//char 16 bits?
//			{
//				rt = 16;
//			}
//			else if (typeof(T) == typeof(int) ||
//				typeof(T) == typeof(uint) ||
//				typeof(T) == typeof(float))
//			{
//				rt = 32;
//			}
//			else if (typeof(T) == typeof(long) ||
//				typeof(T) == typeof(ulong) ||
//				typeof(T) == typeof(double))
//			{
//				rt = 64;
//			}
//			else
//			{
//				throw new Exception("Unsupport type : " + typeof(T).Name);
//			}

//			return rt;
//		}
//	}
//}

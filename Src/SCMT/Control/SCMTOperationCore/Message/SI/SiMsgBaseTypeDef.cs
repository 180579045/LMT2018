using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;

namespace SCMTOperationCore.Message.SI
{
	//双字节有符号
	public class SIs16
	{
		public short m_value;

		public SIs16()
		{
			m_value = 0;
		}

		public SIs16(short left)
		{
			m_value = SerializeHelper.ReverseShort(left);
		}

		//实现short到SIs16的隐式转换。c# 7.0才开始提供operator = 的重载
		public static implicit operator SIs16(short left)
		{
			return new SIs16(left);
		}

		//实现SIs16到short的隐式转换
		public static implicit operator short(SIs16 value)
		{
			return value.GetHostValue();
		}

		public short GetHostValue()
		{
			return SerializeHelper.ReverseShort(m_value);
		}
	}

	//双字节无符号
	public class SIu16
	{
		public ushort m_value;

		public SIu16()
		{
			m_value = 0;
		}

		public SIu16(ushort left)
		{
			m_value = SerializeHelper.ReverseUshort(left);
		}

		//实现ushort到SIu16的隐式转换。c# 7.0才开始提供operator = 的重载
		public static implicit operator SIu16(ushort left)
		{
			return new SIu16(left);
		}

		//实现SIu16到ushort的隐式转换
		public static implicit operator ushort(SIu16 value)
		{
			return value.GetHostValue();
		}

		public ushort GetHostValue()
		{
			return SerializeHelper.ReverseUshort(m_value);
		}
	}

	//四字节有符号
	public class SIs32
	{
		public int m_value;

		public SIs32()
		{
			m_value = 0;
		}

		public SIs32(int left)
		{
			m_value = SerializeHelper.ReverseFourBytesData(left);
		}

		//实现int到SIs32的隐式转换。c# 7.0才开始提供operator = 的重载
		public static implicit operator SIs32(int left)
		{
			return new SIs32(left);
		}

		//实现SIs32到int的隐式转换
		public static implicit operator int(SIs32 value)
		{
			return value.GetHostValue();
		}

		public int GetHostValue()
		{
			return SerializeHelper.ReverseFourBytesData(m_value);
		}
	}

	//四字节无符号
	public class SIu32
	{
		public uint m_value;

		public SIu32()
		{
			m_value = 0;
		}

		public SIu32(uint left)
		{
			m_value = SerializeHelper.ReverseFourBytesData(left);
		}

		//实现int到SIu32的隐式转换。c# 7.0才开始提供operator = 的重载
		public static implicit operator SIu32(uint left)
		{
			return new SIu32(left);
		}

		//实现SIu32到int的隐式转换
		public static implicit operator uint(SIu32 value)
		{
			return value.GetHostValue();
		}

		public uint GetHostValue()
		{
			return SerializeHelper.ReverseFourBytesData(m_value);
		}
	}

	//无符号6字节
	public class SIu48
	{
		[MarshalAs(UnmanagedType.ByValArray)]
		public byte[] m_value;		//6个字节

		public SIu48()
		{
			m_value = new byte[6];
		}

		public SIu48(byte[] data)
		{
			m_value = new byte[6];
			if (null != data && data.Length > 0)
			{
				int len = data.Length > 6 ? 6 : data.Length;        //取最小值
				Buffer.BlockCopy(data, 0, m_value, 0, len);
			}
		}

		//索引器
		public byte this[int index]
		{
			get
			{
				if (index < 0 || index >= 6)
				{
					throw new IndexOutOfRangeException("index must be in [0,5]");
				}

				return m_value[index];
			}

			set
			{
				if (index >=0 && index < 6)
				{
					m_value[index] = value;
				}
			}
		}

		//byte[]隐式转换为SIu48
		public static implicit operator SIu48(byte[] data)
		{
			return new SIu48(data);
		}

		public string GetHexStrValue()
		{
			string value = "";
			for (int i = 0; i < m_value.Length; i++)
			{
				value += m_value[i].ToString("X2");
			}

			return value.ToUpper();
		}

		public ushort Len => 6;

	}

	//有符号8字节
	public class SIs64
	{
		public int m_value;    //实际上是s32处理方式
		public char[] pad;

		public SIs64()
		{
			m_value = 0;
			pad = new char[4];
		}

		public SIs64(int value)
		{
			pad = new char[4];
			m_value = SerializeHelper.ReverseFourBytesData(value);
		}

		public static implicit operator SIs64(int value)
		{
			return  new SIs64(value);
		}

		public int GetHostValue()
		{
			return SerializeHelper.ReverseFourBytesData(m_value);
		}
	}

	public class SIDOS_DATE_TIME2
	{
		public SIs32 dosdt_year;       /* 年 */
		public SIs32 dosdt_month;      /* 月 */
		public SIs32 dosdt_day;        /* 日 */
		public SIs32 dosdt_hour;       /* 时 */
		public SIs32 dosdt_minute;     /* 分 */
		public SIs32 dosdt_second;     /* 秒 */

		public string GetStrTime()
		{
			var year = dosdt_year.GetHostValue().ToString("{0:D4}");
			var month = dosdt_month.GetHostValue().ToString("{0:D2}");
			var day = dosdt_day.GetHostValue().ToString("{0:D2}");
			var hour = dosdt_hour.GetHostValue().ToString("{0:D2}");
			var min = dosdt_minute.GetHostValue().ToString("{0:D2}");
			var second = dosdt_second.GetHostValue().ToString("{0:D2}");
			var time = $"{year}-{month}-{day} {hour}:{min}:{second}";
			return time;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUtility
{
	public interface IASerialize
	{
		/// <summary>
		/// 将结构体序列化为字节数组。
		/// </summary>
		/// <param name="ret">保存序列化后的结果。</param>
		/// <param name="offset">保存结果的数组从什么位置开始写</param>
		/// <returns>返回往ret数组写入了多少字节数据。-1：字节数组剩余空间不够保存结构体数据</returns>
		int SerializeToBytes(ref byte[] ret, int offset = 0);

		/// <summary>
		/// 将字节数组中的内容反序列化为结构体
		/// </summary>
		/// <param name="bytes">保存原始数据的字节数组</param>
		/// <param name="offset">字节数组的偏移量</param>
		/// <returns>此次从字节数组中读出了多少字节数据。-1：字节数组数据长度小于结构体长度</returns>
		int DeserializeToStruct(byte[] bytes, int offset = 0);

		/// <summary>
		/// 计算结构体的占用内存，包括header长度
		/// </summary>
		/// <returns>结构体长度</returns>
		int Len { get; }

		/// <summary>
		/// 计算结构体的除了header外占用内存
		/// </summary>
		/// <returns>结构体除了head的长度</returns>
		ushort ContentLen { get; }
	}
}

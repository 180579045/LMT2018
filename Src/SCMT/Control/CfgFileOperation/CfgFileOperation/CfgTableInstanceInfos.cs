using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfgFileOperation
{
    /// <summary>
    /// 每个表实例化后的结构
    /// </summary>
    [Serializable]
    public class CfgTableInstanceInfos
    {
        string strInstantNum;//实例化的索引
        byte[] InstMem;//实例化后依次排列的内容

        public CfgTableInstanceInfos(string strInstantNumVal, byte[] InstMemVal)
        {
            strInstantNum = strInstantNumVal;
            InstMem = new byte[InstMemVal.Length];// Marshal.SizeOf(InstMemVal)];
            Buffer.BlockCopy(InstMemVal, 0, InstMem, 0, InstMemVal.Length);
        }
        public string GetInstantNum() { return strInstantNum; }
        public byte[] GetInstMem() { return InstMem; }
        /// <summary>
        /// 根据reclist 中指定值修改内存内容
        /// </summary>
        /// <param name="newMem"></param>
        public void ReclistSetInstMem(byte[] newMem)
        {
            Buffer.BlockCopy(newMem, 0, InstMem, 0, newMem.Length);
        }
        public object DeepCopy()
        {
            return new CfgTableInstanceInfos(this.strInstantNum, this.InstMem);
        }
    }
}

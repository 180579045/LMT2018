using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CfgFileOpStruct;
using System.Runtime.InteropServices;
using System.Data;

namespace CfgFileOperation
{
    /// <summary>
    /// mib 叶子节点
    /// </summary>
    class CfgFileLeafNodeOp
    {
        public StruCfgFileFieldInfo m_struFieldInfo;
        public StruMibNode m_struMibNode;
        
        /// <summary>
        /// 生成文件公共信息时使用
        /// </summary>
        /// <param name="leafRow"></param>
        /// <param name="buflen"></param>
        public CfgFileLeafNodeOp(DataRow leafRow, ushort buflen)
        {
            m_struFieldInfo = new StruCfgFileFieldInfo("init");
            m_struMibNode = new StruMibNode();

            m_struFieldInfo.SetAllParmsInfo(leafRow, buflen);
            m_struMibNode.SetAllParmsInfo(leafRow);// 叶子节点 StruMibNode m_struMibNode
        }
        /// <summary>
        /// 5g patch 写叶子头
        /// </summary>
        /// <param name="FieldInfo"></param>
        /// <param name="strNodeName"></param>
        public void SetLeafFieldConfigFlagPDG(bool bFind)
        {
            //如果节点本身是不可配置的即使被选中也是不可配置的，如果是可配置的节点在没有被选中的情况下
            //也是不可配置的。索引字段全部为可配置的。add by yangyuming
            if (true == bFind)
            {
                if (m_struFieldInfo.Getu8ConfigFlag() != (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_NOT_CONFIG_FILE)
                    m_struFieldInfo.Setu8ConfigFlag((byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_CONFIG_FILE);
            }
            else
            {
                if (m_struFieldInfo.Getu8ConfigFlag() == (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_CONFIG_FILE)
                    m_struFieldInfo.Setu8ConfigFlag((byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_NOT_CONFIG_FILE);
                else if (m_struFieldInfo.Getu8ConfigFlag() == (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_NOT_CONFIG_FILE
                    && m_struFieldInfo.u8FieldTag == 'Y')
                {
                    m_struFieldInfo.Setu8ConfigFlag((byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_CONFIG_FILE);
                }
            }

        }

        /// <summary>
        /// 把文件解析到内存结构时用
        /// </summary>
        /// <param name="struFieldInfo"></param>
        public CfgFileLeafNodeOp(StruCfgFileFieldInfo struFieldInfo) {
            m_struFieldInfo = (StruCfgFileFieldInfo)struFieldInfo.DeepCopy();
            m_struMibNode = new StruMibNode();
        }
        private CfgFileLeafNodeOp() { }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object DeepCopy()
        {
            CfgFileLeafNodeOp s = new CfgFileLeafNodeOp();
            s.m_struFieldInfo = (StruCfgFileFieldInfo)this.m_struFieldInfo.DeepCopy();
            s.m_struMibNode = (StruMibNode)this.m_struMibNode.DeepCopy();
            return s;
        }


    }

    /// <summary>
    /// 数据文件表字段信息
    /// sizeof : 60
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    struct StruCfgFileFieldInfo // OM_STRU_CfgFile_FieldInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        byte[] u8FieldName;                    /* [48] 字段名 */
        /// <summary>
        /// 字段相对记录头偏移量(表实例的内存中的偏移位置 m_cfgInsts)
        /// </summary>
        public ushort u16FieldOffset;          /* 字段相对记录头偏移量*/
        public ushort u16FieldLen;             /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
        public byte u8FieldType;               /* 字段类型 */
        /// <summary>
        /// 字段是否为关键字 : 是否为索引 'Y' or 'N'
        /// </summary>
        public byte u8FieldTag;                /* 字段是否为关键字 : 是否为索引 'Y' or 'N'*/
        public byte u8SaveTag;                 /* 字段是否需要存盘 */
        /// <summary>
        /// 字段是否可(需要)配置,0:不可配，1：可配
        /// </summary>
        byte u8ConfigFlag;                     /* 字段是否可(需要)配置,0:不可配，1：可配*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        byte[] u8Pad;                          /* [4] 保留*/
        /*********************        功能函数(结构转byte序等)      ***************************/
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="s"></param>
        public StruCfgFileFieldInfo(string s)
        {
            u8FieldName = new byte[48];    /* [48] 字段名 */
            u16FieldOffset = 0;            /* 字段相对记录头偏移量*/
            u16FieldLen = 0;               /* 字段长度 单位：字节 */
            u8FieldType = 0;               /* 字段类型 */
            u8FieldTag = 0;                /* 字段是否为关键字 */
            u8SaveTag = 0;                 /* 字段是否需要存盘 */
            u8ConfigFlag = 0;              /* 字段是否可(需要)配置,0:不可配，1：可配*/
            u8Pad = new byte[4];           /* [4] 保留*/
        }
        public void SetAllParmsInfo(DataRow leafRow, ushort buflen)
        {
            // 数据文件表字段信息 StruCfgFileFieldInfo
            Set_u8FieldName(leafRow["MIBName"].ToString()); // u8FieldName
            Setu8ConfigFlag((byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_CONFIG_FILE);
            u8SaveTag = Convert.ToByte('Y');
            u16FieldOffset = buflen;
            u8FieldTag = Convert.ToByte(((bool)leafRow["IsIndex"] == true) ? 'Y' : 'N');//是否为索引
            u8FieldType = Get_u8FieldType(leafRow["OMType"].ToString(), leafRow["MIB_Syntax"].ToString());// u8FieldType, u16FieldLen
            u16FieldLen = Get_u16FieldLen(leafRow["OMType"].ToString(), leafRow["MIBVal_AllList"].ToString(), leafRow["MIB_Syntax"].ToString());
        }
        /// <summary>
        /// 把 Struct DataHead 中的参数转化成byte[]串
        /// </summary>
        /// <returns></returns>
        public byte[] StruToByteArray()
        {
            byte[] byteArray = new byte[Marshal.SizeOf(new StruCfgFileFieldInfo("init"))];// DataHead 转 byte[]
            int bytePos = 0; //byteArray 写的位置, 依次往后写
            List<byte[]> byteAL = new List<byte[]>() { byteArray };
            List<int> bytePosL = new List<int>() { bytePos };
            // 按照顺序转换
            string leafName = Encoding.GetEncoding("GB2312").GetString(u8FieldName).TrimEnd('\0');

            SetValueToByteArray(byteAL, bytePosL, u8FieldName);
            SetValueToByteArray(byteAL, bytePosL, u16FieldOffset);
            SetValueToByteArray(byteAL, bytePosL, u16FieldLen);
            SetValueToByteArray(byteAL, bytePosL, u8FieldType);
            SetValueToByteArray(byteAL, bytePosL, u8FieldTag);
            SetValueToByteArray(byteAL, bytePosL, u8SaveTag);
            SetValueToByteArray(byteAL, bytePosL, u8ConfigFlag);
            SetValueToByteArray(byteAL, bytePosL, u8Pad);
            
            //string inAstr = BitConverter.ToString(byteAL[0]);
            //Console.WriteLine(String.Format("{0}:{1}\n", leafName, inAstr));
            return byteArray;
        }
        public byte[] StruToByteArrayReverse()
        {
            byte[] byteArray = new byte[Marshal.SizeOf(new StruCfgFileFieldInfo("init"))];// DataHead 转 byte[]
            int bytePos = 0; //byteArray 写的位置, 依次往后写
            List<byte[]> byteAL = new List<byte[]>() { byteArray };
            List<int> bytePosL = new List<int>() { bytePos };
            // 按照顺序转换
            SetValueToByteArray(byteAL, bytePosL, u8FieldName);
            SetValueToByteArrayReverse(byteAL, bytePosL, u16FieldOffset);
            SetValueToByteArrayReverse(byteAL, bytePosL, u16FieldLen);
            SetValueToByteArray(byteAL, bytePosL, u8FieldType);
            SetValueToByteArray(byteAL, bytePosL, u8FieldTag);
            SetValueToByteArray(byteAL, bytePosL, u8SaveTag);
            SetValueToByteArray(byteAL, bytePosL, u8ConfigFlag);
            SetValueToByteArray(byteAL, bytePosL, u8Pad);
            return byteArray;
        }
        /*********************        功能函数(变量赋值)             ***************************/
        /// <summary>
        /// byte[] u8TblName;// [32] 表名
        /// </summary>
        /// <param name="str"></param>
        public void Set_u8FieldName(string str)
        {
            if (u8FieldName == null)
                u8FieldName = new byte[48];
            StringToByteArray(str, u8FieldName);
        }
        public byte[] Getu8FieldName()
        {
            return u8FieldName;
        }
        public void Setu8ConfigFlag(byte setU8ConfigFlag)
        {
            u8ConfigFlag = setU8ConfigFlag;
        }
        public byte Getu8ConfigFlag()
        {
            return u8ConfigFlag;
        }
        /*********************        功能函数(私有)             ***************************/
        /// <summary>
        /// 把 string(Enlish, not have Chinese) 依次ASCII化后填写到byte[]中
        /// </summary>
        /// <param name="strEn">需要转化的字符串(字符串要求内容要求是英文、字母、数字，不能有中文)</param>
        /// <param name="byteParm">被赋值的变量参数</param>
        private void StringToByteArray(string strEn, byte[] byteParm)
        {
            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(strEn);// 转化成byte[]
            int strLen = (byteArray.Length < byteParm.Length) ? (byteArray.Length) : (byteParm.Length);// 转化长度以短的为主
            Array.Copy(byteArray, byteParm, strLen);// 转化
        }
        /// <summary>
        /// 把 各种type参数 转化成字节串byte[]
        /// </summary>
        /// <param name="byteAL">目的字节串的列表</param>
        /// <param name="bytePosL">目的字节串保存的位置</param>
        /// <param name="objParm">要保存的参数</param>
        private void SetValueToByteArray(List<byte[]> byteAL, List<int> bytePosL, object objParm)
        {
            if (objParm is byte)
            {
                byteAL[0][bytePosL[0]] = (byte)objParm;
                bytePosL[0] += Marshal.SizeOf(objParm);
            }
            else if (objParm is byte[])
            {
                Buffer.BlockCopy((byte[])objParm, 0, byteAL[0], bytePosL[0], ((byte[])objParm).Length);
                bytePosL[0] += ((byte[])objParm).Length;
            }
            else if (objParm is sbyte[])
            {
                Buffer.BlockCopy((sbyte[])objParm, 0, byteAL[0], bytePosL[0], ((sbyte[])objParm).Length);
                bytePosL[0] += ((sbyte[])objParm).Length;
            }
            else if (objParm is ushort)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((ushort)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((uint)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint[])
            {
                foreach (var ui in (uint[])objParm)
                {
                    byte[] TypeToByteArr = BitConverter.GetBytes((uint)ui); //  数据块起始位置 
                    Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                    bytePosL[0] += TypeToByteArr.Length;
                }
            }
            else
            {
                Console.WriteLine(String.Format("SetValueToByteArray : new type : value={0}, type={1}", objParm.ToString(), objParm.GetType()));
            }
        }
        private void SetValueToByteArrayReverse(List<byte[]> byteAL, List<int> bytePosL, object objParm)
        {
            if (objParm is byte)
            {
                byteAL[0][bytePosL[0]] = (byte)objParm;
                bytePosL[0] += Marshal.SizeOf(objParm);
            }
            else if (objParm is byte[])
            {
                Array.Reverse((byte[])objParm);
                Buffer.BlockCopy((byte[])objParm, 0, byteAL[0], bytePosL[0], ((byte[])objParm).Length);
                bytePosL[0] += ((byte[])objParm).Length;
            }
            else if (objParm is sbyte[])
            {
                Array.Reverse((sbyte[])objParm);
                Buffer.BlockCopy((sbyte[])objParm, 0, byteAL[0], bytePosL[0], ((sbyte[])objParm).Length);
                bytePosL[0] += ((sbyte[])objParm).Length;
            }
            else if (objParm is ushort)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((ushort)objParm); //  数据块起始位置 
                Array.Reverse((byte[])TypeToByteArr);
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((uint)objParm); //  数据块起始位置 
                Array.Reverse((byte[])TypeToByteArr);
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint[])
            {
                foreach (var ui in (uint[])objParm)
                {
                    byte[] TypeToByteArr = BitConverter.GetBytes((uint)ui); //  数据块起始位置 
                    Array.Reverse((byte[])TypeToByteArr);
                    Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                    bytePosL[0] += TypeToByteArr.Length;
                }
            }
            else
            {
                Console.WriteLine(String.Format("SetValueToByteArray : new type : value={0}, type={1}", objParm.ToString(), objParm.GetType()));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OmType"></param>
        /// <param name="valList"></param>
        /// <param name="strMibSyntax"></param>
        /// <returns></returns>
        public byte Get_u8FieldType(string OmType, string strMibSyntax)
        {
            Dictionary<string, byte> Recordtype = new Dictionary<string, byte>(){
                {"s32[]", (byte)MacroDefinition.DATATYPE.OM_OID_VALUE },
                {"u32", (byte)MacroDefinition.DATATYPE.OM_U32_VALUE },
                {"s32", (byte)MacroDefinition.DATATYPE.OM_S32_VALUE },
                {"enum", (byte)MacroDefinition.DATATYPE.OM_S32_VALUE },
                {"u8", (byte)MacroDefinition.DATATYPE.OM_U8_VALUE },
                {"s8", (byte)MacroDefinition.DATATYPE.OM_S8_VALUE },
                {"u16", (byte)MacroDefinition.DATATYPE.OM_U16_VALUE },
                {"s16", (byte)MacroDefinition.DATATYPE.OM_S16_VALUE },
                {"float", (byte)MacroDefinition.DATATYPE.OM_FLOAT_VALUE }};

            List<string> OmTypeList = new List<string>() { "s32[]", "u32", "s32", "enum", "u8", "s8", "u16", "s16", "float", };
            if (OmTypeList.Exists(e => e.Equals(OmType)))
            {
                return Recordtype[OmType];
            }
            else if (String.Equals(OmType, "u8[]"))
            {
                if (strMibSyntax.Contains("IPADDR"))
                {
                    return (byte)MacroDefinition.DATATYPE.OM_IP_ADDRESS_VALUE;
                }
                //添加新版的ipv4和ipv6de类型；MacAddress 和DataAndTime
                else if (strMibSyntax.Contains("INETADDRESS"))
                {
                    //IPV4和IPV6新版
                    return (byte)MacroDefinition.DATATYPE.OM_INETADDRESS_VALUE;
                }
                else if (strMibSyntax.Contains("MACADDRESS"))
                {
                    return (byte)MacroDefinition.DATATYPE.OM_MAC_ADDRESS_VALUE;
                }
                else if (strMibSyntax.Contains("MNCMCCTYPE"))
                {
                    return (byte)MacroDefinition.DATATYPE.OM_MNCMCC_VALUE;
                }
                else if (strMibSyntax.Contains("DATEANDTIME"))  //DataAndTime类型  
                {
                    return (byte)MacroDefinition.DATATYPE.OM_DATATIME_VALUE;
                }
                else
                {
                    return (byte)MacroDefinition.DATATYPE.OM_DATATIME_VALUE;
                }
            }
            else if (String.Equals(OmType, "s8[]"))
            {
                return (byte)MacroDefinition.DATATYPE.OM_EBUFFER_VALUE;
            }
            else if (String.Equals(OmType, "u32[]"))
            {
                return (byte)MacroDefinition.DATATYPE.OM_U32ARRAY_VALUE;
            }
            else      //无效的类型
            {
                return (byte)MacroDefinition.DATATYPE.OM_INVALID_VALUE;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OmType"></param>
        /// <param name="valList"></param>
        /// <param name="strMibSyntax"></param>
        public ushort Get_u16FieldLen(string OmType, string valList, string strMibSyntax)
        {
            Dictionary<string, ushort> iLeaftypeSize = new Dictionary<string, ushort>(){
                {"s32[]", (ushort)Marshal.SizeOf(new OM_OBJ_ID_T()) },
                {"u32", (ushort)Marshal.SizeOf(new uint())},
                {"s32", (ushort)Marshal.SizeOf(new int())},
                {"enum", (ushort)Marshal.SizeOf(new int())},
                {"u8", (ushort)Marshal.SizeOf(new byte())},
                {"s8", (ushort)Marshal.SizeOf(new sbyte())},
                {"u16", (ushort)Marshal.SizeOf(new ushort())},
                {"s16", (ushort)Marshal.SizeOf(new short())},
                {"float", (ushort)Marshal.SizeOf(new float())}};

            List<string> OmTypeList = new List<string>() { "s32[]", "u32", "s32", "enum", "u8", "s8", "u16", "s16", "float", };
            if (OmTypeList.Exists(e => e.Equals(OmType)))
            {
                return iLeaftypeSize[OmType];
            }
            else if (String.Equals(OmType, "u8[]"))
            {
                if (strMibSyntax.Contains("IPADDR"))
                {
                    return (ushort)Marshal.SizeOf(new uint());
                }
                //添加新版的ipv4和ipv6de类型；MacAddress 和DataAndTime
                else if (strMibSyntax.Contains("INETADDRESS"))
                {
                    //IPV4和IPV6新版
                    return (ushort)GetMaxStrLen(valList, OmType);
                }
                else if (strMibSyntax.Contains("MACADDRESS"))
                {
                    return (ushort)GetMaxStrLen(valList, OmType); ;
                }
                else if (strMibSyntax.Contains("MNCMCCTYPE"))
                {
                    return (ushort)GetMaxStrLen(valList, OmType); ;
                }
                else if (strMibSyntax.Contains("DATEANDTIME"))  //DataAndTime类型  
                {
                    return (ushort)GetMaxStrLen(valList, OmType); ;
                }
                else
                {
                    return (ushort)GetMaxStrLen(valList, OmType); ;
                }
            }
            else if (String.Equals(OmType, "s8[]"))
            {
                return (ushort)GetMaxStrLen(valList, OmType);
            }
            else if (String.Equals(OmType, "u32[]"))
            {
                int len = GetMaxStrLen(valList, OmType);
                return (ushort)(4 * (len + 1));
            }
            else      //无效的类型
            {
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ValAllList"></param>
        /// <param name="OMType"></param>
        /// <returns></returns>
        public int GetMaxStrLen(string ValAllList, string OMType)
        {
            List<string> OmTypeList = new List<string>() { "s8[]", "u8[]", "u32[]" };
            if (OmTypeList.Exists(e => e.Equals(OMType)))
            {
                string[] sArray = ValAllList.Split('-');//字符串类型标识方式“n1-n2”,n2标识该字符串最大长度
                string strMaxValue = sArray[1];//n2
                if (String.Equals(OMType, "s8[]"))
                    return int.Parse(strMaxValue) + 1;
                else
                    return int.Parse(strMaxValue);
            }
            else
                return 0;
        }

        public int GetDeckNum(string strSrc, string strSeparator)
        {
            int nNum = 0;
            strSrc = strSrc.Trim(' ');
            if (strSrc == "")
            {
                return nNum;
            }
            //计算
            int ipos = strSrc.IndexOf(strSeparator);
            if (ipos > 0)
            {
                while (ipos > 0)
                {
                    nNum = nNum + 1;
                    strSrc = strSrc.Substring(ipos + 1);
                    ipos = strSrc.IndexOf(strSeparator);
                }
                nNum = nNum + 1;
            }
            else
            {
                //就一个
                nNum = 1;
            }
            return nNum;
        }

        public int GetBITSNum(int deckNum)
        {
            int maxNum;
            if (deckNum == 1)
            {
                maxNum = 2;
            }
            else
            {
                maxNum = 2 * GetBITSNum(deckNum - 1);
            }
            return maxNum;
        }

        //
        public void SetValueByBytes(byte[] data)
        {
            //u8FieldName = new byte[48];    /* [48] 字段名 */
            //u16FieldOffset = 0;            /* 字段相对记录头偏移量*/
            //u16FieldLen = 0;               /* 字段长度 单位：字节 */
            //u8FieldType = 0;               /* 字段类型 */
            //u8FieldTag = 0;                /* 字段是否为关键字 */
            //u8SaveTag = 0;                 /* 字段是否需要存盘 */
            //u8ConfigFlag = 0;              /* 字段是否可(需要)配置,0:不可配，1：可配*/
            u8Pad = new byte[4];           /* [4] 保留*/

            int fromOf = 0;
            int lenSize = 0;
            byte[] bytes;
            //u8FieldName
            fromOf = 0;
            lenSize = Marshal.SizeOf(new byte()) * 48;
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u8FieldName = GetBytesValue(bytes);
            string leafName = Encoding.GetEncoding("GB2312").GetString(u8FieldName).TrimEnd('\0');
            //u16FieldOffset
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(u16FieldOffset);
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u16FieldOffset = GetBytesValToU16(bytes);
            //u16FieldLen
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(u16FieldLen);
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u16FieldLen = GetBytesValToU16(bytes);
            //u8FieldType
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new byte());
            u8FieldType = data[fromOf];
            //u8FieldTag
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new byte());
            u8FieldTag = data[fromOf];
            //u8SaveTag
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new byte());
            u8SaveTag = data[fromOf];
            //u8ConfigFlag
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new byte());
            u8ConfigFlag = data[fromOf];
        }
        public void SetValueByBytesByBigEndian(byte[] data)
        {
            u8Pad = new byte[4];           /* [4] 保留*/

            int fromOf = 0;
            int lenSize = 0;
            byte[] bytes;
            //u8FieldName
            fromOf = 0;
            lenSize = Marshal.SizeOf(new byte()) * 48;
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u8FieldName = GetBytesValue(bytes);
            string leafName = Encoding.GetEncoding("GB2312").GetString(u8FieldName).TrimEnd('\0');
            //u16FieldOffset
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(u16FieldOffset);
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u16FieldOffset = GetBytesValToU16(bytes);
            //u16FieldLen
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(u16FieldLen);
            bytes = data.Skip(fromOf).Take(lenSize).ToArray();
            u16FieldLen = GetBytesValToU16(bytes);
            //u8FieldType
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new byte());
            u8FieldType = data[fromOf];
            //u8FieldTag
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new byte());
            u8FieldTag = data[fromOf];
            //u8SaveTag
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new byte());
            u8SaveTag = data[fromOf];
            //u8ConfigFlag
            fromOf += lenSize;
            lenSize = Marshal.SizeOf(new byte());
            u8ConfigFlag = data[fromOf];
        }
        uint GetBytesValToUint(byte[] bytes)
        {
            Array.Reverse((byte[])bytes);
            string reStr = OxbytesToString(bytes);
            return Convert.ToUInt32(OxbytesToString(bytes), 16);
        }
        ushort GetBytesValToU16(byte[] bytes)
        {
            Array.Reverse((byte[])bytes);
            string reStr = OxbytesToString(bytes);
            return Convert.ToUInt16(OxbytesToString(bytes), 16);
        }
        string OxbytesToString(byte[] bytes)
        {
            string hexString = string.Empty;
            Array.Reverse((byte[])bytes);
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        byte[] GetBytesValueRev(byte[] bytes)
        {
            Array.Reverse((byte[])bytes);
            return bytes;
        }
        byte[] GetBytesValue(byte[] bytes)
        {
            return bytes;
        }

        public object DeepCopy()
        {
            StruCfgFileFieldInfo s = new StruCfgFileFieldInfo("init");
            s.u16FieldOffset = this.u16FieldOffset;
            s.u16FieldLen = this.u16FieldLen;
            s.u8FieldType = this.u8FieldType;
            s.u8FieldTag = this.u8FieldTag;
            s.u8SaveTag = this.u8SaveTag;
            s.u8ConfigFlag = this.u8ConfigFlag;
            Array.Copy(this.u8FieldName, s.u8FieldName,  this.u8FieldName.Length);
            return s;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct StruMibNode
    {
        ///////////////////////////////////////////////
        public string strMibName;//MibTree的MibName
        public string strMibDefValue;//Mib的默认值
        public string strMIBVal_AllList;//
        public string strMibSyntax;
        public string strChFriendName; //2009-11-24 张新发添加，中文友好名
        public string strOID;//MibTree的OID
        public string strOMType;//OMType字段2010-03-26 by cuidairui 
        public string strMibDesc;//对应MibTree节点的MIBDesc字段的'('左侧的部分

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public void SetAllParmsInfo(DataRow leafRow)
        {
            strMibName = leafRow["MIBName"].ToString(); 
            strMibDefValue = leafRow["DefaultValue"].ToString(); 
            strMIBVal_AllList = leafRow["MIBVal_AllList"].ToString(); 
            strMibSyntax = leafRow["ASNType"].ToString(); 
            strChFriendName = leafRow["ChFriendName"].ToString();//中文名称;
            strOID = leafRow["OID"].ToString(); 
            strOMType = leafRow["OMType"].ToString(); 
            strMibDesc = leafRow["MIBDesc"].ToString();//描述;
            //bool isIndex = (bool)leafRow["IsIndex"]; //是否为索引??????? col=39
            //u32InstNum = 0;
            //enumMibValueType = MacroDefinition.ENUM_MIBVALUETYPE.MIBVALUETYPE_UNKNOWN;
            //bIsBitSegParent = false;
            //u32MemSize = 0;
            //u8BitSegStartOffset = 0;
            //nBasicDataTypeLen = 0;
            //nIndexNum = 0;
            //nodeType = MacroDefinition.MibNodeType.NormalNode;
            //excelLine = 0;
            //nIsCustomNode = 0;
            //m_bAlterReportNode = true;
            //m_bIsTrigger = false;
        }

        public object DeepCopy()
        {
            StruMibNode s = new StruMibNode();
            s.strMibName = this.strMibName;//MibTree的MibName
            s.strMibDefValue = this.strMibDefValue;//Mib的默认值
            s.strMIBVal_AllList = this.strMIBVal_AllList;//
            s.strMibSyntax = this.strMibSyntax;
            s.strChFriendName = this.strChFriendName; //2009-11-24 张新发添加，中文友好名
            s.strOID = this.strOID;//MibTree的OID
            s.strOMType = this.strOMType;//OMType字段2010-03-26 by cuidairui 
            s.strMibDesc = this.strMibDesc;//对应MibTree节点的MIBDesc字段的'('左侧的部分
            return s;
        }
        /// <summary>
        /// 
        /// </summary>
        //MacroDefinition.USERPRIVILEGE m_enumMibVisibleLevel;//该Mib节点的权限，对应MibTree的ICFWritAble字段
        //uint u32InstNum;//MibTree节点的InstanceNum字段，如果值>1，则说明该节点是数组---add by cuidairui 2009--08-04
        //MacroDefinition.ENUM_MIBVALUETYPE enumMibValueType;//对应MibTree节点的MIB_Syntax字段，表示该节点的参数类型---add by yuxiaowei 2009--08-03

        //bool bIsBitSegParent;//对应MibTree节点的IsBitsegParent字段，标识是不是位段的父节点---add by cuidairui 2009--08-04
        //int nBasicDataTypeLen;//对数组等有用，表示基本数据元的类型，根据OMType获取 //于晓伟 2009-11-09添加
        //long u32MemSize;//对应MibTree节点的MemSize字段，标识该节点所占内存的大小（以bit为单位）--add by cuidairui 2009-08-04
        //byte u8BitSegStartOffset;//对应MibTree节点的BitSegStartOffset字段，如果是位段的话，标识位段在父节点中的起始位置--add by cuidairui 2009-08-04




        ////在参数设置对话框中用户设置的值，其初始值应该为strMibDefValue，作为节点初始值
        //string strSelectedValue;

        //string strMIBVal_List;//MibTree节点的取值范围

        //string strParentOID;//MibTree节点的父节点的OID

        //string strMibTotalDesc;//对应MibTree节点的MIBDesc字段------add by cuidairui 2009-08-06
        //string strTableName;//本节点所属的表名
        //string strEqualObj;//等同对象
        //string strDispMode;//显示模型
        //string strDispTerm;//显示条件
        //string strDispTermSubTo;//显示条件的归属，即对象的显示依据哪个节点的值判断
        //string strDispSort;//分类指示
        //string strDistinctObj;//互斥对象

        //string[] strIndexOID;//[MacroDefinition.MAX_INDEXCOUNT];
        //int nIndexNum;
        //MacroDefinition.MibNodeType nodeType;
        //string strUnit;
        //string strMMLName;//MMLName字段add by cuidairui 2009-10-30


        //bool bIsMib;//是否是Mib节点(对应着MibTree中IsMIB字段)
        //string strMibWriteAble;//Mib节点读写权限(对应着MibTree的ICFWriteAble列)

        ////! fangming add 20100813 for 节点排序-------------------------------->
        //int excelLine;
        ////! fangming add 20100813 for 节点排序<--------------------------------

        ////! fangming add for lmt自定义的节点  20101111 for 节点排序------------>
        //int nIsCustomNode;// 0 为 Mib表节点， 其它为自定义节点
        //                  //! fangming add for lmt自定义的节点  20101111 for 节点排序<------------

        ////! fangming add for lmt变更节点 20110214------------------------------>
        //bool m_bAlterReportNode;
        ////! fangming add for lmt变更节点 20110214<------------------------------

        ////! fangming add for 触发类开关----------------------------------------->
        //bool m_bIsTrigger;
        ////! fangming add for 触发类开关<-----------------------------------------

        //string chDetailDes;

        ////wangyun1 2013-7-18
        //string strManagerWriteAble;
    };


    
}

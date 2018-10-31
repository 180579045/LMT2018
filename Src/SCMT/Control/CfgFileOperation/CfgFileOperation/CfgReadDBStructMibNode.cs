using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CfgFileOperation
{
    class CfgReadDBStructMibNode
    {
        public StruMibNode pTempNode = new StruMibNode();
        public string strTableName = "";
        public bool bIsMib = false;//是否是Mib节点(对应着MibTree中IsMIB字段)
        public string strMibWriteAble = "";//Mib节点读写权限(对应着MibTree的ICFWriteAble列)
        ////wangyun1 2013-7-18
        public string strManagerWriteAble = "";

        public long u32MemSize = 0;//对应MibTree节点的MemSize字段，标识该节点所占内存的大小（以bit为单位）--add by cuidairui 2009-08-04

        ////! fangming add 20100813 for 节点排序-------------------------------->
        public int excelLine = 0;
        ////! fangming add 20100813 for 节点排序<--------------------------------

        ////! fangming add for lmt变更节点 20110214------------------------------>
        public bool m_bAlterReportNode = false;
        ////! fangming add for lmt变更节点 20110214<------------------------------

        ////! fangming add for 触发类开关----------------------------------------->
        public bool m_bIsTrigger = false;
        ////! fangming add for 触发类开关<-----------------------------------------

        public MibNodeType nodeType = MibNodeType.Unknown; //IndexNode = 0,RowStatusNode = 1,NormalNode=2,

        public string strUnit = "";
        public string strMMLName = "";//MMLName字段add by cuidairui 2009-10-30

        public string strMibTotalDesc = "";//对应MibTree节点的MIBDesc字段------add by cuidairui 2009-08-06

        public ENUM_MIBVALUETYPE enumMibValueType = ENUM_MIBVALUETYPE.MIBVALUETYPE_UNKNOWN;//对应MibTree节点的MIB_Syntax字段，表示该节点的参数类型---add by yuxiaowei 2009--08-03

        public string strMIBVal_List = "";//MibTree节点的取值范围

        public string strParentOID = "";//MibTree节点的父节点的OID

        public  uint u32InstNum = 0;//MibTree节点的InstanceNum字段，如果值>1，则说明该节点是数组---add by cuidairui 2009--08-04

        public byte u8BitSegStartOffset = (byte)0;//对应MibTree节点的BitSegStartOffset字段，如果是位段的话，标识位段在父节点中的起始位置--add by cuidairui 2009-08-04

        public string[] strIndexOID = new string[6];//[MacroDefinition.MAX_INDEXCOUNT];

        public int nIndexNum = 0;//2014-2-12 luoxin 当前表索引个数

        public USERPRIVILEGE m_enumMibVisibleLevel = USERPRIVILEGE.USERPRIVILEGE_UNKNOWN;//该Mib节点的权限，对应MibTree的ICFWritAble字段

        public string chDetailDes = "";

        private Dictionary<string, ENUM_MIBVALUETYPE> m_mapSynTax2Type = null;

        private void initm_mapSynTax2Type()
        {
            m_mapSynTax2Type = new Dictionary<string, ENUM_MIBVALUETYPE>(){
            { "LONG", ENUM_MIBVALUETYPE.MIBVALUETYPE_LONG},
            {"UINT32", ENUM_MIBVALUETYPE.MIBVALUETYPE_UINT32},
            {"STRING", ENUM_MIBVALUETYPE.MIBVALUETYPE_STRING},
            {"IPADDR", ENUM_MIBVALUETYPE.MIBVALUETYPE_IPADDR},
            {"ARRAY", ENUM_MIBVALUETYPE.MIBVALUETYPE_ARRAY},
            {"MOI", ENUM_MIBVALUETYPE.MIBVALUETYPE_MOI},
            {"MOC", ENUM_MIBVALUETYPE.MIBVALUETYPE_MOC},
            {"ENUM", ENUM_MIBVALUETYPE.MIBVALUETYPE_ENUM},
            {"MACADDR", ENUM_MIBVALUETYPE.MIBVALUETYPE_MACADDR},
            {"DATETIME", ENUM_MIBVALUETYPE.MIBVALUETYPE_DATETIME},//liyang added at 2009-10-30
            {"DATEANDTIME", ENUM_MIBVALUETYPE.MIBVALUETYPE_DATETIME},//xiejing added at 2010-08-16, 在数据库里看到类型都变成了DATEANDTIME
            {"OCTETS", ENUM_MIBVALUETYPE.MIBVALUETYPE_STRING},//liyang added at 2009-10-30

            /*add by cuidairui 2010-01-08*/
            {"IPV4", ENUM_MIBVALUETYPE.MIBVALUETYPE_IPV4},
            {"IPV6", ENUM_MIBVALUETYPE.MIBVALUETYPE_IPV6},
            {"RETURNCODE", ENUM_MIBVALUETYPE.MIBVALUETYPE_RETURNCODE},
            {"SOFTVER", ENUM_MIBVALUETYPE.MIBVALUETYPE_SOFTVER},
            {"SEQID", ENUM_MIBVALUETYPE.MIBVALUETYPE_SEQID},
            {"SHCMDPARA", ENUM_MIBVALUETYPE.MIBVALUETYPE_SHCMDPARA},
            {"BITS", ENUM_MIBVALUETYPE.MIBVALUETYPE_BITS},
            /*end by cuidairui 2010-01-08*/

            /*add by cuidairui 2010-05-24*/
            {"IMSI", ENUM_MIBVALUETYPE.MIBVALUETYPE_IMSI},
            {"TMSI(PTMSI)", ENUM_MIBVALUETYPE.MIBVALUETYPE_TMSI},
            {"IMEI", ENUM_MIBVALUETYPE.MIBVALUETYPE_IMEI},
            {"IMSISTRING", ENUM_MIBVALUETYPE.MIBVALUETYPE_IMSISTRING},
            {"MNCSTRING", ENUM_MIBVALUETYPE.MIBVALUETYPE_MNCSTRING},
            {"MCCSTRING", ENUM_MIBVALUETYPE.MIBVALUETYPE_MCCSTRING},
            /*end by cuidairui 2010-05-24*/

            /*add by wangshengfu 2010-06-21*/
            {"DATE", ENUM_MIBVALUETYPE.MIBVALUETYPE_DATE},
            {"TIME", ENUM_MIBVALUETYPE.MIBVALUETYPE_TIME},
        };
        }

        public CfgReadDBStructMibNode(DataRow row, Dictionary<string, CfgReadDBStructMibNode> pMapMibNodeByName, Dictionary<string, CfgReadDBStructMibNode> pMapMibNodeByOID)
        {
            //DataRow row = MibdateSet.Tables[0].Rows[loop];

            //CfgReadDBStructMibNode pTempNode = new CfgReadDBStructMibNode(row, pMapMibNodeByName, pMapMibNodeByOID);//StruMibNode pTempNode = new StruMibNode();
            initm_mapSynTax2Type();

            ////Add by xiejing, 如果是叶子节点的话, 填充strTableName字段
            if (String.Compare("true", row["IsLeaf"].ToString(), true) == 0)//不区分大小写比较
            {
                strTableName = GetMibNamebyOidFromCache(pMapMibNodeByOID, row["ParentOID"].ToString());
            }

            //Add by xiejing, 添加bIsMib字段的填写
            bIsMib = GetIsMIBFromRow(row["IsMIB"].ToString());

            //Add by xiejing, 添加strMibWriteAble字段
            strMibWriteAble = row["ICFWriteAble"].ToString();
            //Add by xiejing

            //wangyun1 2013-7-18 
            string strManagerWriteAble = row["ManagerWriteAble"].ToString();//rs.GetValueString(strManagerWriteAble, "ManagerWriteAble");

            //strInstanceNum = row["InstanceNum"].ToString();//rs.GetValueString(strInstanceNum, "InstanceNum");
            //strMibSyntax = row["MIB_Syntax"].ToString();//rs.GetValueString(strMibSyntax, "MIB_Syntax");
            //csDefValue = row["DefaultValue"].ToString();//rs.GetValueString(csDefValue, "DefaultValue");

            //?//rs.GetCollect("MemSize", pTempNode->u32MemSize);
            //string MemSize = row["MemSize"].ToString();
            pTempNode.strChFriendName = row["ChFriendName"].ToString();

            string ExcelLine = row["ExcelLine"].ToString();
            excelLine = int.Parse(ExcelLine);

            //! fangming add for lmt变更节点 20110214------------------------------>
            m_bAlterReportNode = GetpIsAlterReport(row);
            //! fangming add for lmt变更节点 20110214<------------------------------

            //! fangming add for 触发类开关----------------------------------------->
            m_bIsTrigger = GetIsTrigger(row);
            //! fangming add for 触发类开关<-----------------------------------------
            pTempNode.strOMType = row["OMType"].ToString();
            pTempNode.strMibSyntax = row["MIB_Syntax"].ToString();


            //string strAsnType = row["ASNType"].ToString();//rs.GetValueString(strAsnType, "ASNType");
            nodeType = GetNodeType(row);

            strUnit = row["MIBVal_Unit"].ToString();//rs.GetValueString(pTempNode->strUnit, "MIBVal_Unit");
                                                    //2009-11-09于晓伟添加
            int nBasicDataTypeLen = GetBasicDataTypeLen(row);

            strMMLName = GetMMLName(row);

            // strMibDesc
            strMibTotalDesc = row["MIBDesc"].ToString();
            pTempNode.strMibDesc = GetMibDesc(row);


            //// strMibSyntax
            enumMibValueType = GetEnumMibValueType(row);

            ////对defaultvalue字段进行处理
            pTempNode.strMibDefValue = GetNodeDefaultValue(row["DefaultValue"].ToString(), enumMibValueType);
            strMIBVal_List = row["MIBVal_List"].ToString();
            pTempNode.strMIBVal_AllList = row["MIBVal_AllList"].ToString();
            pTempNode.strOID = row["OID"].ToString();
            pTempNode.strMibName = row["MIBName"].ToString();
            strParentOID = row["ParentOID"].ToString();
            u32InstNum = uint.Parse(row["InstanceNum"].ToString());
            //u8BitSegStartOffset = atoi((LPCTSTR)strBitSegStartOffset);

            int MAX_INDEXCOUNT = 6;  //MIB的最大维数
            for (int i = 1; i <= MAX_INDEXCOUNT; i++)
            {
                string strIndexOid = row[String.Format("Index{0}OID", i)].ToString();//rs.GetValueString(strIndexOid, strIndexOID);
                if (strIndexOid != "")
                {
                    strIndexOID[i - 1] = strIndexOid;
                    nIndexNum++;
                    if (strIndexOid == row["OID"].ToString())
                    {
                        nodeType = MibNodeType.IndexNode;
                    }
                }
            }

            //wangyun1 2013-2-22 增加详细描述用来填写到添加框体的下方 mode-------------------------------------->
            chDetailDes = GetChDetailDes(row);
            //wangyun1 2013-2-22 增加详细描述用来填写到添加框体的下方 mode<--------------------------------------

            //于晓伟 添加，获取该节点的可见级别
            m_enumMibVisibleLevel = GetEnumMibVisibleLevel(row);
            
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="pMapMibNodeByOID"></param>
        /// <param name="findOid"></param>
        /// <returns></returns>
        string GetMibNamebyOidFromCache(Dictionary<string, CfgReadDBStructMibNode> pMapMibNodeByOID, string findOid)
        {
            if (null == pMapMibNodeByOID)
            {
                return "";
            }

            if (!pMapMibNodeByOID.ContainsKey(findOid))//不存在
            {
                return "";
            }
            return pMapMibNodeByOID[findOid].pTempNode.strMibName;
        }

        bool GetIsMIBFromRow(string strIsMib)
        {
            if (String.Empty == strIsMib)
                return false;

            if ("√" == strIsMib)
                return true;
            else
                return false;
        }

        bool GetpIsAlterReport(DataRow row)
        {
            string strAlterReport = row["IsAlterReport"].ToString();
            if (String.Compare("FALSE", strAlterReport, true) == 0)//不区分大小写比较
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        bool GetIsTrigger(DataRow row)
        {
            string strIsTrigger = row["IsTriggerParameter"].ToString();
            if (String.Compare(strIsTrigger, "FALSE", true) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        MibNodeType GetNodeType(DataRow row)
        {
            string strAsnType = row["ASNType"].ToString();//rs.GetValueString(strAsnType, "ASNType");
            if (String.Compare(strAsnType, "RowStatus", true) == 0)
            {
                return MibNodeType.RowStatusNode;//IndexNode = 0,RowStatusNode = 1,NormalNode=2,
            }
            return MibNodeType.Unknown;
        }
        int GetBasicDataTypeLen(DataRow row)
        {
            // 去 空格 ' ', /, [后面的，

            string csBasicDataType = row["OMType"].ToString();
            string dataType = csBasicDataType;
            if (dataType.Trim(' ') == string.Empty)
                return 0;
            else if (dataType.Trim('/') == string.Empty)
                return 0;
            else if (-1 != dataType.IndexOfAny("enum".ToArray()))
            {
                return 0;
            }
            else
            {
                int pos = dataType.IndexOf("[");
                if (-1 != pos)
                {
                    dataType = dataType.Substring(0, pos);
                }
                else
                {

                }

                dataType = dataType.Substring(1);
                return int.Parse(dataType);
            }


            //csBasicDataType = csBasicDataType.Substring(0, 3);// csBasicDataType.Left(3);
            //csBasicDataType = csBasicDataType.Trim("[: ".ToCharArray());// csBasicDataType.Trim("[: ");
            //csBasicDataType = csBasicDataType.Substring(1);// csBasicDataType.Mid(1);
            //int nBasicDataTypeLen = int.Parse(csBasicDataType);//pTempNode->nBasicDataTypeLen = atoi(csBasicDataType);
            //return nBasicDataTypeLen;
        }
        string GetMMLName(DataRow row)
        {
            // ??? strMMLName 没有赋值的地方。。。
            //string strMMLName = strMMLName.Trim(' ');// strMMLName.Trim(' ');
            //if (strMMLName.IsEmpty())
            //{
            //    //L_ERROR("MibTree表中MibName = %s 对应的MMLName为空,将用MibName代替MMLName");
            //    pTempNode->strMMLName = strMibName;
            //}
            //else
            //{
            //    pTempNode->strMMLName = strMMLName;
            //}
            return row["MMLName"].ToString();
        }

        string GetMibDesc(DataRow row)
        {
            string strMibDesc = row["MIBDesc"].ToString();
            int nPos = strMibDesc.IndexOf('(');
            if (-1 != nPos)
            {
                return strMibDesc.Substring(0, nPos); //pTempNode->strMibDesc = strMibDesc.Left(nPos);
            }
            else
            {
                return strMibDesc;
            }
        }


        ENUM_MIBVALUETYPE GetEnumMibValueType(DataRow row)
        {
            string strMibSyntax = row["MIB_Syntax"].ToString();
            if (!m_mapSynTax2Type.ContainsKey(strMibSyntax))//不存在
            {
                return ENUM_MIBVALUETYPE.MIBVALUETYPE_UNKNOWN;
            }
            else
            {
                return m_mapSynTax2Type[strMibSyntax];
            }

            //iter = m_mapSynTax2Type.find(strMibSyntax);
            //if (iter != m_mapSynTax2Type.end())
            //{
            //    pTempNode->enumMibValueType = iter->second;
            //    emMibValueType = iter->second;
            //}
            //else
            //{
            //    pTempNode->enumMibValueType = MIBVALUETYPE_UNKNOWN;
            //    emMibValueType = MIBVALUETYPE_UNKNOWN;
            //}
        }

        string GetNodeDefaultValue(string strDefaultValue, ENUM_MIBVALUETYPE emMibValueType)
        {
            //L_AUTRACE("1");//这里应注释掉，该函数调用次数太多，如果打印到log的话，会严重影响效率,cuidairui 2009-10-19
            string strResDefValue = strDefaultValue;
            switch (emMibValueType)
            {
                case ENUM_MIBVALUETYPE.MIBVALUETYPE_STRING:
                    {
                        strResDefValue.Trim('"');
                        break;
                    }
                case ENUM_MIBVALUETYPE.MIBVALUETYPE_ENUM:
                    {
                        int nPos = strResDefValue.IndexOf(":");
                        if (-1 != nPos)
                        {
                            strResDefValue = strResDefValue.Substring(0, nPos);
                        }
                        break;
                    }
                case ENUM_MIBVALUETYPE.MIBVALUETYPE_DATETIME:
                    {
                        strResDefValue.Trim('"');
                    }
                    break;
                case ENUM_MIBVALUETYPE.MIBVALUETYPE_LONG:
                    {
                        int nPos = strResDefValue.IndexOf(":");
                        if (-1 != nPos)
                        {
                            strResDefValue = strResDefValue.Substring(0, nPos);
                        }
                    }
                    break;
                case ENUM_MIBVALUETYPE.MIBVALUETYPE_UINT32:
                case ENUM_MIBVALUETYPE.MIBVALUETYPE_IPADDR:
                case ENUM_MIBVALUETYPE.MIBVALUETYPE_MACADDR:
                case ENUM_MIBVALUETYPE.MIBVALUETYPE_ARRAY:
                case ENUM_MIBVALUETYPE.MIBVALUETYPE_MOI:
                case ENUM_MIBVALUETYPE.MIBVALUETYPE_MOC:
                case ENUM_MIBVALUETYPE.MIBVALUETYPE_UNKNOWN: break;
                default: break;
            }
            return strResDefValue;
        }

        string GetChDetailDes(DataRow row)
        {
            string chDetailDes = row["ChDetailDesc"].ToString();
            if (String.Empty != chDetailDes)
            {
                chDetailDes = "";
            }
            return chDetailDes;
        }


        USERPRIVILEGE GetEnumMibVisibleLevel(DataRow row)
        {
            string nVisibleLevel = row["ICFWriteAble"].ToString();
            if (String.Empty != nVisibleLevel)//if (!rs.GetCollect("ICFWriteAble", nVisibleLevel))
            {
                return USERPRIVILEGE.USERPRIVILEGE_UNKNOWN;//如果没有找到该节点，则默认为最低级别也能看到
            }
            else
                return (USERPRIVILEGE)int.Parse(nVisibleLevel);
        }

    }
    enum ENUM_MIBVALUETYPE
    {
        MIBVALUETYPE_UNKNOWN = -1,
        MIBVALUETYPE_LONG,
        MIBVALUETYPE_UINT32,
        MIBVALUETYPE_STRING,
        MIBVALUETYPE_IPADDR,
        MIBVALUETYPE_MACADDR,
        MIBVALUETYPE_ARRAY,
        MIBVALUETYPE_MOI,
        MIBVALUETYPE_MOC,
        MIBVALUETYPE_ENUM,
        MIBVALUETYPE_DATETIME,
        MIBVALUETYPE_IPV4,
        MIBVALUETYPE_IPV6,
        MIBVALUETYPE_SOFTVER,
        MIBVALUETYPE_RETURNCODE,
        MIBVALUETYPE_SEQID,
        MIBVALUETYPE_SHCMDPARA,
        MIBVALUETYPE_BITS,
        MIBVALUETYPE_IMSI,
        MIBVALUETYPE_TMSI,
        MIBVALUETYPE_IMEI,
        MIBVALUETYPE_IMSISTRING,
        MIBVALUETYPE_MNCSTRING,
        MIBVALUETYPE_MCCSTRING,
        //wangshengfu 2010.06.21
        MIBVALUETYPE_DATE,
        MIBVALUETYPE_TIME
        //end of wangshengfu
    };

    enum MibNodeType
    {
        Unknown = -1,
        IndexNode,
        RowStatusNode,
        NormalNode,
    };

    //用户权限, 与LMTPrivilege表的'PrivilegeID'字段一一对应
    enum USERPRIVILEGE
    {
        USERPRIVILEGE_MODULE_DEVELOPER = 1,//模块权限上的开发人员权限
        USERPRIVILEGE_MODULE_TEST = 2,//模块权限上的测试权限
        USERPRIVILEGE_MODULE_ENGINEER = 3,//模块权限上的工程人员权限
        USERPRIVILEGE_MODULE_RESTRICT = 4, //受限权限
        USERPRIVILEGE_ELEM_ENB = 5,//网元权限上的ENB操作人员权限
        USERPRIVILEGE_ELEM_EPS = 6,//网元权限上的EPS操作人员权限
        USERPRIVILEGE_ELEM_IMS = 7,//网元权限上的IMS操作人员权限
        USERPRIVILEGE_USER_NORMAL = 8,//用户权限上的普通用户
        USERPRIVILEGE_USER_USERMGR = 9,//用户管理
        USERPRIVILEGE_UNKNOWN = 100,//值越大, 权限越小
    };
}

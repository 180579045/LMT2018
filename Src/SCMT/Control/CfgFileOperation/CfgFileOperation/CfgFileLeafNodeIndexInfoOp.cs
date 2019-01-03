using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfgFileOperation
{
    public class CfgFileLeafNodeIndexInfoOp
    {
        public struIndexInfoCFG[] m_struIndex;
        private int indexNo ;// 赋值几个index了？
        public CfgFileLeafNodeIndexInfoOp()
        {
            indexNo = 0;
            m_struIndex = new struIndexInfoCFG[6];
            for(int i=0; i< 6; i++)
                m_struIndex[i].initStruIndexInfoCFG();
        }
        public void Add(DataRow leafRow)
        {
            m_struIndex[indexNo].SetStruIndexInfoCFG(leafRow);
            indexNo++;
        }
        public uint GetIndexRecorNum()
        {
            uint RecorNum = 1;
            for (int i = 0; i < indexNo; i++)
                RecorNum *= m_struIndex[i].indexNum;
            return RecorNum;
        }
        public object DeepCopy()
        {
            CfgFileLeafNodeIndexInfoOp s = new CfgFileLeafNodeIndexInfoOp();
            s.indexNo = this.indexNo;
            Array.Copy(this.m_struIndex, s.m_struIndex, this.m_struIndex.Count());
            return s;
        }
    }
    //索引结构体
    public struct struIndexInfoCFG
    {
        public uint indexNum;                 //索引
        public uint IndexMinValue;            //索引的最小值
        //int* pEnumIndexValue;          //枚举类型索引
        public uint[] pEnumIndexValue;     //枚举类型索引
        //string strIndexOID;           //索引的OID
        //string strIndexValueAllList;  //索引的取值范围
        public bool bEnumIndex;            //是否为枚举索引
        //string strIndexDefaultValue;  //索引的默认值
        public string strIndexOMType;        //索引的OMtype 
        //string strSYNType;            //索引的SYNType
        public ushort typeSize;
        public string strDefaultValue;
        public string asnType;
        public string mibName;

        public void initStruIndexInfoCFG()
        {
            pEnumIndexValue = null;
            bEnumIndex = false;
            strIndexOMType = "";
            indexNum = 1;
            IndexMinValue = 0;
            strDefaultValue = "";
            typeSize = (ushort)0;//iLeaftypeSize;//???
            asnType = "";
            mibName = "";
        }
        public void SetStruIndexInfoCFG(DataRow leafRow)
        {
            pEnumIndexValue = null;
            bEnumIndex = false;

            //  indexNum  ,IndexMinValue
            if ("BITS".Equals(leafRow["ASNType"].ToString()))
            {
                SetValueByBITS(leafRow);
                return;
            }

            // indexNum  ,IndexMinValue, pEnumIndexValue
            string strMIBVal_AllList = leafRow["MIBVal_AllList"].ToString();
            if (strMIBVal_AllList.IndexOf("/") >= 0)
            {
                bEnumIndex = true;
                SetValueByRightSlash(strMIBVal_AllList);//indexNum  ,IndexMinValue, pEnumIndexValue
            }
            else if (strMIBVal_AllList.IndexOf('-') > 0)
            {
                SetValueByMediumDash(strMIBVal_AllList);// indexNum  ,IndexMinValue
            }
            //else if (strMIBVal_AllList.IndexOf("/") >= 0)
            //{
            //    bEnumIndex = true;
            //    SetValueByRightSlash(strMIBVal_AllList);//indexNum  ,IndexMinValue, pEnumIndexValue
            //}
            else//single value
            {
                indexNum = 1;
                IndexMinValue = (uint)getEnumValue(strMIBVal_AllList);
            }
            
            strDefaultValue = leafRow["DefaultValue"].ToString();
            strIndexOMType = leafRow["OMType"].ToString();
            asnType = leafRow["ASNType"].ToString(); 
            typeSize = new StruCfgFileFieldInfo().Get_u16FieldLen(strIndexOMType, leafRow["MIBVal_AllList"].ToString(), leafRow["MIB_Syntax"].ToString());
            mibName = leafRow["MIBName"].ToString();
        }
        public object DeepCopy()
        {
            struIndexInfoCFG s = new struIndexInfoCFG();
            s.indexNum = this.indexNum;
            s.IndexMinValue = this.IndexMinValue;
            s.bEnumIndex = this.bEnumIndex;
            s.pEnumIndexValue = new uint[this.pEnumIndexValue.Count()];
            Array.Copy(s.pEnumIndexValue, this.pEnumIndexValue, this.pEnumIndexValue.Length);
            s.strIndexOMType = this.strIndexOMType;
            s.typeSize = this.typeSize;
            s.strDefaultValue = this.strDefaultValue;
            s.asnType = this.asnType;
            s.mibName = this.mibName;
            return s;
        }
        /// <summary>
        /// type: BITS,为indexNum IndexMinValue 赋值
        /// </summary>
        void SetValueByBITS(DataRow leafRow)
        {
            int nDeckNum = new StruCfgFileFieldInfo().GetDeckNum(leafRow["MIBVal_AllList"].ToString(), "/");
            int minData = 0;
            int maxData = new StruCfgFileFieldInfo().GetBITSNum(nDeckNum);

            indexNum = (uint)(maxData - minData + 1);//nIndexInfos.indexNum = maxData - minData + 1;
            IndexMinValue = (uint)minData;//nIndexInfos.IndexMinValue = minData;
        }
        /// <summary>
        /// '-' 中划线
        /// </summary>
        void SetValueByMediumDash(string strMIBVal_AllList)
        {
            //string strMIBVal_AllList = leafRow["MIBVal_AllList"].ToString();
            int pos = strMIBVal_AllList.IndexOf('-');
            int minData = getEnumValue(strMIBVal_AllList.Substring(0, pos));
            int maxData = getEnumValue(strMIBVal_AllList.Substring(pos + 1));
            indexNum = (uint)(maxData - minData + 1);//nIndexInfos.indexNum = maxData - minData + 1;
            IndexMinValue = (uint)minData;//nIndexInfos.IndexMinValue = minData;
        }

        /// <summary>
        ///  "/" 右斜杠
        /// </summary>
        void SetValueByRightSlash(string strMIBVal_AllList)
        {
            //依次读出索引值填写在索引数组中
            int iDeckNum = new StruCfgFileFieldInfo().GetDeckNum(strMIBVal_AllList, "/");
            pEnumIndexValue = new uint[iDeckNum];

            //以"/"分割的索引取值范围肯定不会超过1000
            //int indexValueArray[1000] = {0};
            uint indexNo = 0;
            int pos;
            string csTemp = strMIBVal_AllList;
            while ((pos = csTemp.IndexOf("/")) >= 0)
            {
                pEnumIndexValue[indexNo++] = (uint)getEnumValue(csTemp.Substring(0, pos));//填写
                csTemp = csTemp.Substring(pos + 1);//读值
            }
            //剩下一个单值处理
            pEnumIndexValue[indexNo++] = (uint)getEnumValue(csTemp);

            indexNum = indexNo;
            IndexMinValue = pEnumIndexValue[0];
        }
        
        int getEnumValue(string inputStr)
        {
            inputStr = inputStr.Trim(' ');
            if (String.Empty == inputStr)
                return 0;
            string csInputValue = "";
            int index = inputStr.IndexOf(":");
            if (index > 0)  //如果含有枚举注释
                csInputValue = inputStr.Substring(0, index);
            else       //只有值的情况
                csInputValue = inputStr;

            return int.Parse(csInputValue);
        }
    }
}

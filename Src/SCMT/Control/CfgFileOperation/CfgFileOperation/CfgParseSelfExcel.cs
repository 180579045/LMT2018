using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace CfgFileOperation
{
    /// <summary>
    /// 自定义_初配数据文件
    /// </summary>
    class CfgParseSelfExcel
    {

        Dictionary<string, string> SheetCellCol = null;

        public CfgParseSelfExcel()
        {
            SheetCellCol = new Dictionary<string, string>(){
                { "TableName" ,"A" },//表名
                { "Index" ,"B" },//索引
                { "NodeName" ,"C" },//字段名称
                { "NodeValue" ,"D" },//取值
            };
        }

        /// <summary>
        /// 总体处理 reclist
        /// </summary>
        /// <param name="strExcelPath"></param>
        /// <param name="strFileToDirectory"></param>
        /// <param name="strUeType"></param>
        public void ProcessingExcel(string strExcelPath, string strFileToDirectory, string strCondition, CfgOp cfgOp)
        {
            if ((String.Empty == strExcelPath) || (String.Empty == strFileToDirectory) || (null == cfgOp))
                return;

            CfgExcelOp excelOp = new CfgExcelOp();

            //strExcelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\RecList_V6.00.50.05.40.07.01.xls";
            Excel.Workbook wbook = excelOp.OpenExcel(strExcelPath);
            if (wbook == null)
                return;

            // "init" 页
            if (0 == String.Compare("init", strCondition, true)) // 不区分大小写，相等
            {
                Excel.Worksheet wks = excelOp.ReadExcelSheet(wbook, strCondition);//
                if (wks == null)
                    return;
                ExportSelfExcelForInit(wks, cfgOp);
            }
            // "patch" 页
            else if (0 == String.Compare("patch", strCondition, true))
            {
                Excel.Worksheet wks = excelOp.ReadExcelSheet(wbook, strCondition);//
                if (wks == null)
                    return;
            }
        }

        /// <summary>
        /// init 页的处理
        /// </summary>
        /// <param name="wbook"></param>
        /// <param name="excelOp"></param>
        void ExportSelfExcelForInit(Excel.Worksheet wks, CfgOp cfgOp)
        {
            int rowCount = GetEndLineNum(wks);                                        // 获取行数
            Dictionary<string, object[,]> ColVals = GetSheetColInfos( wks, rowCount); // 获取所有sheet 每col的数据
            string strCurTableName = "";                                              // 保存当前表名
            string strCurIndex = "";                                                  // 保存当前索引
            // 逐行分析
            for (int currentLine = 2; currentLine < rowCount + 1; currentLine++)
            {
                if (!IsExistTableName(cfgOp, ColVals, currentLine))                 //判断 : 查看是否有这个表
                    break;//

                string TableName = GetCellValue(ColVals, currentLine, "TableName"); //表名
                string Index = GetCellValue(ColVals, currentLine, "Index");         //索引
                string NodeName = GetCellValue(ColVals, currentLine, "NodeName");   //叶子名
                string NodeValue = GetCellValue(ColVals, currentLine, "NodeValue"); //叶子值
                strCurTableName = SetCurTableName(TableName, strCurTableName);      //更新表名
                strCurIndex = SetCurIndex(Index, strCurIndex);                      //更新索引
                
                if (!WriteValueToBuffer(cfgOp.m_mapTableInfo[strCurTableName],      //更新节点值
                    strCurIndex, NodeName, NodeValue))
                    continue;
            }
        }


        void ExportSelfExcelForPatch(Excel.Worksheet wks, CfgOp cfgOp)
        {

        }

        /// <summary>
        /// 与 reclist中的WriteValueToBuffer功能相同
        /// </summary>
        /// <param name="curtable"></param>
        /// <param name="strCurIndex"></param>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <returns></returns>
        bool WriteValueToBuffer(CfgTableOp curtable, string strCurIndex, string strNodeName, string strNodeValue)
        {
            CfgTableInstanceInfos pInstInfo = null;  //实例信息
            CfgFileLeafNodeOp leafNodeOp = null;     //节点属性
            int InstsPos = 0;                        //实例位置

            // 获得表的某个(strIndex)实例信息
            if (!curtable.GetCfgInstsByIndex(strCurIndex, out pInstInfo, out InstsPos))//是否存在这个索引值的实例
                return false;

            // 获得表的某个节点(strNodeName)信息
            if (!curtable.GetLeafNodesByNodeName(strNodeName, out leafNodeOp))
                return false;

            // 组合节点的信息
            byte[] InstMem = pInstInfo.GetInstMem();                           //1.这个表实例的内容的内存;实例化后表中节点依次排列的内容
            ushort u16FieldLen = leafNodeOp.m_struFieldInfo.u16FieldLen;       //2.这个节点字段的长度
            ushort u16FieldOffset = leafNodeOp.m_struFieldInfo.u16FieldOffset; //3.这个节点在实例内存中相对的位置 ;字段相对记录头偏移量
            string strOMType = leafNodeOp.m_struMibNode.strOMType;
            string asnType = leafNodeOp.m_struMibNode.strMibSyntax;            //asnType
            string strDefaultValue = strNodeValue;                             // 修改点, 在InstMem中把strNodeName的值修改为strNodeValue

            // 修改内存数据
            List<byte[]> byteArray = new List<byte[]>() { InstMem };
            List<int> bytePosL = new List<int>() { u16FieldOffset };
            new CfgOp().WriteToBuffer(byteArray, strDefaultValue, bytePosL, strOMType, u16FieldLen, "", asnType);

            return true;
        }

        /// <summary>
        /// 是否是存在的表
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="cfgOp"></param>
        /// <returns></returns>
        bool IsExistTableName(CfgOp cfgOp, Dictionary<string, object[,]> ColVals, int currentLine)
        {
            string strTableName = GetCellValue(ColVals, currentLine, "TableName"); //表名
            if (0 != String.Compare(strTableName, "", true))
            {
                strTableName.Replace("Table", "Entry");
                if (!cfgOp.m_mapTableInfo.ContainsKey(strTableName))//再次查看是否有这个表
                    return false;//pCurTabOp->InitialMibTableInfo(pAdoconnection, strCurTableName);
            }
            return true;
        }

        /// <summary>
        /// 判断是否结束行
        /// </summary>
        /// <param name="wks"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        int GetEndLineNum(Excel.Worksheet wks)
        {
            int rowCount = wks.UsedRange.Rows.Count;
            object[,] arry = (object[,])wks.Cells.get_Range("A" + "1", "A" + rowCount).Value2;

            for (int row = 1; row < rowCount + 1; row++)
            {
                var cellVar = arry[row, 1];
                if (cellVar == null)
                    continue;
                if (0 == String.Compare("end", cellVar.ToString(), true))
                    return row;
            }
            return rowCount;
        }

        /// <summary>
        /// 更新新的表名(条件是非"")
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="strCurTableName"></param>
        /// <returns></returns>
        string SetCurTableName(string strTableName, string strCurTableName)
        {
            return String.Compare(strTableName, "", true) == 0 ? strCurTableName : strTableName.Replace("Table", "Entry");
        }
        /// <summary>
        /// 更新索引值
        /// </summary>
        /// <param name="strIndex"></param>
        /// <param name="strCurIndex"></param>
        /// <returns></returns>
        string SetCurIndex(string strIndex, string strCurIndex)
        {
            if (0 != String.Compare(strIndex, "", true))
            {
                strCurIndex = "." + (0 < strIndex.IndexOf('_') ? strIndex.Replace("_", ".") : strIndex);
            }
            return strCurIndex;
        }
        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="currentLine"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        string GetCellValue(Dictionary<string, object[,]> ColVals, int currentLine, string strKey)
        {
            var cellVal = ColVals[strKey][currentLine, 1];
            return cellVal == null ? "" : cellVal.ToString();
        }

        /// <summary>
        /// 组合信息
        /// </summary>
        /// <param name="wks"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        Dictionary<string, object[,]> GetSheetColInfos(Excel.Worksheet wks, int rowCount)
        {
            // 获取所有sheet 每col的数据
            Dictionary<string, object[,]> ColVals = new Dictionary<string, object[,]>();
            foreach (var colName in SheetCellCol.Keys)//
            {
                object[,] arry = (object[,])wks.Cells.get_Range(SheetCellCol[colName] + "1", SheetCellCol[colName] + rowCount).Value2;
                ColVals.Add(colName, arry);
            }
            return ColVals;
        }

    }//class end

}

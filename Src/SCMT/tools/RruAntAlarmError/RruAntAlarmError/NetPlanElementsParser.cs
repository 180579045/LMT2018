using LogManager;
using MIBDataParser.JSONDataMgr;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RruAntAlarmError
{
    class NetPlanElementsParser
    {
        JObject infoJObect;
        JObject jCfgObject;

        public NetPlanElementsParser()
        {
            JsonFile jsonFile = new JsonFile();
            this.jCfgObject = jsonFile.ReadJsonFileForJObject(@".\cfg\parsecolumns_cfg.json");
            infoJObect = new JObject() { };
        }
        public bool parseNetPlanElementToJsonFile(bool isFirstRowColumn, string fileName)
        {
            if (false == parseNetPlanElements("器件总表", isFirstRowColumn, fileName))
            {
                Log.Error("==== parse sheet:器件总表 in" + fileName + "error, stop parse NetPlanElement");
                return false;
            }
            if (false == parseNetPlanShelfEquipment("机框", isFirstRowColumn, fileName))
            {
                Log.Error("==== parse sheet:机框 in" + fileName + "error, stop parse NetPlanElement");
                return false;
            }
            if (false == parseNetPlanBoardEquipment("板卡", isFirstRowColumn, fileName))
            {
                Log.Error("==== parse sheet:板卡 in" + fileName + "error, stop parse NetPlanElement");
                return false;
            }
            if (false == parseNetPlanRHubEquipment("rHUB", isFirstRowColumn, fileName))
            {
                Log.Error("==== parse sheet:rHUB in" + fileName + "error, stop parse NetPlanElement");
                return false;
            }
            JsonFile jsonFile = new JsonFile();
            jsonFile.WriteFile(@".\output\NetPlanElement_Board.json", infoJObect.ToString());
            Log.Info("======parse " + fileName + "into output\\NetPlanElement_Board.json ok");
            return true;
        }
        public bool parseNetPlanElements(string sheetName, bool isFirstRowColumn, string fileName)
        {
            JArray elementsJArray = new JArray();
            DataSet dataSet = ExcelReadWrite.ExcelToDataSet(sheetName, isFirstRowColumn, fileName);
            if (null == dataSet)
            {
                Log.Error("read ExcelToDataSet file: " + fileName + " sheet:" + sheetName + "fail");
                return false;
            }
            foreach (DataRow rowRec in dataSet.Tables[0].Rows)
            {
                NetPlanElement netPlanElement = new NetPlanElement();
                netPlanElement.number = int.Parse(rowRec["编号"].ToString());
                netPlanElement.elementName = rowRec["器件类型"].ToString();
                JObject tableObject = new JObject {
                    {"number", netPlanElement.number},
                    {"elementName", netPlanElement.elementName}
                };
                elementsJArray.Add(tableObject);
            }
            infoJObect.Add("netPlanElements", elementsJArray);
            return true;
        }
        private string equipNETypeExcelValueToMib(int line, string excelValue)
        {
            string managerRange = jCfgObject["equipNEType"].ToString();
            string[] managerRangeArray = managerRange.Split('/');
            foreach (string temp in managerRangeArray)
            {
                if (temp == excelValue)
                {
                    return temp;
                }
            }
            string result = "器件表 excel Line " + line + " value is not in equipNEType, excel is " + excelValue;
            MessageBox.Show(result);
            Log.Error(result);
            return null;
        }
        public bool parseNetPlanShelfEquipment(string sheetName, bool isFirstRowColumn, string fileName)
        {
            JArray infoJArray = new JArray();
            DataSet dataSet = ExcelReadWrite.ExcelToDataSet(sheetName, isFirstRowColumn, fileName);
            if (null == dataSet)
            {
                Log.Error("read ExcelToDataSet file: " + fileName + " sheet:" + sheetName + "fail");
                return false;
            }
            int line = 2;//与excel表中的行号保持一致
            JArray supportBoardJArray = new JArray();
            string equipNETypeTemp = "";
            ShelfEquipment shelfEquipment = new ShelfEquipment();
            foreach (DataRow rowRec in dataSet.Tables[0].Rows)
            {
                //单元格合并情况下，排除重复多次
                if ((equipNETypeTemp != rowRec["设备型号"].ToString()) && ("" != equipNETypeTemp))
                {
                    JObject tableObject = new JObject {
                        { "number", shelfEquipment.number},
                        {"equipNEType", shelfEquipment.equipNEType},
                        {"equipNETypeName", shelfEquipment.equipNETypeName},
                        { "totalSlotNum", shelfEquipment.totalSlotNum},
                        {"supportPlanSlotNum", shelfEquipment.supportPlanSlotNum},
                        {"columnsUI", shelfEquipment.columnsUI},
                        {"planSlotInfo", supportBoardJArray}
                    };
                    infoJArray.Add(tableObject);
                    supportBoardJArray = new JArray();
                }
                JObject tempJObject = new JObject {
                        { "slotIndex", int.Parse(rowRec["机框槽位号"].ToString())}
                    };
                if ("NULL" != rowRec["槽位支持的板卡类型"].ToString())
                {
                    string boardInfo = netBoardTypeExcelValueToMib(line, rowRec["槽位支持的板卡类型"].ToString());
                    tempJObject.Add("supportBoardType", JArray.Parse(boardInfo));
                }
                else
                {
                    tempJObject.Add("supportBoardType", new JArray());
                }
                supportBoardJArray.Add(tempJObject);
                equipNETypeTemp = equipNETypeExcelValueToMib(line, rowRec["设备型号"].ToString());
                if (null == equipNETypeTemp)
                {
                    return false;
                }
                shelfEquipment.number = int.Parse(rowRec["编号"].ToString());
                shelfEquipment.equipNEType = int.Parse(equipNETypeTemp.Substring(0, equipNETypeTemp.IndexOf(":")));
                shelfEquipment.equipNETypeName = equipNETypeTemp.Substring(equipNETypeTemp.IndexOf(":") + 1);
                shelfEquipment.totalSlotNum = int.Parse(rowRec["总槽位数"].ToString());
                shelfEquipment.supportPlanSlotNum = int.Parse(rowRec["可规划槽位数"].ToString());
                shelfEquipment.columnsUI = int.Parse(rowRec["图形列数"].ToString());

                //将最后一个设备添加到json数据中
                if ((line - 1) == dataSet.Tables[0].Rows.Count)
                {
                    JObject tableObject = new JObject {
                        { "number", shelfEquipment.number},
                        {"equipNEType", shelfEquipment.equipNEType},
                        {"equipNETypeName", shelfEquipment.equipNETypeName},
                        { "totalSlotNum", shelfEquipment.totalSlotNum},
                        {"supportPlanSlotNum", shelfEquipment.supportPlanSlotNum},
                        {"columnsUI", shelfEquipment.columnsUI},
                        {"planSlotInfo", supportBoardJArray}
                    };
                    infoJArray.Add(tableObject);
                    supportBoardJArray = new JArray();
                }

                line++;
            }

            infoJObect.Add("shelfEquipment", infoJArray);
            return true;
        }
        public bool parseNetPlanBoardEquipment(string sheetName, bool isFirstRowColumn, string fileName)
        {
            JArray infoJArray = new JArray();
            DataSet dataSet = ExcelReadWrite.ExcelToDataSet(sheetName, isFirstRowColumn, fileName);
            if (null == dataSet)
            {
                Log.Error("read ExcelToDataSet file: " + fileName + " sheet:" + sheetName + "fail");
                return false;
            }
            int line = 2;//与excel表中的行号保持一致
            JArray irOfpJArray = new JArray();
            string typeTemp = "";
            BoardEquipment boardEquipment = new BoardEquipment();
            foreach (DataRow rowRec in dataSet.Tables[0].Rows)
            {
                //单元格合并情况下，排除重复多次
                if ((typeTemp != rowRec["板卡类型"].ToString()) && ("" != typeTemp))
                {
                    JObject tableObject = new JObject {
                        { "number", boardEquipment.number},
                        {"boardType", boardEquipment.boardType},
                        {"boardTypeName", boardEquipment.boardTypeName},
                        {"supportEquipType", boardEquipment.supportEquipType},
                        {"supportConnectElement", JArray.Parse(boardEquipment.supportConnectElement)},
                        {"irOfpNum", boardEquipment.irOfpNum},
                    };
                    if(0 != boardEquipment.irOfpNum)
                    {
                        tableObject.Add("irOfpPortInfo", irOfpJArray);
                    }
                    else
                    {
                        tableObject.Add("irOfpPortInfo", new JArray());
                    }
                    infoJArray.Add(tableObject);
                    irOfpJArray = new JArray();
                }
                if(0 != int.Parse(rowRec["支持的光口数"].ToString()))
                {
                    string ofpSpeedArray = transSpeedExcelValueToMib(0, line, rowRec["光口支持速率"].ToString());
                    JObject tempJObject = new JObject {
                        {"ofpIndex", int.Parse(rowRec["光口号"].ToString())},
                        {"irOfpPortTransSpeed", JArray.Parse(ofpSpeedArray)}
                    };
                    irOfpJArray.Add(tempJObject);
                }

                typeTemp = rowRec["板卡类型"].ToString();
                boardEquipment.number = int.Parse(rowRec["编号"].ToString());
                if(false == checknetBoardType(line, rowRec["板卡类型"].ToString()))
                {
                    return false;
                }
                string temp = rowRec["板卡类型"].ToString();
                boardEquipment.boardType = int.Parse(temp.Substring(0, temp.IndexOf(':')));
                boardEquipment.boardTypeName = temp.Substring(temp.IndexOf(':') + 1);
                boardEquipment.supportEquipType = int.Parse(rowRec["支持的设备类型"].ToString());
                boardEquipment.supportConnectElement = connectElementsToArray(line, rowRec["可连接的器件类型"].ToString());
                boardEquipment.irOfpNum = int.Parse(rowRec["支持的光口数"].ToString());
                //将最后一个板卡添加到json数据中
                if ((line - 1) == dataSet.Tables[0].Rows.Count)
                {
                    JObject tableObject = new JObject {
                        { "number", boardEquipment.number},
                        {"boardType", boardEquipment.boardType},
                        {"boardTypeName", boardEquipment.boardTypeName},
                        {"supportEquipType", boardEquipment.supportEquipType},
                        {"supportConnectElement", JArray.Parse(boardEquipment.supportConnectElement)},
                        {"irOfpNum", boardEquipment.irOfpNum},
                    };
                    if (0 != boardEquipment.irOfpNum)
                    {
                        tableObject.Add("irOfpPortInfo", irOfpJArray);
                    }
                    else
                    {
                        tableObject.Add("irOfpPortInfo", new JArray());
                    }
                    infoJArray.Add(tableObject);
                }
                line++;
            }
         
            infoJObect.Add("boardEquipment", infoJArray);
            return true;
        }
        public bool parseNetPlanRHubEquipment(string sheetName, bool isFirstRowColumn, string fileName)
        {
            JArray infoJArray = new JArray();
            DataSet dataSet = ExcelReadWrite.ExcelToDataSet(sheetName, isFirstRowColumn, fileName);
            if (null == dataSet)
            {
                Log.Error("read ExcelToDataSet file: " + fileName + " sheet:" + sheetName + "fail");
                return false;
            }
            int line = 2;
            JArray irOfpJArray = new JArray();

            foreach (DataRow rowRec in dataSet.Tables[0].Rows)
            {
                RHUBEquipment rHubElement = new RHUBEquipment();
                rHubElement.number = int.Parse(rowRec["编号"].ToString());
                rHubElement.rHubType = rowRec["类型名称"].ToString();
                rHubElement.friendlyUIName = rowRec["rHUB友好名"].ToString();
                rHubElement.irOfpNum = int.Parse(rowRec["支持的光口数"].ToString());
                rHubElement.irOfpPortTransSpeed = transSpeedExcelValueToMib(0, line, rowRec["光口支持速率"].ToString());
                rHubElement.ethPortRNum = int.Parse(rowRec["支持的以太口数"].ToString());
                rHubElement.ethPortTransSpeed = transSpeedExcelValueToMib(1, line, rowRec["以太口支持速率"].ToString());

                JObject tableObject = new JObject {
                    {"number", rHubElement.number},
                    {"rHubType", rHubElement.rHubType},
                    {"friendlyUIName", rHubElement.friendlyUIName},
                    {"irOfpNum", rHubElement.irOfpNum},
                    {"irOfpPortTransSpeed", JArray.Parse(rHubElement.irOfpPortTransSpeed)},
                    {"ethPortRNum", rHubElement.ethPortRNum},
                    {"ethPortTransSpeed", JArray.Parse(rHubElement.ethPortTransSpeed)}
                };
                infoJArray.Add(tableObject);
                line++;
            }
            infoJObect.Add("rHubEquipment", infoJArray);
            return true;
        }
        private string transSpeedExcelValueToMib(int type, int line, string excelValue)
        {
            string result = "";
            string[] split = excelValue.Split('/');//文档中格式：0:mbps2500|2.5G/1:mbps5000|5G
            string managerRange;
            if (1 == type)
            {
                managerRange = jCfgObject["netEthTransPlanSpeed"].ToString();
                result = "netEthTransPlanSpeed";
            }
            else
            {
                managerRange = jCfgObject["netIROfpTransPlanSpeed"].ToString();
                result = "netIROfpTransPlanSpeed";
            }
            string[] managerRangeArray = managerRange.Split('/');
            JArray fileValueJArray = new JArray();
            JObject objRec = null;
            foreach (string item in split)
            {
                string validContent = "";
                foreach (string managerTemp in managerRangeArray)
                {
                    if (managerTemp == item)
                    {
                        validContent = managerTemp;
                        break;
                    }
                }
                if ("" != validContent)
                {
                    int indexsplit = validContent.IndexOf(":");
                    if (-1 == indexsplit)
                    {
                        result = result + " content in cfgFile is invalid, please check parsecolumns_cfg.json: " + managerRange;
                        Log.Error(result);
                        MessageBox.Show(result);
                        return null;
                    }
                    string key = validContent.Substring(0, indexsplit);
                    string value = validContent.Substring(indexsplit + 1);
                    objRec = new JObject{
                              { "value", key},
                              { "desc", value}
                    };
                    fileValueJArray.Add(objRec);
                }
                else
                {
                    result = "器件表 excel Line " + line + " 光口支持速率 " + item + " is not in parsecolumns_cfg.json: " + managerRange;
                    Log.Error(result);
                    MessageBox.Show(result);
                    return null;
                }
            }
            result = "";
            return fileValueJArray.ToString();
        }
        private bool checknetBoardType(int line, string excelValue)
        {
            string result = "";

            string managerRange = jCfgObject["netBoardType"].ToString();
            string[] managerRangeArray = managerRange.Split('/');
            JArray fileValueJArray = new JArray();
            JObject objRec = null;
            if ("NULL" == excelValue)
            {
                result = "器件表 excel 板卡sheet Line " + line + " 板卡类型 " + excelValue + " is NULL";
                Log.Error(result);
                MessageBox.Show(result);
                return false;
            }
            foreach (string managerTemp in managerRangeArray)
            {
                if (managerTemp == excelValue)
                {
                    return true;
                }
            }
            result = "器件表 excel 板卡sheet Line " + line + " 板卡类型 " + excelValue + " is not in parsecolumns_cfg.json: " + managerRange;
            Log.Error(result);
            MessageBox.Show(result);
            return false;
        }
        private string netBoardTypeExcelValueToMib(int line, string excelValue)
        {
            string[] split = excelValue.Split('/');//文档中格式：178:scte|SCTE板/240:sctf|SCTF板
            string result = "";

            string managerRange = jCfgObject["netBoardType"].ToString();
            string[] managerRangeArray = managerRange.Split('/');
            JArray fileValueJArray = new JArray();
            JObject objRec = null;
            if("NULL" == excelValue)
            {
                return fileValueJArray.ToString();
            }
            foreach (string item in split)
            {
                string validContent = "";
                foreach (string managerTemp in managerRangeArray)
                {
                    if (managerTemp == item)
                    {
                        validContent = managerTemp;
                        break;
                    }
                }
                if ("" != validContent)
                {
                    int indexsplit = validContent.IndexOf(":");
                    if (-1 == indexsplit)
                    {
                        result = " netBoardType content in cfgFile is invalid " + managerRange;
                        Log.Error(result);
                        MessageBox.Show(result);
                        return null;
                    }
                    string key = validContent.Substring(0, indexsplit);
                    string value = validContent.Substring(indexsplit + 1);
                    objRec = new JObject{
                              { "value", key},
                              { "desc", value}
                    };
                    fileValueJArray.Add(objRec);
                }
                else
                {
                    result = "器件表 excel Line " + line + " 槽位支持的板卡类型 " + item + " is not in parsecolumns_cfg.json: " + managerRange;
                    Log.Error(result);
                    MessageBox.Show(result);
                    return null;
                }
            }
            result = "";
            return fileValueJArray.ToString();
        }
        private string connectElementsToArray(int line, string excelValue)
        {
            string[] split = excelValue.Split('/');//文档中格式：4/5
            JArray fileValueJArray = new JArray();
            if("NULL" == excelValue)
            {
                return fileValueJArray.ToString();
            }
            foreach (var item in split)
            {
                //TODO 暂时不做校验
                JObject objRec = new JObject{
                              { "value", item},
                              { "desc" , "" }
                    };
                fileValueJArray.Add(objRec);
            }
            return fileValueJArray.ToString();
        }
    }
}

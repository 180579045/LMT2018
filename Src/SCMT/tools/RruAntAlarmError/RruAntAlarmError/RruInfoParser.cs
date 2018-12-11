using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows;
using MIBDataParser.JSONDataMgr;
using LogManager;

namespace RruAntAlarmError
{
    public enum TypeOfDataFile
    {
        RruType = 0,
        RruTypePort = 1,
        ErrorCode = 2,
        AlarmCode = 3,
        AntWeight = 4
    };
    class RruInfoParser
    {
        string headercfg;
        JObject jCfgObject;
        public RruInfoParser()
        {
            JsonFile jsonFile = new JsonFile();
            this.jCfgObject = jsonFile.ReadJsonFileForJObject(@".\cfg\parsecolumns_cfg.json");
            this.headercfg = jCfgObject["rruFileColumnHeader"].ToString();
        }
        private string ManufacturerIndexExcelValueToMib(int line, string excelValue, out string result)
        {
            string split = excelValue.Substring(0, excelValue.IndexOf(":"));
            //存储为MIB取值4:datang|大唐 这种格式
            string managerRange = jCfgObject["rruTypeManufacturerIndex"].ToString();
            int manufacturerIndex = int.Parse(split);
            string[] managerRangeArray = managerRange.Split('/');
            foreach (string temp in managerRangeArray)
            {
                int tempIndex = int.Parse(temp.Substring(0, excelValue.IndexOf(":")));
                if(manufacturerIndex == tempIndex)
                {
                    result = "";
                    return temp;
                }
            }
            result = "rru excel Line " + line + " value is not in rruTypeManufacturerIndex, excel is " + excelValue;
            MessageBox.Show(result);
            Log.Debug(result);
            return null;
        }

        private string FiberLengthExcelValueToMib(int line, string excelValue, out string result)
        {
            int fileIndex = 1;
            //pico相关的拉远距离填的都是0
            if (-1 == excelValue.IndexOf("公里"))
            {
                fileIndex = 2;
            }
            else
            {
                string split = excelValue.Substring(0, excelValue.IndexOf("公里"));
                fileIndex = int.Parse(split);
                if (10 == fileIndex)
                {
                    fileIndex = 1;
                }
                else if (20 == fileIndex)
                {
                    fileIndex = 2;
                }
                else if (40 == fileIndex)
                {
                    fileIndex = 4;
                }
                else
                {
                    //不支持的默认20KM
                    result = "rru excel Line " + line + "rruTypeFiberLength is not support " + fileIndex;
                    Log.Error(result);
                    fileIndex = 2;
                }
            }

            string managerRange = jCfgObject["rruTypeFiberLength"].ToString();
            string[] managerRangeArray = managerRange.Split('/');
            foreach (string temp in managerRangeArray)
            {
                //注意，mib中该节点是bit类型，在配置文件中转换成了10进制，目前所有的RRU还不支持多种拉远距离，暂时
                int tempIndex = int.Parse(temp.Substring(0, temp.IndexOf(":")));
                if (fileIndex == tempIndex)
                {
                    result = "";
                    JArray objJArray = new JArray() { };
                    JObject objRec = new JObject{
                              { "value", tempIndex.ToString() },
                              { "desc", temp.Substring(temp.IndexOf(":") + 1)}
                             };
                    objJArray.Add(objRec);
                    return objJArray.ToString();
                }
            }
            result = "rru excel Line " + line + " value is not in rruTypeFiberLength, excel is " + excelValue;
            MessageBox.Show(result);
            Log.Debug(result);
            return null;
        }

        private string IrCompressModeExcelValueToMib(int line, string excelValue, out string result)
        {
            //bit表示,该属性基站不会有修改，可以不使用配置文件中的值，直接代码写死
            string[] split = excelValue.Split('/'); //文档中压缩属性列
            JArray fileValueJArray = new JArray();
            JObject objRec = null;
            foreach (string item in split)
            {
                if (item.Equals("不压缩"))
                {
                    objRec = new JObject{
                              { "value","1" },
                              { "desc","notCompress|不压缩"} 
                             };
                    fileValueJArray.Add(objRec);
                }
                else if (item.Equals("压缩"))
                {
                    objRec = new JObject{
                              { "value","2" },
                              { "desc","compress|压缩"}
                             };
                    fileValueJArray.Add(objRec);
                }
                else
                {
                    result = "rru excel Line " + line + " value is invalid, 压缩属性 is " + excelValue;
                    Log.Debug(result);
                    MessageBox.Show(result);
                    return null;
                }
            }
            result = "";
            return fileValueJArray.ToString();
        }
        private string IrCompressModeWithBandwidthExcelValueToMib(int line, string excelValue, out string result)
        {
            //bit表示,该属性基站不会有修改，可以不使用配置文件中的值，直接代码写死
            string[] split = excelValue.Split('|'); //使用压缩属性与带宽列，格式为 压缩:20M|不压缩:5M/10M/15M
            string managerRange = jCfgObject["netLcFreqBandWidth"].ToString();
            string[] managerRangeArray = managerRange.Split('/');
            JArray fileValueJArray = new JArray();
            JObject objRec = null;
            foreach (string item in split)
            {
                string temp = item;
                if(item.IndexOf(':') != -1)
                {
                    temp = temp.Substring(0, item.IndexOf(':'));
                }
                if (temp == "不压缩")
                {
                    objRec = new JObject{
                              { "value","1" },
                              { "desc","notCompress|不压缩"}
                             };
                }
                else if (temp == "压缩")
                {
                    objRec = new JObject{
                              { "value","2" },
                              { "desc","compress|压缩"}
                             };
                }
                else
                {
                    //考虑到TDRRU压缩属性填写的为空,此处处理不返回空
                    result = "rru excel Line " + line + " value is invalid, 压缩属性 is " + excelValue;
                    Log.Debug(result);
                    objRec = new JObject{
                              { "value","2" },
                              { "desc","compress|压缩"}
                             };
                }
                JArray bandwidthJArray = new JArray();
                //取带宽
                int startIndex = item.IndexOf(':');
                string[] splitBand = item.Substring(startIndex + 1).Split('/');
                foreach (string itemBand in splitBand)
                {
                    string validContent = "";
                    //遍历配置文件，取匹配的mib值
                    foreach (string itemManager in managerRangeArray)
                    {
                        if (itemManager.Contains(itemBand))
                        {
                            validContent = itemManager;
                            break;
                        }
                    }
                    if ("" != validContent)
                    {
                        int indexsplit = validContent.IndexOf(":");
                        if (-1 == indexsplit)
                        {
                            result = "netLcFreqBandWidth content in cfgFile is invalid, please check parsecolumns_cfg.json： "+ managerRange;
                            Log.Error(result);
                            MessageBox.Show(result);
                            return null;
                        }
                        string key = validContent.Substring(0, indexsplit);
                        string value = validContent.Substring(indexsplit + 1);
                        JObject objRecBand = new JObject{
                              { "value", key },
                              { "desc",value}
                             };
                        bandwidthJArray.Add(objRecBand);
                    }
                    else
                    {
                        result = "rru excel Line " + line + " 压缩属性与带宽约束关系 " + item + " is not in parsecolumns_cfg.json："+ managerRange;
                        Log.Error(result);
                        MessageBox.Show(result);
                        return null;
                    }
                }
                objRec.Add("bandwidth", bandwidthJArray);
                fileValueJArray.Add(objRec);
            }
            result = "";
            return fileValueJArray.ToString();
        }
        private string SupportCellWorkModeExcelValueToMib(int line, string excelValue, out string result)
        {
            //bit表示，目前不同版本可能有不同取值，使用配置文件的内容
            string[] split = excelValue.Split('/');
            JArray fileValueJArray = new JArray();
            JObject objRec = null;

            string managerRange = jCfgObject["rruTypeSupportCellWorkMode"].ToString();
            string[] managerRangeArray = managerRange.Split('/');
            foreach (string item in split)
            {
                string validContent = "";
                foreach (string managerTemp in managerRangeArray)
                {
                    if (item.Equals("LTE"))
                    {
                        if(-1 != managerTemp.IndexOf("LTE TDD"))
                        {
                            validContent = managerTemp;
                            break;
                        }
                    }
                    else if (item.Equals("TD"))
                    {
                        if (-1 != managerTemp.IndexOf("TD-SCDMA"))
                        {
                            validContent = managerTemp;
                            break;
                        }
                    }
                    else if (item.Equals("NR"))
                    {
                        if (-1 != managerTemp.IndexOf("NR"))
                        {
                            validContent = managerTemp;
                            break;
                        }
                    }
                    else if (item.Equals("FDD"))
                    {
                        if (-1 != managerTemp.IndexOf("LTE FDD"))
                        {
                            validContent = managerTemp;
                            break;
                        }
                    }
                    else if (item.Equals("NBIOT"))
                    {
                        if (-1 != managerTemp.IndexOf("NB-IOT"))
                        {
                            validContent = managerTemp;
                            break;
                        }
                    }
                    else
                    {
                        result = "rru excel Line " + line + " value is invalid, 支持建立的小区类型 is " + excelValue;
                        Log.Error(result);
                        MessageBox.Show(result);
                        return null;
                    }
                }
                if("" != validContent)
                {
                    int indexsplit = validContent.IndexOf(":");
                    if (-1 == indexsplit)
                    {
                        result = "rruTypeSupportCellWorkMode content in cfgFile is invalid, please check parsecolumns_cfg.json: " + managerRange;
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
                    result = "rru excel Line " + line + " 支持建立的小区类型 " + item + " is not in parsecolumns_cfg.json: " + managerRange;
                    Log.Error(result);
                    MessageBox.Show(result);
                    return null;
                }
            }
            result = "";
            return fileValueJArray.ToString();
        }
        private string NetRRUOfpWorkModeExcelValueToMib(int line, string excelValue, out string result)
        {
            string[] split = excelValue.Split('&');//文档中格式：正常&主备&分担
            string managerRange = jCfgObject["netRRUOfpWorkMode"].ToString();
            string[] managerRangeArray = managerRange.Split('/');
            JArray fileValueJArray = new JArray();
            JObject objRec = null;
            foreach (string item in split)
            {
                string validContent = "";
                //出现了换行符，此处处理下
                string updateItem = item.Trim('\n').Trim(' ');
                foreach (string managerTemp in managerRangeArray)
                {
                    if (managerTemp.Contains(updateItem))
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
                        result = "netRRUOfpWorkMode content in cfgFile is invalid, please check parsecolumns_cfg.json: " + managerRange;
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
                    result = "rru excel Line " + line + " 支持的工作模式 " + item + " is not in parsecolumns_cfg.json: "+ managerRange;
                    Log.Error(result);
                    MessageBox.Show(result);
                    return null;
                }
            }
            result = "";
            return fileValueJArray.ToString();
        }
        private string IROfpTransPlanSpeedToMibExcelValueToMib(int type, int line, string excelValue, out string result)
        {
            string[] split = excelValue.Split('&');//文档中格式：2.5G&5G&10G
            string managerRange;
            if (1 == type)
            {
                managerRange = jCfgObject["netEthTransPlanSpeed"].ToString();
            }
            else
            {
                managerRange = jCfgObject["netIROfpTransPlanSpeed"].ToString();
            }
            string[] managerRangeArray = managerRange.Split('/');
            JArray fileValueJArray = new JArray();
            JObject objRec = null;
            foreach (string item in split)
            {
                string validContent = "";
                foreach (string managerTemp in managerRangeArray)
                {
                    if (managerTemp.Contains(item))
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
                        result = "netIROfpTransPlanSpeed/netEthTransPlanSpeed content in cfgFile is invalid, please check parsecolumns_cfg.json: " + managerRange;
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
                    result = "rru excel Line " + line + " 支持的IR口速率 " + item + " is not in parsecolumns_cfg.json: " + managerRange;
                    Log.Error(result);
                    MessageBox.Show(result);
                    return null;
                }
            }
            result = "";
            return fileValueJArray.ToString();
        }

        private string SupportFreqBandExcelValueToMib(int line, string excelValue, out string result)
        {
            //bit表示
            string[] split = excelValue.Split('/');
            JArray fileValueJArray = new JArray();
            JObject objRec = null;
            string managerRange = jCfgObject["rruTypePortSupportFreqBand"].ToString();
            string[] managerRangeArray = managerRange.Split('/');

            foreach (string item in split)
            {
                string validContent = "";
                //提取"X频段"值,之所以不用全集,出现过同一个频段但是频段范围填写不一样
                int indexsplit = item.IndexOf("(");
                if (-1 == indexsplit)
                {
                    result = "rru excel Line " + line + " value ( is invalid, 通道支持频段 is " + excelValue;
                    Log.Error(result);
                    MessageBox.Show(result);
                    return null;
                }
                string freqValue = item.Substring(0, indexsplit);
                foreach (string managerTemp in managerRangeArray)
                {
                    //必须在配置文件中存在
                    if (-1 != managerTemp.IndexOf(freqValue))
                    {
                        validContent = managerTemp;
                        break;
                    }
                }
                if ("" != validContent)
                {
                    indexsplit = validContent.IndexOf(":");
                    if (-1 == indexsplit)
                    {
                        result = "rruTypePortSupportFreqBand content in cfgFile is invalid, please check parsecolumns_cfg.json: " + managerRange;
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
                    result = "rru excel Line " + line + "通道支持频段 " + item + " is not in parsecolumns_cfg.json: " + managerRange;
                    Log.Error(result);
                    MessageBox.Show(result);
                    return null;
                }
            }
            result = "";
            return fileValueJArray.ToString();
        }

        private Dictionary<string, int> TdsCarrierNumExcelValueToMib(string excelValue, string SupportFreqBand)
        {
            //F\A不一定存在，结合另外列支持频段来判断
            string[] split = excelValue.Split('/');

            int indexA = SupportFreqBand.IndexOf("A频段");
            int indexF = SupportFreqBand.IndexOf("F频段");
            Dictionary<string, int> result = new Dictionary<string, int>();

            if (-1 == indexA && -1 == indexF)
            {
                result.Add("A", 0);
                result.Add("F", 0);
            }
            else if(indexA >= indexF)
            {
                if(-1 == indexF)
                {
                    result.Add("F", 0);
                    result.Add("A", int.Parse(split[0].Trim()));
                }
                else
                {
                    if(split.Length < 2)
                    {
                        //测试发现TDRU364FAD填写有问题，填写不正确
                        result.Add("Error", 1);
                        result.Add("A", 0);
                        result.Add("F", 0);
                    }
                    else
                    {
                        result.Add("F", int.Parse(split[1].Trim()));
                        result.Add("A", int.Parse(split[0].Trim()));
                    }
                }
            }
            else
            {
                if(-1 == indexA)
                {
                    result.Add("A", 0);
                    result.Add("F", int.Parse(split[0].Trim()));
                }
                else
                {
                    if (split.Length < 2)
                    {
                        //测试发现TDRU364FAD填写有问题，填写不正确
                        result.Add("Error", 1);
                        result.Add("A", 0);
                        result.Add("F", 0);
                    }
                    else
                    {
                        result.Add("F", int.Parse(split[0].Trim()));
                        result.Add("A", int.Parse(split[1].Trim()));
                    }
                }
            }
            return result;
        }
        private string TxRxStatusExcelValueToMib(int line, string excelValue, out string result)
        {
            JArray fileValueJArray = new JArray();
            JObject objRec = null;
            //此参数在MIB中较稳定，代码写死枚举值，不读取配置文件中参数
            //"1:rx|接收有效/2:tx|发送有效/3:rxAndTx|接收发送均有效/4:invalid|无效"
            if(excelValue.Contains("收"))
            {
                objRec = new JObject{
                              { "value", "1"},
                              { "desc",  "rx|接收有效"}
                             };
                fileValueJArray.Add(objRec);
            }
            if (excelValue.Contains("发"))
            {
                objRec = new JObject{
                              { "value", "2"},
                              { "desc",  "tx|发送有效"}
                             };
                fileValueJArray.Add(objRec);
            }
            if (excelValue.Contains("收") && excelValue.Contains("发"))
            {
                objRec = new JObject{
                              { "value", "3"},
                              { "desc",  "rxAndTx|接收发送均有效"}
                             };
                fileValueJArray.Add(objRec);
            }
            //excel表中不填写则填写为接收发送均有效
            if ("" == excelValue.Trim(' '))
            {
                result = "rru excel Line " + line + " 通道收发 " + excelValue + " is null";
                Log.Warn(result);
                objRec = new JObject{
                              { "value", "3"},
                              { "desc",  "rxAndTx|接收发送均有效"}
                             };
                fileValueJArray.Add(objRec);
            }

            result = "";
            return fileValueJArray.ToString();
        }    
        private string checkColumnName(DataColumnCollection columnCollection)
        {
            string result = "";
            //读取列
            string[] sArray = headercfg.Split('&');
            foreach (string temp in sArray)
            {
                if (-1 == columnCollection.IndexOf(temp))
                {
                    result += temp + ", ";
                }
            }
            return result;
        }

        private void AddRruTypeElement(JArray rruTypeJArray, RruTypeTable table)
        {
            JArray jArrayIrCompressMode = JArray.Parse(table.rruTypeIrCompressMode);
            JArray jArraySupportCellWorkMode = JArray.Parse(table.rruTypeSupportCellWorkMode);
            //JArray jArrayFiberLength = JArray.Parse(table.rruTypeFiberLength);//目前代码当做枚举使用，不支持多个拉远距离
            JArray jArraySupportNetWorkMode = JArray.Parse(table.rruTypeNotMibSupportNetWorkMode);
            JArray jArrayIrRate = JArray.Parse(table.rruTypeNotMibIrRate);
            JArray jArrayIrBand = JArray.Parse(table.rruTypeNotMibIrBand);
            JObject tableObject = new JObject { {"rruTypeManufacturerIndex", table.rruTypeManufacturerIndex},
                {"rruTypeNotMibManufacturerName", table.rruTypeNotMibManufacturerName},
                {"rruTypeIndex", table.rruTypeIndex},
                {"rruTypeName", table.rruTypeName},
                {"rruTypeMaxAntPathNum", table.rruTypeMaxAntPathNum},
                {"rruTypeMaxTxPower", table.rruTypeMaxTxPower},
                {"rruTypeBandWidth", table.rruTypeBandWidth},
                {"rruTypeFiberLength", JArray.Parse(table.rruTypeFiberLength)},
                {"rruTypeIrCompressMode", jArrayIrCompressMode},
                {"rruTypeSupportCellWorkMode", jArraySupportCellWorkMode},
                {"rruTypeFamilyName", table.rruTypeFamilyName},
                {"rruTypeNotMibMaxePortNo", table.rruTypeNotMibMaxePortNo},
                {"rruTypeNotMibSupportNetWorkMode", jArraySupportNetWorkMode},
                {"rruTypeNotMibIrRate", jArrayIrRate},
                {"rruTypeNotMibIrBand", jArrayIrBand},
                {"rruTypeNotMibIsPico",  table.rruTypeNotMibIsPico}
            };
            //同一款RRU多个通道，excel表是单元合并的方式填写的，此处保存数据时需要进行去重
            foreach (var item in rruTypeJArray)
            {
                if (0 == String.Compare(item["rruTypeIndex"].ToString(), table.rruTypeIndex.ToString()))
                {
                    return;
                }
            }
            rruTypeJArray.Add(tableObject);
        }
        private void AddRruTypePortElement(JArray rruTypePortJArray, RruTypePortTble table)
        {
            JArray jArraySupportFreqBand = JArray.Parse(table.rruTypePortSupportFreqBand);
            JArray jArrayRxTx = JArray.Parse(table.rruTypePortNotMibRxTxStatus);
            JObject tableObject = new JObject { {"rruTypePortManufacturerIndex", table.rruTypePortManufacturerIndex},
                {"rruTypePortNotMibManufacturerName", table.rruTypePortNotMibManufacturerName},
                {"rruTypePortIndex", table.rruTypePortIndex},
                {"rruTypePortNo", table.rruTypePortNo},
                {"rruTypePortPathNo", table.rruTypePortPathNo},
                {"rruTypePortSupportFreqBand", jArraySupportFreqBand},
                {"rruTypePortSupportFreqBandWidth", table.rruTypePortSupportFreqBandWidth},
                {"rruTypePortSupportAbandTdsCarrierNum", table.rruTypePortSupportAbandTdsCarrierNum},
                {"rruTypePortSupportFBandTdsCarrierNum", table.rruTypePortSupportFBandTdsCarrierNum},
                {"rruTypePortCalAIqTxNom", table.rruTypePortCalAIqTxNom},
                {"rruTypePortCalAIqRxNom", table.rruTypePortCalAIqRxNom},
                {"rruTypePortCalPoutTxNom", table.rruTypePortCalPoutTxNom},
                {"rruTypePortCalPinRxNom", table.rruTypePortCalPinRxNom},
                {"rruTypePortAntMaxPower", table.rruTypePortAntMaxPower},
                {"rruTypePortNotMibRxTxStatus", jArrayRxTx}
            };
            rruTypePortJArray.Add(tableObject);
        }
        public bool parseRruInfoToJsonFile(string sheetName, bool isFirstRowColumn, string fileName)
        {
            //List<RruTypeTable> rruTypeList = new List<RruTypeTable>();
            //List<RruTypePortTble> rruTypePortList = new List<RruTypePortTble>();
            JArray rruTypeJArray = new JArray();
            JArray rruTypePortJArray = new JArray();

            DataSet dataSet = ExcelReadWrite.ExcelToDataSet(sheetName, isFirstRowColumn, fileName);
            if(null == dataSet)
            {
                return false;
            }
            String columnCheck = checkColumnName(dataSet.Tables[0].Columns);
            if(0 != String.Compare("", columnCheck))
            {
                MessageBox.Show(columnCheck + " colunm is not in rru info excel ");
                Log.Error(columnCheck + " colunm is not in rru info excel ");
                return false;
            }
            string result = "";
            int line = 2;//与excel表中的行号保持一致
            foreach (DataRow rowRec in dataSet.Tables[0].Rows)
            {
                RruTypeTable rruTypeItem = new RruTypeTable();
                RruTypePortTble rruTypePortItem = new RruTypePortTble();
                string mibValue = ManufacturerIndexExcelValueToMib(line, rowRec["RRU厂家名称"].ToString(), out result);
                if(null == mibValue)
                {
                    return false;
                }
                rruTypeItem.rruTypeManufacturerIndex = int.Parse(mibValue.Substring(0, mibValue.IndexOf(":")));
                rruTypeItem.rruTypeNotMibManufacturerName = mibValue.Substring(mibValue.IndexOf(":") + 1);
                rruTypeItem.rruTypeIndex = int.Parse(rowRec["RRU硬件类型"].ToString());
                rruTypeItem.rruTypeName = rowRec["RRU名称"].ToString();
                rruTypeItem.rruTypeMaxAntPathNum = int.Parse(rowRec["支持的天线根数"].ToString());
                rruTypeItem.rruTypeMaxTxPower = int.Parse(rowRec["通道的最大发射功率"].ToString());
                rruTypeItem.rruTypeBandWidth = int.Parse(rowRec["支持的频带总宽度（M）"].ToString());
                mibValue = FiberLengthExcelValueToMib(line, rowRec["拉远属性"].ToString(), out result);
                if(null == mibValue)
                {
                    return false;
                }
                rruTypeItem.rruTypeFiberLength = mibValue;
                mibValue = IrCompressModeExcelValueToMib(line, rowRec["压缩属性"].ToString(), out result);
                if (null == mibValue)
                {
                    return false;
                }
                rruTypeItem.rruTypeIrCompressMode = mibValue;
                mibValue = SupportCellWorkModeExcelValueToMib(line, rowRec["支持建立的小区类型"].ToString(), out result);
                if (null == mibValue)
                {
                    return false;
                }
                rruTypeItem.rruTypeSupportCellWorkMode = mibValue;
                if(-1 != headercfg.IndexOf("RRU类型族名称"))
                {
                    rruTypeItem.rruTypeFamilyName = rowRec["RRU类型族名称"].ToString();
                }
                else
                {
                    rruTypeItem.rruTypeFamilyName = "";
                }
                // 与mib没有直接映射关系
                rruTypeItem.rruTypeNotMibMaxePortNo = int.Parse(rowRec["射频通道数"].ToString());
                mibValue = NetRRUOfpWorkModeExcelValueToMib(line, rowRec["支持的工作模式"].ToString(), out result);
                if (null == mibValue)
                {
                    return false;
                }
                rruTypeItem.rruTypeNotMibSupportNetWorkMode = mibValue;
                int type = 0;
                if(rruTypeItem.rruTypeName.StartsWith("p"))
                {
                    type = 1;
                }
                mibValue = IROfpTransPlanSpeedToMibExcelValueToMib(type, line, rowRec["支持的IR口速率"].ToString(), out result);
                if (null == mibValue)
                {
                    return false;
                }
                rruTypeItem.rruTypeNotMibIrRate = mibValue;
                mibValue = IrCompressModeWithBandwidthExcelValueToMib(line, rowRec["压缩属性与带宽约束关系"].ToString(), out result);
                if (null == mibValue)
                {
                    return false;
                }        
                rruTypeItem.rruTypeNotMibIrBand = mibValue;
                //增加是否为pico的属性
                rruTypeItem.rruTypeNotMibIsPico = rruTypeItem.rruTypeName.StartsWith("p") ? 1 : 0;
                AddRruTypeElement(rruTypeJArray, rruTypeItem);

                //通道信息
                //rruTypePort中厂家信息是一样的
                rruTypePortItem.rruTypePortManufacturerIndex = rruTypeItem.rruTypeManufacturerIndex;
                rruTypePortItem.rruTypePortNotMibManufacturerName = rruTypeItem.rruTypeNotMibManufacturerName;
                rruTypePortItem.rruTypePortIndex = int.Parse(rowRec["RRU硬件类型"].ToString());
                rruTypePortItem.rruTypePortNo = int.Parse(rowRec["射频通道编号"].ToString());
                rruTypePortItem.rruTypePortPathNo = int.Parse(rowRec["所属天线编号"].ToString());
                mibValue = SupportFreqBandExcelValueToMib(line, rowRec["通道支持频段"].ToString(), out result);
                if (null == mibValue)
                {
                    return false;
                }
                rruTypePortItem.rruTypePortSupportFreqBand = mibValue;
                rruTypePortItem.rruTypePortSupportFreqBandWidth = int.Parse(rowRec["通道支持带宽（M）"].ToString());
                Dictionary<string, int> carrierNumFACarrierNum = TdsCarrierNumExcelValueToMib(rowRec["各频段支持的载波数"].ToString(), rowRec["通道支持频段"].ToString());
                if(carrierNumFACarrierNum.ContainsKey("Error"))
                {
                    MessageBox.Show(rowRec["RRU名称"].ToString() + " 各频段支持的载波数 与 通道支持频段 填写不匹配");
                }
                rruTypePortItem.rruTypePortSupportAbandTdsCarrierNum = carrierNumFACarrierNum["A"];
                rruTypePortItem.rruTypePortSupportFBandTdsCarrierNum = carrierNumFACarrierNum["F"];
                rruTypePortItem.rruTypePortCalAIqTxNom = int.Parse(rowRec["通道TX基带信号的标定振幅"].ToString());
                rruTypePortItem.rruTypePortCalAIqRxNom = int.Parse(rowRec["通道RX基带信号的标定振幅"].ToString());
                rruTypePortItem.rruTypePortCalPoutTxNom = int.Parse(rowRec["通道天线单元的标定输出(1/256dbm)"].ToString());
                rruTypePortItem.rruTypePortCalPinRxNom = int.Parse(rowRec["通道天线单元的标定输入(1/256dbm)"].ToString());
                rruTypePortItem.rruTypePortAntMaxPower = int.Parse(rowRec["通道支持的最大发送功率"].ToString());
                //收发属性目前无列名,要求基站填写
                if(null != rowRec["通道收发"])
                {
                    mibValue = TxRxStatusExcelValueToMib(line, rowRec["通道收发"].ToString(), out result);
                    if (null == mibValue)
                    {
                        return false;
                    }
                    rruTypePortItem.rruTypePortNotMibRxTxStatus = mibValue;
                }
                else
                {
                    rruTypePortItem.rruTypePortNotMibRxTxStatus = "";
                }
                AddRruTypePortElement(rruTypePortJArray, rruTypePortItem);
                line++;
            }
            //为了生成的文档方便查看，不用列表进行序列化，前面将list赋值代码删除了
            //string jsonTypeSerial = JsonConvert.SerializeObject(rruTypeList);
            //string jsonPortSerial = JsonConvert.SerializeObject(rruTypeList);
            //JObject jsonData = new JObject { { "rruTypeInfo", jsonTypeSerial }, { "rruTypePortInfo", jsonPortSerial } };
            JObject jsonData = new JObject { { "rruTypeInfo", rruTypeJArray }, { "rruTypePortInfo", rruTypePortJArray } };
            JsonFile jsonFile = new JsonFile();
            jsonFile.WriteFile(@".\output\NetPlanElement_RruType.json", jsonData.ToString());
            Log.Info("======parse " + fileName + "into output\\NetPlanElement_RruType.json ok");
            return true;
        }

    }
}

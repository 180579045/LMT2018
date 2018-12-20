using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows;
using MIBDataParser.JSONDataMgr;
using LogManager;
using System.Data;

namespace RruAntAlarmError
{
    class AntennaInfoParser
    {
        JObject jCfgObject;
        JObject infoJObect;
        JArray antennaTypeJArray;
        JArray antennaWeightJArray;
        JArray couplingCoeffctJArray;
        JArray bfScanWeightArray;

        public AntennaInfoParser()
        {
            JsonFile jsonFile = new JsonFile();
            this.jCfgObject = jsonFile.ReadJsonFileForJObject(@".\cfg\parsecolumns_cfg.json");
            antennaTypeJArray = new JArray();
            antennaWeightJArray = new JArray();
            couplingCoeffctJArray = new JArray();
            bfScanWeightArray = new JArray();
            infoJObect = new JObject();
        }

        public bool parseAntennaInfoToJsonFile(bool isFirstRowColumn, string fileName)
        {
            if (false == parseOriginSheet("原始值", isFirstRowColumn, fileName))
            {
                Log.Error("==== parse sheet:原始值 in " + fileName + " error, stop parse antennainfo");
                return false;
            }
            if (false == parseAntennaCouplingCoeffct("耦合系数原始值", isFirstRowColumn, fileName))
            {
                Log.Error("==== parse sheet:耦合系数原始值 in " + fileName + " error, stop parse antennainfo");
                return false;
            }

            if (false == parseAntennaBfScanWeight("波束扫描原始值", isFirstRowColumn, fileName))
            {
                Log.Error("==== parse sheet:波束扫描原始值 in " + fileName + " error, stop parse antennainfo");
                return false;
            }

            JsonFile jsonFile = new JsonFile();
            infoJObect.Add("antennaTypeTable", antennaTypeJArray);
            infoJObect.Add("antennaWeightTable", antennaWeightJArray);
            infoJObect.Add("couplingCoeffctTable", couplingCoeffctJArray);
            infoJObect.Add("bfScanWeightTable", bfScanWeightArray);
            jsonFile.WriteFile(@".\output\NetPlanElement_AntennaInfo.json", infoJObect.ToString());
            Log.Info("======parse " + fileName + "into output\\NetPlanElement_AntennaInfo.json ok");
            return true;
        }

        //天线阵/多天线权值解析
        public bool parseOriginSheet(string sheetName, bool isFirstRowColumn, string fileName)
        {
            Log.Info("begin to parse sheet:" + sheetName);
            DataSet dataSet = ExcelReadWrite.ExcelToDataSet(sheetName, isFirstRowColumn, fileName);
            if (null == dataSet)
            {
                Log.Error("read ExcelToDataSet file: " + fileName + " sheet:" + sheetName + "fail");
                return false;
            }

            if (false == getAntennaTypeInfo(dataSet.Tables[0].Rows))
            {
                return false;
            }
            if (false == getAntennaWeightInfo(dataSet.Tables[0].Rows))
            {
                return false;
            }
            //if (rowRec[columnAmpName].ToString().Trim().Equals(""))

            return true;
        }

        private string AntArrayTypeExcelValueToMib(int line, string excelValue)
        {
            string result = "";
            string split = excelValue.Substring(0, excelValue.IndexOf(":")); //excel中格式 2:极化阵
            //存储为MIB取值0:ula|线阵/1:uca|圆阵/2:polar|极化阵 这种格式
            string managerRange = jCfgObject["antArrayType"].ToString();
            int manufacturerIndex = int.Parse(split);
            string[] managerRangeArray = managerRange.Split('/');
            foreach (string temp in managerRangeArray)
            {
                int tempIndex = int.Parse(temp.Substring(0, excelValue.IndexOf(":")));
                if (manufacturerIndex == tempIndex)
                {
                    result = "";
                    JArray objJArray = new JArray() { };
                    JObject objRec = new JObject
                    {
                        {"value", tempIndex.ToString()},
                        {"desc", temp.Substring(excelValue.IndexOf(":") + 1)}
                    };
                    objJArray.Add(objRec);
                    return objJArray.ToString();
                }
            }
            result = "权值信息表 excel Line " + line + " value is not in antArrayType, excel is " + excelValue;
            MessageBox.Show(result);
            Log.Error(result);
            return null;
        }

        private string netAntArrayLossFlagExcelValueToMib(int line, string excelValue)
        {
            //excelValue excel中格式 有损
            //存储为MIB取值0:ula|线阵/1:uca|圆阵/2:polar|极化阵 这种格式
            string managerRange = jCfgObject["netAntArrayLossFlag"].ToString();
            string[] managerRangeArray = managerRange.Split('/');
            string result = "";
            JArray objJArray = new JArray() { };
            if (null == excelValue || "" == excelValue)
            {
                //部分天线阵该列值为空，默认填写为0
                string defaultValue = managerRangeArray[0];
                JObject objRec = new JObject
                {
                    {"value", defaultValue.Substring(0, defaultValue.IndexOf(":"))},
                    {"desc", defaultValue.Substring(defaultValue.IndexOf(":") + 1)}
                };
                objJArray.Add(objRec);
                return objJArray.ToString();
            }

            foreach (string temp in managerRangeArray)
            {
                int tempIndex = temp.IndexOf(":");
                if (temp.Substring(tempIndex + 1).Contains(excelValue))
                {
                    result = "";
                    JObject objRec = new JObject
                    {
                        {"value", temp.Substring(0, tempIndex)},
                        {"desc", temp.Substring(tempIndex + 1)}
                    };
                    objJArray.Add(objRec);
                    return objJArray.ToString();
                }
            }
            result = "权值信息表 excel Line " + line + " value is not in netAntArrayLossFlag, excel is " + excelValue;
            MessageBox.Show(result);
            Log.Error(result);
            return null;
        }

        private void AddAntennaTypeElement(AntennaArrayTypeTable table)
        {
            JObject tableObject = new JObject
            {
                {"antArrayNotMibNumber", table.antArrayNotMibNumber},
                {"antArrayVendor", table.antArrayVendor},
                {"antArrayNotMibVendorName", table.antArrayNotMibVendorName},
                {"antArrayIndex", table.antArrayIndex},
                {"antArrayModelName", table.antArrayModelName},
                {"antArrayNum", table.antArrayNum},
                {"antArrayDistance", table.antArrayDistance},
                {"antArrayType", JArray.Parse(table.antArrayType)},
                {"antArrayNotMibAntLossFlag", JArray.Parse(table.antArrayNotMibAntLossFlag)},
                {"netAntArrayNotMibHalfPowerBeamWidth", JArray.Parse(table.netAntArrayNotMibHalfPowerBeamWidth)}
            };
            foreach (var item in antennaTypeJArray)
            {
                if (item["antArrayVendor"].ToString().Equals(table.antArrayVendor.ToString())
                    && item["antArrayIndex"].ToString().Equals(table.antArrayIndex.ToString()))
                {
                    return;
                }
            }
            antennaTypeJArray.Add(tableObject);
        }

        public bool getAntennaTypeInfo(DataRowCollection rows)
        {
            int line = 2;
            string excelValue;
            AntennaArrayTypeTable antennaArrayTypeTable = new AntennaArrayTypeTable();
            int indexFlag = -1; //天线阵编号标识
            string result;
            JArray HalfPowerBeamWidthJArray = null;
            List<string> antHalfPowerBeamWidthList = new List<string>();
            int count = rows.Count;
            foreach (DataRow rowRec in rows)
            {
                string mibValue;
                try
                {
                    int antIndex = 0;
                    excelValue = rowRec["天线编号:antIndex"].ToString();
                    if (excelValue.Equals(""))
                    {
                        result = "权值信息表 excel sheet原始值  Line " + line + " 天线编号:antIndex value is invalid";
                        MessageBox.Show(result);
                        Log.Error(result);
                        return false;
                    }
                    else
                    {
                        //保存信息
                        antIndex = int.Parse(rowRec["天线编号:antIndex"].ToString());
                    }
                    //查到新的天线阵类型，需要做两件事：
                    //(1)将旧的天线阵类型保存到json天线阵数组中
                    //(2)记录antennaArrayTypeTable新的天线阵信息
                    if (indexFlag != -1 && antIndex != indexFlag)
                    {
                        HalfPowerBeamWidthJArray = new JArray();
                        foreach (string half in antHalfPowerBeamWidthList)
                        {
                            JObject objRec = new JObject
                            {
                                {"value", half},
                                {"desc", ""}
                            };
                            HalfPowerBeamWidthJArray.Add(objRec);
                        }
                        antennaArrayTypeTable.netAntArrayNotMibHalfPowerBeamWidth = HalfPowerBeamWidthJArray.ToString();
                        AddAntennaTypeElement(antennaArrayTypeTable);
                        //清空list
                        antHalfPowerBeamWidthList.Clear();
                    }

                    antennaArrayTypeTable.antArrayNotMibNumber = antIndex;
                    excelValue = rowRec["天线厂家名称:antVendorName"].ToString();
                    antennaArrayTypeTable.antArrayVendor = int.Parse(excelValue.Substring(0, excelValue.IndexOf(":")));
                    antennaArrayTypeTable.antArrayNotMibVendorName = excelValue.Substring(excelValue.IndexOf(":") + 1);
                    excelValue = rowRec["天线型号:antMode"].ToString();
                    antennaArrayTypeTable.antArrayIndex = int.Parse(excelValue.Substring(0, excelValue.IndexOf(":")));
                    antennaArrayTypeTable.antArrayModelName = excelValue.Substring(excelValue.IndexOf(":") + 1);
                    antennaArrayTypeTable.antArrayNum = int.Parse(rowRec["天线根数:antNum"].ToString());
                    antennaArrayTypeTable.antArrayDistance = int.Parse(rowRec["天线间距:antDistance（单位0.1mm）"].ToString());
                    mibValue = AntArrayTypeExcelValueToMib(line, rowRec["天线类型:antType"].ToString());
                    if (null == mibValue)
                    {
                        return false;
                    }
                    antennaArrayTypeTable.antArrayType = mibValue;
                    mibValue = netAntArrayLossFlagExcelValueToMib(line, rowRec["有损无损:antLossFlag"].ToString());
                    if (null == mibValue)
                    {
                        return false;
                    }
                    antennaArrayTypeTable.antArrayNotMibAntLossFlag = mibValue;

                    //与上条记录一致，则需要做一件事：
                    //(1)将天线半功率角信息去重保存
                    string temp = rowRec["天线半功率角:antHalfPowerBeamWidth(单位º)"].ToString();
                    if (!antHalfPowerBeamWidthList.Contains(temp.Trim()))
                    {
                        antHalfPowerBeamWidthList.Add(temp.Trim());
                    }

                    //最后一款天线阵保存到json数组中
                    if ((line - 1) == count)
                    {
                        HalfPowerBeamWidthJArray = new JArray();
                        foreach (string half in antHalfPowerBeamWidthList)
                        {
                            JObject objRec = new JObject
                            {
                                {"value", half}
                            };
                            HalfPowerBeamWidthJArray.Add(objRec);
                        }
                        antennaArrayTypeTable.netAntArrayNotMibHalfPowerBeamWidth = HalfPowerBeamWidthJArray.ToString();
                        AddAntennaTypeElement(antennaArrayTypeTable);
                    }
                    indexFlag = antennaArrayTypeTable.antArrayNotMibNumber;
                    line++;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    result = "Line " + line + " exception: " + e.ToString();
                    Log.Error(result);
                    return false;
                }
            }
            return true;
        }

        private void AddAntennaWeightOneRecord(JArray oneAntennaWeightJArray, AntennaWeightTable table)
        {
            JObject tableObject = new JObject
            {
                {"antennaWeightMultFrequencyBand", table.antennaWeightMultFrequencyBand},
                {"antennaWeightMultAntGrpIndex", table.antennaWeightMultAntGrpIndex},
                {"antennaWeightMultAntStatusIndex", table.antennaWeightMultAntStatusIndex},
                {"antennaWeightMultNotMibAntStatus", table.antennaWeightMultNotMibAntStatus},
                {"antennaWeightMultAntHalfPowerBeamWidth", table.antennaWeightMultAntHalfPowerBeamWidth},
                {"antennaWeightMultAntVerHalfPowerBeamWidth", table.antennaWeightMultAntVerHalfPowerBeamWidth},
                {"antennaWeightMultAntAmplitude0", table.antennaWeightMultAntAmplitude0},
                {"antennaWeightMultAntPhase0", table.antennaWeightMultAntPhase0},
                {"antennaWeightMultAntAmplitude1", table.antennaWeightMultAntAmplitude1},
                {"antennaWeightMultAntPhase1", table.antennaWeightMultAntPhase1},
                {"antennaWeightMultAntAmplitude2", table.antennaWeightMultAntAmplitude2},
                {"antennaWeightMultAntPhase2", table.antennaWeightMultAntPhase2},
                {"antennaWeightMultAntAmplitude3", table.antennaWeightMultAntAmplitude3},
                {"antennaWeightMultAntPhase3", table.antennaWeightMultAntPhase3},
                {"antennaWeightMultAntAmplitude4", table.antennaWeightMultAntAmplitude4},
                {"antennaWeightMultAntPhase4", table.antennaWeightMultAntPhase4},
                {"antennaWeightMultAntAmplitude5", table.antennaWeightMultAntAmplitude5},
                {"antennaWeightMultAntPhase5", table.antennaWeightMultAntPhase5},
                {"antennaWeightMultAntAmplitude6", table.antennaWeightMultAntAmplitude6},
                {"antennaWeightMultAntPhase6", table.antennaWeightMultAntPhase6},
                {"antennaWeightMultAntAmplitude7", table.antennaWeightMultAntAmplitude7},
                {"antennaWeightMultAntPhase7", table.antennaWeightMultAntPhase7},
            };
            oneAntennaWeightJArray.Add(tableObject);
        }

        private int round(double val)
        {
            const double c_Median = 0.5;
            return (val - (int) (val - c_Median) >= c_Median ? (int) (val + c_Median) : (int) (val - c_Median));
        }

        public bool getAntennaWeightInfo(DataRowCollection rows)
        {
            int line = 2;
            string excelValue;
            int indexFlag = -1; //天线阵编号标识
            string result;
            JArray oneAntennaWeightJArray = new JArray();

            AntennaWeightTable antennaWeightTable = new AntennaWeightTable();
            //考虑到使用该器件信息主要是以某类型天线阵，下发一个全集的权值，
            //故设计json文件时,采用一个JArray，每个对象标识一款天线阵及基权值信息，而该权值信息不再分层，而是平铺     
            foreach (DataRow rowRec in rows)
            {
                string mibValue;
                try
                {
                    if (rowRec["频段信息:antFrequencyBand"].ToString().Equals(""))
                    {
                        //没有权值，跳过
                        line++;
                        continue;
                    }
                    int antIndex = 0;
                    excelValue = rowRec["天线编号:antIndex"].ToString();
                    if (excelValue.Equals(""))
                    {
                        result = "权值信息表 excel sheet原始值 Line " + line + " 天线编号:antIndex value is invalid";
                        MessageBox.Show(result);
                        Log.Error(result);
                        return false;
                    }
                    else
                    {
                        //保存信息
                        antIndex = int.Parse(rowRec["天线编号:antIndex"].ToString());
                    }

                    //查到新的天线阵类型，将旧的天线阵类型权值保存到json天线阵数组中
                    if (indexFlag != -1 && antIndex != indexFlag)
                    {
                        JObject oneAntennaJObject = new JObject
                        {
                            {"antArrayNotMibNumber", antennaWeightTable.antArrayNotMibNumber},
                            {"antArrayMultWeight", oneAntennaWeightJArray}
                        };
                        antennaWeightJArray.Add(oneAntennaJObject);
                        oneAntennaWeightJArray.Clear();
                    }

                    excelValue = rowRec["频段信息:antFrequencyBand"].ToString().Trim();
                    antennaWeightTable.antArrayNotMibNumber = antIndex;
                    antennaWeightTable.antennaWeightMultFrequencyBand =
                        int.Parse(excelValue.Substring(0, excelValue.IndexOf(':')));
                    antennaWeightTable.antennaWeightMultAntStatusIndex = (1 << int.Parse(rowRec["天线状态索引"].ToString()));
                    antennaWeightTable.antennaWeightMultNotMibAntStatus = rowRec["天线状态"].ToString();
                    excelValue = rowRec["天线半功率角:antHalfPowerBeamWidth(单位º)"].ToString().Trim();
                    string[] split = excelValue.Split(',');
                    if (split.Length == 1)
                    {
                        antennaWeightTable.antennaWeightMultAntVerHalfPowerBeamWidth = -1; //MIB中表示无效值
                    }
                    else
                    {
                        //垂直方向
                        antennaWeightTable.antennaWeightMultAntVerHalfPowerBeamWidth = int.Parse(split[1].ToString());
                    }
                    antennaWeightTable.antennaWeightMultAntHalfPowerBeamWidth = int.Parse(split[0].ToString());
                    //32个天线按照8天线一组
                    for (int grpLoop = 0; grpLoop < 4; grpLoop++)
                    {
                        string columnAmpName = "ANT" + (0 + 8 * grpLoop) + "(幅度)\n[V]";
                        string columnPhaName = "ANT" + (0 + 8 * grpLoop) + "(相位)\n[deg]";
                        if (rowRec[columnAmpName].ToString().Trim().Equals(""))
                        {
                            break;
                        }
                        //给相位四舍五入
                        antennaWeightTable.antennaWeightMultAntGrpIndex = grpLoop;
                        antennaWeightTable.antennaWeightMultAntAmplitude0 =
                            (int) (double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100);
                        antennaWeightTable.antennaWeightMultAntPhase0 =
                            round((double.Parse(rowRec[columnPhaName].ToString().Trim())));
                        columnAmpName = "ANT" + (1 + 8 * grpLoop) + "(幅度)\n[V]";
                        columnPhaName = "ANT" + (1 + 8 * grpLoop) + "(相位)\n[deg]";
                        antennaWeightTable.antennaWeightMultAntAmplitude1 =
                            (int) (double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100);
                        antennaWeightTable.antennaWeightMultAntPhase1 =
                            round((double.Parse(rowRec[columnPhaName].ToString().Trim())));
                        columnAmpName = "ANT" + (2 + 8 * grpLoop) + "(幅度)\n[V]";
                        columnPhaName = "ANT" + (2 + 8 * grpLoop) + "(相位)\n[deg]";
                        antennaWeightTable.antennaWeightMultAntAmplitude2 =
                            (int) (double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100);
                        antennaWeightTable.antennaWeightMultAntPhase2 =
                            round((double.Parse(rowRec[columnPhaName].ToString().Trim())));
                        columnAmpName = "ANT" + (3 + 8 * grpLoop) + "(幅度)\n[V]";
                        columnPhaName = "ANT" + (3 + 8 * grpLoop) + "(相位)\n[deg]";
                        antennaWeightTable.antennaWeightMultAntAmplitude3 =
                            (int) (double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100);
                        antennaWeightTable.antennaWeightMultAntPhase3 =
                            round((double.Parse(rowRec[columnPhaName].ToString().Trim())));
                        columnAmpName = "ANT" + (4 + 8 * grpLoop) + "(幅度)\n[V]";
                        columnPhaName = "ANT" + (4 + 8 * grpLoop) + "(相位)\n[deg]";
                        antennaWeightTable.antennaWeightMultAntAmplitude4 =
                            (int) (double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100);
                        antennaWeightTable.antennaWeightMultAntPhase4 =
                            round((double.Parse(rowRec[columnPhaName].ToString().Trim())));
                        columnAmpName = "ANT" + (5 + 8 * grpLoop) + "(幅度)\n[V]";
                        columnPhaName = "ANT" + (5 + 8 * grpLoop) + "(相位)\n[deg]";
                        antennaWeightTable.antennaWeightMultAntAmplitude5 =
                            (int) (double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100);
                        antennaWeightTable.antennaWeightMultAntPhase5 =
                            round((double.Parse(rowRec[columnPhaName].ToString().Trim())));
                        columnAmpName = "ANT" + (6 + 8 * grpLoop) + "(幅度)\n[V]";
                        columnPhaName = "ANT" + (6 + 8 * grpLoop) + "(相位)\n[deg]";
                        antennaWeightTable.antennaWeightMultAntAmplitude6 =
                            (int) (double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100);
                        antennaWeightTable.antennaWeightMultAntPhase6 =
                            round((double.Parse(rowRec[columnPhaName].ToString().Trim())));
                        columnAmpName = "ANT" + (7 + 8 * grpLoop) + "(幅度)\n[V]";
                        columnPhaName = "ANT" + (7 + 8 * grpLoop) + "(相位)\n[deg]";
                        antennaWeightTable.antennaWeightMultAntAmplitude7 =
                            (int) (double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100);
                        antennaWeightTable.antennaWeightMultAntPhase7 =
                            round((double.Parse(rowRec[columnPhaName].ToString().Trim())));
                        AddAntennaWeightOneRecord(oneAntennaWeightJArray, antennaWeightTable);
                    }
                    //最后一条记录
                    if ((line - 1) == rows.Count)
                    {
                        JObject oneAntennaJObject = new JObject
                        {
                            {"antArrayNotMibNumber", antennaWeightTable.antArrayNotMibNumber},
                            {"antArrayMultWeight", oneAntennaWeightJArray}
                        };
                        antennaWeightJArray.Add(oneAntennaJObject);
                        oneAntennaWeightJArray.Clear();
                    }
                    indexFlag = antIndex;
                    line++;
                }
                catch (Exception e)
                {
                    result = "Line " + line + " exception: " + e.ToString();
                    Log.Error(result);
                    return false;
                }
            }
            return true;
        }

        private bool checkCouplingCoeffctIndexExist(int index)
        {
            foreach (var item in antennaTypeJArray) //移除mJObj  无效
            {
                if (((JObject) item)["antArrayNotMibNumber"].ToString() == index.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        private int calculateAmplitude(double originValue, double minValue)
        {
            double powValue = (minValue - originValue) / 20;
            double temp = Math.Pow(10.00, powValue);
            return round(100 * temp);
        }

        private bool getMinAmplitude(object[] obj, int start, int length, out double minValue)
        {
            double minValueTemp = 65535;
            if ((start + length - 1) > obj.Length)
            {
                //异常保护
                minValue = 0.0000;
                return false;
            }
            //取幅度的最小值
            for (int loop = 0; loop < length; loop++)
            {
                try
                {
                    double loopValue = double.Parse(obj[start + loop].ToString().Trim());
                    if (loopValue < minValueTemp)
                    {
                        minValueTemp = loopValue;
                    }
                }
                catch (Exception e)
                {
                    string result = "column " + (start + loop) + "is invalid";
                    MessageBox.Show(result);
                    Log.Error(result);
                    minValue = 0.0000;
                    return false;
                }
            }
            minValue = minValueTemp;
            return true;
        }

        private void AddCouplingCoeffctOneRecord(JArray oneCouplingCoeffctJArray, AntennaCouplingCoeffctTable table)
        {
            JObject tableObject = new JObject
            {
                {"antCouplCoeffFreq", table.antCouplCoeffFreq},
                {"antCouplCoeffAntGrpIndex", table.antCouplCoeffAntGrpIndex},
                {"antCouplCoeffAmplitude0", table.antCouplCoeffAmplitude0},
                {"antCouplCoeffPhase0", table.antCouplCoeffPhase0},
                {"antCouplCoeffAmplitude1", table.antCouplCoeffAmplitude1},
                {"antCouplCoeffPhase1", table.antCouplCoeffPhase1},
                {"antCouplCoeffAmplitude2", table.antCouplCoeffAmplitude2},
                {"antCouplCoeffPhase2", table.antCouplCoeffPhase2},
                {"antCouplCoeffAmplitude3", table.antCouplCoeffAmplitude3},
                {"antCouplCoeffPhase3", table.antCouplCoeffPhase3},
                {"antCouplCoeffAmplitude4", table.antCouplCoeffAmplitude4},
                {"antCouplCoeffPhase4", table.antCouplCoeffPhase4},
                {"antCouplCoeffAmplitude5", table.antCouplCoeffAmplitude5},
                {"antCouplCoeffPhase5", table.antCouplCoeffPhase5},
                {"antCouplCoeffAmplitude6", table.antCouplCoeffAmplitude6},
                {"antCouplCoeffPhase6", table.antCouplCoeffPhase6},
                {"antCouplCoeffAmplitude7", table.antCouplCoeffAmplitude7},
                {"antCouplCoeffPhase7", table.antCouplCoeffPhase7}
            };
            oneCouplingCoeffctJArray.Add(tableObject);
        }

        //耦合系数
        public bool parseAntennaCouplingCoeffct(string sheetName, bool isFirstRowColumn, string fileName)
        {
            Log.Info("begin to parse sheet:" + sheetName);
            DataSet dataSet = ExcelReadWrite.ExcelToDataSet(sheetName, isFirstRowColumn, fileName);
            if (null == dataSet)
            {
                Log.Error("read ExcelToDataSet file: " + fileName + " sheet:" + sheetName + "fail");
                return false;
            }
            int line = 2;
            string excelValue = "";
            int indexFlag = -1; //天线阵编号标识
            string result = "";
            JArray oneAntennaCouplingCoeffctJArray = new JArray();

            AntennaCouplingCoeffctTable couplingCoeffctTable = new AntennaCouplingCoeffctTable();
            //考虑到使用该器件信息主要是以某类型天线阵，下发一个全集的权值，
            //故设计json文件时,采用一个JArray，每个对象标识一款天线阵及基权值信息，而该权值信息不再分层，而是平铺     
            foreach (DataRow rowRec in dataSet.Tables[0].Rows)
            {
                string mibValue = "";
                try
                {
                    int antIndex = 0;
                    excelValue = rowRec["天线编号:antIndex"].ToString();
                    if (excelValue.Equals(""))
                    {
                        result = "权值信息表 sheet" + sheetName + " Line " + line + " 天线编号:antIndex value is invalid";
                        MessageBox.Show(result);
                        Log.Error(result);
                        return false;
                    }
                    else
                    {
                        //保存信息
                        antIndex = int.Parse(rowRec["天线编号:antIndex"].ToString());
                    }
                    //进行一层校验保护，该天线编号必须在原始值sheet而存在
                    if (false == checkCouplingCoeffctIndexExist(antIndex))
                    {
                        result = "天线编号:antIndex " + antIndex + " is not in sheet 原始值, Line " + line;
                        MessageBox.Show(result);
                        Log.Error(result);
                        return false;
                    }

                    //查到新的天线阵类型，将旧的天线阵类型权值保存到json天线阵数组中
                    if (indexFlag != -1 && antIndex != indexFlag)
                    {
                        JObject onecouplingCoeffctObject = new JObject
                        {
                            {"antArrayNotMibNumber", couplingCoeffctTable.antArrayNotMibNumber},
                            {"antArrayCouplingCoeffctInfo", oneAntennaCouplingCoeffctJArray}
                        };
                        couplingCoeffctJArray.Add(onecouplingCoeffctObject);
                        oneAntennaCouplingCoeffctJArray.Clear();
                    }

                    couplingCoeffctTable.antArrayNotMibNumber = antIndex;
                    couplingCoeffctTable.antCouplCoeffFreq =
                        (int) (double.Parse(rowRec["频点值:antFrequency"].ToString().Trim()) * 10);
                    Object[] itemArray = rowRec.ItemArray;
                    //取幅度最小值
                    double minValue;
                    if (false == getMinAmplitude(itemArray, 4, 64, out minValue))
                    {
                        return false;
                    }
                    //64个天线按照8天线一组
                    for (int grpLoop = 0; grpLoop < 8; grpLoop++)
                    {
                        string columnAmpName = "ANTCPL" + (0 + 8 * grpLoop) + "(幅度)\n[V]";
                        string columnPhaName = "ANTCPL" + (0 + 8 * grpLoop) + "(相位)\n[deg]";
                        if (rowRec[columnAmpName].ToString().Trim().Equals(""))
                        {
                            break;
                        }

                        couplingCoeffctTable.antCouplCoeffAntGrpIndex = grpLoop;
                        couplingCoeffctTable.antCouplCoeffAmplitude0 =
                            calculateAmplitude(double.Parse(rowRec[columnAmpName].ToString().Trim()), minValue);
                        columnAmpName = "ANTCPL" + (1 + 8 * grpLoop) + "(幅度)\n[V]";
                        couplingCoeffctTable.antCouplCoeffAmplitude1 =
                            calculateAmplitude(double.Parse(rowRec[columnAmpName].ToString().Trim()), minValue);
                        columnAmpName = "ANTCPL" + (2 + 8 * grpLoop) + "(幅度)\n[V]";
                        couplingCoeffctTable.antCouplCoeffAmplitude2 =
                            calculateAmplitude(double.Parse(rowRec[columnAmpName].ToString().Trim()), minValue);
                        columnAmpName = "ANTCPL" + (3 + 8 * grpLoop) + "(幅度)\n[V]";
                        couplingCoeffctTable.antCouplCoeffAmplitude3 =
                            calculateAmplitude(double.Parse(rowRec[columnAmpName].ToString().Trim()), minValue);
                        columnAmpName = "ANTCPL" + (4 + 8 * grpLoop) + "(幅度)\n[V]";
                        couplingCoeffctTable.antCouplCoeffAmplitude4 =
                            calculateAmplitude(double.Parse(rowRec[columnAmpName].ToString().Trim()), minValue);
                        columnAmpName = "ANTCPL" + (5 + 8 * grpLoop) + "(幅度)\n[V]";
                        couplingCoeffctTable.antCouplCoeffAmplitude5 =
                            calculateAmplitude(double.Parse(rowRec[columnAmpName].ToString().Trim()), minValue);
                        columnAmpName = "ANTCPL" + (6 + 8 * grpLoop) + "(幅度)\n[V]";
                        couplingCoeffctTable.antCouplCoeffAmplitude6 =
                            calculateAmplitude(double.Parse(rowRec[columnAmpName].ToString().Trim()), minValue);
                        columnAmpName = "ANTCPL" + (7 + 8 * grpLoop) + "(幅度)\n[V]";
                        couplingCoeffctTable.antCouplCoeffAmplitude7 =
                            calculateAmplitude(double.Parse(rowRec[columnAmpName].ToString().Trim()), minValue);
                        couplingCoeffctTable.antCouplCoeffPhase0 =
                            -round(double.Parse(rowRec[columnPhaName].ToString().Trim()));
                        columnPhaName = "ANTCPL" + (1 + 8 * grpLoop) + "(相位)\n[deg]";
                        couplingCoeffctTable.antCouplCoeffPhase1 =
                            -round(double.Parse(rowRec[columnPhaName].ToString().Trim()));
                        columnPhaName = "ANTCPL" + (2 + 8 * grpLoop) + "(相位)\n[deg]";
                        couplingCoeffctTable.antCouplCoeffPhase2 =
                            -round(double.Parse(rowRec[columnPhaName].ToString().Trim()));
                        columnPhaName = "ANTCPL" + (3 + 8 * grpLoop) + "(相位)\n[deg]";
                        couplingCoeffctTable.antCouplCoeffPhase3 =
                            -round(double.Parse(rowRec[columnPhaName].ToString().Trim()));
                        columnPhaName = "ANTCPL" + (4 + 8 * grpLoop) + "(相位)\n[deg]";
                        couplingCoeffctTable.antCouplCoeffPhase4 =
                            -round(double.Parse(rowRec[columnPhaName].ToString().Trim()));
                        columnPhaName = "ANTCPL" + (5 + 8 * grpLoop) + "(相位)\n[deg]";
                        couplingCoeffctTable.antCouplCoeffPhase5 =
                            -round(double.Parse(rowRec[columnPhaName].ToString().Trim()));
                        columnPhaName = "ANTCPL" + (6 + 8 * grpLoop) + "(相位)\n[deg]";
                        couplingCoeffctTable.antCouplCoeffPhase6 =
                            -round(double.Parse(rowRec[columnPhaName].ToString().Trim()));
                        columnPhaName = "ANTCPL" + (7 + 8 * grpLoop) + "(相位)\n[deg]";
                        couplingCoeffctTable.antCouplCoeffPhase7 =
                            -round(double.Parse(rowRec[columnPhaName].ToString().Trim()));
                        AddCouplingCoeffctOneRecord(oneAntennaCouplingCoeffctJArray, couplingCoeffctTable);
                    }

                    //最后一条记录
                    if ((line - 1) == dataSet.Tables[0].Rows.Count)
                    {
                        JObject onecouplingCoeffctObject = new JObject
                        {
                            {"antArrayNotMibNumber", couplingCoeffctTable.antArrayNotMibNumber},
                            {"antArrayCouplingCoeffctInfo", oneAntennaCouplingCoeffctJArray}
                        };
                        couplingCoeffctJArray.Add(onecouplingCoeffctObject);
                        oneAntennaCouplingCoeffctJArray.Clear();
                    }
                    indexFlag = antIndex;
                    line++;
                }
                catch (Exception e)
                {
                    result = "Line " + line + " exception: " + e.ToString();
                    Log.Error(result);
                    return false;
                }
            }
            return true;
        }

        private bool splitKeyValue(string excelValue, string sign, out string key, out string value)
        {
            key = "";
            value = "";
            if (-1 == excelValue.IndexOf(sign))
            {
                return false;
            }
            key = excelValue.Substring(0, excelValue.IndexOf(sign));
            value = excelValue.Substring(excelValue.IndexOf(sign) + sign.Length);

            return true;
        }

        private bool checkBfScanExist(int line, AntennaBfScanWeightTable bfscanTable)
        {
            foreach (var antType in antennaTypeJArray)
            {
                if (antType["antArrayNotMibNumber"].ToString().Equals(bfscanTable.antArrayNotMibNumber)
                    && antType["antArrayVendor"].ToString().Equals(bfscanTable.antArrayBfScanAntWeightVendorIndex)
                    && antType["antArrayIndex"].ToString().Equals(bfscanTable.antArrayBfScanAntWeightTypeIndex))
                {
                    return true;
                }
            }
            recordLog("波束扫描原始值 sheet" + " Line " + line + " 中的天线阵在原始值sheet页中不存在");
            return false;
        }

        private void recordLog(string msg)
        {
            MessageBox.Show(msg);
            Log.Error(msg);
        }

        private bool setBFScanVendorInfo(string excelValue, int line, AntennaBfScanWeightTable bfscanTable)
        {
            string key, value;
            if (false == splitKeyValue(excelValue, ":", out key,
                    out value))
            {
                return false;
            }
            bfscanTable.antArrayBfScanAntWeightVendorIndex = key;
            bfscanTable.antArrayBfScanNotMibVendorName = value;
            return true;
        }

        private bool setBFScanAntTypeInfo(string excelValue, int line, AntennaBfScanWeightTable bfscanTable)
        {
            string key, value;
            if (false == splitKeyValue(excelValue, ":", out key,
                    out value))
            {
                recordLog("波束扫描原始值 sheet" + " Line " + line + "天线型号:antMode的格式填写不正确: " + excelValue);
                return false;
            }
            bfscanTable.antArrayBfScanAntWeightTypeIndex = key;
            bfscanTable.antArrayBfScanNotMibTypeName = value;
            return true;
        }

        private bool setBFScanLossFlagInfo(string excelValue, int line, AntennaBfScanWeightTable bfscanTable)
        {
            if (excelValue.Trim().Equals("有损"))
            {
                bfscanTable.antennaBfScanWeightIsLossFlag = "1";
            }
            else if(excelValue.Trim().Equals("无损"))
            {
                bfscanTable.antennaBfScanWeightIsLossFlag = "0";
            }
            else
            {
                recordLog("波束扫描原始值 sheet" + " Line " + line + "有损无损:antLossFlag的格式填写不正确: " + excelValue);
                return false;
            }
            return true;
        }
        private void AddBfScanOneRecord(AntennaBfScanWeightTable table)
        {
            JObject tableObject = new JObject
            {
                {"antArrayNotMibNumber", table.antArrayNotMibNumber},
                {"antArrayBfScanAntWeightVendorIndex", table.antArrayBfScanAntWeightVendorIndex},
                {"antArrayBfScanNotMibVendorName", table.antArrayBfScanNotMibVendorName},
                {"antArrayBfScanAntWeightTypeIndex", table.antArrayBfScanAntWeightTypeIndex},
                {"antArrayBfScanNotMibTypeName", table.antArrayBfScanNotMibTypeName},
                {"antArrayBfScanAntWeightIndex", table.antArrayBfScanAntWeightIndex},
                {"antennaBfScanWeightBFScanGrpNo", table.antennaBfScanWeightBFScanGrpNo},
                {"antArrayBfScanAntWeightAntGrpNo", table.antArrayBfScanAntWeightAntGrpNo},
                {"antennaBfScanWeightAmplitude0", table.antennaBfScanWeightAmplitude0},
                {"antennaBfScanWeightPhase0", table.antennaBfScanWeightPhase0},
                {"antennaBfScanWeightAmplitude1", table.antennaBfScanWeightAmplitude1},
                {"antennaBfScanWeightPhase1", table.antennaBfScanWeightPhase1},
                {"antennaBfScanWeightAmplitude2", table.antennaBfScanWeightAmplitude2},
                {"antennaBfScanWeightPhase2", table.antennaBfScanWeightPhase2},
                {"antennaBfScanWeightAmplitude3", table.antennaBfScanWeightAmplitude3},
                {"antennaBfScanWeightPhase3", table.antennaBfScanWeightPhase3},
                {"antennaBfScanWeightAmplitude4", table.antennaBfScanWeightAmplitude4},
                {"antennaBfScanWeightPhase4", table.antennaBfScanWeightPhase4},
                {"antennaBfScanWeightAmplitude5", table.antennaBfScanWeightAmplitude5},
                {"antennaBfScanWeightPhase5", table.antennaBfScanWeightPhase5},
                {"antennaBfScanWeightAmplitude6", table.antennaBfScanWeightAmplitude6},
                {"antennaBfScanWeightPhase6", table.antennaBfScanWeightPhase6},
                {"antennaBfScanWeightAmplitude7", table.antennaBfScanWeightAmplitude7},
                {"antennaBfScanWeightPhase7", table.antennaBfScanWeightPhase7},
                {"antennaBfScanWeightHorizonNum", table.antennaBfScanWeightHorizonNum},
                {"antennaBfScanWeightVerticalNum", table.antennaBfScanWeightVerticalNum},
                {"antennaBfScanWeightHorizonDowntiltAngle", table.antennaBfScanWeightHorizonDowntiltAngle},
                {"antennaBfScanWeightVerticalDowntiltAngle", table.antennaBfScanWeightVerticalDowntiltAngle},
                {"antennaBfScanWeightIsLossFlag", table.antennaBfScanWeightIsLossFlag}

            };
            bfScanWeightArray.Add(tableObject);
        }

        private bool AddBfScanIndex5Record(int line, int index3Value, int index4Value, DataRow rowRec)
        {
            //32个天线按照8天线一组, 对应第五维索引
            for (int index5Valueloop = 0; index5Valueloop < 4; index5Valueloop++)
            {
                string columnAmpName = "ANTCPL" + (0 + 8 * index5Valueloop) + "(幅度)\n[V]";
                string columnPhaName = "ANTCPL" + (0 + 8 * index5Valueloop) + "(相位)\n[deg]";
                if (rowRec[columnAmpName].ToString().Trim().Equals(""))
                {
                    break;
                }
                AntennaBfScanWeightTable bfscanTable = new AntennaBfScanWeightTable();
                bfscanTable.antArrayNotMibNumber = rowRec["天线编号:antIndex"].ToString().Trim();
                if (false == setBFScanVendorInfo(rowRec["天线厂家名称:antVendorName"].ToString(), line, bfscanTable))
                {
                    return false;
                }
                if (false == setBFScanAntTypeInfo(rowRec["天线型号:antMode"].ToString().Trim(), line, bfscanTable))
                {
                    return false;
                }
                //作校验，需要在antennaTypeJArray中存在此天线阵
                if (!checkBfScanExist(line, bfscanTable))
                {
                    return false;
                }
                bfscanTable.antArrayBfScanAntWeightIndex = Convert.ToString(index3Value);
                bfscanTable.antennaBfScanWeightBFScanGrpNo = Convert.ToString(index4Value);
                bfscanTable.antArrayBfScanAntWeightAntGrpNo = Convert.ToString(index5Valueloop);
                bfscanTable.antennaBfScanWeightAmplitude0 = ((int)(double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100)).ToString();
                columnAmpName = "ANTCPL" + (1 + 8 * index5Valueloop) + "(幅度)\n[V]";
                bfscanTable.antennaBfScanWeightAmplitude1 = ((int)(double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100)).ToString();
                columnAmpName = "ANTCPL" + (2 + 8 * index5Valueloop) + "(幅度)\n[V]";
                bfscanTable.antennaBfScanWeightAmplitude2 = ((int)(double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100)).ToString();
                columnAmpName = "ANTCPL" + (3 + 8 * index5Valueloop) + "(幅度)\n[V]";
                bfscanTable.antennaBfScanWeightAmplitude3 = ((int)(double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100)).ToString();
                columnAmpName = "ANTCPL" + (4 + 8 * index5Valueloop) + "(幅度)\n[V]";
                bfscanTable.antennaBfScanWeightAmplitude4 = ((int)(double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100)).ToString();
                columnAmpName = "ANTCPL" + (5 + 8 * index5Valueloop) + "(幅度)\n[V]";
                bfscanTable.antennaBfScanWeightAmplitude5 = ((int)(double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100)).ToString();
                columnAmpName = "ANTCPL" + (6 + 8 * index5Valueloop) + "(幅度)\n[V]";
                bfscanTable.antennaBfScanWeightAmplitude6 = ((int)(double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100)).ToString();
                columnAmpName = "ANTCPL" + (7 + 8 * index5Valueloop) + "(幅度)\n[V]";
                bfscanTable.antennaBfScanWeightAmplitude7 = ((int)(double.Parse(rowRec[columnAmpName].ToString().Trim()) * 100)).ToString();

                bfscanTable.antennaBfScanWeightPhase0 =
                    (round((double.Parse(rowRec[columnPhaName].ToString().Trim())))).ToString();
                columnPhaName = "ANTCPL" + (1 + 8 * index5Valueloop) + "(相位)\n[deg]";
                bfscanTable.antennaBfScanWeightPhase1 =
                    (round((double.Parse(rowRec[columnPhaName].ToString().Trim())))).ToString();
                columnPhaName = "ANTCPL" + (2 + 8 * index5Valueloop) + "(相位)\n[deg]";
                bfscanTable.antennaBfScanWeightPhase2 =
                    (round((double.Parse(rowRec[columnPhaName].ToString().Trim())))).ToString();
                columnPhaName = "ANTCPL" + (3 + 8 * index5Valueloop) + "(相位)\n[deg]";
                bfscanTable.antennaBfScanWeightPhase3 =
                    (round((double.Parse(rowRec[columnPhaName].ToString().Trim())))).ToString();
                columnPhaName = "ANTCPL" + (4 + 8 * index5Valueloop) + "(相位)\n[deg]";
                bfscanTable.antennaBfScanWeightPhase4 =
                    (round((double.Parse(rowRec[columnPhaName].ToString().Trim())))).ToString();
                columnPhaName = "ANTCPL" + (5 + 8 * index5Valueloop) + "(相位)\n[deg]";
                bfscanTable.antennaBfScanWeightPhase5 =
                    (round((double.Parse(rowRec[columnPhaName].ToString().Trim())))).ToString();
                columnPhaName = "ANTCPL" + (6 + 8 * index5Valueloop) + "(相位)\n[deg]";
                bfscanTable.antennaBfScanWeightPhase6 =
                    (round((double.Parse(rowRec[columnPhaName].ToString().Trim())))).ToString();
                columnPhaName = "ANTCPL" + (7 + 8 * index5Valueloop) + "(相位)\n[deg]";
                bfscanTable.antennaBfScanWeightPhase7 =
                    (round((double.Parse(rowRec[columnPhaName].ToString().Trim())))).ToString();
                bfscanTable.antennaBfScanWeightHorizonNum = rowRec["水平方向波束个数:horBeamScanning"].ToString().Trim();
                bfscanTable.antennaBfScanWeightVerticalNum = rowRec["垂直方向波束个数:verBeamScanning"].ToString().Trim();
                bfscanTable.antennaBfScanWeightHorizonDowntiltAngle = rowRec["水平方向数字下倾角:horDowntiltAngle(单位º)"].ToString().Trim();
                bfscanTable.antennaBfScanWeightVerticalDowntiltAngle = rowRec["垂直方向数字下倾角:verDowntiltAngle(单位º)"].ToString().Trim();
                if (!setBFScanLossFlagInfo(rowRec["有损无损:antLossFlag"].ToString().Trim(), line, bfscanTable))
                {
                    return false;
                }
                AddBfScanOneRecord(bfscanTable);
            }
            return true;
        }

        public bool parseAntennaBfScanWeight(string sheetName, bool isFirstRowColumn, string fileName)
        {
            Log.Info("begin to parse sheet:" + sheetName);
            DataSet dataSet = ExcelReadWrite.ExcelToDataSet(sheetName, isFirstRowColumn, fileName);
            if (null == dataSet)
            {
                Log.Error("read ExcelToDataSet file: " + fileName + " sheet:" + sheetName + "fail");
                return false;
            }
            string arrayNumber = ""; //存储大排行号，即excel表中第一列值 
            string result;
            int index3Value = -1; //第三维索引起始值
            int indexFlag = -1; //第三维索引切换标识
            for (int loop = 0; loop < dataSet.Tables[0].Rows.Count;)
            {
                DataRow rowRec = dataSet.Tables[0].Rows[loop];
                int line = loop + 2;
                string excelValue = rowRec["天线编号:antIndex"].ToString();
                if (string.IsNullOrEmpty(excelValue))
                {
                    recordLog(sheetName + " Line " + line + " 天线编号:antIndex value is invalid");
                    return false;
                }
                try
                {
                    if (!excelValue.Equals(arrayNumber))
                    {
                        //后面统一处理
                    }
                    else
                    {
                        //新的天线阵类型，重置第三维相关索引信息，恢复为初始值
                        index3Value = -1;
                        indexFlag = -1;
                    }

                    int horizonNum = Convert.ToInt32(rowRec["水平方向波束个数:horBeamScanning"].ToString().Trim());
                    int verticalNum = Convert.ToInt32(rowRec["垂直方向波束个数:verBeamScanning"].ToString().Trim());
                    //对应第四维索引的个数
                    int maxBFScanGrpNo = horizonNum + verticalNum;
                    //增加保护，MIB取值范围目前最大为7
                    if (maxBFScanGrpNo > 8)
                    {
                        recordLog("波束扫描原始值 sheet" + sheetName + " Line " + line +
                                  " 水平方向波束个数+垂直方向波束个数超过了波束扫描组号的最大值");
                        return false;
                    }
                    if (indexFlag != maxBFScanGrpNo)
                    {
                        //第三维索引加1，新记录
                        index3Value++;
                        //循环进行第四维索引保存表记录,最多maxBFScanGrpNo
                        for (int index4Valueloop = 0; index4Valueloop < maxBFScanGrpNo; index4Valueloop++)
                        {
                            DataRow rowRecGrp = dataSet.Tables[0].Rows[loop + index4Valueloop];
                            //32个天线按照8天线一组对应第五维索引
                            if (!AddBfScanIndex5Record(line, index3Value, index4Valueloop, rowRecGrp))
                            {
                                return false;
                            }
                            line++;
                        }
                        loop = loop + maxBFScanGrpNo;
                        indexFlag = maxBFScanGrpNo;
                    }
                    else
                    {
                        recordLog(sheetName + " Line " + line + " maxBFScanGrpNo(水平方向波束个数+垂直方向波束个数)出现重复的，请检查");
                        return false;
                    }               
                }
                catch(Exception e)
                {
                    result = "Line " + line + " exception: " + e.ToString();
                    Log.Error(result);
                    return false;
                }
            }
            return true;
        }
    }
}

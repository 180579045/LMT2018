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
    class AlarmInfoParser
    {
        JObject jCfgObject;
        public AlarmInfoParser()
        {
            JsonFile jsonFile = new JsonFile();
            this.jCfgObject = jsonFile.ReadJsonFileForJObject(@".\cfg\parsecolumns_cfg.json");
        }
        private string getAlarmType(int line, string excelvalue)
        {
            string result;
            if (excelvalue.Equals(""))
            {
                result = "告警信息表 excel Line " + line + " 告警类型 value is null ";
                MessageBox.Show(result);
                Log.Error(result);
                return null;
            }
            string alarmTypeNumResult = "";
            if ("1" == excelvalue)
            {
                alarmTypeNumResult = excelvalue + " - " + "通信告警";
            }
            else if ( "2" == excelvalue)
            {
                alarmTypeNumResult = excelvalue + " - " + "服务器质量告警";
            }
            else if ("3" == excelvalue)
            {
                alarmTypeNumResult = excelvalue + " - " + "处理器告警";
            }
            else if ("4" == excelvalue)
            {
                alarmTypeNumResult = excelvalue + " - " + "设备告警";
            }
            else if ("5" == excelvalue)
            {
                alarmTypeNumResult = excelvalue + " - " + "环境告警";
            }
            else
            {
                result = "告警信息表 excel Line " + line + " 告警类型 value is not support " + excelvalue;
                MessageBox.Show(result);
                Log.Error(result);
                return null;
            }
            return alarmTypeNumResult;
        }
        private string getAlarmSeverity(int line, string excelvalue)
        {
            string result;
            if (excelvalue.Equals(""))
            {
                result = "告警信息表 excel Line " + line + " 厂家告警级别 value is null ";
                MessageBox.Show(result);
                Log.Error(result);
                return null;
            }
            string alarmTypeNumResult = "";
            if ("1" == excelvalue)
            {
                alarmTypeNumResult = excelvalue + " - " + "严重告警";
            }
            else if ("2" == excelvalue)
            {
                alarmTypeNumResult = excelvalue + " - " + "主要告警";
            }
            else if ("3" == excelvalue)
            {
                alarmTypeNumResult = excelvalue + " - " + "次要告警";
            }
            else if ("4" == excelvalue)
            {
                alarmTypeNumResult = excelvalue + " - " + "警告告警";
            }
            else
            {
                result = "告警信息表 excel Line " + line + " 厂家告警级别 value is not support " + excelvalue;
                MessageBox.Show(result);
                Log.Error(result);
                return null;
            }
            return alarmTypeNumResult;

        }
        private string getAllConsequenceResult(string equipmentresult, string businessreult, string equipmentdetail, string businessdetail)
        {
            string allstr = "";
            allstr = "对设备的影响：" + equipmentresult.ToString() + "\n" + "对业务的影响：" + businessreult.ToString() + "\n" + "对设备的影响的详细描述：" + equipmentdetail.ToString() + "\n" + "对业务的影响的详细描述：" + equipmentdetail.ToString();
            return allstr;
        }
        private string getAllRepairAdvise(string procedureDispose, string preDispose, string remoteDispose, string nearDispose)
        {
            string allstr = "";
            allstr = "程序自动后处理：" + procedureDispose.ToString() + "\n" + "预处理操作建议" + preDispose.ToString() + "\n" + "远端处理建议:" + remoteDispose + "\n" + "近端处理建议:" + nearDispose;
            return allstr;
        }
        private int getAlarmIsStateful(string excelvalue)
        {
            if (excelvalue == "Y")
            {
                //故障类告警填写为1
                return 1;
            }
            else
            {
                return 0;
            }
        }
        private string getAlarmSubCause(int line, string excelvalue, string excelMeaningValue)
        {
            string result;
            JArray subCauseJArray = new JArray();

            //目前只有告警值含义描述取值为2时，告警子原因才有用
            if("2" != excelMeaningValue.Trim())
            {
                JObject objRec = new JObject{
                              { "value", "" },
                              { "desc", excelvalue}
                             };
                subCauseJArray.Add(objRec);
                return subCauseJArray.ToString();
            }

            if (excelvalue.Equals(""))
            {
                result = "告警信息表 excel Line " + line + " 告警子原因 value is null ";
                MessageBox.Show(result);
                Log.Error(result);
                return null;
            }
            int index = excelvalue.IndexOf(":");
            //文档中有不带编号的子原因
            if(-1 == index)
            {
                JObject objRec = new JObject{
                              { "value", "" },
                              { "desc", excelvalue}
                             };
                subCauseJArray.Add(objRec);
                result = "告警信息表 excel Line " + line + " 告警子原因 不包含数值 " + excelvalue;
                Log.Warn(result);
                return subCauseJArray.ToString();
            }
            string[] rangeArray = excelvalue.Split('/');
            foreach (string temp in rangeArray)
            {
                if(temp.Equals(""))
                {
                    result = "告警信息表 excel Line " + line + " 告警子原因 最后多了一个/ " + excelvalue;
                    Log.Warn(result);
                    return subCauseJArray.ToString();
                }
                //存在三种写法： 0:@@@@/1:@@@@  , 0..10:@@@@  ，1-254:告警位置标识/255:无效
                string frontValue = temp.Substring(0, temp.IndexOf(":"));
                int frontIndex = frontValue.IndexOf("..");
                int symbolLength = ("..").Length;
                if(-1 == frontIndex)
                {
                    frontIndex = frontValue.IndexOf("-");
                    symbolLength = ("-").Length;
                }
                if (-1!= frontIndex)
                {
                    int start = int.Parse(frontValue.Substring(0, frontIndex));
                    int end = int.Parse(frontValue.Substring(frontIndex + symbolLength));
                    for (int loop = start; loop < end; loop++)
                    {
                        JObject objRec = new JObject{
                              { "value", loop.ToString() },
                              { "desc", temp.Substring(0, temp.IndexOf(":") + 1)}
                             };
                        subCauseJArray.Add(objRec);
                    }
                }
                else
                {
                    int tempIndex = int.Parse(frontValue);

                    JObject objRec = new JObject{
                              { "value", tempIndex.ToString() },
                              { "desc", temp.Substring(temp.IndexOf(":"))}
                             };
                    subCauseJArray.Add(objRec);
                }
            }
            return subCauseJArray.ToString();
        }
        private string getFaultObjectType(int line, string excelValue)
        {
            string result;
            JArray objJArray = new JArray() { };
            if (excelValue.Equals("") || excelValue.Equals("无"))
            {
                result = "Line " + line + " 故障对象名称 value is 空" + excelValue;
                Log.Warn(result);
                return objJArray.ToString();
            }
            //可以不使用取值，直接填写
            int index = excelValue.IndexOf(":");
            if(-1 == index)
            {
                result = "alarm excel Line " + line + " 故障对象名称 value is invalid " + excelValue;
                MessageBox.Show(result);
                Log.Error(result);
                return null;
            }
            JObject objRec = new JObject{
                              { "value", excelValue.Substring(0, index) },
                              { "desc", excelValue.Substring(index + 1)}
                             };
            objJArray.Add(objRec);
            return objJArray.ToString();
            /*
            string split = excelValue.Substring(0, excelValue.IndexOf(":"));
            int fileIndex = int.Parse(split);
            string managerRange = jCfgObject["alarmCauseFaultObjectType"].ToString();
            string[] managerRangeArray = managerRange.Split('/');
            foreach (string temp in managerRangeArray)
            {
                int tempIndex = int.Parse(temp.Substring(0, temp.IndexOf(":")));
                if (fileIndex == tempIndex)
                {
                    result = "";
                    JObject objRec = new JObject{
                              { "value", tempIndex.ToString() },
                              { "desc", temp.Substring(temp.IndexOf(":"))}
                             };
                    objJArray.Add(objRec);
                    return objJArray.ToString();
                }
                //0表示所有对象
                else if (0 == fileIndex)
                {
                    JObject objRec = new JObject{
                              { "value", tempIndex.ToString() },
                              { "desc", temp.Substring(temp.IndexOf(":"))}
                             };
                    objJArray.Add(objRec);
                }
            }
            if(0 == fileIndex)
            {
                return objJArray.ToString();
            }
            result = "alarm excel Line " + line + " 故障对象名称 value is not in alarmCauseFaultObjectType, excel is " + excelValue;
            MessageBox.Show(result);
            Log.Error(result);
            */
            return null;
        }
        private void addAlarmInfoElement(JArray alarmInfoArray, AlarmTable alarm)
        {
            JObject alarmobject = new JObject {
                {"alarmCauseNo", alarm.alarmCauseNo},
                { "alarmModel", alarm.alarmModel},
                { "alarmIsStateful", alarm.alarmIsStateful},
                { "alarmChineseDesc", alarm.alarmChineseDesc},
                { "alarmSubCauseNo", JArray.Parse(alarm.alarmSubCauseNo)},
                { "alarmType", alarm.alarmType},
                { "alarmSeverity", alarm.alarmSeverity},
                { "alarmExplain", alarm.alarmExplain},
                { "alarmConsequence", alarm.alarmConsequence},
                { "alarmRepairAdvise", alarm.alarmRepairAdvise},
                { "alarmFaultObjectType", JArray.Parse(alarm.alarmFaultObjectType)}
            };
            alarmInfoArray.Add(alarmobject);
        }

        public bool parseAlarmInfoToJsonFile(string sheetName, bool isFirstRowColumn, string fileName)
        {
            JArray alarmInfoArray = new JArray();
            DataSet dataSet = ExcelReadWrite.ExcelToDataSet(sheetName, isFirstRowColumn, fileName);
            if (null == dataSet)
            {
                return false;
            }
            AlarmTable alarmTable = new AlarmTable();
            string excelValue;
            int line = 2;
            string result;
            string lastValue;
            foreach (DataRow rowRec in dataSet.Tables[0].Rows)
            {
                excelValue = rowRec["告警编号"].ToString();
                if (excelValue.Equals(""))
                {
                    result = "告警信息表 excel Line " + line + " 告警编号 value is invalid";
                    MessageBox.Show(result);
                    Log.Error(result);
                    return false;
                }
                else
                {
                    //tds的告警不再进行解析，取值范围20000~23999，其中有的带*都是TDS的
                    if(excelValue.Contains("*"))
                    {
                        continue;
                    }
                    int causeNo = int.Parse(excelValue);
                    if (causeNo >= 20000 && causeNo < 23999)
                    {
                        continue;
                    }
                    //保存信息
                    alarmTable.alarmCauseNo = int.Parse(excelValue);
                }
                alarmTable.alarmModel = rowRec["模块名称"].ToString();
                alarmTable.alarmIsStateful = getAlarmIsStateful(rowRec["是否为故障类告警"].ToString());
                alarmTable.alarmChineseDesc = rowRec["中文描述"].ToString();
                lastValue = getAlarmSubCause(line, rowRec["告警子原因描述"].ToString(), rowRec["告警值含义描述"].ToString());
                if(null == lastValue)
                {
                    return false;
                }
                alarmTable.alarmSubCauseNo = lastValue;
                lastValue = getAlarmType(line, rowRec["告警类型"].ToString());
                if (null == lastValue)
                {
                    return false;
                }     
                alarmTable.alarmType = lastValue;
                lastValue = getAlarmSeverity(line, rowRec["厂家告警级别"].ToString());
                if (null == lastValue)
                {
                    return false;
                }
                alarmTable.alarmSeverity = lastValue;
                alarmTable.alarmExplain = rowRec["告警解释"].ToString();
                alarmTable.alarmConsequence = getAllConsequenceResult(rowRec["对设备的影响"].ToString(), rowRec["对业务的影响"].ToString(), rowRec["对设备的影响详细描述"].ToString(), rowRec["对业务的影响描述"].ToString());
                alarmTable.alarmRepairAdvise = getAllRepairAdvise(rowRec["程序自动后处理"].ToString(), rowRec["预处理操作建议"].ToString(), rowRec["远端处理建议"].ToString(), rowRec["近端处理建议"].ToString());
                lastValue = getFaultObjectType(line, rowRec["故障对象名称"].ToString());
                if (null == lastValue)
                {
                    return false;
                }
                alarmTable.alarmFaultObjectType = lastValue;
                addAlarmInfoElement(alarmInfoArray, alarmTable);
                line++;
            }
            JObject jsonData = new JObject { { "AlarmInfo", alarmInfoArray } };
            JsonFile jsonFile = new JsonFile();
            jsonFile.WriteFile(@".\output\AlarmInfo.json", jsonData.ToString());
            Log.Info("======parse " + fileName + "into output\\AlarmInfo.json ok");

            return true;
        }
    }
}

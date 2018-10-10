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
    class ErrorCodeParser
    {
        JArray ErrorCodeJArray;
        public ErrorCodeParser()
        {
            ErrorCodeJArray = new JArray();
        }
        public bool parseErrorCodeToJsonFile(string sheetName, bool isFirstRowColumn, string fileName)
        {
            DataSet dataSet = ExcelReadWrite.ExcelToDataSet(sheetName, isFirstRowColumn, fileName);
            if (null == dataSet)
            {
                return false;
            }
            string result = "";
            int line = 2;//与excel表中的行号保持一致
            foreach (DataRow rowRec in dataSet.Tables[0].Rows)
            {
                try
                {
                    int errorID = int.Parse(rowRec["全局编号"].ToString());
                    string errorChDesc = rowRec["错误描述_CH"].ToString();
                    string errorEnDesc = rowRec["错误描述_EN"].ToString();
                    JObject obj = new JObject{
                        { "errorID", errorID},
                        { "errorChDesc", errorChDesc},
                        { "errorEnDesc", errorEnDesc}
                    };
                    ErrorCodeJArray.Add(obj);
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
            JObject jsonData = new JObject { { "errorCodeInfo", ErrorCodeJArray} };
            JsonFile jsonFile = new JsonFile();
            jsonFile.WriteFile(@".\output\ErrorCodeInfo.json", jsonData.ToString());
            Log.Info("======parse " + fileName + "into output\\ErrorCodeInfo.json ok");
            return true;
        }
    }
}

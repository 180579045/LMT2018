using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System;
using System.IO;
using System.Collections;

namespace MIBDataParser.JSONDataMgr
{
    class ObjTreeBase
    {
        public string objNameCh;
        public string objNameChParent;
        public int level;
        public string objNameMibTable;
    }
    class ObjTreeInfo
    {
        public string objNameCh;
        public string objNameChParent;
        public int level;
        public string objNameMibTable;
        public ObjTreeInfo[] childObj = null;
    }

    /// <summary>
    /// Tree_Reference.json 的节点
    /// </summary>
    class ObjTreeReference
    {
        public int objID;//对象的id
        public int objParentID;//父节点id
        public int childRenCount;//子节点个数
        public string objName;//"双模基站"
        public string objNameEn;
        public string mibTableName;//"equipmentSysInfo"
        public string mibList;
    }

    class ObjTressJsonData
    {
        public ObjTreeInfo objTreeInfo;
        string mibVersion;
        string stringObjTreeJson;

        private JObject treeReferenceJson;

        public ObjTressJsonData(string mibVersion)
        {
            this.mibVersion = mibVersion;
            this.objTreeInfo = new ObjTreeInfo();
            this.stringObjTreeJson = "";
        }

        public ObjTressJsonData()
        {
            this.objTreeInfo = new ObjTreeInfo();
            this.mibVersion = "";
            this.stringObjTreeJson = "";

        }

        public void SetObjTreeMibVersion(string mibVersion)
        {
            this.mibVersion = mibVersion;
            return;
        }

        public string GetStringObjTreeJson()
        {
            return this.stringObjTreeJson;
        }
        public string GetStringTreeReference()
        {
            return this.treeReferenceJson.ToString();
        }

        //获取一个父节点下的所有叶子节点信息
        public ObjTreeBase[] GetRecordByObjNameCh(DataSet mibDataSet, string parentName)
        {
            List<ObjTreeBase> objTreeBaseList = new List<ObjTreeBase>();
            foreach (DataRow rowRec in mibDataSet.Tables[0].Rows)
            {
                if (0 == string.Compare(rowRec["ObjParent"].ToString(), parentName))
                {
                    string objMibTables = rowRec["ObjMibTables"].ToString();
                    string realMibName = "";
                    if (true == objMibTables.EndsWith("Table"))
                    {
                        int lastIndex = objMibTables.LastIndexOf("Table");
                        realMibName = objMibTables.Substring(0, lastIndex) + "Entry";
                    }
                    else
                    {
                        realMibName = objMibTables;
                    }
                    ObjTreeBase tempObj = new ObjTreeBase();
                    tempObj.objNameCh = rowRec["ObjName"].ToString();
                    tempObj.objNameChParent = rowRec["ObjParent"].ToString();
                    tempObj.level = int.Parse(rowRec["ObjLevel"].ToString());
                    tempObj.objNameMibTable = realMibName;
                    objTreeBaseList.Add(tempObj);
                }
            }

            return objTreeBaseList.ToArray();
        }
        public void creatTheTree(DataSet mibDataSet, string parentName, ObjTreeInfo info)
        {
            //获取
            ObjTreeBase[] items = GetRecordByObjNameCh(mibDataSet, parentName);
            //如果没有字节点了，那就返回空
            if (0 == items.Length)
            {
                return;
            }
            List<ObjTreeInfo> infoList = new List<ObjTreeInfo>();
            for (int i = 0; i < items.Length; i++)
            {
                ObjTreeInfo tempInfo = new ObjTreeInfo();
                
                tempInfo.objNameCh = items[i].objNameCh;
                tempInfo.objNameChParent = items[i].objNameChParent;
                tempInfo.level = items[i].level;
                tempInfo.objNameMibTable = items[i].objNameMibTable;
                //递归循环
                creatTheTree(mibDataSet, items[i].objNameCh.ToString(), tempInfo);
                infoList.Add(tempInfo);
            }
            info.childObj = infoList.ToArray(); //由于对象是引用类型，因为可以改变参数的值
        }
    
        public void ObjParseDataSet(DataSet dataInput)
        {
            //将第一级根目录
            DataRow row = dataInput.Tables[0].Rows[0];
            this.objTreeInfo.objNameCh = row["ObjName"].ToString();
            this.objTreeInfo.objNameChParent = "";
            this.objTreeInfo.objNameMibTable = row["ObjMibTables"].ToString();
            this.objTreeInfo.level = 0;

            creatTheTree(dataInput, row["ObjName"].ToString(), this.objTreeInfo);
            JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
            jsonSetting.DefaultValueHandling = DefaultValueHandling.Include;
            this.stringObjTreeJson = JsonConvert.SerializeObject(objTreeInfo, Formatting.Indented, jsonSetting);
            return;
        }
        //public void TreeReferenceParseDataSetOld(DataSet dataInput)
        //{
        //    JObject objRec = new JObject();

        //    Dictionary<string, int> objIdList = new Dictionary<string, int>();
        //    JArray objJArray = new JArray();
        //    int objId = 1;
        //    int tableNum = dataInput.Tables[0].Rows.Count;
        //    for (int loop = 0; loop < tableNum - 1; loop++)
        //    {
        //        DataRow row = dataInput.Tables[0].Rows[loop];
        //        JObject objOne = new JObject();
        //        //
        //        objOne.Add("ObjID", objId);
        //        if (0 == objIdList.Count)
        //        {
        //            objOne.Add("ObjParentID", 0);
        //        }
        //        else
        //        {
        //            objOne.Add("ObjParentID", objIdList[row["ObjParent"].ToString()]);
        //        }
        //        objOne.Add("ChildRenCount", "");//"ChildRenCount":21,
        //        objOne.Add("ObjName", row["ObjName"].ToString());//"ObjName":"双模基站",    
        //        objOne.Add("ObjNameEn", "");//"ObjNameEn":"TLSNB",                                         
        //        objOne.Add("MibTableName", row["ObjMibTables"].ToString());//"MibTableName":"equipmentSysInfo",
        //        objOne.Add("MIBList", "");//"MIBList":null
        //        try
        //        {
        //            objIdList.Add(row["ObjName"].ToString(), objId);
        //        }
        //        catch {
        //            //Console.WriteLine("TreeReferenceParseDataSet ({0}) is chongfu.", row["ObjName"].ToString());
        //        }
        //        objId += 1;
        //        objJArray.Add(objOne);
        //    }
        //    objRec.Add("version", this.mibVersion);
        //    objRec.Add("NodeList", objJArray);

        //    this.treeReferenceJson = objRec;
        //}
        //public void TreeReferenceParseDataSetOld2(DataSet dataInput)
        //{
        //    Dictionary<string, int> objIdList = new Dictionary<string, int>();

        //    JObject objTreeReference = new JObject();
        //    JArray objJArray = new JArray();
        //    int selfId = 1;
        //    for (int loop = 0; loop < dataInput.Tables[0].Rows.Count - 1; loop++)
        //    {
        //        DataRow row = dataInput.Tables[0].Rows[loop];
        //        JObject objOne = ParseRowObj(selfId, row, objIdList);
        //        if (objIdList.ContainsKey(row["ObjName"].ToString()) == false)
        //        {
        //            objIdList.Add(row["ObjName"].ToString(), selfId);
        //        }
        //        selfId += 1;
        //        objJArray.Add(objOne);
        //    }
        //    objTreeReference.Add("version", this.mibVersion);
        //    objTreeReference.Add("NodeList", objJArray);

        //    this.treeReferenceJson = objTreeReference;
        //}
        public void TreeReferenceParseDataSet(DataSet dataInput)
        {
            /// key : 中文名字"ObjName"， value:id no. 。
            Dictionary<string, int> objDict = new Dictionary<string, int>();
            /// 对象的列表
            JArray objJArray = new JArray();

            int id_n = 1;
            for (int loop = 0; loop < dataInput.Tables[0].Rows.Count - 1; loop++)
            {
                DataRow row = dataInput.Tables[0].Rows[loop];
                // 去重
                if (objDict.ContainsKey(row["ObjName"].ToString()))
                {
                    //Console.WriteLine("tree reference obj name {0} is repeat.", row["ObjName"].ToString());
                    continue;
                }
                // 添加队列内容
                objDict.Add(row["ObjName"].ToString(), id_n - 1);
                objJArray.Add( TreeReferenceParseRowObj(id_n, row, objDict));
                // 计算子节点个数
                if (objDict.ContainsKey(row["ObjParent"].ToString()))
                {
                    objJArray[objDict[row["ObjParent"].ToString()]]["ChildRenCount"] = 
                        int.Parse(objJArray[objDict[row["ObjParent"].ToString()]]["ChildRenCount"].ToString()) + 1;
                }

                id_n += 1;
            }
            this.treeReferenceJson = new JObject() {
                { "version", this.mibVersion},
                { "NodeList", objJArray},
            };
        }
        private JObject TreeReferenceParseRowObj(int id_n, DataRow row, Dictionary<string, int> objDict)
        {
            return new JObject() {
                    { "ObjID", id_n },
                    { "ObjParentID", TreeReferenceParseDataSetGetParentIdByNameEn(row["ObjParent"].ToString(), objDict) },
                    { "ChildRenCount", 0},                            //"ChildRenCount":21,
                    { "ObjName", row["ObjName"].ToString()},          //"ObjName":"基站信息", 
                    { "ObjNameEn", ""},                               //"ObjNameEn":"StartMode", 
                    {"MibTableName", row["ObjMibTables"].ToString() },//"MibTableName":"sysStartMode",
                    {"MIBList", "" },                                 //"MIBList":null
                };
        }

        private int TreeReferenceParseDataSetGetParentIdByNameEn(string parentNameEn, Dictionary<string, int> objIdList)
        {
            int parentId = 0;
            if (!objIdList.TryGetValue(parentNameEn, out parentId))
            {
                return parentId;
            }
            return parentId+1;
        }

        //private JObject ParseRowObj(int selfId, DataRow row, Dictionary<string, int> objIdList)
        //{
        //    return new JObject() {
        //        { "ObjID", selfId },
        //        { "ObjParentID", GetParentIdByNameEn(row["ObjParent"].ToString(), objIdList)},
        //        { "ChildRenCount", 0},                            //"ChildRenCount":21,
        //        { "ObjName", row["ObjName"].ToString()},          //"ObjName":"基站信息", 
        //        { "ObjNameEn", ""},                               //"ObjNameEn":"StartMode", 
        //        {"MibTableName", row["ObjMibTables"].ToString() },//"MibTableName":"sysStartMode",
        //        {"MIBList", "" },                                 //"MIBList":null
        //    };
        //}

        private int GetParentIdByNameEn(string parentNameEn, Dictionary<string, int> objIdList)
        {
            int parentId = 0;
            objIdList.TryGetValue(parentNameEn, out parentId);
            return parentId;
        }

        private void ObjTreeAdd(DataRow rowRec, JArray objJarray)
        {
            string objMibTables = rowRec["ObjMibTables"].ToString();
            string realMibName = "";
            if (true == objMibTables.EndsWith("Table"))
            {
                int lastIndex = objMibTables.LastIndexOf("Table");
                realMibName = objMibTables.Substring(0, lastIndex) + "Entry";
            }
            else
            {
                realMibName = objMibTables;
            }
            JObject childJObject = new JObject { { "nameCh", rowRec["ObjName"].ToString()},
                { "nameMib", realMibName},
                { "level", rowRec["ObjLevel"].ToString()},
                { "child", rowRec["MIBVal_list"].ToString()} };

            objJarray.Add(childJObject);
            return;
        }
        public void rdbObjTreeToJsonData()
        {
            return;
        }
    }
}

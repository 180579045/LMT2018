using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LogManager;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;
using System.Text.RegularExpressions;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace NetPlan
{
    public enum EnumResultType
    {
        fail = -1,  //失败，指示终止处理
        fail_continue = 0,//失败，但是跳出本次以继续
        success_true = 1,//成功执行规运算,exp的计算结果返回前台指示校验通过
        success_false = 2,//成功执行规运算，exp的计算结果返回前台指示校验失败
    }

    public class NetPlanCheckRules
    {
        public string versionId;
        public List<BaseCheckRule> _netBoardCheck;
        public List<BaseCheckRule> _netRHUBCheck;
        public List<BaseCheckRule> _netRRUCheck;
        public List<BaseCheckRule> _netAntennaArrayCheck;
        public List<BaseCheckRule> _netIROptPlanCheck;
        public List<BaseCheckRule> _netEthPlanCheck;
        public List<BaseCheckRule> _netPRRUCheck;
        public List<BaseCheckRule> _netRRUAntennaSettingCheck;
        public List<BaseCheckRule> _netLocalCellCheck;
        public List<BaseCheckRule> _nrNetLocalCellCheck;
        public List<BaseCheckRule> _netLocalCellCtrlCheck;
        public List<BaseCheckRule> _nrNetLocalCellCtrlCheck;
        public NetPlanCheckRules()
        {
            _netRRUCheck = new List<BaseCheckRule>();
            _netBoardCheck = new List<BaseCheckRule>();
            _netAntennaArrayCheck = new List<BaseCheckRule>();
            _netRHUBCheck = new List<BaseCheckRule>();
            _netIROptPlanCheck = new List<BaseCheckRule>();
            _netEthPlanCheck = new List<BaseCheckRule>();
            _netPRRUCheck = new List<BaseCheckRule>();
            _netRRUAntennaSettingCheck = new List<BaseCheckRule>();
            _netLocalCellCheck = new List<BaseCheckRule>();
            _nrNetLocalCellCheck = new List<BaseCheckRule>();
            _netLocalCellCtrlCheck = new List<BaseCheckRule>();
            _nrNetLocalCellCtrlCheck = new List<BaseCheckRule>();
        }
    }

    public class NPEMibData
    {
        public List<DevAttributeInfo> _netBoard;
        public List<DevAttributeInfo> _netAntennaArray;//rru_ant
        public List<DevAttributeInfo> _netRRU;
        public List<DevAttributeInfo> _netRRUAntennaSetting;
        public List<DevAttributeInfo> _netIROptPlan;//board_rru
        public List<DevAttributeInfo> _netRHUB;
        public List<DevAttributeInfo> _netEthPlan;//rhub_prru
        public List<DevAttributeInfo> _nrNetLocalCell;
        public List<DevAttributeInfo> _netLocalCell;
        public List<DevAttributeInfo> _nrNetLocalCellCtrl;
        public List<DevAttributeInfo> _netLocalCellCtrl;
        public List<DevAttributeInfo> _nrLocalCell;
        public List<DevAttributeInfo> _nrCell;
        public List<DevAttributeInfo> _localCell;
        public List<DevAttributeInfo> _cell;
        public List<DevAttributeInfo> _baseBandwidth;
        //public List<DevAttributeInfo> _netIROptPlan_boardrhub;//board_rhub
        //public List<DevAttributeInfo> _netRRUAntennaSetting_prruant;//prru_ant

        public NPEMibData()
        {
            _netBoard = new List<DevAttributeInfo>();
            _netAntennaArray = new List<DevAttributeInfo>();
            _netRRU = new List<DevAttributeInfo>();
            _netRRUAntennaSetting = new List<DevAttributeInfo>();
            _netIROptPlan = new List<DevAttributeInfo>();
            _netRHUB = new List<DevAttributeInfo>();
            _netEthPlan = new List<DevAttributeInfo>();
            _nrNetLocalCell = new List<DevAttributeInfo>();
            _netLocalCell = new List<DevAttributeInfo>();
            _nrNetLocalCellCtrl = new List<DevAttributeInfo>();
            _netLocalCellCtrl = new List<DevAttributeInfo>();
            _nrLocalCell = new List<DevAttributeInfo>();
            _nrCell = new List<DevAttributeInfo>();
            _localCell = new List<DevAttributeInfo>();
            _cell = new List<DevAttributeInfo>();
            _baseBandwidth = new List<DevAttributeInfo>();
        }
    }

    public class NPELibData
    {
        public List<RruInfo> _rruTypeInfo_lib;
        public List<RruPortInfo> _rruTypePortInfo_lib;
        public List<NetPlanElementType> _netPlanElements_lib;
        public List<Shelf> _shelfEquipment_lib;
        public List<BoardEquipment> _boardEquipment_lib;
        public List<RHUBEquipment> _rHubEquipment_lib;
        public List<AntType> _antType_lib;

        public NPELibData()
        {
            _rruTypeInfo_lib = new List<RruInfo>();
            _rruTypePortInfo_lib = new List<RruPortInfo>();
            _netPlanElements_lib = new List<NetPlanElementType>();
            _shelfEquipment_lib = new List<Shelf>();
            _boardEquipment_lib = new List<BoardEquipment>();
            _rHubEquipment_lib = new List<RHUBEquipment>();
            _antType_lib = new List<AntType>();
        }
    }
    public class NPECheckRulesDeal
    {
        //前台UI初始当前基站mib数据,
        private NPEMibData _npedata_this;
        //器件库中所有的板卡、RHUB、机框等信息
        private NPELibData _npedata_lib;
        //json文件中所有的校验规则
        private NetPlanCheckRules _allRules;
        private int _equipType_this;
        private Dictionary<string, Object> _dicDatasrcUIPara;//key为规则中的数据来源名，value为UI参数
        private Dictionary<string, List<BaseCheckRule>> _sequence;//校验顺序
        private Dictionary<string, List<DevAttributeInfo>> _dicRuleNameUIPara;//key为规则类名,value为对应要校验的UI参数

        public NPELibData getDataLib()
        {
            return _npedata_lib;
        }
        private void SetMapOfDatasrcAndPara()
        {
            //key值为校验规则json文件里用到的集合名称
            //value为对象值
            _dicDatasrcUIPara.Add("this.netBoardEntry", _npedata_this._netBoard);
            _dicDatasrcUIPara.Add("this.netRRUEntry", _npedata_this._netRRU);
            _dicDatasrcUIPara.Add("this.netAntennaArrayEntry", _npedata_this._netAntennaArray);
            _dicDatasrcUIPara.Add("this.netRRUAntennaSettingEntry", _npedata_this._netRRUAntennaSetting);
            _dicDatasrcUIPara.Add("this.netIROptPlanEntry", _npedata_this._netIROptPlan);
            _dicDatasrcUIPara.Add("this.netRHUBEntry", _npedata_this._netRHUB);
            _dicDatasrcUIPara.Add("this.netEthPlanEntry", _npedata_this._netEthPlan);
            _dicDatasrcUIPara.Add("this.netLocalCellEntry", _npedata_this._netLocalCell);
            _dicDatasrcUIPara.Add("this.nrNetLocalCellEntry", _npedata_this._nrNetLocalCell);
            _dicDatasrcUIPara.Add("this.nrNetLocalCellCtrlEntry", _npedata_this._nrNetLocalCellCtrl);
            _dicDatasrcUIPara.Add("this.netLocalCellCtrlEntry", _npedata_this._netLocalCellCtrl);
            _dicDatasrcUIPara.Add("lib.rruTypeInfo", _npedata_lib._rruTypeInfo_lib);//注意，key值"lib.XX"需要与NetPlan_CheckRules.json文件中使用到的器件名一致
            _dicDatasrcUIPara.Add("lib.rruTypePortInfo", _npedata_lib._rruTypePortInfo_lib);
            _dicDatasrcUIPara.Add("lib.netPlanElements", _npedata_lib._netPlanElements_lib);
            _dicDatasrcUIPara.Add("lib.shelfEquipment", _npedata_lib._shelfEquipment_lib);
            _dicDatasrcUIPara.Add("lib.boardEquipment", _npedata_lib._boardEquipment_lib);
            _dicDatasrcUIPara.Add("lib.rHubEquipment", _npedata_lib._rHubEquipment_lib);
            _dicDatasrcUIPara.Add("lib.antennaTypeTable", _npedata_lib._antType_lib);
            _dicDatasrcUIPara.Add("mib.netBoardEntry", _npedata_this._netBoard);
            _dicDatasrcUIPara.Add("mib.netRRUEntry", _npedata_this._netRRU);
            _dicDatasrcUIPara.Add("mib.netAntennaArrayEntry", _npedata_this._netAntennaArray);
            _dicDatasrcUIPara.Add("mib.netRRUAntennaSettingEntry", _npedata_this._netRRUAntennaSetting);
            _dicDatasrcUIPara.Add("mib.netIROptPlanEntry", _npedata_this._netIROptPlan);
            _dicDatasrcUIPara.Add("mib.netRHUBEntry", _npedata_this._netRHUB);
            _dicDatasrcUIPara.Add("mib.netEthPlanEntry", _npedata_this._netEthPlan);
            _dicDatasrcUIPara.Add("mib.nrNetLocalCellEntry", _npedata_this._nrNetLocalCell);
            _dicDatasrcUIPara.Add("mib.netLocalCellEntry", _npedata_this._netLocalCell);
            _dicDatasrcUIPara.Add("mib.nrNetLocalCellCtrlEntry", _npedata_this._nrNetLocalCellCtrl);
            _dicDatasrcUIPara.Add("mib.netLocalCellCtrlEntry", _npedata_this._netLocalCellCtrl);
            _dicDatasrcUIPara.Add("mib.nrLocalCellEntry", _npedata_this._nrLocalCell);
            _dicDatasrcUIPara.Add("mib.localCellEntry", _npedata_this._localCell);
            _dicDatasrcUIPara.Add("mib.cellEntry", _npedata_this._cell);
            _dicDatasrcUIPara.Add("mib.nrCellEntry", _npedata_this._nrCell);
            _dicDatasrcUIPara.Add("mib.baseBandwidthEntry", _npedata_this._baseBandwidth);
        }

        private void SetMapOfRulesNameAndPara()
        {
            //校验规则中，填写对应需要遍历校验的集合
            _dicRuleNameUIPara.Add("netBoardCheck", _npedata_this._netBoard);
            _dicRuleNameUIPara.Add("netRRUCheck", _npedata_this._netRRU);
            _dicRuleNameUIPara.Add("netRHUBCheck", _npedata_this._netRHUB);
            _dicRuleNameUIPara.Add("netPRRUCheck", _npedata_this._netRRU);
            _dicRuleNameUIPara.Add("netIROptPlanCheck", _npedata_this._netIROptPlan);
            _dicRuleNameUIPara.Add("netEthPlanCheck", _npedata_this._netEthPlan);
            _dicRuleNameUIPara.Add("netAntennaArrayCheck", _npedata_this._netAntennaArray);
            _dicRuleNameUIPara.Add("netRRUAntennaSettingCheck", _npedata_this._netRRUAntennaSetting);
            _dicRuleNameUIPara.Add("nrNetLocalCellCheck", _npedata_this._nrNetLocalCell);
            _dicRuleNameUIPara.Add("netLocalCellCheck", _npedata_this._netLocalCell);
            _dicRuleNameUIPara.Add("nrNetLocalCellCtrlCheck", _npedata_this._nrNetLocalCellCtrl);
            _dicRuleNameUIPara.Add("netLocalCellCtrlCheck", _npedata_this._netLocalCellCtrl);
        }

        private void SetSequence()
        {
            //全数据校验时，顺序不可变化,key此处取值与json文件一样以便查看
            if (null != _allRules._netLocalCellCtrlCheck && _allRules._netLocalCellCtrlCheck.Count != 0)
            {
                _sequence.Add("netLocalCellCtrlCheck", _allRules._netLocalCellCtrlCheck);
            }
            if (null != _allRules._nrNetLocalCellCtrlCheck && _allRules._nrNetLocalCellCtrlCheck.Count != 0)
            {
                _sequence.Add("nrNetLocalCellCtrlCheck", _allRules._nrNetLocalCellCtrlCheck);
            }
            if (null != _allRules._netBoardCheck && _allRules._netBoardCheck.Count != 0)
            {
                _sequence.Add("netBoardCheck", _allRules._netBoardCheck);
            }
            if (null != _allRules._netRHUBCheck && _allRules._netRHUBCheck.Count != 0)
            {
                _sequence.Add("netRHUBCheck", _allRules._netRHUBCheck);
            }
            if (null != _allRules._netRRUCheck && _allRules._netRRUCheck.Count != 0)
            {
                _sequence.Add("netRRUCheck", _allRules._netRRUCheck);
            }
            if (null != _allRules._netPRRUCheck && _allRules._netPRRUCheck.Count != 0)
            {
                _sequence.Add("netPRRUCheck", _allRules._netPRRUCheck);
            }
            if (null != _allRules._netIROptPlanCheck && _allRules._netIROptPlanCheck.Count != 0)
            {
                _sequence.Add("netIROptPlanCheck", _allRules._netIROptPlanCheck);
            }
            if (null != _allRules._netEthPlanCheck && _allRules._netEthPlanCheck.Count != 0)
            {
                _sequence.Add("netEthPlanCheck", _allRules._netEthPlanCheck);
            }
            if (null != _allRules._netAntennaArrayCheck && _allRules._netAntennaArrayCheck.Count != 0)
            {
                _sequence.Add("netAntennaArrayCheck", _allRules._netAntennaArrayCheck);
            }
            if (null != _allRules._netRRUAntennaSettingCheck && _allRules._netRRUAntennaSettingCheck.Count != 0)
            {
                _sequence.Add("netRRUAntennaSettingCheck", _allRules._netRRUAntennaSettingCheck);
            }
            if (null != _allRules._nrNetLocalCellCheck && _allRules._nrNetLocalCellCheck.Count != 0)
            {
                _sequence.Add("nrNetLocalCellCheck", _allRules._nrNetLocalCellCheck);
            }
            if (null != _allRules._netLocalCellCheck && _allRules._netLocalCellCheck.Count != 0)
            {
                _sequence.Add("netLocalCellCheck", _allRules._netLocalCellCheck);
            }
        }

        private void InitMibData(MAP_DEVTYPE_DEVATTRI mapMib, NPEMibData npeMibData)
        {
            npeMibData._netBoard = mapMib.ContainsKey(EnumDevType.board)
                ? mapMib[EnumDevType.board]
                : new List<DevAttributeInfo>();
            npeMibData._netAntennaArray = mapMib.ContainsKey(EnumDevType.ant)
                ? mapMib[EnumDevType.ant]
                : new List<DevAttributeInfo>();
            npeMibData._netRRU = mapMib.ContainsKey(EnumDevType.rru)
                ? mapMib[EnumDevType.rru]
                : new List<DevAttributeInfo>();
            npeMibData._netRRUAntennaSetting = mapMib.ContainsKey(EnumDevType.rru_ant)
                ? mapMib[EnumDevType.rru_ant]
                : new List<DevAttributeInfo>();
            //prru_ant
            if (mapMib.ContainsKey(EnumDevType.prru_ant))
            {
                foreach (var tmp in mapMib[EnumDevType.prru_ant])
                {
                    npeMibData._netRRUAntennaSetting.Add(tmp);
                }
            }
            npeMibData._netIROptPlan = mapMib.ContainsKey(EnumDevType.board_rru)
                ? mapMib[EnumDevType.board_rru]
                : new List<DevAttributeInfo>();
            //board_rhub
            if (mapMib.ContainsKey(EnumDevType.board_rhub))
            {
                foreach (var tmp in mapMib[EnumDevType.board_rhub])
                {
                    npeMibData._netIROptPlan.Add(tmp);
                }
            }
            npeMibData._netRHUB = mapMib.ContainsKey(EnumDevType.rhub)
                ? mapMib[EnumDevType.rhub]
                : new List<DevAttributeInfo>();
            npeMibData._netEthPlan = mapMib.ContainsKey(EnumDevType.rhub_prru)
                ? mapMib[EnumDevType.rhub_prru]
                : new List<DevAttributeInfo>();
            npeMibData._nrNetLocalCell = mapMib.ContainsKey(EnumDevType.nrNetLc)
                ? mapMib[EnumDevType.nrNetLc]
                : new List<DevAttributeInfo>();
            npeMibData._netLocalCell = mapMib.ContainsKey(EnumDevType.netLc)
                ? mapMib[EnumDevType.netLc]
                : new List<DevAttributeInfo>();
            npeMibData._nrNetLocalCellCtrl = mapMib.ContainsKey(EnumDevType.nrNetLcCtr)
                ? mapMib[EnumDevType.nrNetLcCtr]
                : new List<DevAttributeInfo>();
            npeMibData._netLocalCellCtrl = mapMib.ContainsKey(EnumDevType.netLcCtr)
                ? mapMib[EnumDevType.netLcCtr]
                : new List<DevAttributeInfo>();
            npeMibData._nrLocalCell = mapMib.ContainsKey(EnumDevType.nrLc)
                ? mapMib[EnumDevType.nrLc]
                : new List<DevAttributeInfo>();
            npeMibData._nrCell = mapMib.ContainsKey(EnumDevType.nrCell)
                ? mapMib[EnumDevType.nrCell]
                : new List<DevAttributeInfo>();
            npeMibData._localCell = mapMib.ContainsKey(EnumDevType.lc)
                ? mapMib[EnumDevType.lc]
                : new List<DevAttributeInfo>();
            npeMibData._cell = mapMib.ContainsKey(EnumDevType.cell)
                ? mapMib[EnumDevType.cell]
                : new List<DevAttributeInfo>();
            npeMibData._baseBandwidth = mapMib.ContainsKey(EnumDevType.rhub_rhub)
                ? mapMib[EnumDevType.rhub_rhub]
                : new List<DevAttributeInfo>();
        }
        private void InitLibData()
        {
            NPERru npeRRU_lib = NPERruHelper.GetInstance().GetNPERru();
            _npedata_lib._rruTypeInfo_lib = npeRRU_lib.rruTypeInfo;
            _npedata_lib._rruTypePortInfo_lib = npeRRU_lib.rruTypePortInfo;
            NetPlanElement npeElement_lib = NPEBoardHelper.GetInstance().GetNetPlanBoardInfo();
            _npedata_lib._netPlanElements_lib = npeElement_lib.netPlanElements;
            _npedata_lib._shelfEquipment_lib = npeElement_lib.shelfEquipment;
            _npedata_lib._boardEquipment_lib = npeElement_lib.boardEquipment;
            _npedata_lib._rHubEquipment_lib = npeElement_lib.rHubEquipment;
            _npedata_lib._antType_lib = NPEAntHelper.GetInstance().GetWholeAntInfo().antennaTypeTable;
        }
        private void InitCheckRulesData()
        {
            var path = FilePathHelper.GetAppPath() + ConfigFileHelper.NetPlanCheckRulesJson;
            var jsonContent = FileRdWrHelper.GetFileContent(path, Encoding.UTF8);
            _allRules = JsonHelper.SerializeJsonToObject<NetPlanCheckRules>(jsonContent);
        }
        public NPECheckRulesDeal(MAP_DEVTYPE_DEVATTRI mapMib_this, int equipType)
        {
            _npedata_this = new NPEMibData();
            _npedata_lib = new NPELibData();
            _dicDatasrcUIPara = new Dictionary<string, object>();
            _sequence = new Dictionary<string, List<BaseCheckRule>>();
            _dicRuleNameUIPara = new Dictionary<string, List<DevAttributeInfo>>();
            _equipType_this = equipType; //5 4G, 10 5G
            //初始化修改后的网规数据
            InitMibData(mapMib_this, _npedata_this);
            //器件库信息初始化
            InitLibData();
            //获取校验规则
            InitCheckRulesData();
            //设置校验顺序
            SetSequence();
            //映射关系,遍历查询时的集合名会使用到
            SetMapOfDatasrcAndPara();
            SetMapOfRulesNameAndPara();
        }

        public bool IsValidParaName(string name)
        {
            //表示数据来源的
            if (name.StartsWith("mib.") || name.StartsWith("this.") || name.StartsWith("cur.")
                || name.StartsWith("lib."))
            {
                return true;
            }
            //只用会用propery中用来表示取修改前的值
            if (name.StartsWith("old."))
            {
                return true;
            }
            //查询语句返回的结果变量query\d
            string queryPre;
            if (IsValidQueryWord(name, out queryPre))
            {
                return true;
            }
            //中间变量，遍历元素it
            if (name.StartsWith("it.") || name.Equals("it"))
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        public EnumResultType GetMibParaValue(DevAttributeInfo record, string name, out string value)
        {
            value = "";
            if (!name.StartsWith("cur.") && !name.StartsWith("old.")
                && !name.StartsWith("mib.") && !name.StartsWith("this."))
            {
                Log.Error("name is not mib para : " + name);
                return EnumResultType.fail;
            }
            //取叶子节点名称
            //考虑到不同的版本，其mib参数可能不一样，对于在本版本中不存在的校验参数，直接返回false_continue
            string leafName = name.Substring(name.LastIndexOf('.') + 1);
            if (leafName.Equals("操作类型"))
            {
                //理论上该参数只会出现在校验规则的property中
                RecordDataType recordType = record.m_recordType;
                //例如配置文件/当前基站已经存在的数据，虽然没有修改没有变化，此处也做一些校验保护，故未变化的参数设置为"添加"
                if (recordType == RecordDataType.Original || recordType == RecordDataType.NewAdd)
                {
                    value = "添加";
                }
                else if(recordType == RecordDataType.Modified)
                {
                    value = "修改";
                }
                else if (recordType == RecordDataType.WaitDel)
                {
                    value = "删除";
                }
                else
                {
                    Log.Error(name + "mib para  recordType is not support: " + recordType);
                    return EnumResultType.fail;
                }
            }
            else
            {
                MibLeafNodeInfo leafValue;
                if (!record.m_mapAttributes.TryGetValue(leafName, out leafValue))
                {
                    Log.Warn(name + " leaf is not in mib!");
                    return EnumResultType.fail_continue;
                }
                if (name.StartsWith("cur.") || name.StartsWith("this."))
                {
                    value = leafValue.m_strLatestValue;
                }
                else if (name.StartsWith("old.") || name.StartsWith("mib."))
                {
                    value = leafValue.m_strOriginValue;
                }
            }
            return EnumResultType.success_true;
        }

        public EnumResultType GetPropertyConditionValue(string property, DevAttributeInfo curRecord)
        {
            List<string> propertyNameList;
            Dictionary<string, object> propertyValueDic = new Dictionary<string, object>();
            CommCheckRuleDeal commCheckRule = new CommCheckRuleDeal();
            if (!CommCheckRuleDeal.GetParaByConditionalExp(property, out propertyNameList))
            {
                //获取条件参数失败，直接跳过该条规则
                Log.Error("GetParaByConditionalExp error");
                return EnumResultType.fail;
            }
            foreach (var name in propertyNameList)
            {
                //初步过滤，认为非关键词开头的变量名，真实的对应是一个值，而不是变量名,不需要填写到propertyValueDic中
                if (!IsValidParaName(name))
                {
                    continue;
                }
                //参数名有可能重复进行去重保护
                if (propertyValueDic.ContainsKey(name))
                {
                    continue;
                }
                //根据参数名称，获取参数的值,为了方便表示变化，只有在property中可以使用old.
                //例如 where cur.netRRUAntennaSettingEntry.netSetRRUPortSubtoLocalCellId ！= old.netRRUAntennaSettingEntry.netSetRRUPortSubtoLocalCellId
                if (!name.StartsWith("cur.") && !name.StartsWith("old."))
                {
                    Log.Error(property + " is not start with cur. or old.!");
                    return EnumResultType.fail;
                }
                string value;
                EnumResultType mibRes = GetMibParaValue(curRecord, name, out value);
                if (mibRes == EnumResultType.fail_continue)
                {
                    return EnumResultType.fail_continue;
                }
                else if (mibRes == EnumResultType.success_true)
                {
                    propertyValueDic.Add(name, value);
                }
                else
                {
                    return EnumResultType.fail;
                }
            }
            //计算最终的结果，条件表达式进行运算
            string result;
            if (!CommCheckRuleDeal.CalculateConditionExpr(property, propertyValueDic, out result))
            {
                Log.Warn("CalculateConditionExpr property fail :" + property);
                return EnumResultType.fail;
            }
            if (result.Equals(true.ToString()))
            {
                return EnumResultType.success_true;
            }
            else
            {
                return EnumResultType.success_false;
            }
        }

        private EnumResultType GetFieldValueOfType<T>(object obj, string fieldName, out object fieldValue)
        {
            fieldValue = new object();
            if (obj == null)
            {
                return EnumResultType.fail;
            }
            if (obj is T)
            {
                T newObj = (T) Convert.ChangeType(obj, typeof(T));
                PropertyInfo info = newObj.GetType().GetProperty(fieldName);
                if (null == info)
                {
                    Log.Error(typeof(T).Name + " not have property "+ fieldName);
                    return EnumResultType.fail_continue;
                }
                object originValue = info.GetValue(newObj,null);
                //进行下数据转换，如果为整型则转换为字符串,方便后面的校验运算
                if (originValue is int)
                {
                    fieldValue = Convert.ToString(originValue);
                }
                else
                {
                    fieldValue = originValue;
                }
                return EnumResultType.success_true;
            }
            return EnumResultType.fail;
        }

        public EnumResultType MapLibQueryOfDataType(string preQueryName, object queryValue, string leafName,
            out object objValue)
        {
            objValue = new object();
            //TO DO:暂时不支持查询的leafName是多层,例如Irband.bandwidth.value
            //结合MapQueryAndData2Layer，MapQueryAndData3Layer，MapQueryAndData4Layer，
            //不区分preQueryName，只根据preQueryNamer的数据类型来处理
            if (queryValue is EnumerableQuery<VD> || queryValue is List<VD>)
            {
                //"lib.rruTypePortInfo.rruTypeNotMibIrBand.bandwidth"、"lib.shelfEquipment.planSlotInfo.supportBoardType"、"lib.boardEquipment.irOfpPortInfo.irOfpPortTransSpeed"
                //"lib.rruTypePortInfo.rruTypeFiberLength"、"lib.rruTypeInfo.rruTypeIrCompressMode"、"lib.rruTypeInfo.rruTypeSupportCellWorkMode"、"lib.rruTypeInfo.rruTypeNotMibSupportNetWorkMode"
                //"lib.rruTypeInfo.rruTypeNotMibIrRate"、"lib.rruTypePortInfo.rruTypePortSupportFreqBand"、"lib.rruTypePortInfo.rruTypePortNotMibRxTxStatus":
                //"lib.boardEquipment.supportConnectElement"、"lib.rHubEquipment.irOfpPortTransSpeed"、"lib.rHubEquipment.ethPortTransSpeed"、"lib.rruTypeInfo"
                IEnumerable list = queryValue as IEnumerable;
                foreach (VD tmp in list)
                {
                    return GetFieldValueOfType<VD>(tmp, leafName, out objValue);
                }
                Log.Error(preQueryName + " query_value is null, type VD");
                return EnumResultType.fail;
            }
            else if (queryValue is EnumerableQuery<IrBand> || queryValue is List<IrBand>)
            {
                //"lib.rruTypeInfo.rruTypeNotMibIrBand"
                IEnumerable list = queryValue as IEnumerable;
                foreach (IrBand tmp in list)
                {
                    return GetFieldValueOfType<IrBand>(tmp, leafName, out objValue);
                }
                Log.Error(preQueryName + " query_value is null, type IrBand");
                return EnumResultType.fail;
            }
            else if (queryValue is EnumerableQuery<ShelfSlotInfo> || queryValue is List<ShelfSlotInfo>)
            {
                //"lib.shelfEquipment.planSlotInfo"
                IEnumerable list = queryValue as IEnumerable;
                foreach (ShelfSlotInfo tmp in list)
                {
                    return GetFieldValueOfType<ShelfSlotInfo>(tmp, leafName, out objValue);
                }
                Log.Error(preQueryName + " query_value is null, type ShelfSlotInfo");
                return EnumResultType.fail;

            }
            else if (queryValue is EnumerableQuery<OfpPortInfo> || queryValue is List<OfpPortInfo>)
            {
                //"lib.boardEquipment.irOfpPortInfo"
                IEnumerable list = queryValue as IEnumerable;
                foreach (OfpPortInfo tmp in list)
                {
                    return GetFieldValueOfType<OfpPortInfo>(tmp, leafName, out objValue);
                }
                Log.Error(preQueryName + " query_value is null, type OfpPortInfo");
                return EnumResultType.fail;

            }
            else if (queryValue is EnumerableQuery<RruInfo> || queryValue is List<RruInfo>)
            {
                //"lib.rruTypeInfo"
                IEnumerable list = queryValue as IEnumerable;
                foreach (RruInfo tmp in list)
                {
                    //取list中的第一个元素返回
                    return GetFieldValueOfType<RruInfo>(tmp, leafName, out objValue);
                }
                Log.Error(preQueryName + " query_value is null, type RruInfo");
                return EnumResultType.fail;

            }
            else if (queryValue is EnumerableQuery<RruPortInfo> || queryValue is List<RruPortInfo>)
            {
                //"lib.rruTypePortInfo"
                IEnumerable list = queryValue as IEnumerable;
                foreach (RruPortInfo tmp in list)
                {
                    return GetFieldValueOfType<RruPortInfo>(tmp, leafName, out objValue);
                }
                Log.Error(preQueryName + " query_value is null, type RruPortInfo");
                return EnumResultType.fail;

            }
            else if (queryValue is EnumerableQuery<NetPlanElementType> || queryValue is List<NetPlanElementType>)
            {
                //"lib.netPlanElements"
                IEnumerable list = queryValue as IEnumerable;
                foreach (NetPlanElementType tmp in list)
                {
                    return GetFieldValueOfType<NetPlanElementType>(tmp, leafName, out objValue);
                }
                Log.Error(preQueryName + " query_value is null, type NetPlanElementType");
                return EnumResultType.fail;

            }
            else if (queryValue is EnumerableQuery<Shelf> || queryValue is List<Shelf>)
            {
                //"lib.shelfEquipment"
                IEnumerable list = queryValue as IEnumerable;
                foreach (Shelf tmp in list)
                {
                    return GetFieldValueOfType<Shelf>(tmp, leafName, out objValue);
                }
                Log.Error(preQueryName + " query_value is null, type Shelf");
                return EnumResultType.fail;

            }
            else if (queryValue is EnumerableQuery<BoardEquipment> || queryValue is List<BoardEquipment>)
            {
                //"lib.boardEquipment"
                IEnumerable list = queryValue as IEnumerable;
                foreach (BoardEquipment tmp in list)
                {
                    return GetFieldValueOfType<BoardEquipment>(tmp, leafName, out objValue);
                }
                Log.Error(preQueryName + " query_value is null, type BoardEquipment");
                return EnumResultType.fail;

            }
            else if (queryValue is EnumerableQuery<RHUBEquipment> || queryValue is List<RHUBEquipment>)
            {
                //"lib.rHubEquipment"
                IEnumerable list = queryValue as IEnumerable;
                foreach (RHUBEquipment tmp in list)
                {
                    return GetFieldValueOfType<RHUBEquipment>(tmp, leafName, out objValue);
                }
                Log.Error(preQueryName + " query_value is null, type RHUBEquipment");
                return EnumResultType.fail;
            }
            else if (queryValue is EnumerableQuery<AntType> || queryValue is List<AntType>)
            {
                //"lib.antennaTypeTable"
                IEnumerable list = queryValue as IEnumerable;
                foreach (AntType tmp in list)
                {
                    return GetFieldValueOfType<AntType>(tmp, leafName, out objValue);
                }
                Log.Error(preQueryName + " query_value is null, type AntType");
                return EnumResultType.fail;
            }

            Log.Error(preQueryName + " TYPE is not support, leafName is " + leafName);
            return EnumResultType.fail;
        }

        public EnumResultType MapQueryAndData4Layer(string preQueryName, object queryValue, string leafName, out object objValue)
        {
            objValue = new object();
            switch (preQueryName)
            {
                case "lib.rruTypePortInfo.rruTypeNotMibIrBand.bandwidth":
                case "lib.shelfEquipment.planSlotInfo.supportBoardType":
                case "lib.boardEquipment.irOfpPortInfo.irOfpPortTransSpeed":
                    //只需要取第一个元素进行处理
                    if (queryValue is List<VD>)
                    {
                        IEnumerable list = queryValue as IEnumerable;
                        foreach (VD tmp in list)
                        {
                            //return GetFieldValueOfType<VD>(tmp, leafName, out objValue);
                            return MapLibQueryOfDataType(preQueryName, queryValue, leafName,
                                out objValue);
                        }
                    }
                    Log.Error("queryValue type is not List<VD>, leafName is " + leafName);
                    return EnumResultType.fail;
                default:
                    //做一层保护，List<VD>较常见
                    if (queryValue is List<VD>)
                    {
                        IEnumerable list = queryValue as IEnumerable;
                        foreach (VD tmp in list)
                        {
                            return GetFieldValueOfType<VD>(tmp, leafName, out objValue);
                        }
                    }
                    //校验规则里有此节点，但是这里没有
                    Log.Error(preQueryName + " preQueryName is invalid, leafName is " + leafName);
                    return EnumResultType.fail;
            }
        }

        private EnumResultType MapQueryAndData3Layer(string preQueryName, object queryValue, string leafName, out object objValue)
        {
            objValue = new object();
            switch (preQueryName)
            {
                case "lib.rruTypePortInfo.rruTypeFiberLength":
                case "lib.rruTypeInfo.rruTypeIrCompressMode":
                case "lib.rruTypeInfo.rruTypeSupportCellWorkMode":
                case "lib.rruTypeInfo.rruTypeNotMibSupportNetWorkMode":
                case "lib.rruTypeInfo.rruTypeNotMibIrRate":
                case "lib.rruTypePortInfo.rruTypePortSupportFreqBand":
                case "lib.rruTypePortInfo.rruTypePortNotMibRxTxStatus":
                case "lib.boardEquipment.supportConnectElement":
                case "lib.rHubEquipment.irOfpPortTransSpeed":
                case "lib.rHubEquipment.ethPortTransSpeed":
                    //只需要取第一个元素进行处理
                    if (queryValue is List<VD>)
                    {
                        IEnumerable list = queryValue as IEnumerable;
                        foreach (VD tmp in list)
                        {
                            return GetFieldValueOfType<VD>(tmp, leafName, out objValue);
                        }
                    }
                    Log.Error("queryValue type is not List<VD>, leafName is " + leafName);
                    return EnumResultType.fail;
                case "lib.rruTypeInfo.rruTypeNotMibIrBand":
                    if (queryValue is List<IrBand>)
                    {
                        IEnumerable list = queryValue as IEnumerable;
                        foreach (IrBand tmp in list)
                        {
                            return GetFieldValueOfType<IrBand>(tmp, leafName, out objValue);
                        }
                    }
                    Log.Error("queryValue type is not List<IrBand>, leafName is " + leafName);
                    return EnumResultType.fail;
                case "lib.shelfEquipment.planSlotInfo":
                    if (queryValue is List<ShelfSlotInfo>)
                    {
                        IEnumerable list = queryValue as IEnumerable;
                        foreach (ShelfSlotInfo tmp in list)
                        {
                            return GetFieldValueOfType<ShelfSlotInfo>(tmp, leafName, out objValue);
                        }
                    }
                    Log.Error("queryValue type is not List<ShelfSlotInfo>, leafName is " + leafName);
                    return EnumResultType.fail;
                case "lib.boardEquipment.irOfpPortInfo":
                    //为集合
                    if (queryValue is List<OfpPortInfo>)
                    {
                        IEnumerable list = queryValue as IEnumerable;
                        foreach (OfpPortInfo tmp in list)
                        {
                            return GetFieldValueOfType<OfpPortInfo>(tmp, leafName, out objValue);
                        }
                    }
                    Log.Error("queryValue type is not List<OfpPortInfo>, leafName is " + leafName);
                    return EnumResultType.fail;
                default:
                    Log.Error(preQueryName + " preQueryName is invalid, leafName is " + leafName);
                    return EnumResultType.fail;
            }
        }

        private EnumResultType MapQueryAndData2Layer(string preQueryName, object queryValue, string leafName, out object objValue)
        {
            objValue = new object();
            switch (preQueryName)
            {
                //paraName为取preName.leafName
                case "lib.rruTypeInfo":
                    //为集合
                    if (queryValue is List<RruInfo>)
                    {
                        IEnumerable list = queryValue as IEnumerable;
                        foreach (RruInfo tmp in list)
                        {
                            //取list中的第一个元素返回
                            return GetFieldValueOfType<RruInfo>(tmp, leafName, out objValue);
                        }
                    }
                    Log.Error("queryValue type is not List<RruInfo>, leafName is " + leafName);
                    return EnumResultType.fail;
                case "lib.rruTypePortInfo":
                    //为集合
                    if (queryValue is List<RruPortInfo>)
                    {
                        IEnumerable list = queryValue as IEnumerable;
                        foreach (RruPortInfo tmp in list)
                        {
                            return GetFieldValueOfType<RruPortInfo>(tmp, leafName, out objValue);
                        }
                    }
                    Log.Error("queryValue type is not List<RruPortInfo>, leafName is " + leafName);
                    return EnumResultType.fail;
                case "lib.netPlanElements":
                    //为集合
                    if (queryValue is List<NetPlanElementType>)
                    {
                        IEnumerable list = queryValue as IEnumerable;
                        foreach (NetPlanElementType tmp in list)
                        {
                            return GetFieldValueOfType<NetPlanElementType>(tmp, leafName, out objValue);
                        }
                    }
                    Log.Error("queryValue type is not List<NetPlanElementType>, leafName is " + leafName);
                    return EnumResultType.fail;
                case "lib.shelfEquipment":
                    //为集合
                    if (queryValue is List<Shelf>)
                    {
                        IEnumerable list = queryValue as IEnumerable;
                        foreach (Shelf tmp in list)
                        {
                            return GetFieldValueOfType<Shelf>(tmp, leafName, out objValue);
                        }
                    }
                    Log.Error("queryValue type is not List<Shelf>, leafName is " + leafName);
                    return EnumResultType.fail;
                case "lib.boardEquipment":
                    //为集合
                    if (queryValue is List<BoardEquipment>)
                    {
                        IEnumerable list = queryValue as IEnumerable;
                        foreach (BoardEquipment tmp in list)
                        {
                            return GetFieldValueOfType<BoardEquipment>(tmp, leafName, out objValue);
                        }
                    }
                    Log.Error("queryValue type is not List<BoardEquipment>, leafName is " + leafName);
                    return EnumResultType.fail;
                case "lib.rHubEquipment":
                    //为集合
                    if (queryValue is List<RHUBEquipment>)
                    {
                        IEnumerable list = queryValue as IEnumerable;
                        foreach (RHUBEquipment tmp in list)
                        {
                            return GetFieldValueOfType<RHUBEquipment>(tmp, leafName, out objValue);
                        }
                    }
                    Log.Error("queryValue type is not List<RHUBEquipment>, leafName is " + leafName);
                    return EnumResultType.fail;
                default:
                    Log.Error(preQueryName + " preQueryName is invalid, leafName is " + leafName);
                    return EnumResultType.fail;
            }
        }

        public bool IsValidQueryWord(string exptr, out string queryPre)
        {
            string pattern = @"^\bquery\d+\.";
            queryPre = "";

            if (string.IsNullOrEmpty(exptr))
            {
                Log.Error(" exptr is null");
                return false;
            }
            MatchCollection match = Regex.Matches(exptr, pattern);
            //如果以query\d.开头
            if (match.Count > 0)
            {
                //exptr的"queryPre"标识是前面round中outvar得到的查询结果,
                //"queryPre"在.后面的是取对应叶子节点值，正常情况下queryPre是一个list，而使用中如果包含.则说明list的长度是1,取第一个元素就可以
                //"queryPre"对应在queryDic中key可能情况是:mib.表名，lib.信息表名.XX，lib信息表名.XX.XX
                queryPre = match[0].Value;
            }
            else
            {
                //如果以query\d开头，但是无.，使用场合是集合进行遍历
                pattern = @"^\bquery\d+$";
                match = Regex.Matches(exptr, pattern);
                //如果以query\d开头，但是无.，使用场合是集合进行遍历
                if (match.Count > 0)
                {
                    queryPre = match[0].Value;
                }

            }
            if (queryPre.Equals(""))
            {
                return false;
            }
            return true;
        }

        public bool GetQueryFatherName(string exptr, Dictionary<string, object> queryDic, out string fatherName)
        {
            string query1 = exptr;
            fatherName = "";
            //exptr的"query1"标识是前面round中outvar得到的查询结果,
            //"query1"在.后面的是取对应叶子节点值，正常情况下query1是一个list，而使用中如果包含.则说明list的长度是1,取第一个元素就可以
            //"query1"对应在queryDic中key可能情况是:mib.表名，lib.信息表名.XX，lib信息表名.XX.XX
            foreach (var oneQuery in queryDic)
            {
                int length;
                //key以query\d开头
                //exptr有的时候是类似query1.，有的时候类型query1
                //是为了避免query12.XX在查询query1时出现被错误的找到，针对query1格式增加".”
                int index = CommCheckRuleDeal.GetIndex(exptr, ".", out length);
                query1 = (index == -1) ? (exptr + ".") : exptr.Substring(0, index + length);

                index = CommCheckRuleDeal.GetIndex(oneQuery.Key, query1, out length);
                if (-1 != index)
                {
                    fatherName = oneQuery.Key;
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 根据query变量名称计算值
        /// </summary>
        /// <param name="exptr">json文件中的参数名query...</param>
        /// <param name="queryDic">通过查询已经得到的query值，key为query\d.数据源，value为值</param>
        /// <param name="obj">通过映射查表等方式得到的参数exptr的值</param>
        /// <returns></returns>
        public EnumResultType ConvertQueryValue(string exptr, Dictionary<string, object> queryDic, out object objResult)
        {
            string fatherFullName;
            objResult = new object();
            string query1;
            if (!IsValidQueryWord(exptr, out query1))
            {
                return EnumResultType.fail;
            }

            if (!GetQueryFatherName(query1, queryDic, out fatherFullName))
            {
                Log.Warn(query1 + " -- exptr can not find father queryName!");
                return EnumResultType.fail;
            }
            object fatherValue;
            if (!queryDic.TryGetValue(fatherFullName, out fatherValue))
            {
                return EnumResultType.fail;
            }
            //取此次查询的叶子节点名称,有可能是多层的,也有可能只有query\d
            int length;
            int index = CommCheckRuleDeal.GetIndex(exptr, ".", out length);
            if (index == -1)
            {
                //直接取queryDic对应的值返回即可
                objResult = fatherValue;
                //支持select it.XX后,
                return EnumResultType.success_true;
            }
            string leafName = exptr.Substring(index + length);
            //特殊处理
            if (leafName.Equals("Count"))
            {
                IEnumerable list = fatherValue as IEnumerable;
                objResult = list.ToDynamicArray().Count().ToString();
                return EnumResultType.success_true;
            }
            string fatherName = fatherFullName.Substring(query1.Length);
            MibLeafNodeInfo leafValue;
            if (fatherName.StartsWith("mib.") || fatherName.StartsWith("this."))
            {
                string[] split = fatherName.Split('.');
                if ((fatherValue is EnumerableQuery<DevAttributeInfo>)
                    || (fatherValue is List<DevAttributeInfo>))
                {
                    IEnumerable list = fatherValue as IEnumerable;
                    foreach (DevAttributeInfo tmp in list)
                    {
                        if (!tmp.m_mapAttributes.TryGetValue(leafName, out leafValue))
                        {
                            Log.Warn(leafName + " leaf is not in obj mib!");
                            return EnumResultType.fail_continue;
                        }
                        //只需要取第一个元素
                        objResult = fatherName.StartsWith("mib.") ? leafValue.m_strOriginValue : leafValue.m_strLatestValue;
                        return EnumResultType.success_true;
                    }
                    //此处不会进入
                    Log.Warn(leafName + " leaf is not in obj mib or empty");
                    return EnumResultType.fail;
                }
                else
                {
                    Log.Warn("fatherValue type is not List<DevAttributeInfo>");
                    return EnumResultType.fail;
                }
            }
            else if (fatherName.StartsWith("lib."))
            {
                return MapLibQueryOfDataType(fatherName, fatherValue, leafName, out objResult);
            }
            else
            {
                Log.Error("expr start with query\\d\\. ,but not in lib/mib");
                return EnumResultType.fail;
            }
        }
        public EnumResultType GetRoundParaValue(DevAttributeInfo curRecord, Dictionary<string, Object> queryDic, List<string> propertyNameList, out Dictionary<string, Object> paraValueDic)
        {
            paraValueDic = new Dictionary<string, object>();
            foreach (var name in propertyNameList)
            {
                //增加重复参数的保护
                if (paraValueDic.ContainsKey(name))
                {
                    continue;
                }
                if (name.Equals("this.equipType"))
                {
                    paraValueDic.Add(name, _equipType_this);
                }
                else if (name.StartsWith("this.") || name.StartsWith("mib.") || name.StartsWith("lib."))
                {
                    //这三个关键词主要出现在from语句中，用来遍历表示的集合
                    Object obj;
                    if (!_dicDatasrcUIPara.TryGetValue(name, out obj))
                    {
                        Log.Error("_dicDatasrcUIPara get " + name + " value fail!");
                        return EnumResultType.fail;
                    }
                    paraValueDic.Add(name, obj);
                }
                else if (name.StartsWith("cur."))
                {
                    //该参数表示为当前正在校验的某表中的参数
                    string leafName = name.Substring(name.LastIndexOf('.') + 1);
                    MibLeafNodeInfo leafValue;
                    if (!curRecord.m_mapAttributes.TryGetValue(leafName, out leafValue))
                    {
                        //当前版本不支持该MIB节点，则不进行校验
                        Log.Warn(name + " leaf is not in mib!");
                        return EnumResultType.fail_continue;
                    }
                    paraValueDic.Add(name, leafValue.m_strLatestValue);
                }
                else if (name.StartsWith("it.") || name.Equals("it"))
                {
                    //查询语句中的it.，是指遍历到的元素，它不需要去查询值，直接保留用来组linq语句
                    //it.关键词只会出现在查询语句中,
                    continue;                  
                }
                else
                {
                    string queryPre;
                    if (IsValidQueryWord(name, out queryPre))
                    {
                        object objResult;
                        EnumResultType convertRes = ConvertQueryValue(name, queryDic, out objResult);
                        if (EnumResultType.success_true != convertRes)
                        {
                            Log.Error(name + " convert query value fail: " + convertRes);
                            return convertRes;
                        }
                        paraValueDic.Add(name, objResult);
                    }
                    else
                    {
                        //非query\d有效参数，则认为是一个值，而不是变量
                        continue;
                    }
                }
            }
            return EnumResultType.success_true;
        }

        public EnumResultType TakeRoundRulesConditionalResult(string rules, DevAttributeInfo curRecord, Dictionary<string, Object> queryDic, out bool finalResult)
        {
            finalResult = false;
            List<string> conditionalNameList;
            Dictionary<string, object> paraValueDic = new Dictionary<string, object>();
            //进到该功能函数中肯定是条件语句，不再做重复性的校验保护
            //1.获取条件语句使用到的参数名
            if (!CommCheckRuleDeal.GetParaByConditionalExp(rules, out conditionalNameList))
            {
                //获取条件参数失败，直接跳过该条规则
                Log.Error("GetParaByConditionalExp error: " + rules);
                return EnumResultType.fail;
            }
            //2.根据参数名查询参数的值 
            EnumResultType paraRes = GetRoundParaValue(curRecord, queryDic, conditionalNameList, out paraValueDic);
            if (EnumResultType.success_true != paraRes)
            {
                return paraRes;
            }
            //3.直接进行计算
            string result;
            if (!CommCheckRuleDeal.CalculateConditionExpr(rules, paraValueDic, out result))
            {
                Log.Warn("CalculateConditionExpr " + rules + " fail");
                return EnumResultType.fail;
            }
            finalResult = result.Equals(true.ToString()) ? true : false;           
            return EnumResultType.success_true;
        }

        public bool IsMibTable(string name)
        {
            //考虑到查询结果允许填写为it.XX,导致后面置outvar--query后,再取query的父节点会出现两种情况:
            //1.mib.XX, this.XX 表示MIB表， 2.mib.XX.XX, this.XX.XX表示MIB表中的叶子节点
            //针对第一种情况，使用到它组linq查询语句时(例如it.netBoardRowStatus)是要进行统一转换格式的
            if (name.StartsWith("mib.") || name.StartsWith("this."))
            {
                string[] split = name.Split('.');
                if (split.Length == 2 && (!split[1].Equals("")))
                {
                    return true;
                }
                else if (split.Length != 3)
                {
                    //只会有一层取叶子节点
                    Log.Warn("format is invalid, please check " + name);
                    return false;
                }
            }
            return false;
        }

        public bool IsMibTableOfQueryName(string queryName, Dictionary<string, object> queryDic, out string realQueryName)
        {
            realQueryName = queryName;
            string queryPre;
            if (!IsValidQueryWord(queryName, out queryPre))
            {
                return false;
            }
            //query\d开头的参数
            //获取到来源数据类型
            string queryFatherName;
            if (!GetQueryFatherName(queryPre, queryDic, out queryFatherName))
            {
                Log.Warn(queryPre + " is not exist in queryDic!" );
                return false;
            }

            int length;
            int index = CommCheckRuleDeal.GetIndex(queryFatherName, ".", out length);
            if (-1 == index)
            {
                Log.Warn(queryFatherName + " is invalid, please check");
                return false;
            }
            realQueryName = queryFatherName.Substring(index + length);
            return IsMibTable(realQueryName);
        }

        public EnumResultType CheckItPartParaValid(string fromName, object fromValue, Dictionary<string, object> queryDic,
            List<string> whereNameList)
        {
            //进行保护，考虑到linq查询中不会进行校验，在表达式where,select中是否MIB表中存在该叶子节点，是否器件库存在该节点
            string realFatherName = fromName;
            bool isTable = false;

            //from语句是否为MIB类表,需要综合考虑from语句是query\d的可能性
            isTable = (IsMibTable(fromName) || IsMibTableOfQueryName(fromName, queryDic, out realFatherName));
            
            DevAttributeInfo record = null;
            if (fromValue is EnumerableQuery<DevAttributeInfo> || fromValue is List<DevAttributeInfo>)
            {
                IEnumerable list = fromValue as IEnumerable;
                foreach (DevAttributeInfo tmp in list)
                {
                    //取第一条记录意思意思
                    record = tmp;
                    break;
                }
                //说明该from对象为空，所以直接返回??
                if (record == null)
                {
                    return EnumResultType.success_true;
                }
            }
            foreach (var name in whereNameList)
            {
                if (name.StartsWith("it."))
                {
                    string[] split = name.Split('.');
                    if (isTable)
                    {
                        //1.针对MIB
                        if (split.Length != 2)
                        {
                            Log.Warn("select sentence format is error : " + name);
                            return EnumResultType.fail;
                        }
                        if (split[1].Equals(""))
                        {
                            Log.Warn("select sentence format is error : " + name);
                            return EnumResultType.fail;
                        }
                        //取叶子节点
                        string leafName = realFatherName + "." + split[1];
                        string leafValue;
                        return GetMibParaValue(record, leafName, out leafValue);

                    }
                    //2.针对器件库参数lib.
                    string queryPre;
                    if (realFatherName.StartsWith("lib."))
                    {
                        int length;
                        int index = CommCheckRuleDeal.GetIndex(name, ".", out length);
                        if (index == -1)
                        {
                            return EnumResultType.success_true;
                        }
                        string leafName = name.Substring(index + length);
                        //特殊处理
                        if (leafName.Equals("Count"))
                        {
                            return EnumResultType.success_true;
                        }
                        object objResult;
                        EnumResultType convertRes = MapLibQueryOfDataType(fromName, fromValue, leafName, out objResult);
                        return convertRes;
                    }

                }
            }
            return EnumResultType.success_true;
        }

        public bool ConvertNameInWhere(string fromName, string wherePart, Dictionary<string, object> queryDic, List<string> whereNameList, out string whereLastPart)
        {
            whereLastPart = wherePart;
            string realFatherName = fromName;
            bool isTable = false;

            //from语句是否为MIB类表,需要综合考虑from语句是query\d的可能性
            isTable = (IsMibTable(fromName) || IsMibTableOfQueryName(fromName, queryDic, out realFatherName));

            foreach (var name in whereNameList)
            {
                //1.对于常量加上双引号
                if (!IsValidParaName(name))
                {
                    //对于负数的考虑，正则表达式需要加上\
                    string constantName = constantName = @" """ + name + @"""";
                    string pattern;
                    if (name.StartsWith("-"))
                    {
                        pattern = @"\s\" + name + @"\b";
                    }
                    else
                    {
                        pattern = @"\s" + name + @"\b";
                    }
                    Regex regex = new Regex(pattern);
                    //只替换一个
                    whereLastPart = regex.Replace(whereLastPart, constantName, 1);
                }
                //2.MIB类表与where语句中的it.绑定使用时要转换
                else if (name.StartsWith("it."))
                {
                    if (!isTable)
                    {
                        //查询数据源必须是MIB类,不是MIB类数据不需要进行转换
                        continue;
                    }
                    string[] split = name.Split('.');
                    //针对MIB
                    if (split.Length != 2)
                    {
                        //原来写的是continue
                        Log.Warn("select sentence format is error : " + name);
                        return false;
                    }
                    if (split[1].Equals(""))
                    {
                        Log.Warn("select sentence format is error : " + name);
                        return false;
                    }
                    //取叶子节点
                    string leafName = split[1];
                    string afterIt = name;                    
                    //只有this.XX, mib.XX的即UI、基站的MIB数据才需要进行转换
                    afterIt = realFatherName.StartsWith("this.")
                        ? @" it.m_mapAttributes[""" + leafName + @"""].m_strLatestValue"
                        : @" it.m_mapAttributes[""" + leafName + @"""].m_strOriginValue";
                    //将it.leafName替换,为避免叶子节点名有包含关系的名称，例如boardType, boardTypeName而替换错误，增加一个保护
                    //只替换一个
                    string pattern = @"\s?\b" + name + @"\b";
                    Regex regex = new Regex(pattern);
                    //只替换一个
                    whereLastPart = regex.Replace(whereLastPart, afterIt, 1);
                }
            }
            return true;
        }

        public bool ConvertItNameInSelect(string fromName, string itName, Dictionary<string, object> queryDic, out string selectLast)
        {
            selectLast = itName;
            string realFatherName = fromName;
            bool isTable = false;
            
            //selectNameList长度只存在1,不再重复性校验
            if (itName.Equals("it"))
            {
                return true;
            }

            //from语句是否为MIB类表,需要综合考虑from语句是query\d的可能性
            isTable = (IsMibTable(fromName) || IsMibTableOfQueryName(fromName, queryDic, out realFatherName));
            //查询数据源必须是MIB类
            if (!isTable)
            {
                //不是MIB类数据不需要进行转换
                return true;
            }
            //只有mib类的需要转换格式
            if (itName.StartsWith("it."))
            {
                string[] itSplit = itName.Split('.');
                if (itSplit.Length != 2 || itSplit[1].Equals(""))
                {
                    Log.Error("select part itName is invalid: " + " " + itName);
                    return false;
                }
                string afterIt = realFatherName.StartsWith("this.")
                    ? @"it.m_mapAttributes[""" + itSplit[1] + @"""].m_strLatestValue"
                    : @"it.m_mapAttributes[""" + itSplit[1] + @"""].m_strOriginValue";
                //将it.leafName替换,为避免叶子节点名有包含关系的名称，例如boardType, boardTypeName而替换错误，增加一个保护
                selectLast = afterIt;
            }
            //lib的不需要更新格式
            return true;
        }

        public EnumResultType TakeRoundRulesQueryResult(RoundRule roundRule, DevAttributeInfo curRecord,
            Dictionary<string, object> queryDic)
        {
            string rules = roundRule.rules;
            //1.分离出from, where, select语句
            Dictionary<string, string> splitDic;
            if (false == CommCheckRuleDeal.SplitQueryExpr(rules, out splitDic))
            {
                Log.Error("SplitQueryExpr error!");
                return EnumResultType.fail;
            }

            //2.分别获取from, where, select中使用的参数名
            Dictionary<string, List<string>> paraDic;
            if (!CommCheckRuleDeal.GetParaByQueryExpr(splitDic, out paraDic))
            {
                return EnumResultType.fail;
            }
            //3.分别获取from,where,select中的参数值
            Dictionary<string, object> fromParaValueDic;
            Dictionary<string, object> selectParaValueDic;
            Dictionary<string, object> whereParaValueDic;
            List<string> fromParaList;
            List<string> whereParaList;
            List<string> selectParaList;
            if (!paraDic.TryGetValue("fromParaList", out fromParaList))
            {
                return EnumResultType.fail;
            }
            EnumResultType res = GetRoundParaValue(curRecord, queryDic, fromParaList, out fromParaValueDic);
            if (EnumResultType.success_true != res)
            {
                return res;
            }
            if (!paraDic.TryGetValue("whereParaList", out whereParaList))
            {
                return EnumResultType.fail;
            }
            res = GetRoundParaValue(curRecord, queryDic, whereParaList, out whereParaValueDic);
            if (EnumResultType.success_true != res)
            {
                return res;
            }
            if (!paraDic.TryGetValue("selectParaList", out selectParaList))
            {
                return EnumResultType.fail;
            }
            //做一个保护，from、select中只允许有一个参数
            if (fromParaList.Count != 1 || selectParaList.Count != 1)
            {
                return EnumResultType.fail;
            }
            
            //3.组linq语句进行查询
            foreach (var tmp in fromParaValueDic)
            {
                string key = tmp.Key;
                IEnumerable fromCollection = tmp.Value as IEnumerable;
                string fromPart;
                string wherePart;
                string strSelect = selectParaList[0].ToString();
                if (!splitDic.TryGetValue("fromPart", out fromPart))
                {
                    Log.Error("Get fromPart error!");
                    return EnumResultType.fail;
                }
                if (!splitDic.TryGetValue("wherePart", out wherePart))
                {
                    Log.Error("Get wherePart error!");
                    return EnumResultType.fail;
                }
                //参数校验是否有效,有可查询字段
                res = CheckItPartParaValid(key, fromCollection, queryDic, whereParaList);
                if (res != EnumResultType.success_true)
                {
                    Log.Warn(wherePart + "-- wherePart have it para is invalid");
                    return res;
                }
                List<object> realWhereParaList;
                //@"it.m_mapAttributes[""workMode""].m_strLatestValue==@0 "
                //it关键词可能需要转换
                string realWhere;
                if (!ConvertNameInWhere(key, wherePart, queryDic, whereParaList, out realWhere))
                {
                    return EnumResultType.fail;
                }
                string realSelect;
                if (!ConvertItNameInSelect(tmp.Key, strSelect, queryDic, out realSelect))
                {
                    return EnumResultType.fail;
                }
                //realWhere中去除"where"
                realWhere = realWhere.Substring("where ".Length).Trim();
                string strWhere = CommCheckRuleDeal.GetFilterWhere(realWhere, whereParaValueDic, out realWhereParaList);
                try
                {
                    var listRes = fromCollection.AsQueryable().Where(strWhere.ToString(), realWhereParaList.ToArray()).Select(realSelect).Distinct();//默认进行一个去重处理
                    //4.如果该查询有outvar输出参数，则填写queryDic
                    //做一个保护,如果outvar不是it, 而是it.XX,需要保证返回值是List<string>类型,
                    if (!realSelect.Equals("it"))
                    {
                        if (!((listRes is List<string>) || (listRes is EnumerableQuery<string>)))
                        {
                            Log.Warn("Please check select part of rules:" + roundRule.rules + " , should be string, not struct");
                            return EnumResultType.fail;
                        }
                    }
                    if (!AddQueryDicName(roundRule.outvar.Trim(), key, listRes, strSelect, queryDic))
                    {
                        return EnumResultType.fail;
                    }
                    return EnumResultType.success_true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;//后续不进行抛出异常而是直接返回失败,方便继续使用其它功能
                    //return EnumResultType.fail;
                }
                //from语句只有一个集合名，故此处取一个
            }        
            return EnumResultType.fail;
        }

        public bool specialDealValueThenAddQuery(string newQueryName, Dictionary<string, Object> queryDic, object newQueryValue)
        {
            //1.对于select it.@@ 的查询结果需要特殊处理：
            //1.1 IEnumerable<List<XX>>的类型,需要转换List<XX>保存，一般来说这个IEnumerable长度是1，代码中没有直接写死1，留有余地处理
            //1.2 其它的一般来说是IEnumerable<string>，IEnumerable<class>非list，则直接进行保存即可
            //2.对于select it的查询结果，也直接保存即可
            if (newQueryValue is EnumerableQuery<List<VD>>)
            {
                IEnumerable list = newQueryValue as IEnumerable;
                List<VD> newObjValue = new List<VD>();
                foreach (List<VD> tmp in list)
                {
                    newObjValue = newObjValue.Union(tmp).ToList<VD>();
                }
                queryDic.Add(newQueryName, newObjValue);
            }
            else if (newQueryValue is EnumerableQuery<List<IrBand>>)
            {
                IEnumerable list = newQueryValue as IEnumerable;
                List<IrBand> newObjValue = new List<IrBand>();
                foreach (List<IrBand> tmp in list)
                {
                    newObjValue = newObjValue.Union(tmp).ToList<IrBand>();
                }
                queryDic.Add(newQueryName, newObjValue);
            }
            else if (newQueryValue is EnumerableQuery<List<ShelfSlotInfo>>)
            {
                //"lib.shelfEquipment.planSlotInfo"
                IEnumerable list = newQueryValue as IEnumerable;
                List<ShelfSlotInfo> newObjValue = new List<ShelfSlotInfo>();
                foreach (List<ShelfSlotInfo> tmp in list)
                {
                    newObjValue = newObjValue.Union(tmp).ToList<ShelfSlotInfo>();
                }
                queryDic.Add(newQueryName, newObjValue);
            }
            else if (newQueryValue is EnumerableQuery<List<OfpPortInfo>>)
            {
                //"lib.boardEquipment.irOfpPortInfo"
                IEnumerable list = newQueryValue as IEnumerable;
                List<OfpPortInfo> newObjValue = new List<OfpPortInfo>();
                foreach (List<OfpPortInfo> tmp in list)
                {
                    newObjValue = newObjValue.Union(tmp).ToList<OfpPortInfo>();
                }
                queryDic.Add(newQueryName, newObjValue);
            }
            else
            {
                queryDic.Add(newQueryName, newQueryValue);
            }
            return true;
        }
        public bool AddQueryDicName(string outvar, string fatherName, object outValue, string strSelect, Dictionary<string, Object> queryDic)
        {
            string queryName;
            if (outvar.Equals(""))
            {
                //既然写了查询expr就应该有输出，否则没有意义，故此处严格校验
                return false;
            }
            string last = "";
            if (-1 != strSelect.IndexOf("."))
            {
                last = strSelect.Substring(strSelect.IndexOf("."));
            }
            //再做一个保护，outvar必须满足query\d的写法
            string pattern = @"^\bquery\d+$";
            //如果以query\d开头，但是无.，使用场合是集合进行遍历
            if (Regex.Matches(outvar, pattern).Count <= 0)
            {
                Log.Warn(outvar + " is not match format query+数字");
                return false;
            }

            //输出变量保存到queryDic中,它与from语句中的查询的集合名称有关
            if (fatherName.StartsWith("mib.") || fatherName.StartsWith("this.") || fatherName.StartsWith("lib."))
            {
                queryName = outvar + "." + fatherName + last;
            }
            else if (fatherName.StartsWith("query"))
            {
                //分四步：
                //1.取from语句中的集合名fatherName对应的以query\d开头的关键词
                //2.根据1去获取queryDic中的完整名称
                //3.根据select中的变量得到最后的尾缀
                //4.组合新的outvar的完整名称： outvar+2得到的完整名称去除query\d关键词+1中提到的fatherName去除query\d关键词+3得到的尾缀
                string fatherFatherName;
                string fatherPre;
                if (!IsValidQueryWord(fatherName, out fatherPre))
                {
                    return false;
                }
                if (!GetQueryFatherName(fatherPre, queryDic, out fatherFatherName))
                {
                    Log.Warn(fatherPre + " is not exist in queryDic!");
                    return false;
                }
                string middle1 = "";
                //从queryDic取出的fatherFatherName肯定是有数据源的,即IndexOf(".")是肯定有的
                middle1 = fatherFatherName.Substring(fatherFatherName.IndexOf("."));
                if (middle1.Equals(""))
                {
                    return false;
                }
                //fatherName有可能只是query\d，也有可能是query\d.XX
                string middle2 = (fatherPre.IndexOf(".") == -1) ? "" : fatherName.Substring(fatherPre.Length - 1);
                queryName = outvar + middle1 + middle2 + last;
                //做校验保护，避免添加重复的query变量，出现此种情况 请检查json文件
                string outName;
                if (GetQueryFatherName(outvar, queryDic, out outName))
                {
                    Log.Warn("queryDic already have query key " + outvar + ", please check json file");
                    return false;
                }
            }
            else
            {
                return false;
            }
            return specialDealValueThenAddQuery(queryName, queryDic, outValue);
        }
        public EnumResultType GetRoundCheckValue(List<RoundRule> exp, DevAttributeInfo curRecord)
        {
            //key为outvar变量名，value为对应的值,每步查询如果有返回值，则保存到queryDic
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            int loop = 0;
            int roundCount = exp.Count;
            foreach (var round in exp)
            {
                loop++;
                EXPRESSIONTYPE type = CommCheckRuleDeal.ExpressionIsConditionOrQuery(round.rules);
                //约定最后一个表达式必须为条件语句
                //如果非last为条件语句，即中间出现了条件语句其计算结果为false则返回success_false;
                //如果是查询语句，则转换为动态linq语句查询
                if (type == EXPRESSIONTYPE.CONDITIONTYPE)
                {
                    bool result;
                    EnumResultType res = TakeRoundRulesConditionalResult(round.rules, curRecord, queryDic, out result);
                    if (EnumResultType.success_true != res)
                    {
                        return res;
                    }
                    //运算结果false则认为校验失败
                    if (!result)
                    {
                        return EnumResultType.success_false;
                    }
                    else if (loop == roundCount)
                    {
                        //最后一条校验，直接将本个expr返回
                        return EnumResultType.success_true;
                    }
                }
                else if (type == EXPRESSIONTYPE.QUERYTYPE)
                {
                    if (loop == roundCount)
                    {
                        //最后一条表达式为查询则返回失败,严格处理以保证json文件写的正确
                        return EnumResultType.fail;
                    }
                    EnumResultType res = TakeRoundRulesQueryResult(round, curRecord, queryDic);
                    if (EnumResultType.success_true != res)
                    {
                        return res;
                    }
                    //运算成功,执行下一个round
                }
                else
                {
                    return EnumResultType.fail;
                }
            }
            return EnumResultType.success_true;       
        }

        private string ConvertDevTypeName(EnumDevType typeId)
        {
            Dictionary<EnumDevType, string> devType = new Dictionary<EnumDevType, string>();
            devType.Add(EnumDevType.board, "netBoardEntry");
            devType.Add(EnumDevType.rru, "netRRUEntry");
            devType.Add(EnumDevType.rhub, "netRHUBEntry");
            devType.Add(EnumDevType.ant, "netAntennaArrayEntry");
            devType.Add(EnumDevType.nrNetLc, "nrNetLocalCellEntry");
            devType.Add(EnumDevType.nrNetLcCtr, "netLocalCellPowerCtrlEntry");
            devType.Add(EnumDevType.netLc, "netLocalCellEntry");
            devType.Add(EnumDevType.netLcCtr, "netLocalCellCtrlEntry");
            devType.Add(EnumDevType.board_rru, "netIROptPlanEntry");
            devType.Add(EnumDevType.board_rhub, "netIROptPlanEntry");
            devType.Add(EnumDevType.rru_ant, "netRRUAntennaSettingEntry");
            devType.Add(EnumDevType.rhub_prru, "netEthPlanEntry");//??
            string result;
            if (devType.TryGetValue(typeId, out result))
            {
                return result;
            }
            return typeId.ToString();
        }

        public bool CheckAllPlanData(out string falseTip)
        {
            falseTip = "";
            //遍历校验规则顺序，依次进行校验
            foreach (var who in _sequence)
            {
                string key = who.Key;
                var whoRule = who.Value;
                List<DevAttributeInfo> dataSrcList;
                //获取此类规划校验的数据源
                if (!_dicRuleNameUIPara.TryGetValue(key, out dataSrcList))
                {
                    Log.Warn("_dicRuleNameUIPara does not have key " + key);
                    continue;
                }
                if (dataSrcList.Count == 0)
                {
                    //没有对应的规划数据，则结束本次循环
                    continue;
                }
                Log.Info("!!start to " + key + ", data record num is " + dataSrcList.Count);
                foreach (var dataSrc in dataSrcList)
                {
                    foreach (var oneCheck in whoRule)
                    {
                        //先判断条件是否满足校验的必要条件：property属性
                        EnumResultType result = GetPropertyConditionValue(oneCheck.property, dataSrc);
                        if (result == EnumResultType.fail)
                        {
                            //中止所有的校验，返回失败，暂时先严格处理
                            Log.Error("stop all netplan check ");
                            falseTip = "NetPlan_CheckRules.json 校验规则填写有问题，出现此情况请联系tangyun看下!";
                            return false;
                        }
                        else if (result == EnumResultType.fail_continue)
                        {
                            //跳出本条规则继续下一条
                            Log.Warn("this rule id: " + oneCheck.id + " desc: " + oneCheck.desc + "property fail: "+ oneCheck.property);
                            continue;
                        }
                        else if (result == EnumResultType.success_false)
                        {
                            //不满足校验的前提，进入下一条规则校验
                            continue;
                        }
                        //满足条件，则开始exp的计算
                        result = GetRoundCheckValue(oneCheck.exp, dataSrc);
                        if (result == EnumResultType.fail)
                        {
                            //中止所有的校验，返回失败，暂时先严格处理
                            Log.Error("stop all netplan check");
                            falseTip = "NetPlan_CheckRules.json 校验规则填写有问题，出现此情况请联系tangyun看下!";
                            return false;
                        }
                        else if (result == EnumResultType.fail_continue)
                        {
                            //跳出本条规则继续下一条
                            Log.Warn("this rule id: " + oneCheck.id + " desc: " + oneCheck.desc + "property fail: " + oneCheck.property);
                            continue;
                        }
                        else if (result == EnumResultType.success_false)
                        {
                            //不满足校验的前提，进入下一条规则校验
                            string checkRes = "Type: " + dataSrc.m_enumDevType.ToString() + "(实例" +dataSrc.m_strOidIndex +
                                              ") check "+ oneCheck.desc + " fail, " + oneCheck.false_tip;
                            Log.Error(checkRes);
                            falseTip = ConvertDevTypeName(dataSrc.m_enumDevType) + " (实例" + dataSrc.m_strOidIndex + ") :" + oneCheck.false_tip;
                            return false;
                        }
                        else if (result == EnumResultType.success_true)
                        {
                            //满足校验的前提，进入下一条规则校验
                            continue;
                        }
                    }
                }
            }
            Log.Info("!!all netplan check is ok");
            return true;
        }
    }
}

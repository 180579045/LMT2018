using LogManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace NetPlan.Tests
{
    public class AttributeInfoT
    {
        public Dictionary<string, MibLeafNodeInfo> m_mapAttributes;

        public EnumDevType m_enumDevType;                   // 设备类型枚举值
        public int index;

        public RecordDataType m_recordType { get; set; }    // 记录类型
        public AttributeInfoT(EnumDevType mEnumDevType, int id)
        {
            m_enumDevType = mEnumDevType;
            m_mapAttributes = new Dictionary<string, MibLeafNodeInfo>();
            index = id;
        }

    }
    [TestClass()]
    public class CommCheckRuleDealTests
    {
        public CommCheckRuleDealTests()
        {
            Log.SetLogFileName("CommCheckRuleDealTests.log");
        }
        [TestMethod()]
        public void GetParaByQueryExprTest5()
        {
            AttributeInfoT boardAtt = new AttributeInfoT(EnumDevType.board, 1);
            MibLeafNodeInfo leafNode = new MibLeafNodeInfo();
            leafNode.m_strOriginValue = "1";
            leafNode.m_strLatestValue = "5";
            boardAtt.m_mapAttributes.Add("rowStatus", leafNode);
            leafNode = new MibLeafNodeInfo();
            leafNode.m_strOriginValue = "2";
            leafNode.m_strLatestValue = "6";
            boardAtt.m_mapAttributes.Add("boardTye", leafNode);
            leafNode = new MibLeafNodeInfo();
            leafNode.m_strOriginValue = "3";
            leafNode.m_strLatestValue = "3";
            boardAtt.m_mapAttributes.Add("workMode", leafNode);
            List<AttributeInfoT> allBoard = new List<AttributeInfoT>();
            allBoard.Add(boardAtt);
            boardAtt = new AttributeInfoT(EnumDevType.board, 2);
            leafNode = new MibLeafNodeInfo();
            leafNode.m_strOriginValue = "4";
            leafNode.m_strLatestValue = "8";
            boardAtt.m_mapAttributes.Add("rowStatus", leafNode);
            leafNode = new MibLeafNodeInfo();
            leafNode.m_strOriginValue = "5";
            leafNode.m_strLatestValue = "10";
            boardAtt.m_mapAttributes.Add("boardTye", leafNode);
            leafNode = new MibLeafNodeInfo();
            leafNode.m_strOriginValue = "6";
            leafNode.m_strLatestValue = "6";
            boardAtt.m_mapAttributes.Add("workMode", leafNode);
            allBoard.Add(boardAtt);
            StringBuilder strWhere = new StringBuilder();
            int id = 3;
            List<Object> paramList = new List<object>();
            string r = "6";
            string leafName = "workMode";
            string testw = @"it.m_mapAttributes[""" + leafName + @"""].m_strLatestValue==@0";
            strWhere.AppendFormat(@"it.m_mapAttributes[""workMode""].m_strLatestValue==@0 ");
            paramList.Add("6");
            var list = allBoard.AsQueryable().Where(testw, paramList.ToArray()).Select("it");
            //如果不是新建一个对象来查询结果，可以直接var使用；否则需要使用dynamic tmp in list的方式 
            //如果返回的是it，也得使用dynamic 或者显式写成类
            Console.WriteLine(list.Count());

            List<RoundRule> roundList = new List<RoundRule>();
            RoundRule roundr = new RoundRule();
            roundr.round = 1;
            roundr.rules = "where it.index = 4";
            roundr.outvar = "query1";
            roundList.Add(roundr);

            roundr = new RoundRule();
            roundr.round = 2;
            roundr.rules = "where it.rowstatus = 6";
            roundr.outvar = "query21";
            roundList.Add(roundr);

            roundr = new RoundRule();
            roundr.round = 3;
            roundr.rules = "where it.rowstatus = 6";
            roundr.outvar = "query32";
            roundList.Add(roundr);

            roundr = new RoundRule();
            roundr.round = 2;
            roundr.rules = "where it.rowstatus = 6";
            roundr.outvar = "query22";
            roundList.Add(roundr);

            roundr = new RoundRule();
            roundr.round = 3;
            roundr.rules = "where it.rowstatus = 6";
            roundr.outvar = "query31";
            roundList.Add(roundr);

            paramList = new List<object>();
            strWhere = new StringBuilder();
            strWhere.Append(@"it.rules.Contains (@0)");
            paramList.Add("rowstatus");
            var list1 = roundList.AsQueryable().Where(strWhere.ToString(), paramList.ToArray()).Select("it.round").Distinct();
            Console.WriteLine(list1.Count());

            paramList = new List<object>();
            strWhere = new StringBuilder();
            string para = "query21";
            strWhere.Append(@"it.outvar == @0");
            paramList.Add(list1);
            //数组包含关系
            var list2 = roundList.AsQueryable().Where("@0.Contains(it.round)", paramList.ToArray()).Select("it");
            Console.WriteLine(list2.Count());
        }

        [TestMethod()]
        public void ExpressionIsConditionOrQueryTest()
        {
            string expr = " where this.board.rowStatus == 4";
            EXPRESSIONTYPE type = CommCheckRuleDeal.ExpressionIsConditionOrQuery(expr);
            Assert.IsTrue(type == EXPRESSIONTYPE.CONDITIONTYPE);

            expr = " from it in this.board \r\n where this.board.rowStatus == 4 \r\n select it";
            type = CommCheckRuleDeal.ExpressionIsConditionOrQuery(expr);
            Assert.IsTrue(type == EXPRESSIONTYPE.QUERYTYPE);

            expr = "  it in this.board \r\n where this.board.rowStatus == 4 \r\n select it";
            type = CommCheckRuleDeal.ExpressionIsConditionOrQuery(expr);
            Assert.IsTrue(type == EXPRESSIONTYPE.INVALIDTYPE);
        }

        [TestMethod()]
        public void GetParaByConditionalExpTest()
        {
            string expr = "  it in this.board \r\n where this.board.rowStatus == 4 \r\n select it";
            List<string> paraList;
            bool res = CommCheckRuleDeal.GetParaByConditionalExp(expr, out paraList);
            Assert.IsTrue(res == false);

            expr = " where cur.board.rowStatus == 4";
            List<string> realParaList = new List<string>() { "cur.board.rowStatus", "4" };
            res = CommCheckRuleDeal.GetParaByConditionalExp(expr, out paraList);
            Assert.IsTrue(res == true);
            int loop = 0;
            foreach (var tmp in realParaList)
            {
                Assert.IsTrue(tmp == paraList[loop]);
                loop++;
            }
            Assert.IsTrue(paraList.Count == realParaList.Count);
        }

        [TestMethod()]
        public void CalculateConditionExprTest()
        {
            string expr = " where cur.board.rowStatus == 4";
            string calRes;
            Dictionary<string, object> paraDic = new Dictionary<string, object>();
            paraDic.Add("cur.board.rowStatus", "6");
            bool res = CommCheckRuleDeal.CalculateConditionExpr(expr, paraDic, out calRes);
            Assert.IsTrue(res == true);
            Assert.IsTrue(calRes == false.ToString());

            expr = "  it in this.board \r\n where this.board.rowStatus == 4 \r\n select it";
            res = CommCheckRuleDeal.CalculateConditionExpr(expr, paraDic, out calRes);
            Assert.IsTrue(res == false);
        }

        [TestMethod()]
        public void SplitQueryExprTest()
        {
            string expr = " where cur.board.rowStatus == 4";
            Dictionary<string, string> paraDic;
            bool res = CommCheckRuleDeal.SplitQueryExpr(expr, out paraDic);
            Assert.IsTrue(res == false);

            expr = " from it in this.board \r\n where this.board.rowStatus == 4 \r\n select it";
            res = CommCheckRuleDeal.SplitQueryExpr(expr, out paraDic);
            Assert.IsTrue(res == true);
            Assert.IsTrue(paraDic.Count == 3);
            string outStr;
            Assert.IsTrue(paraDic.TryGetValue("selectPart", out outStr));
            Assert.IsTrue(outStr.Equals("select it"));
            Assert.IsTrue(paraDic.TryGetValue("fromPart", out outStr));
            Assert.IsTrue(outStr.Equals("from it in this.board"));
            Assert.IsTrue(paraDic.TryGetValue("wherePart", out outStr));
            Assert.IsTrue(outStr.Equals("where this.board.rowStatus == 4"));

            expr = " from it in this.board select it";
            res = CommCheckRuleDeal.SplitQueryExpr(expr, out paraDic);
            Assert.IsTrue(res == true);
            Assert.IsTrue(paraDic.Count == 2);
            Assert.IsTrue(paraDic.TryGetValue("selectPart", out outStr));
            Assert.IsTrue(outStr.Equals("select it"));
            Assert.IsTrue(paraDic.TryGetValue("fromPart", out outStr));
            Assert.IsFalse(paraDic.TryGetValue("wherePart", out outStr));

            expr = " select it";
            res = CommCheckRuleDeal.SplitQueryExpr(expr, out paraDic);
            Assert.IsTrue(res == false);
        }

        [TestMethod()]
        public void GetIndexTest()
        {
            string expr = " select it";
            int length;
            int res = CommCheckRuleDeal.GetIndex(expr, "select ", out length);
            Assert.IsTrue(res == 1);
            Assert.IsTrue(length == 7);

            res = CommCheckRuleDeal.GetIndex(expr, "where ", out length);
            Assert.IsTrue(res == -1);
        }

        [TestMethod()]
        public void GetParaByQueryExprTest()
        {
            Dictionary<string, string> splitDic = new Dictionary<string, string>();
            Dictionary<string, List<string>> paraDic;
            splitDic.Add("selectPart", "select it");
            splitDic.Add("wherePart", "where it.netBoardRackNo == cur.netIROptPlanEntry.netIROfpPortRackNo && it.netBoardShelfNo == cur.netIROptPlanEntry.netIROfpPortShelfNo && it.netBoardSlotNo == cur.netIROptPlanEntry.netIROfpPortSlotNo");
            splitDic.Add("fromPart", "from it in mib.netBoardEntry");
            bool res = CommCheckRuleDeal.GetParaByQueryExpr(splitDic, out paraDic);
            Assert.IsTrue(res == true && paraDic.Count == 3);
            List<string> selectParaList;
            List<string> whereParaList;
            List<string> fromParaList;
            List<string> whereRealList = new List<string>();
            whereRealList.Add("it.netBoardRackNo");
            whereRealList.Add("cur.netIROptPlanEntry.netIROfpPortRackNo");
            whereRealList.Add("it.netBoardShelfNo");
            whereRealList.Add("cur.netIROptPlanEntry.netIROfpPortShelfNo");
            whereRealList.Add("it.netBoardSlotNo");
            whereRealList.Add("cur.netIROptPlanEntry.netIROfpPortSlotNo");
            Assert.IsTrue(paraDic.TryGetValue("selectParaList", out selectParaList) && selectParaList.Count == 1 && selectParaList[0] == "it");
            Assert.IsTrue(paraDic.TryGetValue("fromParaList", out fromParaList) && fromParaList.Count == 1 && fromParaList[0] == "mib.netBoardEntry");
            Assert.IsTrue(paraDic.TryGetValue("whereParaList", out whereParaList) && whereParaList.Count == 6);
            int loop = 0;
            foreach (var tmp in whereRealList)
            {
                Assert.IsTrue(whereParaList[loop] == tmp);
                loop++;
            }
        }

        [TestMethod()]
        public void GetParaByQueryExprTest1()
        {
            //没有from
            Dictionary<string, string> splitDic = new Dictionary<string, string>();
            Dictionary<string, List<string>> paraDic;
            splitDic.Add("selectPart", "select it");
            splitDic.Add("wherePart", "where it.netBoardRackNo == cur.netIROptPlanEntry.netIROfpPortRackNo && it.netBoardShelfNo == cur.netIROptPlanEntry.netIROfpPortShelfNo && it.netBoardSlotNo == cur.netIROptPlanEntry.netIROfpPortSlotNo");
            bool res = CommCheckRuleDeal.GetParaByQueryExpr(splitDic, out paraDic);
            Assert.IsTrue(res == false);
        }

        [TestMethod()]
        public void GetParaByQueryExprTest2()
        {
            //select没有填写为it
            Dictionary<string, string> splitDic = new Dictionary<string, string>();
            Dictionary<string, List<string>> paraDic;
            splitDic.Add("selectPart", "select it.netBoardType");
            splitDic.Add("wherePart", "where it.netBoardRackNo == cur.netIROptPlanEntry.netIROfpPortRackNo && it.netBoardShelfNo == cur.netIROptPlanEntry.netIROfpPortShelfNo && it.netBoardSlotNo == cur.netIROptPlanEntry.netIROfpPortSlotNo");
            splitDic.Add("fromPart", "from it in mib.netBoardEntry");
            bool res = CommCheckRuleDeal.GetParaByQueryExpr(splitDic, out paraDic);
            Assert.IsTrue(res == true);
        }

        [TestMethod()]
        public void GetFilterWhereTest()
        {
            string expr = " where cur.board.rowStatus == 4";
            Dictionary<string, object> paraValueDic = new Dictionary<string, object>();
            paraValueDic.Add("cur.board.rowStatus", 6);
            List<object> paramList;
            string res = CommCheckRuleDeal.GetFilterWhere(expr, paraValueDic, out paramList);
            Assert.IsTrue(res == " where @0 == 4");
            Assert.IsTrue(paramList.Count == 1 && Convert.ToInt32(paramList[0]) == 6);
        }
        [TestMethod()]
        public void GetFilterWhereTest1()
        {
            string expr = "where it.netBoardRackNo == cur.netIROptPlanEntry.netIROfpPortRackNo && it.netBoardShelfNo == cur.netIROptPlanEntry.netIROfpPortShelfNo && it.netBoardSlotNo == cur.netIROptPlanEntry.netIROfpPortSlotNo";
            Dictionary<string, object> paraValueDic = new Dictionary<string, object>();
            paraValueDic.Add("cur.netIROptPlanEntry.netIROfpPortRackNo", 0);
            paraValueDic.Add("cur.netIROptPlanEntry.netIROfpPortShelfNo", 0);
            paraValueDic.Add("cur.netIROptPlanEntry.netIROfpPortSlotNo", "6");//如果是字符串

            List<object> paramList;
            string res = CommCheckRuleDeal.GetFilterWhere(expr, paraValueDic, out paramList);
            Assert.IsTrue(res == "where it.netBoardRackNo == @0 && it.netBoardShelfNo == @1 && it.netBoardSlotNo == @2");
            Assert.IsTrue(paramList.Count == 3 && Convert.ToInt32(paramList[0]) == 0 && Convert.ToInt32(paramList[1]) == 0 && Convert.ToString(paramList[2]) == "6");
        }

        [TestMethod()]
        public void GetFilterWhereTest2()
        {
            List<RoundRule> roundRuleList = new List<RoundRule>();
            RoundRule rule = new RoundRule();
            rule.round = 1;
            rule.rules = "where this.netLocalCellCtrlEntry.netPlanControlLcConfigSwitch !=0";
            roundRuleList.Add(rule);
            rule = new RoundRule();
            rule.round = 3;
            rule.rules = "this.netLocalCellCtrlEntry.lcid";
            roundRuleList.Add(rule);
            StringBuilder strWhere = new StringBuilder();
            int id = 3;
            List<Object> paramList = new List<object>();
            string r = "lcid";
            strWhere.AppendFormat("round==@1 ");
            strWhere.AppendFormat("&& rules.Contains (@0)");
            paramList.Add(r);
            paramList.Add(id); //只要paramList的添加顺序与strWhere中的一致序号即可
            //ok:round=={0} && rules.Contains (@1)
            //OK:round==@0 && rules.Contains (@1)
            //strWhere.AppendFormat("round==@0 && rules.Contains (@1)", id, r);
            //strWhere.AppendFormat("&& rules == @{0}", @"where this.netLocalCellCtrlEntry.lcid ==0");
            //paramList.Add(r);
            //select ok:it.rules, rules,new(rules, round)
            var list = roundRuleList.AsQueryable().Where(strWhere.ToString(), paramList.ToArray()).Select("it");
            //如果不是新建一个对象来查询结果，可以直接var使用；否则需要使用dynamic tmp in list的方式 
            //如果返回的是it，也得使用dynamic 或者显式写成类
            Console.WriteLine(list.Count());
        }

        [TestMethod()]
        public void GetFilterWhereTest3()
        {
            //缺少参数值，该函数不做保护~~
            string expr = "where it.netBoardRackNo == cur.netIROptPlanEntry.netIROfpPortRackNo && it.netBoardShelfNo == cur.netIROptPlanEntry.netIROfpPortShelfNo && it.netBoardSlotNo == cur.netIROptPlanEntry.netIROfpPortSlotNo";
            Dictionary<string, object> paraValueDic = new Dictionary<string, object>();
            paraValueDic.Add("cur.netIROptPlanEntry.netIROfpPortRackNo", 0);
            paraValueDic.Add("cur.netIROptPlanEntry.netIROfpPortShelfNo", 0);
            List<object> paramList;
            string res = CommCheckRuleDeal.GetFilterWhere(expr, paraValueDic, out paramList);
            Assert.IsTrue(res == "where it.netBoardRackNo == @0 && it.netBoardShelfNo == @1 && it.netBoardSlotNo == cur.netIROptPlanEntry.netIROfpPortSlotNo");
        }

        [TestMethod()]
        public void GetParaByQueryExprTest3()
        {
            string expr = " from it in this.board \r\n where it.rowStatus == 4 \r\n select it";
            Dictionary<string, List<string>> paraDic;
            List<string> selectParaList;
            List<string> whereParaList;
            List<string> fromParaList;
            bool res = CommCheckRuleDeal.GetParaByQueryExpr(expr, out paraDic);
            Assert.IsTrue(res == true);
            Assert.IsTrue(paraDic.TryGetValue("selectParaList", out selectParaList) && selectParaList.Count == 1 && selectParaList[0] == "it");
            Assert.IsTrue(paraDic.TryGetValue("fromParaList", out fromParaList) && fromParaList.Count == 1 && fromParaList[0] == "this.board");
            Assert.IsTrue(paraDic.TryGetValue("whereParaList", out whereParaList) && whereParaList.Count == 2 && whereParaList[0] == "it.rowStatus" && whereParaList[1] == "4");
        }

        [TestMethod()]
        public void GetParaByQueryExprTest4()
        {
            string expr = " from it in this.board \r\n where it.rowStatus == 4";
            Dictionary<string, List<string>> paraDic;
            List<string> selectParaList;
            List<string> whereParaList;
            List<string> fromParaList;
            bool res = CommCheckRuleDeal.GetParaByQueryExpr(expr, out paraDic);
            Assert.IsTrue(res == false);
        }

        [TestMethod()]
        public void GetFilterWhereTest4()
        {
            //12.3对于Contains进行转换
            Dictionary<string, object> paraDic = new Dictionary<string, object>();
            List<string> cellList = new List<string> {"1","3","5"};
            paraDic.Add("query1", cellList);
            paraDic.Add("it.id", "1");
            string wherePart = "query1 Contains it.id";
            List<object> paramList;
            string res = CommCheckRuleDeal.GetFilterWhere(wherePart, paraDic, out paramList);
            Assert.IsTrue(res.Equals("@0.Contains (it.id)"));

            wherePart = "(query1 Contains it.id) && (it.id != -1)";            
            res = CommCheckRuleDeal.GetFilterWhere(wherePart, paraDic, out paramList);
            Assert.IsTrue(res.Equals("(@0.Contains (it.id)) && (it.id != -1)"));
        }
    }
}
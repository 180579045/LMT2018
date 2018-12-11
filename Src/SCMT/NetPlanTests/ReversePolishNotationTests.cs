using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogManager;

namespace NetPlan.Tests
{
    [TestClass()]
    public class ReversePolishNotationTests
    {
        public ReversePolishNotationTests()
        {
            Log.SetLogFileName("ReversePolishNotationTests.log");
        }

        [TestMethod()]
        public void getOpeLevelTest()
        {
            Dictionary<string, int> sign = new Dictionary<string, int>();
            sign.Add("(", 1);
            sign.Add(")", 1);
            sign.Add("#", 0);
            sign.Add("<", 5);
            sign.Add(">", 5);
            sign.Add("<=", 5);
            sign.Add(">=", 5);
            sign.Add("==", 4);
            sign.Add("!=", 4);
            sign.Add("&&", 3);
            sign.Add("||", 2);
            sign.Add("Contains", 9);
            sign.Add("~", -1);
            foreach (var tmp in sign)
            {
                int res = ReversePolishNotation.getOpeLevel(tmp.Key);
                Assert.IsTrue(tmp.Value == res);
            }
        }

        [TestMethod()]
        public void IsOperatorTest()
        {
            Dictionary<string, bool> sign = new Dictionary<string, bool>();
            sign.Add("(", false);
            sign.Add(")", false);
            sign.Add("#", false);
            sign.Add("thi.netboard", false);
            sign.Add("<", true);
            sign.Add(">", true);
            sign.Add("<=", true);
            sign.Add(">=", true);
            sign.Add("==", true);
            sign.Add("!=", true);
            sign.Add("&&", true);
            sign.Add("||", true);
            sign.Add("~", false);
            foreach (var tmp in sign)
            {
                bool res = ReversePolishNotation.IsOperator(tmp.Key);
                Assert.IsTrue(tmp.Value == res);
            }
        }

        [TestMethod()]
        public void IsOperatorTest1()
        {
            string[] sign = { "=", "<", "<", "<", "q", "(" };
            string[] nextSign = { " ", "", "=", "q", "<", "l" };
            bool[] rightRes = { false, true, true, true, false, true };
            int[] len = { 1, 1, 2, 1, 0, 1 };
            int length;
            for (int loop = 0; loop < sign.Length; loop++)
            {
                bool res = ReversePolishNotation.IsOperator(sign[loop], nextSign[loop], out length);
                Assert.IsTrue(res == rightRes[loop]);
                if (res == true)
                {
                    Assert.IsTrue(length == len[loop]);
                }
            }

        }

        [TestMethod()]
        public void CalculateSignExprTest()
        {
            string[] opera1 = { "2", "9", "9", true.ToString(), false.ToString(), false.ToString(), true.ToString(), "2", "2", "16", "16", "2", false.ToString(), "4" };
            string[] opera2 = { "8", "4", "4", false.ToString(), false.ToString(), true.ToString(), true.ToString(), "1", "2", "1", "16", "2", true.ToString(), "1" };
            string[] sign = { "*", "/", "%", "||", "||", "&&", "&&", "==", "==", "<", ">=", "+", "!=", ">>" };
            string[] rightRes = { "16", "2", "1", true.ToString(), false.ToString(), false.ToString(), true.ToString(), false.ToString(), true.ToString(), false.ToString(), true.ToString(), "4", true.ToString(), "2" };
            for (int loop = 0; loop < sign.Length; loop++)
            {
                string res = ReversePolishNotation.CalculateSignExpr(opera1[loop], opera2[loop], sign[loop]);
                Assert.IsTrue(res == rightRes[loop]);
            }
        }

        [TestMethod()]
        public void getTokenTest()
        {
            string exptr = "query1.board <  this.b";
            string token;
            int len;
            TOKENFLAG res = ReversePolishNotation.getToken(exptr, out token, out len);
            Assert.IsTrue(res == TOKENFLAG.Tokenpara);
            Assert.IsTrue(token == "query1.board");
            Assert.IsTrue(len == "query1.board".Length);

            exptr = " <  this.b";
            res = ReversePolishNotation.getToken(exptr, out token, out len);//第一个空格字符长度要算在里面
            Assert.IsTrue(res == TOKENFLAG.Tokenop);
            Assert.IsTrue(token == "<");
            Assert.IsTrue(len == " <".Length);

            exptr = "  this.b ";
            res = ReversePolishNotation.getToken(exptr, out token, out len);
            Assert.IsTrue(res == TOKENFLAG.Tokenpara);
            Assert.IsTrue(token == "this.b");
            Assert.IsTrue(len == "  this.b".Length);

            exptr = " ";
            res = ReversePolishNotation.getToken(exptr, out token, out len);
            Assert.IsTrue(res == TOKENFLAG.Tokenend);
        }

        [TestMethod()]
        public void ParseExprTest()
        {
            string expr = " this.netRRUAntennaSettingEntry.netRRURowStatus == 6";
            List<string> rightRes = new List<string>() { "this.netRRUAntennaSettingEntry.netRRURowStatus", "6", "==" };
            List<string> exprPolishList;
            bool res = ReversePolishNotation.ParseExpr(expr, out exprPolishList);
            Assert.IsTrue(res == true);
            Assert.IsTrue(exprPolishList.Count == rightRes.Count);
            for (int loop = 0; loop < rightRes.Count; loop++)
            {
                Assert.IsTrue(rightRes[loop] == exprPolishList[loop]);
            }

            expr = " it.netBoardRackNo == cur.netIROptPlanEntry.netIROfpPortRackNo && it.netBoardShelfNo == cur.netIROptPlanEntry.netIROfpPortShelfNo && (it.netBoardSlotNo == this.netIROptPlanEntry.netIROfpPortSlotNo || it.aaa == this.aaa)";
            rightRes = new List<string>() { "it.netBoardRackNo", "cur.netIROptPlanEntry.netIROfpPortRackNo", "==", "it.netBoardShelfNo", "cur.netIROptPlanEntry.netIROfpPortShelfNo", "==", "&&", "it.netBoardSlotNo", "this.netIROptPlanEntry.netIROfpPortSlotNo", "==", "it.aaa", "this.aaa", "==", "||", "&&" };
            res = ReversePolishNotation.ParseExpr(expr, out exprPolishList);
            Assert.IsTrue(res == true);
            Assert.IsTrue(exprPolishList.Count == rightRes.Count);
            for (int loop = 0; loop < rightRes.Count; loop++)
            {
                Assert.IsTrue(rightRes[loop] == exprPolishList[loop]);
            }
        }

        [TestMethod()]
        public void CalculatePolishExpTest()
        {
            string expr = " it.netBoardRackNo == cur.netIROptPlanEntry.netIROfpPortRackNo && it.netBoardShelfNo == cur.netIROptPlanEntry.netIROfpPortShelfNo && (it.netBoardSlotNo == this.netIROptPlanEntry.netIROfpPortSlotNo || it.aaa == this.aaa)";
            List<string> rightRes = new List<string>() { "it.netBoardRackNo", "cur.netIROptPlanEntry.netIROfpPortRackNo", "==", "it.netBoardShelfNo", "cur.netIROptPlanEntry.netIROfpPortShelfNo", "==", "&&", "it.netBoardSlotNo", "this.netIROptPlanEntry.netIROfpPortSlotNo", "==", "it.aaa", "this.aaa", "==", "||", "&&" };
            List<string> exprPolishList;
            bool res = ReversePolishNotation.ParseExpr(expr, out exprPolishList);
            Assert.IsTrue(res == true);
            Dictionary<string, string> paraValueDic = new Dictionary<string, string>();
            string exprResult;
            //1.第一个&&就失败
            paraValueDic.Add("it.netBoardRackNo", "0");
            paraValueDic.Add("cur.netIROptPlanEntry.netIROfpPortRackNo", "1");
            paraValueDic.Add("it.netBoardShelfNo", "0");
            paraValueDic.Add("cur.netIROptPlanEntry.netIROfpPortShelfNo", "0");
            paraValueDic.Add("it.netBoardSlotNo", "6");
            paraValueDic.Add("this.netIROptPlanEntry.netIROfpPortSlotNo", "6");
            paraValueDic.Add("it.aaa", "7");
            paraValueDic.Add("this.aaa", "7");

            res = ReversePolishNotation.CalculatePolishExp(exprPolishList, paraValueDic, out exprResult);
            Assert.IsTrue(res == true);
            Assert.IsTrue(exprResult == false.ToString());

            //2.计算为true结果
            paraValueDic.Remove("it.netBoardRackNo");
            paraValueDic.Add("it.netBoardRackNo", "1");
            res = ReversePolishNotation.CalculatePolishExp(exprPolishList, paraValueDic, out exprResult);
            Assert.IsTrue(res == true);
            Assert.IsTrue(exprResult == true.ToString());

            //3.括号号||失败
            paraValueDic.Remove("it.netBoardSlotNo");
            paraValueDic.Add("it.netBoardSlotNo", "5");
            paraValueDic.Remove("it.aaa");
            paraValueDic.Add("it.aaa", "5");
            res = ReversePolishNotation.CalculatePolishExp(exprPolishList, paraValueDic, out exprResult);
            Assert.IsTrue(res == true);
            Assert.IsTrue(exprResult == false.ToString());

            //4.缺少某个参数值
            paraValueDic.Remove("it.aaa");
            res = ReversePolishNotation.CalculatePolishExp(exprPolishList, paraValueDic, out exprResult);
            Assert.IsTrue(res == false);
        }

        [TestMethod()]
        public void CalculatePolishExpTest1()
        {
            bool res;
            List<string> exprPolishList = new List<string>();
            Dictionary<string, string> paraValueDic = new Dictionary<string, string>();
            string exprResult;
            //1.波兰表达式为空
            paraValueDic.Add("it.netBoardRackNo", "0");
            paraValueDic.Add("cur.netIROptPlanEntry.netIROfpPortRackNo", "1");
            res = ReversePolishNotation.CalculatePolishExp(exprPolishList, paraValueDic, out exprResult);
            Assert.IsTrue(res == false);

            //2.where all语句,表达不需要判断条件
            exprPolishList.Add("all");
            res = ReversePolishNotation.CalculatePolishExp(exprPolishList, paraValueDic, out exprResult);
            Assert.IsTrue(res == true);
            Assert.IsTrue(exprResult == true.ToString());

            //3.波兰表达式存在一目运算符等错误
            exprPolishList.RemoveAt(0);
            exprPolishList.Add("aa");
            exprPolishList.Add("bb");
            exprPolishList.Add("=");
            res = ReversePolishNotation.CalculatePolishExp(exprPolishList, paraValueDic, out exprResult);
            Assert.IsTrue(res == false);
        }

        [TestMethod()]
        public void CalculatePolistExpTest()
        {
            //鉴于与CalculatePolishExp是一样的，只是入参有点区别，故不做太多测试
            bool res;
            Dictionary<string, object> paraValueDic = new Dictionary<string, object>();
            string exprResult;
            string expr = "where all";
            res = ReversePolishNotation.CalculatePolishExp(expr, paraValueDic, out exprResult);
            Assert.IsTrue(res == true);
            Assert.IsTrue(exprResult == true.ToString());

            expr = " all";
            res = ReversePolishNotation.CalculatePolishExp(expr, paraValueDic, out exprResult);
            Assert.IsTrue(res == true);
            Assert.IsTrue(exprResult == true.ToString());

            expr = " aa > bb";
            paraValueDic.Add("aa", 5.ToString());
            paraValueDic.Add("bb", 51.ToString());
            res = ReversePolishNotation.CalculatePolishExp(expr, paraValueDic, out exprResult);
            Assert.IsTrue(res == true);
            Assert.IsTrue(exprResult == false.ToString());

            expr = " cur.aa <= 5";
            paraValueDic.Add("cur.aa", 5.ToString());
            res = ReversePolishNotation.CalculatePolishExp(expr, paraValueDic, out exprResult);
            Assert.IsTrue(res == true);
            Assert.IsTrue(exprResult == true.ToString());

            //验证Contains
            expr = "query1 Contains 2";
            List<string> list1 = new List<string> { "3", "5", "18", "2" };
            paraValueDic.Add("query1", list1);
            res = ReversePolishNotation.CalculatePolishExp(expr, paraValueDic, out exprResult);
            Assert.IsTrue(res == true);
        }

        [TestMethod()]
        public void CalculateSignExprTest1()
        {
            List<string> list1 = new List<string> {"3", "5", "18", "2"};
            string para2 = "18";
            string res = ReversePolishNotation.CalculateSignExpr(list1, para2, "Contains");
            Assert.IsTrue(res == true.ToString());

            para2 = "1";
            res = ReversePolishNotation.CalculateSignExpr(list1, para2, "Contains");
            Assert.IsTrue(res == false.ToString());

            List<int> listint = new List<int> { 3,5,18,2 };
            para2 = "1";
            res = ReversePolishNotation.CalculateSignExpr(listint, para2, "Contains");
            Assert.IsTrue(string.IsNullOrEmpty(res));
        }
    }
}
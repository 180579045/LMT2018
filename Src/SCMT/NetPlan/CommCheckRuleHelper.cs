using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LogManager;
using Microsoft.CSharp;

namespace NetPlan
{
    public enum EXPRESSIONTYPE
    {
        CONDITIONTYPE = 0,        // 条件语句
        QUERYTYPE = 1,        // 查询
        INVALIDTYPE = -1  //未知类型
    }
    public class RoundRule
    {
        public int round { get; set; } //规则第N步
        public string rules { get; set; }  //规则表达式
        public string outvar { get; set; }  //该规则表达式输出变量，可以为空
    }

    public class CheckProperty
    {
        public string condition { get; set; }  //被校验参数取值满足的条件，填写为all时，表示不区分场景
    }
    public class BaseCheckRule
    {
        public int id;//规则ID，大排行
        public string desc { get; set; }  //规则描述
        public string property { get; set; } //被校验参数满足什么条件才需要进行校验
        public List<RoundRule> exp { get; set; } //规则表达式，一个BaseCheckRule只能校验一种情况
        public string false_tip { get; set; }   //校验失败，返回给UI的错误提示信息

        public BaseCheckRule()
        {
            exp = new List<RoundRule>();
        }     
    }
    public class CommCheckRuleHelper
    {
        public CommCheckRuleHelper()
        {
            
        }
        
        //判断表达式是条件还是查询语句
        public static EXPRESSIONTYPE ExpressionIsConditionOrQuery(string expression)
        {
            string expr = expression.Trim();
            //条件语句以where开头
            //查询语句以from .. in ..开头，以select..语句结尾,where条件可能会有
            string pattern = @"^where\s+.*"; 
            if (Regex.Matches(expr, pattern).Count > 0)
            {
                return EXPRESSIONTYPE.CONDITIONTYPE;
            }
            pattern = @"^from\s+\w+\s+in\s+\w+.*\s+(\r\n\s+)?where\s+.*\s+(\r\n\s+)?select\s+\w+";
            if (Regex.Matches(expr, pattern).Count > 0)
            {
                return EXPRESSIONTYPE.QUERYTYPE;
            }
            return EXPRESSIONTYPE.INVALIDTYPE;
        }

        //条件语句，取出表达式中使用到的变量
        public static bool GetParaByConditionalExp(string expression, out List<string> variableList)
        {
            string expr = expression.Trim();
            List<string> keyWordList;
            variableList = new List<string>();
            if (EXPRESSIONTYPE.CONDITIONTYPE != ExpressionIsConditionOrQuery(expr))
            {
                Log.Error("The expr is not conditional expr :" + expr);
                return false;
            }
            //去除where头，保留纯表达式
            expr = expr.Substring("where".Length).Trim();
            if (!ReversePolishNotation.ParseExpr(expr, out keyWordList))
            {
                Log.Error("ReversePolishNotation error :"+ expr);
                return false;
            }
            foreach (var keyWord in keyWordList)
            {
                if (!ReversePolishNotation.IsOperator(keyWord))
                {
                    //不是操作符，则保存变量名返回查询数值
                    //注意，里面可能有包含纯值的参数（非变量名)
                    variableList.Add(keyWord);
                }
            }
            if (variableList.Count == 0)
            {
                Log.Error("Get variablelist is empty!");
                return false;
            }
            return true;
        }

        //条件语句，使用波兰表达式计算
        public static bool CalculateConditionExpr(String expr, Dictionary<string, object> paraValueDic, out string exprResult)
        {
            //进行简单的保护，以where开头
            if (!expr.Trim().StartsWith("where"))
            {
                exprResult = "";
                return false;
            }
            //去除where头，保留纯表达式
            expr = expr.Trim().Substring("where".Length);
            return ReversePolishNotation.CalculatePolishExp(expr, paraValueDic, out exprResult);
        }
        //查询语句进行转换成linq语句
        public static bool SplitQueryExpr(string rules, out Dictionary<string, string> splitDic)
        {
            splitDic = new Dictionary<string, string>();
            string rulesInfo = rules;
            int length;
            int index = GetIndex(rulesInfo, "select ", out length);
            if (-1 != index)
            {
                var selctPart = rules.Substring(index).Replace('\n', ' ').Replace('\r', ' ').Trim();
                splitDic.Add("selectPart", selctPart);
                rulesInfo = rulesInfo.Substring(0, index);
            }
            else
            {
                Log.Error("the rules must have selct word!");
                return false;
            }
            index = GetIndex(rulesInfo, "where ", out length);
            if (-1 != index)
            {
                string wherePart = rulesInfo.Substring(index).Replace('\n', ' ').Replace('\r', ' ').Trim();
                splitDic.Add("wherePart", wherePart);
                rulesInfo = rulesInfo.Substring(0, index);
            }
            
            index = GetIndex(rulesInfo, "from ", out length);
            if (-1 == index)
            {
                Log.Error("the rules must have from word!");
                return false;
            }
            string fromPart = rulesInfo.Substring(index).Replace('\n', ' ').Replace('\r', ' ').Trim();
            splitDic.Add("fromPart", fromPart);
            return true;
        }

        public static int GetIndex(string rulesInfo, string key, out int length)
        {
            length = 0;
            int index = rulesInfo.IndexOf(key, StringComparison.Ordinal);
            if (index != -1)
            {
                length = key.Length;
            }
            return index;
        }

        public static bool GetParaByQueryExpr(Dictionary<string, string> splitDic, out Dictionary<string, List<string>> paraDic)
        {
            paraDic = new Dictionary<string, List<string>>();
            //前期处理，肯定是有from it in...
            //from语句，取集合名称
            string fromPart;
            if (!splitDic.TryGetValue("fromPart", out fromPart))
            {
                Log.Error("Get fromPart error!");
                return false;
            }
            int length;
            int index = GetIndex(fromPart, " in ", out length);
            List<string> fromList = new List<string>();
            fromList.Add(fromPart.Substring(index + length).Trim(' '));
            paraDic.Add("fromParaList", fromList);
            //取条件语句中的参数
            string wherePart;
            if (splitDic.TryGetValue("wherePart", out wherePart))
            {
                List<string> whereList;
                if (!GetParaByConditionalExp(wherePart, out whereList))
                {
                    Log.Error("Get where para error!");
                    return false;
                }
                paraDic.Add("whereParaList", whereList);
            }
            //取select语句中的参数
            string selectPart;
            if (!splitDic.TryGetValue("selectPart", out selectPart))
            {
                Log.Error("Get selectPart error!");
                return false;
            }
            index = GetIndex(selectPart, "select ", out length);
            List<string> selectList = new List<string>();
            string strIt = selectPart.Substring(index + length).Trim(' ');
            //只能填写it -- 11.15修改可支持it.xx
            if ("it".Equals(strIt))
            {
                selectList.Add(strIt);
            }
            else
            {
                //只支持it.XX.XX这样的,且数据结果只能是List<string>类型,后面获取数据时作校验保护
                if (string.IsNullOrEmpty(strIt))
                {
                    Log.Error(" select para is null");
                    return false;
                }
                string pattern = @"^\bit\.\b\w+$";
                MatchCollection match = Regex.Matches(strIt, pattern);
                if (match.Count == 1)
                {
                    selectList.Add(match[0].Value);
                }
                else
                {
                    Log.Error(" select para: "+ strIt + " is invalid");
                    return false;
                }
            }
            paraDic.Add("selectParaList", selectList);
            return true;
        }
        public static bool GetParaByQueryExpr(string rules, out Dictionary<string, List<string>> paraDic)
        {
            paraDic = new Dictionary<string, List<string>>();
            Dictionary<string, string> splitDic;
            if (false == SplitQueryExpr(rules, out splitDic))
            {
                Log.Error("SplitQueryExpr error!");
                return false;
            }
            //前期处理，肯定是有from it in...
            //from语句，取集合名称
            string fromPart;
            if (!splitDic.TryGetValue("fromPart", out fromPart))
            {
                Log.Error("Get fromPart error!");
                return false;
            }
            int length;
            int index = GetIndex(fromPart, "in ", out length);
            List<string> fromList = new List<string>();
            fromList.Add(fromPart.Substring(index + length).Trim(' '));
            paraDic.Add("fromParaList", fromList);
            //取条件语句中的参数
            string wherePart;
            if (splitDic.TryGetValue("wherePart", out wherePart))
            {
                List<string> whereList;
                if (!GetParaByConditionalExp(wherePart, out whereList))
                {
                    Log.Error("Get where para error!");
                    return false;
                }
                paraDic.Add("whereParaList", whereList);
            }
            //取select语句中的参数
            string selectPart;
            if (!splitDic.TryGetValue("selectPart", out selectPart))
            {
                Log.Error("Get selectPart error!");
                return false;
            }
            index = GetIndex(selectPart, "select ", out length);
            List<string> selectList = new List<string>();
            //只能填写it
            string strIt = selectPart.Substring(index + length).Trim(' ');
            if (!"it".Equals(strIt))
            {
                Log.Error("Select setence is not equal it!");
                return false;
            }
            selectList.Add(strIt);
            paraDic.Add("selectParaList", selectList);
            return true;
        }
        public static string GetFilterWhere(string wherePart, Dictionary<string, Object> paraValueDic, out List<object> paramList)
        {
            StringBuilder strBuilder = new StringBuilder();
            string exptr = wherePart;
            paramList = new List<object>();
            int paraIndex = 0;

            //需要做一个保护，如果paraValueDic参数不全的话返回空
            //携带关键词where的话，则把此去除
            string upateExpr = wherePart.Trim();
            if (upateExpr.StartsWith("where "))
            {
                upateExpr = upateExpr.Substring("where ".Length);
            }
            List<string> exprPolishList;
            if (false == ReversePolishNotation.ParseExpr(upateExpr, out exprPolishList))
            {
                Log.Error("parseExpr " + upateExpr + " to Polish error");
                return null;
            }
            foreach (var keyWord in exprPolishList)
            {
                if (!ReversePolishNotation.IsOperator(keyWord))
                {
                    //如果是以it.开头的参数名，表示是遍历的元素，也是不需要获取参数值的
                    if (keyWord.StartsWith("it."))
                    {
                        continue;
                    }
                    //是有效参数名
                    object dataValue;
                    if (!paraValueDic.TryGetValue(keyWord, out dataValue))
                    {
                        //说明是纯数值，不需要进行转换
                        continue;
                    }
                    int length;
                    int index = GetIndex(exptr, keyWord, out length);
                    if (index != -1)
                    {
                        exptr = exptr.Substring(0, index) + "@" + paraIndex.ToString() +
                                exptr.Substring(index + length);
                        paramList.Add(dataValue);
                        paraIndex++;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            strBuilder.AppendFormat(exptr);

            string convertBuilder = strBuilder.ToString();
            //最后一步，需要将Contains特殊处理下,back部分不太好获取，规则较复杂
            string pattern = @"\w+\s+Contains\s+";
            MatchCollection collectionRegex = Regex.Matches(convertBuilder, pattern);
            foreach (var tmp in collectionRegex)
            {
                string value = tmp.ToString();
                string forward = value.Substring(0, value.IndexOf(" ")) + ".Contains ";
                string back = "";
                int length;
                int indexForward = GetIndex(convertBuilder, value, out length);//肯定会找到
                string leftContains = convertBuilder.Substring(indexForward + length);//找到Contains关键词后面的部分
                string leftPart;
                //查找Contains后面的参数，标志是空格间隔
               int indexBack = GetIndex(leftContains, " ", out length);
                if (indexBack == -1)
                {
                    back =  "(" + leftContains + ")";
                    leftPart = "";
                }
                else
                {
                    back = "(" + leftContains.Substring(0, indexBack) + ")";
                    //最后剩下的字段
                    leftPart = leftContains.Substring(indexBack);
                }
                //替换XX Contains YY => XX.Contains (YY)
                convertBuilder = convertBuilder.Substring(0, indexForward) + forward + back + leftPart;
            }
            return convertBuilder;
        }

        public static string GetFilterSelect(List<string> selectList)
        {
            string selectPara = "";
            
            //暂时限制，只能填写为it
            //取select语句中的参数
            foreach (var obj in selectList)
            {
                selectPara += obj.ToString() + ",";
            }
            //删除最后一个,
            int index = selectPara.LastIndexOf(",");
            if (-1 != index)
            {
                selectPara = selectPara.Substring(0, index);
            }
            return selectPara;
        }
        /*
        public bool CalculateQueryExpr<T>(RoundRule roundRule, T collectionPara, Dictionary<string, Object> whereParaValueDic, out Object result)
        {
            Type type = typeof(T);
            Type collectionType = collectionPara.GetType();
            Dictionary<string, string> splitDic;
            result = null;
            if (false == SplitQueryExpr(roundRule.rules, out splitDic))
            {
                Log.Error("SplitQueryExpr error!");
                return false;
            }
            string wherePart;
            if (!splitDic.TryGetValue("wherePart", out wherePart))
            {
                Log.Error("Get wherePart error!");
                return false;
            }
            List<Object> wherePara;
            string whereLinq = GetFilterWhere(wherePart, whereParaValueDic, out wherePara);
            var list = collectionPara.AsQueryable().Where(whereLinq.ToString(), wherePara.ToArray()).Select("it");
        }
        */
    }
}

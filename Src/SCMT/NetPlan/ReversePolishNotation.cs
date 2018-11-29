using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogManager;

namespace NetPlan
{
    public enum TOKENFLAG
    {
        Tokenend = 0,        // 结束
        Tokenop = 1,        // 操作符
        Tokenpara = -1  //操作数
    }
    public class ReversePolishNotation
    {
        public static int getOpeLevel(string sign)
        {
            //目前关注的有>=、<=、==、!=等双目运算符
            switch (sign)
            {
                case "#":
                    return 0;
                case "(":
                case ")":
                    return 1;
                case "||":
                    return 2;
                case "&&":
                    return 3;
                case "==":
                case "!=":
                    return 4;
                case "<":
                case "<=":
                case ">":
                case ">=":
                    return 5;
                case "<<":
                case ">>":
                    return 6;
                case "+":
                //case "-":
                    return 7;
                case "*":
                case "/":
                case "%":
                    return 8;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// 校验关键字是否为操作符,该函数主要用在运算中
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool IsOperator(string sign)
        {
            sign = sign.Trim();
            //括号不会出现在栈中,暂不考虑-减号
            if ( (sign == "*" || sign == "/" || sign == "%")
                || (sign == "+" ) || (sign == "<<" || sign == ">>")
                || (sign == "<=" || sign == ">=") || (sign == "<" || sign == ">")
                || (sign == "==" || sign == "!=") || (sign == "&&") || (sign == "||"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据原始表达式查找并提取操作运算符
        /// </summary>
        /// <param name="sign">当前字符</param>
        /// <param name="nextSign">下一个字符</param>
        /// <param name="length">输出运算符的长度,有的运算符有两个</param>
        /// <returns></returns>
        public static bool IsOperator(string sign, string nextSign, out int length)
        {
            length = 0;
            //只有网规模块使用，暂时不考虑-减号，主要是目前校验规则中有条件语句判断是否为-1，影响计算
            if ((sign == "(" || sign == ")") || (sign == "*" || sign == "/" || sign == "%")
                || (sign == "+"))
            {
                length = sign.Length;
                return true;
            }

            //如下场景需要联合判断下一个字符是否一起拼接成一个运算符
            if (nextSign == " ")
            {
                nextSign = "";
            }
            string newSign = sign + nextSign;
            if ((newSign == "<<" || newSign == ">>")
                     || (newSign == "<=" || newSign == ">=") || (newSign == "<" || newSign == ">")
                     || (newSign == "==" || newSign == "!=") || (newSign == "&&") || (newSign == "||"))
            {
                length = newSign.Length;
                return true;
            }
            else if (sign == "<" || sign == ">")
            {
                //主要是先排除<=， >=的影响
                length = sign.Length;
                return true;
            }               
            else
                return false;
        }
        public static string CalculateSignExpr(string para1, string para2, string sign)
        {
            try
            {
                switch (sign)
                {
                    case "*":
                        return Convert.ToString(Convert.ToInt32(para1) * Convert.ToInt32(para2));
                    case "/":
                        return Convert.ToString(Convert.ToInt32(para1) / Convert.ToInt32(para2));
                    case "%":
                        return Convert.ToString(Convert.ToInt32(para1) % Convert.ToInt32(para2));
                    case "+":
                        return Convert.ToString(Convert.ToInt32(para1) + Convert.ToInt32(para2));
                    /*暂不考虑-减号，影响网规校验
                    case "-":
                        return Convert.ToString(Convert.ToInt32(para1) - Convert.ToInt32(para2));
                        */
                    case "<<":
                        return Convert.ToString(Convert.ToInt32(para1) << Convert.ToInt32(para2));
                    case ">>":
                        return Convert.ToString(Convert.ToInt32(para1) >> Convert.ToInt32(para2));
                    //以上都是整型的计算
                    //>、<、>=、<=也按照整数进行计算（mib中没有float)
                    case "<":
                        return Convert.ToInt32(para1) < Convert.ToInt32(para2) ? true.ToString() : false.ToString();                      
                    case "<=":
                        return Convert.ToInt32(para1) <= Convert.ToInt32(para2) ? true.ToString() : false.ToString();
                    case ">":
                        return Convert.ToInt32(para1) > Convert.ToInt32(para2) ? true.ToString() : false.ToString();
                    case ">=":
                        return Convert.ToInt32(para1) >= Convert.ToInt32(para2) ? true.ToString() : false.ToString();
                    //以上都是整型的计算，下面有可能是字符串比较
                    case "==":
                        {
                            int ret = String.Compare(para1, para2, StringComparison.OrdinalIgnoreCase);
                            return 0 == ret ? true.ToString() : false.ToString();
                        }
                    case "!=":
                        {
                            int ret = string.Compare(para1, para2, StringComparison.OrdinalIgnoreCase);
                            if (0 != ret)
                            {
                                return true.ToString();
                            }
                            else
                            {
                                return false.ToString();
                            }
                        }
                    case "&&":
                        {
                            var paraInt1 = para1.Equals(true.ToString());
                            var paraInt2 = para2.Equals(true.ToString());

                            if (paraInt1 && paraInt2)
                            {
                                return true.ToString();
                            }
                            else
                            {
                                return false.ToString();
                            }
                        }
                    case "||":
                        {
                            var paraInt1 = para1.Equals(true.ToString());
                            var paraInt2 = para2.Equals(true.ToString());
                            if (paraInt1 || paraInt2)
                            {
                                return true.ToString();
                            }
                            else
                            {
                                return false.ToString();
                            }
                        }
                    default:
                        return null;
                }
            }
            catch (Exception e)
            {
                Log.Error("input operator and sign is invalid, para1:" + para1 + " para2: " + para2 + " sign: "+sign);
                throw;
            }
            return null;
        }

        public static TOKENFLAG getToken(string inputStr, out string token, out int tokenLen)
        {
            token = "";
            tokenLen = 0;
            for (int loop = 0; loop < inputStr.Length;)
            {
                //当前字符为空格,则寻找下一个
                string curStr = inputStr[loop].ToString();
                if (" " == curStr)
                {
                    tokenLen++;
                    loop++;
                    continue;
                }

                string nextStr = "";
                if (loop + 1 < inputStr.Length)
                {
                    nextStr = inputStr[loop + 1].ToString();
                }
                int length = 0;
                if (IsOperator(curStr, nextStr, out length))
                {
                    if (length == 1)
                    {
                        token = curStr;
                    }
                    else if (length == 2)
                    {
                        
                        token = curStr + nextStr;
                    }
                    tokenLen += length;
                    return TOKENFLAG.Tokenop;
                }
                else
                {
                    var index = loop;
                    while (index < inputStr.Length && (!IsOperator(curStr, nextStr, out length)) && (curStr != " "))
                    {
                        token = token + curStr;
                        tokenLen++;
                        index++;
                        if (index == inputStr.Length)
                        {
                            break;
                        }
                        curStr = inputStr[index].ToString();
                        if (index + 1 < inputStr.Length)
                        {
                            nextStr = inputStr[index + 1].ToString();
                        }
                        else
                        {
                            nextStr = "";
                        }
                    }
                    return TOKENFLAG.Tokenpara;
                }
                
            }
            return TOKENFLAG.Tokenend;
        }
        //将表达式转换为逆波兰式
        public static bool ParseExpr(string expr, out List<string> exprPolishList)
        {
            Stack<string> opStack = new Stack<string>();//临时存储运算符的栈
            opStack.Push("#");
            bool leftBracket = false;  //左括号存在标志
            bool threeOp = false;      //"+","-","*"存在标志
            string loopStr = expr;//去除首尾的空格
            exprPolishList = new List<string>();
            for (int loop = 0; loop < expr.Length;)
            {
                string token = "";
                int tokenLen = 0;

                TOKENFLAG ret = getToken(loopStr, out token, out tokenLen);
                loop = loop + tokenLen;
                loopStr = expr.Substring(loop);//下一个循环要提取的表达式,
                if (ret == TOKENFLAG.Tokenend)
                {
                    break;

                }
                //若为关键字，则直接送入向量中
                else if (ret == TOKENFLAG.Tokenpara)
                {
                    exprPolishList.Add(token);
                    leftBracket = false;
                    threeOp = false;
                }
                else if (ret == TOKENFLAG.Tokenop)
                {
                    var nowLevel = getOpeLevel(token);
                    if (token == "(")
                    {
                        opStack.Push(token);
                        leftBracket = true;
                        threeOp = false;
                    }
                    else if (token == ")")
                    {
                        if (threeOp)
                        {
                            Log.Error("expression " + expr + " has ) after opration sign");
                            return false;   //操作符后出现了右括号
                        }
                        threeOp = false;
                        leftBracket = false;
                        while (true)
                        {
                            if (opStack.Peek() == "(")
                            {
                                opStack.Pop();
                                break;
                            }
                            if (opStack.Peek() == "#")
                            {
                                Log.Error("expression " + expr + " does not have ( but has )");
                                return false;  //缺少左括号
                            }     
                            else
                            {
                                exprPolishList.Add(opStack.Peek());
                                opStack.Pop();
                            }
                        }
                    }
                    else
                    {
                        if (threeOp)
                        {
                            Log.Error("expression " + expr + " has continuous operation sign");
                            return false; //连续出现两个"+","-","*"等错误
                        }
                        threeOp = true;

                        //若该操作符优先级大于栈顶运算符优先级则入栈,否则出栈送入向量中
                        while (true)
                        {
                            var topLevel = getOpeLevel(opStack.Peek());
                            if (nowLevel > topLevel)
                            {
                                opStack.Push(token);
                                break;
                            }
                            else
                            {
                                exprPolishList.Add(opStack.Peek());
                                opStack.Pop();
                            }
                        }
                        if (leftBracket)
                        {
                            Log.Error("expression " + expr + " has ( after operation sign )");
                            return false; //如果括号后直接跟的是一个操作符则说明填写规则错误
                        }
                        leftBracket = false;
                    }
                }

            }
            while (true)
            {
                if (opStack.Peek() == "#")
                    break;
                if (opStack.Peek() == "(")
                {
                    Log.Error("expression " + expr + " does not have ) but has (");
                    //缺少右括号
                    return false;
                }
                else
                {
                    exprPolishList.Add(opStack.Peek());
                    opStack.Pop();
                }
            }
            Log.Info("test print _exprPolishList:");
            foreach (string temp in exprPolishList)
            {
                Log.Info("****"+ temp.ToString());
            }
            return true;
        }       

        public static bool CalculatePolishExp(List<string> exprPolishList, Dictionary<string,string> paraValueDic, out string exprResult)
        {
            Stack<string> dataValueStack = new Stack<string>();
            exprResult = "";
            if (0 == exprPolishList.Count)
            {
                Log.Error("exprPolishList input is empty");
                return false;
            }

            //特殊情况，条件语句是where all表示什么条件都ok,则直接返回true
            if (exprPolishList.Count == 1 && exprPolishList[0].Equals("all"))
            {
                exprResult = true.ToString();
                return true;
            }

            foreach (var keyWord in exprPolishList)
            {
                if (!IsOperator(keyWord))
                {
                    //是操作数，则从字典中取值
                    if (paraValueDic.ContainsKey(keyWord))
                    {
                        string dataValue;
                        if (!paraValueDic.TryGetValue(keyWord, out dataValue))
                        {
                            Log.Error("can't find value of " + keyWord + "in paraValueDic");
                            return false;
                        }
                        dataValueStack.Push(dataValue);
                    }
                    else
                    {
                        //不包含，则返回失败
                        Log.Error("paraValueDic can't find key " + keyWord);
                        return false;
                    }
                }
                else
                {
                    //目前使用的都是二目运算符,此处限制
                    if (dataValueStack.Count < 2)
                    {
                        Log.Error("dataValueStack is empty, but exist a sign: " + keyWord);
                        return false;
                    }                  
                    var secondData = dataValueStack.Pop();
                    var firstData = dataValueStack.Pop();
                    //将操作数与操作符组合计算，并将最新计算的值放入栈中保存
                    dataValueStack.Push(CalculateSignExpr(firstData, secondData, keyWord));
                }
            }
            //栈顶元素就是计算的最终结果
            exprResult = dataValueStack.Peek();
            return true;
        }

        public static bool CalculatePolishExp(string expr, Dictionary<string, string> paraValueDic, out string exprResult)
        {
            Stack<string> dataValueStack = new Stack<string>();
            List<string> exprPolishList;
            exprResult = "";
            //携带关键词where的话，则把此去除
            string upateExpr = expr.Trim();
            if (upateExpr.StartsWith("where "))
            {
                upateExpr = upateExpr.Substring("where ".Length);
            }
            if (false == ParseExpr(upateExpr, out exprPolishList))
            {
                Log.Error("parseExpr "+ upateExpr + " to Polish error");
                return false;
            }

            if (0 == exprPolishList.Count)
            {
                Log.Error("parseExpr " + expr + " to Polish result is empty");
                return false;
            }
            //特殊情况，条件语句是where all表示什么条件都ok,则直接返回true
            if (exprPolishList.Count == 1 && exprPolishList[0].Equals("all"))
            {
                exprResult = true.ToString();
                return true;
            }

            foreach (var keyWord in exprPolishList)
            {
                if (!IsOperator(keyWord))
                {
                    //是操作数，则从字典中取值
                    if (paraValueDic.ContainsKey(keyWord))
                    {
                        string dataValue;
                        if (!paraValueDic.TryGetValue(keyWord, out dataValue))
                        {
                            Log.Error("can't find value of " + keyWord + "in paraValueDic");
                            return false;
                        }
                        dataValueStack.Push(dataValue);
                    }
                    else
                    {
                        //如果不在paraValueDic中，则认为是值而不是参数名
                        dataValueStack.Push(keyWord);
                    }
                }
                else
                {
                    //目前使用的都是二目运算符,此处限制
                    if (dataValueStack.Count < 2)
                    {
                        Log.Error("dataValueStack is empty, but exist a sign: " + keyWord);
                        return false;
                    }
                    var secondData = dataValueStack.Pop();
                    var firstData = dataValueStack.Pop();
                    //将操作数与操作符组合计算，并将最新计算的值放入栈中保存
                    dataValueStack.Push(CalculateSignExpr(firstData, secondData, keyWord));
                }
            }
            //栈顶元素就是计算的最终结果
            exprResult = dataValueStack.Peek();
            return true;
        }
    }

}

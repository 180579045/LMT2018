using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.IO;
namespace CDLBrowser.Parser.DatabaseMgr
{
    public class MyLog
    {
        public static void Log(string info)
        {
#if DEBUG
            ////1表示忽略堆栈层数
            //System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            ////Trace.WriteLine(string.Format("filename={0}, line={1} ",
            ////    st.GetFrame(0).GetFileName(),
            ////    st.GetFrame(0).GetFileLineNumber()));

            ////从路径获取文件名
            ////string str=@"F:\Test\Codes\code.txt";
            //string str = st.GetFrame(0).GetFileName();
            //int linenum = st.GetFrame(0).GetFileLineNumber();
            //string now = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"); //2008-09-04   08:05:57 
            ////获取调用堆栈方法(函数)名称
            //MethodBase method = st.GetFrame(0).GetMethod();
            //string methodName = method.Name;

            ////method = MethodBase.GetCurrentMethod();
            ////Trace.WriteLine("GetCurrentMethod method.Name = " + method.Name);

            //int x = str.LastIndexOf("\\");
            //string filename = "";

            //if (x > 0)
            //{
            //    filename = str.Substring(x + 1); //获取文件名
            //}
            //int tid = Thread.CurrentThread.ManagedThreadId;

            //string outinfo = string.Format("<{0}>[{5}] {1}:{2}:{3} {4}", now, filename, methodName, linenum, info, tid);

            //File.AppendAllText("dlog.txt", "\r\n"+ outinfo);

            //if (info[info.Length - 1] == '\n')
            //{
            //    Trace.Write(outinfo);
               
                
            //}
            //else
            //{
            //    Trace.WriteLine(outinfo);
            //}
#endif

        }
    }
}

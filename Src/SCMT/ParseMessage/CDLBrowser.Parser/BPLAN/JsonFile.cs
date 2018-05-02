using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLBrowser.Parser.BPLAN
{
    class JsonFile
    {
        public string ReadFile(string filepath)
        {
            string filestr = "";
            try
            {
                FileStream file = new FileStream(filepath, FileMode.Open);
                StreamReader fileread = new StreamReader(file, Encoding.GetEncoding("gb2312"));
                filestr = fileread.ReadToEnd().ToString();
                fileread.Close();
            }
            catch (Exception e)
            {
                //记日志
                Console.WriteLine("open file " + filepath + " failed!");
            }
            return filestr;
        }
        public void WriteFile(string filepath, string content)
        {
            try
            {
                FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);//找到文件如果文件不存在则创建文件如果存在则覆盖文件
                //清空文件
                fs.SetLength(0);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                //记日志
                Console.WriteLine("write file " + filepath + " failed!");
            }
        }
    }
}

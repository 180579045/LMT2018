using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfgFileOperation
{
    /// <summary>
    /// 测试验证 CfgParseFileToShow
    /// </summary>
    class TestCfgParseFileToShow
    {
        public bool TestMain(string[] args)
        {
            Dictionary<string, string> argsPath = GetParamPath(args);
            // 解析一下，在重新写入
            CfgParseFileToShow parseFile = new CfgParseFileToShow();
            parseFile.LoadFileToMemory(argsPath["FilePathA"]);
            parseFile.ReWriteFile(argsPath["FilePathB"]);

            new TestCfgForInitAndPatch().BeyondComparePatchExCfgMain(args);
            return true;
        }

        /// <summary>
        /// 解析参数成固定格式
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        Dictionary<string, string> GetParamPath(string[] args)
        {
            Dictionary<string, string> path = new Dictionary<string, string>();
            foreach (var par in args)
            {
                string par_str = par.ToString();
                int pos = par_str.IndexOf(':');
                if (pos != -1)
                {
                    string key = par_str.Substring(0, pos);
                    string val = par_str.Substring(pos + 1);
                    path.Add(key, val);
                    Console.WriteLine(String.Format("Cmdline CreateInitPatch par: key({0}), val({1}).\n", key, val));
                }
            }
            return path;
        }
    }
}

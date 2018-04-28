/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ReadIniFile$
* 机器名称：       $machinename$
* 命名空间：       $SCMT_json.JSONDataMgr$
* 文 件 名：       $ReadIniFile.cs$
* 创建时间：       $2018.04.20$
* 作    者：       $luanyibo$
* 说   明 ：
*     读取.ini文件
* 修改时间     修 改 人    修改内容：
* 2018.04.20   栾义博      创建文件并实现类 ReadIniFile，and Happy birthday!
*************************************************************************************/
using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace SCMT_json.JSONDataMgr
{
    /// <summary>
    /// 读取配置文件 JsonDataMgr.ini
    /// </summary>
    class ReadIniFile
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
                    string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def,
                    StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// 写INI文件 
        /// </summary>
        /// <param name="filePath"> 指定的ini文件路径</param>
        /// <param name="Section">某个小节名</param>
        /// <param name="Key">上面section下某个项的键名</param>
        /// <param name="Value">上面key对应的value</param>
        public void IniWriteValue(string filePath, string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, filePath);
        }

        /// <summary>
        /// 读取INI文件指定
        /// </summary>
        /// <param name="filePath">指定的ini文件路径</param>
        /// <param name="Section">某个小节名(不区分大小写)</param>
        /// <param name="Key">欲获取信息的某个键名(不区分大小写)</param>
        /// <returns>上面key对应的keyValue</returns>
        public string IniReadValue(string filePath, string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, filePath);
            return temp.ToString();
        }

        public string getIniFilePath(string inifilename)
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            //Console.WriteLine("当前位置:{0}", currentPath);
            //ReadIniFile iniFile = new ReadIniFile();
            //string iniFilePath = currentPath + "JsonDataMgr.ini";

            string getInitPath = currentPath + inifilename;
            //是否存在
            if (!File.Exists(getInitPath))
            {
                return "";
            }
            return getInitPath;
        }

        public void test()
        {
            //string str1 = Process.GetCurrentProcess().MainModule.FileName;//可获得当前执行的exe的文件名。 
            string str2 = Environment.CurrentDirectory;//获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。(备注:按照定义，如果该进程在本地或网络驱动器的根目录中启动，则此属性的值为驱动器名称后跟一个尾部反斜杠（如“C:\”）。如果该进程在子目录中启动，则此属性的值为不带尾部反斜杠的驱动器和子目录路径[如“C:\mySubDirectory”])。 
            //string str3 = Directory.GetCurrentDirectory(); //获取应用程序的当前工作目录。 
            //string str4 = AppDomain.CurrentDomain.BaseDirectory;//获取基目录，它由程序集冲突解决程序用来探测程序集。 
            //string str7 = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;//获取或设置包含该应用程序的目录的名称。
            string temp = IniReadValue("D:\\C#\\SCMT\\test.ini", "ZipFileInfo", "filePath");

            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine("当前位置:{0}", currentPath);

            Console.WriteLine(temp);

        }
    }

}

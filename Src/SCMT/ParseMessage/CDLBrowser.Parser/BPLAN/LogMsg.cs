using LogManager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLBrowser.Parser.BPLAN
{
    public enum LogOfType
    {
        UE_MSGLOG = 0,
        eNB_MSGLOG = 1,
        gNB_MSGLOG = 2
    }
    class LogMsg
    {
        private string logUEFile;
        private string logeNBFile;
        private string loggNBFile;
        private StreamWriter writer;
        private FileStream fileStream = null;
        public LogMsg()
        {
            initFileName();
        }

        public void initFileName()
        {
            logUEFile = "";
            logeNBFile = "";
            loggNBFile = "";
            return;
        }

        public string GetFileName(LogOfType type)
        {
            string path = @".\LogMsg";
            string fileName = "";
            switch (type)
            {
                case LogOfType.UE_MSGLOG:
                    //当前没有此文件则按照时间生成一个新的文件
                    if ("" == logUEFile)
                    {
                        fileName = path + "\\" + "UE_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".log";
                        logUEFile = fileName;
                    }
                    else
                    {
                        fileName = logUEFile;
                    }
                    break;
                case LogOfType.eNB_MSGLOG:
                    if ("" == logeNBFile)
                    {
                        fileName = path + "\\" + "eNB_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".log";
                        logeNBFile = fileName;
                    }
                    else
                    {
                        fileName = logeNBFile;
                    }
                    break;
                case LogOfType.gNB_MSGLOG:
                    if ("" == loggNBFile)
                    {
                        fileName = path + "\\" + "gNB_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".log";
                        loggNBFile = fileName;
                    }
                    else
                    {
                        fileName = loggNBFile;
                    }
                    break;
                default:
                    Log.Error("invalid Log msg Type:" + type.ToString());
                    return fileName;
            }
            //创建文件夹
            if("" != fileName)
            {
                CreateDirectory(fileName);
            }
            return fileName;
        }
        public void WriteLog(LogOfType type, string info)
        {
            string fileName = GetFileName(type);
            if ("" == fileName)
            {
                return;
            }
            try
            {
                if (!File.Exists(fileName))
                {
                    fileStream = System.IO.File.Create(fileName);
                    writer = new StreamWriter(fileStream);
                }
                else
                {
                    writer = File.AppendText(fileName);
                }
                //把消息写入文件
                ScriptMessage message = JsonConvert.DeserializeObject<ScriptMessage>(info);
                writer.WriteLine(DateTime.Now + ":" );
                writer.WriteLine("    " + "No:" + message.NO.ToString() + "    Time:" + message.time.ToString());
                writer.WriteLine("    " + "UEID:" + message.ENBUEID.ToString() + "    Message:" + message.message.ToString());
                writer.WriteLine("    " + "Source:" + message.MessageSource.ToString() + "    Destination:" + message.MessageDestination.ToString());
                writer.WriteLine("    " + "data:" + message.data.ToString());
            }
            catch(Exception e)
            {
                Log.Error("open file " + fileName + " fail"+ e.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }

            long size = 0;
            //获取文件大小
            //文件大小(byte)
            using (FileStream file = File.OpenRead(fileName))
            {
                size = file.Length;
            }

            //判断日志文件大于2M，创建新的文件
            if (size > (1024 * 1024 * 2))
            {
                if(LogOfType.UE_MSGLOG == type)
                {
                    logUEFile = "";
                }
                else if (LogOfType.gNB_MSGLOG == type)
                {
                    loggNBFile = "";
                }
                else if (LogOfType.eNB_MSGLOG == type)
                {
                    logeNBFile = "";
                }
            }
        }

        public void CreateDirectory(string infoPath)
        {
            DirectoryInfo directoryInfo = Directory.GetParent(infoPath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }
        

    }
}

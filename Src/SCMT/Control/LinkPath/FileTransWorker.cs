using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LmtbSnmp;

namespace LinkPath
{
    /// <summary>
    /// 提供文件传输功能
    /// </summary>
    public class FileTransWorker
    {
        public static string FileTransferNextAvailableIDOid = "1.3.6.1.4.1.5105.100.1.2.2.1.2.0";

        public static string FileTransferRowStatusOid = "1.3.6.1.4.1.5105.100.1.2.2.2.1.2.";

        public static string FileTransferTypeOid = "1.3.6.1.4.1.5105.100.1.2.2.2.1.3.";

        public static string FileTransferDirectionOid = "1.3.6.1.4.1.5105.100.1.2.2.2.1.4.";

        public static string FileTransferUploadPathOid = "1.3.6.1.4.1.5105.100.1.2.2.2.1.6.";

        /// <summary>
        /// 上传lm.dzt文件
        /// </summary>
        /// <param name="netElementAddress">ip地址</param>
        /// <returns></returns>
        public static bool UploadMibFile(string netElementAddress)
        {
            var lmPath = FilePathHelper.GetIPV6OrIPV4LMDtzFilePath(netElementAddress);
            FilePathHelper.CreateFolder(lmPath);
            
            if(!UploadFile(netElementAddress, Transfiletype5216.TRANSFILE_lmtMDBFile, lmPath))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 上传数据一致性文件
        /// </summary>
        /// <param name="netElementAddress"></param>
        /// <returns></returns>
        public static bool UploadDataConsistencyFile(string netElementAddress)
        {
            var dstPath = FilePathHelper.GetConsistencyFilePath();
            FilePathHelper.CreateFolder(dstPath);

            if (!UploadFile(netElementAddress, Transfiletype5216.TRANSFILE_dataConsistency, dstPath))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="netElementAddress"></param>
        /// <param name="uploadFileType"></param>
        /// <param name="uploadPath"></param>
        /// <returns></returns>
        private static bool UploadFile(string netElementAddress, Transfiletype5216 uploadFileType, string uploadPath)
        {
            string fileTransferIdleTaskNo = GetIdleTransferTaskNo(netElementAddress);

            if(string.IsNullOrEmpty(fileTransferIdleTaskNo))
            {
                return false;
            }
            
            return SendUploadFileCommand(netElementAddress, fileTransferIdleTaskNo, uploadFileType, uploadPath); ;
        }

        /// <summary>
        /// 得到空闲的文件传输任务号
        /// </summary>
        /// <param name="netElementAddress"></param>
        /// <returns></returns>
        private static string GetIdleTransferTaskNo(string netElementAddress)
        {
            List<CDTLmtbVb> queryVbs = new List<CDTLmtbVb>();
            Dictionary<string, string> queryResult;

            CDTLmtbVb fileTransferIdleTaskIdVb = new CDTLmtbVb { Oid = FileTransferNextAvailableIDOid };

            queryVbs.Add(fileTransferIdleTaskIdVb);

            long timeElapsed = 200; //等待时间

            if (!SnmpSessionWorker.SnmpGetSync(netElementAddress, queryVbs, out queryResult, timeElapsed))
                return "";

            return SnmpSessionWorker.GetQueryValueByOid(FileTransferNextAvailableIDOid, queryResult);
        }

        /// <summary>
        /// 下发文件上传命令
        /// </summary>
        /// <param name="netElementAddress"></param>
        /// <param name="transferTaskId"></param>
        /// <param name="uploadFileType"></param>
        /// <param name="uploadPath"></param>
        /// <returns></returns>
        private static bool SendUploadFileCommand(string netElementAddress, string transferTaskId, Transfiletype5216 uploadFileType, string uploadPath)
        {
            List<CDTLmtbVb> setVbs = new List<CDTLmtbVb>();
            //文件传输任务行有效
            string fileTransferTaskRowStatusOid = FileTransferRowStatusOid + transferTaskId;
            CDTLmtbVb fileTransferTaskRowStatusVb = new CDTLmtbVb();
            SnmpSessionWorker.PacketVb(ref fileTransferTaskRowStatusVb, fileTransferTaskRowStatusOid, "4", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32);
            setVbs.Add(fileTransferTaskRowStatusVb);

            //文件传输类型
            string uploadFileTypeOid = FileTransferTypeOid + transferTaskId;
            CDTLmtbVb uploadFileTypeVb = new CDTLmtbVb();

            string fileType = ((int)uploadFileType).ToString();
            SnmpSessionWorker.PacketVb(ref uploadFileTypeVb, uploadFileTypeOid, fileType, SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32);
            setVbs.Add(uploadFileTypeVb);

            //文件传输方向：上传
            string fileTransferTaskDirectionOid = FileTransferDirectionOid + transferTaskId;
            CDTLmtbVb fileTransferDirectionVb = new CDTLmtbVb();
            SnmpSessionWorker.PacketVb(ref fileTransferDirectionVb, fileTransferTaskDirectionOid, "1", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32);
            setVbs.Add(fileTransferDirectionVb);

            //文件上传路径
            string fileTransferTaskUploadPathOid = FileTransferUploadPathOid + transferTaskId;
            CDTLmtbVb fileTransferTaskUploadPathVb = new CDTLmtbVb();
            SnmpSessionWorker.PacketVb(ref fileTransferTaskUploadPathVb, fileTransferTaskUploadPathOid, uploadPath, SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS);
            setVbs.Add(fileTransferTaskUploadPathVb);

            return SnmpSessionWorker.SnmpSetSync(netElementAddress, setVbs, 300);
        }
    }
}

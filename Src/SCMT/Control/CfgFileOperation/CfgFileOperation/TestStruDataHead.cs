using CfgFileOpStruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfgFileOperation
{
    /// <summary>
    /// 验证 文件的数据块头 struct StruDataHead
    /// </summary>
    class TestStruDataHead
    {
        public bool testBeyondCompare()
        {
            string dataBasePath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\";
            string YSFilePath = dataBasePath + "5GCfg\\init_qyx.cfg";
            string NewFilePath = dataBasePath + "init.cfg";
            CfgOp cfgOp = new CfgOp();
            int offset = 956;
            int readCount = 24;
            byte[] Ysdata = cfgOp.CfgReadFile(YSFilePath, offset, readCount);
            byte[] Newdata = cfgOp.CfgReadFile(NewFilePath, offset, readCount);

            StruDataHead ysD = new StruDataHead("");
            ysD.SetValueByBytes(Ysdata);

            StruDataHead newD = new StruDataHead("");
            newD.SetValueByBytes(Newdata);

            return beyondCom( ysD,  newD);

        }

        bool beyondCom(StruDataHead ysD, StruDataHead newD)
        {
            bool re = true;
            if (string.Compare(Convert.ToBase64String(ysD.u8VerifyStr), Convert.ToBase64String(newD.u8VerifyStr), false) != 0)
            {
                re = false;
                Console.WriteLine("u8VerifyStr is not same.");
            }

            if (ysD.u32DatType != newD.u32DatType)
            {
                re = false;
                Console.WriteLine("u32DatType is not same.");
            }

            if (ysD.u32DatVer != newD.u32DatVer)
            {
                re = false;
                Console.WriteLine("u32DatVer is not same.");
            }

            if (ysD.u32TableCnt != newD.u32TableCnt)
            {
                re = false;
                Console.WriteLine("u32TableCnt is not same.");
            }

            return re;
        }

    }
}

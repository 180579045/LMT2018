using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIBDataParser
{
    public delegate void ResultInitData(bool result);

    public interface IReDataByEnglishName
    {
        string oid { get; }
    }
    public interface IReDataByOid
    {
        string nameEn { get; }
        string isLeaf { get; }
        string indexNum { get; }
    }

    public interface IReDataByTableEnglishNameChild
    {
        string childNameMib { get; set; } // "alarmCauseNostring ,
        string childNo { get; set; } // 1,
        string childOid { get; set; } // "1.1.1.1.1.1",
        string childNameCh { get; set; } // "告警原因编号",
        string isMib { get; set; } // 1,
        string ASNType { get; set; } // "Integer32",
        string OMType { get; set; } // "s32",
        string UIType { get; set; } // 0,
        string managerValueRange { get; set; } // "0-2147483647",
        string defaultValue { get; set; } // "×",
        string detailDesc { get; set; } // "告警原因编号， 取值  :0..2147483647。",
        string leafProperty { get; set; } // 0,
        string unit { get; set; } // ""
    }
    public interface IReDataByTableEnglishName
    {
        string oid { get; }
        string indexNum { get; }
        List<IReDataByTableEnglishNameChild> childrenList { get; }
    }
    public interface IReCmdDataByCmdEnglishName {
        string cmdNameEn { get; set; } // 命令的英文名
        string tableName { get; set; } // 命令的mib表英文名
        string cmdType { get; set; } //命令类型
        string cmdDesc { get; set; } //命令描述
        List<string> leaflist { get; set; } // 命令节点名
    }
    public interface IDatabase
    {
        // 返回初始化 initDatabase 的结果
        //   true: 初始化成功； flase: 初始化失败、
        //   void ResultInitData(bool result);
        // ResultInitData resultInitData;

        // 线程 : 初始化(1.解压lm.dtz;2.解析.mdb;3.解析json;)
        void initDatabase();// IParseResultNotify parseResultListener);
        void initDatabase(string connectIp);

        //查询数据
        //bool getDataByEnglishName(string nameEn, out IReDataByEnglishName reData);
        //bool getDataByEnglishName(List<string> nameEnList, out List<IReDataByEnglishName> reDataList);
        //bool getDataByOid(string oid, out IReDataByOid reData);
        //bool getDataByTableEnglishName(string nameEn, out IReDataByTableEnglishName  reData);
        //bool getCmdDataByCmdEnglishName(string cmdEn, out IReCmdDataByCmdEnglishName reCmdData);

        //
        bool getDataByEnglishName(string nameEn, out IReDataByEnglishName reData, string connectIp);
        bool getDataByEnglishName(List<string> nameEnList, out List<IReDataByEnglishName> reDataList, string connectIp);
        bool getDataByOid(string oid, out IReDataByOid reData, string connectIp);
        bool getDataByTableEnglishName(string nameEn, out IReDataByTableEnglishName reData, string connectIp);
        //bool getCmdDataByCmdEnglishName(string cmdEn, out IReCmdDataByCmdEnglishName reCmdData, string connectIp);
    }
}


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
    public interface IReDataByTableEnglishName
    {
        string oid { get; }
        string indexNum { get; }
        List<Dictionary<string, object>> childrenList { get; }
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

        //查询数据
        bool getDataByEnglishName(string nameEn, out IReDataByEnglishName reData);
        bool getDataByOid(string oid, out IReDataByOid reData);
        bool getDataByTableEnglishName(string nameEn, out IReDataByTableEnglishName  reData);
        bool getCmdDataByCmdEnglishName(string cmdEn, out IReCmdDataByCmdEnglishName reCmdData);
    }
}


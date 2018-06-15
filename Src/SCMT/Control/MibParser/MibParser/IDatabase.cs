using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIBDataParser
{
    public interface IReDataByEnglishName
    {
        string oid { get; }
        string mibSyntax { get; }//MIB_Syntax取值类型
        string mangerValue { get; }//取值范围
        string mibDesc { get; }//描述信息 MIBDesc
        string parentOid { get; }//父oid，table=""
    }
    public interface IReDataByOid
    {
        string nameEn { get; }
        string isLeaf { get; }
        string indexNum { get; }
    }

    public interface IReDataByTableEnglishNameChild
    {
        string childNameMib { get;  } // "alarmCauseNostring ,
        string childNo { get;  } // 1,
        string childOid { get;  } // "1.1.1.1.1.1",
        string childNameCh { get;  } // "告警原因编号",
        string isMib { get;  } // 1,
        string ASNType { get;  } // "Integer32",
        string OMType { get;  } // "s32",
        string UIType { get;  } // 0,
        string managerValueRange { get;  } // "0-2147483647",
        string defaultValue { get;  } // "×",
        string detailDesc { get;  } // "告警原因编号， 取值  :0..2147483647。",
        string leafProperty { get;  } // 0,
        string unit { get; } // ""
    }
    public interface IReDataByTableEnglishName
    {
        string oid { get; }
        string indexNum { get; }
        List<IReDataByTableEnglishNameChild> childrenList { get; }
    }
    public interface IReCmdDataByCmdEnglishName {
        string m_cmdNameEn { get; } // 命令的英文名
        string m_tableName { get; } // 命令的mib表英文名
        string m_cmdType { get;} //命令类型
        string m_cmdDesc { get; } //命令描述
        List<string> m_leaflist { get; } // 命令节点名
    }

    /// <summary>
    /// 委托 : 初始化Database 结果
    /// </summary>
    /// <param name="result">初始化成功,true;失败,false</param>
    public delegate void ResultInitData(bool result);

    /// <summary>
    /// 操作数据库接口 : 初始化, 查询. 没有修改的接口.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// 获取单实例数据库句柄
        /// </summary>
        /// <returns>Database</returns>
        /// Database GetInstance();

        /// <summary>
        /// 初始化的线程 : 初始化(1.解压lm.dtz;2.解析.mdb;3.解析json;)
        /// </summary>
        /// <param name="connectIp">信息归属的标识</param>
        void initDatabase(string connectIp);

        /// <summary>
        /// 删除断链的ip的数据库
        /// </summary>
        /// <param name="connectIp">信息归属的标识</param>
        /// <returns></returns>
        bool delDatabase(string connectIp);

        /// <summary>
        /// 返回初始化 initDatabase 的结果。void ResultInitData(bool result);
        /// </summary>
        /// <param name="result">true: 初始化成功； flase: 初始化失败。</param>
        /// <returns>void</returns>
        /// ResultInitData resultInitData;

        ///查询数据
        /// <summary>
        /// [查询]通过节点英文名字，查询节点信息。支持多节点查找。
        /// </summary>
        /// <param name="reData">其中key为查询的英文名(需要输入)，value为对应的节点信息。</param>
        /// <param name="connectIp">信息归属的标识</param>
        /// <param name="err">查询失败的原因</param>
        /// <returns></returns>
        bool getDataByEnglishName(Dictionary<string, IReDataByEnglishName> reData, string connectIp, out string err);
        /// <summary>
        /// [查询]通过节点英文名字，查询节点信息。支持单节点查找。
        /// </summary>
        /// <param name="nameEn">节点英文名字</param>
        /// <param name="reData">节点信息</param>
        /// <param name="curConnectIp">信息归属的标识</param>
        /// <returns></returns>
        bool getDataByEnglishName(string nameEn, out IReDataByEnglishName reData, string curConnectIp, out string err);

        /// <summary>
        /// [查询]通过节点oid，查询节点信息。支持多节点查找。
        /// </summary>
        /// <param name="reData">其中key为查询的oid(需要输入)，value为对应的节点信息。</param>
        /// <param name="connectIp">信息归属的标识</param>
        /// <param name="err">查询失败的原因</param>
        /// <returns></returns>
        //bool getDataByOid(string oid, string connectIp, out IReDataByOid reData, out string err);
        bool getDataByOid(Dictionary<string, IReDataByOid>reData, string connectIp, out string err);

        /// <summary>
        /// [查询]通过表的英文名，查询表的信息。支持多表查找。
        /// </summary>
        /// <param name="reData">其中key为查询的表名(需要输入)，value为对应的表的信息。</param>
        /// <param name="connectIp">信息归属的标识</param>
        /// <param name="err">查询失败的原因</param>
        /// <returns></returns>
        bool getDataByTableEnglishName(Dictionary<string, IReDataByTableEnglishName> reData, string connectIp, out string err);

        /// <summary>
        /// [查询]通过命令的英文名，查询命令的信息。支持多个命令查找。
        /// </summary>
        /// <param name="reData">其中key为查询的命令英文名(需要输入)，value为对应的命令的信息。</param>
        /// <param name="connectIp">信息归属的标识</param>
        /// <param name="err">查询失败的原因</param>
        /// <returns></returns>
        bool getCmdDataByCmdEnglishName(Dictionary<string, IReCmdDataByCmdEnglishName> reData, string connectIp, out string err);

        //bool testDictExample(Dictionary<string, IReDataByEnglishName> reData);
    }
}


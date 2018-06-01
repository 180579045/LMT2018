using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIBDataParser
{
    /// <summary>
    /// 委托 : 初始化Database 结果
    /// </summary>
    /// <param name="result">初始化成功,true;失败,false</param>
    public delegate void ResultInitData(bool result);

    public interface IReDataByEnglishName
    {
        string oid { get; }
        //MIB_Syntax
        //取值范围
        //描述信息 MIBDesc
        //父oid，table=null
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
        string leafProperty { get; set; } // 0, TODO 这是什么鬼？
        string unit { get; set; } // ""

        bool idIndex { get; set; }		// TODO 为什么没有解析出这个属性？
    }
    public interface IReDataByTableEnglishName
    {

        string oid { get; }
        string indexNum { get; }
        List<IReDataByTableEnglishNameChild> childrenList { get; }
    }

    public interface IReCmdDataByCmdEnglishName
    {
        string cmdNameEn { get; set; }		// 命令的英文名
        string tableName { get; set; }		// 命令的mib表英文名
        string cmdType { get; set; }		//命令类型
        string cmdDesc { get; set; }		//命令描述
        List<string> leaflist { get; set; } // 命令节点名
    }

    /// <summary>
    /// 操作数据库接口 : 初始化, 查询. 没有修改的接口.
    /// </summary>
    public interface IDatabase
    {
        // 返回初始化 initDatabase 的结果
        //   true: 初始化成功； flase: 初始化失败、
        //   void ResultInitData(bool result);
        // ResultInitData resultInitData;

        // 线程 : 初始化(1.解压lm.dtz;2.解析.mdb;3.解析json;)
        void initDatabase(string connectIp);

        //查询数据
        /// <summary>
        /// [查询]通过节点英文名字，查询节点信息。支持多节点查找。
        /// </summary>
        /// <param name="reData">其中key为查询的英文名(需要输入)，value为对应的节点信息。</param>
        /// <param name="connectIp">信息归属的标识</param>
        /// <param name="err">查询失败的原因</param>
        /// <returns></returns>
        bool getDataByEnglishName(Dictionary<string, IReDataByEnglishName> reData, string connectIp, out string err);

        /// <summary>
        /// [查询]通过节点oid，查询节点信息。支持多节点查找。
        /// </summary>
        /// <param name="reData">其中key为查询的oid(需要输入)，value为对应的节点信息。</param>
        /// <param name="connectIp">信息归属的标识</param>
        /// <param name="err">查询失败的原因</param>
        /// <returns></returns>
        //bool getDataByOid(string oid, string connectIp, out IReDataByOid reData, out string err);
        bool getDataByOid(Dictionary<string, IReDataByOid>reData, string connectIp, out string err);


        //bool getDataByTableEnglishName(string nameEn, string connectIp, out IReDataByTableEnglishName reData, out string err);
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


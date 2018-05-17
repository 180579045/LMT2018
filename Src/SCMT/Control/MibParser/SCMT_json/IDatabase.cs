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
    public interface IDatabase
    {
        //ResultInitData resultInitData;

        //初始化(1.解压lm.dtz;2.解析.mdb;3.解析json;)
        bool initDatabase();// IParseResultNotify parseResultListener);

        //查询数据
        bool getDataByEnglishName(string nameEn, out IReDataByEnglishName reData);
        bool getDataByOid(string oid, out IReDataByOid reData);
        bool getDataByTableEnglishName(string nameEn, out IReDataByTableEnglishName reData);
    }
}


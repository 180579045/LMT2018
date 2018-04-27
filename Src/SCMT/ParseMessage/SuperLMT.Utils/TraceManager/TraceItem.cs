using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnmpSharpNet;
using System.Net;

namespace SuperLMT.Utils.TraceManager
{
    public class TraceItem
    {
        #region 构造函数
        public TraceItem()
        {
 
        }
        public TraceItem(string oid, string displayName, string range, string value,string type)
        {
            Oid = oid;
            DisplayName = displayName;
            Range = range;
            Value = value;
            Type = type;
        }
        #endregion

        #region 公共函数
        /// <summary>
        /// 获取下发是snmp命令时所下发的值
        /// </summary>
        public AsnType GetValue()
        {
            switch (Type)
            {
                case "s32[]":
                    return new Oid(Value);
                    break;
                case "u8[]":
                    //return new Sequence(Value));
                    IpAddress ipAddress = new IpAddress(IPAddress.Parse(Value));
                    Byte[] curBytes = new Byte[4];
                    curBytes[0] = ipAddress[0];
                    curBytes[1] = ipAddress[1];
                    curBytes[2] = ipAddress[2];
                    curBytes[3] = ipAddress[3];

                    OctetString octIpAddr = new OctetString(curBytes);
                    return octIpAddr;
                    //new 
                    break;
                case "enum":
                    if ("" == Value)
                    {
                        Value = "0";
                    }
                    return new Integer32(int.Parse(Value));
                    break;
                case "u8":
                    if ("" == Value)
                    {
                        Value = "0";
                    }
                    return new Integer32(int.Parse(Value));
                    break;
                case "s32":
                    if ("" == Value)
                    {
                        Value = "0";
                    }
                    return new Integer32(int.Parse(Value));
                    break;
                case "BITS":
                    if ("" == Value)
                    {
                        Value = "0";
                    }
                    return new Gauge32(Value);
                    break;
                case "s8[]":
                    if ("" == Value)
                    {
                        Value = "";
                    }
                    return new OctetString(Value);
                    break;
                default:
                    return null;
                    break;
            }
        }


        #endregion



        #region 属性
        private string _oid; //OID
        public string Oid
        {
            get { return _oid; }
            set { _oid = value; }
        }

        private string _displayName;  //中文名
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
            }
        }

        private string _range;   //取值范围
        public string Range
        {
            get { return _range; }
            set { _range = value; }
        }



        private string _value;   //取值
        public string Value
        {
            get 
            {return _value; }
            set
            {
                _value = value;
            }
        }
        private string _type;   //数据类型
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
            	_type = value;
            }
        }


        #endregion

    }
}

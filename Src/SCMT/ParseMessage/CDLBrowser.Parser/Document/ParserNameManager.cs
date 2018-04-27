// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserNameManager.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the LogFilesManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document
{
    using System;
    using System.Collections.Generic;
    using CDLBrowser.Parser.DatabaseMgr;

    public class ParseringOpts
    {
        //public DbConn dbConn;
        public string ParserName;
        public bool IsRelocation;

    }
    /// <summary>
    /// The log files manager.
    /// </summary>
    public class ParserNameManager
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly ParserNameManager Instance = new ParserNameManager();

        /// <summary>
        /// The log file name to id map.
        /// </summary>
        private readonly IDictionary<int, string> parserIdToNameMap = new Dictionary<int, string>();

        private  IDictionary<int, DbConn> parsingIdToDBconnMap = new Dictionary<int, DbConn>(); 
      
        private IDictionary<int , ParseringOpts> parserId2Options = new Dictionary<int, ParseringOpts>(); 
        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static ParserNameManager Singleton
        {
            get
            {
                return Instance;
            }
        }

        /// <summary>
        /// create new parser name.
        /// </summary>
        /// <param name="parserId">parser id.</param>
        /// <returns>parser name.if is repeated, add time at the end.</returns>
        public string GetNewParserName(int parserId)
        {
            string name = string.Format("日志列表{0}", parserId);
            if (this.IsRepeated(name))
            {
                return string.Format("{0}_{1}", name, DateTime.Now.ToString("yyyyMMddHHmmss"));
            }

            return name;
        }

        /// <summary>
        /// is name repeated.
        /// </summary>
        /// <param name="name">parser name.</param>
        /// <returns>repeated or not.</returns>
        public bool IsRepeated(string name)
        {
            foreach (var item in this.parserIdToNameMap)
            {
                if (name == item.Value)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddParsingOptinsById(int parsingid, ParseringOpts opts)
        {
            if (!this.parserId2Options.ContainsKey(parsingid))
            {
                if (opts == null)
                {
                    return;
                }
                this.parserId2Options.Add(parsingid, opts);

            }
            else
            {
                MyLog.Log("Error:AddParsingOptinsById, parsingid:" + parsingid.ToString() + " already exist in Map.");
            }
        }
        public void UpdateParsingOptinsById(int parsingid, ParseringOpts optsnew)
        {
            if (this.parserId2Options.ContainsKey(parsingid))
            {
                if (optsnew == null)
                {
                    return;
                }
                this.parserId2Options[parsingid] = optsnew;

            }
            else
            {
                MyLog.Log("Error:AddParsingOptinsById, parsingid:" + parsingid.ToString() + " not exist in Map.");
            }
        }
        public ParseringOpts GetParsingOptinsById(int parsingid)
        {
            ParseringOpts opts = null;
            if (this.parserId2Options.ContainsKey(parsingid))
            {
                this.parserId2Options.TryGetValue(parsingid, out opts);
            }
            return opts;
        }
        public bool IsRelocationOn(int parsingid)
        {
            ParseringOpts opt = GetParsingOptinsById(parsingid);
            if (opt != null)
            {

                return opt.IsRelocation;
            }

            return false;
        }
        public void RmvParsingOptinsById(int parsingid)
        {
            if (this.parserId2Options.ContainsKey(parsingid))
            {
                this.parserId2Options.Remove(parsingid);
            }
            else
            {
                MyLog.Log("Error: RemoveParsingIdDBconn, parsingid does not exist in Map.");
            }
        }

        public void AddParsingIdDBconn(int parsingid, DbConn dbconn)
        {
            if (!this.parsingIdToDBconnMap.ContainsKey(parsingid))
            {
                this.parsingIdToDBconnMap.Add(parsingid,dbconn);
               
            }
            else
            {
                MyLog.Log("Error:AddParsingIdDBconn, parsingid:" + parsingid.ToString() + " already exist in Map.");
            }
        }

        public DbConn GetDbconnByParsingId(int parsingid)
        {
            DbConn dbconn = null;
            if (this.parsingIdToDBconnMap.ContainsKey(parsingid))
            {
                this.parsingIdToDBconnMap.TryGetValue(parsingid, out dbconn);
            }
            return dbconn;
        }
        public void RemoveParsingIdDBconn(int parsingid)
        {
            if (this.parsingIdToDBconnMap.ContainsKey(parsingid))
            {
                parsingIdToDBconnMap[parsingid].Close();
                parsingIdToDBconnMap[parsingid] = null;
                this.parsingIdToDBconnMap.Remove(parsingid);
            }
            else
            {
                MyLog.Log("Error: RemoveParsingIdDBconn, parsingid does not exist in Map.");
            }
        }

        
        /// <summary>
        /// The add parser.
        /// </summary>
        /// <param name="parserId">
        /// The parser id.
        /// </param>
        /// <param name="parserName">
        /// The parser name.
        /// </param>
        public void AddParserId(int parserId, string parserName)
        {
            if (!this.parserIdToNameMap.ContainsKey(parserId))
            {
                this.parserIdToNameMap.Add(parserId, parserName);    
            }
        }

        /// <summary>
        /// update parser name.
        /// </summary>
        /// <param name="parserId">parser id.</param>
        /// <param name="newName">new name.</param>
        public void UpdateParserName(int parserId, string newName)
        {
            // foreach (var item in this.parserIdToNameMap)
            // {
            //    if (item.Key == parserId)
            //    {
            //        this.parserIdToNameMap.Remove(item.Key);
            //        break;
            //    }
            // }
            // this.parserIdToNameMap.Add(parserId, newName);
            string oldName;
            if (this.parserIdToNameMap.TryGetValue(parserId, out oldName))
            {
                this.parserIdToNameMap[parserId] = newName;
            }
            else
            {
                this.parserIdToNameMap.Add(parserId, newName);
            }
        }

        /// <summary>
        /// update name.
        /// </summary>
        /// <param name="oldName">old name.</param>
        /// <param name="newName">new name.</param>
        public void UpdateParserName(string oldName,string newName)
        {
            int parserId = this.GetParserIdByName(oldName);
            this.UpdateParserName(parserId, newName);
        }

        /// <summary>
        /// The add parser.
        /// </summary>
        /// <param name="parserId">
        /// The parser id.
        /// </param>
        public void RemoveParserById(int parserId)
        {
            if (this.parserIdToNameMap.ContainsKey(parserId))
            {
                this.parserIdToNameMap.Remove(parserId);
            }

            RemoveParsingIdDBconn(parserId);
            RmvParsingOptinsById(parserId);
           
        }

        /// <summary>
        /// The add parser.
        /// </summary>
        /// <param name="parserName">
        /// The parser name.
        /// </param>
        public void RemoveParserByName(string parserName)
        {
            int parserId = this.GetParserIdByName(parserName);
            this.RemoveParserById(parserId);
           
        }

        /// <summary>
        /// get parser name by id.
        /// </summary>
        /// <param name="parserId">parser id.</param>
        /// <returns>parser name.</returns>
        public string GetParserNameByID(int parserId)
        {
            string parserName;
            this.parserIdToNameMap.TryGetValue(parserId, out parserName);
            return parserName;
        }

        /// <summary>
        /// get parser id by name.
        /// </summary>
        /// <param name="parserName">parser name.</param>
        /// <returns>parser id.</returns>
        public int GetParserIdByName(string parserName)
        {
            foreach (var item in this.parserIdToNameMap)
            {
                if (parserName == item.Value)
                {
                    return item.Key;
                }
            }

            return -1;
        }
    }
}

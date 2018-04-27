// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogFilesManager.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the LogFilesManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// The log files manager.
    /// </summary>
    public class LogFilesManager
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly LogFilesManager Instance = new LogFilesManager();

        /// <summary>
        /// The log file ids.
        /// </summary>
        private readonly IDictionary<int, string> logFileIdToNameMap = new Dictionary<int, string>();

        /// <summary>
        /// The parsing number to log file name.
        /// </summary>
        private readonly IDictionary<int, List<string>> parsingNumTologFileName = new Dictionary<int, List<string>>();

        /// <summary>
        /// The log file name to id map.
        /// </summary>
        private readonly IDictionary<string, int> logFileNameToIdMap = new Dictionary<string, int>();

        /// <summary>
        /// The log file id to version map.
        /// </summary>
        private readonly IDictionary<string, string> logFileNameToVersionMap = new Dictionary<string, string>();

        /// <summary>
        /// The log file id to display index map.
        /// </summary>
        private readonly IDictionary<int, int> logFileIdToDisplayIndexMap = new Dictionary<int, int>();

        /// <summary>
        /// The log file id.
        /// </summary>
        private int logFileId;

        /// <summary>
        /// The parsing id.
        /// </summary>
        private int parsingId;

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static LogFilesManager Singleton
        {
            get
            {
                return Instance;
            }
        }

        /// <summary>
        /// Gets the parsing id.
        /// </summary>
        public int ParsingId
        {
            get
            {
                return this.parsingId;
            }
        }

        /// <summary>
        /// The create parsing id.
        /// </summary>
        /// <param name="parsingName">
        /// The parsing name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CreateParsingId(out string parsingName)
        {
            ++this.parsingId;
            parsingName = ParserNameManager.Singleton.GetNewParserName(this.parsingId);
            ParserNameManager.Singleton.AddParserId(this.parsingId, parsingName);
            return this.parsingId;
        }

        /// <summary>
        ///  add file information.
        /// </summary>
        /// <param name="files">
        /// the files.
        /// </param>
        public void AddFilesInfo(IList<FileInfo> files)
        {
            var logFileName = new List<string>();
            
            foreach (var file in files)
            {
                ++this.logFileId;
                string name = ParserNameManager.Singleton.GetParserNameByID(this.parsingId) + "." + file.FullName;
                this.logFileNameToIdMap.Add(name, this.logFileId);
                this.logFileIdToNameMap.Add(this.logFileId, name);
                logFileName.Add(file.Name);
            }

            this.parsingNumTologFileName.Add(this.parsingId, logFileName);
        }

        /// <summary>
        /// The get file id.
        /// </summary>
        /// <param name="paringFileName">
        /// The paring file name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetFileId(string paringFileName)
        {
            int fileId;
            return this.logFileNameToIdMap.TryGetValue(paringFileName, out fileId) ? fileId : 0;
        }

        /// <summary>
        /// The set file version.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="fileVersion">
        /// The file version.
        /// </param>
        public void AddFileVersion(string fileName, string fileVersion)
        {
            this.logFileNameToVersionMap.Add(fileName, fileVersion);
        }

        /// <summary>
        /// The get file name.
        /// </summary>
        /// <param name="parsingNumber">
        /// The parsing Number.
        /// </param>
        /// <param name="enbId">
        /// The E node B id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFileName(int parsingNumber, int enbId)
        {
            List<string> parsingFileName;
            this.parsingNumTologFileName.TryGetValue(parsingNumber, out parsingFileName);
            string returnValue = null;
            if (parsingFileName != null)
            {
                foreach (string fileName in parsingFileName)
                {
                    string[] arrayName = fileName.Split('_');
                    string enbName = arrayName[1];
                    if (enbId.ToString(CultureInfo.InvariantCulture) == enbName)
                    {
                        returnValue = returnValue + "\n" + fileName + "\n";
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// The get file name.
        /// </summary>
        /// <param name="parsingNumber">
        /// The parsing Number.
        /// </param>
        /// <param name="enbId">
        /// The E node B id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetChartFileName(int parsingNumber, int enbId)
        {
            List<string> parsingFileName;
            this.parsingNumTologFileName.TryGetValue(parsingNumber, out parsingFileName);
            string returnValue = " ";
            if (parsingFileName != null)
            {
                foreach (string fileName in parsingFileName)
                {
                    string[] arrayName = fileName.Split('_');
                    string enbName = arrayName[1];
                    if (enbId.ToString(CultureInfo.InvariantCulture) == enbName)
                    {
                        returnValue = returnValue  + fileName + "; ";
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// The get file version.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFileVersion(string fileName)
        {
            string fileVersion;
            this.logFileNameToVersionMap.TryGetValue(fileName, out fileVersion);
            return fileVersion;
        }

        /// <summary>
        /// The get file name by id.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFileNameById(int index)
        {
            string fileName;
            this.logFileIdToNameMap.TryGetValue(index, out fileName);
            return fileName;
        }

        /// <summary>
        /// The get file name by id.
        /// </summary>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public int GetFileIdByName(string fileName)
        {
            foreach (var item in this.logFileIdToNameMap)
            {
                if (item.Value.Contains(fileName))
                {
                    return item.Key;
                }
            }

            return -1;
        }

        /// <summary>
        /// The get all file name.
        /// </summary>
        /// <returns>
        /// The <see>
        ///       <cref>IEnumerable</cref>
        ///     </see> .
        /// </returns>
        public IEnumerable<string> GetAllFileName()
        {
            foreach (var item in this.logFileNameToIdMap)
            {
                yield return item.Key;
            }
        }

        /// <summary>
        /// Add File First DisplayIndex.
        /// </summary>
        /// <param name="fileId">the file identity.</param>
        /// <param name="displayIndex">the display index.</param>
        public void AddFileFirstDisplayIndex(int fileId,int displayIndex)
        {
            this.logFileIdToDisplayIndexMap.Add(fileId, displayIndex);
        }

        /// <summary>
        /// Get First Display Index By File Identity.
        /// </summary>
        /// <param name="fileId">the file identity.</param>
        /// <returns>the file identity.</returns>
        public int GetFirstDisplayIndexByFileId(int fileId)
        {
            int displayIndex = 1;
            this.logFileIdToDisplayIndexMap.TryGetValue(fileId, out displayIndex);
            return displayIndex;
        }
    }
}

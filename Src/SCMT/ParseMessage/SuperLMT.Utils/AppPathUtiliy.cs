// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppPathUtiliy.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   The app path utiliy.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils
{
    /// <summary>
    /// The app path utiliy.
    /// </summary>
    public class AppPathUtiliy
    {
        /// <summary>
        /// The g_instance.
        /// </summary>
        private static readonly AppPathUtiliy Instance = new AppPathUtiliy();

        #region

        /// <summary>
        /// The str app path.
        /// 存放App路径的文本串
        /// </summary>
        private string strAppPath;

        /// <summary>
        /// The str dal cfg folder path.
        /// 数据访问层配置文件的存放目录
        /// </summary>
        private string strDalCfgFolderPath;

        /// <summary>
        /// The str data consistentcy folder path.
        /// 存放数据一致性文件的目录
        /// </summary>
        private string strDataConsistentcyFolderPath;

        /// <summary>
        /// The str temp folder path.
        /// 存放临时文件的目录
        /// </summary>
        private string strTempFolderPath;

        #endregion

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static AppPathUtiliy Singleton
        {
            get
            {
                return Instance;
            }
        }

        #region 公共函数

        /// <summary>
        /// The get app path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetAppPath()
        {
            if (this.strAppPath == null)
            {
                this.strAppPath = System.AppDomain.CurrentDomain.BaseDirectory; 
            }
            
            return this.strAppPath;
        }

        /// <summary>
        /// The get dal cfg folder path.
        /// 得到数据访问层配置文件的路径
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDALCfgFolderPath()
        {
            return this.PathConstruct(ref this.strDalCfgFolderPath, @"\config\DALCfg\");
        }

        /// <summary>
        /// The get data consistency folder path.
        /// 得到存放数据一致性文件的目录
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDataConsistencyFolderPath()
        {
            return this.PathConstruct(ref this.strDataConsistentcyFolderPath, @"\DATA_CONSISTENCY\");
        }

        /// <summary>
        /// The get temp folder path.
        /// 得到Temp的路径
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTempFolderPath()
        {
            return this.PathConstruct(ref this.strTempFolderPath, @"\Temp\");
        }
       
        /// <summary>
        /// The path construct.
        /// 构建路径
        /// </summary>
        /// <param name="strTarget">
        /// The str target.
        /// </param>
        /// <param name="strAppend">
        /// The str append.
        /// 需要附加的后缀
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string PathConstruct(ref string strTarget, string strAppend)
        {
            if (strTarget == null)
            {
                string appPath = this.GetAppPath();

                strTarget = appPath + strAppend;
            }

            return strTarget;
        }

        #endregion
    }
}

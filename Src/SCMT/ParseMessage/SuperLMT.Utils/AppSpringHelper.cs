// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSpringHelper.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   The app spring helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SuperLMT.Utils
{
    using System;

    using Common.Logging;

    using Spring.Context;
    using Spring.Context.Support;

    /// <summary>
    /// The app spring helper.
    /// </summary>
    public class AppSpringHelper
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(AppSpringHelper));

        /// <summary>
        /// The g_instance.
        /// </summary>
        private static readonly AppSpringHelper Instance = new AppSpringHelper();

        /// <summary>
        /// The _ app ctx.
        /// Spring的应用程序上下文
        /// </summary>
        private IApplicationContext appCtx;

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static AppSpringHelper Singleton
        {
            get
            {
                return Instance;
            }
        }

        #region 公共函数

        /// <summary>
        /// The get application context.
        /// 得到应用程序上下文
        /// </summary>
        /// <returns>
        /// The <see cref="IApplicationContext"/>.
        /// </returns>
        public IApplicationContext GetApplicationContext()
        {
            if (this.appCtx == null)
            {
                try
                {
                    this.appCtx = ContextRegistry.GetContext();
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }

                // Check.Ensure(null != _AppCtx, @"Spring的应用程序环境初始化失败!");
            }

            return this.appCtx;
        }

        /// <summary>
        /// The get object.
        /// 根据ID得到对象
        /// </summary>
        /// <param name="strObjID">
        /// The str obj id.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetObject(string strObjID)
        {
           // Check.Require("" != strObjID, @"对象名不能为空!");
            IApplicationContext ctx = this.GetApplicationContext();
            object createObj = null;
            if (null != ctx)
            {
                try
                {
                     createObj = ctx.GetObject(strObjID);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }

           // Check.Ensure(null != createObj, @"创建对象失败");
            return createObj;
        }

        /// <summary>
        /// The get object.
        /// 根据ID和传参得到对象
        /// </summary>
        /// <param name="strObjID">
        /// The str obj id.
        /// </param>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetObject(string strObjID, object[] arguments)
        {
            // Check.Require("" != strObjID, @"对象名不能为空!");
            IApplicationContext ctx = this.GetApplicationContext();
            object createObj = null;
            if (null != ctx)
            {
                try
                {
                    createObj = ctx.GetObject(strObjID, arguments);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }

            // Check.Ensure(null != createObj, @"创建对象失败");
            return createObj;
        }
        #endregion
    }
}

//
// File: "WorkerConfig.cs"
//
// Revision History: 
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using EZSeleniumLib;
using log4net;

namespace EZAsesAutoType
{
    /// <summary>
    ///  Custom entity to store configuration settings that
    ///  shall be applied and used within instances 
    ///  of class "WorkerThread".
    /// </summary>
    internal class WorkerConfig
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(WorkerConfig));
                return m_Log;
            }
        }
        #endregion

        #region propertiez

        private AppConfig? m_AppConfig = null;
        private AppConfig AppConfig
        {
            get
            {
                if (m_AppConfig == null)
                    m_AppConfig = new AppConfig();
                return m_AppConfig;
            }
            set
            {
                m_AppConfig = value;
            }
        }
        public AppConfig GetAppConfig()
        {
            return this.AppConfig;
        }
        public AppConfig SetAppConfig(AppConfig appConfig)
        {
            AppConfig prev = this.GetAppConfig();
            this.AppConfig = appConfig;
            return prev;
        }

        private UserSettings? m_UserSettings = null;
        private UserSettings UserSettings
        {
            get
            {
                if (m_UserSettings == null)
                    m_UserSettings = new UserSettings();
                return m_UserSettings;
            }
            set
            {
                m_UserSettings = value;
            }
        }
        public UserSettings GetUserSettings()
        {
            return this.UserSettings;
        }
        public UserSettings SetUserSettings(UserSettings userSettings)
        {
            UserSettings prev = this.GetUserSettings();
            this.UserSettings = userSettings;
            return prev;
        }

        private BrowserOptions? m_BrowserOptions = null;
        private BrowserOptions BrowserOptions
        {
            get
            {
                if (m_BrowserOptions == null)
                    m_BrowserOptions = new BrowserOptions();
                return m_BrowserOptions;
            }
            set
            {
                m_BrowserOptions = value;
            }
        }
        public BrowserOptions GetBrowserOptions()
        {
            return this.BrowserOptions;
        }
        public BrowserOptions SetBrowserOptions(BrowserOptions browserOptions)
        {
            BrowserOptions prev = this.GetBrowserOptions();
            this.BrowserOptions = browserOptions;
            return prev;
        }

        #endregion

        /// <summary>
        /// Init instance.
        /// </summary>
        /// <returns></returns>
        private bool Initialze(UserSettings userSettings, AppConfig appConfig, BrowserOptions browserOptions)
        {
            try
            {
                Log.Debug(Const.LogStart);
                this.SetUserSettings(userSettings);
                this.SetAppConfig(appConfig);
                this.SetBrowserOptions(browserOptions);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        ///  Default constructor.
        /// </summary>
        public WorkerConfig()
        {
            try
            {
                Log.Debug(Const.LogStart);
                UserSettings userSettings = new UserSettings();
                AppConfig appConfig = new AppConfig();
                BrowserOptions browserOptions = new BrowserOptions();
                if (!Initialze(userSettings, appConfig, browserOptions))
                    throw new Exception(nameof(Initialze) + Const.LogFail);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        ///  Custom constructor.
        /// </summary>
        public WorkerConfig(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                AppConfig appConfig = new AppConfig();
                BrowserOptions browserOptions = new BrowserOptions();
                if (!Initialze(userSettings, appConfig, browserOptions))
                    throw new Exception(nameof(Initialze) + Const.LogFail);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

    } // class

} // namespace

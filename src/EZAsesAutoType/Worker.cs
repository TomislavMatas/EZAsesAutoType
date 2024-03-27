//
// File: "WorkerThread.cs"
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using EZSeleniumLib;
using log4net;
using OpenQA.Selenium;

namespace EZAsesAutoType
{
    /// <summary>
    ///  This class shall implement all functionality 
    ///  that originate from any dialog or form using 
    ///  dedicated worker methodz. Obay genral rule, that
    ///  Windows Apps shall always be designed with 
    ///  multi-threaded approach to prevent "freezing".
    /// </summary>
    internal class Worker
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(Worker));
                return m_Log;
            }
        }
        #endregion

        #region propertiez
        private WorkerConfig? m_WorkerConfig = null;
        private WorkerConfig WorkerConfig
        {
            get
            {
                if (this.m_WorkerConfig == null)
                    this.m_WorkerConfig = new WorkerConfig();
                return this.m_WorkerConfig;
            }
            set 
            {
                this.m_WorkerConfig = value;
            }
        }
        public WorkerConfig GetWorkerConfig()
        {
            return this.WorkerConfig;
        }
        public WorkerConfig SetWorkerConfig(WorkerConfig workerConfig)
        {
            WorkerConfig prev = this.GetWorkerConfig();
            this.WorkerConfig = workerConfig;
            return prev;
        }

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

        #endregion

        #region Instantiation

        /// <summary>
        /// Initialize instance.
        /// </summary>
        /// <param name="workerConfig"></param>
        /// <returns></returns>
        private bool Initialze(WorkerConfig workerConfig)
        {
            try
            {
                Log.Debug(Const.LogStart);
                this.SetWorkerConfig(workerConfig);
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
        public Worker()
        {
            try
            {
                Log.Debug(Const.LogStart);
                if(!Initialze(new WorkerConfig()))
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
        public Worker(WorkerConfig workerConfig)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if(!Initialze(workerConfig))
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

        #endregion

        #region "userSettings" - wrapperz

        private string GetBaseUrl()
        {
            return this.WorkerConfig.GetUserSettings().ASESBaseUrl;
        }

        #endregion "userSettings" - wrapperz

        #region "AppConfig" - wrapperz

        private int GetTimeoutNavigationLoginPage()
        {
            return this.AppConfig.GetTimeoutLoginPage();
        }

        private string GetLoginPageFooterXPath()
        {
            return this.AppConfig.GetLoginPageFooterXPath();
        }

        private string GetLoginPageUsernameXPath()
        {
            return this.AppConfig.GetLoginPageUsernameXPath();
        }

        #endregion "AppConfig" - wrapperz

        /// <summary>
        /// Return a specific descendant of class "BrowserBase" using "BrowserFactory".
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="browserOptions"></param>
        /// <returns></returns>
        private BrowserBase GetBrowserInstance(string webDriver, BrowserOptions browserOptions)
        {
            try
            {
                Log.Debug(Const.LogStart);
                return BrowserFactory.GetBrowserInstance(webDriver, browserOptions);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

        #region Navigation 

        /// <summary>
        /// Use Browser-Interop Helper to navigate to "Login Page".
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeout">Timeout in seconds.</param>
        /// <returns></returns>
        private bool ASESNavigateToLoginPage(BrowserBase browser, string baseUrl, int timeoutLoginPage)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (!browser.GoToUrl(baseUrl))
                    throw new Exception(String.Format("GoToUrl '{0}' failed", baseUrl));

                #region validate complete rendering 

                // consider "loaded" when footer and all required input elements can be found.
                // use a "long" timeout only for first element that should be found.
                // although not reliable, use the buttom~most element as first element to look for.
                string xPath = this.GetLoginPageUsernameXPath(); ;// this.GetLoginPageFooterXPath();
                IWebElement element = browser.FindElement(By.XPath(xPath), timeoutLoginPage);
                if (element == null)
                    throw new Exception(String.Format("Element '{0}' not found", xPath));

                // look for other rquired elements with an arbitary and shorter timeout
                xPath = this.GetLoginPageUsernameXPath();
                element = browser.FindElement(By.XPath(xPath), 1);
                if (element == null)
                    throw new Exception(String.Format("Element '{0}' not found", xPath));

                #endregion

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

        public bool DoDailyPunch(UserSettings userSettings)
        {
            BrowserBase? browser = null;
            try
            {
                Log.Debug(Const.LogStart);
                int timeoutLoginPage = this.GetTimeoutNavigationLoginPage();
                string webDriver = userSettings.WebDriver;
                BrowserOptions browserOptions = new BrowserOptions();
                browser = this.GetBrowserInstance(webDriver, browserOptions);
                if (browser == null)
                    throw new Exception(nameof(this.GetBrowserInstance) + Const.LogFail);

                string baseUrl = userSettings.ASESBaseUrl;
                if (!this.ASESNavigateToLoginPage(browser, baseUrl,timeoutLoginPage))
                    throw new Exception(nameof(this.ASESNavigateToLoginPage) + Const.LogFail);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                if(browser != null)
                {
                    browser.Cleanup();
                    browser = null;
                }
                Log.Debug(Const.LogDone);
            }
        }

        #endregion Navigation 


    } // class

} // namespace

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

        private int GetTimeoutFindElement()
        {
            return this.AppConfig.GetTimeoutFindElement();
        }

        private string GetApplicationIFrameXPath()
        {
            return this.AppConfig.GetApplicationIFrameXPath();
        }

        private string GetLoginPageUsernameXPath()
        {
            return this.AppConfig.GetLoginPageUsernameXPath();
        }

        private string GetLoginPagePasswordXPath()
        {
            return this.AppConfig.GetLoginPagePasswordXPath();
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
        private bool ASESNavigateToLoginPage(BrowserBase browser, string baseUrl, int timeoutInSeconds)
        {

            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                if (baseUrl == null)
                    throw new ArgumentNullException(nameof(baseUrl));

                Log.Debug(String.Format("baseUrl={0}", baseUrl));

                if (!browser.GoToUrl(baseUrl, timeoutInSeconds))
                    throw new Exception(nameof(browser.SwitchToIFrame) + Const.LogFail);

                #region validate succossfull "load" of LoginPage
                // The entire ASES Application runs in an iFrame.
                // In order to make this work with Selenium,
                // need to switch to that specific iFrame first,
                // before _any_ other FindElement will work as expected.
                int timeoutFindElement = this.GetTimeoutFindElement();
                string xPath = this.GetApplicationIFrameXPath();
                IWebElement element = browser.FindElementByXpath(xPath, timeoutFindElement);
                if (element == null)
                    throw new Exception(String.Format("'{0}'{1}", xPath, Const.LogNotFound));

                IWebDriver iFrame = browser.SwitchToIFrame(element);
                if (iFrame == null)
                    throw new Exception(nameof(browser.SwitchToIFrame) + Const.LogFail);

                // consider "loaded" when all required input elements can be found.
                xPath = this.GetLoginPageUsernameXPath(); 
                element = browser.FindElementByXpath(xPath, timeoutFindElement);
                if (element == null)
                    throw new Exception(String.Format("'{0}'{1}", xPath, Const.LogNotFound));

                xPath = this.GetLoginPagePasswordXPath();
                element = browser.FindElementByXpath(xPath, timeoutFindElement);
                if (element == null)
                    throw new Exception(String.Format("'{0}'{1}", xPath, Const.LogNotFound));

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
                if (!this.ASESNavigateToLoginPage(browser, baseUrl, timeoutLoginPage))
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

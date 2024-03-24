//
// File: "BrowserEdge.cs"
//
// Summary:
// Specific browser implementation using 
// Selenium's WebDriver Class "OpenQA.Selenium.Edge".
// 
// Notes:
// Requires a Selenium driver binary named 
// "MicrosoftWebDriver.exe" 
// within system's search path.
// See "README.md" for details.
//
// Revision History: 
// 2024/03/24:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

using log4net;

namespace EZSeleniumLib
{
    /// <summary>
    /// Specific browser implementation using 
    /// Selenium's WebDriver Class "OpenQA.Selenium.Edge".
    /// Requires a Selenium driver binary named 
    /// "MicrosoftWebDriver.exe" 
    /// within system's search path.
    /// See "README.md" for details.
    /// Using "internal" modifier because this class shall
    /// not be referenced from outside of the assembly.
    /// Instead, access shall be done sololy through 
    /// abstract class "BrowserBase".
    /// </summary>
    internal class BrowserEdge : BrowserBase
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(BrowserEdge));
                return m_Log;
            }
        }
        #endregion

        #region private memberz

        /// <summary>
        /// Startup Argument decoration prefix.
        /// Either "--", "-", "/" or "" depending on browser implementation. 
        /// </summary>
        private const string m_ArgPfx = ""; 

        #endregion

        #region constructorz

        public BrowserEdge()
             : base()
        {
            // nothing special in here
        }

        public BrowserEdge(bool enablePopups, bool enableNotifications) 
             : base(enablePopups, enableNotifications)
        { 
            // nothing special in here
        }

        #endregion 

        /// <summary>
        /// Instantiate and initiialze instance of Edge WebDriver.
        /// The instatiation method uses a minimalistic approach,
        /// applying only thare bare defaults.
        /// This mehtod will be called by "Initialize", 
        /// when "App.config" value for "ÊZSeleniumLib.Browser.WebDriverInitMode" is set to "simple".
        /// </summary>
        /// <returns></returns>
        public override bool InitializeSimple()
        {
            try
            {
                Log.Debug(Const.LogStart);

                if (!GetDriverOptions(out EdgeOptions? options))
                    throw new Exception(nameof(GetDriverOptions) + Const.LogFail);

                this.m_Driver = new EdgeDriver(options);
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
        /// Instantiate and initiialze an instance of Edge WebDriver.
        /// The instatiation method uses a more sophisticated approach,
        /// allowing more detailed tweaking of individual properties.
        /// This mehtod will be called by "Initialize", 
        /// when "App.config" value for "ÊZSeleniumLib.Browser.WebDriverInitMode" is set to "extended".
        /// </summary>
        /// <returns></returns>
        public override bool InitializeExtended()
        {
            try
            {
                Log.Debug(Const.LogStart);

                Log.Info("EdgeDriverService init ...");
                EdgeDriverService service = EdgeDriverService.CreateDefaultService();
#if DEBUG
                service.UseVerboseLogging = true;
                service.UseSpecCompliantProtocol = true;
                service.HideCommandPromptWindow = false;
                service.SuppressInitialDiagnosticInformation = false;
#else
                service.UseVerboseLogging = false;
                service.UseSpecCompliantProtocol = true;
                service.HideCommandPromptWindow = true;
                service.SuppressInitialDiagnosticInformation = true;
#endif
                Log.Info("EdgeDriverService init OK");

                Log.Info("GetDriverOptions ...");
                if (!GetDriverOptions(out EdgeOptions? options))
                    throw new Exception(nameof(GetDriverOptions) + Const.LogFail);

                Log.Info("GetDriverOptions OK");

                Log.Info("EdgeDriverService start ...");
                service.Start();
                m_Service = service;
                Log.Info(string.Format("EdgeDriverService ServiceUrl: {0}", m_Service.ServiceUrl));
                Log.Info("EdgeDriverService start OK");

                Log.Info("RemoteWebDriver init ...");
                this.m_Driver = new RemoteWebDriver(m_Service.ServiceUrl, options);
                Log.Info(string.Format("RemoteWebDriver SessionId: {0}", this.m_Driver.SessionId));
                Log.Info("RemoteWebDriver init OK");

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

        private bool GetDriverOptions(out EdgeOptions? edgeOptions)
        {
            try {
                edgeOptions = new EdgeOptions();
                
                #region Basic Options
                
                edgeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                edgeOptions.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
#if DEBUG
                edgeOptions.SetLoggingPreference(LogType.Browser, LogLevel.Debug);
                edgeOptions.SetLoggingPreference(LogType.Client, LogLevel.Debug);
                edgeOptions.SetLoggingPreference(LogType.Driver, LogLevel.Debug);
                edgeOptions.SetLoggingPreference(LogType.Profiler, LogLevel.Debug);
                edgeOptions.SetLoggingPreference(LogType.Server, LogLevel.Debug);
#endif

                #endregion Basic Options
               
                #region Startup Arguments

                List<string> argList = [
                      string.Format("{0}disable-infobars", m_ArgPfx)
                    , string.Format("{0}disable-automation", m_ArgPfx)
                ];

                List<string> excludeSwitchesList = [
                    string.Format("{0}enable-automation", m_ArgPfx)
                ];

                #endregion Startup Arguments

                #region Capabilities

                edgeOptions.AddAdditionalCapability("useAutomationExtension", false);

                Dictionary<string, object> profileDict = [];
                // supress popups with "0", enable popups with "1"
                int popups = m_EnablePopups ? 1 : 0;
                profileDict.Add("profile.default_content_settings.popups", popups);

                // Values for "notifications": 0 - Default, 1 - Allow, 2 - Block
                int notifications = m_EnableNotifications ? 1 : 2;
                profileDict.Add("profile.default_content_setting_values.notifications", notifications);

                Dictionary<string, object> optionDict = [];
                if (argList.Count > 0)
                    optionDict.Add("args", argList);

                if (excludeSwitchesList.Count > 0)
                    optionDict.Add("excludeSwitches", excludeSwitchesList);

                if (profileDict.Count > 0)
                    optionDict.Add("prefs", profileDict);

                //edgeOptions.AddAdditionalCapability("ms:edgeChrominum", true);
                if (optionDict.Count > 0)
                    edgeOptions.AddAdditionalCapability("ms:edgeOptions", optionDict);

                #endregion Capabilities

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                edgeOptions = null;
                return false;
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

//        /// <summary>
//        /// #TODO: this does not work using even when using Selenium 4.x and latest Edge WebDriver : - (
//        /// anather approach modifying user prefs could be:
//        /// a) https://stackoverflow.com/questions/60739613/change-default-download-location-on-edge-chromium
//        /// b) using selenium to navigate to "edge://settings/system" and toggle the according switch
//        /// </summary>
//        /// <returns></returns>
//        private bool GetUserPreferenceSleepingTabs(out string prefName, out Dictionary<string, object> prefValue)
//        {
//            try
//            {
////                Dictionary<string, object> awarenessBubbleSetting = new Dictionary<string, object>
////                {
////                    { "shown_times", 1 }
////                };
//
//                Dictionary<string, object> sleepingTabsSetting = new Dictionary<string, object>
//                {   
//                    //{"awareness_bubble", awarenessBubbleSetting },
//                    { "enabled", false }
//                    //{ "threshold", 43200 }
//                };
//
//                prefName = "sleeping_tabs";
//                prefValue = sleepingTabsSetting;
//
//                return true;
//            }
//            catch (Exception ex)
//            {
//                Log.Error(ex);
//                prefName = null;
//                prefValue = null;
//                return false;
//            }
//            finally
//            {
//                Log.Debug(Const.LogDone);
//            }
//        }

    } // class

} // namespace


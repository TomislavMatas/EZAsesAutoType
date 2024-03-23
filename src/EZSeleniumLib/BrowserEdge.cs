//
// File: BrowserEdge.cs
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
// 2024/03/22:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

using System;
using System.Collections.Generic;

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
    /// </summary>
    public class BrowserEdge : BrowserBase
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

        /// <summary>
        /// Singleton helper variabe.
        /// </summary>
        private string? m_InitMode = null ;

        /// <summary>
        /// Return value for "ÊZSeleniumLib.Browser.InitMode" from "App.config".
        /// </summary>
        public string InitMode { 
            get 
            {
                if (m_InitMode == null)
                    m_InitMode = ConfigSettings.GetBrowserInitMode();  
                return m_InitMode;
            }
        }

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
        /// Instantiate an instance of Edge WebDriver.
        /// </summary>
        /// <returns></returns>
        public override bool Initialize()
        {
            try
            {
                Log.Debug(Const.LogStart);
 
                string initMode = this.InitMode;
                if (String.IsNullOrEmpty(initMode))
                    throw new Exception(nameof(initMode) + Const.LogInvalid);

                if (ConfigSettings.BrowserInitModeSimple.Equals(initMode, StringComparison.OrdinalIgnoreCase))
                    return this.InitializeSimple();

                if (ConfigSettings.BrowserInitModeExtended.Equals(initMode, StringComparison.OrdinalIgnoreCase))
                    return InitializeExtended();

                throw new Exception(nameof(initMode) + Const.LogNotImpl);
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
        /// Instantiate an instance of Edge WebDriver.
        /// The instatiation method uses a minimalistic approach,
        /// applying only thare bare defaults.
        /// This mehtod will be called by "Initialize", 
        /// when "App.config" value for "ÊZSeleniumLib.Browser.InitMode" is set to "simple".
        /// </summary>
        /// <returns></returns>
        private bool InitializeSimple()
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
        /// Instantiate an instance of Edge WebDriver.
        /// The instatiation method uses a more sophisticated approach,
        /// allowing more detailed tweaking of individual properties.
        /// This mehtod will be called by "Initialize", 
        /// when "App.config" value for "ÊZSeleniumLib.Browser.InitMode" is set to "extended".
        /// </summary>
        /// <returns></returns>
        private bool InitializeExtended()
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
                Log.Info(String.Format("EdgeDriverService ServiceUrl: {0}", m_Service.ServiceUrl));
                Log.Info("EdgeDriverService start OK");

                Log.Info("RemoteWebDriver init ...");
                this.m_Driver = new RemoteWebDriver(m_Service.ServiceUrl, options);
                Log.Info(String.Format("RemoteWebDriver SessionId: {0}", this.m_Driver.SessionId));
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
                      String.Format("{0}disable-infobars", m_ArgPfx)
                    , String.Format("{0}disable-automation", m_ArgPfx)
                ];

                List<string> excludeSwitchesList = [
                    String.Format("{0}enable-automation", m_ArgPfx)
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


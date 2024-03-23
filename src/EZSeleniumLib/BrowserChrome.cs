//
// File: BrowserChrome.cs
//
// Summary:
// Specific browser implementation using 
// Selenium's WebDriver Class "OpenQA.Selenium.Chrome".
// 
// Notes:
// Requires a Selenium driver binary named 
// "chromedriver.exe" 
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
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

using log4net;

namespace EZSeleniumLib
{
    /// <summary>
    /// Specific browser implementation using 
    /// Selenium's WebDriver Class "OpenQA.Selenium.Chrome".
    /// Requires a Selenium driver binary named 
    /// "chromedriver.exe" 
    /// within system's search path.
    /// See "README.md" for details.
    /// </summary>
    public class BrowserChrome : BrowserBase
    {
        #region log4net

        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(BrowserChrome));
                return m_Log;
            }
        }

        #endregion

        #region private memberz

        /// <summary>
        /// Startup Argument decoration prefix.
        /// Either "--", "-", "/" or "" depending on browser implementation. 
        /// </summary>
        private const string m_ArgPfx = "--";

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
                    m_InitMode = LibConfig.GetBrowserInitMode();
                return m_InitMode;
            }
        }

        #endregion

        #region constructorz

        public BrowserChrome()
             : base()
        {
            // nothing special in here
        }

        public BrowserChrome(bool enablePopups, bool enableNotifications) 
             : base(enablePopups, enableNotifications)
        { 
            // nothing special in here
        }

        #endregion 

        /// <summary>
        /// Instantiate an instance of Chrome WebDriver.
        /// </summary>
        /// <returns></returns>
        public override bool Initialize()
        {
            try
            {
                Log.Debug(LogConst.Start);
 
                string initMode = this.InitMode;
                if(String.IsNullOrEmpty(initMode))
                    throw new Exception(nameof(initMode) + LogConst.Invalid);

                if (LibConfig.BrowserInitModeSimple.Equals(initMode, StringComparison.OrdinalIgnoreCase))
                    return this.InitializeSimple();

                if (LibConfig.BrowserInitModeExtended.Equals(initMode, StringComparison.OrdinalIgnoreCase))
                    return InitializeExtended();

                throw new Exception(nameof(initMode) + LogConst.NotImpl);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(LogConst.Done);
            }
        }

        /// <summary>
        /// Instantiate an instance of Chrome WebDriver.
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
                Log.Debug(LogConst.Start);

                if (!GetDriverOptions(out ChromeOptions? options))
                    throw new Exception(nameof(GetDriverOptions) + LogConst.Fail);

                this.m_Driver = new ChromeDriver(options);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(LogConst.Done);
            }
        }

        /// <summary>
        /// Instantiate an instance of Chrome WebDriver.
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
                Log.Debug(LogConst.Start);

                Log.Info("ChromeDriverService init ...");
                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
#if DEBUG
                service.EnableVerboseLogging = true;
                service.HideCommandPromptWindow = false;
                service.SuppressInitialDiagnosticInformation = false;
                //string logPath = @"ChromeDriverService.log";
                //try
                //{
                //    if (System.IO.File.Exists(logPath))
                //        System.IO.File.Delete(logPath);
                //}
                //catch
                //{
                //    // fail silently
                //}
                //service.LogPath = logPath;
                service.LogPath = "ChromeDriverService.log";
#else
                service.EnableVerboseLogging = false;
                service.HideCommandPromptWindow = true;
                service.SuppressInitialDiagnosticInformation = true;
                service.LogPath = null;
#endif
                Log.Info("ChromeDriverService init OK");

                Log.Info("GetDriverOptions ...");
                if (!GetDriverOptions(out ChromeOptions? options))
                    throw new Exception(nameof(GetDriverOptions) + LogConst.Fail);

                Log.Info("GetDriverOptions OK");

                Log.Info("ChromeDriverService start ...");
                service.Start();
                m_Service = service;
                Log.Info(String.Format("ChromeDriverService ServiceUrl: {0}", m_Service.ServiceUrl));
                Log.Info("ChromeDriverService start OK");

                Log.Info("RemoteWebDriver init ...");
                this.m_Driver = new RemoteWebDriver(remoteAddress: m_Service.ServiceUrl, options: options);
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
                Log.Debug(LogConst.Done);
            }
        }

        private bool GetDriverOptions(out ChromeOptions? chromeOptions)
        {
            try
            {
                chromeOptions = new ChromeOptions();

                #region Basic Options
                chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                chromeOptions.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
                chromeOptions.UseSpecCompliantProtocol = true;
#if DEBUG
                chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.Debug);
                chromeOptions.SetLoggingPreference(LogType.Client, LogLevel.Debug);
                chromeOptions.SetLoggingPreference(LogType.Driver, LogLevel.Debug);
                chromeOptions.SetLoggingPreference(LogType.Profiler, LogLevel.Debug);
                chromeOptions.SetLoggingPreference(LogType.Server, LogLevel.Debug);
#endif
                #endregion Basic Options

                #region Startup Arguments
                chromeOptions.AddArguments(String.Format("{0}disable-infobars", m_ArgPfx));
                chromeOptions.AddArguments(String.Format("{0}disable-automation", m_ArgPfx));
                chromeOptions.AddExcludedArguments(String.Format("{0}enable-automation", m_ArgPfx));
                if (!m_EnableNotifications)
                    chromeOptions.AddArguments(String.Format("{0}disable-notifications",m_ArgPfx));

                if (m_EnablePopups)
                    chromeOptions.AddArguments(String.Format("{0}disable-popup-blocking", m_ArgPfx));

                #endregion Startup Arguments

                # region Capabilities
                int popups = m_EnablePopups ? 1 : 0; // 1: enable popups, 0: supress popups.
                chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.popups", popups);

                int notifications = m_EnableNotifications ? 1 : 2; // 0: Default, 1: Allow, 2: Block.
                chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.notifications", notifications);

                List<string> tabDiscardExceptionsList = [];
//              tabDiscardExceptionsList.Add("web.whatsapp.com");

                Dictionary<string, object> tabDiscardDict = new Dictionary<string, object>
                {
                    { "exceptions", tabDiscardExceptionsList }
                };

                Dictionary<string, object> performanceTuningDict = new Dictionary<string, object>
                {
                    { "tab_discarding", tabDiscardDict }
                };

                chromeOptions.AddUserProfilePreference("performance_tuning", performanceTuningDict);

                #endregion Capabilities

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                chromeOptions = null;
                return false;
            }
            finally
            {
                Log.Debug(LogConst.Done);
            }
        }

//        /// <summary>
//        /// #TODO: this does not work using even when using Selenium 4.x and latest Chrome WebDriver : - (
//        /// anather approach modifying user prefs could be:
//        /// a) https://stackoverflow.com/questions/60739613/change-default-download-location-on-Chrome-chromium
//        /// b) using selenium to navigate to "Chrome://settings/system" and toggle the according switch
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
//                Log.Debug(LogConst.Done);
//            }
//        }

    } // class

} // namespace


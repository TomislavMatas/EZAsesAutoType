//
// File: "BrowserChrome.cs"
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
// 2024/07/26:TomislavMatas: Version "4.22.4"
// * Implement handling of browser specific "App.config"
//   setting "EZSeleniumLib.Browser.AdditionalOptions.Chrome".
// 2024/05/31:TomislavMatas: Version "4.21.1"
// * Simplify log4net implementations.
// 2024/05/29:TomislavMatas: Version "4.21.0"
// * Refactoring: Implement specific "SendKeys(IWebElement? element, string? keysToSend)".
// 2024/05/04:TomislavMatas: Version "4.20.0"
// * Upgrade to .NET version 8.
// 2024/04/04:TomislavMatas: Version "1.0.123"
// * Tidy~Up in "InitializeExtended": Change "Log.Info" to "Log.Debug".
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System.Diagnostics;

using log4net;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
    internal class BrowserChrome : BrowserBase
    {
        #region log4net

        private static readonly ILog Log = LogManager.GetLogger(typeof(BrowserChrome));

        [Conditional("DEBUG")]
        private static void LogTrace(object message)
        {
#if DEBUG
            Log.Debug(message);
#endif
        }

        #endregion

        #region private memberz

        private ChromeDriver? _driver = null;
        protected override WebDriver? GetDriver()
        {
            if (_driver == null)
                return null;

        	// Explicit type cast below is optional,
            // but kept for the sake of clarity.
            return (ChromeDriver)_driver;
        }

        private ChromeDriverService? _service = null;
        protected override DriverService? GetService()
        {
            if (_service == null)
                return null;

            return _service;
        }

        #endregion

        #region constructorz

        public BrowserChrome()
             : base()
        {
            // nothing special in here
        }

        public BrowserChrome(BrowserOptions browserOptions)
             : base(browserOptions)
        {
            // nothing special in here
        }

        #endregion 

        protected override string GetArgPrefix()
        {
            if (_argPfx == null)
                _argPfx = Consts.ARG_PREFIX_CHROME;

            return _argPfx;
        }

        protected override string GetProcessName()
        {
            if (_processName == null)
                _processName = Consts.BROWSER_PROCESSNAME_CHROME;

            return _processName;
        }

        /// <summary>
        /// Instantiate an instance of Chrome WebDriver.
        /// </summary>
        /// <returns></returns>
        public override bool Initialize()
        {
            try
            {
                LogTrace(Consts.LogStart);
 
                string initMode = this.BrowserOptions.InitMode;
                if(String.IsNullOrEmpty(initMode))
                    throw new Exception("InitMode invalid");

                if (Consts.INITMODE_SIMPLE.Equals(initMode, StringComparison.OrdinalIgnoreCase))
                    return this.InitializeSimple();

                if (Consts.INITMODE_EXTENDED.Equals(initMode, StringComparison.OrdinalIgnoreCase))
                    return InitializeExtended();

                throw new Exception("InitMode unsupported");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Instantiate an instance of Chrome WebDriver.
        /// The instatiation method uses a minimalistic approach,
        /// applying only thare bare defaults.
        /// This mehtod will be called by "Initialize", 
        /// when "App.config" value for "EZSeleniumLib.Browser.InitMode" is set to "simple".
        /// </summary>
        /// <returns></returns>
        private bool InitializeSimple()
        {
            try
            {
                LogTrace(Consts.LogStart);

                ChromeOptions? options = this.GetDriverOptions();
                if (options==null)
                    throw new Exception(nameof(this.GetDriverOptions)+Consts.LogFail);

                this._driver = new ChromeDriver(options);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Instantiate an instance of Chrome WebDriver.
        /// The instatiation method uses a more sophisticated approach,
        /// allowing more detailed tweaking of individual properties.
        /// This mehtod will be called by "Initialize", 
        /// when "App.config" value for "EZSeleniumLib.Browser.InitMode" is set to "extended".
        /// </summary>
        /// <returns></returns>
        private bool InitializeExtended()
        {
            try
            {
                LogTrace(Consts.LogStart);

                Log.Debug("ChromeDriverService init ...");
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
                Log.Debug("ChromeDriverService init OK");
                ChromeOptions? options = this.GetDriverOptions();
                if (options == null)
                    throw new Exception(nameof(this.GetDriverOptions) + Consts.LogFail);

                Log.Debug("ChromeDriverService start ...");
                service.Start();
                _service = service;
                Log.Debug(String.Format("ChromeDriverService ServiceUrl: {0}", _service.ServiceUrl));
                Log.Debug("ChromeDriverService start OK");

                Log.Debug("WebDriver init ...");
//                this._driver = new WebDriver(remoteAddress: _service.ServiceUrl, options: options);
                this._driver = new ChromeDriver(_service, options: options);
                Log.Debug(String.Format("WebDriver SessionId: {0}", this._driver.SessionId));
                Log.Debug("WebDriver init OK");

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        private ChromeOptions? GetDriverOptions()
        {
            try
            {
                LogTrace(Consts.LogStart);

                ChromeOptions options = new ChromeOptions();

                #region Basic Options
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                options.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
				// Selenium v3
				// options.UseSpecCompliantProtocol = true;
#if DEBUG
                options.SetLoggingPreference(LogType.Browser, LogLevel.Debug);
                options.SetLoggingPreference(LogType.Client, LogLevel.Debug);
                options.SetLoggingPreference(LogType.Driver, LogLevel.Debug);
                options.SetLoggingPreference(LogType.Profiler, LogLevel.Debug);
                options.SetLoggingPreference(LogType.Server, LogLevel.Debug);
#endif
                #endregion

                #region Startup Arguments

                string argPfx = this.GetArgPrefix();
                options.AddArguments(String.Format("{0}disable-infobars", argPfx));
                options.AddArguments(String.Format("{0}disable-automation", argPfx));
// Use "--no-sandbox" with caution - exposes unsecure exploits!
//              options.AddArguments(String.Format("{0}no-sandbox", argPfx));
// "--disable-dev-shm-usage" is only on linux / docker helpfull
//              options.AddArguments(String.Format("{0}disable-dev-shm-usage", argPfx));

                bool disableGPU = this.BrowserOptions.DisableGPU;
                if(disableGPU)
                    options.AddArguments(String.Format("{0}disable-gpu", argPfx));
                
                bool exposeGC = this.BrowserOptions.ExposeGC;
                if (exposeGC)
                    options.AddArguments(String.Format("{0}js-flags=--expose-gc", argPfx));

                bool preciseMemoryInfo = this.BrowserOptions.PreciseMemoryInfo;
                if (preciseMemoryInfo)
                    options.AddArguments(String.Format("{0}enable-precise-memory-info", argPfx));

                int scriptPID = this.BrowserOptions.ScriptPID;
                if (scriptPID > 0)
                {
                    SetScriptPID(scriptPID);
                    options.AddArguments(GetArgScriptPID());
                }

                options.AddExcludedArguments(String.Format("{0}enable-automation", argPfx));

                bool notificationsEnabled = this.BrowserOptions.NotificationsEnabled;
                if (!notificationsEnabled)
                    options.AddArguments(String.Format("{0}disable-notifications",argPfx));

                bool popupsEnabled = this.BrowserOptions.PopupsEnabled;
                if (popupsEnabled)
                    options.AddArguments(String.Format("{0}disable-popup-blocking", argPfx));

                string customAdditionalOptions = this.BrowserOptions.AdditionalOptions;
                if (!string.IsNullOrEmpty(customAdditionalOptions))
                {
                    string splitSeparator = ";";
                    StringSplitOptions stringSplitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
                    string[] customAdditionalOptionsArray = customAdditionalOptions.Split(splitSeparator, stringSplitOptions);
                    int customAdditionalOptionsArrayLength = customAdditionalOptionsArray.Length;
                    for (int i = 0; i < customAdditionalOptionsArrayLength; i++)
                        options.AddArguments(customAdditionalOptionsArray[i]);

                }

                #endregion

                # region Capabilities
                int popups = popupsEnabled ? 1 : 0; // 1: enable popups, 0: supress popups.
                options.AddUserProfilePreference("profile.default_content_setting_values.popups", popups);

                int notifications = notificationsEnabled ? 1 : 2; // 0: Default, 1: Allow, 2: Block.
                options.AddUserProfilePreference("profile.default_content_setting_values.notifications", notifications);

                List<string> tabDiscardExceptionsList = new List<string>();
                tabDiscardExceptionsList.Add("web.whatsapp.com");

                Dictionary<string, object> tabDiscardDict = new Dictionary<string, object>();
                tabDiscardDict.Add("exceptions", tabDiscardExceptionsList);

                Dictionary<string, object> performanceTuningDict = new Dictionary<string, object>();
                performanceTuningDict.Add("tab_discarding", tabDiscardDict);

                options.AddUserProfilePreference("performance_tuning", performanceTuningDict);

                #endregion

                return options;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                LogTrace(Consts.LogDone);
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
        //                LogTrace(Consts.LogDone);
        //            }
        //        }

        /// <summary>
        /// Execute driver command "Page.reload".
        /// With "ignoreCache = true" set, shall mimic "Control + F5".
        /// see -->< https://bugs.chromium.org/p/chromedriver/issues/detail?id=3249 >
        /// </summary>
        /// <param name="ignoreCache"></param>
        /// <returns></returns>
        public override bool PageReload(bool ignoreCache = true)
        {
            try
            {
                LogTrace(Consts.LogStart);
                if (_driver == null)
                    throw new Exception("_driver is null");

                Dictionary<string, object> details = new Dictionary<string, object>();
                details["ignoreCache"] = ignoreCache;
				// Selenium v3
				// _driver.ExecuteChromeCommand("Page.reload", details);
				// Selenium v4				
                _driver.ExecuteCustomDriverCommand("Page.reload", details);
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Initiate JavaScript VM Garbage Collection.
        /// </summary>
        /// <returns></returns>
        public override void GarbageCollect()
        {
            try
            {
                bool exposeGC = this.BrowserOptions.ExposeGC;
                if (!exposeGC)
                    return;

                this.ExecuteScript("window.gc()");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for "IJavaScriptExecutor.ExecuteScript()".
        /// </summary>
        /// <param name="script"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override object? ExecuteScript(string script, params object[] args)
        {
            try
            { 
                if (_driver == null)
                    throw new Exception("_driver is null");

                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                return js.ExecuteScript(script, args);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Return a new instance of class MemoryInfo. 
        /// MemoryInfo is the representation of the json object 
        /// returned by the JavaScript call "window.performance.memory".
        /// </summary>
        /// <returns></returns>
        public override MemoryInfo? GetMemoryInfo()
        {
            try
            { 
                object? obj = this.ExecuteScript("return window.performance.memory");
                if (obj == null)
                    throw new Exception(nameof(obj)+Consts.LogFail);

                MemoryInfo memoryInfo = new MemoryInfo(obj);
                return memoryInfo;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Write values of MemoryInfo's properties to Log. 
        /// </summary>
        /// <returns></returns>
        public override void DumpMemoryInfo(MemoryInfo memoryInfo)
        {
            try
            {
                if (memoryInfo == null)
                    return;

                Log.Info(memoryInfo.ToJsonString());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Transfer control to a specific TAB identified 
        /// by it's window handle.
        /// </summary>
        /// <returns></returns>
        public override bool SwitchTo(string handle)
        {
            try
            {
                LogTrace(Consts.LogStart);

                if (_driver == null)
                    throw new Exception("_driver is null");

                if (handle == null)
                    throw new Exception("handle is null");

                Log.Info(string.Format("Switch to TAB '{0}' ...", handle));
                IWebDriver _newDriver = _driver.SwitchTo().Window(handle);
                if(_newDriver == null )
                    throw new Exception("_newDriver is null");

                Log.Info(string.Format("Switch to TAB '{0}' OK", handle));
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Open a new TAB and transfer control to the newly opened TAB.
        /// </summary>
        /// <param name="closeOldTab">if set to true, closes the old TAB
        /// after the control has been successfully transfered to the newly opened TAB</param>
        /// <returns></returns>
        public override bool SwitchToNewTab(bool closeOldTab = true)
        {
            try
            {
                LogTrace(Consts.LogStart);

                if (_driver == null)
                    throw new Exception("_driver is null");

                // Store the old TAB list.
                List<string> oldTabList = new List<string>(_driver.WindowHandles);
                if (oldTabList == null)
                    throw new Exception("oldTabList is null");

                // store handle to current / old TAB, just fyi.
                string oldTabHandle = _driver.CurrentWindowHandle;
                if (oldTabHandle == null)
                    throw new Exception("oldTabHandle is null");

                // JavaScript "window.open()" opens a new TAB,
                // but Selenium Driver does not transfer control
                // to the newly opened TAB implizitly.
                Log.Info("Open new TAB ...");
                object? objOpenResult = this.ExecuteScript("window.open()");

                // although the scipt should execute "synchronously",
                // give browser some time to render the newly opened TAB.
                // Depending on browser implementation, there might be some 
                // initial loading going on which might require some time to finish.
                Thread.Sleep(this.GetDelay());

                // obtain "new" TAB list
                List<string> newTabList = new List<string>(_driver.WindowHandles);
                // the last element within newTabList contains the handle to the newly opend TAB
                int newTabIndex = newTabList.Count - 1;
                string newTabHandle = newTabList[newTabIndex];

                if (closeOldTab)
                {
                    // before switching to new TAB close "current" TAB _NOW_.
                    // It is still under Selenium Driver's control, as long
                    // as the driver.SwitchTo() command as not been issued.
                    Log.Info("Close old TAB ...");
                    object? objCloseResult = this.ExecuteScript("window.close()");
                    Thread.Sleep(this.GetDelay());
                }

                Log.Info("Switch to new TAB ...");
                if (!SwitchTo(newTabHandle))
                    throw new Exception("Switch to new TAB failed");

                Log.Info("Switch to new TAB OK");
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for element.SendKeys(keysToSend). 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="keysToSend"></param>
        /// <returns></returns>
        public override bool SendKeys(IWebElement? element, string? keysToSend)
        {
            try
            {
                if (element == null)
                    throw new ArgumentNullException(nameof(element));

                if (keysToSend == null)
                    throw new ArgumentNullException(nameof(keysToSend));

                element.SendKeys(keysToSend);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

    } // class

} // namespace


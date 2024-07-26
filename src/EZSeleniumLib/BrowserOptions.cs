//
// File: "BrowserOptions.cs"
//
// Summary:
// Generic browser options class. 
// 
// Revision History:
// 2024/07/26:TomislavMatas: Version "4.22.4"
// * Implement handling of browser specific "App.config"
//   settings "EZSeleniumLib.Browser.AdditionalOptions.Chrome",
//   "EZSeleniumLib.Browser.AdditionalOptions.Edge" and
//   "EZSeleniumLib.Browser.AdditionalOptions.Firefox".
// 2024/05/04:TomislavMatas: Version "4.20.0"
// * Upgrade to .NET version 8.
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

namespace EZSeleniumLib
{
    /// <summary>
    /// Generic browser options class.
    /// </summary>
    public class BrowserOptions
    {
        /// <summary>
		/// Currently supported values for "InitMode":
		/// "extended" : More sophisticated initalization.
		/// "simple"   : Limitied control during initalization.
        /// </summary>
        public string InitMode;
        public bool PopupsEnabled;
        public bool NotificationsEnabled;
        public bool DisableGPU;
        public bool ExposeGC;
        public bool PreciseMemoryInfo;
        /// <summary>
		/// The value for "Delay" specifies the
		/// delay in milliseconds after each browser interaction like 
		/// "enter some text", "click an element" or "move to element". 
        /// </summary>
        public int Delay;

        /// <summary>
		/// The value for the browser specific option can be specified in 
        /// "App.config" file using key name "EZSeleniumLib.Browser.*.AdditionalOptions".
		/// Multiple options can be provided using ';' (semicolon) as separator.
        /// Note: When supplying values for this option in "App.config" file,
        /// use the appropriate argument prefix:
        /// Chrome and Edge usuallay expect "double dash", e.g: "--OptionName". 
        /// Firefox usuallay expects "single dash", e.g.: "-OptionName".
        /// </summary>
        public String AdditionalOptions;

        /// <summary>
        /// The requestor script's process id.
        /// see: ->< https://stackoverflow.com/questions/47789640/get-handle-to-opened-chrome-window-in-selenium > 
        /// </summary>
        public int ScriptPID;

        /// <summary>
        /// Default constructor.
        /// Assign property values from "App.config".
        /// </summary>
        public BrowserOptions()
        {
            // the generic options
            this.InitMode             = Configs.GetAppSettingString(Consts.BrowserInitModeKeyName, Consts.INITMODE_DEFAULT);
            this.PopupsEnabled        = Configs.GetAppSettingBool(Consts.BrowserPopupsEnabledKeyName, Consts.POPUPSENABLED_DEFAULT);
            this.NotificationsEnabled = Configs.GetAppSettingBool(Consts.BrowserNotificationsEnabledKeyName, Consts.NOTIFICATIONSENABLED_DEFAULT);
            this.DisableGPU           = Configs.GetAppSettingBool(Consts.BrowserDisableGPUKeyName, Consts.DISABLEGPU_DEFAULT);
            this.ExposeGC             = Configs.GetAppSettingBool(Consts.BrowserExposeGCKeyName, Consts.EXPOSEGC_DEFAULT);
            this.PreciseMemoryInfo    = Configs.GetAppSettingBool(Consts.BrowserPreciseMemoryInfoEnabledKeyName, Consts.PRECISEMEMORYINFO_DEFAULT);
            this.Delay                = Configs.GetAppSettingInt(Consts.BrowserDelayKeyName, Consts.BROWSERDELAY_DEFAULT);
            this.ScriptPID            = System.Diagnostics.Process.GetCurrentProcess().Id;
            // the browser specific options require additional lookups against "App.config".
            string webdriver          = Configs.GetAppSettingString(Consts.WebDriverKeyName, Consts.BROWSERIMPLEMENTATATION_DEFAULT);
            this.AdditionalOptions    = this.GetBrowserSpecificSettingAdditionalOptions(webdriver);
        }

        /// <summary>
        /// Custom constructor.
        /// </summary>
        /// <param name="initMode"></param>
        /// <param name="popupsEnabled"></param>
        /// <param name="notificationsEnabled"></param>
        /// <param name="disableGPU"></param>
        /// <param name="exposeGC"></param>
        /// <param name="preciseMemoryInfo "></param>
        /// <param name="scriptPID"></param>
        public BrowserOptions(
              string initMode = "extended"
            , bool popupsEnabled = false
            , bool notificationsEnabled = false
            , bool disableGPU = false
            , bool exposeGC = true
            , bool preciseMemoryInfo = true
            , int scriptPID = -1 )
        {
            this.InitMode = initMode;
            this.PopupsEnabled = popupsEnabled;
            this.NotificationsEnabled = notificationsEnabled;
            this.DisableGPU = disableGPU; 
            this.ExposeGC = exposeGC;
            this.PreciseMemoryInfo= preciseMemoryInfo;
            this.ScriptPID = scriptPID;
            // the browser specific options require additional lookups against "App.config".
            string webdriver = Configs.GetAppSettingString(Consts.WebDriverKeyName, Consts.BROWSERIMPLEMENTATATION_DEFAULT);
            this.AdditionalOptions = this.GetBrowserSpecificSettingAdditionalOptions(webdriver);
        }

        /// <summary>
        /// Additiona browser specific lookups against "App.config" file.
        /// </summary>
        /// <param name="webdriver"></param>
        /// <returns></returns>
        private string GetBrowserSpecificSettingAdditionalOptions(string webdriver)
        {
            if (string.IsNullOrEmpty(webdriver))
                return string.Empty;

            string appConfigKeyName = Consts.BrowserAdditionalOptionsKeyNamePfx + webdriver;
            if (Consts.BROWSERIMPLEMENTATATION_CHROME.Equals(webdriver))
                return Configs.GetAppSettingString(appConfigKeyName, Consts.CHROME_ADDITIONALOPTIONS_DEFAULT);
            else if(Consts.BROWSERIMPLEMENTATATION_EDGE.Equals(webdriver))
                return Configs.GetAppSettingString(appConfigKeyName, Consts.EDGE_ADDITIONALOPTIONS_DEFAULT);
            else if (Consts.BROWSERIMPLEMENTATATION_FIREFOX.Equals(webdriver))
                return Configs.GetAppSettingString(appConfigKeyName, Consts.FIREFOX_ADDITIONALOPTIONS_DEFAULT);

            return string.Empty;
        }

    } // class

} // namespace

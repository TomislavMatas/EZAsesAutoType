//
// File: "BrowserOptions.cs"
//
// Summary:
// Generic browser options class. 
// 
// Revision History: 
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
            this.InitMode             = Configs.GetAppSettingString(Consts.BrowserInitModeKeyName, Consts.INITMODE_DEFAULT);
            this.PopupsEnabled        = Configs.GetAppSettingBool(Consts.BrowserPopupsEnabledKeyName, Consts.POPUPSENABLED_DEFAULT);
            this.NotificationsEnabled = Configs.GetAppSettingBool(Consts.BrowserNotificationsEnabledKeyName, Consts.NOTIFICATIONSENABLED_DEFAULT);
            this.DisableGPU           = Configs.GetAppSettingBool(Consts.BrowserDisableGPUKeyName, Consts.DISABLEGPU_DEFAULT);
            this.ExposeGC             = Configs.GetAppSettingBool(Consts.BrowserExposeGCKeyName, Consts.EXPOSEGC_DEFAULT);
            this.PreciseMemoryInfo    = Configs.GetAppSettingBool(Consts.BrowserPreciseMemoryInfoEnabledKeyName, Consts.PRECISEMEMORYINFO_DEFAULT);
            this.Delay                = Configs.GetAppSettingInt(Consts.BrowserDelayKeyName, Consts.BROWSERDELAY_DEFAULT);
            
            this.ScriptPID = System.Diagnostics.Process.GetCurrentProcess().Id;
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
        }

    } // class

} // namespace

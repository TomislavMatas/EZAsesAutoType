//
// File: "Consts.cs"
//
// Summary:
// Assembly specific "internal" constants. 
//
// Revision History: 
// 2024/03/24:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using System.Diagnostics;
using System.Reflection;

namespace EZSeleniumLib
{
    /// <summary>
    /// Project specific constants.
    /// </summary>
    internal static class Consts
    {
        #region fixed constantz

        /// <summary>
        /// Customized assembly's display name.
        /// </summary>
        public const string DisplayName = "EZSeleniumLib.dll";

        #region logging constantz
        public const string DEBUG_START = "START";
        public const string DEBUG_DONE = "DONE";
        public const string LogStart = " START";
        public const string LogDone = " DONE";
        public const string LogFail = " FAIL";
        public const string LogInvalid = " INVALID";
        public const string LogNotImpl = " NOT IMPLEMENTED";
        public const string LogIsNull = " IS NULL";
        #endregion

        #region "App.config" key namez 
        public const string WebDriverKeyName = "EZSeleniumLib.WebDriver";
        public const string BrowserInitModeKeyName = "EZSeleniumLib.Browser.InitMode";
        public const string BrowserDelayKeyName = "EZSeleniumLib.Browser.Delay";
        public const string BrowserPopupsEnabledKeyName = "EZSeleniumLib.Browser.PopupsEnabled";
        public const string BrowserNotificationsEnabledKeyName = "EZSeleniumLib.Browser.NotificationsEnabled";
        public const string BrowserDisableGPUKeyName = "EZSeleniumLib.Browser.DisableGPU";
        public const string BrowserExposeGCKeyName = "EZSeleniumLib.Browser.ExposeGC";
        public const string BrowserPreciseMemoryInfoEnabledKeyName = "EZSeleniumLib.Browser.PreciseMemoryInfo.Enabled";

        #endregion

        public const string ARG_PREFIX_CHROME = "--";
        public const string ARG_PREFIX_EDGE = "--";
        public const string ARG_PREFIX_FIREFOX = "-";

        /// <summary>
        /// Custom command line switch to store back reference to requestor script's process id.
        /// Note: For unknown reason, chrome-driver 
        /// will convert _any_ command line argument to lower case.
        /// </summary>
        public const string ARG_SCRIPTPID = "scriptpid_";

        /// <summary>
        /// One of the valid values for "EZSeleniumLib.Browser.InitMode" from "App.config".
        /// </summary>
        public const string INITMODE_SIMPLE = "simple";

        /// <summary>
        /// One of the valid values for "EZSeleniumLib.Browser.InitMode" from "App.config".
        /// </summary>
        public const string INITMODE_EXTENDED = "extended";

        /// <summary>
        /// Default value if value "EZSeleniumLib.Browser.InitMode" is missing in "App.config".
        /// </summary>
        public const string INITMODE_DEFAULT = Consts.INITMODE_EXTENDED;

        /// <summary>
        /// Default value if value "EZSeleniumLib.Browser.Delay" is missing in "App.config".
        /// </summary>
        public const int BROWSERDELAY_DEFAULT = 250;

        /// <summary>
        /// Default value if "EZSeleniumLib.Browser.PopupsEnabled" is missing in "App.config".
        /// </summary>
        public const bool POPUPSENABLED_DEFAULT = false;

        /// <summary>
        /// Default value if "EZSeleniumLib.Browser.NotificationsEnabled" is missing in "App.config".
        /// </summary>
        public const bool NOTIFICATIONSENABLED_DEFAULT = false;

        /// <summary>
        /// Default value if "EZSeleniumLib.Browser.DisableGPU" is missing in "App.config".
        /// </summary>
        public const bool DISABLEGPU_DEFAULT = false;

        /// <summary>
        /// Default value if "EZSeleniumLib.Browser.ExposeGC" is missing in "App.config".
        /// </summary>
        public const bool EXPOSEGC_DEFAULT = false;

        /// <summary>
        /// Default value if "EZSeleniumLib.Browser.PreciseMemoryInfo.Enabled" is missing in "App.config".
        /// </summary>
        public const bool PRECISEMEMORYINFO_DEFAULT = false;

        public const string BROWSERIMPLEMENTATATION_CHROME = Constant.WebDriverChrome;
        public const string BROWSERIMPLEMENTATATION_EDGE = Constant.WebDriverEdge;
        public const string BROWSERIMPLEMENTATATION_FIREFOX = Constant.WebDriverFirefox;
        public const string BROWSERIMPLEMENTATATION_DEFAULT = Constant.WebDriverChrome;

        public const string BROWSER_PROCESSNAME_CHROME = "chrome";
        public const string BROWSER_PROCESSNAME_EDGE = "msedge";
        public const string BROWSER_PROCESSNAME_FIREFOX = "firefox";

        #endregion 

        #region derived constantz

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        public static Assembly? m_ExecutingAssembly = null;

        /// <summary>
        /// Return instance of class FileVersionInfo reflecting AssemblyFileVersion from AssemblyInfo.cs.
        /// </summary>
        public static Assembly ExecutingAssembly
        {
            get
            {
                if (m_ExecutingAssembly == null)
                    m_ExecutingAssembly = Assembly.GetExecutingAssembly();
                return m_ExecutingAssembly;
            }
        }

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static FileVersionInfo? m_AssemblyVersionInfo = null;

        /// <summary>
        /// Return instance of class FileVersionInfo reflecting AssemblyFileVersion from AssemblyInfo.cs.
        /// </summary>
        public static FileVersionInfo AssemblyVersionInfo
        {
            get
            {
                if (m_AssemblyVersionInfo == null)
                    m_AssemblyVersionInfo = FileVersionInfo.GetVersionInfo(ExecutingAssembly.Location);
                return m_AssemblyVersionInfo;
            }
        }

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        public static string? m_AssemblyPath = null;

        /// <summary>
        /// Return the executing assembly's path - aka "installation folder path".
        /// </summary>
        public static string ExecutingAssemblyPath
        {
            get
            {
                if (m_AssemblyPath == null)
                {
                    FileInfo file = new FileInfo(ExecutingAssembly.Location);
                    if (file.DirectoryName != null)
                        m_AssemblyPath = file.DirectoryName;
                    else
                        m_AssemblyPath = ".";
                }
                return m_AssemblyPath;
            }
        }

#if DEBUG
        private const string DEBUGSUFFIX = " (dbg)";
#else
        private const string DEBUGSUFFIX = "";
#endif

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static string? m_AssemblyVersionString = null;

        /// <summary>
        /// Return AssemblyFileVersion from AssemblyInfo.cs as dot seperated string.
        /// </summary>
        public static string AssemblyVersionString
        {
            get
            {
                if (m_AssemblyVersionString == null)
                {
                    m_AssemblyVersionString = string.Format("{0}.{1}.{2}.{3}{4}"
                        , Consts.AssemblyVersionInfo.FileMajorPart
                        , Consts.AssemblyVersionInfo.FileMinorPart
                        , Consts.AssemblyVersionInfo.FileBuildPart
                        , Consts.AssemblyVersionInfo.FilePrivatePart
                        , Consts.DEBUGSUFFIX);
                }
                return m_AssemblyVersionString;
            }
        }

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static string? m_Description = null;

        /// <summary>
        /// Return custom Assembly Description.
        /// </summary>
        public static string Description
        {
            get
            {
                if (m_Description == null)
                {
                    m_Description = string.Format("{0} {1}"
                        , Consts.DisplayName
                        , Consts.AssemblyVersionString);
                }
                return m_Description;
            }
        }
        #endregion

    } // class

} // namespace

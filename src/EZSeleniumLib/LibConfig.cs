//
// File: LibConfig.cs
//
// Summary:
// Project specific configuration settings. 
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

using ConfigApi = EZSeleniumLib.ConfigApi;

namespace EZSeleniumLib
{
    /// <summary>
    /// Project specific constants.
    /// </summary>
    public static class LibConfig
    {
        #region "ÊZSeleniumLib.Browser.WebDriver"
        public const string BROWSERIMPLEMENTATIONCHROME = "Chrome";
        public const string BROWSERIMPLEMENTATIONEDGE = "Edge";
        private const string BROWSERIMPLEMENTATIONNAME = "ÊZSeleniumLib.Browser.WebDriver";
        private const string BROWSERIMPLEMENTATIONDEFAULT = BROWSERIMPLEMENTATIONCHROME;
        private static string? m_BrowserImplementation = null;
        private static string BrowserImplementation
        {
            get
            {
                if (m_BrowserImplementation == null)
                    m_BrowserImplementation = ConfigApi.GetAppSettingString(BROWSERIMPLEMENTATIONNAME, BROWSERIMPLEMENTATIONDEFAULT);
                return m_BrowserImplementation;
            }
            set
            {
                m_BrowserImplementation = value;
            }
        }
        public static string GetBrowserImplementation()
        {
            return BrowserImplementation;
        }
        public static string SetBrowserImplementation(string value)
        {
            string prev = BrowserImplementation;
            BrowserImplementation = value;
            return prev;
        }
        #endregion 

        #region "ÊZSeleniumLib.Browser.InitMode"
        public const string BrowserInitModeSimple = "simple";
        public const string BrowserInitModeExtended = "extended";
        private const string BrowserInitModeKeyName = "ÊZSeleniumLib.Browser.InitMode";
        private const string BrowserInitModeDefault = BrowserInitModeExtended;
        private static string? m_BrowserInitMode = null;
        private static string BrowserInitMode
        {
            get
            {
                if (m_BrowserInitMode == null)
                    m_BrowserInitMode = ConfigApi.GetAppSettingString(BrowserInitModeKeyName, BrowserInitModeDefault);
                return m_BrowserInitMode;
            }
            set
            {
                m_BrowserInitMode = value;
            }
        }
        public static string GetBrowserInitMode()
        {
            return BrowserInitMode;
        }
        public static string SetBrowserInitMode(string value)
        {
            string prev = BrowserInitMode;
            BrowserInitMode = value;
            return prev;
        }
        #endregion 

        #region "ÊZSeleniumLib.Browser.Interaction.Delay"
        private const string BrowserInteractionDelayKeyName = "ÊZSeleniumLib.Browser.Interaction.Delay";
        private const int BrowserInteractionDelayDefault = 500;
        private static int? m_BrowserInteractionDelay = null;
        private static int BrowserInteractionDelay
        {
            get
            {
                if (m_BrowserInteractionDelay == null)
                    m_BrowserInteractionDelay = ConfigApi.GetAppSettingInt(BrowserInteractionDelayKeyName, BrowserInteractionDelayDefault);
                return (int)m_BrowserInteractionDelay;
            }
            set
            {
                m_BrowserInteractionDelay = value;
            }
        }
        public static int GetBrowserInteractionDelay()
        {
            return BrowserInteractionDelay;
        }
        public static int SetBrowserInteractionDelay(int value)
        {
            int prev = BrowserInteractionDelay;
            BrowserInteractionDelay = value;
            return prev;
        }
        #endregion 

    } // class

} // namespace

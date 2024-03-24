//
// File: "ConfigSettings.cs"
//
// Summary:
// Assembly specific configuration settings.
//
// Revision History: 
// 2024/03/24:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

namespace EZSeleniumLib
{
    /// <summary>
    /// Assembly specific configuration settings.
    /// </summary>
    public static class ConfigSettings
    {
        #region "EZSeleniumLib.WebDriver"
        private static string? m_WebDriver = null;
        private static string WebDriver
        {
            get
            {
                if (m_WebDriver == null)
                    m_WebDriver = ConfigApi.GetAppSettingString(Const.WebDriverKeyName, Const.WebDriverDefault);
                return m_WebDriver;
            }
            set
            {
                m_WebDriver = value;
            }
        }
        public static string GetWebDriver()
        {
            return WebDriver;
        }
        public static string SetWebDriver(string value)
        {
            string prev = WebDriver;
            WebDriver = value;
            return prev;
        }
        #endregion 

        #region "EZSeleniumLib.WebDriver.InitMode"
        private static string? m_WebDriverInitMode = null;
        private static string WebDriverInitMode
        {
            get
            {
                if (m_WebDriverInitMode == null)
                    m_WebDriverInitMode = ConfigApi.GetAppSettingString(Const.WebDriverInitModeKeyName, Const.WebDriverInitModeDefault);
                return m_WebDriverInitMode;
            }
            set
            {
                m_WebDriverInitMode = value;
            }
        }
        public static string GetWebDriverInitMode()
        {
            return WebDriverInitMode;
        }
        public static string SetWebDriverInitMode(string value)
        {
            string prev = WebDriverInitMode;
            WebDriverInitMode = value;
            return prev;
        }
        #endregion 

        #region "EZSeleniumLib.WebDriver.Delay"
        private static int? m_WebDriverDelay = null;
        private static int WebDriverDelay
        {
            get
            {
                if (m_WebDriverDelay == null)
                    m_WebDriverDelay = ConfigApi.GetAppSettingInt(Const.WebDriverDelayKeyName, Const.WebDriverDelayDefault);
                return (int)m_WebDriverDelay;
            }
            set
            {
                m_WebDriverDelay = value;
            }
        }
        public static int GetWebDriverDelay()
        {
            return WebDriverDelay;
        }
        public static int SetWebDriverDelay(int value)
        {
            int prev = WebDriverDelay;
            WebDriverDelay = value;
            return prev;
        }
        #endregion 

    } // class

} // namespace

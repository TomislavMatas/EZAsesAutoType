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

using System.Diagnostics;
using System.Reflection;

namespace AsesAutoTypeLib
{
    /// <summary>
    /// Project specific constants.
    /// </summary>
    public static class LibConfig
    {
        #region "Browser.Implementation"
        public const string BROWSERIMPLEMENTATIONCHROME = "Chrome";
        public const string BROWSERIMPLEMENTATIONEDGE = "Edge";
        private const string BROWSERIMPLEMENTATIONNAME = "Browser.Implementation";
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

        #region "Browser.InitMode"
        public const string BROWSERINITMODESIMPLE = "simple";
        public const string BROWSERINITMODEEXTENDED = "extended";
        private const string BROWSERINITMODENAME = "Browser.InitMode";
        private const string BROWSERINITMODEDEFAULT = BROWSERINITMODEEXTENDED;
        private static string? m_BrowserInitMode = null;
        private static string BrowserInitMode
        {
            get
            {
                if (m_BrowserInitMode == null)
                    m_BrowserInitMode = ConfigApi.GetAppSettingString(BROWSERINITMODENAME, BROWSERINITMODEDEFAULT);
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

    } // class

} // namespace

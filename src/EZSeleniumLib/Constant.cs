//
// File: "Constant.cs"
//
// Summary:
// Cononstants with "public" relevance.
//
// Revision History: 
// 2024/03/24:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

namespace EZSeleniumLib
{
    /// <summary>
    /// Assembly specific constants.
    /// </summary>
    public static class Constant
    {
        #region WebDriver
        public const string WebDriverChrome = "Chrome";
        public const string WebDriverChromeExe = "chromedriver.exe";
        /// <summary>
        /// WebDriver version used on Build.
        /// </summary>
        public const string WebDriverChromeVersion = "125.0.6368.2";
        public const string WebDriverEdge = "Edge";
        public const string WebDriverEdgeExe = "MicrosoftWebDriver.exe";
        /// <summary>
        /// WebDriver version used on Build.
        /// </summary>
        public const string WebDriverEdgeVersion = "124.0.2464.2";
        public const string WebDriverInitModeSimple = "simple";
        public const string WebDriverInitModeExtended = "extended";
        #endregion

    } // class

} // namespace

//
// File: "Constant.cs"
//
// Summary:
// Constants with "public" relevance.
//
// Revision History: 
// 2024/03/24:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

namespace EZSeleniumLib
{
    /// <summary>
    /// Constants with "public" relevance.
    /// </summary>
    public static class Constant
    {
        #region WebDriver

        #region "Chrome"

        /// <summary>
        /// WebDriver name.
        /// </summary>
        public const string WebDriverChrome = "Chrome";
        
        /// <summary>
        /// WebDriver executable.
        /// </summary>
        public const string WebDriverChromeExe = "chromedriver.exe";
        
        /// <summary>
        /// WebDriver executable version used during build.
        /// </summary>
        public const string WebDriverChromeVersion = "125.0.6368.2";

        #endregion

        #region "Edge"

        /// <summary>
        /// "Logical" WebDriver name.
        /// </summary>
        public const string WebDriverEdge = "Edge";

        /// <summary>
        /// WebDriver executable.
        /// </summary>
        public const string WebDriverEdgeExe = "MicrosoftWebDriver.exe";

        /// <summary>
        /// WebDriver executable version used during build.
        /// </summary>
        public const string WebDriverEdgeVersion = "124.0.2464.2";

        #endregion

        #region "Firefox"

        /// <summary>
        /// "Logical" WebDriver name.
        /// </summary>
        public const string WebDriverFirefox = "Chrome";

        /// <summary>
        /// WebDriver executable.
        /// </summary>
        public const string WebDriverFirefoxExe = "geckodriver.exe";

        /// <summary>
        /// WebDriver executable version used during build.
        /// </summary>
        public const string WebDriverFirefoxVersion = "0.34.0";

        #endregion

        #endregion

    } // class

} // namespace

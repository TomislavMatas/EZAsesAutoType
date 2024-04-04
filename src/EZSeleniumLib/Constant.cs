//
// File: "Constant.cs"
//
// Summary:
// Constants with "public" relevance.
//
// Revision History: 
// 2024/04/04:TomislavMatas: Version "1.0.123"
// * Update "MicrosoftWebDriver.exe" to version "123.0.2420.65".
// 2024/04/04:TomislavMatas: Version "1.0.0"
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
        public const string WebDriverChromeVersion = "123.0.6312.58";

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
        public const string WebDriverEdgeVersion = "123.0.2420.65";

        #endregion

        #region "Firefox"

        /// <summary>
        /// "Logical" WebDriver name.
        /// </summary>
        public const string WebDriverFirefox = "Firefox";

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

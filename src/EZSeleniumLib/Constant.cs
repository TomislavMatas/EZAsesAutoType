//
// File: "Constant.cs"
//
// Summary:
// Constants with "public" relevance.
//
// Revision History:
// 2024/05/09:TomislavMatas: Version "4.20.0"
// * Upgrade "Selenium" libs to version "4.20.0".
// 2024/05/03:TomislavMatas: Version "1.125.0"
// * Update "chromedriver.exe"       to version "125.0.6422.26".
// * Update "MicrosoftWebDriver.exe" to version "125.0.2535.13".
// 2024/05/02:TomislavMatas: Version "1.124.0"
// * Update "chromedriver.exe"       to version "124.0.6367.91".
// * Update "MicrosoftWebDriver.exe" to version "124.0.2477.0".
// 2024/04/04:TomislavMatas: Version "1.124.0"
// * Update "chromedriver.exe"       to version "124.0.6367.29".
// * Update "MicrosoftWebDriver.exe" to version "124.0.2478.19".
// 2024/04/04:TomislavMatas: Version "1.0.123"
// * Update "MicrosoftWebDriver.exe" to version "123.0.2420.65".
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
        public const string WebDriverChromeVersion = "125.0.6422.26";

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
        public const string WebDriverEdgeVersion = "125.0.2535.13";

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

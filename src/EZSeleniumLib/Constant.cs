//
// File: "Constant.cs"
//
// Summary:
// Constants with "public" relevance.
//
// Revision History:
// 2024/11/19:TomislavMatas: Version "4.25.1"
// * Update "chromedriver.exe" to version "131.0.6778.69".
// * Update "msedgedriver.exe" to version "131.0.2903.51".
// 2024/07/06:TomislavMatas: Version "4.22.3"
// * Update "chromedriver.exe" to version "127.0.6533.26".
// * Update "msedgedriver.exe" to version "127.0.2651.31".
// 2024/06/24:TomislavMatas: Version "4.22.0"
// * Update "chromedriver.exe" to version "126.0.6478.115".
// * Update "msedgedriver.exe" to version "126.0.2592.68".
// 2024/05/31:TomislavMatas: Version "4.21.1"
// * Update "chromedriver.exe" to version "126.0.6478.26".
// * Update "msedgedriver.exe" to version "126.0.2592.24".
// 2024/05/16:TomislavMatas: Version "4.20.1"
// * Update "chromedriver.exe" to version "125.0.6422.60".
// * Update "msedgedriver.exe" to version "125.0.2535.47".
// 2024/05/04:TomislavMatas: Version "4.20.0"
// * Upgrade to .NET version 8.
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
        public const string WebDriverChromeVersion = "131.0.6778.69";
        
        #endregion

        #region "Edge"

        /// <summary>
        /// "Logical" WebDriver name.
        /// </summary>
        public const string WebDriverEdge = "Edge";

        /// <summary>
        /// WebDriver executable.
        /// </summary>
        public const string WebDriverEdgeExe = "msedgedriver.exe";

        /// <summary>
        /// WebDriver executable version used during build.
        /// </summary>
        public const string WebDriverEdgeVersion = "131.0.2903.51";

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
        public const string WebDriverFirefoxVersion = "0.35.0";

        #endregion

        #endregion

    } // class

} // namespace

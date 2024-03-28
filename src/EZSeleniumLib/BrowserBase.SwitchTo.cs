//
// File: "BrowserBase.SwitchTo.cs"
//
// Summary:
// Supplementary methods for "BrowserBase".
// Implementation of wrappers for "WebDriver.SwitchTo().*" methods.
//
// Revision History: 
// 2024/03/24:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using OpenQA.Selenium;

namespace EZSeleniumLib
{
    public abstract partial class BrowserBase
    {
        /// <summary>
        /// Wrapper for "Driver.SwitchTo.Frame(iFrame);".
        /// See: < https://www.selenium.dev/documentation/webdriver/interactions/frames/ >
        /// </summary>
        /// <param name="iFrame"></param>
        /// <returns></returns>
        public IWebDriver SwitchToIFrame(IWebElement iFrame)
        {
            try
            {
                Log.Debug(DEBUG_START);
                if (iFrame == null)
                    throw new ArgumentNullException(nameof(iFrame));

                if (Driver == null)
                    throw new Exception(nameof(Driver) + Consts.LogIsNull);

                IWebDriver webDriverIFrame = Driver.SwitchTo().Frame(iFrame);
                if (webDriverIFrame == null)
                    throw new Exception(nameof(webDriverIFrame) + Consts.LogIsNull);

                Thread.Sleep(this.GetDelay());
                return webDriverIFrame;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                Log.Debug(DEBUG_DONE);
            }

        }

    } // class

} // namespace

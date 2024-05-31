//
// File: "Worker.BrowserConfig.cs"
//
// Revision History: 
// 2024/05/31:TomislavMatas: Version "1.126.0"
// * Simplify log4net implementations.
// 2024/05/10:TomislavMatas: Version "1.125.0"
// * Enhance NULL value handling and validation.
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using EZSeleniumLib;

namespace EZAsesAutoType
{
    internal partial class Worker
    {
        #region "BrowserOptions" - wrapperz

        /// <summary>
        /// Return a new instance of class "BrowserOptions" 
        /// with default values.
        /// </summary>
        /// <returns></returns>
        private BrowserOptions GetBrowserOptionsDefault()
        {
            BrowserOptions browserOptions = this.WorkerConfig.GetBrowserOptions();
            return browserOptions;
        }

        #endregion

        #region Browser instantiation wrapperz

        /// <summary>
        /// Return a specific descendant of class "BrowserBase" 
        /// using "BrowserFactory".
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="browserOptions"></param>
        /// <returns></returns>
        private BrowserBase? GetBrowserInstance(string webDriver, BrowserOptions browserOptions)
        {
            try
            {
                LogTrace(Const.LogStart);
                return BrowserFactory.GetBrowserInstance(webDriver, browserOptions);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        /// <summary>
        /// Graceful TearDown of browser.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="browserOptions"></param>
        /// <returns></returns>
        private void TeardownBrowserInstance(BrowserBase? browser)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (browser != null)
                {
                    browser.Cleanup();
                    browser = null;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        #endregion

    } // class

} // namespace

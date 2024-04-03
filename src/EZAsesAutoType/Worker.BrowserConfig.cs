//
// File: "Worker.BrowserConfig.cs"
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "1.0.0.0"
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

        #endregion "BrowserOptions" - wrapperz

        #region Browser instantiation wrapperz

        /// <summary>
        /// Return a specific descendant of class "BrowserBase" 
        /// using "BrowserFactory".
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="browserOptions"></param>
        /// <returns></returns>
        private BrowserBase GetBrowserInstance(string webDriver, BrowserOptions browserOptions)
        {
            try
            {
                Log.Debug(Const.LogStart);
                return BrowserFactory.GetBrowserInstance(webDriver, browserOptions);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        /// Graceful TearDown of browser.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="browserOptions"></param>
        /// <returns></returns>
        private void TeardownBrowserInstance(BrowserBase browser)
        {
            try
            {
                Log.Debug(Const.LogStart);
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
                Log.Debug(Const.LogDone);
            }
        }

        #endregion Browser instantiation wrapperz

    } // class

} // namespace

//
// File: "BrowserFactory.cs"
//
// Summary:
// Browser factory helper class. 
// 
// Revision History: 
// 2024/05/31:TomislavMatas: Version "4.21.1"
// * Simplify log4net implementations.
// 2024/05/04:TomislavMatas: Version "4.20.0"
// * Upgrade to .NET version 8.
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System.Diagnostics;

using log4net;

namespace EZSeleniumLib
{
    /// <summary>
    /// Browser factory helper class.
    /// </summary>
    public static class BrowserFactory
    {
        #region log4net

        private static readonly ILog Log = LogManager.GetLogger(typeof(BrowserFactory));

        [Conditional("DEBUG")]
        private static void LogTrace(object message)
        {
#if DEBUG
            Log.Debug(message);
#endif
        }

        #endregion

        /// <summary>
        /// Instantiate a specific descendant of "BrowserBase".
        /// </summary>
        /// <returns></returns>
        public static BrowserBase? GetBrowserInstance()
        {
            try
            {
                LogTrace(Consts.LogStart);
                string browserImplementation = Consts.BROWSERIMPLEMENTATATION_DEFAULT;
                BrowserOptions browserOptions = new BrowserOptions();
                return GetBrowserInstance(browserImplementation, browserOptions);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Instantiate a specific descendant of "BrowserBase".
        /// </summary>
        /// <returns></returns>
        public static BrowserBase? GetBrowserInstance(string browserImplementation)
        {
            try
            {
                LogTrace(Consts.LogStart);
                BrowserOptions browserOptions = new BrowserOptions();
                return GetBrowserInstance(browserImplementation, browserOptions);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Instantiate a specific descendant of "BrowserBase".
        /// </summary>
        /// <returns></returns>
        public static BrowserBase? GetBrowserInstance(string browserImplementation, BrowserOptions browserOptions)
        {
            try
            {
                LogTrace(Consts.LogStart);

                if(String.IsNullOrEmpty(browserImplementation))
                    throw new ArgumentNullException(nameof(browserImplementation));

                if(browserOptions==null)
                    throw new ArgumentNullException(nameof(browserOptions));
                
                Log.Debug(String.Format("browserImplementation: {0}", browserImplementation));
                browserImplementation = browserImplementation.ToLower();
                if (browserImplementation.Equals(Consts.BROWSERIMPLEMENTATATION_EDGE.ToLower()))
                    return GetBrowserInstanceEdge(browserOptions);

                if (browserImplementation.Equals(Consts.BROWSERIMPLEMENTATATION_CHROME.ToLower()))
                    return GetBrowserInstanceChrome(browserOptions);

                if (browserImplementation.Equals(Consts.BROWSERIMPLEMENTATATION_FIREFOX.ToLower()))
                    return GetBrowserInstanceFirefox(browserOptions);

                throw new Exception("Unsupported browserImplementation value");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Return instance of class "BorwserEdge".
        /// </summary>
        /// <returns></returns>
        private static BrowserBase? GetBrowserInstanceEdge(BrowserOptions browserOptions)
        {
            try
            {
                LogTrace(Consts.LogStart);
                return new BrowserEdge(browserOptions);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Return instance of class "BorwserChrome".
        /// </summary>
        /// <returns></returns>
        private static BrowserBase? GetBrowserInstanceChrome(BrowserOptions browserOptions)
        {
            try
            {
                LogTrace(Consts.LogStart);
                return new BrowserChrome(browserOptions);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Return instance of class "BorwserFirefox".
        /// </summary>
        /// <returns></returns>
        private static BrowserBase? GetBrowserInstanceFirefox(BrowserOptions browserOptions)
        {
            try
            {
                LogTrace(Consts.LogStart);
                return new BrowserFirefox(browserOptions);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

    } // class

} // namespace


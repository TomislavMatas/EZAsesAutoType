//
// File: "BrowserFactory.cs"
//
// Summary:
// Browser factory helper class. 
// 
// Revision History: 
// 2024/05/04:TomislavMatas: Version "4.20.0"
// * Upgrade to .NET version 8.
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using log4net;

namespace EZSeleniumLib
{
    /// <summary>
    /// Browser factory helper class.
    /// </summary>
    public static class BrowserFactory
    {
        #region log4net

        private static ILog? _log = null;
        private static ILog Log
        {
            get
            {
                if (_log == null)
                    _log = LogManager.GetLogger(typeof(BrowserFactory));
                return _log;
            }
        }

        #endregion

        private const string DEBUG_START = Consts.DEBUG_START;
        private const string DEBUG_DONE = Consts.DEBUG_DONE;

        /// <summary>
        /// Instantiate a specific descendant of "BrowserBase".
        /// </summary>
        /// <returns></returns>
        public static BrowserBase? GetBrowserInstance()
        {
            try
            {
                Log.Debug(DEBUG_START);
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
                Log.Debug(DEBUG_DONE);
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
                Log.Debug(DEBUG_START);
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
                Log.Debug(DEBUG_DONE);
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
                Log.Debug(DEBUG_START);

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
                Log.Debug(DEBUG_DONE);
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
                Log.Debug(DEBUG_START);
                return new BrowserEdge(browserOptions);
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

        /// <summary>
        /// Return instance of class "BorwserChrome".
        /// </summary>
        /// <returns></returns>
        private static BrowserBase? GetBrowserInstanceChrome(BrowserOptions browserOptions)
        {
            try
            {
                Log.Debug(DEBUG_START);
                return new BrowserChrome(browserOptions);
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

        /// <summary>
        /// Return instance of class "BorwserFirefox".
        /// </summary>
        /// <returns></returns>
        private static BrowserBase? GetBrowserInstanceFirefox(BrowserOptions browserOptions)
        {
            try
            {
                Log.Debug(DEBUG_START);
                return new BrowserFirefox(browserOptions);
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


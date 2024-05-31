//
// File: "BrowserBase.Cookies.cs"
//
// Summary:
// Supplementary methods for "BrowserBase".
// Wrapper for "WebDriver.Manage().Cookies.*" methods.
//
// Revision History: 
// 2024/05/31:TomislavMatas: Version "4.21.1"
// * Simplify log4net implementations.
// 2024/05/04:TomislavMatas: Version "4.20.0"
// * Upgrade to .NET version 8.
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System.Collections.ObjectModel;

using OpenQA.Selenium;

namespace EZSeleniumLib
{
    public abstract partial class BrowserBase
    {
        /// <summary>
        /// Wrapper for "Driver.Manage().Cookies.AllCookies()".
        /// </summary>
        public ReadOnlyCollection<Cookie> AllCookies()
        {
            try
            {
                LogTrace(Consts.LogStart);
                if (Driver == null)
                    throw new Exception("Driver is null");

                IOptions manage = Driver.Manage();
                if (manage == null)
                    throw new Exception("manage is null");

                ICookieJar cookieJar = manage.Cookies;
                if (cookieJar == null)
                    throw new Exception("cookieJar is null");

                return cookieJar.AllCookies;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new ReadOnlyCollection<Cookie>(new List<Cookie>());
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.Manage().Cookies.AddCookie()".
        /// </summary>
        public bool AddCookie(Cookie cookie)
        {
            try
            {
                LogTrace(Consts.LogStart);
                if (Driver == null)
                    throw new Exception("Driver is null");

                IOptions manage = Driver.Manage();
                if (manage == null)
                    throw new Exception("manage is null");

                ICookieJar cookieJar = manage.Cookies;
                if (cookieJar == null)
                    throw new Exception("cookieJar is null");

                cookieJar.AddCookie(cookie);
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.Manage().Cookies.DeleteCookie()".
        /// </summary>
        public bool DeleteCookie(Cookie cookie)
        {
            try
            {
                LogTrace(Consts.LogStart);
                if (Driver == null)
                    throw new Exception("Driver is null");

                IOptions manage = Driver.Manage();
                if (manage == null)
                    throw new Exception("manage is null");

                ICookieJar cookieJar = manage.Cookies;
                if (cookieJar == null)
                    throw new Exception("cookieJar is null");

                cookieJar.DeleteCookie(cookie);
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.Manage().Cookies.DeleteCookieNamed()".
        /// </summary>
        public bool DeleteCookieNamed(string name)
        {
            try
            {
                LogTrace(Consts.LogStart);
                if (Driver == null)
                    throw new Exception("Driver is null");

                IOptions manage = Driver.Manage();
                if (manage == null)
                    throw new Exception("manage is null");

                ICookieJar cookieJar = manage.Cookies;
                if (cookieJar == null)
                    throw new Exception("cookieJar is null");

                cookieJar.DeleteCookieNamed(name);
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.Manage().Cookies.DeleteAllCookies()".
        /// </summary>
        public bool DeleteAllCookies()
        {
            try
            {
                LogTrace(Consts.LogStart);
                if (Driver == null)
                    throw new Exception("Driver is null");

                IOptions manage = Driver.Manage();
                if (manage == null)
                    throw new Exception("manage is null");

                ICookieJar cookieJar = manage.Cookies;
                if (cookieJar == null)
                    throw new Exception("cookieJar is null");

                cookieJar.DeleteAllCookies();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.Manage().Cookies.GetCookieNamed()".
        /// </summary>
        public Cookie? GetCookieNamed(string name)
        {
            try
            {
                LogTrace(Consts.LogStart);
                if (Driver == null)
                    throw new Exception("Driver is null");

                IOptions manage = Driver.Manage();
                if (manage == null)
                    throw new Exception("manage is null");

                ICookieJar cookieJar = manage.Cookies;
                if (cookieJar == null)
                    throw new Exception("cookieJar is null");

                return cookieJar.GetCookieNamed(name);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

    } // class

} // namespace

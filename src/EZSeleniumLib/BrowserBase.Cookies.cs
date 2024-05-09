//
// File: "BrowserBase.Cookies.cs"
//
// Summary:
// Supplementary methods for "BrowserBase".
// Wrapper for "WebDriver.Manage().Cookies.*" methods.
//
// Revision History: 
// 2024/05/09:TomislavMatas: Version "4.20.0"
// * Upgrade "Selenium" libs to version "4.20.0".
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
                Log.Debug(DEBUG_START);
                if (Driver == null)
                    throw new Exception("Driver is null");

                IOptions manage = Driver.Manage();
                if (manage == null)
                    throw new Exception("manage is null");

                ICookieJar cookieJar = manage.Cookies;
                if (cookieJar == null)
                    throw new Exception(nameof(cookieJar)+" is null");

                return cookieJar.AllCookies;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new ReadOnlyCollection<Cookie>(new List<Cookie>());
            }
            finally
            {
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.Manage().Cookies.AddCookie()".
        /// </summary>
        public bool AddCookie(Cookie cookie)
        {
            try
            {
                Log.Debug(DEBUG_START);
                if (Driver == null)
                    throw new Exception("Driver is null");

                IOptions manage = Driver.Manage();
                if (manage == null)
                    throw new Exception("manage is null");

                ICookieJar cookieJar = manage.Cookies;
                if (cookieJar == null)
                    throw new Exception(nameof(cookieJar)+" is null");

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
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.Manage().Cookies.DeleteCookie()".
        /// </summary>
        public bool DeleteCookie(Cookie cookie)
        {
            try
            {
                Log.Debug(DEBUG_START);
                if (Driver == null)
                    throw new Exception("Driver is null");

                IOptions manage = Driver.Manage();
                if (manage == null)
                    throw new Exception("manage is null");

                ICookieJar cookieJar = manage.Cookies;
                if (cookieJar == null)
                    throw new Exception(nameof(cookieJar)+" is null");

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
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.Manage().Cookies.DeleteCookieNamed()".
        /// </summary>
        public bool DeleteCookieNamed(string name)
        {
            try
            {
                Log.Debug(DEBUG_START);
                if (Driver == null)
                    throw new Exception("Driver is null");

                IOptions manage = Driver.Manage();
                if (manage == null)
                    throw new Exception("manage is null");

                ICookieJar cookieJar = manage.Cookies;
                if (cookieJar == null)
                    throw new Exception(nameof(cookieJar)+" is null");

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
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.Manage().Cookies.DeleteAllCookies()".
        /// </summary>
        public bool DeleteAllCookies()
        {
            try
            {
                Log.Debug(DEBUG_START);
                if (Driver == null)
                    throw new Exception("Driver is null");

                IOptions manage = Driver.Manage();
                if (manage == null)
                    throw new Exception("manage is null");

                ICookieJar cookieJar = manage.Cookies;
                if (cookieJar == null)
                    throw new Exception(nameof(cookieJar)+" is null");

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
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.Manage().Cookies.GetCookieNamed()".
        /// </summary>
        public Cookie? GetCookieNamed(string name)
        {
            try
            {
                Log.Debug(DEBUG_START);
                if (Driver == null)
                    throw new Exception("Driver is null");

                IOptions manage = Driver.Manage();
                if (manage == null)
                    throw new Exception("manage is null");

                ICookieJar cookieJar = manage.Cookies;
                if (cookieJar == null)
                    throw new Exception(nameof(cookieJar)+" is null");

                return cookieJar.GetCookieNamed(name);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
            finally
            {
                Log.Debug(DEBUG_DONE);
            }
        }

    } // class

} // namespace

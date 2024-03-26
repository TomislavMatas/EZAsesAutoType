//
// File: "BrowserBase.Navigate.cs"
//
// Summary:
// Supplementary methods for "BrowserBase".
// Implementation for wrapper methods related to
// "Navigation" in general.
//
// Revision History: 
// 2024/03/24:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using System;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace EZSeleniumLib
{
    public abstract partial class BrowserBase
    {
        /// <summary>
        /// Basic validation of given url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool IsValidURI(string url)
        {
            try
            {
                Log.Debug(DEBUG_START);

                if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
                    return false;

//                if (uriResult.Scheme != Uri.UriSchemeHttps)
//                    return false;

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.Url = url;".
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool SetUrl(string url)
        {
            try
            {
                Log.Debug(DEBUG_START);

                if (String.IsNullOrEmpty(url))
                    throw new ArgumentNullException("url");

                if (!IsValidURI(url))
                    throw new ArgumentException("url is not a valid URI");

                if (Driver == null)
                    throw new Exception("Driver is null");

                Driver.Url = url;

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Thread.Sleep(this.GetDelay());
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Wrapper for "return Driver.Url;".
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            try
            {
                Log.Debug(DEBUG_START);

                if (Driver == null)
                    throw new Exception("Driver is null");

                return Driver.Url;
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
        /// Wrapper for "Driver.Navigate().GoToUrl(url);".
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool GoToUrl(string url)
        {
            try
            {
                Log.Debug(DEBUG_START);

                if (String.IsNullOrEmpty(url))
                    throw new ArgumentNullException("url");

                if (!IsValidURI(url))
                    throw new ArgumentException("url is not a valid URL");

                if (Driver == null)
                    throw new Exception("Driver is null");

                Log.Debug(String.Format("url={0}", url));
                Driver.Navigate().GoToUrl(url);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Thread.Sleep(this.GetDelay());
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Moves the mouse to the specified element. 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool MoveToElement(IWebElement element)
        {
            try
            {
                Log.Debug(DEBUG_START);
                Actions actions = new Actions(Driver);
                actions.MoveToElement(element).Perform();
                Thread.Sleep(500);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Thread.Sleep(this.GetDelay());
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Click the specified element. 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool ClickElement(IWebElement element)
        {
            try
            {
                Log.Debug(DEBUG_START);
                Actions actions = new Actions(Driver);
                actions.MoveToElement(element).Click().Build().Perform();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Thread.Sleep(this.GetDelay());
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Wrapper for "wait.Until(ExpectedConditions.AlertIsPresent())".
        /// </summary>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public IAlert WaitUntilAlertIsPresent(int timeoutInSeconds)
        {
            try
            {
                Log.Debug(DEBUG_START);

                if (Driver == null)
                    throw new Exception("Driver is null");

                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
                if (alert == null)
                    throw new Exception("alert is null");

                alert = Driver.SwitchTo().Alert();

                return alert;

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

        public bool AlertAccept(IAlert alert)
        {
            try
            {
                Log.Debug(DEBUG_START);

                if (Driver == null)
                    throw new Exception("Driver is null");

                // first chance fallback
                if (alert == null)
                    alert = Driver.SwitchTo().Alert();

                if (alert == null)
                    throw new Exception("alert is null");

                alert.Accept();

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
            finally
            {
                Thread.Sleep(this.GetDelay());
                Log.Debug(DEBUG_DONE);
            }
        }

        public bool AlertDismiss(IAlert alert)
        {
            try
            {
                Log.Debug(DEBUG_START);

                if (Driver == null)
                    throw new Exception("Driver is null");

                // first chance fallback
                if (alert == null)
                    alert = Driver.SwitchTo().Alert();

                if (alert == null)
                    throw new Exception("alert is null");

                alert.Dismiss();

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
            finally
            {
                Thread.Sleep(this.GetDelay());
                Log.Debug(DEBUG_DONE);
            }
        }

    } // class

} // namespace

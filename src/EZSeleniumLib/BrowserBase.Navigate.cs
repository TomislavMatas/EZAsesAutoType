﻿//
// File: "BrowserBase.Navigate.cs"
//
// Summary:
// Supplementary methods for "BrowserBase".
// Implementation for wrapper methods related to
// "Navigation" in general.
//
// Revision History: 
// 2024/05/31:TomislavMatas: Version "4.21.1"
// * Simplify log4net implementations.
// 2024/05/09:TomislavMatas: Version "4.20.0"
// * Firefox enhancement in "ClickElement()": 
//   Use "actions.Click(element).Perform()" instead of "element.Click()".
// 2024/05/04:TomislavMatas: Version "4.20.0"
// * Upgrade to .NET version 8.
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

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
                LogTrace(Consts.LogStart);

                if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult))
                    return false;

                if (uriResult == null)
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
                LogTrace(Consts.LogDone);
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
                LogTrace(Consts.LogStart);

                if (String.IsNullOrEmpty(url))
                    throw new ArgumentNullException("url");

                if (!IsValidURI(url))
                    throw new ArgumentException("url is not a valid URI");

                if (Driver == null)
                    throw new Exception("Driver is null");

                Driver.Url = url;

                Thread.Sleep(this.GetDelay());
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for "return Driver.Url;".
        /// </summary>
        /// <returns></returns>
        public string? GetUrl()
        {
            try
            {
                LogTrace(Consts.LogStart);

                if (this.Driver == null)
                    throw new Exception(nameof(this.Driver)+Consts.LogIsNull);

                return this.Driver.Url;
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
        /// Wrapper for "Driver.Navigate().GoToUrl(url);".
        /// If "timeoutInSeconds > 0", than change the 
        /// current "PageLoad" timeout temporarialy and revert 
        /// it back upun completion.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool GoToUrl(string url, int timeoutInSeconds = 0)
        {
            TimeSpan revert = TimeSpan.Zero;
            try
            {
                LogTrace(Consts.LogStart);
                Log.Debug(String.Format("url={0},timeout={1}",url, timeoutInSeconds));
                if (String.IsNullOrEmpty(url))
                    throw new ArgumentNullException("url");

                if (!IsValidURI(url))
                    throw new ArgumentException("url is not a valid URL");

                if (Driver == null)
                    throw new Exception("Driver is null");

                if (timeoutInSeconds > 0)
                {
                    revert = this.Driver.Manage().Timeouts().PageLoad;
                    this.Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(timeoutInSeconds);
                }

                Driver.Navigate().GoToUrl(url);

                Thread.Sleep(this.GetDelay());
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                if (revert != TimeSpan.Zero)
                    if(this.Driver != null)
                        this.Driver.Manage().Timeouts().PageLoad = revert;

                LogTrace(Consts.LogDone);
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
                LogTrace(Consts.LogStart);
                if (!this.GoToUrl(url,0))
                    throw new Exception(nameof(this.GoToUrl) + Consts.LogFail);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
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
                LogTrace(Consts.LogStart);
                Actions actions = new Actions(Driver);
                actions.MoveToElement(element).Perform();
                Thread.Sleep(this.GetDelay());
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
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
                LogTrace(Consts.LogStart);
                if(element==null)
                    throw new ArgumentNullException(nameof(element)); 

                object? result = this.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                Actions actions = new Actions(Driver);
                actions.MoveToElement(element).Perform();
                Thread.Sleep(this.GetDelay());
                actions.Click(element).Perform();
                Thread.Sleep(this.GetDelay());
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for "wait.Until(ExpectedConditions.AlertIsPresent())".
        /// </summary>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public IAlert? WaitUntilAlertIsPresent(int timeoutInSeconds)
        {
            try
            {
                LogTrace(Consts.LogStart);
                if (Driver == null)
                    throw new Exception("Driver is null");

                // Selenium v3 did provide "ExpectedConditions.AlertIsPresent()" implementation.
                // WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                // IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
                // Selenium v4 does not even have the class "ExpectedConditions" any more, make custom culprit.
                int secondsElapsed = 0;
                while (secondsElapsed < timeoutInSeconds)
                {
                    try
                    {
                        IAlert? alert = Driver.SwitchTo().Alert();
                        if (alert != null)
                            return alert; // SUCCESS!

                        Log.Debug("Driver.SwitchTo().Alert()" + Consts.LogIsNull);
                    }
                    catch (Exception ex) 
                    {
                        Log.Debug(ex);
                    }
                    Thread.Sleep(1000); // 1000 ms = one second
                    secondsElapsed++;
                    if (secondsElapsed >= timeoutInSeconds)
                        throw new Exception(nameof(WaitUntilAlertIsPresent) + Consts.LogTimeout);

                }
                return null;
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

        public bool AlertAccept(IAlert alert)
        {
            try
            {
                LogTrace(Consts.LogStart);

                if (Driver == null)
                    throw new Exception("Driver is null");

                // first chance fallback
                if (alert == null)
                    alert = Driver.SwitchTo().Alert();

                if (alert == null)
                    throw new Exception("alert is null");

                alert.Accept();

                Thread.Sleep(this.GetDelay());
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

        public bool AlertDismiss(IAlert alert)
        {
            try
            {
                LogTrace(Consts.LogStart);

                if (Driver == null)
                    throw new Exception("Driver is null");

                // first chance fallback
                if (alert == null)
                    alert = Driver.SwitchTo().Alert();

                if (alert == null)
                    throw new Exception("alert is null");

                alert.Dismiss();

                Thread.Sleep(this.GetDelay());
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

    } // class

} // namespace

//
// File: BrowserBase.cs
//
// Summary:
// Base class used for abstract browser implementations 
// using Selenium's WebDriver Classes.
// Place wrapper for the abastract Selenium WebDriver 
// helper methods here and implmenent in respective descendents.
//
// Notes:
// "OpenQA.Selenium.Support.UI.ExpectedConditions" has been deprecated, see details on
// -->< https://stackoverflow.com/questions/49866334/c-sharp-selenium-expectedconditions-is-obsolete >
// There for, the fork "DotNetSeleniumExtras.WaitHelpers" has been added to this project using NuGet.
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

using System;
using System.Collections.ObjectModel;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

using log4net;
using OpenQA.Selenium.Interactions;
using System.Threading;

using LibConfig = EZSeleniumLib.ConfigSettings;

namespace EZSeleniumLib
{
    /// <summary>
    /// Base class used for specific browser implementations 
    /// using Selenium's WebDriver Classes.
    /// Provides wrapper methods for the most relevant 
    /// relevant Selenium WebDriver methods, which in turn can
    /// be extended within specifc descendants - if needed.
    /// </summary>
    public abstract class BrowserBase
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(BrowserBase));
                return m_Log;
            }
        }
        #endregion

        #region configuration valuez
        private int? m_BrowserInteractionDelay = null;
        private int BrowserInteractionDelay
        {
            get
            {
                if (this.m_BrowserInteractionDelay == null)
                    this.m_BrowserInteractionDelay = ConfigSettings.GetBrowserInteractionDelay();
                return (int)this.m_BrowserInteractionDelay;
            }
            set
            {
                this.m_BrowserInteractionDelay = value;
            }
        }
        public int GetBrowserInteractionDelay()
        {
            return this.BrowserInteractionDelay;
        }
        public int SetBrowserInteractionDelay(int value)
        {
            int prev = this.GetBrowserInteractionDelay();
            this.BrowserInteractionDelay = value;
            return prev;
        }
        #endregion 

        #region protected memberz

        protected RemoteWebDriver? m_Driver = null;
        protected DriverService? m_Service = null;

        protected bool m_EnablePopups = false;
        protected bool m_EnableNotifications = false;

        #endregion 

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BrowserBase()
        {
            try
            {
                Log.Debug(Const.LogStart);
                m_EnablePopups = false;
                m_EnableNotifications = false;
                if (!Initialize())
                    throw new Exception(nameof(Initialize) + Const.LogFail);
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

        /// <summary>
        /// Custom constructor.
        /// </summary>
        public BrowserBase(bool enablePopups, bool enableNotifications)
        {
            try
            {
                Log.Debug(Const.LogStart);
                m_EnablePopups = enablePopups;
                m_EnableNotifications = enableNotifications;
                if (!Initialize())
                    throw new Exception(nameof(Initialize) + Const.LogFail);
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

        /// <summary>
        /// Default destructor.
        /// </summary>
        ~BrowserBase()
        {
            try
            {
                Log.Debug(Const.LogStart);
                if(!Cleanup())
                    throw new Exception(nameof(Cleanup) + Const.LogFail);
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

        /// <summary>
        /// Initialize specific browser instance.
        /// Must be implement in descandants.
        /// </summary>
        /// <returns></returns>
        public abstract bool Initialize();

        /// <summary>
        /// Graceful teardown of browser instance.
        /// </summary>
        /// <returns></returns>
        public bool Cleanup()
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (m_Driver != null)
                {
                    m_Driver.Close();
                    m_Driver.Quit();
                    m_Driver.Dispose();
                    m_Driver = null;
                }
                if (m_Service != null)
                {
                    m_Service.Dispose();
                    m_Service = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for "m_Driver.Url = url;".
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool SetUrl(string url)
        {
            try
            {
                Log.Debug(Const.LogStart);

                if (String.IsNullOrEmpty(url))
                    throw new ArgumentNullException(nameof(url));

                if (!IsValidURI(url))
                    throw new ArgumentException(nameof(url) + Const.LogInvalid);

                if (m_Driver == null)
                    throw new Exception(nameof(m_Driver) + Const.LogIsNull);

                m_Driver.Url = url;

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for "return m_Driver.Url;".
        /// </summary>
        /// <returns></returns>
        public string? GetUrl()
        {
            try
            {
                Log.Debug(Const.LogStart);

                if (m_Driver == null)
                    throw new Exception(nameof(m_Driver) + Const.LogIsNull);

                return m_Driver.Url;
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
        /// Basic validation of given url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidURI(string url)
        {
            try
            {
                Log.Debug(Const.LogStart);

                if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult))
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
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for "m_Driver.Navigate().GoToUrl(url);".
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool GoToUrl(string url)
        {
            try
            {
                Log.Debug(Const.LogStart);

                if (String.IsNullOrEmpty(url))
                    throw new ArgumentNullException(nameof(url));

                if (!IsValidURI(url))
                    throw new ArgumentException(nameof(url) + Const.LogInvalid);

                if (m_Driver == null)
                    throw new Exception(nameof(m_Driver) + Const.LogIsNull);

                Log.Debug(String.Format("url={0}",url));
                m_Driver.Navigate().GoToUrl(url);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        /// Wrapper for "m_Driver.FindElement(by);".
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public IWebElement? FindElement(By by)
        {
            return this.FindElement(by, timeoutInSeconds: 0);
        }

        /// <summary>
        /// Wrapper for "m_Driver.FindElement(By.XPath(xPath));".
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public IWebElement? FindElement(string xPath)
        {
            try
            {
                Log.Debug(Const.LogStart);
                Log.Debug(String.Format("xPath={0}", xPath));
                return this.FindElement(By.XPath(xPath), timeoutInSeconds: 0);
            }
            catch( OpenQA.Selenium.WebDriverTimeoutException)
            {
                Log.Debug("WebDriverTimeout");
                return null;
            }
            catch ( OpenQA.Selenium.NoSuchElementException)
            {
                Log.Debug("NoSuchElement");
                return null;
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
        /// Wrapper for "m_Driver.FindElement(By.XPath(xPath));".
        /// If "timeoutInSeconds" has a value > 0, 
        /// a sepcific "WebDriverWait.Until()" call will be added.
        /// </summary>
        /// <param name="xPath"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public IWebElement? FindElement(string xPath, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                Log.Debug(String.Format("xPath={0}", xPath));
                return this.FindElement(By.XPath(xPath), timeoutInSeconds);
            }
            catch(OpenQA.Selenium.WebDriverTimeoutException)
            {
                Log.Debug("WebDriverTimeout");
                return null;
            }
            catch(OpenQA.Selenium.NoSuchElementException)
            {
                Log.Debug("NoSuchElement");
                return null;
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
        /// Wrapper for "m_Driver.FindElement(by);".
        /// If "timeoutInSeconds" has a value > 0, 
        /// a sepcific "WebDriverWait.Until()" call will be added.
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public IWebElement? FindElement( By by, int timeoutInSeconds )
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (m_Driver == null)
                    throw new Exception(nameof(m_Driver) + Const.LogIsNull);

                if (timeoutInSeconds > 0)
                {
                    WebDriverWait wait = new WebDriverWait(m_Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    wait.IgnoreExceptionTypes(typeof(UnhandledAlertException));
                    wait.IgnoreExceptionTypes(typeof(WebDriverException));
                    return wait.Until(drv => drv.FindElement(by));
                }
                return m_Driver.FindElement(by);
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException)
            {
                Log.Debug("WebDriverTimeout");
                return null;
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                Log.Debug("NoSuchElement");
                return null;
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
        /// Wrapper for "m_Driver.FindElements(by);".
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public ReadOnlyCollection<IWebElement>? FindElements(By by)
        {
            return this.FindElements(by, timeoutInSeconds: 0);
        }

        /// <summary>
        /// Wrapper for "m_Driver.FindElements(by);".
        /// If "timeoutInSeconds" has a value > 0, 
        /// a sepcific "WebDriverWait.Until()" call will be added.
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public ReadOnlyCollection<IWebElement>? FindElements(By by, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (m_Driver == null)
                    throw new Exception(nameof(m_Driver) + Const.LogIsNull);

                if (timeoutInSeconds > 0)
                {
                    WebDriverWait wait = new WebDriverWait(m_Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    wait.IgnoreExceptionTypes(typeof(UnhandledAlertException));
                    wait.IgnoreExceptionTypes(typeof(WebDriverException));
                    return wait.Until(drv => drv.FindElements(by));
                }
                return m_Driver.FindElements(by);
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException)
            {
                Log.Debug("WebDriverTimeout");
                return null;
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                Log.Debug("NoSuchElement");
                return null;
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
        /// Moves the mouse to the specified element. 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool MoveToElement(IWebElement element)
        {
            try
            {
                Log.Debug(Const.LogStart);
                Actions actions = new Actions(m_Driver);
                actions.MoveToElement(element).Perform();
                int delay = this.GetBrowserInteractionDelay();
                Thread.Sleep(delay);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(Const.LogDone);
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
                Log.Debug(Const.LogStart);
                Actions actions = new Actions(m_Driver);
                actions.MoveToElement(element).Click().Perform();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(Const.LogDone);
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
                Log.Debug(Const.LogStart);

                if (m_Driver == null)
                    throw new Exception(nameof(m_Driver) + Const.LogIsNull);

                WebDriverWait wait = new WebDriverWait(m_Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
                if (alert == null)
                    throw new Exception(nameof(alert) + Const.LogIsNull);

                alert = m_Driver.SwitchTo().Alert();

                return alert;

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

        public bool AlertAccept(IAlert alert)
        {
            try
            {
                Log.Debug(Const.LogStart);

                if (m_Driver == null)
                    throw new Exception(nameof(m_Driver) + Const.LogIsNull);

                // first chance fallback
                if (alert == null)
                    alert = m_Driver.SwitchTo().Alert();

                if (alert == null)
                    throw new Exception(nameof(alert) + Const.LogIsNull);

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
                Log.Debug(Const.LogDone);
            }
        }

        public bool AlertDismiss(IAlert alert)
        {
            try
            {
                Log.Debug(Const.LogStart);

                if (m_Driver == null)
                    throw new Exception(nameof(m_Driver) + Const.LogIsNull);

                // first chance fallback
                if (alert == null)
                    alert = m_Driver.SwitchTo().Alert();

                if (alert == null)
                    throw new Exception(nameof(alert) + Const.LogIsNull);

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
                Log.Debug(Const.LogDone);
            }
        }

    } // class

} // namespace

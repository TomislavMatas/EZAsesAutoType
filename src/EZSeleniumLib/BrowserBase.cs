//
// File: "BrowserBase.cs"
//
// Summary:
// Base class used for abstract browser implementations 
// using Selenium's WebDriver Classes.
// Place wrapper for the abstract Selenium WebDriver 
// helper methods here and implmenent in respective descendents.
//
// Notes:
// "OpenQA.Selenium.Support.UI.ExpectedConditions" has been deprecated, see details on
// -->< https://stackoverflow.com/questions/49866334/c-sharp-selenium-expectedconditions-is-obsolete >
// There for, the fork "DotNetSeleniumExtras.WaitHelpers" has been added to this project using NuGet.
//
// Revision History: 
// 2024/03/24:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

using System.Collections.ObjectModel;

using log4net;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace EZSeleniumLib
{
    /// <summary>
    /// Base class used for specific browser implementations 
    /// using Selenium's WebDriver Classes.
    /// Provides wrapper methods for the most relevant 
    /// Selenium WebDriver methods, which in turn can
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
        private string? m_WebDriver = null;
        private string WebDriver
        {
            get
            {
                if (this.m_WebDriver == null)
                    this.m_WebDriver = ConfigSettings.GetWebDriver();
                return this.m_WebDriver;
            }
            set
            {
                this.m_WebDriver = value;
            }
        }
        public string GetWebDriver()
        {
            return this.WebDriver;
        }
        public string SetWebDriver(string value)
        {
            string prev = this.GetWebDriver();
            this.WebDriver = value;
            return prev;
        }

        private string? m_WebDriverInitMode = null;
        private string WebDriverInitMode
        {
            get
            {
                if (m_WebDriverInitMode == null)
                    m_WebDriverInitMode = ConfigSettings.GetWebDriverInitMode();
                return m_WebDriverInitMode;
            }
            set
            {
                m_WebDriverInitMode = value;
            }
        }
        public string GetWebDriverInitMode()
        {
            return this.WebDriverInitMode;
        }
        public string SetWebDriverInitMode(string value)
        {
            string prev = this.GetWebDriverInitMode();
            this.WebDriverInitMode = value;
            return prev;
        }

        private int? m_WebDriverDelay = null;
        private int WebDriverDelay
        {
            get
            {
                if (this.m_WebDriverDelay == null)
                    this.m_WebDriverDelay = ConfigSettings.GetWebDriverDelay();
                return (int)this.m_WebDriverDelay;
            }
            set
            {
                this.m_WebDriverDelay = value;
            }
        }
        public int GetWebDriverDelay()
        {
            return this.WebDriverDelay;
        }
        public int SetWebDriverDelay(int value)
        {
            int prev = this.GetWebDriverDelay();
            this.WebDriverDelay = value;
            return prev;
        }

        protected bool m_EnablePopups = false; // #TODO: implement config get/set
        protected bool m_EnableNotifications = false; // #TODO: implement config get/set 

        #endregion 

        #region protected memberz

        protected RemoteWebDriver? m_Driver = null;
        protected DriverService? m_Service = null;


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
        /// Initialize specific WebDriver instance.
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            try
            {
                Log.Debug(Const.LogStart);

                string initMode = this.GetWebDriverInitMode();
                if (string.IsNullOrEmpty(initMode))
                    throw new Exception(nameof(initMode) + Const.LogInvalid);

                if (Constant.WebDriverInitModeSimple.Equals(initMode, StringComparison.OrdinalIgnoreCase))
                    return this.InitializeSimple();

                if (Constant.WebDriverInitModeExtended.Equals(initMode, StringComparison.OrdinalIgnoreCase))
                    return InitializeExtended();

                throw new Exception(nameof(initMode) + Const.LogNotImpl);
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

        #region abstrct methodz
        public abstract bool InitializeExtended();
        public abstract bool InitializeSimple();
        #endregion

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

                Log.Debug(string.Format("url={0}",url));
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
                Log.Debug(string.Format("xPath={0}", xPath));
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
                Log.Debug(string.Format("xPath={0}", xPath));
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
                int delay = this.GetWebDriverDelay();
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

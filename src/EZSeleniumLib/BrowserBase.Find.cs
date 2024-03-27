//
// File: "BrowserBase.Find.cs"
//
// Summary:
// Supplementary methods for "BrowserBase".
// Implementation of wrappers for "WebDriver.Find*" methods.
//
// Revision History: 
// 2024/03/24:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using System;
using System.Collections.ObjectModel;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EZSeleniumLib
{
    public abstract partial class BrowserBase
    {
        /// <summary>
        /// Wrapper for "Driver.FindElement(by);".
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public IWebElement FindElement(By by)
        {
            return this.FindElement(by, timeoutInSeconds: 0);
        }

        /// <summary>
        /// Wrapper for "Driver.FindElement(By.XPath(xPath));".
        /// </summary>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public IWebElement FindElement(string xPath)
        {
            try
            {
                Log.Debug(DEBUG_START);
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
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.FindElement(By.XPath(xPath));".
        /// If "timeoutInSeconds" has a value > 0, 
        /// a sepcific "WebDriverWait.Until()" call will be added.
        /// </summary>
        /// <param name="xPath"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public IWebElement FindElement(string xPath, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(DEBUG_START);
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
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.FindElement(by);".
        /// If "timeoutInSeconds" has a value > 0, 
        /// a sepcific "WebDriverWait.Until()" call will be added.
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public IWebElement FindElement( By by, int timeoutInSeconds )
        {
            try
            {
                Log.Debug(DEBUG_START);
                if (Driver == null)
                    throw new Exception("Driver is null");

                if (timeoutInSeconds > 0)
                {
                    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    wait.IgnoreExceptionTypes(typeof(UnhandledAlertException));
                    wait.IgnoreExceptionTypes(typeof(WebDriverException));
                    return wait.Until(drv => drv.FindElement(by));
                }
                return Driver.FindElement(by);
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
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Wrapper for "Driver.FindElements(by);".
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return this.FindElements(by, timeoutInSeconds: 0);
        }

        /// <summary>
        /// Wrapper for "Driver.FindElements(by);".
        /// If "timeoutInSeconds" has a value > 0, 
        /// a sepcific "WebDriverWait.Until()" call will be added.
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public ReadOnlyCollection<IWebElement> FindElements(By by, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(DEBUG_START);
                if (Driver == null)
                    throw new Exception("Driver is null");

                if (timeoutInSeconds > 0)
                {
                    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    wait.IgnoreExceptionTypes(typeof(UnhandledAlertException));
                    wait.IgnoreExceptionTypes(typeof(WebDriverException));
                    return wait.Until(drv => drv.FindElements(by));
                }
                return Driver.FindElements(by);
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
                Log.Debug(DEBUG_DONE);
            }
        }

    } // class

} // namespace

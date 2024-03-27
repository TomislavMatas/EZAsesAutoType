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

using System.Collections.ObjectModel;

using OpenQA.Selenium;

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
        public IWebElement FindElementByXPath(string xPath)
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
        public IWebElement FindElementByXpath(string xPath, int timeoutInSeconds)
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
        /// Wrapper for "Driver.FindElement(by);" with extension to 
        /// obey a given and specific "timeout".
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public IWebElement FindElement( By by, int timeoutInSeconds )
        {
            TimeSpan revert = TimeSpan.Zero;
            try
            {
                Log.Debug(DEBUG_START);
                if (Driver == null)
                    throw new Exception("Driver is null");

                if (timeoutInSeconds > 0)
                {
                    revert = Driver.Manage().Timeouts().ImplicitWait;
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
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
                if(revert != TimeSpan.Zero)
                    Driver.Manage().Timeouts().ImplicitWait = revert;

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
            TimeSpan revert = TimeSpan.Zero;
            try
            {
                Log.Debug(DEBUG_START);
                if (Driver == null)
                    throw new Exception("Driver is null");

                if (timeoutInSeconds > 0)
                {
                    revert = Driver.Manage().Timeouts().ImplicitWait;
                    Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
                }
                ReadOnlyCollection<IWebElement> elements = Driver.FindElements(by);
                return elements;
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
                if (revert != TimeSpan.Zero)
                    Driver.Manage().Timeouts().ImplicitWait = revert;

                Log.Debug(DEBUG_DONE);
            }
        }

    } // class

} // namespace

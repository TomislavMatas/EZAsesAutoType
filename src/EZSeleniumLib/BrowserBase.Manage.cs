//
// File: "BrowserBase.Manage.cs"
//
// Summary:
// Supplementary methods for "BrowserBase".
// Implementation for wrapper methods derived 
// from "WebDriver.Manage()".
//
// Revision History: 
// 2024/05/09:TomislavMatas: Version "4.20.0"
// * Upgrade "Selenium" libs to version "4.20.0".
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.

using OpenQA.Selenium.Interactions;
using Keys = OpenQA.Selenium.Keys;

namespace EZSeleniumLib
{
    public abstract partial class BrowserBase
    {
        /// <summary>
        /// Enumeration of implemented "Refresh-Methods".
        /// </summary>
        public enum RefreshMethod
        {
            Driver = 0, // kind a default 
            SendKeys = 1
        }

        /// <summary>
        /// Refresh current page using "Driver.Navigate().Refresh()".
        /// </summary>
        private bool RefreshImplDriver()
        {
            try
            {
                Log.Debug(DEBUG_START);
                if (Driver == null)
                    throw new Exception("Driver is null");

                Driver.Navigate().Refresh();
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
        /// Refresh current page by sending "STRG + F5".
        /// Can be done with an one~liner like:
        /// "Actions.KeyDown(Keys.Control).SendKeys(Keys.F5).KeyUp(Keys.Control).Perform()".
        /// To gain more control, the one~liner 
        /// has been split up into it's individual steps.
        /// 
        /// BUT: Unfortunately, not all Control key combinations are 
        /// supported when using ChromeDriver. Thus, even if this
        /// method using SendKeys _might_ hav worked in the past, 
        /// it seems, that current web driver implementation 
        /// does not do any thing with latest web drivers.
        /// </summary>
        private bool RefreshImplSendKeys()
        {
            try
            {
                Log.Debug(DEBUG_START);

                if (Driver == null)
                    throw new Exception("Driver is null");

                Actions actions = new Actions(Driver);
                actions.KeyDown(Keys.Control);
                actions.SendKeys(Keys.F5);
                actions.KeyUp(Keys.Control);
                actions.Perform();
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
        /// Refresh current page using a specific "RefreshMethod".
        /// </summary>
        public bool Refresh(RefreshMethod method)
        {
            try
            {
                Log.Debug(DEBUG_START);
                bool result = false;
                switch (method)
                {
                    case RefreshMethod.Driver:
                        result = this.RefreshImplDriver();
                        break;

                    case RefreshMethod.SendKeys:
                        result = this.RefreshImplSendKeys();
                        break;

                    default:
                        result = this.RefreshImplDriver();
                        break;
                }

                return result;
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
        /// Refresh current page using the default "RefreshMethod.mDriver" implementation.
        /// </summary>
        public bool Refresh()
        {
            try
            {
                Log.Debug(DEBUG_START);
                return this.Refresh(RefreshMethod.Driver);
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

    } // class

} // namespace

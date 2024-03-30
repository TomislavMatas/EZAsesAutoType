//
// File: "Worker.Navigation.cs"
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using System.Collections.Specialized;

using EZSeleniumLib;
using OpenQA.Selenium;

namespace EZAsesAutoType
{
    internal partial class Worker
    {
        #region Navigation 

        /// <summary>
        /// Use Browser-Interop Helper to navigate to "Login Page".
        /// Uses a single "browser.GoToUrl()" call.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESNavigateToLoginPage(BrowserBase browser, string baseUrl, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                if (baseUrl == null)
                    throw new ArgumentNullException(nameof(baseUrl));

                if (!browser.GoToUrl(baseUrl, timeoutInSeconds))
                    throw new Exception(nameof(browser.GoToUrl) + Const.LogFail);

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
        /// Use Browser-Interop Helper to switch to the 
        /// iFrame element that "hosts" the application.
        /// The entire ASES Application runs in an iFrame.
        /// In order to make ASES controled by Selenium,
        /// need to switch to that specific iFrame first,
        /// before _any_ other FindElement will work as expected.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="iFrameXPath"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESSwitchToIFrame(BrowserBase browser, string iFrameXPath, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                if (iFrameXPath == null)
                    throw new ArgumentNullException(nameof(iFrameXPath));

                IWebElement iFrameElement = browser.FindElementByXpath(iFrameXPath, timeoutInSeconds);
                if (iFrameElement == null)
                    throw new Exception(String.Format("'{0}'{1}", iFrameXPath, Const.LogNotFound));

                IWebDriver iFrameDriver = browser.SwitchToIFrame(iFrameElement);
                if (iFrameDriver == null)
                    throw new Exception(nameof(browser.SwitchToIFrame) + Const.LogFail);

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
        /// Use Browser-Interop Helper to perform login.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESDoLogin(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                string xPath = this.GetLoginPageUsernameXPath();
                IWebElement element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (!browser.MoveToElement(element))
                    throw new Exception(nameof(browser.MoveToElement) + Const.LogFail);
                element.SendKeys(this.GetASESUserId());

                xPath = this.GetLoginPagePasswordXPath();
                element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (!browser.MoveToElement(element))
                    throw new Exception(nameof(browser.MoveToElement) + Const.LogFail);
                element.SendKeys(this.GetASESPassword());

                if (!ASESUrlContainsClienNoParm(this.GetASESBaseUrl()))
                {   // Client DropDown is only editable, when login
                    // baseUrl does not contain a clientno parm.
                    string client = this.GetASESClient();
                    if (!string.IsNullOrEmpty(client))
                    {   // considered "optional"
                        xPath = this.GetLoginPageClientXPath();
                        element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                        if (browser.MoveToElement(element))
                            element.SendKeys(client);
                    }
                }

                string langauge = this.GetASESLanguage();
                if (!string.IsNullOrEmpty(langauge))
                {   // considered "optional"
                    xPath = this.GetLoginPageLanguageXPath();
                    element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                    if (browser.MoveToElement(element))
                        element.SendKeys(langauge);
                }

                xPath = this.GetLoginPageLoginButtonXPath();
                element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (browser.MoveToElement(element))
                    if (!browser.ClickElement(element))
                        throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

                if (!WaitUntilMainPageHasLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(WaitUntilMainPageHasLoaded) + Const.LogFail);

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
        /// Use Browser-Interop Helper to navigate to "Time Entry Grid Canvas".
        /// In Essence: Click the respective menu elements in coorect order.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESNavigateToTimeEntryCanvas(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                if (!this.ASESMainPageIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESMainPageIsLoaded) + Const.LogFail);

                // this extra sleep is required, because the splash
                // image is moved by an "animation" effect - which
                // invalidates the DOM for a a while : - (
                Thread.Sleep(1000);

                string xPath = this.GetNavMenuXPath();
                IWebElement element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));
                if(!browser.ClickElement(element))
                    throw new Exception(nameof(browser.ClickElement) + Const.LogFail);


                xPath = this.GetNavMenuZeitbuchungXPath();
                element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));
                if (!browser.ClickElement(element))
                    throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

                Thread.Sleep(5000);
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

        private bool CancelRequested()
        {
            return Global.GetCancelRequested();
        }

        /// <summary>
        /// Use Browser-Interop Helper to perform the daily 
        /// "punch in / punch out" routine.
        /// </summary>
        /// <returns></returns>
        public bool DoDailyPunch()
        {
            BrowserBase? browser = null;
            try
            {
                Log.Debug(Const.LogStart);
                string webDriver = this.GetWebDriver(); ;
                BrowserOptions browserOptionsDefault = this.GetBrowserOptionsDefault();
                browser = this.GetBrowserInstance(webDriver, browserOptionsDefault);
                if (browser == null)
                    throw new Exception(nameof(this.GetBrowserInstance) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceld);

                string baseUrl = this.GetASESBaseUrl();
                int timeoutLoginPage = this.GetTimeoutNavigationLoginPage();
                if (!this.ASESNavigateToLoginPage(browser, baseUrl, timeoutLoginPage))
                    throw new Exception(nameof(this.ASESNavigateToLoginPage) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceld);

                string iFrameXPath = this.GetApplicationIFrameXPath();
                int timeoutFindElement = this.GetTimeoutFindElement();
                if (!this.ASESSwitchToIFrame(browser, iFrameXPath, timeoutFindElement))
                    throw new Exception(nameof(this.ASESSwitchToIFrame) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceld);

                if (!this.ASESLoginPageIsLoaded(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESLoginPageIsLoaded) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceld);

                if (!this.ASESDoLogin(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESDoLogin) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceld);

                if (!this.ASESNavigateToTimeEntryCanvas(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESNavigateToTimeEntryCanvas) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceld);

                if (!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                if (browser != null)
                {
                    browser.Cleanup();
                    browser = null;
                }
                Log.Debug(Const.LogDone);
            }
        }

        #endregion Navigation 

    } // class

} // namespace

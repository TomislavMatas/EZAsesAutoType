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
        /// In Essence: Click the respective menu elements in correct order.
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
                // image is moved by an "animation effect" which
                // invalidates the DOM for a while : - (
                Thread.Sleep(1000);

                string xPath = this.GetNavMenuXPath();
                IWebElement element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));
                if(!browser.ClickElement(element))
                    throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

                if(!this.WaitUntilNavMenuPopupHasLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.WaitUntilNavMenuPopupHasLoaded) + Const.LogFail);

                xPath = this.GetNavMenuZeitbuchungXPath();
                element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));
                if (!browser.ClickElement(element))
                    throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

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
        /// Use Browser-Interop Helper to navigate to sort the "Time Entry Grid Canvas" ascending.
        /// In Essence: Click the respective header item.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESSortTimeEntryCanvasAscending(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                if (!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                if (!this.ASESTimeEntryCanvasIsSortedAscending(browser, timeoutInSeconds))
                {
                    if (this.ASESTimeEntryCanvasIsSortedDescending(browser, timeoutInSeconds))
                    {
                        const string subProcess = "Set sorting state ascending";
                        Log.Info(subProcess + Const.LogInProgress);
                        string sortindicatorXPath = this.GetTimeGridCanvasSortingDescXPath();
                        IWebElement sortindicatorElement = browser.FindElementByXpath(sortindicatorXPath, timeoutInSeconds);
                        if (sortindicatorElement == null)
                            throw new Exception(string.Format("'{0}'{1}", sortindicatorXPath, Const.LogNotFound));
                        if (!browser.ClickElement(sortindicatorElement))
                            throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

                        // sleep a while before validating succes of "sort" operation
                        Thread.Sleep(1000);

                        if(!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutInSeconds))
                            throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                        if (!this.ASESTimeEntryCanvasIsSortedAscending(browser, timeoutInSeconds))
                            throw new Exception(subProcess + Const.LogFail);
                        
                        Log.Info(subProcess + Const.LogDone);
                    }
                    else
                    {
                        throw new Exception("SortingState"+Const.LogInvalid);
                    }
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
        /// Use Browser-Interop Helper to open up "Time Pair Entry" popup dialog.
        /// In Essence: Assuming, the grid is sorted ascending by date,
        /// then an item in the grid's last row must be clicked.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESOpenTimePairEntryPopup(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                if (!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                string xPath = this.GetTimeGridCanvasLastRowDateFromXPath();
                IWebElement element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));
                if (!browser.ClickElement(element))
                    throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

                // first click on "span" element activates "edit mode" by
                // embedding a "input" control. Need to click a second time
                // but only if that "input" control has appeared
                Thread.Sleep(1000);
                xPath = xPath + "/input";
                IWebElement input = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (input == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));

                if (!browser.ClickElement(input))
                    throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

                if (!this.WaitUntilTimePairEntryPopupHasLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.WaitUntilTimePairEntryPopupHasLoaded) + Const.LogFail);

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
        /// Use Browser-Interop Helper to "type" the "in/out Time Pair" 
        /// within "Time Pair Entry" popup dialog. 
        /// In Essence: When successfully clicked on the respective "span" 
        /// element, it shall activate edit mode by embedding an "input" 
        /// control. Use Thread.Sleep() to give browser some time 
        /// to render that "input" control.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool ASESEnterInOutTimePair(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                if (!this.ASESTimePairEntryPopupIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimePairEntryPopupIsLoaded) + Const.LogFail);

                string xPath= this.GetTimePairFirstRowTimeFromXPath();
                IWebElement element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));

                if (!browser.ClickElement(element))
                    throw new Exception(nameof(browser.MoveToElement) + Const.LogFail);

                Thread.Sleep(1000);
                xPath = xPath + "/input";
                element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));

                string value = this.GetASESPunchIn();
                element.SendKeys(value);

                xPath = this.GetTimePairFirstRowTimeToXPath();
                element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));

                if (!browser.ClickElement(element))
                    throw new Exception(nameof(browser.MoveToElement) + Const.LogFail);

                Thread.Sleep(1000);
                xPath = xPath + "/input";
                element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));

                value = this.GetASESPunchOut();
                element.SendKeys(value);

                xPath = this.GetTimePairFooterAcceptButtonPath();
                element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));

                if (!browser.ClickElement(element))
                    throw new Exception(nameof(browser.MoveToElement) + Const.LogFail);

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
        /// Use Browser-Interop Helper to save the current data.
        /// In Essence: Click the "save button".
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESSaveTimeEntryCanvas(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                if (!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                string xPath = this.GetTimeGridCanvasSaveButtonPath();
                IWebElement element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));
                if (!browser.ClickElement(element))
                    throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

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
        /// After time pair entry, wait for the amount of seconds 
        /// configured in "App.config" to give user the opportunity to review 
        /// the actual data before proceeding with logoout.
        /// Returns true, if the wait time has elapsed without cancelation request.
        /// Otherwise, if user requested to cancel the pending opration, returns false.
        /// </summary>
        /// <returns></returns>
        private bool ASESWaitBeforeLogout()
        {
            try
            {
                Log.Debug(Const.LogStart);
                int waitTimeInSeconds = this.GetWaitBeforeLogout();
                if (!this.CancelableWait(waitTimeInSeconds))
                    throw new Exception(nameof(CancelableWait) + Const.LogCanceled);

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
        /// Use Browser-Interop Helper to save the current data.
        /// In Essence: Click the menu sequence for "log out current user".
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESDoLogout(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                if (!this.ASESMainPageIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESMainPageIsLoaded) + Const.LogFail);

                string xPath = this.GetNavMenuUsernameXPath();
                IWebElement element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));
                if (!browser.ClickElement(element))
                    throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

                Thread.Sleep(1000);
                xPath = this.GetNavMenuUsernameLogoutButtonXPath();
                element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));
                if (!browser.ClickElement(element))
                    throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

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
        /// Use Browser-Interop Helper to perform the daily 
        /// "punch in / punch out" routine.
        /// </summary>
        /// <returns></returns>
        public bool DoDailyPunch()
        {
            BrowserBase? browser = null;
            try
            {
                Log.Info(Const.LogStart);

                Log.Info("Init browser" + Const.LogInProgress);

                string webDriver = this.GetWebDriver(); ;
                BrowserOptions browserOptionsDefault = this.GetBrowserOptionsDefault();
                browser = this.GetBrowserInstance(webDriver, browserOptionsDefault);
                if (browser == null)
                    throw new Exception(nameof(this.GetBrowserInstance) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                string baseUrl = this.GetASESBaseUrl();
                Log.Info(String.Format("Navigate to '{0}'{1}", baseUrl, Const.LogInProgress));
                int timeoutLoginPage = this.GetTimeoutNavigationLoginPage();
                if (!this.ASESNavigateToLoginPage(browser, baseUrl, timeoutLoginPage))
                    throw new Exception(nameof(this.ASESNavigateToLoginPage) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                string iFrameXPath = this.GetApplicationIFrameXPath();
                int timeoutFindElement = this.GetTimeoutFindElement();
                if (!this.ASESSwitchToIFrame(browser, iFrameXPath, timeoutFindElement))
                    throw new Exception(nameof(this.ASESSwitchToIFrame) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                if (!this.ASESLoginPageIsLoaded(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESLoginPageIsLoaded) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                Log.Info("Login" + Const.LogInProgress);
                if (!this.ASESDoLogin(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESDoLogin) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                Log.Info("Open time card" + Const.LogInProgress);
                if (!this.ASESNavigateToTimeEntryCanvas(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESNavigateToTimeEntryCanvas) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                if (!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                if (!this.ASESSortTimeEntryCanvasAscending(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESSortTimeEntryCanvasAscending) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                Log.Info("Open time pair" + Const.LogInProgress);
                if (!this.ASESOpenTimePairEntryPopup(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESOpenTimePairEntryPopup) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                Log.Info("Type time pair" + Const.LogInProgress);
                if (!this.ASESEnterInOutTimePair(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESEnterInOutTimePair) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                Log.Info("Save time card" + Const.LogInProgress);
                if (!this.ASESSaveTimeEntryCanvas(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESSaveTimeEntryCanvas) + Const.LogFail);

                Log.Info("Wait before logout" + Const.LogInProgress);
                if(!this.ASESWaitBeforeLogout())
                    throw new Exception(nameof(ASESWaitBeforeLogout) + Const.LogCanceled);

                Log.Info("Logout" + Const.LogInProgress);
                if (!this.ASESDoLogout(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESDoLogout) + Const.LogFail);

                if (!this.ASESLoginPageIsLoaded(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESLoginPageIsLoaded) + Const.LogFail);

                Log.Info("Teardown browser" + Const.LogInProgress);
                TeardownBrowserInstance(browser);
                browser = null;

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                if(browser != null)
                    TeardownBrowserInstance(browser);

                Log.Info(Const.LogDone);
            }
        }

        #endregion Navigation 

    } // class

} // namespace

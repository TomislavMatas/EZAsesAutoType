//
// File: "Worker.Navigation.cs"
//
// Revision History: 
// 2024/05/10:TomislavMatas: Version "1.125.0"
// * Enhance NULL value handling and validation.
// 2024/04/12:TomislavMatas: Version "1.123.4"
// * Rename "ASESEnterInOutTimePair" to "ASESEnterInOutTimePairs"
// * Add "multiple time pairs" capability to function "ASESEnterInOutTimePairs()".
// 2024/04/10:TomislavMatas: Version "1.123.3"
// * Add check to "DoDailyPunch": When user hitted the "Cancel" button,
//   the already loaded browser instance shall stay "active". Otherwise,
//   when processing has not been canceled by user, the browser instance
//   will be closed immediatly after "logout".
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using EZSeleniumLib;
using OpenQA.Selenium;
using Keys = OpenQA.Selenium.Keys;

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
                ArgumentNullException.ThrowIfNull(browser,nameof(browser));
                ArgumentNullException.ThrowIfNull(baseUrl,nameof(baseUrl));

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
        /// The entire ASES application runs in an iFrame.
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
                ArgumentNullException.ThrowIfNull(browser,nameof(browser));
                ArgumentNullException.ThrowIfNull(iFrameXPath, nameof(iFrameXPath));

                IWebElement? iFrameElement = browser.FindElementByXpath(iFrameXPath, timeoutInSeconds);
                if (iFrameElement == null)
                    throw new Exception(String.Format("'{0}'{1}", iFrameXPath, Const.LogNotFound));

                IWebDriver? iFrameDriver = browser.SwitchToIFrame(iFrameElement);
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
                ArgumentNullException.ThrowIfNull(browser,nameof(browser));

                string xPath = this.GetLoginPageUsernameXPath();
                IWebElement? element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("ElementNotFound, xPath='{0}'",xPath));

                if (!browser.MoveToElement(element))
                    throw new Exception(nameof(browser.MoveToElement) + Const.LogFail);

                element.SendKeys(this.GetASESUserId());

                xPath = this.GetLoginPagePasswordXPath();
                element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("ElementNotFound, xPath='{0}'", xPath));

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
                        if (element == null)
                            throw new Exception(string.Format("ElementNotFound, xPath='{0}'", xPath));

                        if (browser.MoveToElement(element))
                            element.SendKeys(client);
                    }
                }

                string langauge = this.GetASESLanguage();
                if (!string.IsNullOrEmpty(langauge))
                {   // considered "optional"
                    xPath = this.GetLoginPageLanguageXPath();
                    element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                    if (element == null)
                        throw new Exception(string.Format("ElementNotFound, xPath='{0}'", xPath));

                    if (browser.MoveToElement(element))
                        element.SendKeys(langauge);
                }

                xPath = this.GetLoginPageLoginButtonXPath();
                element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("ElementNotFound, xPath='{0}'", xPath));

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
                ArgumentNullException.ThrowIfNull(browser,nameof(browser));

                if (!this.ASESMainPageIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESMainPageIsLoaded) + Const.LogFail);

                // this extra sleep is required, because the splash
                // image is moved by an "animation effect" which
                // invalidates the DOM for a while : - (
                Thread.Sleep(1000);

                string xPath = this.GetNavMenuXPath();
                IWebElement? element = browser.FindElementByXpath(xPath, timeoutInSeconds);
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
                ArgumentNullException.ThrowIfNull(browser,nameof(browser));

                if (!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                if (!this.ASESTimeEntryCanvasIsSortedAscending(browser, timeoutInSeconds))
                {
                    if (this.ASESTimeEntryCanvasIsSortedDescending(browser, timeoutInSeconds))
                    {
                        const string subProcess = "Set sorting state ascending";
                        Log.Info(subProcess + Const.LogInProgress);
                        string sortindicatorXPath = this.GetTimeGridCanvasSortingDescXPath();
                        IWebElement? sortindicatorElement = browser.FindElementByXpath(sortindicatorXPath, timeoutInSeconds);
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
                ArgumentNullException.ThrowIfNull(browser,nameof(browser));

                if (!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                string xPath = this.GetTimeGridCanvasLastRowDateFromXPath();
                IWebElement? element = browser.FindElementByXpath(xPath, timeoutInSeconds);
                if (element == null)
                    throw new Exception(string.Format("'{0}'{1}", xPath, Const.LogNotFound));
                
                if (!browser.ClickElement(element))
                    throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

                // first click on "span" element activates "edit mode" by
                // embedding a "input" control. Need to click a second time
                // but only if that "input" control has appeared
                Thread.Sleep(1000);
                xPath += "/input";
                IWebElement? input = browser.FindElementByXpath(xPath, timeoutInSeconds);
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
        /// <param name="timePairList"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool ASESEnterInOutTimePairs(BrowserBase browser, int timeoutInSeconds, List<TimePair> timePairList)
        {
            try
            {
                Log.Debug(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser, nameof(browser));

                if (!this.ASESTimePairEntryPopupIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimePairEntryPopupIsLoaded) + Const.LogFail);

                int rowIndex = 0;
                foreach (TimePair timePair in timePairList)
                {
                    if (timePair == null)
                        continue;

                    if (string.IsNullOrEmpty(timePair.PunchIn))
                        continue;

                    if (string.IsNullOrEmpty(timePair.PunchOut))
                        continue;

                    rowIndex++;
                    if (rowIndex == 1)
                    {   // On first time pair use FindElement for "validation" 
                        string xPathRow = this.GetTimePairFirstRowTimeFromXPath();
                        IWebElement? rowElement = browser.FindElementByXpath(xPathRow, timeoutInSeconds);
                        if (rowElement == null)
                            throw new Exception(string.Format("'{0}'{1}", xPathRow, Const.LogNotFound));

                        if (!browser.ClickElement(rowElement))
                            throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

                        // this sleep is required, because the input element 
                        // will be renderd _after_ the cell item's span element
                        // has been activated by "mouse click".
                        Thread.Sleep(1000);

                        xPathRow += "/input";
                        rowElement = browser.FindElementByXpath(xPathRow, timeoutInSeconds);
                        if (rowElement == null)
                            throw new Exception(string.Format("'{0}'{1}", xPathRow, Const.LogNotFound));

                        string punchInValue = timePair.PunchIn;
                        rowElement.SendKeys(punchInValue);

                        xPathRow = this.GetTimePairFirstRowTimeToXPath();
                        rowElement = browser.FindElementByXpath(xPathRow, timeoutInSeconds);
                        if (rowElement == null)
                            throw new Exception(string.Format("'{0}'{1}", xPathRow, Const.LogNotFound));

                        if (!browser.ClickElement(rowElement))
                            throw new Exception(nameof(browser.MoveToElement) + Const.LogFail);

                        // this sleep is required, because the input element 
                        // will be renderd _after_ the cell item's span element
                        // has been activated by "mouse click".
                        Thread.Sleep(1000);
                        xPathRow += "/input";
                        rowElement = browser.FindElementByXpath(xPathRow, timeoutInSeconds);
                        if (rowElement == null)
                            throw new Exception(string.Format("'{0}'{1}", xPathRow, Const.LogNotFound));

                        string punchOutValue = timePair.PunchOut;
                        rowElement.SendKeys(punchOutValue);

                        if (timePairList.Count == 1)
                            break; // nothing left to do

                        // sending a single "TAB" key stroke _now_
                        // should implicitly add a new time pair entry row.
                        rowElement.SendKeys(Keys.Tab);

                        // this sleep is required to give app some time
                        // to render the "new row".
                        Thread.Sleep(1000);
                    } // rowIndex == 1

                    if (rowIndex > 1)
                    {   // On rowIndex == 0, cursor should already have been moved to
                        // new time pair row py sending a single "TAB" key stroke.
                        // On rowIndex > 1, simple keyboard navigation can be used.
                        IWebDriver? webDriver = browser.GetWebDriver();
                        if(webDriver==null)
                            throw new Exception(nameof(webDriver) + Const.LogIsNull);

                        IWebElement? activeElement = webDriver.SwitchTo().ActiveElement();
                        if(activeElement==null)
                            throw new Exception(nameof(activeElement) + Const.LogIsNull);

                        // do additional inspections to check if theactive element is the expected one.
                        string tagName = activeElement.TagName;
                        if(!string.Equals(tagName, "input", StringComparison.OrdinalIgnoreCase))
                            throw new Exception(nameof(activeElement) + Const.LogInvalid);

                        string punchInValue = timePair.PunchIn;
                        activeElement.SendKeys(punchInValue);

                        // sending a single "TAB" key stroke _now_ to move
                        // out from "PunchIn" element and proceed to "PunchOut".
                        activeElement.SendKeys(Keys.Tab);

                        // this sleep is required to give app some time
                        // to render the "PunchOut" element.
                        Thread.Sleep(1000);

                        activeElement = webDriver.SwitchTo().ActiveElement();
                        if (activeElement == null)
                            throw new Exception(nameof(activeElement) + Const.LogIsNull);

                        tagName = activeElement.TagName;
                        if (!string.Equals(tagName, "input", StringComparison.OrdinalIgnoreCase))
                            throw new Exception(nameof(activeElement) + Const.LogInvalid);

                        string punchOutValue = timePair.PunchOut;
                        activeElement.SendKeys(punchOutValue);

                        if (rowIndex == timePairList.Count)
                            break; // nothing left to do

                        // sending a single "TAB" key stroke _now_
                        // should implicitly add a new time pair entry row.
                        activeElement.SendKeys(Keys.Tab);

                        // this sleep is required to give app some time
                        // to render the "new row".
                        Thread.Sleep(1000);

                    } // rowIndex > 1

                } // foreach (TimePair timePair in timePairList)

                string xPathAcceptButton = this.GetTimePairFooterAcceptButtonPath();
                IWebElement? elementAcceptButton = browser.FindElementByXpath(xPathAcceptButton, timeoutInSeconds);
                if (elementAcceptButton == null)
                    throw new Exception(string.Format("'{0}'{1}", xPathAcceptButton, Const.LogNotFound));

                if (!browser.ClickElement(elementAcceptButton))
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
                ArgumentNullException.ThrowIfNull(browser, nameof(browser));

                if (!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                string xPath = this.GetTimeGridCanvasSaveButtonPath();
                IWebElement? element = browser.FindElementByXpath(xPath, timeoutInSeconds);
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
                ArgumentNullException.ThrowIfNull(browser, nameof(browser));

                if (!this.ASESMainPageIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESMainPageIsLoaded) + Const.LogFail);

                string xPath = this.GetNavMenuUsernameXPath();
                IWebElement? element = browser.FindElementByXpath(xPath, timeoutInSeconds);
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

                List<TimePair>? timePairList = this.GetTimePairListDefault();
                if (timePairList == null)
                    throw new Exception(nameof(timePairList) + Const.LogIsNull);

                if (timePairList.Count == 0)
                    throw new Exception(nameof(timePairList) + Const.LogInvalid);

                Log.Info("Type time pairs" + Const.LogInProgress);
                if (!this.ASESEnterInOutTimePairs(browser, timeoutFindElement, timePairList))
                    throw new Exception(nameof(this.ASESEnterInOutTimePairs) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                Log.Info("Save time card" + Const.LogInProgress);
                if (!this.ASESSaveTimeEntryCanvas(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESSaveTimeEntryCanvas) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                Log.Info("Wait before logout" + Const.LogInProgress);
                if(!this.ASESWaitBeforeLogout())
                    throw new Exception(nameof(ASESWaitBeforeLogout) + Const.LogCanceled);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                Log.Info("Logout" + Const.LogInProgress);
                if (!this.ASESDoLogout(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESDoLogout) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                if (!this.ASESLoginPageIsLoaded(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESLoginPageIsLoaded) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

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
                    if (!this.CancelRequested())
                        TeardownBrowserInstance(browser);

                Log.Info(Const.LogDone);
            }
        }

        #endregion

    } // class

} // namespace

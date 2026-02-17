//
// File: "Worker.Navigation.cs"
//
// Revision History:
// 2026/02/17:TomislavMatas: v4.40.1450
// * Implement SSO handling.
// 2024/11/24:TomislavMatas: Version "1.131.2"
// * Prevent browser teardown on exception.
// 2024/11/22:TomislavMatas: Version "1.131.2"
// * Add "ASESClickLoadTimeEntryCanvas".
// * Custom handling for Client="24-Cargo Zentrale".
// 2024/08/07:TomislavMatas: Version "1.127.2"
// * Assure that canvas has been sorted by date ascending before punching.
// 2024/08/06:TomislavMatas: Version "1.127.1"
// * Implement "RandomizeDeviation" and "RoundDown" 
//   to make punches look more "human~like".
// 2024/08/05:TomislavMatas: Version "1.127.1"
// * Handle UserSetting "ASESPunchDeviation".
// 2024/07/08:TomislavMatas: Version "1.126.4"
// * Handle UserSetting "DoLogout".
// * Removed input.Clear() call, because it was interfering with other 
//   implementations when punching in time pairs.
// 2024/07/04:TomislavMatas: Version "1.126.2"
// * Prevent (possible, but unlikely) null references.
// * Implement handling of "${RowIndex}" token replacement for
//   time pair xPathes: During processing of the time pairs,
//   for each time pair, the literal "${RowIndex}" within
//   the xPath string from "App.config" will be
//   replaced by respective "rowIndex" value.
// 2024/07/03:TomislavMatas: Version "1.126.2"
// * Handle UserSetting "DoLogin" and "DoPunch".
// 2024/07/01:TomislavMatas: Version "1.126.2"
// * Replace calls to "ClickElement" with calls to "ClickElementWithRetry".
//   in function "ASESEnterInOutTimePairs".
// * Add new parameter "maxRetries" to function "ASESEnterInOutTimePairs".
// 2024/05/31:TomislavMatas: Version "1.126.0"
// * Simplify log4net implementations.
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
        private const string ReplacmentToken_RowIndex = @"${RowIndex}";

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
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
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
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
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
                LogTrace(Const.LogStart);
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

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                LogTrace(Const.LogDone);
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
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
            }
        }

        /// <summary>
        /// Click the Date column header to sort canvas by date ascending.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ClickSortingheader(BrowserBase browser, int timeoutInSeconds)
        {
            try { 
                string sortingheaderXPath = this.GetDateGridCanvasSortingheaderXPath();
                IWebElement? sortingheaderElement = browser.FindElementByXpath(sortingheaderXPath, timeoutInSeconds);
                if (sortingheaderElement == null)
                    throw new Exception(string.Format("'{0}'{1}", sortingheaderXPath, Const.LogNotFound));

                if (!browser.ClickElement(sortingheaderElement))
                    throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

                // sorting may take some time - await rendering
                Thread.Sleep(1000);
                
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        /// <summary>
        /// Use Browser-Interop Helper to navigate to sort the "Time Entry Grid Canvas" ascending.
        /// In Essence: Click the respective header items to make sure, the canvas
        /// is sorted by column date in ascending order.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESSortTimeEntryCanvasByDateAscending(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser,nameof(browser));

                if (!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                // It is not guaranteed, that the canvas is sorted by the date
                // column in ascending order, because user might have sorted
                // by other column by default or has sorted by date descending
                // by default. In either case, click header for date column once.
                // This should implicitly sort the canvas by date ascending.
                // Use a short timeout for fast results.
                const int shortTimeout = 1;
                if (!this.ASESTimeEntryCanvasIsSortedByDate(browser, shortTimeout))
                    if(!this.ClickSortingheader(browser, shortTimeout))
                        throw new Exception(nameof(this.ClickSortingheader) + Const.LogFail);

                // Should be sorted by date ascending now,
                // but must not be true in any case.
                if (this.ASESTimeEntryCanvasIsSortedAscending(browser, timeoutInSeconds))
                    return true;

                // If not sorted by date ascending now,
                // check if sorted by date descending.
                if (!this.ASESTimeEntryCanvasIsSortedDescending(browser, timeoutInSeconds))
                    throw new Exception("SortingState" + Const.LogInvalid);

                // canvas seems to be sorted by date descending.
                // Click sortindicator to toggle sort order to ascending.
                string sortindicatorXPath = this.GetDateGridCanvasSortingDescXPath();
                IWebElement? sortindicatorElement = browser.FindElementByXpath(sortindicatorXPath, timeoutInSeconds);
                if (sortindicatorElement == null)
                    throw new Exception(string.Format("'{0}'{1}", sortindicatorXPath, Const.LogNotFound));
                        
                if (!browser.ClickElement(sortindicatorElement))
                    throw new Exception(nameof(browser.ClickElement) + Const.LogFail);

                // sleep a while before validating succes of "sort" operation
                Thread.Sleep(1000);

                if(!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                // doublecheck sort order to be by date ascending.
                if (!this.ASESTimeEntryCanvasIsSortedAscending(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsSortedAscending) + Const.LogFail);
                        
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                LogTrace(Const.LogDone);
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
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser,nameof(browser));

                if (!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                string xPath = this.GetDateGridCanvasLastRowDateFromXPath();
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
                LogTrace(Const.LogDone);
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
        /// <param name="maxRetries"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool ASESEnterInOutTimePairs(BrowserBase browser, int timeoutInSeconds, List<TimePair> timePairList, int maxRetries)
        {
            try
            {
                LogTrace(Const.LogStart);
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

                    string xPathTimeFrom = this.GetTimePairTimeFromXPath();
                    if(xPathTimeFrom == null)
                        throw new Exception(nameof(xPathTimeFrom)+Const.LogIsNull);

                    xPathTimeFrom = xPathTimeFrom.Replace(ReplacmentToken_RowIndex, rowIndex.ToString());
                    IWebElement? rowElement = browser.FindElementByXpath(xPathTimeFrom, timeoutInSeconds);
                    if (rowElement == null)
                        throw new Exception(string.Format("'{0}'{1}", xPathTimeFrom, Const.LogNotFound));

                    if (!browser.ClickElementWithRetry(By.XPath(xPathTimeFrom), maxRetries))
                        throw new Exception(nameof(browser.ClickElementWithRetry) + Const.LogFail);

                    // this sleep is required, because the input element 
                    // will be renderd _after_ the cell item's span element
                    // has been activated by "mouse click".
                    Thread.Sleep(1000);

                    string xPathTimeFromInput = xPathTimeFrom + "/input";
//                    if (!browser.ClearElementWithRetry(By.XPath(xPathTimeFromInput), maxRetries))
//                        throw new Exception(nameof(browser.ClearElementWithRetry) + Const.LogFail);

                    string punchInValue = timePair.PunchIn;
                    if (!browser.SendKeysWithRetry(By.XPath(xPathTimeFromInput), punchInValue, maxRetries))
                        throw new Exception(nameof(browser.SendKeysWithRetry) + Const.LogFail);

                    // sending an additional single "TAB" key stroke _now_
                    // to trigger "validated" event on input element.
                    if (!browser.SendKeysWithRetry(By.XPath(xPathTimeFromInput), Keys.Tab, maxRetries))
                        throw new Exception(nameof(browser.SendKeysWithRetry) + Const.LogFail);

                    string xPathTimeTo = this.GetTimePairTimeToXPath();
                    if (xPathTimeTo == null)
                        throw new Exception(nameof(xPathTimeTo) + Const.LogIsNull);

                    xPathTimeTo = xPathTimeTo.Replace(ReplacmentToken_RowIndex, rowIndex.ToString());
                    rowElement = browser.FindElementByXpath(xPathTimeTo, timeoutInSeconds);
                    if (rowElement == null)
                        throw new Exception(string.Format("'{0}'{1}", xPathTimeTo, Const.LogNotFound));

                    if (!browser.ClickElementWithRetry(By.XPath(xPathTimeTo), maxRetries))
                        throw new Exception(nameof(browser.ClickElementWithRetry) + Const.LogFail);

                    // this sleep is required, because the input element 
                    // will be renderd _after_ the cell item's span element
                    // has been activated by "mouse click".
                    Thread.Sleep(1000);

                    string xPathTimeToInput = xPathTimeTo + "/input";
//                    if (!browser.ClearElementWithRetry(By.XPath(xPathTimeToInput), maxRetries))
//                        throw new Exception(nameof(browser.ClearElementWithRetry) + Const.LogFail);

                    string punchOutValue = timePair.PunchOut;
                    if (!browser.SendKeysWithRetry(By.XPath(xPathTimeToInput), punchOutValue, maxRetries))
                        throw new Exception(nameof(browser.SendKeysWithRetry) + Const.LogFail);

                    // sending an additional single "TAB" key stroke _now_ 
                    // to trigger "validated" event on input element.
                    if (!browser.SendKeysWithRetry(By.XPath(xPathTimeToInput), Keys.Tab, maxRetries))
                        throw new Exception(nameof(browser.SendKeysWithRetry) + Const.LogFail);

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
                LogTrace(Const.LogDone);
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
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser, nameof(browser));

                if (!this.ASESTimeEntryCanvasIsLoaded(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoaded) + Const.LogFail);

                string xPath = this.GetDateGridCanvasSaveButtonPath();
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
                LogTrace(Const.LogDone);
            }
        }

        private bool ASESClickLoadTimeEntryCanvas(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser, nameof(browser));

                string xPath = this.GetDateGridCanvasLoadButtonPath();
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
                LogTrace(Const.LogDone);
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
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
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
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
            }
        }

        /// <summary>
        /// Use Browser-Interop Helper to perform the daily 
        /// "punch in / punch out" routine.
        /// </summary>
        /// <returns></returns>
        public bool DoDailyPunch()
        {
            bool doTeardown = true;
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

                // client specific flow of sequence might differ slightly
                // deppending on client specific templates.
                string asesClient = this.GetASESClient();

                string baseUrl = this.GetASESBaseUrl();
                Log.Info(String.Format("Navigate to '{0}'{1}", baseUrl, Const.LogInProgress));
                int timeoutLoginPage = this.GetTimeoutNavigationLoginPage();
                if (!this.ASESNavigateToLoginPage(browser, baseUrl, timeoutLoginPage))
                    throw new Exception(nameof(this.ASESNavigateToLoginPage) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                int timeoutFindElement = this.GetTimeoutFindElement();
                int maxRetriesForElementOperations = this.GetMaxRetriesForElementOperations();
                string iFrameXPath = this.GetApplicationIFrameXPath();
                bool doLogin = this.GetWorkerConfig().GetUserSettings().DoLogin;
                bool useSso = this.GetWorkerConfig().GetUserSettings().ASESUseSso;
                if (!useSso)
                {
                    if (!doLogin)
                    {   // Login automation with username and password shall not be executed.
                        // Raise "CancelRequested" to quit automation,
                        // but leave browser up and running.
                        // See "finally" part of this method.
                        Global.SetCancelRequested(true);
                        return true;
                    }

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

                }
                else
                {
                    if (!doLogin)
                    {   // Login automation using SSO shall not be executed.
                        // Raise "CancelRequested" to quit automation,
                        // but leave browser up and running.
                        // See "finally" part of this method.
                        Global.SetCancelRequested(true);
                        return true;
                    }

                    if (!this.ASESSwitchToIFrame(browser, iFrameXPath, timeoutFindElement))
                        throw new Exception(nameof(this.ASESSwitchToIFrame) + Const.LogFail);

                }

                if (!WaitUntilMainPageHasLoaded(browser, timeoutFindElement))
                    throw new Exception(nameof(WaitUntilMainPageHasLoaded) + Const.LogFail);

                Log.Info("Open time card" + Const.LogInProgress);
                if (!this.ASESNavigateToTimeEntryCanvas(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESNavigateToTimeEntryCanvas) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                if (!this.ASESTimeEntrySideBarIsLoaded(browser, asesClient, timeoutFindElement))
                    throw new Exception(nameof(this.ASESTimeEntrySideBarIsLoaded) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                if (!this.ASESTimeEntrySideBarClickLoadButton(browser, asesClient, timeoutFindElement))
                    throw new Exception(nameof(this.ASESTimeEntrySideBarClickLoadButton) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                if (!this.ASESTimeEntryCanvasIsLoadedClientSpecific(browser, asesClient, timeoutFindElement))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoadedClientSpecific) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                if (!this.ASESSortTimeEntryCanvasByDateAscending(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESSortTimeEntryCanvasByDateAscending) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                bool doPunch = this.GetWorkerConfig().GetUserSettings().DoPunch;
                if (!doPunch)
                {   // Punch-Automation shall not be executed.
                    // Raise "CancelRequested" to quit automation,
                    // but leave browser up and running.
                    // See "finally" part of this method.
                    Global.SetCancelRequested(true);
                    return true;
                }

                Log.Info("Open time pair" + Const.LogInProgress);
                if (!this.ASESOpenTimePairEntryPopup(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESOpenTimePairEntryPopup) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                List<TimePair> timePairList = this.GetTimePairListDefault();
                if (timePairList == null)
                    throw new Exception(nameof(timePairList) + Const.LogIsNull);

                if (timePairList.Count == 0)
                    throw new Exception(nameof(timePairList) + Const.LogInvalid);

                int asesPunchDeviation = this.GetASESPunchDeviation();
                if (asesPunchDeviation > 0)
                {
                    int deviationToApply = this.RandomizeDeviation(asesPunchDeviation);
                    timePairList = ApplyDeviation(timePairList, deviationToApply);
                }

                Log.Info("Type time pairs" + Const.LogInProgress);
                if (!this.ASESEnterInOutTimePairs(browser, timeoutFindElement, timePairList, maxRetriesForElementOperations))
                    throw new Exception(nameof(this.ASESEnterInOutTimePairs) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                Log.Info("Save time card" + Const.LogInProgress);
                if (!this.ASESSaveTimeEntryCanvas(browser, timeoutFindElement))
                    throw new Exception(nameof(this.ASESSaveTimeEntryCanvas) + Const.LogFail);

                if (this.CancelRequested())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogCanceled);

                bool doLogout= this.GetWorkerConfig().GetUserSettings().DoLogout;
                if (!doLogout)
                {   // Logout-Automation shall not be executed.
                    // Raise "CancelRequested" to quit automation,
                    // but leave browser up and running.
                    // See "finally" part of this method.
                    Global.SetCancelRequested(true);
                    return true;
                }

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
                doTeardown = false;
                return false;
            }
            finally
            {
                if(browser != null)
                    if (!this.CancelRequested())
                        if(doTeardown)
                            TeardownBrowserInstance(browser);

                Log.Info(Const.LogDone);
            }
        }

        #endregion

    } // class

} // namespace

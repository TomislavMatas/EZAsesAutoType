//
// File: "Worker.Validation.cs"
//
// Revision History:
// 2024/11/22:TomislavMatas: Version "1.131.2"
// * Various code changes to meet code style conventions.
// * Custom handling for Client="24-Cargo Zentrale".
// 2024/08/07:TomislavMatas: Version "1.127.2"
// * Add "ASESTimeEntryCanvasIsSortedByDate".
// 2024/07/04:TomislavMatas: Version "1.126.2"
// * Fix in "ASESTimePairEntryPopupIsLoaded": Implement handling of
//   "${RowIndex}" token replacement for time pair xPathes.
// 2024/05/31:TomislavMatas: Version "1.126.0"
// * Simplify log4net implementations.
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System.Collections.Specialized;
using System.Text.RegularExpressions;

using EZSeleniumLib;

namespace EZAsesAutoType
{
    internal partial class Worker
    {
        #region cancelation handling

        /// <summary>
        /// Check a global semaphore if user has requested "cancelation".
        /// </summary>
        /// <returns></returns>
        private bool CancelRequested()
        {
            return Global.GetCancelRequested();
        }

        /// <summary>
        /// Wait for specified seconds with opportunity to react on "cacnel request".
        /// Returns true, if the wait time has elapsed without cancelation request.
        /// Otherwise, if user requested to cancel the pending opration, returns false.
        /// </summary>
        /// <param name="waitForSeconds"></param>
        /// <returns></returns>
        private bool CancelableWait(int waitForSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (waitForSeconds < 0)
                    return true;

                int secondsElapsed = 0;
                while (secondsElapsed < waitForSeconds)
                {
                    if (this.CancelRequested())
                        throw new Exception(nameof(CancelableWait) + Const.LogCanceled);

                    Thread.Sleep(1000); // 1000 ms = one second
                    secondsElapsed++;
                    if (secondsElapsed >= waitForSeconds)
                        return true;
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
                LogTrace(Const.LogDone);
            }
        }
        #endregion 

        #region page loaded validatorz

        /// <summary>
        /// Use Browser-Interop Helper to find all the elements,
        /// that shall be present on the app's login page.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESLoginPageIsLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);

                StringCollection requiredElementsXPathCollection = [
                  this.GetLoginPageUsernameXPath()
                , this.GetLoginPagePasswordXPath()
                , this.GetLoginPageClientXPath()
                , this.GetLoginPageLanguageXPath()
                , this.GetLoginPageLoginButtonXPath()
                ];

                foreach (string? requiredElementXPath in requiredElementsXPathCollection)
                    if (requiredElementXPath != null)
                        if (browser.FindElementByXpath(requiredElementXPath, timeoutInSeconds) == null)
                            throw new Exception(String.Format("'{0}'{1}", requiredElementXPath, Const.LogNotFound));

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
        /// Use Browser-Interop Helper to find all the elements,
        /// that shall be present on the app's SSO account page.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESSsoPageIsLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);

                StringCollection requiredElementsXPathCollection = [
                  this.GetSsoAccountXPath()
                , this.GetSsoSubmitXPath()
                ];

                foreach (string? requiredElementXPath in requiredElementsXPathCollection)
                    if (requiredElementXPath != null)
                        if (browser.FindElementByXpath(requiredElementXPath, timeoutInSeconds) == null)
                            throw new Exception(String.Format("'{0}'{1}", requiredElementXPath, Const.LogNotFound));

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
        /// Use Browser-Interop Helper to find all the elements,
        /// that shall be present on the app's main page.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESMainPageIsLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);

                // consider "correct page displayd" only when all required elements can be found.
                StringCollection requiredElementXPathCollection = [
                  this.GetNavMenuXPath()
                , this.GetNavMenuUsernameXPath()
                , this.GetMainPageIFrameXPath()
                ];

                foreach (string? requiredElementXPath in requiredElementXPathCollection)
                    if (requiredElementXPath != null)
                        if (browser.FindElementByXpath(requiredElementXPath, timeoutInSeconds) == null)
                            throw new Exception(String.Format("'{0}'{1}", requiredElementXPath, Const.LogNotFound));

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
        /// Wait until a specific content has been loaded.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool WaitUntilMainPageHasLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);

                int timeoutFindElement = this.GetTimeoutFindElement();
                int secondsElapsed = 0;
                while (secondsElapsed < timeoutInSeconds)
                {
                    if (this.CancelRequested())
                        throw new Exception(nameof(WaitUntilMainPageHasLoaded) + Const.LogCanceled);

                    try
                    {
                        if (this.ASESMainPageIsLoaded(browser, timeoutFindElement))
                            return true; // SUCCESS
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(ex);
                    }
                    Thread.Sleep(1000); // 1000 ms = one second
                    secondsElapsed++;
                    if (secondsElapsed >= timeoutInSeconds)
                        throw new Exception(nameof(WaitUntilMainPageHasLoaded) + Const.LogTimeout);

                }
                return false;
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
        /// Use Browser-Interop Helper to find all the elements,
        /// that shall be present when nav menu is poped up.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESNavMenuPopupIsLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);

                // consider "correct page displayd" only when all required elements can be found.
                StringCollection requiredElementXPathCollection = [
                    this.GetNavMenuZeitbuchungXPath()
                ];

                foreach (string? requiredElementXPath in requiredElementXPathCollection)
                    if (requiredElementXPath != null)
                        if (browser.FindElementByXpath(requiredElementXPath, timeoutInSeconds) == null)
                            throw new Exception(String.Format("'{0}'{1}", requiredElementXPath, Const.LogNotFound));

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
        /// Wait until a specific content has been loaded.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool WaitUntilNavMenuPopupHasLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);

                int timeoutFindElement = this.GetTimeoutFindElement();
                int secondsElapsed = 0;
                while (secondsElapsed < timeoutInSeconds)
                {
                    if (this.CancelRequested())
                        throw new Exception(nameof(WaitUntilNavMenuPopupHasLoaded) + Const.LogCanceled);

                    try
                    {
                        if (this.ASESNavMenuPopupIsLoaded(browser, timeoutFindElement))
                            return true; // SUCCESS
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(ex);
                    }
                    Thread.Sleep(1000); // 1000 ms = one second
                    secondsElapsed++;
                    if (secondsElapsed >= timeoutInSeconds)
                        throw new Exception(nameof(WaitUntilNavMenuPopupHasLoaded) + Const.LogTimeout);

                }
                return false;
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
        /// Use Browser-Interop Helper to find all the elements,
        /// that shall be present on the app's time entry page.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="requiredElementXPathCollection"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool ASESTimeEntryCanvasIsLoadedImpl(BrowserBase browser, StringCollection requiredElementXPathCollection, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);
                ArgumentNullException.ThrowIfNull(requiredElementXPathCollection);

                foreach (string? requiredElementXPath in requiredElementXPathCollection)
                    if (requiredElementXPath != null)
                        if (browser.FindElementByXpath(requiredElementXPath, timeoutInSeconds) == null)
                            throw new Exception(String.Format("'{0}'{1}", requiredElementXPath, Const.LogNotFound));

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
        /// Use Browser-Interop Helper to find a specific set of all the elements,
        /// that shall be present on the app's time entry page.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool ASESTimeEntryCanvasIsLoadedClientSpecific(BrowserBase browser, string clientId, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);
                ArgumentNullException.ThrowIfNull(clientId);

                // #TODO: Implement ore generic throug clever App.config settings.
                StringCollection requiredElementXPathCollection;
                if (clientId == Const.Client_DbSystel)
                {
                    requiredElementXPathCollection = [
                          this.GetDateGridFormXPath()
                        , this.GetDateGridCanvasXPath()
                        , this.GetDateGridCanvasLastRowXPath()
                        , this.GetDateGridCanvasLastRowDateFromXPath()
                        , this.GetDateGridCanvasLastRowDateToXPath()
                    ];
                }
                else if (clientId == Const.Client_DbCargo)
                {
                    requiredElementXPathCollection = [
                          this.GetDateGridFormXPath()
                        , this.GetDateGridCanvasXPath()
                        , this.GetDateGridCanvasLastRowXPath()
                        , this.GetDateGridCanvasLastRowDateFromXPath()
                        , this.GetDateGridCanvasLastRowDateToXPath()
                    ];
                }
                else 
                {
                    throw new Exception(nameof(clientId) + Const.LogInvalid);
                }

                if (!this.ASESTimeEntryCanvasIsLoadedImpl(browser,requiredElementXPathCollection, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoadedImpl) + Const.LogFail);

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
        /// Use Browser-Interop Helper to find a specific set of all the elements,
        /// that shall be present on the app's time entry page.
        /// Use custom sepcific handling for client "06-DB-Systel".
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESTimeEntryCanvasIsLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);

                if (!this.ASESTimeEntryCanvasIsLoadedClientSpecific(browser, Const.Client_DbSystel, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntryCanvasIsLoadedClientSpecific) + Const.LogFail);

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
        /// Wait until a specific content has been loaded.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
/*
        private bool WaitUntilTimeEntryCanvasHasLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                int timeoutFindElement = this.GetTimeoutFindElement();
                int secondsElapsed = 0;
                while (secondsElapsed < timeoutInSeconds)
                {
                    if (this.CancelRequested())
                        throw new Exception(nameof(WaitUntilTimeEntryCanvasHasLoaded) + Const.LogCanceled);

                    string clientId = WorkerConfig.GetUserSettings().ASESClient;
                    try
                    {
                        if (this.ASESTimeEntryCanvasIsLoaded(browser, timeoutFindElement))
                            return true; // SUCCESS
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(ex);
                    }
                    Thread.Sleep(1000); // 1000 ms = one second
                    secondsElapsed++;
                    if (secondsElapsed >= timeoutInSeconds)
                        throw new Exception(nameof(WaitUntilTimeEntryCanvasHasLoaded) + Const.LogTimeout);


                }
                return false;
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
*/
        
        private bool ASESTimePairEntryPopupIsLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);

                // consider "correct page displayd" only when all required elements can be found.
                StringCollection requiredElementXPathCollection = [
                  this.GetTimePairTimeFromXPath()
                , this.GetTimePairTimeToXPath()
                ];

                foreach (string? requiredElementXPath in requiredElementXPathCollection)
                {
                    if (requiredElementXPath != null)
                    {
                        string elementXPath = requiredElementXPath.Replace(ReplacmentToken_RowIndex, "1");
                        if (browser.FindElementByXpath(elementXPath, timeoutInSeconds) == null)
                            throw new Exception(String.Format("'{0}'{1}", elementXPath, Const.LogNotFound));

                        // found this particular required element
                        Log.Debug(string.Format("required element '{0}' found", elementXPath));
                    }
                }

                // this point can only be reached, if all elements in 
                // "requiredElementXPathCollection" have been found
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
        /// Wait until a specific content has been loaded.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool WaitUntilTimePairEntryPopupHasLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);

                int timeoutFindElement = this.GetTimeoutFindElement();
                bool timeoutReached = false;
                int secondsElapsed = 0;
                while (!timeoutReached)
                {
                    if (this.CancelRequested())
                        throw new Exception(nameof(WaitUntilTimePairEntryPopupHasLoaded) + Const.LogCanceled);

                    if (secondsElapsed > timeoutInSeconds)
                        throw new Exception(Const.LogTimeout);

                    try
                    {
                        if (this.ASESTimePairEntryPopupIsLoaded(browser, timeoutFindElement))
                            return true; // SUCCESS
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(ex);
                    }
                    Thread.Sleep(1000); // 1000 ms = one second
                }
                return false;
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

        #endregion

        #region state validators

        /// <summary>
        /// Check if any of the sortindicators for date is displayed.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool ASESTimeEntryCanvasIsSortedByDate(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);

                string xPath = this.GetDateGridCanvasSortingAscXPath();
                if (browser.FindElementByXpath(xPath, timeoutInSeconds) != null)
                    return true;

                xPath = this.GetDateGridCanvasSortingDescXPath();
                if (browser.FindElementByXpath(xPath, timeoutInSeconds) != null)
                    return true;

                return false;
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

        private bool ASESTimeEntryCanvasIsSortedAscending(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);

                string xPath = this.GetDateGridCanvasSortingAscXPath();
                if (browser.FindElementByXpath(xPath, timeoutInSeconds) != null)
                    return true;

                return false;
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

        private bool ASESTimeEntryCanvasIsSortedDescending(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);

                string xPath = this.GetDateGridCanvasSortingDescXPath();
                if (browser.FindElementByXpath(xPath, timeoutInSeconds) != null)
                    return true;

                return false;
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

        private bool ASESTimeEntrySideBarIsLoadedImpl(BrowserBase browser, StringCollection requiredElementXPathCollection, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);
                ArgumentNullException.ThrowIfNull(requiredElementXPathCollection);

                foreach (string? requiredElementXPath in requiredElementXPathCollection)
                    if (requiredElementXPath != null)
                        if (browser.FindElementByXpath(requiredElementXPath, timeoutInSeconds) == null)
                            throw new Exception(String.Format("'{0}'{1}", requiredElementXPath, Const.LogNotFound));

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
        /// for some clients there is a sidebar displayed containing
        /// a "load data" button to be clicked to actually "load" 
        /// the time entry canvas for a specific time range.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="clientId"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool ASESTimeEntrySideBarIsLoaded(BrowserBase browser, string clientId, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);
                ArgumentNullException.ThrowIfNull(clientId);

                if (clientId == Const.Client_DbSystel)
                    return true; // This client does not have a sidbar

                // Other clients are assumed to have a sidbar
                StringCollection requiredElementXPathCollection = [
                    this.GetDateGridCanvasSaveButtonPath()
                ];

                if(!this.ASESTimeEntrySideBarIsLoadedImpl(browser, requiredElementXPathCollection, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESTimeEntrySideBarIsLoadedImpl) + Const.LogFail);

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
        /// for some clients there is a button on the sidebar
        /// to be clicked to actually "load" the time entry canvas
        /// for a specific time range.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="clientId"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool ASESTimeEntrySideBarClickLoadButton(BrowserBase browser, string clientId, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                ArgumentNullException.ThrowIfNull(browser);
                ArgumentNullException.ThrowIfNull(clientId);

                if (clientId == Const.Client_DbSystel)
                    return true; // this client does not have a sidbar

                // Other clients are assumed to have a sidbar
                if (this.ASESClickLoadTimeEntryCanvas(browser, timeoutInSeconds))
                    throw new Exception(nameof(this.ASESClickLoadTimeEntryCanvas) + Const.LogFail);

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

        #endregion

        #region value validatorz

        private bool ASESUrlContainsClienNoParm(string url)
        {
            if(url == null)
                return false;

            string matchPattern = Const.UrlParmClientNo + "..";
            return Regex.IsMatch(url, matchPattern);
        }

        #endregion

    } // class

} // namespace

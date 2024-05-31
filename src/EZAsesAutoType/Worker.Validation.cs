//
// File: "Worker.Validation.cs"
//
// Revision History: 
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
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

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
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

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
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

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
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

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
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

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
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool ASESTimeEntryCanvasIsLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                // consider "correct page displayd" only when all required elements can be found.
                StringCollection requiredElementXPathCollection = [
                  this.GetTimeGridFormXPath()
                , this.GetTimeGridCanvasXPath()
                , this.GetTimeGridCanvasLastRowXPath()
                , this.GetTimeGridCanvasLastRowDateFromXPath()
                , this.GetTimeGridCanvasLastRowDateToXPath()
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
        
        private bool ASESTimePairEntryPopupIsLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                // consider "correct page displayd" only when all required elements can be found.
                StringCollection requiredElementXPathCollection = [
                  this.GetTimePairFirstRowTimeFromXPath()
                , this.GetTimePairFirstRowTimeToXPath()
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
        private bool WaitUntilTimePairEntryPopupHasLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

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

        private bool ASESTimeEntryCanvasIsSortedAscending(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                string xPath = this.GetTimeGridCanvasSortingAscXPath();
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
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                string xPath = this.GetTimeGridCanvasSortingDescXPath();
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

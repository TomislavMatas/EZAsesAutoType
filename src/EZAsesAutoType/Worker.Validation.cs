//
// File: "Worker.Validation.cs"
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using System.Collections.Specialized;
using System.Text.RegularExpressions;

using EZSeleniumLib;
using OpenQA.Selenium;

namespace EZAsesAutoType
{
    internal partial class Worker
    {
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
                Log.Debug(Const.LogStart);
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
                Log.Debug(Const.LogDone);
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
                Log.Debug(Const.LogStart);
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
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        /// Wait until a specif content has been loaded.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool WaitUntilMainPageHasLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                int timeoutFindElement = this.GetTimeoutFindElement();
                bool timeoutReached = false;
                int secondsElapsed = 0;
                while (!timeoutReached)
                {
                    if (secondsElapsed > timeoutInSeconds)
                        throw new Exception(Const.LogTimeout);

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
                Log.Debug(Const.LogDone);
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
                Log.Debug(Const.LogStart);
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
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        /// Wait until a specif content has been loaded.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool WaitUntilNavMenuPopupHasLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                int timeoutFindElement = this.GetTimeoutFindElement();
                bool timeoutReached = false;
                int secondsElapsed = 0;
                while (!timeoutReached)
                {
                    if (secondsElapsed > timeoutInSeconds)
                        throw new Exception(Const.LogTimeout);

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
                Log.Debug(Const.LogDone);
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
                Log.Debug(Const.LogStart);
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
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        /// Wait until a specif content has been loaded.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool WaitUntilTimeEntryCanvasHasLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                int timeoutFindElement = this.GetTimeoutFindElement();
                bool timeoutReached = false;
                int secondsElapsed = 0;
                while (!timeoutReached)
                {
                    if (secondsElapsed > timeoutInSeconds)
                        throw new Exception(Const.LogTimeout);

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
                Log.Debug(Const.LogDone);
            }
        }
        
        private bool ASESTimePairEntryPopupIsLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
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
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        /// Wait until a specif content has been loaded.
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        private bool WaitUntilTimePairEntryPopupHasLoaded(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (browser == null)
                    throw new ArgumentNullException(nameof(browser));

                int timeoutFindElement = this.GetTimeoutFindElement();
                bool timeoutReached = false;
                int secondsElapsed = 0;
                while (!timeoutReached)
                {
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
                Log.Debug(Const.LogDone);
            }
        }

        #endregion page loaded validatorz

        #region state validators

        private bool ASESTimeEntryCanvasIsSortedAscending(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
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
                Log.Debug(Const.LogDone);
            }
        }

        private bool ASESTimeEntryCanvasIsSortedDescending(BrowserBase browser, int timeoutInSeconds)
        {
            try
            {
                Log.Debug(Const.LogStart);
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
                Log.Debug(Const.LogDone);
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

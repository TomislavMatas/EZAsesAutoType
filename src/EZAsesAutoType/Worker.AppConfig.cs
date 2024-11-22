//
// File: "Worker.AppConfig.cs"
//
// Revision History: 
// 2024/11/22:TomislavMatas: Version "1.131.2"
// * Add "GetDateGridCanvasLoadButtonPath".
// 2024/08/07:TomislavMatas: Version "1.127.2"
// * Add "GetDateGridCanvasSortingheaderXPath".
// 2024/07/02:TomislavMatas: Version "1.126.2"
// * Rename "ASES.TimeGrid.*" to "ASES.DateGrid.*".
// * Rename "ASES.TimePair.FirstRow.*" to "ASES.TimePair.*".
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

namespace EZAsesAutoType
{
    internal partial class Worker
    {
        #region "AppConfig" - wrapperz

        private int GetTimeoutNavigationLoginPage()
        {
            return this.WorkerConfig.GetAppConfig().GetTimeoutLoginPage();
        }

        private int GetTimeoutFindElement()
        {
            return this.WorkerConfig.GetAppConfig().GetTimeoutFindElement();
        }

        private int GetMaxRetriesForElementOperations()
        {
            return this.WorkerConfig.GetAppConfig().GetMaxRetriesForElementOperations();
        }

        private int GetTimeoutPopup()
        {
            return this.WorkerConfig.GetAppConfig().GetTimeoutPopup();
        }

        private int GetWaitBeforeLogout()
        {
            return this.WorkerConfig.GetAppConfig().GetWaitBeforeLogout();
        }

        private string GetApplicationIFrameXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetApplicationIFrameXPath();
        }

        private string GetLoginPageUsernameXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetLoginPageUsernameXPath();
        }

        private string GetLoginPagePasswordXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetLoginPagePasswordXPath();
        }

        private string GetLoginPageClientXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetLoginPageClientXPath();
        }

        private string GetLoginPageLanguageXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetLoginPageLanguageXPath();
        }

        private string GetLoginPageLoginButtonXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetLoginPageLoginButtonXPath();
        }

        private string GetMainPageIFrameXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetMainPageIFrameXPath();
        }

        private string GetMainPageWelcomeImageXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetMainPageWelcomeImageXPath();
        }

        private string GetNavMenuXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetNavMenuXPath();
        }

        private string GetNavMenuZeitbuchungXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetNavMenuZeitbuchungXPath();
        }

        private string GetNavMenuUsernameXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetNavMenuUsernameXPath();
        }

        private string GetNavMenuUsernameLogoutButtonXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetNavMenuUsernameLogoutButtonXPath();
        }

        private string GetDateGridFormXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetDateGridFormXPath();
        }

        private string GetDateGridCanvasXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetDateGridCanvasXPath();
        }

        private string GetDateGridCanvasSortingheaderXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetDateGridCanvasSortingheaderXPath();
        }

        private string GetDateGridCanvasSortingAscXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetDateGridCanvasSortingAscXPath();
        }

        private string GetDateGridCanvasSortingDescXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetDateGridCanvasSortingDescXPath();
        }

        private string GetDateGridCanvasLastRowXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetDateGridCanvasLastRowXPath();
        }

        private string GetDateGridCanvasLastRowDateFromXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetDateGridCanvasLastRowDateFromXPath();
        }

        private string GetDateGridCanvasLastRowDateToXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetDateGridCanvasLastRowDateToXPath();
        }

        private string GetTimePairTimeFromXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetTimePairTimeFromXPath();
        }

        private string GetTimePairTimeToXPath()
        {
            return this.WorkerConfig.GetAppConfig().GetTimePairTimeToXPath();
        }

        private string GetTimePairFooterAcceptButtonPath()
        {
            return this.WorkerConfig.GetAppConfig().GetTimePairFooterAcceptButtonPath();
        }
        
        private string GetDateGridCanvasSaveButtonPath()
        {
            return this.WorkerConfig.GetAppConfig().GetDateGridCanvasSaveButtonPath();
        }

        private string GetDateGridCanvasLoadButtonPath()
        {
            return this.WorkerConfig.GetAppConfig().GetDateGridCanvasLoadButtonPath();
        }

        #endregion

    } // class

} // namespace

//
// File: "Worker.AppConfig.cs"
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "1.0.0.0"
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

        private int GetTimeoutPopup()
        {
            return this.WorkerConfig.GetAppConfig().GetTimeoutPopup();
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
        
        #endregion "AppConfig" - wrapperz

    } // class

} // namespace

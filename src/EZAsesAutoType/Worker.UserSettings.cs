//
// File: "Worker.UserSettings.cs"
//
// Revision History: 
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

namespace EZAsesAutoType
{
    internal partial class Worker
    {
        #region "UserSetting" - wrapperz

        private string GetWebDriver()
        {
            return this.WorkerConfig.GetUserSettings().WebDriver;
        }

        private string GetASESBaseUrl()
        {
            return this.WorkerConfig.GetUserSettings().ASESBaseUrl;
        }

        private string GetASESUserId()
        {
            return this.WorkerConfig.GetUserSettings().ASESUserId;
        }

        private string GetASESPassword()
        {
            return this.WorkerConfig.GetUserSettings().ASESPassword;
        }

        private string GetASESClient()
        {
            return this.WorkerConfig.GetUserSettings().ASESClient;
        }

        private string GetASESLanguage()
        {
            return this.WorkerConfig.GetUserSettings().ASESLanguage;
        }

        private string GetASESPunchIn()
        {
            return this.WorkerConfig.GetUserSettings().ASESPunchIn;
        }

        private string GetASESPunchOut()
        {
            return this.WorkerConfig.GetUserSettings().ASESPunchOut;
        }

        #endregion

    } // class

} // namespace

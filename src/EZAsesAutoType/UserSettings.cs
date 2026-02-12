//
// File: "UserSettings.cs"
//
// Summary:
// Wrapper for "userSettings". 
//
// Revision History:
// 2026/02/12:TomislavMatas: v4.38.1450
// * Add UserSetting "UseSSO", tied to "checkBox_Sso".
// 2024/07/08:TomislavMatas: Version "1.126.4"
// * Add UserSetting "DoLogout".
// 2024/07/03:TomislavMatas: Version "1.126.2"
// * Add UserSetting "DoLogin" and "DoPunch".
// 2024/05/31:TomislavMatas: Version "1.126.0"
// * Simplify log4net implementations.
// 2024/04/13:TomislavMatas: Version "1.123.4"
// * Rename "ASESPunchIn"  to "ASESPunchInAM".
// * Rename "ASESPunchOut" to "ASESPunchOutAM".
// * Add "ASESPunchInPM" and "ASESPunchOutPM".
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System.Collections.Specialized;
using System.Diagnostics;
using log4net;

namespace EZAsesAutoType
{
    /// <summary>
    /// Wrapper for "userSettings".
    /// </summary>
    internal class UserSettings
    {
        #region log4net

        private static readonly ILog Log = LogManager.GetLogger(typeof(UserSettings));

        [Conditional("DEBUG")]
        private static void LogTrace(object message)
        {
#if DEBUG
            Log.Debug(message);
#endif
        }

        #endregion

        #region "userSettings"
        public string ASESBaseUrl;
        public string ASESUserId;
        public string ASESPassword;
        public string ASESClient;
        public StringCollection ASESClientList;
        public string ASESLanguage;
        public StringCollection ASESLanguageList;
        public string ASESPunchInAM;
        public string ASESPunchOutAM;
        public string ASESPunchInPM;
        public string ASESPunchOutPM;
        public string WebDriver;
        public StringCollection WebDriverList;
        /// <summary>
        /// Perform (at least) "Login" when "Run" button has been clicked.
        /// </summary>
        public bool DoLogin;
        /// <summary>
        /// Perform "Login" and "time punch" when "Run" button has been clicked.
        /// </summary>
        public bool DoPunch;
        /// <summary>
        /// Perform "Login", "time punch" and "Logout" when "Run" button has been clicked.
        /// </summary>
        public bool DoLogout;
        /// <summary>
        /// If a deviation value greater than 0 (zero) is set,
        /// than the punch in and punch out times will be 
        /// randomized up to a maximum of the sepcified value.
        /// The deviation shall help to generate organic looking
        /// punch in and punch out times.
        /// </summary>
        public int ASESPunchDeviation;
        /// <summary>
        /// Recently the company added automatically performed 
        /// "Single Sign-On (SSO)" as soon as the respective 
        /// base URL is accessed. This setting allows to control,
        /// if this app shall still try to type Username and Password
        /// into the respective fields, or if it shall just wait 
        /// for the SSO to complete.
        /// </summary>
        public bool UseSSO;
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserSettings() 
        {
            this.ASESBaseUrl = string.Empty;
            this.ASESUserId = string.Empty;
            this.ASESPassword = string.Empty;
            this.ASESClient = string.Empty;
            this.ASESClientList = [];
            this.ASESLanguage = string.Empty;
            this.ASESLanguageList = [];
            this.ASESPunchInAM = string.Empty;
            this.ASESPunchOutAM = string.Empty;
            this.ASESPunchInPM = string.Empty;
            this.ASESPunchOutPM = string.Empty;
            this.WebDriver = string.Empty;
            this.WebDriverList = [];
            this.DoLogin = true;
            this.DoPunch = true;
            this.DoLogout = true;
            this.ASESPunchDeviation = 60;
            this.UseSSO = true;
        }

        /// <summary>
        /// Default destructor.
        /// </summary>
        ~UserSettings()
        {
        }
    
        /// <summary>
        /// Load all "userSettings".
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            try
            {
                LogTrace(Const.LogStart);
                this.ASESBaseUrl        = Properties.Settings.Default.ASESBaseUrl;
                this.ASESUserId         = Properties.Settings.Default.ASESUserId;
                this.ASESPassword       = Properties.Settings.Default.ASESPassword;
                this.ASESClient         = Properties.Settings.Default.ASESClient;
                this.ASESClientList     = Properties.Settings.Default.ASESClientList;
                this.ASESLanguage       = Properties.Settings.Default.ASESLanguage;
                this.ASESLanguageList   = Properties.Settings.Default.ASESLanguageList;
                this.ASESPunchInAM      = Properties.Settings.Default.ASESPunchInAM;
                this.ASESPunchOutAM     = Properties.Settings.Default.ASESPunchOutAM;
                this.ASESPunchInPM      = Properties.Settings.Default.ASESPunchInPM;
                this.ASESPunchOutPM     = Properties.Settings.Default.ASESPunchOutPM;
                this.WebDriver          = Properties.Settings.Default.WebDriver;
                this.WebDriverList      = Properties.Settings.Default.WebDriverList;
                this.DoLogin            = Properties.Settings.Default.ASESDoLogin;
                this.DoPunch            = Properties.Settings.Default.ASESDoPunch;
                this.DoLogout           = Properties.Settings.Default.ASESDoLogout;
                this.ASESPunchDeviation = Properties.Settings.Default.ASESPunchDeviation;
                this.UseSSO             = Properties.Settings.Default.ASESUseSSO;
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
        /// Save all "userSettings".
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            try 
            { 
                Properties.Settings.Default.ASESBaseUrl        = this.ASESBaseUrl;
                Properties.Settings.Default.ASESUserId         = this.ASESUserId;
                Properties.Settings.Default.ASESPassword       = this.ASESPassword;
                Properties.Settings.Default.ASESClient         = this.ASESClient;
                Properties.Settings.Default.ASESClientList     = this.ASESClientList;
                Properties.Settings.Default.ASESLanguage       = this.ASESLanguage;
                Properties.Settings.Default.ASESLanguageList   = this.ASESLanguageList;
                Properties.Settings.Default.ASESPunchInAM      = this.ASESPunchInAM;
                Properties.Settings.Default.ASESPunchOutAM     = this.ASESPunchOutAM;
                Properties.Settings.Default.ASESPunchInPM      = this.ASESPunchInPM;
                Properties.Settings.Default.ASESPunchOutPM     = this.ASESPunchOutPM;
                Properties.Settings.Default.WebDriver          = this.WebDriver;
                Properties.Settings.Default.WebDriverList      = this.WebDriverList;
                Properties.Settings.Default.ASESDoLogin        = this.DoLogin;
                Properties.Settings.Default.ASESDoPunch        = this.DoPunch;
                Properties.Settings.Default.ASESDoLogout       = this.DoLogout;
                Properties.Settings.Default.ASESPunchDeviation = this.ASESPunchDeviation;
                Properties.Settings.Default.ASESUseSSO         = this.UseSSO;
                Properties.Settings.Default.Save();
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

    } // class

} // namespace

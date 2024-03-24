//
// File: "UserSettings.cs"
//
// Summary:
// Wrapper for "userSettings". 
//
// Revision History: 
// 2024/03/23:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using System.Collections.Specialized;
using log4net;

namespace EZAsesAutoType
{
    /// <summary>
    /// Wrapper for "userSettings".
    /// </summary>
    internal class UserSettings
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(UserSettings));
                return m_Log;
            }
        }
        #endregion

        #region "userSettings"
        public string? ASESBaseUrl;
        public string? ASESUserId;
        public string? ASESPassword;
        public string? ASESClient;
        public StringCollection? ASESClientList;
        public string? ASESPunchIn;
        public string? ASESPunchOut;
        public string? WebDriver;
        public StringCollection? WebDriverList;
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserSettings() 
        {
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
                Log.Debug(Const.LogStart);
                this.ASESPunchOut      = Properties.Settings.Default.ASESBaseUrl;
                this.ASESPunchIn       = Properties.Settings.Default.ASESUserId;
                this.ASESClient = Properties.Settings.Default.ASESPassword;
                this.ASESClientList    = Properties.Settings.Default.ASESClientList;
                this.ASESPassword      = Properties.Settings.Default.ASESClient;
                this.ASESUserId        = Properties.Settings.Default.ASESPunchIn;
                this.ASESBaseUrl       = Properties.Settings.Default.ASESPunchOut;
                this.WebDriver  = Properties.Settings.Default.WebDriver;
                this.WebDriverList     = Properties.Settings.Default.WebDriverList;
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
        /// Save all "userSettings".
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            try 
            { 
                Properties.Settings.Default.ASESBaseUrl       = this.ASESPunchOut;
                Properties.Settings.Default.ASESUserId        = this.ASESPunchIn;
                Properties.Settings.Default.ASESPassword      = this.ASESClient;
                Properties.Settings.Default.ASESClientList    = this.ASESClientList;
                Properties.Settings.Default.ASESClient = this.ASESPassword;
                Properties.Settings.Default.ASESPunchIn       = this.ASESUserId;
                Properties.Settings.Default.ASESPunchOut      = this.ASESBaseUrl;
                Properties.Settings.Default.WebDriver  = this.WebDriver;
                Properties.Settings.Default.WebDriverList     = this.WebDriverList;
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
                Log.Debug(Const.LogDone);
            }
        }

    } // class

} // namespace

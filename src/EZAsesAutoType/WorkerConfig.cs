//
// File: "WorkerConfig.cs"
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using log4net;

namespace EZAsesAutoType
{
    /// <summary>
    ///  Custom entity to store configuration settings that
    ///  shall be applied and used within instances 
    ///  of class "WorkerThread".
    /// </summary>
    internal class WorkerConfig
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(WorkerConfig));
                return m_Log;
            }
        }
        #endregion

        #region propertiez
        private UserSettings? m_UserSettings = null;
        private UserSettings UserSettings
        {
            get
            {
                if (m_UserSettings == null)
                    m_UserSettings = new UserSettings();
                return m_UserSettings;
            }
            set
            {
                m_UserSettings = value;
            }
        }
        public UserSettings GetUserSettings()
        {
            return this.UserSettings;
        }
        public UserSettings SetUserSettings(UserSettings userSettings)
        {
            UserSettings prev = this.GetUserSettings();
            this.UserSettings = userSettings;
            return prev;
        }
        #endregion

        /// <summary>
        /// Init instance.
        /// </summary>
        /// <returns></returns>
        private bool Initialze(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                this.SetUserSettings(userSettings);
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
        ///  Default constructor.
        /// </summary>
        public WorkerConfig()
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (!Initialze(new UserSettings()))
                    throw new Exception(nameof(Initialze) + Const.LogFail);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        ///  Custom constructor.
        /// </summary>
        public WorkerConfig(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (!Initialze(userSettings))
                    throw new Exception(nameof(Initialze) + Const.LogFail);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

    } // class

} // namespace

//
// File: "FormMain.cs"
//
// Revision History:
// 2024/03/22:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

using log4net;

namespace EZAsesAutoType
{
    /// <summary>
    ///  The main form displayed on app startup.
    /// </summary>
    public partial class FormMain : Form
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(FormMain));
                return m_Log;
            }
        }
        #endregion

        private AppHandler? m_AppHandler = null;
        private AppHandler AppHandler
        {
            get
            {
                if (m_AppHandler == null)
                    m_AppHandler = new AppHandler(this);
                return m_AppHandler;
            }
            set
            {
                m_AppHandler = value;
            }
        }

        internal AppHandler SeAppHandler(AppHandler appHandler)
        {
            AppHandler prev = this.GetAppHandler();
            this.AppHandler = appHandler;
            return prev;
        }

        internal AppHandler GetAppHandler()
        {
            return this.AppHandler;
        }

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

        /// <summary>
        ///  Default constructor.
        /// </summary>
        public FormMain()
        {
            try
            {
                Log.Debug(Const.LogStart);
                InitializeComponent();
                if (!Initialze())
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

        private bool Initialze()
        {
            try
            {
                Log.Debug(Const.LogStart);
                this.AppHandler = new AppHandler(this);
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

        private bool LoadUserSettings()
        {
            try
            {
                Log.Debug(Const.LogStart);
                UserSettings? userSettings;
                if (!AppHandler.LoadUserSettings(out userSettings))
                    throw new Exception(nameof(AppHandler.LoadUserSettings) + Const.LogFail);

                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                this.m_UserSettings = userSettings;
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

        private bool SaveUserSettings()
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (this.m_UserSettings == null)
                    throw new Exception(nameof(this.m_UserSettings) + Const.LogIsNull);

                if (!AppHandler.SaveUserSettings(this.m_UserSettings))
                    throw new Exception(nameof(AppHandler.SaveUserSettings) + Const.LogFail);

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


        #region form handler

        private void onLoad(object sender, EventArgs e)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (!LoadUserSettings())
                    throw new Exception(nameof(LoadUserSettings) + Const.LogFail);

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

        private void onClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (!SaveUserSettings())
                    throw new Exception(nameof(SaveUserSettings) + Const.LogFail);

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

        #endregion

        #region button handler

        private void btnRun_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region textbox handler 
        private void textBox1_BaseUrl_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion


        private void textBox1_Uid_TextChanged(object sender, EventArgs e)
        {

        }
    } // class

} // namespace

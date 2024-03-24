//
// File: "FormMain.cs"
//
// Revision History:
// 2024/03/22:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

using EZAsesAutoType;
using log4net;
using Microsoft.VisualBasic.Logging;
using System.Collections.Specialized;

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

        #region private propertiez
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
        private AppHandler GetAppHandler()
        {
            return this.AppHandler;
        }
        private AppHandler SetAppHandler(AppHandler appHandler)
        {
            AppHandler prev = this.GetAppHandler();
            this.AppHandler = appHandler;
            return prev;
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
        private UserSettings GetUserSettings()
        {
            return this.UserSettings;
        }
        private UserSettings SetUserSettings(UserSettings userSettings)
        {
            UserSettings prev = this.GetUserSettings();
            this.UserSettings = userSettings;
            return prev;
        }
        #endregion

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

        private UserSettings? GetUserSettingsFromGui()
        {
            try
            { 
                UserSettings userSettings      = new UserSettings();
                userSettings.ASESBaseUrl       = this.textBoxUrl.Text;
                userSettings.ASESUserId        = this.textBoxUid.Text;
                userSettings.ASESPassword      = this.textBoxPwd.Text;
                userSettings.ASESClientDefault = this.comboBoxClientNo.Text;
                userSettings.ASESPunchIn       = this.textBoxPunchIn.Text; 
                userSettings.ASESPunchOut      = this.textBoxPunchOut.Text;
                userSettings.WebDriverDefault  = this.comboBoxWebDriver.Text;
                return userSettings;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
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
                UserSettings? userSettings = this.GetUserSettings();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                // map only a subset of the usersettings
                UserSettings? userSettingsGui = this.GetUserSettingsFromGui();
                if (userSettingsGui == null)
                    throw new Exception(nameof(userSettingsGui) + Const.LogIsNull);


                if (!AppHandler.SaveUserSettings(userSettings))
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

        private bool InitializeComboBoxClientNo(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                StringCollection? itemList = userSettings.ASESClientList;
                if (itemList == null)
                    throw new Exception(nameof(itemList) + Const.LogIsNull);

                ComboBox comboBox = this.comboBoxClientNo;
                if (comboBox == null)
                    throw new Exception(nameof(comboBox) + Const.LogIsNull);

                comboBox.Items.Clear();
                foreach (string? item in itemList)
                    if (!string.IsNullOrEmpty(item))
                        this.comboBoxClientNo.Items.Add(item);
    
                string? defautValue = userSettings.ASESClientDefault;
                if (string.IsNullOrEmpty(defautValue))
                {
                    if (comboBox.Items.Count > 0)
                    {
                        object? firstItem = comboBox.Items[0];
                        if (firstItem != null)
                            defautValue = (string)firstItem;
                    }
                }
                comboBox.Text = defautValue;

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

        private bool InitializeComboBoxWebDriver(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                StringCollection? itemList = userSettings.WebDriverList;
                if (itemList == null)
                    throw new Exception(nameof(itemList) + Const.LogIsNull);

                ComboBox comboBox = this.comboBoxWebDriver;
                if (comboBox == null)
                    throw new Exception(nameof(comboBox) + Const.LogIsNull);

                comboBox.Items.Clear();
                foreach (string? item in itemList)
                    if (!string.IsNullOrEmpty(item))
                        comboBox.Items.Add(item);

                string? defautValue = userSettings.WebDriverDefault;
                if (string.IsNullOrEmpty(defautValue))
                {
                    if (comboBox.Items.Count > 0)
                    {
                        object? firstItem = comboBox.Items[0];
                        if (firstItem != null)
                            defautValue = (string)firstItem;
                    }
                }
                comboBox.Text = defautValue;

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

        private bool InitializeComboBoxes(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if(userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                if (!InitializeComboBoxClientNo(userSettings))
                    throw new Exception(nameof(InitializeComboBoxClientNo) + Const.LogFail);

                if(!InitializeComboBoxWebDriver(userSettings))
                    throw new Exception(nameof(InitializeComboBoxWebDriver) + Const.LogFail);
                
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

        private bool RenderControls()
        {
            try
            {
                Log.Debug(Const.LogStart);
                UserSettings? userSettings = this.GetUserSettings();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                
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

                UserSettings userSettings = this.GetUserSettings();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                if (!InitializeComboBoxes(userSettings))
                    throw new Exception(nameof(InitializeComboBoxes) + Const.LogFail);

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

    } // class

} // namespace

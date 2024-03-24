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

        /// <summary>
        /// Initialize instance.
        /// </summary>
        /// <returns></returns>
        private bool Initialze()
        {
            try
            {
                Log.Debug(Const.LogStart);
                this.SetAppHandler(new AppHandler(this));
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
                AppHandler? appHandler = this.GetAppHandler();
                if (appHandler == null)
                    throw new Exception(nameof(appHandler) + Const.LogIsNull);

                if (!appHandler.LoadUserSettings(out UserSettings? userSettings))
                    throw new Exception(nameof(appHandler.LoadUserSettings) + Const.LogFail);

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

        private StringCollection GetComboBoxItemsAsStringCollection(ComboBox comboBox)
        {
            Log.Debug(Const.LogStart);
            StringCollection stringCollection = new StringCollection();
            foreach (object item in comboBox.Items)
                if (item != null)
                    stringCollection.Add(item.ToString());

            return stringCollection;
        }

        private UserSettings? GetUserSettingsValuesFromControls()
        {
            try
            { 
                UserSettings userSettings      = new UserSettings();
                userSettings.ASESBaseUrl       = this.textBoxUrl.Text;
                userSettings.ASESClient = this.comboBoxClientNo.Text;
                userSettings.ASESClientList    = this.GetComboBoxItemsAsStringCollection(this.comboBoxClientNo);
                userSettings.ASESPassword      = this.textBoxPwd.Text;
                userSettings.ASESPunchIn       = this.textBoxPunchIn.Text; 
                userSettings.ASESPunchOut      = this.textBoxPunchOut.Text;
                userSettings.ASESUserId        = this.textBoxUid.Text;
                userSettings.WebDriver  = this.comboBoxWebDriver.Text;
                userSettings.WebDriverList     = this.GetComboBoxItemsAsStringCollection(this.comboBoxWebDriver);
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
                UserSettings? userSettings = this.GetUserSettingsValuesFromControls();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                AppHandler? appHandler = this.GetAppHandler();
                if (appHandler == null)
                    throw new Exception(nameof(appHandler) + Const.LogIsNull);

                if (appHandler.SaveUserSettings(userSettings))
                    throw new Exception(nameof(appHandler.SaveUserSettings) + Const.LogFail);

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
    
                string? defautValue = userSettings.ASESClient;
                if (!comboBox.Items.Contains(defautValue))
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

                string? defautValue = userSettings.WebDriver;
                if (!comboBox.Items.Contains(defautValue))
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

        private bool RenderWebDriverVersion(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                string? webDriver = userSettings.WebDriver;
                if (webDriver == null)
                    throw new Exception(nameof(webDriver) + Const.LogIsNull);

                string? webDriverVersion = EZSeleniumLib.WebDriverVersion.GetWebDriverVersionString(webDriver);
                if (webDriverVersion == null)
                    throw new Exception(nameof(webDriverVersion) + Const.LogIsNull);

                this.textBoxWebDriverVersion.Text = webDriverVersion;
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
        /// Render controls depending on their actual values.
        /// Individual controls layout and content might
        /// depend on others.
        /// </summary>
        /// <returns></returns>
        private bool RenderControls(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                if(!RenderWebDriverVersion(userSettings))
                    throw new Exception(nameof(RenderWebDriverVersion) + Const.LogFail);

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

                UserSettings? userSettings = this.GetUserSettings();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                if (!InitializeComboBoxes(userSettings))
                    throw new Exception(nameof(InitializeComboBoxes) + Const.LogFail);

                if(!RenderControls(userSettings))
                    throw new Exception(nameof(RenderControls) + Const.LogFail);

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

//
// File: "FormMain.cs"
//
// Revision History:
// 2024/03/22:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using System.Collections.Specialized;
using System.Text.RegularExpressions;

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

        private object m_LockCancelRequested = new object();
        private bool m_CancelRequested = false;
        private bool CancelRequested
        {
            get
            {
                lock (m_LockCancelRequested)
                {
                    return this.m_CancelRequested;
                }
            }
            set
            {
                lock (m_LockCancelRequested)
                {
                    this.m_CancelRequested = value;
                }
            }
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
                UserSettings userSettings = new UserSettings();
                userSettings.ASESBaseUrl = this.textBoxUrl.Text;
                userSettings.ASESUserId = this.textBoxUid.Text;
                userSettings.ASESPassword = this.textBoxPwd.Text;
                userSettings.ASESClient = this.comboBoxClientNo.Text;
                userSettings.ASESClientList = this.GetComboBoxItemsAsStringCollection(this.comboBoxClientNo);
                userSettings.ASESLanguage = this.comboBoxLanguage.Text;
                userSettings.ASESLanguageList = this.GetComboBoxItemsAsStringCollection(this.comboBoxLanguage);
                userSettings.ASESPunchIn = this.textBoxPunchIn.Text;
                userSettings.ASESPunchOut = this.textBoxPunchOut.Text;
                userSettings.WebDriver = this.comboBoxWebDriver.Text;
                userSettings.WebDriverList = this.GetComboBoxItemsAsStringCollection(this.comboBoxWebDriver);
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

        private bool InitializeComboBox(ComboBox comboBox, StringCollection itemList, string defaultValue)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (comboBox == null)
                    throw new ArgumentNullException(nameof(comboBox));

                if (itemList == null)
                    throw new ArgumentNullException(nameof(itemList));

                if (defaultValue == null)
                    throw new ArgumentNullException(nameof(defaultValue));

                comboBox.Items.Clear();
                foreach (string? item in itemList)
                    if (!string.IsNullOrEmpty(item))
                        comboBox.Items.Add(item);

                if (!comboBox.Items.Contains(defaultValue))
                {
                    if (comboBox.Items.Count > 0)
                    {
                        object? firstItem = comboBox.Items[0];
                        if (firstItem != null)
                            defaultValue = (string)firstItem;
                    }
                }
                comboBox.Text = defaultValue;
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

                if (this.comboBoxClientNo == null)
                    throw new Exception(nameof(this.comboBoxClientNo) + Const.LogIsNull);

                if (userSettings.ASESClientList == null)
                    throw new Exception(nameof(userSettings.ASESClientList) + Const.LogIsNull);

                if (userSettings.ASESClient == null)
                    throw new Exception(nameof(userSettings.ASESClient) + Const.LogIsNull);

                ComboBox comboBox = this.comboBoxClientNo;
                StringCollection? itemList = userSettings.ASESClientList;
                string? defaultValue = userSettings.ASESClient;
                if (!InitializeComboBox(comboBox, itemList, defaultValue))
                    throw new Exception(nameof(InitializeComboBox) + Const.LogFail);

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

        private bool InitializeComboBoxLanguage(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                if (this.comboBoxLanguage == null)
                    throw new Exception(nameof(this.comboBoxLanguage) + Const.LogIsNull);

                if (userSettings.ASESLanguageList == null)
                    throw new Exception(nameof(userSettings.ASESLanguageList) + Const.LogIsNull);

                if (userSettings.ASESLanguage == null)
                    throw new Exception(nameof(userSettings.ASESLanguage) + Const.LogIsNull);

                ComboBox comboBox = this.comboBoxLanguage;
                StringCollection? itemList = userSettings.ASESLanguageList;
                string? defaultValue = userSettings.ASESLanguage;
                if (!InitializeComboBox(comboBox, itemList, defaultValue))
                    throw new Exception(nameof(InitializeComboBox) + Const.LogFail);

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

                if (this.comboBoxWebDriver == null)
                    throw new Exception(nameof(this.comboBoxWebDriver) + Const.LogIsNull);

                if (userSettings.WebDriverList == null)
                    throw new Exception(nameof(userSettings.WebDriverList) + Const.LogIsNull);

                if (userSettings.WebDriver == null)
                    throw new Exception(nameof(userSettings.WebDriver) + Const.LogIsNull);

                ComboBox comboBox = this.comboBoxWebDriver;
                StringCollection? itemList = userSettings.WebDriverList;
                string? defaultValue = userSettings.WebDriver;
                if (!InitializeComboBox(comboBox, itemList, defaultValue))
                    throw new Exception(nameof(InitializeComboBox) + Const.LogFail);

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
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                if (!InitializeComboBoxClientNo(userSettings))
                    throw new Exception(nameof(InitializeComboBoxClientNo) + Const.LogFail);

                if (!InitializeComboBoxLanguage(userSettings))
                    throw new Exception(nameof(InitializeComboBoxLanguage) + Const.LogFail);

                if (!InitializeComboBoxWebDriver(userSettings))
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

        private bool RenderUrl(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                string? asesBaseUrl = userSettings.ASESBaseUrl;
                if (asesBaseUrl == null)
                    throw new Exception(nameof(asesBaseUrl) + Const.LogIsNull);

                asesBaseUrl = asesBaseUrl.Trim();
                if (asesBaseUrl.Length <= 0)
                    return true; // nothing to do

                string? asesClient = userSettings.ASESClient;
                if (asesClient == null)
                    throw new Exception(nameof(asesClient) + Const.LogIsNull);

                asesClient = asesClient.Trim();
                if (asesClient.Length < 2)
                    return true; // nothing to do

                string asesClientNo = asesClient.Substring(0, 2);
                string matchPattern = "ClientNo=..";
                asesBaseUrl = Regex.Replace(asesBaseUrl, matchPattern, "ClientNo=" + asesClientNo, RegexOptions.IgnoreCase);
                this.textBoxUrl.Text = asesBaseUrl;
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

        private void ComboBoxClientNoChanged()
        {
            try
            {
                Log.Debug(Const.LogStart);
                UserSettings? userSettings = GetUserSettingsValuesFromControls();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                if (!RenderUrl(userSettings))
                    throw new Exception(nameof(RenderWebDriverVersion) + Const.LogFail);

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

                this.labelWebDriverVersion.Text = webDriverVersion;
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
        private bool RenderControlsSettings(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                if (!RenderWebDriverVersion(userSettings))
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

        private void RenderControlsBusy(bool busy)
        {
            if (busy)
            {
                this.textBoxUrl.Enabled = false;
                this.textBoxUid.Enabled = false;
                this.textBoxPwd.Enabled = false;
                this.comboBoxClientNo.Enabled = false;
                this.comboBoxLanguage.Enabled = false;
                this.comboBoxWebDriver.Enabled = false;
                this.textBoxPunchIn.Enabled = false;
                this.textBoxPunchOut.Enabled = false;
                this.btnRun.Enabled = false;
                this.btnRun.Visible = false;
                this.btnCancel.Enabled = true;
                this.btnCancel.Visible = true;
                this.btnCancel.Focus();
                return;
            }
            this.textBoxUrl.Enabled = true;
            this.textBoxUid.Enabled = true;
            this.textBoxPwd.Enabled = true;
            this.comboBoxClientNo.Enabled = true;
            this.comboBoxLanguage.Enabled = true;
            this.comboBoxWebDriver.Enabled = true;
            this.textBoxPunchIn.Enabled = true;
            this.textBoxPunchOut.Enabled = true;
            this.btnRun.Enabled = true;
            this.btnRun.Visible = true;
            this.btnCancel.Enabled = false;
            this.btnCancel.Visible = false;
            this.btnRun.Focus();
        }

        #region form handlerz

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

                if (!RenderControlsSettings(userSettings))
                    throw new Exception(nameof(RenderControlsSettings) + Const.LogFail);

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

        private void onShown(object sender, EventArgs e)
        {
            this.RenderControlsBusy(false);
        }

        #endregion

        #region button handlerz

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                UserSettings? userSettings = this.GetUserSettingsValuesFromControls();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                this.CancelRequested = false;
                this.RenderControlsBusy(true);
                this.backgroundWorker1.RunWorkerAsync(userSettings);
                bool backroundWorkerIsBusy = this.backgroundWorker1.IsBusy;
                while (backroundWorkerIsBusy)
                {
                    Application.DoEvents();
                    if (this.CancelRequested)
                        backgroundWorker1.CancelAsync();

                    backroundWorkerIsBusy = this.backgroundWorker1.IsBusy;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                this.CancelRequested = false;
                this.RenderControlsBusy(false);
                Log.Debug(Const.LogDone);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.CancelRequested = true;
        }

        #endregion

        #region textbox handlerz

        #endregion

        #region combobox handlerz

        private void comboBoxClientNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxClientNoChanged();
        }

        private void comboBoxClientNo_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBoxClientNoChanged();
        }

        private void comboBoxClientNo_TextChanged(object sender, EventArgs e)
        {
            ComboBoxClientNoChanged();
        }

        private void comboBoxWebDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Log.Debug(Const.LogStart);
                UserSettings? userSettings = GetUserSettingsValuesFromControls();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                if (!RenderWebDriverVersion(userSettings))
                    throw new Exception(nameof(RenderWebDriverVersion) + Const.LogFail);

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

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (e == null)
                    throw new ArgumentNullException(nameof(e));

                if (e.Argument == null)
                    throw new Exception(nameof(e.Argument) + Const.LogIsNull);

                object? obj = e.Argument;
                if (obj.GetType() != typeof(UserSettings))
                    throw new Exception(nameof(obj) + Const.LogNotImpl);

                UserSettings userSettings = (UserSettings)obj;

                AppHandler? appHandler = this.GetAppHandler();
                if (appHandler == null)
                    throw new Exception(nameof(appHandler) + Const.LogIsNull);

                if (!appHandler.DoDailyPunch(userSettings))
                    throw new Exception(nameof(appHandler.DoDailyPunch) + Const.LogFail);
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

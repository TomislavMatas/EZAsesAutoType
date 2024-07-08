//
// File: "FormMain.cs"
//
// Revision History:
// 2024/07/08:TomislavMatas: Version "1.126.4"
// * Add new control "checkBox_DoLogout".
// 2024/07/03:TomislavMatas: Version "1.126.2"
// * Add new controls "checkBox_DoLogin" and "checkBox_DoPunch".
// 2024/05/31:TomislavMatas: Version "1.126.0"
// * Simplify log4net implementations.
// 2024/05/27:TomislavMatas: Version "1.126.0":
// * Add handling of commandline argument switch "/close".
// 2024/05/04:TomislavMatas: Version "1.125.0":
// * Add handling of commandline arguments in general and 
//   the switch "/run" in special.
// 2024/05/10:TomislavMatas: Version "1.125.0"
// * Enhance NULL value handling and validation.
// 2024/04/13:TomislavMatas: Version "1.123.4"
// * Rename "textBoxPunchIn"  to "textBoxPunchInAM".
// * Rename "textBoxPunchOut" to "textBoxPunchOutAM".
// * Add "textBoxPunchInPM" and "textBoxPunchOutPM".
// * Add function "SelectAll()".
// * Enhance function "EvalTimeByFragment()".
// * Add "Enter" and "Validated" handlers for the new 
//   text boxes "textBoxPunchInPM" and "textBoxPunchOutPM".
// * Add function "SetTitle()".
// 2024/04/04:TomislavMatas: Version "1.0.123"
// * BugFix in "SaveUserSettings".
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System.Collections.Specialized;
using System.Diagnostics;
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

        private static readonly ILog Log = LogManager.GetLogger(typeof(FormMain));

        [Conditional("DEBUG")]
        private static void LogTrace(object message)
        {
#if DEBUG
            Log.Debug(message);
#endif
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

        private AppConfig? m_AppConfig = null;
        private AppConfig AppConfig
        {
            get
            {
                if (m_AppConfig == null)
                    m_AppConfig = new AppConfig();
                return m_AppConfig;
            }
            set
            {
                m_AppConfig = value;
            }
        }
        private AppConfig GetAppConfig()
        {
            return this.AppConfig;
        }
        private AppConfig SetAppConfig(AppConfig appConfig)
        {
            AppConfig prev = this.GetAppConfig();
            this.AppConfig = appConfig;
            return prev;
        }

        private bool m_AlwaysOnTopInitialized = false;
        private bool m_AlwaysOnTop;
        private bool GetAlwaysOnTop()
        {
            if (!m_AlwaysOnTopInitialized)
            {
                m_AlwaysOnTop = this.GetAppConfig().GetMainDialogAlwaysOnTop();
                m_AlwaysOnTopInitialized = true;
            }
            return m_AlwaysOnTop;
        }

        #endregion

        #region constructorz 

        /// <summary>
        ///  Default constructor.
        /// </summary>
        public FormMain()
        {
            try
            {
                LogTrace(Const.LogStart);
                InitializeComponent();
                if (!Initialze(Program.GetArgs()))
                    throw new Exception(nameof(Initialze) + Const.LogFail);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        #endregion

        #region initializerz

        private void SetTitle()
        {
            try
            {
                this.Text = Const.AssemblyDisplayTitle;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        /// <summary>
        /// Initialize instance.
        /// </summary>
        /// <returns></returns>
        private bool Initialze(string[]? args = null)
        {
            try
            {
                LogTrace(Const.LogStart);
                this.SetTitle();
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
                LogTrace(Const.LogDone);
            }
        }

        private bool InitializeComboBox(ComboBox comboBox, StringCollection itemList, string defaultValue)
        {
            try
            {
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
            }
        }

        private bool InitializeComboBoxClientNo(UserSettings userSettings)
        {
            try
            {
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
            }
        }

        private bool InitializeComboBoxLanguage(UserSettings userSettings)
        {
            try
            {
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
            }
        }

        private bool InitializeComboBoxWebDriver(UserSettings userSettings)
        {
            try
            {
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
            }
        }

        private bool InitializeComboBoxes(UserSettings userSettings)
        {
            try
            {
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
            }
        }

        private bool InitializeTextBoxes(UserSettings userSettings)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                this.textBoxUrl.Text = userSettings.ASESBaseUrl;
                this.textBoxUid.Text = userSettings.ASESUserId;
                this.textBoxPwd.Text = userSettings.ASESPassword;
                this.textBoxPunchInAM.Text = userSettings.ASESPunchInAM;
                this.textBoxPunchOutAM.Text = userSettings.ASESPunchOutAM;
                this.textBoxPunchInPM.Text = userSettings.ASESPunchInPM;
                this.textBoxPunchOutPM.Text = userSettings.ASESPunchOutPM;

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

        private bool InitializeControls(UserSettings userSettings)
        {
            try
            {
                LogTrace(Const.LogStart);
                this.SuspendLayout();
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                if (!InitializeComboBoxes(userSettings))
                    throw new Exception(nameof(InitializeComboBoxes) + Const.LogFail);

                if (!InitializeTextBoxes(userSettings))
                    throw new Exception(nameof(InitializeTextBoxes) + Const.LogFail);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                this.ResumeLayout();
                LogTrace(Const.LogDone);
            }
        }

        #endregion

        #region "UserSettings" - Handlerz

        private bool LoadUserSettings()
        {
            try
            {
                LogTrace(Const.LogStart);
                if (!this.AppHandler.LoadUserSettings(out UserSettings? userSettings))
                    throw new Exception(nameof(this.AppHandler.LoadUserSettings) + Const.LogFail);

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
                LogTrace(Const.LogDone);
            }
        }

        private bool SaveUserSettings()
        {
            try
            {
                LogTrace(Const.LogStart);
                UserSettings? userSettings = this.GetUserSettingsValuesFromControls();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                if (!this.AppHandler.SaveUserSettings(userSettings))
                    throw new Exception(nameof(this.AppHandler.SaveUserSettings) + Const.LogFail);

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

        private StringCollection GetComboBoxItemsAsStringCollection(ComboBox comboBox)
        {
            LogTrace(Const.LogStart);
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
                userSettings.ASESPunchInAM = this.textBoxPunchInAM.Text;
                userSettings.ASESPunchOutAM = this.textBoxPunchOutAM.Text;
                userSettings.ASESPunchInPM = this.textBoxPunchInPM.Text;
                userSettings.ASESPunchOutPM = this.textBoxPunchOutPM.Text;
                userSettings.WebDriver = this.comboBoxWebDriver.Text;
                userSettings.WebDriverList = this.GetComboBoxItemsAsStringCollection(this.comboBoxWebDriver);
                userSettings.DoLogin = this.checkBox_DoLogin.Checked;
                userSettings.DoPunch = this.checkBox_DoPunch.Checked;
                userSettings.DoLogout = this.checkBox_DoLogout.Checked;
                return userSettings;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        #endregion

        #region rendererz

        private bool RenderUrl(UserSettings userSettings)
        {
            try
            {
                LogTrace(Const.LogStart);
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
                string matchPattern = Const.UrlParmClientNo + "..";
                asesBaseUrl = Regex.Replace(asesBaseUrl, matchPattern, Const.UrlParmClientNo + asesClientNo, RegexOptions.IgnoreCase);
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
                LogTrace(Const.LogDone);
            }
        }

        private void ComboBoxClientNoChanged()
        {
            try
            {
                LogTrace(Const.LogStart);
                UserSettings? userSettings = GetUserSettingsValuesFromControls();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                if (!RenderUrl(userSettings))
                    throw new Exception(nameof(RenderUrl) + Const.LogFail);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }

        }

        private bool RenderWebDriverVersion(UserSettings userSettings)
        {
            try
            {
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
            }
        }

        private bool RenderCeckBoxes(UserSettings userSettings)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                this.checkBox_DoLogin.Checked = userSettings.DoLogin;
                this.checkBox_DoPunch.Checked = userSettings.DoPunch;
                this.checkBox_DoLogout.Checked = userSettings.DoLogout;

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

        private bool RenderControlsDoLoginChanged(bool flag)
        {
            try
            {
                LogTrace(Const.LogStart);
                this.SuspendLayout();
                if (!flag)
                {
                    // "DoPunch" without "DoLogin" does not make sense.
                    // Change checked state for "DoPunch" when
                    // turning off "DoLogin".
                    this.checkBox_DoPunch.Checked = false;

                    // "DoLogout" without "DoLogin" does not make sense.
                    // Change checked state for "DoLogout" when
                    // turning off "DoLogin".
                    this.checkBox_DoLogout.Checked = false;

                    if (!RenderControlsDoPunchChanged(false))
                        throw new Exception(nameof(RenderControlsDoPunchChanged) + Const.LogFail);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                this.ResumeLayout();
                LogTrace(Const.LogDone);
            }
        }

        private bool RenderControlsDoPunchChanged(bool flag)
        {
            try
            {
                LogTrace(Const.LogStart);
                this.SuspendLayout();
                this.textBoxPunchInAM.Enabled = flag;
                this.textBoxPunchInPM.Enabled = flag;
                this.textBoxPunchOutAM.Enabled = flag;
                this.textBoxPunchOutPM.Enabled = flag;
                if (flag)
                {
                    // "DoPunch" without "DoLogin" does not make sense.
                    // Change checked state for "DoLogin" only when
                    // turning on "DoPunch".
                    this.checkBox_DoLogin.Checked = true;
                    if (!RenderControlsDoLoginChanged(true))
                        throw new Exception(nameof(RenderControlsDoLoginChanged) + Const.LogFail);
                }

                if (!flag)
                {
                    // if "DoPunch" has been "unchecked", there's no sense
                    // in performing "DoLogout".
                    this.checkBox_DoLogout.Checked = false;
                    if (!RenderControlsDoLogoutChanged(false))
                        throw new Exception(nameof(RenderControlsDoLogoutChanged) + Const.LogFail);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                this.ResumeLayout();
                LogTrace(Const.LogDone);
            }
        }

        private bool RenderControlsDoLogoutChanged(bool flag)
        {
            try
            {
                LogTrace(Const.LogStart);       
                this.SuspendLayout();
                this.textBoxPunchInAM.Enabled = flag;
                this.textBoxPunchInPM.Enabled = flag;
                this.textBoxPunchOutAM.Enabled = flag;
                this.textBoxPunchOutPM.Enabled = flag;
                if (flag)
                {
                    // If "DoLogout" has been checked, enable "DoLogin" and "DoPunch".
                    this.checkBox_DoLogin.Checked = true;
                    this.checkBox_DoPunch.Checked = true;

                    if (!RenderControlsDoLoginChanged(true))
                        throw new Exception(nameof(RenderControlsDoLoginChanged) + Const.LogFail);

                    if (!RenderControlsDoPunchChanged(true))
                        throw new Exception(nameof(RenderControlsDoPunchChanged) + Const.LogFail);
                }
                this.checkBox_DoLogout.Checked = flag;
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                this.ResumeLayout();
                LogTrace(Const.LogDone);
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
                LogTrace(Const.LogStart);
                this.SuspendLayout();
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                if (!RenderUrl(userSettings))
                    throw new Exception(nameof(RenderUrl) + Const.LogFail);

                if (!RenderWebDriverVersion(userSettings))
                    throw new Exception(nameof(RenderWebDriverVersion) + Const.LogFail);

                if (!RenderCeckBoxes(userSettings))
                    throw new Exception(nameof(RenderCeckBoxes) + Const.LogFail);

                if (!RenderControlsDoLoginChanged(userSettings.DoLogin))
                    throw new Exception(nameof(RenderControlsDoLoginChanged) + Const.LogFail);

                if (!RenderControlsDoPunchChanged(userSettings.DoPunch))
                    throw new Exception(nameof(RenderControlsDoPunchChanged) + Const.LogFail);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                this.ResumeLayout();
                LogTrace(Const.LogDone);
            }
        }

        private void RenderControlsWorkerStatus(bool busy)
        {
            try
            {
                LogTrace(Const.LogStart);
                this.SuspendLayout();
                if (busy)
                {
                    this.textBoxUrl.Enabled = false;
                    this.textBoxUid.Enabled = false;
                    this.textBoxPwd.Enabled = false;
                    this.comboBoxClientNo.Enabled = false;
                    this.comboBoxLanguage.Enabled = false;
                    this.comboBoxWebDriver.Enabled = false;
                    this.textBoxPunchInAM.Enabled = false;
                    this.textBoxPunchOutAM.Enabled = false;
                    this.textBoxPunchInPM.Enabled = false;
                    this.textBoxPunchOutPM.Enabled = false;
                    this.checkBox_DoLogin.Enabled = false;
                    this.checkBox_DoPunch.Enabled = false;
                    this.checkBox_DoLogout.Enabled = false;
                    this.btnRun.Enabled = false;
                    this.btnRun.Visible = true;
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
                this.textBoxPunchInAM.Enabled = true;
                this.textBoxPunchOutAM.Enabled = true;
                this.textBoxPunchInPM.Enabled = true;
                this.textBoxPunchOutPM.Enabled = true;
                this.checkBox_DoLogin.Enabled = true;
                this.checkBox_DoPunch.Enabled = true;
                this.checkBox_DoLogout.Enabled = true;
                this.btnRun.Enabled = true;
                this.btnRun.Visible = true;
                this.btnCancel.Enabled = true;
                this.btnCancel.Visible = true;
                this.btnRun.Focus();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                this.ResumeLayout();
                LogTrace(Const.LogDone);
            }
        }

        #endregion

        #region automations

        /// <summary>
        /// Check if command line argument "/Run" has been provided.
        /// </summary>
        /// <returns></returns>
        private bool IsArgRunProvided()
        {
            return Program.IsArgProvided(Const.CommandlineArg_Run);
        }

        /// <summary>
        /// Check if command line argument "/Close" has been provided.
        /// </summary>
        /// <returns></returns>
        private bool IsArgCloseProvided()
        {
            return Program.IsArgProvided(Const.CommandlineArg_Close);
        }

        /// <summary>
        /// Check if command line argument "/DoLogin" has been provided.
        /// </summary>
        /// <returns></returns>
        private bool IsArgDoLoginProvided()
        {
            return Program.IsArgProvided(Const.CommandlineArg_DoLogin);
        }

        /// <summary>
        /// Check if command line argument "/DoPunch" has been provided.
        /// </summary>
        /// <returns></returns>
        private bool IsArgDoPunchProvided()
        {
            return Program.IsArgProvided(Const.CommandlineArg_DoPunch);
        }

        /// <summary>
        /// Check if command line argument "/DoLogout" has been provided.
        /// </summary>
        /// <returns></returns>
        private bool IsArgDoLogoutProvided()
        {
            return Program.IsArgProvided(Const.CommandlineArg_DoLogout);
        }

        private void Run()
        {
            try
            {
                this.SaveUserSettings();

                UserSettings? userSettings = this.GetUserSettingsValuesFromControls();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                Global.SetCancelRequested(false);
                this.RenderControlsWorkerStatus(true);
                this.backgroundWorker1.RunWorkerAsync(userSettings);
                bool backroundWorkerIsBusy = this.backgroundWorker1.IsBusy;
                while (backroundWorkerIsBusy)
                {
                    if (GetAlwaysOnTop())
                        this.Activate();

                    Application.DoEvents();
                    if (Global.GetCancelRequested())
                        backgroundWorker1.CancelAsync();

                    Thread.Sleep(1000);
                    backroundWorkerIsBusy = this.backgroundWorker1.IsBusy;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                this.RenderControlsWorkerStatus(false);
                LogTrace(Const.LogDone);
            }
        }

        /// <summary>
        /// Kick of processing by spawning a background task
        /// using implementation in "this.Run()".
        /// </summary>
        private void RunOnLoad()
        {
            try
            {
                this.Show();
                Application.DoEvents();
                this.Refresh();
                this.Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        private void CloseAfterRun()
        {
            try
            {
                this.Show();
                Application.DoEvents();
                this.Refresh();
                this.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        #endregion 

        #region form handlerz

        private void onLoad(object sender, EventArgs e)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (!LoadUserSettings())
                    throw new Exception(nameof(LoadUserSettings) + Const.LogFail);

                UserSettings? userSettings = this.GetUserSettings();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                bool doAutomationOnLoad = false;
                if (this.IsArgDoLoginProvided())
                {
                    this.UserSettings.DoLogin = true;
                    this.UserSettings.DoPunch = false;
                    this.UserSettings.DoLogout = false;
                    doAutomationOnLoad = true;
                }

                if (this.IsArgDoPunchProvided())
                {
                    this.UserSettings.DoLogin = true;
                    this.UserSettings.DoPunch = true;
                    this.UserSettings.DoLogout = false;
                    doAutomationOnLoad = true;
                }

                if (this.IsArgDoLogoutProvided())
                {
                    this.UserSettings.DoLogin = true;
                    this.UserSettings.DoPunch = true;
                    this.UserSettings.DoLogout = true;
                    doAutomationOnLoad = true;
                }

                if (this.IsArgRunProvided())
                {
                    this.UserSettings.DoLogin = true;
                    this.UserSettings.DoPunch = true;
                    this.UserSettings.DoLogout = true;
                    doAutomationOnLoad = true;
                }

                if (!InitializeControls(userSettings))
                    throw new Exception(nameof(InitializeControls) + Const.LogFail);

                if (!RenderControls(userSettings))
                    throw new Exception(nameof(RenderControls) + Const.LogFail);

                if (doAutomationOnLoad)
                {
                    this.RunOnLoad();

                    // although "Run" kicks of a background task,
                    // control will be passed back to this point after
                    // processing (regardless if successful or not)
                    // or if user hits the cancel button.
                    if (this.IsArgCloseProvided())
                    {
                        if (!Global.GetCancelRequested())
                        {   // startup argument "/Close" has been passed in
                            // and processing has _NOT_ been canceled by user,
                            // close the app by closing the main form.
                            this.CloseAfterRun();
                        }
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        private void onClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (!SaveUserSettings())
                    throw new Exception(nameof(SaveUserSettings) + Const.LogFail);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        private void onShown(object sender, EventArgs e)
        {
            try
            {
                LogTrace(Const.LogStart);
                RenderControlsWorkerStatus(false);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        #endregion

        #region button handlerz

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                this.Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                this.RenderControlsWorkerStatus(false);
                LogTrace(Const.LogDone);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!this.backgroundWorker1.IsBusy)
                this.Close();

            while (this.backgroundWorker1.IsBusy)
            {
                Global.SetCancelRequested(true);
                backgroundWorker1.CancelAsync();
                Application.DoEvents();
            }
        }

        #endregion

        #region textbox handlerz

        private string? EvalTimeByFragment(string value)
        {
            if (value == null)
                return null;

            value = value.Trim();
            if (string.Equals(value, "now", StringComparison.OrdinalIgnoreCase)
              || string.Equals(value, ".")
               )
            {
                return DateTime.Now.ToString("HH:mm");
            }

            if (string.Equals(value, "nine", StringComparison.OrdinalIgnoreCase))
                return "09:00";

            if (string.Equals(value, "five", StringComparison.OrdinalIgnoreCase))
                return "17:00";

            if (value.Length == 1)
            {
                if (int.TryParse(value, out int intValue))
                {
                    return intValue.ToString("00") + ":00";
                }
            }

            if (value.Length == 2)
            {
                if (int.TryParse(value, out int intValue))
                {
                    if (intValue >= 10 && intValue <= 23)
                    {
                        return intValue.ToString("00") + ":00";
                    }
                    if (intValue > 23 & intValue <= 59)
                    {
                        return DateTime.Now.ToString("HH") + ":" + intValue.ToString("00");
                    }
                }
            }

            if (value.Length == 3)
            {
                if (string.Equals(value.Substring(1, 1), ":"))
                {
                    // "a:b"
                    string hour = value.Substring(0, 1);
                    string minute = value.Substring(2, 1);
                    if (int.TryParse(hour, out int intHour)
                      && int.TryParse(minute, out int intMinute)
                       )
                    {
                        // "h:m" --> "HH:mm"
                        return intHour.ToString("00") + ":" + intMinute.ToString("00");
                    }
                }
                if (int.TryParse(value, out int intValue))
                {
                    // "nnn"
                    if (intValue == 0)
                    {
                        // "000" --> "00:00"
                        return "00:00";
                    }
                    if (intValue <= 23)
                    {
                        // assume "hour" portion with either one or two leading zeroes.
                        return intValue.ToString("00") + ":00";
                    }
                    if (intValue > 23 && intValue <= 59)
                    {
                        // assume "minute" portion with either one or two leading zeroes.
                        // prefix result with current hour portion.
                        return DateTime.Now.ToString("HH") + ":" + intValue.ToString("00");
                    }
                    if (intValue >= 100 && intValue <= 959)
                    {
                        string hour = value.Substring(0, 1);
                        string minute = value.Substring(1, 2);
                        if (int.TryParse(hour, out int intHour)
                          && int.TryParse(minute, out int intMinute)
                           )
                        {
                            return intHour.ToString("00") + ":" + intMinute.ToString("00");
                        }

                    }
                }
            }
            if (value.Length == 4)
            {
                if (string.Equals(value.Substring(1, 1), ":"))
                {
                    // "a:bc"
                    string hour = value.Substring(0, 1);
                    string minute = value.Substring(2, 2);
                    if (int.TryParse(hour, out int intHour)
                      && int.TryParse(minute, out int intMinute)
                       )
                    {
                        if (intMinute >= 0 && intMinute <= 59)
                        {
                            // "h:mm" --> "HH:mm"
                            return intHour.ToString("00") + ":" + intMinute.ToString("00");
                        }
                    }
                }
                if (string.Equals(value.Substring(2, 1), ":"))
                {
                    // "ab:c"
                    string hour = value.Substring(0, 2);
                    string minute = value.Substring(3, 1);
                    if (int.TryParse(hour, out int intHour)
                      && int.TryParse(minute, out int intMinute)
                       )
                    {
                        if (intHour >= 0 && intHour <= 23)
                        {
                            // "hh:m" --> "HH:mm"
                            return intHour.ToString("00") + ":" + intMinute.ToString("00");
                        }
                    }
                }
                if (int.TryParse(value, out int intValue))
                {
                    // "nnnn"
                    if (intValue == 0)
                    {
                        // "0000" --> "00:00"
                        return "00:00";
                    }
                    string hour = value.Substring(0, 2);
                    string minute = value.Substring(2, 2);
                    if (int.TryParse(hour, out int intHour)
                      && int.TryParse(minute, out int intMinute)
                       )
                    {
                        if (intHour >= 0 && intHour <= 23
                         && intMinute >= 0 && intMinute <= 59)
                        {
                            // "hhmm" --> "HH:mm"
                            return intHour.ToString("00") + ":" + intMinute.ToString("00");
                        }
                    }
                }
            }
            if (value.Length == 5)
            {
                if (string.Equals(value.Substring(2, 1), ":"))
                {
                    // "ab:cd"
                    string hour = value.Substring(0, 2);
                    string minute = value.Substring(3, 2);
                    if (int.TryParse(hour, out int intHour)
                      && int.TryParse(minute, out int intMinute)
                       )
                    {
                        if (intHour >= 0 && intHour <= 23
                         && intMinute >= 0 && intMinute <= 59)
                        {
                            // "hh:mm" --> "HH:mm"
                            return intHour.ToString("00") + ":" + intMinute.ToString("00");
                        }
                    }
                }
                if (int.TryParse(value, out int intValue))
                {
                    // "nnnnn"
                    if (intValue == 0)
                    {
                        // "00000" --> "00:00"
                        return "00:00";
                    }
                }
            }

            if (TimeOnly.TryParse(value, out TimeOnly timeOnly))
                return timeOnly.ToString("HH:mm");

            // if none of the above "matched", return string.empty.
            return string.Empty;
        }

        private void SelectAll(TextBox textBox)
        {
            if (textBox == null)
                return;

            textBox.SelectAll();
        }

        #region "textBoxPunchInAM"

        /// <summary>
        /// Handle "got focus" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPunchInAM_Enter(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            if (sender is not TextBox)
                return;

            this.SelectAll((TextBox)sender);
        }

        /// <summary>
        /// Handle "value has changed" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPunchInAM_Validated(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            if (sender is not TextBox)
                return;

            TextBox textBox = (TextBox)sender;
            string fragment = textBox.Text;
            string? expanded = this.EvalTimeByFragment(fragment);
            if (string.Equals(fragment, expanded))
                return;

            textBox.Text = expanded;
        }

        #endregion

        #region "textBoxPunchOutAM"

        /// <summary>
        /// Handle "got focus" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPunchOutAM_Enter(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            if (sender is not TextBox)
                return;

            this.SelectAll((TextBox)sender);
        }

        /// <summary>
        /// Handle "value has changed" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPunchOutAM_Validated(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            if (sender is not TextBox)
                return;

            TextBox textBox = (TextBox)sender;
            string fragment = textBox.Text;
            string? expanded = this.EvalTimeByFragment(fragment);
            if (string.Equals(fragment, expanded))
                return;

            textBox.Text = expanded;
        }

        #endregion

        #region "textBoxPunchInPM"

        /// <summary>
        /// Handle "got focus" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPunchInPM_Enter(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            if (sender is not TextBox)
                return;

            this.SelectAll((TextBox)sender);
        }

        /// <summary>
        /// Handle "value has changed" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPunchInPM_Validated(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            if (sender is not TextBox)
                return;

            TextBox textBox = (TextBox)sender;
            string fragment = textBox.Text;
            string? expanded = this.EvalTimeByFragment(fragment);
            if (string.Equals(fragment, expanded))
                return;

            textBox.Text = expanded;
        }

        #endregion

        #region "textBoxPunchOutPM"

        /// <summary>
        /// Handle "got focus" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPunchOutPM_Enter(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            if (sender is not TextBox)
                return;

            this.SelectAll((TextBox)sender);
        }

        /// <summary>
        /// Handle "value has changed" event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPunchOutPM_Validated(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            if (sender is not TextBox)
                return;

            TextBox textBox = (TextBox)sender;
            string fragment = textBox.Text;
            string? expanded = this.EvalTimeByFragment(fragment);
            if (string.Equals(fragment, expanded))
                return;

            textBox.Text = expanded;
        }

        #endregion

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
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
            }
        }

        private void checkBox_DoLogin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogTrace(Const.LogStart);
                UserSettings? userSettings = GetUserSettingsValuesFromControls();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                if (!RenderCeckBoxes(userSettings))
                    throw new Exception(nameof(RenderCeckBoxes) + Const.LogFail);

                if (!RenderControlsDoLoginChanged(userSettings.DoLogin))
                    throw new Exception(nameof(RenderControlsDoLoginChanged) + Const.LogFail);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        private void checkBox_DoPunch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogTrace(Const.LogStart);
                UserSettings? userSettings = GetUserSettingsValuesFromControls();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                if (!RenderCeckBoxes(userSettings))
                    throw new Exception(nameof(RenderCeckBoxes) + Const.LogFail);

                if (!RenderControlsDoPunchChanged(userSettings.DoPunch))
                    throw new Exception(nameof(RenderControlsDoPunchChanged) + Const.LogFail);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        private void checkBox_DoLogout_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogTrace(Const.LogStart);
                UserSettings? userSettings = GetUserSettingsValuesFromControls();
                if (userSettings == null)
                    throw new Exception(nameof(userSettings) + Const.LogIsNull);

                if (!RenderControlsDoLogoutChanged(userSettings.DoLogout))
                    throw new Exception(nameof(RenderControlsDoLogoutChanged) + Const.LogFail);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        #endregion

        #region background worker taskz

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (e == null)
                    throw new ArgumentNullException(nameof(e));

                if (e.Argument == null)
                    throw new Exception(nameof(e.Argument) + Const.LogIsNull);

                object? obj = e.Argument;
                if (obj.GetType() != typeof(UserSettings))
                    throw new Exception(nameof(obj) + Const.LogNotImpl);

                UserSettings userSettings = (UserSettings)obj;

                AppHandler appHandler = this.GetAppHandler();
                if (!appHandler.DoDailyPunch(userSettings))
                    throw new Exception(nameof(appHandler.DoDailyPunch) + Const.LogFail);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        #endregion


    } // class

} // namespace

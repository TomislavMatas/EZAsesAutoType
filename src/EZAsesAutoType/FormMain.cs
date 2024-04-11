//
// File: "FormMain.cs"
//
// Revision History:
// 2024/04/14:TomislavMatas: Version "1.123.4.0"
// * Rename "textBoxPunchIn"  to "textBoxPunchInAM".
// * Rename "textBoxPunchOut" to "textBoxPunchOutAM".
// * Add "textBoxPunchInPM" and "textBoxPunchOutPM".
// 2024/04/04:TomislavMatas: Version "1.0.123"
// * BugFix in "SaveUserSettings".
// 2024/04/04:TomislavMatas: Version "1.0.0"
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

        #endregion

        #region constructorz 
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

        #endregion

        #region initializerz

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

        private bool InitializeTextBoxes(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (userSettings == null)
                    throw new ArgumentNullException(nameof(userSettings));

                this.textBoxUrl.Text = userSettings.ASESBaseUrl;
                this.textBoxUid.Text = userSettings.ASESUserId;
                this.textBoxPwd.Text = userSettings.ASESPassword;
                this.textBoxPunchInAM.Text  = userSettings.ASESPunchInAM;
                this.textBoxPunchOutAM.Text = userSettings.ASESPunchOutAM;
                this.textBoxPunchInPM.Text  = userSettings.ASESPunchInPM;
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
                Log.Debug(Const.LogDone);
            }
        }

        private bool InitializeControls(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
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
                Log.Debug(Const.LogDone);
            }
        }

        #endregion

        #region "UserSettings" - Handlerz

        private bool LoadUserSettings()
        {
            try
            {
                Log.Debug(Const.LogStart);
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
                userSettings.ASESBaseUrl      = this.textBoxUrl.Text;
                userSettings.ASESUserId       = this.textBoxUid.Text;
                userSettings.ASESPassword     = this.textBoxPwd.Text;
                userSettings.ASESClient       = this.comboBoxClientNo.Text;
                userSettings.ASESClientList   = this.GetComboBoxItemsAsStringCollection(this.comboBoxClientNo);
                userSettings.ASESLanguage     = this.comboBoxLanguage.Text;
                userSettings.ASESLanguageList = this.GetComboBoxItemsAsStringCollection(this.comboBoxLanguage);
                userSettings.ASESPunchInAM    = this.textBoxPunchInAM.Text;
                userSettings.ASESPunchOutAM   = this.textBoxPunchOutAM.Text;
                userSettings.ASESPunchInPM    = this.textBoxPunchInPM.Text;
                userSettings.ASESPunchOutPM   = this.textBoxPunchOutPM.Text;
                userSettings.WebDriver        = this.comboBoxWebDriver.Text;
                userSettings.WebDriverList    = this.GetComboBoxItemsAsStringCollection(this.comboBoxWebDriver);
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

        #endregion

        #region rendererz

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
        private bool RenderControls(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                this.SuspendLayout();
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
                this.ResumeLayout();
                Log.Debug(Const.LogDone);
            }
        }

        private void RenderControlsWorkerStatus(bool busy)
        {
            try
            {
                Log.Debug(Const.LogStart);
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
                Log.Debug(Const.LogDone);
            }
        }

        #endregion 

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

                if (!InitializeControls(userSettings))
                    throw new Exception(nameof(InitializeControls) + Const.LogFail);

                if (!RenderControls(userSettings))
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

        private void onShown(object sender, EventArgs e)
        {
            try
            {
                Log.Debug(Const.LogStart);
                RenderControlsWorkerStatus(false);
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

        #region button handlerz

        private void btnRun_Click(object sender, EventArgs e)
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
                    Application.DoEvents();
                    if (Global.GetCancelRequested())
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
                this.RenderControlsWorkerStatus(false);
                Log.Debug(Const.LogDone);
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

        private string EvalTimeByFragment(string value)
        {
            if (value == null)
                return null;

            value = value.Trim();
            if ( string.Equals(value, "now", StringComparison.OrdinalIgnoreCase)
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
                if(int.TryParse(value, out int intValue))
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
                    if ( int.TryParse(hour, out int intHour) 
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
                    if (intValue == 0 )
                    {
                        // "000" --> "00:00"
                        return "00:00";
                    }
                    if (intValue <= 23)
                    {
                        // assume "hour" portion with either one or two leading zeroes.
                        return intValue.ToString("00") + ":00";
                    }
                    if (intValue > 23 && intValue <= 59 )
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
                         && intMinute >=0 && intMinute <=59 )
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

            // if none of the above "matched", return unmodified value.
            return value;
        }

        private void textBoxPunchInAM_TextChanged(object sender, EventArgs e)
        {
            if (sender == null)
                return;
        }

        private void textBoxPunchInAM_Validated(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            if (sender is not TextBox)
                return;

            TextBox textBox = (TextBox)sender;
            string fragment = textBox.Text;
            string expanded = this.EvalTimeByFragment(fragment);
            if (string.Equals(fragment, expanded))
                return;

            this.textBoxPunchInAM.Text = expanded;
        }

        private void textBoxPunchOutAM_TextChanged(object sender, EventArgs e)
        {
            if (sender == null)
                return;
        }

        private void textBoxPunchOutAM_Validated(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            if (sender is not TextBox)
                return;

            TextBox textBox = (TextBox)sender;
            string fragment = textBox.Text;
            string expanded = this.EvalTimeByFragment(fragment);
            if (string.Equals(fragment, expanded))
                return;

            this.textBoxPunchOutAM.Text = expanded;
        }

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

        #region background worker taskz

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

                if (!this.AppHandler.DoDailyPunch(userSettings))
                    throw new Exception(nameof(this.AppHandler.DoDailyPunch) + Const.LogFail);
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

    } // class

} // namespace

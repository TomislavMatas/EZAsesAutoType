//
// File: AppConfig.cs
//
// Summary:
// Project specific configuration settings. 
//
// Revision History: 
// 2024/07/02:TomislavMatas: Version "1.126.2"
// * Rename "ASES.TimeGrid.*" to "ASES.DateGrid.*".
// * Rename "ASES.TimePair.FirstRow.*" to "ASES.TimePair.*".
// * Add new property "MaxRetriesElementOperations".
// 2024/05/31:TomislavMatas: Version "1.126.0"
// * Simplify log4net implementations.
// 2024/05/26:TomislavMatas: Version "1.126.0"
// * Increase default timeout values.
// 2024/04/04:TomislavMatas: Version "1.0.123"
// * Use "WaitBeforeLogoutDefault=2" instead of ""WaitBeforeLogoutDefault=5".
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

namespace EZAsesAutoType
{
    /// <summary>
    /// Access app specific configuration settings from "App.config" file.
    /// </summary>
    internal class AppConfig
    {
        #region propertiez

        private const string MainDialogAlwaysOnTopKeyName = "EZAsesAutoType.MainDialog.AlwaysOnTop";
        private const bool MainDialogAlwaysOnTopDefault = false;
        private bool m_MainDialogAlwaysOnTopInitialized = false;
        private bool m_MainDialogAlwaysOnTop;
        private bool MainDialogAlwaysOnTop
        {
            get
            {
                if (!m_MainDialogAlwaysOnTopInitialized)
                {
                    m_MainDialogAlwaysOnTop = ConfigApi.GetAppSettingBool(MainDialogAlwaysOnTopKeyName, MainDialogAlwaysOnTopDefault);
                    m_MainDialogAlwaysOnTopInitialized = true;
                }
                return m_MainDialogAlwaysOnTop;
            }
            set
            {
                m_MainDialogAlwaysOnTop = value;
                m_MainDialogAlwaysOnTopInitialized = true;

            }
        }
        public bool GetMainDialogAlwaysOnTop()
        {
            return this.MainDialogAlwaysOnTop;
        }
        public bool SetMainDialogAlwaysOnTop(bool value)
        {
            bool prev = this.GetMainDialogAlwaysOnTop();
            this.MainDialogAlwaysOnTop = value;
            return prev;
        }

        private const string TimeoutLoginPageKeyName = "ASES.Timeout.LoginPage";
        private const int TimeoutLoginPageDefault = 15;
        private int m_TimeoutLoginPage = -1;
        private int TimeoutLoginPage
        {
            get
            {
                if (m_TimeoutLoginPage == -1)
                    m_TimeoutLoginPage = ConfigApi.GetAppSettingInt(TimeoutLoginPageKeyName, TimeoutLoginPageDefault);
                return m_TimeoutLoginPage;
            }
            set
            {
                m_TimeoutLoginPage = value;
            }
        }
        public int GetTimeoutLoginPage()
        {
            return this.TimeoutLoginPage;
        }
        public int SetTimeoutLoginPage(int value)
        {
            int prev = this.GetTimeoutLoginPage();
            this.TimeoutLoginPage = value;
            return prev;
        }

        private const string TimeoutFindElementKeyName = "ASES.Timeout.FindElement";
        private const int TimeoutFindElementDefault = 15;
        private int m_TimeoutFindElement = -1;
        private int TimeoutFindElement
        {
            get
            {
                if (m_TimeoutFindElement == -1)
                    m_TimeoutFindElement = ConfigApi.GetAppSettingInt(TimeoutFindElementKeyName, TimeoutFindElementDefault);
                return m_TimeoutFindElement;
            }
            set
            {
                m_TimeoutFindElement = value;
            }
        }
        public int GetTimeoutFindElement()
        {
            return this.TimeoutFindElement;
        }
        public int SetTimeoutFindElement(int value)
        {
            int prev = this.GetTimeoutFindElement();
            this.TimeoutFindElement = value;    
            return prev;
        }

        private const string MaxRetriesElementOperationsKeyName = "ASES.MaxRetries.ElementOperations";
        private const int MaxRetriesElementOperationsDefault = 5;
        private int m_MaxRetriesElementOperations = -1;
        private int MaxRetriesElementOperations
        {
            get
            {
                if (m_MaxRetriesElementOperations == -1)
                    m_MaxRetriesElementOperations = ConfigApi.GetAppSettingInt(MaxRetriesElementOperationsKeyName, MaxRetriesElementOperationsDefault);
                return m_MaxRetriesElementOperations;
            }
            set
            {
                m_MaxRetriesElementOperations = value;
            }
        }
        public int GetMaxRetriesForElementOperations()
        {
            return this.MaxRetriesElementOperations;
        }
        public int SetMaxRetriesForElementOperations(int value)
        {
            int prev = this.GetMaxRetriesForElementOperations();
            this.MaxRetriesElementOperations = value;
            return prev;
        }

        private const string TimeoutPopupKeyName = "ASES.Timeout.Popup";
        private const int TimeoutPopupDefault = 15;
        private int m_TimeoutPopup = -1;
        private int TimeoutPopup
        {
            get
            {
                if (m_TimeoutPopup == -1)
                    m_TimeoutPopup = ConfigApi.GetAppSettingInt(TimeoutPopupKeyName, TimeoutPopupDefault);
                return m_TimeoutPopup;
            }
            set
            {
                m_TimeoutPopup = value;
            }
        }
        public int GetTimeoutPopup()
        {
            return this.TimeoutPopup;
        }
        public int SetTimeoutPopup(int value)
        {
            int prev = this.GetTimeoutPopup();
            this.TimeoutPopup = value;
            return prev;
        }
        
        private const string WaitBeforeLogoutKeyName = "ASES.WaitBefore.Logout";
        private const int WaitBeforeLogoutDefault = 2;
        private int m_WaitBeforeLogout = -1;
        private int WaitBeforeLogout
        {
            get
            {
                if (m_WaitBeforeLogout == -1)
                    m_WaitBeforeLogout = ConfigApi.GetAppSettingInt(WaitBeforeLogoutKeyName, WaitBeforeLogoutDefault);
                return m_WaitBeforeLogout;
            }
            set
            {
                m_WaitBeforeLogout = value;
            }
        }
        public int GetWaitBeforeLogout()
        {
            return this.WaitBeforeLogout;
        }
        public int SetWaitBeforeLogout(int value)
        {
            int prev = this.GetWaitBeforeLogout();
            this.WaitBeforeLogout = value;
            return prev;
        }

        private const string ApplicationIFrameXPathKeyName = "ASES.Application.iFrame.xPath";
        private string? m_ApplicationIFrameXPath = null;
        private string ApplicationIFrameXPath
        {
            get
            {
                if (m_ApplicationIFrameXPath == null)
                    m_ApplicationIFrameXPath = ConfigApi.GetAppSettingString(ApplicationIFrameXPathKeyName, string.Empty);
                return m_ApplicationIFrameXPath;
            }
            set
            {
                m_ApplicationIFrameXPath = value;
            }
        }
        public string GetApplicationIFrameXPath()
        {
            return this.ApplicationIFrameXPath;
        }
        public string SetApplicationIFrameXPath(string value)
        {
            string prev = this.GetApplicationIFrameXPath();
            this.ApplicationIFrameXPath = value;
            return prev;
        }

        private const string LoginPageUsernameXPathKeyName = "ASES.LoginPage.Username.xPath";
        private string? m_LoginPageUsernameXPath = null;
        private string LoginPageUsernameXPath
        {
            get
            {
                if (m_LoginPageUsernameXPath == null)
                    m_LoginPageUsernameXPath = ConfigApi.GetAppSettingString(LoginPageUsernameXPathKeyName, string.Empty);
                return m_LoginPageUsernameXPath;
            }
            set
            {
                m_LoginPageUsernameXPath = value;
            }
        }
        public string GetLoginPageUsernameXPath()
        {
            return this.LoginPageUsernameXPath;
        }
        public string SetLoginPageUsernameXPath(string value)
        {
            string prev = this.GetLoginPageUsernameXPath();
            this.LoginPageUsernameXPath = value;
            return prev;
        }

        private const string LoginPagePasswordXPathKeyName = "ASES.LoginPage.Password.xPath";
        private string? m_LoginPagePasswordXPath = null;
        private string LoginPagePasswordXPath
        {
            get
            {
                if (m_LoginPagePasswordXPath == null)
                    m_LoginPagePasswordXPath = ConfigApi.GetAppSettingString(LoginPagePasswordXPathKeyName, string.Empty);
                return m_LoginPagePasswordXPath;
            }
            set
            {
                m_LoginPagePasswordXPath = value;
            }
        }
        public string GetLoginPagePasswordXPath()
        {
            return this.LoginPagePasswordXPath;
        }
        public string SetLoginPagePasswordXPath(string value)
        {
            string prev = this.GetLoginPagePasswordXPath();
            this.LoginPagePasswordXPath = value;
            return prev;
        }

        private const string LoginPageClientXPathKeyName = "ASES.LoginPage.Client.xPath";
        private string? m_LoginPageClientXPath = null;
        private string LoginPageClientXPath
        {
            get
            {
                if (m_LoginPageClientXPath == null)
                    m_LoginPageClientXPath = ConfigApi.GetAppSettingString(LoginPageClientXPathKeyName, string.Empty);
                return m_LoginPageClientXPath;
            }
            set
            {
                m_LoginPageClientXPath = value;
            }
        }
        public string GetLoginPageClientXPath()
        {
            return this.LoginPageClientXPath;
        }
        public string SetLoginPageClientXPath(string value)
        {
            string prev = this.GetLoginPageClientXPath();
            this.LoginPageClientXPath = value;
            return prev;
        }

        private const string LoginPageLanguageXPathKeyName = "ASES.LoginPage.Langauge.xPath";
        private string? m_LoginPageLanguageXPath = null;
        private string LoginPageLanguageXPath
        {
            get
            {
                if (m_LoginPageLanguageXPath == null)
                    m_LoginPageLanguageXPath = ConfigApi.GetAppSettingString(LoginPageLanguageXPathKeyName, string.Empty);
                return m_LoginPageLanguageXPath;
            }
            set
            {
                m_LoginPageLanguageXPath = value;
            }
        }
        public string GetLoginPageLanguageXPath()
        {
            return this.LoginPageLanguageXPath;
        }
        public string SetLoginPageLanguageXPath(string value)
        {
            string prev = this.GetLoginPageLanguageXPath();
            this.LoginPageLanguageXPath = value;
            return prev;
        }

        private const string LoginPageLoginButtonXPathKeyName = "ASES.LoginPage.LoginButton.xPath";
        private string? m_LoginPageLoginButtonXPath = null;
        private string LoginPageLoginButtonXPath
        {
            get
            {
                if (m_LoginPageLoginButtonXPath == null)
                    m_LoginPageLoginButtonXPath = ConfigApi.GetAppSettingString(LoginPageLoginButtonXPathKeyName, string.Empty);
                return m_LoginPageLoginButtonXPath;
            }
            set
            {
                m_LoginPageLoginButtonXPath = value;
            }
        }
        public string GetLoginPageLoginButtonXPath()
        {
            return this.LoginPageLoginButtonXPath;
        }
        public string SetLoginPageLoginButtonXPath(string value)
        {
            string prev = this.GetLoginPageLoginButtonXPath();
            this.LoginPageLoginButtonXPath = value;
            return prev;
        }

        private const string MainPageIFrameXPathKeyName = "ASES.MainPage.iFrame.xPath";
        private string? m_MainPageIFrameXPath = null;
        private string MainPageIFrameXPath
        {
            get
            {
                if (m_MainPageIFrameXPath == null)
                    m_MainPageIFrameXPath = ConfigApi.GetAppSettingString(MainPageIFrameXPathKeyName, string.Empty);
                return m_MainPageIFrameXPath;
            }
            set
            {
                m_MainPageIFrameXPath = value;
            }
        }
        public string GetMainPageIFrameXPath()
        {
            return this.MainPageIFrameXPath;
        }
        public string SetMainPageIFrameXPath(string value)
        {
            string prev = this.GetMainPageIFrameXPath();
            this.MainPageIFrameXPath = value;
            return prev;
        }

        private const string MainPageWelcomeImageXPathKeyName = "ASES.MainPage.iFrame.WelcomeImage.xPath";
        private string? m_MainPageWelcomeImageXPath = null;
        private string MainPageWelcomeImageXPath
        {
            get
            {
                if (m_MainPageWelcomeImageXPath == null)
                    m_MainPageWelcomeImageXPath = ConfigApi.GetAppSettingString(MainPageWelcomeImageXPathKeyName, string.Empty);
                return m_MainPageWelcomeImageXPath;
            }
            set
            {
                m_MainPageWelcomeImageXPath = value;
            }
        }
        public string GetMainPageWelcomeImageXPath()
        {
            return this.MainPageWelcomeImageXPath;
        }
        public string SetMainPageWelcomeImageXPath(string value)
        {
            string prev = this.GetMainPageWelcomeImageXPath();
            this.MainPageWelcomeImageXPath = value;
            return prev;
        }

        private const string NavMenuXPathKeyName = "ASES.MainPage.NavMenu.xPath";
        private string? m_NavMenuXPath = null;
        private string NavMenuXPath
        {
            get
            {
                if (m_NavMenuXPath == null)
                    m_NavMenuXPath = ConfigApi.GetAppSettingString(NavMenuXPathKeyName, string.Empty);
                return m_NavMenuXPath;
            }
            set
            {
                m_NavMenuXPath = value;
            }
        }
        public string GetNavMenuXPath()
        {
            return this.NavMenuXPath;
        }
        public string SetNavMenuXPath(string value)
        {
            string prev = this.GetNavMenuXPath();
            this.NavMenuXPath = value;
            return prev;
        }

        private const string NavMenuZeitbuchungXPathKeyName = "ASES.MainPage.NavMenu.Zeitbuchung.xPath";
        private string? m_NavMenuZeitbuchungXPath = null;
        private string NavMenuZeitbuchungXPath
        {
            get
            {
                if (m_NavMenuZeitbuchungXPath == null)
                    m_NavMenuZeitbuchungXPath = ConfigApi.GetAppSettingString(NavMenuZeitbuchungXPathKeyName, string.Empty);
                return m_NavMenuZeitbuchungXPath;
            }
            set
            {
                m_NavMenuZeitbuchungXPath = value;
            }
        }
        public string GetNavMenuZeitbuchungXPath()
        {
            return this.NavMenuZeitbuchungXPath;
        }
        public string SetNavMenuZeitbuchungXPath(string value)
        {
            string prev = this.GetNavMenuZeitbuchungXPath();
            this.NavMenuZeitbuchungXPath = value;
            return prev;
        }

        private const string NavMenuUsernameXPathKeyName = "ASES.MainPage.NavMenu.UserName.xPath";
        private string? m_NavMenuUsernameXPath = null;
        private string NavMenuUsernameXPath
        {
            get
            {
                if (m_NavMenuUsernameXPath == null)
                    m_NavMenuUsernameXPath = ConfigApi.GetAppSettingString(NavMenuUsernameXPathKeyName, string.Empty);
                return m_NavMenuUsernameXPath;
            }
            set
            {
                m_NavMenuUsernameXPath = value;
            }
        }
        public string GetNavMenuUsernameXPath()
        {
            return this.NavMenuUsernameXPath;
        }
        public string SetNavMenuUsernameXPath(string value)
        {
            string prev = this.GetNavMenuUsernameXPath();
            this.NavMenuUsernameXPath = value;
            return prev;
        }

        private const string NavMenuUsernameLogoutButtonXPathKeyName = "ASES.MainPage.NavMenu.UserName.LogoutButton.xPath";
        private string? m_NavMenuUsernameLogoutButtonXPath = null;
        private string NavMenuUsernameLogoutButtonXPath
        {
            get
            {
                if (m_NavMenuUsernameLogoutButtonXPath == null)
                    m_NavMenuUsernameLogoutButtonXPath = ConfigApi.GetAppSettingString(NavMenuUsernameLogoutButtonXPathKeyName, string.Empty);
                return m_NavMenuUsernameLogoutButtonXPath;
            }
            set
            {
                m_NavMenuUsernameLogoutButtonXPath = value;
            }
        }
        public string GetNavMenuUsernameLogoutButtonXPath()
        {
            return this.NavMenuUsernameLogoutButtonXPath;
        }
        public string SetNavMenuUsernameLogoutButtonXPath(string value)
        {
            string prev = this.GetNavMenuUsernameLogoutButtonXPath();
            this.NavMenuUsernameLogoutButtonXPath = value;
            return prev;
        }

        private const string DateGridFormXPathKeyName = "ASES.DateGrid.Form.xPath";
        private string? m_DateGridFormXPath = null;
        private string DateGridFormXPath
        {
            get
            {
                if (m_DateGridFormXPath == null)
                    m_DateGridFormXPath = ConfigApi.GetAppSettingString(DateGridFormXPathKeyName, string.Empty);
                return m_DateGridFormXPath;
            }
            set
            {
                m_DateGridFormXPath = value;
            }
        }
        public string GetDateGridFormXPath()
        {
            return this.DateGridFormXPath;
        }
        public string SetDateGridFormXPath(string value)
        {
            string prev = this.GetDateGridFormXPath();
            this.DateGridFormXPath = value;
            return prev;
        }

        private const string DateGridCanvasXPathKeyName = "ASES.DateGrid.Canvas.xPath";
        private string? m_DateGridCanvasXPath = null;
        private string DateGridCanvasXPath
        {
            get
            {
                if (m_DateGridCanvasXPath == null)
                    m_DateGridCanvasXPath = ConfigApi.GetAppSettingString(DateGridCanvasXPathKeyName, string.Empty);
                return m_DateGridCanvasXPath;
            }
            set
            {
                m_DateGridCanvasXPath = value;
            }
        }
        public string GetDateGridCanvasXPath()
        {
            return this.DateGridCanvasXPath;
        }
        public string SetDateGridCanvasXPath(string value)
        {
            string prev = this.GetDateGridCanvasXPath();
            this.DateGridCanvasXPath = value;
            return prev;
        }

        private const string DateGridCanvasSortingAscXPathKeyName = "ASES.DateGrid.Canvas.Sortindicator.Ascending.xPath";
        private string? m_DateGridCanvasSortingAscXPath = null;
        private string DateGridCanvasSortingAscXPath
        {
            get
            {
                if (m_DateGridCanvasSortingAscXPath == null)
                    m_DateGridCanvasSortingAscXPath = ConfigApi.GetAppSettingString(DateGridCanvasSortingAscXPathKeyName, string.Empty);
                return m_DateGridCanvasSortingAscXPath;
            }
            set
            {
                m_DateGridCanvasSortingAscXPath = value;
            }
        }
        public string GetDateGridCanvasSortingAscXPath()
        {
            return this.DateGridCanvasSortingAscXPath;
        }
        public string SetDateGridCanvasSortingAscXPath(string value)
        {
            string prev = this.GetDateGridCanvasSortingAscXPath();
            this.DateGridCanvasSortingAscXPath = value;
            return prev;
        }

        private const string DateGridCanvasSortingDescXPathKeyName = "ASES.DateGrid.Canvas.Sortindicator.Descending.xPath";
        private string? m_DateGridCanvasSortingDescXPath = null;
        private string DateGridCanvasSortingDescXPath
        {
            get
            {
                if (m_DateGridCanvasSortingDescXPath == null)
                    m_DateGridCanvasSortingDescXPath = ConfigApi.GetAppSettingString(DateGridCanvasSortingDescXPathKeyName, string.Empty);
                return m_DateGridCanvasSortingDescXPath;
            }
            set
            {
                m_DateGridCanvasSortingDescXPath = value;
            }
        }
        public string GetDateGridCanvasSortingDescXPath()
        {
            return this.DateGridCanvasSortingDescXPath;
        }
        public string SetDateGridCanvasSortingDescXPath(string value)
        {
            string prev = this.GetDateGridCanvasSortingDescXPath();
            this.DateGridCanvasSortingDescXPath = value;
            return prev;
        }

        private const string DateGridCanvasLastRowXPathKeyName = "ASES.DateGrid.Canvas.LastRow.xPath";
        private string? m_DateGridCanvasLastRowXPath = null;
        private string DateGridCanvasLastRowXPath
        {
            get
            {
                if (m_DateGridCanvasLastRowXPath == null)
                    m_DateGridCanvasLastRowXPath = ConfigApi.GetAppSettingString(DateGridCanvasLastRowXPathKeyName, string.Empty);
                return m_DateGridCanvasLastRowXPath;
            }
            set
            {
                m_DateGridCanvasLastRowXPath = value;
            }
        }
        public string GetDateGridCanvasLastRowXPath()
        {
            return this.DateGridCanvasLastRowXPath;
        }
        public string SetDateGridCanvasLastRowXPath(string value)
        {
            string prev = this.GetDateGridCanvasLastRowXPath();
            this.DateGridCanvasLastRowXPath = value;
            return prev;
        }

        private const string DateGridCanvasLastRowDateFromXPathKeyName = "ASES.DateGrid.Canvas.LastRow.DateFrom.xPath";
        private string? m_DateGridCanvasLastRowDateFromXPath = null;
        private string DateGridCanvasLastRowDateFromXPath
        {
            get
            {
                if (m_DateGridCanvasLastRowDateFromXPath == null)
                    m_DateGridCanvasLastRowDateFromXPath = ConfigApi.GetAppSettingString(DateGridCanvasLastRowDateFromXPathKeyName, string.Empty);
                return m_DateGridCanvasLastRowDateFromXPath;
            }
            set
            {
                m_DateGridCanvasLastRowDateFromXPath = value;
            }
        }
        public string GetDateGridCanvasLastRowDateFromXPath()
        {
            return this.DateGridCanvasLastRowDateFromXPath;
        }
        public string SetDateGridCanvasLastRowDateFromXPath(string value)
        {
            string prev = this.GetDateGridCanvasLastRowDateFromXPath();
            this.DateGridCanvasLastRowDateFromXPath = value;
            return prev;
        }

        private const string DateGridCanvasLastRowDateToXPathKeyName = "ASES.DateGrid.Canvas.LastRow.DateTo.xPath";
        private string? m_DateGridCanvasLastRowDateToXPath = null;
        private string DateGridCanvasLastRowDateToXPath
        {
            get
            {
                if (m_DateGridCanvasLastRowDateToXPath == null)
                    m_DateGridCanvasLastRowDateToXPath = ConfigApi.GetAppSettingString(DateGridCanvasLastRowDateToXPathKeyName, string.Empty);
                return m_DateGridCanvasLastRowDateToXPath;
            }
            set
            {
                m_DateGridCanvasLastRowDateToXPath = value;
            }
        }
        public string GetDateGridCanvasLastRowDateToXPath()
        {
            return this.DateGridCanvasLastRowDateToXPath;
        }
        public string SetDateGridCanvasLastRowDateToXPath(string value)
        {
            string prev = this.GetDateGridCanvasLastRowDateToXPath();
            this.DateGridCanvasLastRowDateToXPath = value;
            return prev;
        }

        private const string TimePairTimeFromXPathKeyName = "ASES.TimePair.TimeFrom.xPath";
        private string? m_TimePairTimeFromXPath = null;
        private string TimePairTimeFromXPath
        {
            get
            {
                if (m_TimePairTimeFromXPath == null)
                    m_TimePairTimeFromXPath = ConfigApi.GetAppSettingString(TimePairTimeFromXPathKeyName, string.Empty);
                return m_TimePairTimeFromXPath;
            }
            set
            {
                m_TimePairTimeFromXPath = value;
            }
        }
        public string GetTimePairTimeFromXPath()
        {
            return this.TimePairTimeFromXPath;
        }
        public string SetTimePairTimeFromXPath(string value)
        {
            string prev = this.GetTimePairTimeFromXPath();
            this.TimePairTimeFromXPath = value;
            return prev;
        }

        private const string TimePairTimeToXPathKeyName = "ASES.TimePair.TimeTo.xPath";
        private string? m_TimePairTimeToXPath = null;
        private string TimePairTimeToXPath
        {
            get
            {
                if (m_TimePairTimeToXPath == null)
                    m_TimePairTimeToXPath = ConfigApi.GetAppSettingString(TimePairTimeToXPathKeyName, string.Empty);
                return m_TimePairTimeToXPath;
            }
            set
            {
                m_TimePairTimeToXPath = value;
            }
        }
        public string GetTimePairTimeToXPath()
        {
            return this.TimePairTimeToXPath;
        }
        public string SetTimePairTimeToXPath(string value)
        {
            string prev = this.GetTimePairTimeToXPath();
            this.TimePairTimeToXPath = value;
            return prev;
        }

        private const string TimePairFooterAcceptButtonPathKeyName = "ASES.TimePair.Footer.AccecptButton.xPath";
        private string? m_TimePairFooterAcceptButtonPath = null;
        private string TimePairFooterAcceptButtonPath
        {
            get
            {
                if (m_TimePairFooterAcceptButtonPath == null)
                    m_TimePairFooterAcceptButtonPath = ConfigApi.GetAppSettingString(TimePairFooterAcceptButtonPathKeyName, string.Empty);
                return m_TimePairFooterAcceptButtonPath;
            }
            set
            {
                m_TimePairFooterAcceptButtonPath = value;
            }
        }
        public string GetTimePairFooterAcceptButtonPath()
        {
            return this.TimePairFooterAcceptButtonPath;
        }
        public string SetTimePairFooterAcceptButtonPath(string value)
        {
            string prev = this.GetTimePairFooterAcceptButtonPath();
            this.TimePairFooterAcceptButtonPath = value;
            return prev;
        }

        private const string DateGridCanvasSaveButtonPathKeyName = "ASES.DateGrid.Canvas.SaveButton.xPath";
        private string? m_DateGridCanvasSaveButtonPath = null;
        private string DateGridCanvasSaveButtonPath
        {
            get
            {
                if (m_DateGridCanvasSaveButtonPath == null)
                    m_DateGridCanvasSaveButtonPath = ConfigApi.GetAppSettingString(DateGridCanvasSaveButtonPathKeyName, string.Empty);
                return m_DateGridCanvasSaveButtonPath;
            }
            set
            {
                m_DateGridCanvasSaveButtonPath = value;
            }
        }
        public string GetDateGridCanvasSaveButtonPath()
        {
            return this.DateGridCanvasSaveButtonPath;
        }
        public string SetDateGridCanvasSaveButtonPath(string value)
        {
            string prev = this.GetDateGridCanvasSaveButtonPath();
            this.DateGridCanvasSaveButtonPath = value;
            return prev;
        }

        #endregion

    } // class

} // namespace

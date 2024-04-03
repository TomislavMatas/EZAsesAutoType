//
// File: AppConfig.cs
//
// Summary:
// Project specific configuration settings. 
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using log4net;
using System.Threading;

namespace EZAsesAutoType
{
    /// <summary>
    /// Access app specific configuration settings from "App.config" file.
    /// </summary>
    internal class AppConfig
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(AppConfig));
                return m_Log;
            }
        }
        #endregion

        #region propertiez
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
        private const int TimeoutFindElementDefault = 1;
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

        private const string TimeoutPopupKeyName = "ASES.Timeout.Popup";
        private const int TimeoutPopupDefault = 1;
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
        private const int WaitBeforeLogoutDefault = 5;
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

        private const string TimeGridFormXPathKeyName = "ASES.TimeGrid.Form.xPath";
        private string? m_TimeGridFormXPath = null;
        private string TimeGridFormXPath
        {
            get
            {
                if (m_TimeGridFormXPath == null)
                    m_TimeGridFormXPath = ConfigApi.GetAppSettingString(TimeGridFormXPathKeyName, string.Empty);
                return m_TimeGridFormXPath;
            }
            set
            {
                m_TimeGridFormXPath = value;
            }
        }
        public string GetTimeGridFormXPath()
        {
            return this.TimeGridFormXPath;
        }
        public string SetTimeGridFormXPath(string value)
        {
            string prev = this.GetTimeGridFormXPath();
            this.TimeGridFormXPath = value;
            return prev;
        }

        private const string TimeGridCanvasXPathKeyName = "ASES.TimeGrid.Canvas.xPath";
        private string? m_TimeGridCanvasXPath = null;
        private string TimeGridCanvasXPath
        {
            get
            {
                if (m_TimeGridCanvasXPath == null)
                    m_TimeGridCanvasXPath = ConfigApi.GetAppSettingString(TimeGridCanvasXPathKeyName, string.Empty);
                return m_TimeGridCanvasXPath;
            }
            set
            {
                m_TimeGridCanvasXPath = value;
            }
        }
        public string GetTimeGridCanvasXPath()
        {
            return this.TimeGridCanvasXPath;
        }
        public string SetTimeGridCanvasXPath(string value)
        {
            string prev = this.GetTimeGridCanvasXPath();
            this.TimeGridCanvasXPath = value;
            return prev;
        }

        private const string TimeGridCanvasSortingAscXPathKeyName = "ASES.TimeGrid.Canvas.Sortindicator.Ascending.xPath";
        private string? m_TimeGridCanvasSortingAscXPath = null;
        private string TimeGridCanvasSortingAscXPath
        {
            get
            {
                if (m_TimeGridCanvasSortingAscXPath == null)
                    m_TimeGridCanvasSortingAscXPath = ConfigApi.GetAppSettingString(TimeGridCanvasSortingAscXPathKeyName, string.Empty);
                return m_TimeGridCanvasSortingAscXPath;
            }
            set
            {
                m_TimeGridCanvasSortingAscXPath = value;
            }
        }
        public string GetTimeGridCanvasSortingAscXPath()
        {
            return this.TimeGridCanvasSortingAscXPath;
        }
        public string SetTimeGridCanvasSortingAscXPath(string value)
        {
            string prev = this.GetTimeGridCanvasSortingAscXPath();
            this.TimeGridCanvasSortingAscXPath = value;
            return prev;
        }

        private const string TimeGridCanvasSortingDescXPathKeyName = "ASES.TimeGrid.Canvas.Sortindicator.Descending.xPath";
        private string? m_TimeGridCanvasSortingDescXPath = null;
        private string TimeGridCanvasSortingDescXPath
        {
            get
            {
                if (m_TimeGridCanvasSortingDescXPath == null)
                    m_TimeGridCanvasSortingDescXPath = ConfigApi.GetAppSettingString(TimeGridCanvasSortingDescXPathKeyName, string.Empty);
                return m_TimeGridCanvasSortingDescXPath;
            }
            set
            {
                m_TimeGridCanvasSortingDescXPath = value;
            }
        }
        public string GetTimeGridCanvasSortingDescXPath()
        {
            return this.TimeGridCanvasSortingDescXPath;
        }
        public string SetTimeGridCanvasSortingDescXPath(string value)
        {
            string prev = this.GetTimeGridCanvasSortingDescXPath();
            this.TimeGridCanvasSortingDescXPath = value;
            return prev;
        }

        private const string TimeGridCanvasLastRowXPathKeyName = "ASES.TimeGrid.Canvas.LastRow.xPath";
        private string? m_TimeGridCanvasLastRowXPath = null;
        private string TimeGridCanvasLastRowXPath
        {
            get
            {
                if (m_TimeGridCanvasLastRowXPath == null)
                    m_TimeGridCanvasLastRowXPath = ConfigApi.GetAppSettingString(TimeGridCanvasLastRowXPathKeyName, string.Empty);
                return m_TimeGridCanvasLastRowXPath;
            }
            set
            {
                m_TimeGridCanvasLastRowXPath = value;
            }
        }
        public string GetTimeGridCanvasLastRowXPath()
        {
            return this.TimeGridCanvasLastRowXPath;
        }
        public string SetTimeGridCanvasLastRowXPath(string value)
        {
            string prev = this.GetTimeGridCanvasLastRowXPath();
            this.TimeGridCanvasLastRowXPath = value;
            return prev;
        }

        private const string TimeGridCanvasLastRowDateFromXPathKeyName = "ASES.TimeGrid.Canvas.LastRow.DateFrom.xPath";
        private string? m_TimeGridCanvasLastRowDateFromXPath = null;
        private string TimeGridCanvasLastRowDateFromXPath
        {
            get
            {
                if (m_TimeGridCanvasLastRowDateFromXPath == null)
                    m_TimeGridCanvasLastRowDateFromXPath = ConfigApi.GetAppSettingString(TimeGridCanvasLastRowDateFromXPathKeyName, string.Empty);
                return m_TimeGridCanvasLastRowDateFromXPath;
            }
            set
            {
                m_TimeGridCanvasLastRowDateFromXPath = value;
            }
        }
        public string GetTimeGridCanvasLastRowDateFromXPath()
        {
            return this.TimeGridCanvasLastRowDateFromXPath;
        }
        public string SetTimeGridCanvasLastRowDateFromXPath(string value)
        {
            string prev = this.GetTimeGridCanvasLastRowDateFromXPath();
            this.TimeGridCanvasLastRowDateFromXPath = value;
            return prev;
        }

        private const string TimeGridCanvasLastRowDateToXPathKeyName = "ASES.TimeGrid.Canvas.LastRow.DateTo.xPath";
        private string? m_TimeGridCanvasLastRowDateToXPath = null;
        private string TimeGridCanvasLastRowDateToXPath
        {
            get
            {
                if (m_TimeGridCanvasLastRowDateToXPath == null)
                    m_TimeGridCanvasLastRowDateToXPath = ConfigApi.GetAppSettingString(TimeGridCanvasLastRowDateToXPathKeyName, string.Empty);
                return m_TimeGridCanvasLastRowDateToXPath;
            }
            set
            {
                m_TimeGridCanvasLastRowDateToXPath = value;
            }
        }
        public string GetTimeGridCanvasLastRowDateToXPath()
        {
            return this.TimeGridCanvasLastRowDateToXPath;
        }
        public string SetTimeGridCanvasLastRowDateToXPath(string value)
        {
            string prev = this.GetTimeGridCanvasLastRowDateToXPath();
            this.TimeGridCanvasLastRowDateToXPath = value;
            return prev;
        }

        private const string TimePairFirstRowTimeFromXPathKeyName = "ASES.TimePair.FirstRow.TimeFrom.xPath";
        private string? m_TimePairFirstRowTimeFromXPath = null;
        private string TimePairFirstRowTimeFromXPath
        {
            get
            {
                if (m_TimePairFirstRowTimeFromXPath == null)
                    m_TimePairFirstRowTimeFromXPath = ConfigApi.GetAppSettingString(TimePairFirstRowTimeFromXPathKeyName, string.Empty);
                return m_TimePairFirstRowTimeFromXPath;
            }
            set
            {
                m_TimePairFirstRowTimeFromXPath = value;
            }
        }
        public string GetTimePairFirstRowTimeFromXPath()
        {
            return this.TimePairFirstRowTimeFromXPath;
        }
        public string SetTimePairFirstRowTimeFromXPath(string value)
        {
            string prev = this.GetTimePairFirstRowTimeFromXPath();
            this.TimePairFirstRowTimeFromXPath = value;
            return prev;
        }

        private const string TimePairFirstRowTimeToXPathKeyName = "ASES.TimePair.FirstRow.TimeTo.xPath";
        private string? m_TimePairFirstRowTimeToXPath = null;
        private string TimePairFirstRowTimeToXPath
        {
            get
            {
                if (m_TimePairFirstRowTimeToXPath == null)
                    m_TimePairFirstRowTimeToXPath = ConfigApi.GetAppSettingString(TimePairFirstRowTimeToXPathKeyName, string.Empty);
                return m_TimePairFirstRowTimeToXPath;
            }
            set
            {
                m_TimePairFirstRowTimeToXPath = value;
            }
        }
        public string GetTimePairFirstRowTimeToXPath()
        {
            return this.TimePairFirstRowTimeToXPath;
        }
        public string SetTimePairFirstRowTimeToXPath(string value)
        {
            string prev = this.GetTimePairFirstRowTimeToXPath();
            this.TimePairFirstRowTimeToXPath = value;
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

        private const string TimeGridCanvasSaveButtonPathKeyName = "ASES.TimeGrid.Canvas.SaveButton.xPath";
        private string? m_TimeGridCanvasSaveButtonPath = null;
        private string TimeGridCanvasSaveButtonPath
        {
            get
            {
                if (m_TimeGridCanvasSaveButtonPath == null)
                    m_TimeGridCanvasSaveButtonPath = ConfigApi.GetAppSettingString(TimeGridCanvasSaveButtonPathKeyName, string.Empty);
                return m_TimeGridCanvasSaveButtonPath;
            }
            set
            {
                m_TimeGridCanvasSaveButtonPath = value;
            }
        }
        public string GetTimeGridCanvasSaveButtonPath()
        {
            return this.TimeGridCanvasSaveButtonPath;
        }
        public string SetTimeGridCanvasSaveButtonPath(string value)
        {
            string prev = this.GetTimeGridCanvasSaveButtonPath();
            this.TimeGridCanvasSaveButtonPath = value;
            return prev;
        }

        #endregion

    } // class

} // namespace

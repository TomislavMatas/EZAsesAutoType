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

        private const string TimeoutLoginPageKeyName = "ASES.Timeout.LoginPage";
        private const int TimeoutLoginPageDefault = 60;
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
        private const int TimeoutFindElementDefault = 10;
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
        private const int TimeoutPopupDefault = 10;
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

        private const string ApplicationIFrameXPathKeyName = "ASES.Application.iFrame.xPath";
        private string m_ApplicationIFrameXPath = null;
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
        private string m_LoginPageUsernameXPath = null;
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
        private string m_LoginPagePasswordXPath = null;
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


    } // class

} // namespace

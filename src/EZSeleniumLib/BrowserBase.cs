//
// File: "BrowserBase.cs"
//
// Summary:
// Base class used for browser specific implementations 
// using Selenium's WebDriver Classes.
// Place wrapper for the relevant Selenium WebDriver 
// methods here or in respective descendents.
//
// Notes:
// "OpenQA.Selenium.Support.UI.ExpectedConditions" has been deprecated, see details on
// -->< https://stackoverflow.com/questions/49866334/c-sharp-selenium-expectedconditions-is-obsolete >
// There for, the fork "DotNetSeleniumExtras.WaitHelpers" has been added to this project using NuGet.
//
// Revision History: 
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text.RegularExpressions;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace EZSeleniumLib
{
    /// <summary>
    /// Base class used for specific browser implementations 
    /// using Selenium's WebDriver Classes.
    /// Provides wrapper methods for the most relevant 
    /// Selenium WebDriver methods, which in turn can
    /// be extended within specifc descendants - if needed.
    /// </summary>
    public abstract partial class BrowserBase
    {
        #region log4net
        private static ILog _log = null;
        private static ILog Log
        {
            get
            {
                if (_log == null)
                    _log = LogManager.GetLogger(typeof(BrowserBase));
                return _log;
            }
        }
        #endregion

        #region protected memberz

        protected const string DEBUG_START = Consts.DEBUG_START;
        protected const string DEBUG_DONE = Consts.DEBUG_DONE;

        private WebDriver m_Driver = null;
        protected WebDriver Driver
        {
            get
            {
                if (m_Driver == null)
                    m_Driver = this.GetDriver();

                return m_Driver;
            }
            set 
            { 
                m_Driver = value; 
            }
        }
        protected abstract WebDriver GetDriver();

        /// <summary>
        /// Return reference on current instance of IWebDriver.
        /// </summary>
        /// <returns></returns>
        public IWebDriver GetWebDriver()
        {
            return GetDriver();
        }

        private DriverService m_Service = null;
        protected DriverService Service
        {
            get
            {
                if (m_Service == null)
                    m_Service = this.GetService();

                return m_Service;
            }
            set 
            { 
                m_Service = value; 
            }
        }
        protected abstract DriverService GetService();

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private BrowserOptions _browserOptions = null;

        /// <summary>
        /// Browser Options.
        /// </summary>
        protected BrowserOptions BrowserOptions
        {
            get
            {
                if(_browserOptions == null)
                    _browserOptions = new BrowserOptions();

                return _browserOptions;
            }
            set
            {
                _browserOptions= value;
            }
        }

        protected int m_Delay = -1;
        protected int Delay
        {
            get
            {
                if (m_Delay == -1)
                    m_Delay = this.BrowserOptions.Delay;

                return m_Delay;
            }
            set
            {
                m_Delay = value;
            }
        }
        public int GetDelay()
        {
            return this.Delay;
        }
        public int SetDelay(int delay)
        {
            int prev = this.GetDelay();
            this.Delay = delay;
            return prev;
        }

        /// <summary>
        /// The browser specific startup argument decoration prefix.
        /// Either "--", "-", "/" or "" depending on browser implementation. 
        /// </summary>
        protected string _argPfx = null;

        /// <summary>
        /// Set startup argument decoration prefix.
        /// Either "--", "-", "/" or "" depending on browser implementation. 
        /// Requires implementation in descendants.
        /// </summary>
        protected abstract void SetArgPrefix();

        /// <summary>
        /// Return startup argument decoration prefix.
        /// Either "--", "-", "/" or "" depending on browser implementation. 
        /// </summary>
        protected string GetArgPrefix()
        {
            if (_argPfx == null)
                SetArgPrefix();

            return _argPfx;
        }

        /// <summary>
        /// The requestor script's process id.
        /// see: ->< https://stackoverflow.com/questions/47789640/get-handle-to-opened-chrome-window-in-selenium > 
        /// </summary>
        private int _scriptPID;

        /// <summary>
        /// Set requestor script's process id.
        /// </summary>
        protected void SetScriptPID(int scriptPID)
        {
            _scriptPID = scriptPID; 
        }

        /// <summary>
        /// Get requestor script's process id.
        /// </summary>
        protected int GetScriptPID()
        {
            return _scriptPID;
        }

        /// <summary>
        /// Get requestor script's process id as formated command line argument.
        /// </summary>
        protected string GetArgScriptPID()
        {
            string argScriptPID = String.Format("{0}{1}{2}", GetArgPrefix(), Consts.ARG_SCRIPTPID, GetScriptPID());
            return argScriptPID;
        }

        /// <summary>
        /// The browser specific executable process name.
        /// </summary>
        protected string _processName = null;

        /// <summary>
        /// Set the browser specific executable process name.
        /// </summary>
        protected abstract void SetProcessName();

        /// <summary>
        /// Get the browser specific executable process name.
        /// </summary>
        protected string GetProcessName()
        {
            if (_processName == null)
                SetProcessName();

            return _processName;
        }

        #endregion

        #region private memberz

        [DllImport("user32.dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        private enum GetWindowCmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }

        private void EvalProcessByScriptPID()
        {
            int scriptPID = this.BrowserOptions.ScriptPID;
            if (scriptPID <= 0)
                return;

            string processName = this.GetProcessName();
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(processName);
            for (int p = 0; p < processes.Length; p++)
            {
                ManagementObjectSearcher commandLineSearcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + processes[p].Id);
                String commandLine = "";
                foreach (ManagementObject commandLineObject in commandLineSearcher.Get())
                    commandLine += (String)commandLineObject["CommandLine"];

                string argPart = String.Format("{0}{1}", this.GetArgPrefix(), Consts.ARG_SCRIPTPID);
                string regExp = String.Format("{0}(.+?) ", argPart);
                String pidStr = (new Regex(regExp)).Match(commandLine).Groups[1].Value;
                if (!pidStr.Equals(String.Empty) && Convert.ToInt32(pidStr).Equals(scriptPID))
                {
                    _process = processes[p];
                    return;
                }
            }
        }

        private Process _process = null;
        private Process Process
        {
            get
            {
                if (_process == null)
                    EvalProcessByScriptPID();

                return _process;
            }
            set
            {
                _process = value;
            }
        }

        private IntPtr GetChildWindow(IntPtr hwnd)
        {
            return GetWindow(hwnd, (uint)GetWindowCmd.GW_CHILD);
        }

        #endregion 

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BrowserBase()
        {
            try
            {
                Log.Debug(DEBUG_START);
                if (!this.Initialize())
                    throw new Exception("Initialize failed");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Custom constructor.
        /// </summary>
        public BrowserBase(BrowserOptions browserOptions)
        {
            try
            {
                Log.Debug(DEBUG_START);
                this._browserOptions = browserOptions;
                if (!this.Initialize())
                    throw new Exception("Initialize failed");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Default destructor.
        /// </summary>
        ~BrowserBase()
        {
            try
            {
                Log.Debug(DEBUG_START);
                if(!this.Cleanup())
                    throw new Exception("Cleanup failed");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Graceful teardown of browser instance.
        /// </summary>
        /// <returns></returns>
        public bool Cleanup()
        {
            try
            {
                Log.Debug(DEBUG_START);
                if (Driver != null)
                {
                    Driver.Close();
                    Driver.Quit();
                    Driver.Dispose();
                    Driver = null;
                }
                if (Service != null)
                {
                    Service.Dispose();
                    Service = null;
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
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Write current Browser's MemoryInfo's properties to Log.
        /// Failrues when calling this function should be swallowed silently. 
        /// </summary>
        /// <returns></returns>
        public void DumpMemoryInfo()
        {
            try
            {
                MemoryInfo memoryInfo = this.GetMemoryInfo();
                this.DumpMemoryInfo(memoryInfo);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(DEBUG_DONE);
            }
        }

        /// <summary>
        /// Enumartion of some special key chords.
        /// </summary>
        public enum KeyChord
        {
            /// <summary>
            /// CTRL + T = New Tab
            /// </summary>
            CTRL_T,
            /// <summary>
            /// CTRL + W = Close (current) Tab
            /// </summary>
            CTRL_W
        }

        /// <summary>
        /// ->< https://stackoverflow.com/questions/2664804/is-it-possible-to-sendkeys-post-message-directly-to-a-htmlelement-rather-than >
        /// "<...>
        /// Use p/invoke GetWindow with the child command to repeatedly get 
        /// child window handles until you get down to one that responds 
        /// to GetClassName with "Internet Explorer_Server" or something like that.
        /// Then PostMessage on that hWnd and the activeElement of the WebBrowser 
        /// will recieve them.Make sure you have the WM_KEYUP in there as that 
        /// seems to be the important one.
        /// <...>"
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        private Process GetServerProcess(Process process)
        {
            return process;
        }

        public bool SendKeyChord(KeyChord keyChord)
        {
            try
            {
                Process p = this.Process;
                if (p == null)
                    throw new Exception("process is null");

                int browserPID = p.Id;
                IntPtr hwnd = p.MainWindowHandle;
//hwnd = GetChildWindow(hwnd);

                uint MSG_KEY_DOWN = 0x0100;
                uint MSG_KEY_UP = 0x0101;
                IntPtr KEY_CTRL = new IntPtr(0x11);

                switch (keyChord)
                {
                    case KeyChord.CTRL_T:
//                        IntPtr key = new IntPtr(0x54); // "T" (Uppercase)
                        IntPtr key = new IntPtr(0x74); // "t" (lowercase)
                        PostMessage(hwnd, MSG_KEY_DOWN, KEY_CTRL, IntPtr.Zero);
                        PostMessage(hwnd, MSG_KEY_DOWN, key, IntPtr.Zero);
                        PostMessage(hwnd, MSG_KEY_UP, key, IntPtr.Zero);
                        PostMessage(hwnd, MSG_KEY_UP, KEY_CTRL, IntPtr.Zero);
                        Thread.Sleep(this.GetDelay());
                        return true;
                    case KeyChord.CTRL_W:
//                      IntPtr KEY_W = new IntPtr(0x57); // "W" (Uppercase)
                        IntPtr KEY_w = new IntPtr(0x77); // "w" (lppercase)
                        PostMessage(hwnd, MSG_KEY_DOWN, KEY_CTRL, IntPtr.Zero);
                        PostMessage(hwnd, MSG_KEY_DOWN, KEY_w, IntPtr.Zero);
                        PostMessage(hwnd, MSG_KEY_UP, KEY_w, IntPtr.Zero);
                        PostMessage(hwnd, MSG_KEY_UP, KEY_CTRL, IntPtr.Zero);
                        Thread.Sleep(this.GetDelay());
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(DEBUG_DONE);
            }
        }

        #region abstract memberz

        /// <summary>
        /// Initialize specific browser instance.
        /// Must be implement in descandants.
        /// </summary>
        /// <returns></returns>
        public abstract bool Initialize();

        /// <summary>
        /// Execute driver command "Page.reload".
        /// Shall mimic Control + F5.
        /// see ->< https://bugs.chromium.org/p/chromedriver/issues/detail?id=3249 >
        /// </summary>
        /// <param name="ignoreCache"></param>
        /// <returns></returns>
        public abstract bool PageReload(bool ignoreCache);

        /// <summary>
        /// Initiate JavaScript VM Garbage Collection.
        /// </summary>
        /// <returns></returns>
        public abstract void GarbageCollect();

        /// <summary>
        /// Wrapper for "IJavaScriptExecutor.ExecuteScript()".
        /// </summary>
        /// <param name="script"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract object ExecuteScript(string script, params object[] args);

        /// <summary>
        /// Return a new instance of class MemoryInfo. 
        /// MemoryInfo is the representation of the json object 
        /// returned by the JavaScript call "window.performance.memory".
        /// </summary>
        /// <returns></returns>
        public abstract MemoryInfo GetMemoryInfo();

        /// <summary>
        /// Write values of MemoryInfo's properties to Log. 
        /// </summary>
        /// <returns></returns>
        public abstract void DumpMemoryInfo(MemoryInfo memoryInfo);

        /// <summary>
        /// Transfer control to a specific TAB identified 
        /// by it's window hwnd.
        /// </summary>
        /// <returns></returns>
        public abstract bool SwitchTo(string handle);

        /// <summary>
        /// Open a new TAB and immdiatly transfer control 
        /// to the newly opened TAB.
        /// </summary>
        /// <param name="closeOldTab">if set to true, closes the old TAB also
        /// after the control has been successfully transfered to the newly opened TAB</param>
        /// <returns></returns>
        public abstract bool SwitchToNewTab(bool closeOldTab = true);

        #endregion abstract memberz

    } // class

} // namespace

//
// File: "Const.cs"
//
// Summary:
// Assembly specific "internal" constants. 
//
// Revision History: 
// 2024/03/24:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using System.Diagnostics;
using System.Reflection;

namespace EZSeleniumLib
{
    /// <summary>
    /// Assembly specific "internal" constants.
    /// </summary>
    internal static class Const
    {
        #region assembly constantz

        /// <summary>
        /// Customized assembly's display name.
        /// </summary>
        public const string DisplayName = "EZSeleniumLib.dll";

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        public static Assembly? m_ExecutingAssembly = null;

        /// <summary>
        /// Return instance of class FileVersionInfo reflecting AssemblyFileVersion from AssemblyInfo.cs.
        /// </summary>
        public static Assembly ExecutingAssembly
        {
            get
            {
                if (m_ExecutingAssembly == null)
                    m_ExecutingAssembly = Assembly.GetExecutingAssembly();
                return m_ExecutingAssembly;
            }
        }

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static FileVersionInfo? m_AssemblyVersionInfo = null;

        /// <summary>
        /// Return instance of class FileVersionInfo reflecting AssemblyFileVersion from AssemblyInfo.cs.
        /// </summary>
        public static FileVersionInfo AssemblyVersionInfo
        {
            get
            {
                if (m_AssemblyVersionInfo == null)
                    m_AssemblyVersionInfo = FileVersionInfo.GetVersionInfo(ExecutingAssembly.Location);
                return m_AssemblyVersionInfo;
            }
        }

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        public static string? m_AssemblyPath = null;

        /// <summary>
        /// Return the executing assembly's path - aka "installation folder path".
        /// </summary>
        public static string ExecutingAssemblyPath
        {
            get
            {
                if (m_AssemblyPath == null)
                {
                    FileInfo file = new FileInfo(ExecutingAssembly.Location);
                    m_AssemblyPath = file.DirectoryName;
                }
                return m_AssemblyPath;
            }
        }

#if DEBUG
        private const string DEBUGSUFFIX = " (dbg)";
#else
        private const string DEBUGSUFFIX = "";
#endif

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static string? m_AssemblyVersionString = null;

        /// <summary>
        /// Return AssemblyFileVersion from AssemblyInfo.cs as dot seperated string.
        /// </summary>
        public static string AssemblyVersionString
        {
            get
            {
                if (m_AssemblyVersionString == null)
                {
                    m_AssemblyVersionString = string.Format("{0}.{1}.{2}.{3}{4}"
                        , Const.AssemblyVersionInfo.FileMajorPart
                        , Const.AssemblyVersionInfo.FileMinorPart
                        , Const.AssemblyVersionInfo.FileBuildPart
                        , Const.AssemblyVersionInfo.FilePrivatePart
                        , Const.DEBUGSUFFIX);
                }
                return m_AssemblyVersionString;
            }
        }

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static string? m_Description = null;

        /// <summary>
        /// Return custom Assembly Description.
        /// </summary>
        public static string Description
        {
            get
            {
                if (m_Description == null)
                {
                    m_Description = string.Format("{0} {1}"
                        , Const.DisplayName
                        , Const.AssemblyVersionString);
                }
                return m_Description;
            }
        }
        #endregion

        #region logging constantz
        public const string LogStart = " START";
        public const string LogDone = " DONE";
        public const string LogFail = " FAIL";
        public const string LogInvalid = " INVALID";
        public const string LogNotImpl = " NOT IMPLEMENTED";
        public const string LogIsNull = " IS NULL";
        #endregion

        #region "App.config" key namez
        public const string WebDriverKeyName         = "EZSeleniumLib.WebDriver";
        public const string WebDriverInitModeKeyName = "EZSeleniumLib.WebDriver.InitMode";
        public const string WebDriverDelayKeyName    = "EZSeleniumLib.WebDriver.Delay";
        #endregion

        #region "App.config" default valuez
        public const string WebDriverDefault         = Constant.WebDriverChrome;
        public const string WebDriverInitModeDefault = Constant.WebDriverInitModeExtended;
        public const int    WebDriverDelayDefault    = 500;
        #endregion

    } // class

} // namespace

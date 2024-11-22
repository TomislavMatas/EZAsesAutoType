//
// File: "Const.cs"
//
// Summary:
// Project specific constants. 
//
// Revision History: 
// 2024/11/22:TomislavMatas: Version "1.131.2"
// * Add "Client_DbSystel" and "Client_DbCargo".
// 2024/08/06:TomislavMatas: Version "1.127.1"
// * Add "AssemblyCompanyName".
// 2024/07/08:TomislavMatas: Version "1.126.4"
// * Add "CommandlineArg_DoLogout".
// 2024/07/03:TomislavMatas: Version "1.126.2"
// * Add "CommandlineArg_DoLogin" and "CommandlineArg_DoPunch".
// 2024/05/27:TomislavMatas: Version "1.126.0"
// * Add "CommandlineArg_Close".
// 2024/05/15:TomislavMatas: Version "1.125.0"
// * Add "CommandlineArg_Run".
// 2024/05/10:TomislavMatas: Version "1.125.0"
// * Enhance NULL value handling and validation.
// 2024/04/12:TomislavMatas: Version "1.123.4"
// * Add "AssemblyDisplayName", "AssemblyDisplayVersion" and "AssemblyDisplayTitle".
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System.Diagnostics;
using System.Reflection;

namespace EZAsesAutoType
{
    /// <summary>
    /// Project specific constants.
    /// </summary>
    public static class Const
    {
        #region assembly constantz

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static FileVersionInfo? m_AssemblyVersionInfo = null;
        /// <summary>
        /// Return instance of class FileVersionInfo.
        /// </summary>
        public static FileVersionInfo AssemblyVersionInfo
        {
            get
            {
                if (m_AssemblyVersionInfo == null)
                {
                    m_AssemblyVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                }
                return m_AssemblyVersionInfo;
            }
        }

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static string? m_AssemblyFileName = null;
        /// <summary>
        /// Executing assembly's file name.
        /// </summary>
        public static string AssemblyFileName
        {
            get
            {
                if (m_AssemblyFileName == null)
                    m_AssemblyFileName = Const.AssemblyVersionInfo.FileName;

                return m_AssemblyFileName;
            }
        }

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static string? m_AssemblyProductName = null;
        /// <summary>
        /// Executing assembly's product name.
        /// </summary>
        public static string AssemblyProductName
        {
            get
            {
                if (m_AssemblyProductName == null)
                    m_AssemblyProductName = Const.AssemblyVersionInfo.ProductName;

                if (string.IsNullOrEmpty(m_AssemblyProductName))
                    m_AssemblyProductName = "EZ ASES AutoTyoe";

                return m_AssemblyProductName;
            }
        }

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static string? m_AssemblyDisplayName = null;
        /// <summary>
        /// Custom product name to be displayed.
        /// </summary>
        public static string AssemblyDisplayName
        {
            get
            {
                if (m_AssemblyDisplayName == null)
                    m_AssemblyDisplayName = Const.AssemblyProductName;

                return m_AssemblyDisplayName;
            }
        }

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static string? m_AssemblyCompanyName = null;
        /// <summary>
        /// Executing assembly's company name.
        /// </summary>
        public static string AssemblyCompanyName
        {
            get
            {
                if (m_AssemblyCompanyName == null)
                    m_AssemblyCompanyName = Const.AssemblyVersionInfo.CompanyName;

                if (string.IsNullOrEmpty(m_AssemblyCompanyName))
                    m_AssemblyCompanyName = "matas consulting";

                return m_AssemblyCompanyName;
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
        /// Return AssemblyFileVersion as dot seperated string.
        /// Format: Major.Minor.Build.Private[ (dbg)]
        /// </summary>
        public static string AssemblyVersionString
        {
            get
            {
                if (m_AssemblyVersionString == null)
                    m_AssemblyVersionString = string.Format("{0}.{1}.{2}.{3}{4}"
                        , Const.AssemblyVersionInfo.FileMajorPart
                        , Const.AssemblyVersionInfo.FileMinorPart
                        , Const.AssemblyVersionInfo.FileBuildPart
                        , Const.AssemblyVersionInfo.FilePrivatePart
                        , Const.DEBUGSUFFIX);

                return m_AssemblyVersionString;
            }
        }

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static string? m_AssemblyVersionDisplayed = null;
        /// <summary>
        /// Return AssemblyFileVersion as dot seperated string.
        /// Format: Major.Minor.Build
        /// </summary>
        public static string AssemblyDisplayVersion
        {
            get
            {
                if (m_AssemblyVersionDisplayed == null)
                    m_AssemblyVersionDisplayed = string.Format("{0}.{1}.{2}"
                        , Const.AssemblyVersionInfo.FileMajorPart
                        , Const.AssemblyVersionInfo.FileMinorPart
                        , Const.AssemblyVersionInfo.FileBuildPart
                        );

                return m_AssemblyVersionDisplayed;
            }
        }

        /// <summary>
        /// Singleton helper variable.
        /// </summary>
        private static string? m_AssemblyDisplayTitle = null;
        /// <summary>
        /// Return assembly display title including version info.
        /// </summary>
        public static string AssemblyDisplayTitle
        {
            get
            {
                if (m_AssemblyDisplayTitle == null)
                    m_AssemblyDisplayTitle = string.Format("{0} {1}"
                        , Const.AssemblyDisplayName
                        , Const.AssemblyDisplayVersion);

                return m_AssemblyDisplayTitle;
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
        public const string LogNotFound = " NOT FOUND";
        public const string LogCanceled = " CANCELD";
        public const string LogTimeout = " TIMEOUT";
        public const string LogInProgress = " ...";
        #endregion

        public const string UrlParmClientNo = "ClientNo=";

        /// <summary>
        /// When "/run" or "/dopunch" has been supplied,
        /// the time punch sequence will be executed at
        /// programm startup automatically.
        /// </summary>
        public const string CommandlineArg_Run = "/run";

        /// <summary>
        /// If "/run" or "/dopunch" has been supplied,
        /// in conjunction with "/close", the application will
        /// be closed implictly after processing.
        /// </summary>
        public const string CommandlineArg_Close = "/close";

        /// <summary>
        /// Do automated login and than stop. 
        /// Can be helpful to review current content in ASES.
        /// </summary>
        public const string CommandlineArg_DoLogin = "/dologin";

        /// <summary>
        /// Do automated login and execute time pair punches. 
        /// </summary>
        public const string CommandlineArg_DoPunch = "/dopunch";

        /// <summary>
        /// Do automated logout after time pair punches. 
        /// </summary>
        public const string CommandlineArg_DoLogout = "/dologout";

        public const string Client_DbSystel ="06-DB-Systel";
        public const string Client_DbCargo = "24-Cargo Zentrale";

    } // class

} // namespace

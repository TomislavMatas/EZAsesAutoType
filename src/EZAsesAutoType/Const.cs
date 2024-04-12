//
// File: "Const.cs"
//
// Summary:
// Project specific constants. 
//
// Revision History: 
// 2024/04/12:TomislavMatas: Version "1.123.4.0"
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
        private static string m_AssemblyFileName = null;
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

    } // class

} // namespace

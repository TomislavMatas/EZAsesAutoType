//
// File: LibConst.cs
//
// Summary:
// Project specific constants. 
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

using System.Diagnostics;
using System.Reflection;

namespace AsesAutoTypeLib
{
    /// <summary>
    /// Project specific constants.
    /// </summary>
    public static class LibConst
    {
        public const string DisplayName = "AsesAutoTypeLib.dll";

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
                {
                    m_AssemblyVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                }
                return m_AssemblyVersionInfo;
            }
        }

#if DEBUG
        private const string DEBUGSUFFIX = " (dbg)";
#else
        private const string DEBUGSUFFIX = string.Empty;
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
                        , LibConst.AssemblyVersionInfo.FileMajorPart
                        , LibConst.AssemblyVersionInfo.FileMinorPart
                        , LibConst.AssemblyVersionInfo.FileBuildPart
                        , LibConst.AssemblyVersionInfo.FilePrivatePart
                        , LibConst.DEBUGSUFFIX);
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
                        , LibConst.DisplayName
                        , LibConst.AssemblyVersionString);
                }
                return m_Description;
            }
        }

    } // class

} // namespace

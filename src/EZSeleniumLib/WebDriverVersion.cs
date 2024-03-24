//
// File: "WebDriverVersion.cs"
//
// Summary:
// This class provides an interface for quering and
// updating the installed WebDriver binaries.
//
// Revision History: 
// 2024/03/24:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using log4net;

namespace EZSeleniumLib
{
    /// <summary>
    /// This class provides the interface for access to 
    /// the "App.config" file using specific wrapper methods.
    /// </summary>
    public static class WebDriverVersion
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(WebDriverVersion));
                return m_Log;
            }
        }
        #endregion

        /// <summary>
        /// For unknown reason, Google decided not to add a version info 
        /// ressource to Chrome WebDriver "chromedriver.exe". 
        /// So this implementation will work for the Edge WebDriver 
        /// "MicrosoftWebDriver.exe" only.
        /// </summary>
        /// <param name="webDriverFullPath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        private static string? GetVersionStringFromBinary(string webDriverFullPath)
        {
            try
            {
                if (string.IsNullOrEmpty(webDriverFullPath))
                    throw new ArgumentNullException(nameof(webDriverFullPath));
                
                if(!File.Exists(webDriverFullPath)) 
                    throw new FileNotFoundException(nameof(webDriverFullPath));
                
                FileVersionInfo? webDriverVersionInfo = FileVersionInfo.GetVersionInfo(webDriverFullPath);
                if (webDriverVersionInfo == null)
                    throw new Exception(nameof(webDriverVersionInfo) + Const.LogIsNull);

                string versionString = string.Format("{0}.{1}.{2}.{3}"
                    , webDriverVersionInfo.FileMajorPart
                    , webDriverVersionInfo.FileMinorPart
                    , webDriverVersionInfo.FileBuildPart
                    , webDriverVersionInfo.FilePrivatePart);

                return versionString;
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

        public static string? GetWebDriverVersionString(string webDriver)
        {
            try
            { 
                if(string.IsNullOrEmpty(webDriver)) 
                    throw new ArgumentNullException(nameof(webDriver));

                string webDriverExe;
                if (Constant.WebDriverChrome.Equals(webDriver, StringComparison.OrdinalIgnoreCase))
                {
                    // For unknown reason, Google decided not to add a version
                    // info ressource to Chrome WebDriver "chromedriver.exe". 
                    // so simply return the version used on build.
                    return Constant.WebDriverChromeVersion;
                }
                else if (Constant.WebDriverEdge.Equals(webDriver, StringComparison.OrdinalIgnoreCase))
                    webDriverExe = Constant.WebDriverEdgeExe;
                else
                    throw new Exception(nameof(webDriver) + Const.LogNotImpl);

                string executingAssemblyPath = Const.ExecutingAssemblyPath;
                if (string.IsNullOrEmpty(executingAssemblyPath))
                    throw new Exception(nameof(executingAssemblyPath) + Const.LogIsNull);

                string webDriverFullPath = Path.Combine(executingAssemblyPath, webDriverExe);
                if (string.IsNullOrEmpty(webDriverFullPath))
                    throw new Exception(nameof(webDriverFullPath) + Const.LogIsNull);

                string? webDriverVersionString = GetVersionStringFromBinary(webDriverFullPath);
                if (string.IsNullOrEmpty(webDriverVersionString))
                    throw new Exception(nameof(webDriverVersionString) + Const.LogIsNull);

                return webDriverVersionString;
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

    } // class

} // namespace

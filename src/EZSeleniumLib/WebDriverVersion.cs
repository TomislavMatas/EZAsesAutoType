//
// File: "WebDriverVersion.cs"
//
// Summary:
// This class provides an interface for quering and
// updating the installed WebDriver binaries.
//
// Revision History: 
// 2025/04/10:TomislavMatas: v4.31.1
// * Google finally decided to compile executable "chromedriver.exe"
//   with version info resource, so read version from executable.
// 2024/05/31:TomislavMatas: Version "4.21.1
// * Simplify log4net implementations.
// 2024/05/04:TomislavMatas: v4.20.0
// * Upgrade to .NET version 8.
// 2024/04/04:TomislavMatas: v1.0.0
// * Initial version.
//

using System.Diagnostics;

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

        private static readonly ILog Log = LogManager.GetLogger(typeof(WebDriverVersion));

        [Conditional("DEBUG")]
        private static void LogTrace(object message)
        {
#if DEBUG
            Log.Debug(message);
#endif
        }

        #endregion

        /// <summary>
        /// Read version info from binary provided by parameter `webDriverFullPath`.
        /// </summary>
        /// <param name="webDriverFullPath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        private static string? GetVersionStringFromBinary(string webDriverFullPath)
        {
            try
            {
                LogTrace(Consts.LogStart);

                if (string.IsNullOrEmpty(webDriverFullPath))
                    throw new ArgumentNullException(nameof(webDriverFullPath));
                
                if(!File.Exists(webDriverFullPath)) 
                    throw new FileNotFoundException(nameof(webDriverFullPath));
                
                FileVersionInfo? webDriverVersionInfo = FileVersionInfo.GetVersionInfo(webDriverFullPath);
                if (webDriverVersionInfo == null)
                    throw new Exception(nameof(webDriverVersionInfo) + Consts.LogIsNull);

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
                LogTrace(Consts.LogDone);
            }
        }

        /// <summary>
        /// Read version info from respective WebDriver executable binary.
        /// This works only for Edge and Chrome at the moment. For some 
        /// unknown reason, Mozilla decided not to add a version info 
        /// ressource to WebDriver "geckodriver.exe". For FireFox, 
        /// a constant value is returned, see: <see cref="Constant.WebDriverFirefoxVersion"/>.
        /// </summary>
        /// <param name="webDriver"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string? GetWebDriverVersionString(string webDriver)
        {
            try
            {
                LogTrace(Consts.LogStart);

                if (string.IsNullOrEmpty(webDriver)) 
                    throw new ArgumentNullException(nameof(webDriver));

                string webDriverExe;
                if (Constant.WebDriverChrome.Equals(webDriver, StringComparison.OrdinalIgnoreCase))
                    webDriverExe = Constant.WebDriverChromeExe;
                else if (Constant.WebDriverEdge.Equals(webDriver, StringComparison.OrdinalIgnoreCase))
                    webDriverExe = Constant.WebDriverEdgeExe;
                else if (Constant.WebDriverFirefox.Equals(webDriver, StringComparison.OrdinalIgnoreCase))
                    return Constant.WebDriverFirefoxVersion;
                else
                    throw new Exception(nameof(webDriver) + Consts.LogNotImpl);

                string executingAssemblyPath = Consts.ExecutingAssemblyPath;
                if (string.IsNullOrEmpty(executingAssemblyPath))
                    throw new Exception(nameof(executingAssemblyPath) + Consts.LogIsNull);

                string webDriverFullPath = Path.Combine(executingAssemblyPath, webDriverExe);
                if (string.IsNullOrEmpty(webDriverFullPath))
                    throw new Exception(nameof(webDriverFullPath) + Consts.LogIsNull);

                string? webDriverVersionString = GetVersionStringFromBinary(webDriverFullPath);
                if (string.IsNullOrEmpty(webDriverVersionString))
                    throw new Exception(nameof(webDriverVersionString) + Consts.LogIsNull);

                return webDriverVersionString;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
            finally
            {
                LogTrace(Consts.LogDone);
            }
        }

    } // class

} // namespace

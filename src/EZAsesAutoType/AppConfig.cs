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

namespace EZAsesAutoType
{
    /// <summary>
    /// Access app specific configuration settings from "App.config" file.
    /// </summary>
    internal static class AppConfig
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

        private static bool Initialze()
        {
            try
            {
                Log.Debug(Const.LogStart);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }

        }

    } // class

} // namespace

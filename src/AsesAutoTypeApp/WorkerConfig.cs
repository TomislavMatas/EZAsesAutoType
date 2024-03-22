//
// File: "WorkerConfig.cs"
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

using AsesAutoTypeLib;
using log4net;

namespace AsesAutoTypeApp
{
    /// <summary>
    ///  Custom entity to store configuration settings that
    ///  shall be applied and used within instances 
    ///  of class "WorkerThread".
    /// </summary>
    internal class WorkerConfig
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(WorkerConfig));
                return m_Log;
            }
        }
        #endregion

        /// <summary>
        ///  Default constructor.
        /// </summary>
        public WorkerConfig()
        {
            try
            {
                Log.Debug(LogConst.START);
                if (!Initialze())
                    throw new Exception("Initialze failed");
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(LogConst.DONE);
            }
        }

        private bool Initialze()
        {
            try
            {
                Log.Debug(LogConst.START);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(LogConst.DONE);
            }
        }
    }
}

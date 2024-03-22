//
// File: "WorkerThread.cs"
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

using log4net;

namespace AsesAutoTypeLib
{
    /// <summary>
    ///  This class shall implement all functionality 
    ///  that originate from any dialog or form using 
    ///  dedicated worker threads.
    ///  Windows Apps shall always be designed multi-threaded 
    ///  to prevent "freezing" situations.
    /// </summary>
    internal class WorkerThread
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(WorkerThread));
                return m_Log;
            }
        }
        #endregion

        private WorkerConfig? m_WorkerConfig = null;
        private WorkerConfig WorkerConfig
        {
            get
            {
                if (this.m_WorkerConfig == null)
                    this.m_WorkerConfig = new WorkerConfig();
                return this.m_WorkerConfig;
            }
            set 
            {
                this.m_WorkerConfig = value;
            }
        }
        public WorkerConfig GetWorkerConfig()
        {
            return this.WorkerConfig;
        }
        public WorkerConfig SetWorkerConfig(WorkerConfig workerConfig)
        {
            WorkerConfig prev = this.GetWorkerConfig();
            this.WorkerConfig = workerConfig;
            return prev;
        }

        /// <summary>
        ///  Default constructor.
        /// </summary>
        public WorkerThread()
        {
            try
            {
                Log.Debug(LogConst.Start);
                Initialze(new WorkerConfig());
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(LogConst.Done);
            }
        }

        /// <summary>
        ///  Custom constructor.
        /// </summary>
        public WorkerThread(WorkerConfig workerConfig)
        {
            try
            {
                Log.Debug(LogConst.Start);
                Initialze(workerConfig);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(LogConst.Done);
            }
        }

        private bool Initialze(WorkerConfig workerConfig)
        {
            try
            {
                Log.Debug(LogConst.Start);
                this.SetWorkerConfig(workerConfig);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                Log.Debug(LogConst.Done);
            }
        }
    }
}

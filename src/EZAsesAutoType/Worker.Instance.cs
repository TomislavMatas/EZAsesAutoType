//
// File: "Worker.Instance.cs"
//
// Revision History: 
// 2024/05/31:TomislavMatas: Version "1.126.0"
// * Simplify log4net implementations.
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System.Diagnostics;

using log4net;

namespace EZAsesAutoType
{
    /// <summary>
    ///  This class implements app specific worker methodz. 
    /// </summary>
    internal partial class Worker
    {
        #region log4net

        private static readonly ILog Log = LogManager.GetLogger(typeof(WorkerConfig));

        [Conditional("DEBUG")]
        private static void LogTrace(object message)
        {
#if DEBUG
            Log.Debug(message);
#endif
        }

        #endregion

        #region propertiez

        private AppHandler? m_AppHandler = null;
        private AppHandler AppHandler
        {
            get
            {
                if (m_AppHandler == null)
                    m_AppHandler = new AppHandler(this);
                return m_AppHandler;
            }
            set
            {
                m_AppHandler = value;
            }
        }
        public AppHandler GetAppHandler()
        {
            return this.AppHandler;
        }
        public AppHandler SetAppHandler(AppHandler appHandler)
        {
            AppHandler prev = this.GetAppHandler();
            this.AppHandler = appHandler;
            return prev;
        }

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
        #endregion

        #region initializerz

        /// <summary>
        /// Initialize instance.
        /// </summary>
        /// <param name="workerConfig"></param>
        /// <returns></returns>
        private bool Initialze(WorkerConfig workerConfig)
        {
            try
            {
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
            }
        }

        #endregion

        #region constructorz

        /// <summary>
        ///  Default constructor.
        /// </summary>
        public Worker()
        {
            try
            {
                LogTrace(Const.LogStart);
                if(!Initialze(new WorkerConfig()))
                    throw new Exception(nameof(Initialze) + Const.LogFail);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        /// <summary>
        ///  Custom constructor.
        /// </summary>
        public Worker(WorkerConfig workerConfig)
        {
            try
            {
                LogTrace(Const.LogStart);
                if(!Initialze(workerConfig))
                    throw new Exception(nameof(Initialze) + Const.LogFail);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        #endregion

    } // class

} // namespace

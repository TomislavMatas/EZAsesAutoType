//
// File: "AppHandler.cs"
//
// Revision History: 
// 2024/08/06:TomislavMatas: Version "1.127.1"
// * Implement RegistryHelper.
// 2024/05/31:TomislavMatas: Version "1.126.0"
// * Simplify log4net implementations.
// 2024/04/10:TomislavMatas: Version "1.123.3"
// * Implement singleton pattern for "WorkerConfig" and "Worker".
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System.Diagnostics;

using log4net;

namespace EZAsesAutoType
{
    /// <summary>
    ///  This class shall handle all user interactions which
    ///  originate from any dialog or form.
    /// </summary>
    internal class AppHandler
    {
        #region log4net

        private static readonly ILog Log = LogManager.GetLogger(typeof(AppHandler));

        [Conditional("DEBUG")]
        private static void LogTrace(object message)
        {
#if DEBUG
            Log.Debug(message);
#endif
        }

        #endregion

        #region propertiez
        private object? m_Requestor = null;
        private object? Requestor
        {
            get
            {
                return m_Requestor;
            }
            set
            {
                m_Requestor = value;
            }
        }
        public object? GetRequestor()
        {
            return this.Requestor;
        }
        public object? SetRequestor(object? requestor)
        {
            object? prev = this.GetRequestor();
            this.Requestor = requestor;
            return prev ;
        }

        private WorkerConfig? m_WorkerConfig = null;
        private WorkerConfig? WorkerConfig
        {
            get
            {
                if (m_WorkerConfig == null)
                    m_WorkerConfig = new WorkerConfig();

                return m_WorkerConfig;
            }
            set
            {
            m_WorkerConfig = value;
            }
        }
        private WorkerConfig? GetWorkerConfig()
        {
            return this.WorkerConfig;
        }
        private WorkerConfig? SetWorkerConfig(WorkerConfig? workerConfig)
        {
            WorkerConfig? prev = this.GetWorkerConfig();
            this.WorkerConfig = workerConfig;
            return prev;
        }

        private Worker? m_Worker = null;
        private Worker? Worker
        {
            get
            {
                if (m_Worker == null)
                    m_Worker = new Worker();

                return m_Worker;
            }
            set
            {
                m_Worker = value;
            }
        }
        private Worker? GetWorker()
        {
            return this.Worker;
        }
        private Worker? SetWorker(Worker? worker)
        {
            Worker? prev = this.GetWorker();
            this.Worker = worker;
            return prev;
        }

        private RegistryHelper? m_RegistryHelper = null;
        private RegistryHelper? RegistryHelper
        {
            get
            {
                if (m_RegistryHelper == null)
                    m_RegistryHelper = new RegistryHelper();

                return m_RegistryHelper;
            }
            set
            {
                m_RegistryHelper = value;
            }
        }
        private RegistryHelper? GetRegistryHelper()
        {
            return this.RegistryHelper;
        }
        private RegistryHelper? SetRegistryHelper(RegistryHelper? registryHelper)
        {
            RegistryHelper? prev = this.GetRegistryHelper();
            this.RegistryHelper = registryHelper;
            return prev;
        }

        #endregion

        #region initializerz

        /// <summary>
        /// Initialize instance
        /// </summary>
        /// <param name="requestor"></param>
        /// <returns></returns>
        private bool Initialze(object? requestor = null)
        {
            try
            {
                LogTrace(Const.LogStart);
                this.SetRequestor(requestor);
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
        public AppHandler()
        {
            try
            {
                LogTrace(Const.LogStart);
                if (!Initialze(null))
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
        public AppHandler(object requestor)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (!Initialze(requestor))
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
        
        public bool LoadUserSettings(out UserSettings? userSettings)
        {
            try
            {
                LogTrace(Const.LogStart);
                RegistryHelper? registryHelper = this.GetRegistryHelper();
                if (registryHelper != null)
                    if (registryHelper.LoadVersionIndependentUserSettings(out userSettings))
                        return true;

                // nothing in registry yet try to initialze with UserSettings default values.
                userSettings = new UserSettings();
                if(!userSettings.Load())
                    throw new Exception(nameof(userSettings.Load) + Const.LogFail);
                
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                userSettings = null ;
                return false;
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        public bool SaveUserSettings(UserSettings userSettings)
        {
            try
            {
                LogTrace(Const.LogStart);
                if(!userSettings.Save())
                    throw new Exception(nameof(userSettings.Save) + Const.LogFail);

                RegistryHelper? registryHelper = this.GetRegistryHelper();
                if (registryHelper != null)
                    if (!registryHelper.SaveVersionIndependentUserSettings(userSettings))
                        throw new Exception(nameof(registryHelper.SaveVersionIndependentUserSettings) + Const.LogFail);

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

        public bool DoDailyPunch(UserSettings userSettings)
        {
            try
            {
                LogTrace(Const.LogStart);
                WorkerConfig? workerConfig = this.GetWorkerConfig();
                if (workerConfig == null)
                    throw new Exception(nameof(workerConfig) + Const.LogIsNull);

                Worker? worker = this.GetWorker();
                if (worker == null)
                    throw new Exception(nameof(worker) + Const.LogIsNull);

                workerConfig.SetUserSettings(userSettings);
                worker.SetWorkerConfig(workerConfig);
                worker.SetAppHandler(this);
                if(!worker.DoDailyPunch())
                    throw new Exception(nameof(DoDailyPunch) + Const.LogFail);

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

    } // class

} // namespace

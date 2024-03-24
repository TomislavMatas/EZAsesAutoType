//
// File: "AppHandler.cs"
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

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
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(AppHandler));
                return m_Log;
            }
        }
        #endregion

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

        public object? SetRequestor(object? requestor)
        {
            object? prev = this.GetRequestor();
            this.Requestor = requestor;
            return prev ;
        }

        public object? GetRequestor()
        {
            return this.Requestor;
        }

        /// <summary>
        ///  Default constructor.
        /// </summary>
        public AppHandler()
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (!Initialze(null))
                    throw new Exception(nameof(Initialze) + Const.LogFail);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

        /// <summary>
        ///  Custom constructor.
        /// </summary>
        public AppHandler(object requestor)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if (!Initialze(null))
                    throw new Exception(nameof(Initialze) + Const.LogFail);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            finally
            {
                Log.Debug(Const.LogDone);
            }
        }

        public bool LoadUserSettings(out UserSettings? userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
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
                Log.Debug(Const.LogDone);
            }
        }

        public bool SaveUserSettings(UserSettings userSettings)
        {
            try
            {
                Log.Debug(Const.LogStart);
                if(!userSettings.Save())
                    throw new Exception(nameof(userSettings.Save) + Const.LogFail);

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

        private bool Initialze(object? requestor = null)
        {
            try
            {
                Log.Debug(Const.LogStart);
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
                Log.Debug(Const.LogDone);
            }
        }

    } // class

} // namespace

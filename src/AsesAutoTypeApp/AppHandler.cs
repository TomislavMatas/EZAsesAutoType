//
// File: "AppHandler.cs"
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

using log4net;
using LogConst = AsesAutoTypeLib.LogConst;

namespace AsesAutoTypeApp
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
                Log.Debug(LogConst.START);
                this.Initialze(null);
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

        /// <summary>
        ///  Custom constructor.
        /// </summary>
        public AppHandler(object requestor)
        {
            try
            {
                Log.Debug(LogConst.START);
                this.Initialze(requestor);
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

        private bool Initialze(object? requestor = null)
        {
            try
            {
                Log.Debug(LogConst.START);
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
                Log.Debug(LogConst.DONE);
            }
        }
    }
}

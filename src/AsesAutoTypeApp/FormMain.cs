//
// File: "FormMain.cs"
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
    ///  The main form displayed on app startup.
    /// </summary>
    public partial class FormMain : Form
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(FormMain));
                return m_Log;
            }
        }
        #endregion

        private InteractionHandler? m_InteractionHandler = null;
        private InteractionHandler InteractionHandler
        {
            get
            {
                if (m_InteractionHandler == null)
                    m_InteractionHandler = new InteractionHandler(this);
                return m_InteractionHandler;
            }
            set
            {
                m_InteractionHandler = value;
            }
        }

        internal InteractionHandler SetInteractionHandler(InteractionHandler interactionHandler)
        {
            InteractionHandler prev = this.GetInteractionHandler();
            this.InteractionHandler = interactionHandler;
            return prev;
        }

        internal InteractionHandler GetInteractionHandler()
        {
            return this.InteractionHandler;
        }

        /// <summary>
        ///  Default constructor.
        /// </summary>
        public FormMain()
        {
            try
            {
                Log.Debug(LogConst.START);
                InitializeComponent();
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
                this.InteractionHandler = new InteractionHandler(this);
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

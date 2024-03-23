//
// File: "FormMain.cs"
//
// Revision History:
// 2024/03/22:TomislavMatas: Version "24.123.0.0"
// * Initial version.
//

using log4net;
using LogConst = EZSeleniumLib.LogConst;

namespace EZAsesAutoType
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

        private AppHandler? m_InteractionHandler = null;
        private AppHandler InteractionHandler
        {
            get
            {
                if (m_InteractionHandler == null)
                    m_InteractionHandler = new AppHandler(this);
                return m_InteractionHandler;
            }
            set
            {
                m_InteractionHandler = value;
            }
        }

        internal AppHandler SetInteractionHandler(AppHandler interactionHandler)
        {
            AppHandler prev = this.GetInteractionHandler();
            this.InteractionHandler = interactionHandler;
            return prev;
        }

        internal AppHandler GetInteractionHandler()
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
                Log.Debug(LogConst.Start);
                InitializeComponent();
                if (!Initialze())
                    throw new Exception(nameof(Initialze) + LogConst.Fail);
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

        private bool Initialze()
        {
            try
            {
                Log.Debug(LogConst.Start);
                this.InteractionHandler = new AppHandler(this);
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

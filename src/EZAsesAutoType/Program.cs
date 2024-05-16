//
// File: "Program.cs"
//
// Revision History:
// 2024/05/04:TomislavMatas: Version "1.125.0":
// * Add handling of commandline arguments.
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using log4net;

namespace EZAsesAutoType
{
    /// <summary>
    ///  The main program class of the application.
    ///  Provides the "Main()" function as the 
    ///  main entry point for the application.
    /// </summary>
    internal static class Program
    {
        #region log4net
        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(Program));
                return m_Log;
            }
        }
        #endregion

        /// <summary>
        ///  The main entry point for the application.
        ///  To customize application configuration such as 
        ///  set high DPI settings or default font,
        ///  see: < https://aka.ms/applicationconfiguration >
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            try
            {
                Log.Debug(Const.LogStart);
                ApplicationConfiguration.Initialize();
                Application.Run(new FormMain(args));
                return 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return 1;
            }
            finally
            { 
                Log.Debug(Const.LogDone);
            }
        }

    } // class

} // namespace

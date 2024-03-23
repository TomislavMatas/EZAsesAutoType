//
// File: "Program.cs"
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "24.123.0.0"
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
        static void Main()
        {
            try
            {
                Log.Debug(Const.LogStart);
                ApplicationConfiguration.Initialize();
                Application.Run(new FormMain());
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

    } // class

} // namespace

//
// File: "Program.cs"
//
// Revision History:
// 2024/05/31:TomislavMatas: Version "1.126.0"
// * Simplify log4net implementations.
// 2024/05/04:TomislavMatas: Version "1.125.0":
// * Add handling of commandline arguments.
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using log4net;
using System.Diagnostics;

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

        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        [Conditional("DEBUG")]
        private static void LogTrace(object message)
        {
#if DEBUG
            Log.Debug(message);
#endif
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
                LogTrace(Const.LogStart);
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
                LogTrace(Const.LogDone);
            }
        }

    } // class

} // namespace

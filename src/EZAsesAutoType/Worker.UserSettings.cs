//
// File: "Worker.UserSettings.cs"
//
// Revision History: 
// 2024/05/31:TomislavMatas: Version "1.126.0"
// * Simplify log4net implementations.
// 2024/04/13:TomislavMatas: Version "1.123.4"
// * Rename "ASESPunchIn"  to "ASESPunchInAM".
// * Rename "ASESPunchOut" to "ASESPunchOutAM".
// * Add "ASESPunchInPM" and "ASESPunchOutPM".
// * Add "GetTimePairListDefault()".
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System.Collections.Generic;

namespace EZAsesAutoType
{
    internal partial class Worker
    {
        #region "UserSetting" - wrapperz

        private string GetWebDriver()
        {
            return this.WorkerConfig.GetUserSettings().WebDriver;
        }

        private string GetASESBaseUrl()
        {
            return this.WorkerConfig.GetUserSettings().ASESBaseUrl;
        }

        private string GetASESUserId()
        {
            return this.WorkerConfig.GetUserSettings().ASESUserId;
        }

        private string GetASESPassword()
        {
            return this.WorkerConfig.GetUserSettings().ASESPassword;
        }

        private string GetASESClient()
        {
            return this.WorkerConfig.GetUserSettings().ASESClient;
        }

        private string GetASESLanguage()
        {
            return this.WorkerConfig.GetUserSettings().ASESLanguage;
        }

        private string GetASESPunchInAM()
        {
            return this.WorkerConfig.GetUserSettings().ASESPunchInAM;
        }

        private string GetASESPunchOutAM()
        {
            return this.WorkerConfig.GetUserSettings().ASESPunchOutAM;
        }

        private string GetASESPunchInPM()
        {
            return this.WorkerConfig.GetUserSettings().ASESPunchInPM;
        }

        private string GetASESPunchOutPM()
        {
            return this.WorkerConfig.GetUserSettings().ASESPunchOutPM;
        }

        private List<TimePair>? GetTimePairListDefault()
        {
            try
            {
                LogTrace(Const.LogStart);

                string? punchInAM  = this.GetASESPunchInAM();
                string? punchOutAM = this.GetASESPunchOutAM();
                string? punchInPM  = this.GetASESPunchInPM();
                string? punchOutPM = this.GetASESPunchOutPM();

                // 1: " " - " "
                // 2: " " - " "
                if( string.IsNullOrEmpty(punchInAM) && string.IsNullOrEmpty(punchOutAM)
                 && string.IsNullOrEmpty(punchInPM) && string.IsNullOrEmpty(punchOutPM)
                )
                {   // --> empty list
                    return [];
                }

                // 1: "a" - "b"
                // 2: " " - " "
                if( !string.IsNullOrEmpty(punchInAM) && !string.IsNullOrEmpty(punchOutAM)
                 &&  string.IsNullOrEmpty(punchInPM) &&  string.IsNullOrEmpty(punchOutPM)
                )
                {   // --> ["a" - "b"]
                    return [new TimePair(punchInAM, punchOutAM)];
                }

                // 1: "a" - " "
                // 2: " " - "b"
                if( !string.IsNullOrEmpty(punchInAM) &&  string.IsNullOrEmpty(punchOutAM)        
                 &&  string.IsNullOrEmpty(punchInPM) && !string.IsNullOrEmpty(punchOutPM)
                )
                {
                    // --> ["a" - "b"]
                    return [new TimePair(punchInAM, punchOutPM)];
                }

                // 1: " " - " "
                // 2: "a" - "b"
                if(  string.IsNullOrEmpty(punchInAM) &&  string.IsNullOrEmpty(punchOutAM)
                 && !string.IsNullOrEmpty(punchInPM) && !string.IsNullOrEmpty(punchOutPM)
                )
                {   // --> ["a" - "b"]
                    return [new TimePair(punchInPM, punchOutPM)];
                }

                // 1: "a" - "b"
                // 2: "c" - "d"
                if(!string.IsNullOrEmpty(punchInAM) && !string.IsNullOrEmpty(punchOutAM)
                && !string.IsNullOrEmpty(punchInPM) && !string.IsNullOrEmpty(punchOutPM)
                )
                {   // --> ["a" - "b", "c" - "d"]
                    return [new TimePair(punchInAM, punchOutAM)
                           ,new TimePair(punchInPM, punchOutPM)];
                }

                // no filter matched same as "exception": --> null
                return null;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null; 
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        #endregion

    } // class

} // namespace

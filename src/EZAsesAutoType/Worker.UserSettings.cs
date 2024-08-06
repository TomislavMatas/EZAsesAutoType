//
// File: "Worker.UserSettings.cs"
//
// Revision History: 
// 2024/08/05:TomislavMatas: Version "1.127.1"
// * Implement "RandomizeDeviation" and "RoundDown" 
//   to make punches look more "human~like".
// * Fix FlipCoin.
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
using System.Diagnostics.Eventing.Reader;

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

        private int GetASESPunchDeviation()
        {
            return this.WorkerConfig.GetUserSettings().ASESPunchDeviation;
        }

        private List<TimePair> GetTimePairListDefault()
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

                // no filter matched same as "exception":
                // return empty array
                return [];
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return []; 
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        /// <summary>
        /// Singelton helper variable.
        /// </summary>
        private Random _randomizer = null;

        /// <summary>
        /// Instance of class "Random".
        /// </summary>
        private Random Randomizer
        {
            get
            {
                if (_randomizer == null)
                    _randomizer = new Random();
                return _randomizer;
            }
        }

        /// <summary>
        /// Flip a coin using private "Randomizer" class instance.
        /// Note: Randomizer.Next(0,2) returns either 0 or 1.
        /// </summary>
        /// <returns>
        /// true = head, false = tail
        /// </returns>
        private bool FlipCoin()
        {

            int coinFlip = Randomizer.Next(0,2);
            return (coinFlip == 1);
        }

        /// <summary>
        /// Return a random integer between 1 and maxValue (inklusivly).
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private int RollDice(int maxValue)
        {
            return Randomizer.Next(1, maxValue + 1);
        }

        private string? addMinutes(string? timeStr, int minutes)
        {
            if(timeStr == null)
                return null;

            if (DateTime.TryParse(timeStr, out DateTime dateTime))
                return dateTime.AddMinutes(minutes).ToString("HH:mm");

            return null;
        }

        /// <summary>
        /// Round down value to given fraction.
        /// Expected results:
        /// RoundDown( 0,5) =  0
        /// RoundDown( 6,5) =  5
        /// RoundDown(10,5) = 10
        /// RoundDown(11,5) = 10
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fraction"></param>
        /// <returns></returns>
        private int RoundDown(int value, int fraction)
        {
            if (value < 0 || value > 60)
                value = 0;

            if (fraction<0 || fraction>60)
                fraction = 0;

            var delta = value % fraction;
            return value - delta;
        }

        private int RandomizeDeviation(int maxDeviation)
        {
            if (maxDeviation <= 0)
                maxDeviation = 0;

            // Generate a random deviation up to the maximum value
            // to make the deviation look "organic".
            int deviation = this.RollDice(maxDeviation);

            // do not use "exact minutes" like "09:01" or "09:02",
            // because no human would punch in exactly to the minute.
            // Use a stepping spread of five minutes .
            deviation = this.RoundDown(deviation, 5);

            // flip a coin to decide wether to "add" or "substract" deviation.
            if (FlipCoin())
                deviation = deviation * (-1);

            return deviation;
        }

        private List<TimePair> ApplyDeviation(List<TimePair> timePairs, int deviation)
        {
            try
            {
                LogTrace(Const.LogStart);
                if (deviation == 0)
                    return timePairs;
                
                int timePairsCount = timePairs.Count;
                switch (timePairsCount)
                {
                    case 1:
                        // adjust punch in and punch out value
                        timePairs[0].PunchIn = addMinutes(timePairs[0].PunchIn, deviation);
                        timePairs[0].PunchOut = addMinutes(timePairs[0].PunchOut, deviation);
                        break;
                    case 2:
                        // adjust "first" punch in and "last" punch out value
                        // and leave "lunch break" as is.
                        timePairs[0].PunchIn = addMinutes(timePairs[0].PunchIn, deviation);
                        timePairs[1].PunchOut = addMinutes(timePairs[1].PunchOut, deviation);
                        break;
                    default:
                        break;
                }
                return timePairs;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return timePairs;
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        #endregion

    } // class

} // namespace

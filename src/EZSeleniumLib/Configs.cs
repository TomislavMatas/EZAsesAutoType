//
// File: "Configs.cs"
//
// Summary:
// This class provides the interface for access to 
// the "App.config" file using specific wrapper methods.
//
// Revision History: 
// 2024/07/01:TomislavMatas: Version "4.22.2"
// * Add masking of "passwd" to "GetAppSettingString".
// 2024/05/31:TomislavMatas: Version "4.21.1"
// * Simplify log4net implementations.
// 2024/05/04:TomislavMatas: Version "4.20.0"
// * Upgrade to .NET version 8.
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

using System.Configuration;
using System.Diagnostics;

using log4net;

namespace EZSeleniumLib
{
    /// <summary>
    /// Diese Klasse stellt Methoden fuer den Zugriff
    /// auf die Werte in der "App.config" zuer Verfuegung.
    /// </summary>
    public static class Configs
    {
        #region log4net

        private static readonly ILog Log = LogManager.GetLogger(typeof(Configs));

        [Conditional("DEBUG")]
        private static void LogTrace(object message)
        {
#if DEBUG
            Log.Debug(message);
#endif
        }

        #endregion

        public static int ToInt(string value, int defaultValue = 0)
        {
            if (value == null)
                return defaultValue;

            if (Int32.TryParse(value, out int result))
                return result;

            return defaultValue;
        }

        public static bool ToBool(string value, bool defaultValue = false)
        {
            if (value == null)
                return defaultValue;

            if (bool.TryParse(value, out bool result))
                return result; // try parse handles variations of "true" and "false" only.

            #region dedicated "true" values

            if ("1".Equals(value))
                return true;

            if ("y".Equals(value, StringComparison.InvariantCultureIgnoreCase))
                return true;

            if ("yes".Equals(value, StringComparison.InvariantCultureIgnoreCase))
                return true;

            if ("enabled".Equals(value, StringComparison.InvariantCultureIgnoreCase))
                return true;

            #endregion

            #region dedicated "false" values

            if ("0".Equals(value))
                return false;

            if ("n".Equals(value, StringComparison.InvariantCultureIgnoreCase))
                return false;

            if ("no".Equals(value, StringComparison.InvariantCultureIgnoreCase))
                return false;

            if ("disabled".Equals(value, StringComparison.InvariantCultureIgnoreCase))
                return false;

            #endregion

            // none of the supproted string values : return defaultValue
            return defaultValue;
        }

        public static bool GetAppSettingBool(string name, bool defaultValue = false)
        {
            try
            {
                string? valueString = ConfigurationManager.AppSettings[name];
                if (valueString == null)
                    return defaultValue;

                bool value = ToBool(valueString, defaultValue);
                Log.Debug(String.Format("name='{0}' value={1}", name, value));
                return value;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return defaultValue;
            }
        }

        public static int GetAppSettingInt(string name, int defaultValue = 0)
        {
            try
            {
                string? valueString = ConfigurationManager.AppSettings[name];
                if (valueString == null)
                    return defaultValue;

                int value = ToInt(valueString, defaultValue);
                Log.Debug(String.Format("name='{0}' value={1}", name, value));
                return value;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return defaultValue;
            }
        }

        public static string GetAppSettingString(string name, string defaultValue = "")
        {
            try
            {
                string? value = ConfigurationManager.AppSettings[name];
                if (value == null)
                    value=defaultValue;

                if( name.ToLower().Contains("password") 
                 || name.ToLower().Contains("pwd")
                 || name.ToLower().Contains("passwd") )
                    Log.Debug(String.Format("name='{0}' value='{1}'", name, "***"));
                else
                    Log.Debug(String.Format("name='{0}' value='{1}'", name, value));

                return value;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return defaultValue;
            }
        }

    } // class

} // namespace
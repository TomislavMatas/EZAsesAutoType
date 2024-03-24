//
// File: ConfigApi.cs
//
// Summary:
// This class provides the interface for access to 
// the "App.config" file using specific wrapper methods.
//
// Revision History: 
// 2024/03/22:TomislavMatas: Version "1.0.0.0"
// * Initial version.
//

using System.Configuration;

using log4net;

namespace EZAsesAutoType
{
    /// <summary>
    /// Diese Klasse stellt Methoden fuer den Zugriff
    /// auf die Werte in der "App.config" zuer Verfuegung.
    /// </summary>
    internal static class ConfigApi
    {
        #region log4net

        private static ILog? m_Log = null;
        private static ILog Log
        {
            get
            {
                if (m_Log == null)
                    m_Log = LogManager.GetLogger(typeof(ConfigApi));
                return m_Log;
            }
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
                Log.Debug(String.Format("name='{0}' appSetting={1}", name, value));
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
                Log.Debug(String.Format("name='{0}' appSetting={1}", name, value));
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
                string? appSetting = ConfigurationManager.AppSettings[name];
                string value;
                if (appSetting == null)
                    value = defaultValue;
                else
                    value = appSetting;

                if (name.Contains("password", StringComparison.OrdinalIgnoreCase)
                || name.Contains("password", StringComparison.OrdinalIgnoreCase)
                || name.Contains("pwd", StringComparison.OrdinalIgnoreCase))
                    Log.Debug(String.Format("name='{0}' appSetting='{1}'", name, "***"));
                else
                    Log.Debug(String.Format("name='{0}' appSetting='{1}'", name, value));

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

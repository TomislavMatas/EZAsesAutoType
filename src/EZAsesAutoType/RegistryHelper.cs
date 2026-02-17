//
// File: "AppHandler.cs"
//
// Revision History: 
// 2026/02/17:TomislavMatas: v4.40.1450
// * Implement "ASESUseSso" and "ASESSsoAccount" ,
// 2024/09/26:TomislavMatas: Version "1.129.0"
// * Make sure that version independent values from registry
//   supersede/override version dependent values from "App.config".
// * Implement more distinct handling of TimePairs in new method
//   "LoadVersionIndependentUserSettingsTimePairs".
// * Extract handling of boolean flags into new method
//   "LoadVersionIndependentUserSettingsBoolFlags".
// 2024/08/07:TomislavMatas: Version "1.127.2"
// * Fix writing of values to registry.
// * Add "GetValueInt".
// 2024/08/06:TomislavMatas: Version "1.127.1"
// * Initial version.
//

using System.Diagnostics;

using log4net;
using Microsoft.Win32;

namespace EZAsesAutoType
{
    /// <summary>
    ///  This class shall handle all interactions whith the windows registry
    /// </summary>
    internal class RegistryHelper
    {
        #region log4net

        private static readonly ILog Log = LogManager.GetLogger(typeof(RegistryHelper));

        [Conditional("DEBUG")]
        private static void LogTrace(object message)
        {
#if DEBUG
            Log.Debug(message);
#endif
        }

        #endregion

        private string GetRegistrySubKeyPath()
        {
            string companyName = EZAsesAutoType.Const.AssemblyCompanyName;
            string productName = EZAsesAutoType.Const.AssemblyProductName;
            return string.Format(@"Software\{0}\{1}", companyName, productName);
        }

        /// <summary>
        /// Returns null, if value not found.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string? GetValueString(RegistryKey key, string name)
        {
            object? value = key.GetValue(name);
            if (value == null)
                return null;

            return (string)value;
        }

        /// <summary>
        /// Returns defaultValue, if value not found.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private string GetValueString(RegistryKey key, string name, string defaultValue)
        {
            object? value = key.GetValue(name, defaultValue);
            if(value == null)
                return defaultValue;

            return (string)value;
        }

        private int GetValueInt(RegistryKey key, string name, int defaultValue)
        {
            object? value = key.GetValue(name, defaultValue);
            if (value == null)
                return defaultValue;

            return (int)value;
        }

        /// <summary>
        /// Lookup version independent individual values from registry.
        /// Version dependent values are loaded from "App.config" first and
        /// shall be used as defaults, if version independent values 
        /// are not (yet) present in registry.
        /// </summary>
        /// <param name="userSettings"></param>
        /// <returns></returns>
        public bool LoadVersionIndependentUserSettings(out UserSettings? userSettings)
        {
            try
            {
                string registrySubKeyPath = this.GetRegistrySubKeyPath();
                if (registrySubKeyPath == null)
                    throw new Exception(nameof(this.GetRegistrySubKeyPath) + Const.LogFail);

                RegistryKey? key = Registry.CurrentUser.OpenSubKey(registrySubKeyPath);
                if(key == null)
                    throw new Exception(nameof(Registry.CurrentUser.OpenSubKey) + Const.LogFail);

                userSettings = new UserSettings();
                userSettings.Load(); 
                userSettings.ASESBaseUrl        = this.GetValueString(key, nameof(userSettings.ASESBaseUrl),        userSettings.ASESBaseUrl        );
                userSettings.ASESUserId         = this.GetValueString(key, nameof(userSettings.ASESUserId),         userSettings.ASESUserId         );
                userSettings.ASESPassword       = this.GetValueString(key, nameof(userSettings.ASESPassword),       userSettings.ASESPassword       );
                userSettings.ASESClient         = this.GetValueString(key, nameof(userSettings.ASESClient),         userSettings.ASESClient         );
                userSettings.ASESLanguage       = this.GetValueString(key, nameof(userSettings.ASESLanguage),       userSettings.ASESLanguage       );
                userSettings.WebDriver          = this.GetValueString(key, nameof(userSettings.WebDriver),          userSettings.WebDriver          );
                userSettings.ASESPunchDeviation = this.GetValueInt(key,    nameof(userSettings.ASESPunchDeviation), userSettings.ASESPunchDeviation );
                userSettings.ASESSsoAccount     = this.GetValueString(key, nameof(userSettings.ASESSsoAccount),     userSettings.ASESSsoAccount     );
                LoadVersionIndependentUserSettingsTimePairs(key, userSettings);
                LoadVersionIndependentUserSettingsBoolFlags(key, userSettings);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                userSettings = null;
                return false;
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

        /// <summary>
        /// The processing flags and their combinations are arbitary and optional depending to individual needs.
        /// The boolean flag values required dedicated type casts.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userSettings"></param>
        private void LoadVersionIndependentUserSettingsBoolFlags(RegistryKey key, UserSettings userSettings)
        {
            if (!bool.TryParse(this.GetValueString(key, nameof(userSettings.DoLogin), userSettings.DoLogin.ToString()), out userSettings.DoLogin))
                userSettings.DoLogin = true;

            if (!bool.TryParse(this.GetValueString(key, nameof(userSettings.DoPunch), userSettings.DoPunch.ToString()), out userSettings.DoPunch))
                userSettings.DoPunch = true;

            if (!bool.TryParse(this.GetValueString(key, nameof(userSettings.DoLogout), userSettings.DoLogout.ToString()), out userSettings.DoLogout))
                userSettings.DoLogout = true;

            if (!bool.TryParse(this.GetValueString(key, nameof(userSettings.ASESUseSso), userSettings.ASESUseSso.ToString()), out userSettings.ASESUseSso))
                userSettings.ASESUseSso = true;
        }

        /// <summary>
        /// The TimePair combinations are arbitary and optional depending to individual needs.
        /// See --> Worker.GetTimePairListDefault() for some typical normalizations.
        /// The first TimePair (AM) is assumed the default use case and thus "mandatory".
        /// In such cases (and only this case) use the default from "App.config".
        /// The second TimePair (PM) is optional and utilized by users,
        /// which require a "lunch break" or need to split working time for 
        /// other reasons. If only one single pair is stored to registry,
        /// the default mimic does not work here.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userSettings"></param>
        /// <returns></returns>
        private void LoadVersionIndependentUserSettingsTimePairs(RegistryKey key, UserSettings userSettings)
        {
            string? registryValue;

            // The more sophisticate handling of the TimePairs
            // will allow some of the most common combinations:
            //   1: "09:00" to "17:00"
            //   2: "     " to "     "
            // --> Handle as single TimePair
            //   1: "09:00" to "     "
            //   2: "     " to "17:00"
            // --> Handle as single TimePair
            //   1: "09:00" to "12:00"
            //   2: "12:00" to "17:00"
            // --> Handle as two TimePairs
            registryValue = this.GetValueString(key, nameof(userSettings.ASESPunchInAM));
            if (registryValue == null)
                userSettings.ASESPunchInAM = string.Empty;
            else
                userSettings.ASESPunchInAM = registryValue;

            registryValue = this.GetValueString(key, nameof(userSettings.ASESPunchOutAM));
            if (registryValue == null)
                userSettings.ASESPunchOutAM = string.Empty;
            else
                userSettings.ASESPunchOutAM = registryValue;

            registryValue = this.GetValueString(key, nameof(userSettings.ASESPunchInPM));
            if (registryValue == null)
                userSettings.ASESPunchInPM = string.Empty;
            else
                userSettings.ASESPunchInPM = registryValue;

            registryValue = this.GetValueString(key, nameof(userSettings.ASESPunchOutPM));
            if (registryValue == null)
                userSettings.ASESPunchOutPM = string.Empty;
            else
                userSettings.ASESPunchOutPM = registryValue;

        }

        /// <summary>
        /// Store version independent individual values to registry.
        /// </summary>
        /// <param name="userSettings"></param>
        /// <returns></returns>
        public bool SaveVersionIndependentUserSettings(UserSettings userSettings)
        {
            try
            {
                string registrySubKeyPath = this.GetRegistrySubKeyPath();
                if (registrySubKeyPath == null)
                    throw new Exception(nameof(this.GetRegistrySubKeyPath) + Const.LogFail);

                RegistryKey? key = Registry.CurrentUser.OpenSubKey(registrySubKeyPath, true);
                if (key == null)
                {   // might be the first time this function has been called.
                    // create registry hive now.
                    key = Registry.CurrentUser.CreateSubKey(registrySubKeyPath, true);
                    if (key == null)
                        throw new Exception(nameof(this.GetRegistrySubKeyPath) + Const.LogFail);
                }

                key.SetValue(nameof(userSettings.ASESBaseUrl), userSettings.ASESBaseUrl);
                key.SetValue(nameof(userSettings.ASESUserId), userSettings.ASESUserId);
                key.SetValue(nameof(userSettings.ASESPassword), userSettings.ASESPassword);
                key.SetValue(nameof(userSettings.ASESClient), userSettings.ASESClient);
                key.SetValue(nameof(userSettings.ASESLanguage), userSettings.ASESLanguage);
                key.SetValue(nameof(userSettings.ASESPunchInAM), userSettings.ASESPunchInAM);
                key.SetValue(nameof(userSettings.ASESPunchOutAM), userSettings.ASESPunchOutAM);
                key.SetValue(nameof(userSettings.ASESPunchInPM), userSettings.ASESPunchInPM);
                key.SetValue(nameof(userSettings.ASESPunchOutPM), userSettings.ASESPunchOutPM);
                key.SetValue(nameof(userSettings.WebDriver), userSettings.WebDriver);
                key.SetValue(nameof(userSettings.DoLogin), userSettings.DoLogin);
                key.SetValue(nameof(userSettings.DoPunch), userSettings.DoPunch);
                key.SetValue(nameof(userSettings.DoLogout), userSettings.DoLogout);
                key.SetValue(nameof(userSettings.ASESPunchDeviation), userSettings.ASESPunchDeviation);
                key.SetValue(nameof(userSettings.ASESUseSso), userSettings.ASESUseSso);
                key.SetValue(nameof(userSettings.ASESSsoAccount), userSettings.ASESSsoAccount);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
            finally
            {
                LogTrace(Const.LogDone);
            }
        }

    } // class

} // namespace

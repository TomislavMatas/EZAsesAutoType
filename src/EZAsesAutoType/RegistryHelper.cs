//
// File: "AppHandler.cs"
//
// Revision History: 
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

        private string GetValueString(RegistryKey key, string name, string defaultValue)
        {
            object? value = key.GetValue(name, defaultValue);
            if(value == null)
                return defaultValue;

            return (string)value;
        }

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

                // make sure version dependent settings are loaded in any case.
                userSettings = new UserSettings();
                userSettings.Load(); 

                // lookup individual values and assign only if valuable.
                if(!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.ASESBaseUrl), string.Empty)) )
                    userSettings.ASESBaseUrl = this.GetValueString(key, nameof(userSettings.ASESBaseUrl), string.Empty);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.ASESUserId), string.Empty)))
                    userSettings.ASESUserId = this.GetValueString(key, nameof(userSettings.ASESUserId), string.Empty);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.ASESPassword), string.Empty)))
                    userSettings.ASESPassword = this.GetValueString(key, nameof(userSettings.ASESPassword), string.Empty);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.ASESClient), string.Empty)))
                    userSettings.ASESClient = this.GetValueString(key, nameof(userSettings.ASESClient), string.Empty);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.ASESLanguage), string.Empty)))
                    userSettings.ASESLanguage = this.GetValueString(key, nameof(userSettings.ASESLanguage), string.Empty);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.ASESPunchInAM), string.Empty)))
                    userSettings.ASESPunchInAM = this.GetValueString(key, nameof(userSettings.ASESPunchInAM), string.Empty);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.ASESPunchOutAM), string.Empty)))
                    userSettings.ASESPunchOutAM = this.GetValueString(key, nameof(userSettings.ASESPunchOutAM), string.Empty);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.ASESPunchInPM), string.Empty)))
                    userSettings.ASESPunchInPM = this.GetValueString(key, nameof(userSettings.ASESPunchInPM), string.Empty);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.ASESPunchOutPM), string.Empty)))
                    userSettings.ASESPunchOutPM = this.GetValueString(key, nameof(userSettings.ASESPunchOutPM), string.Empty);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.WebDriver), string.Empty)))
                    userSettings.WebDriver = this.GetValueString(key, nameof(userSettings.WebDriver), string.Empty);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.DoLogin), string.Empty)))
                    bool.TryParse(this.GetValueString(key, nameof(userSettings.DoLogin), string.Empty), out userSettings.DoLogin);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.DoPunch), string.Empty)))
                    bool.TryParse(this.GetValueString(key, nameof(userSettings.DoPunch), string.Empty), out userSettings.DoPunch);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.DoLogout), string.Empty)))
                    bool.TryParse(this.GetValueString(key, nameof(userSettings.DoLogout), string.Empty), out userSettings.DoLogout);

                if (!String.IsNullOrEmpty(this.GetValueString(key, nameof(userSettings.ASESPunchDeviation), string.Empty)))
                    int.TryParse(this.GetValueString(key, nameof(userSettings.ASESPunchDeviation), string.Empty), out userSettings.ASESPunchDeviation);

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

        public bool SaveVersionIndependentUserSettings(UserSettings userSettings)
        {
            try
            {
                string registrySubKeyPath = this.GetRegistrySubKeyPath();
                if (registrySubKeyPath == null)
                    throw new Exception(nameof(this.GetRegistrySubKeyPath) + Const.LogFail);

                RegistryKey? key = Registry.CurrentUser.OpenSubKey(registrySubKeyPath);
                if (key == null)
                    key = Registry.CurrentUser.CreateSubKey(registrySubKeyPath, true);

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

//
// File: "Settings.cs"
//
// Summary:
// Project specific settings handler implementations.
//
// Revision History: 
// 2024/04/04:TomislavMatas: Version "1.0.0"
// * Initial version.
//

namespace EZAsesAutoType.Properties {

    /// <summary>
    ///  Diese Klasse ermöglicht die Behandlung bestimmter Ereignisse der Einstellungsklasse:
    ///   Das SettingChanging-Ereignis wird ausgelöst, bevor der Wert einer Einstellung geändert wird.
    ///   Das PropertyChanged-Ereignis wird ausgelöst, nachdem der Wert einer Einstellung geändert wurde.
    ///   Das SettingsLoaded-Ereignis wird ausgelöst, nachdem die Einstellungswerte geladen wurden.
    ///   Das SettingsSaving-Ereignis wird ausgelöst, bevor die Einstellungswerte gespeichert werden.
    /// </summary>
    internal sealed partial class Settings 
    {
        
        public Settings() 
        {
            // this.SettingChanging += this.SettingChangingEventHandler;
            // this.SettingsSaving += this.SettingsSavingEventHandler;
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) 
        {
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) 
        {
        }

    } // class

} // namespace

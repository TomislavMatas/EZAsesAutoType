# "EZAsesAutoType.csproj"
This project is used to build the assembly "EZAsesAutoType.exe".

The required Selenium framework wrappers are provided by "EZSeleniumLib.dll".

Please refer to "EZSeleniumLib" project for details.

# Revision History
## 2024/04/10:TomislavMatas: Version "1.123.3"
* Implement singleton pattern for "WorkerConfig" and "Worker"
  within "AppHandler". Now when hitting "Cancel", the already 
  loaded browser instance stays "active". Otherwise,
  when processing has not been canceled by user, the
  browser instance will be closed immediatly after "logout".

## 2024/04/04:TomislavMatas: Version "1.123.1"
* BugFix in "FormMain": Set WorkerSupportsCancellation = true.

## 2024/04/04:TomislavMatas: Version "1.0.123"
* Update "MicrosoftWebDriver.exe" to version "123.0.2420.65".
* Edit "App.config" defaults:
* - Use "log4net.File=${UserProfile}\.EZAsesAutoType\EZAsesAutoType.log".
* - Use "ASES.WaitBefore.Logout=2".
* BugFix in "FormMain.SaveUserSettings".
* Change product name from "EZAsesAutoType" to "EZ ASES AutoType".

## 2024/04/04:TomislavMatas: Version "1.0.0"
* Initial Version.

# "EZAsesAutoType.csproj"
This project is used to build the assembly "EZAsesAutoType.exe".

The required Selenium framework wrappers are provided by "EZSeleniumLib.dll".

Please refer to "EZSeleniumLib" project for details.

# Revision History
## 2024/08/06:TomislavMatas: Version "1.127.1"
* Implement management of version independent UserSettings in windows registry. 

## 2024/08/05:TomislavMatas: Version "1.127.1"
* Add new UserSetting "ASESPunchDeviation".

## 2024/07/31:TomislavMatas: Version "1.127.0"
* Add "--search-engine-choice-country" to "EZSeleniumLib.Browser.AdditionalOptions.Chrome"
  to avoid the nagging popup that appears after upgrading to Chrome version 127.
* Update "chromedriver.exe" to version "127.0.6533.88".
* Update "msedgedriver.exe" to version "127.0.2651.74".
* Use EZSeleniumLib version "4.22.4".

## 2024/07/06:TomislavMatas: Version "1.127.0"
* Update "chromedriver.exe" to version "127.0.6533.26".
* Update "msedgedriver.exe" to version "127.0.2651.31".

## 2024/07/08:TomislavMatas: Version "1.126.4"
* Add checkbox "Do Logout" to main window.
* Implement handling of new command line arguments "/DoLogout".
* Removed input.Clear() call, because it was interfering with other 
  implementations when punching in time pairs.

## 2024/06/24:TomislavMatas: Version "1.126.3"
* Add input.Clear() call to improve editing of values already 
  present in time pair entry panel.

## 2024/06/24:TomislavMatas: Version "1.126.2"
* Update "EZSeleniumLib" to version "4.22.2" to 
  mitigate "stale element reference" errors.
* Improve fault tolerance on time slice entry.
* Force "repaint" when app has been startet with "/run" argument in order
  to show the correct driver versions.
* Fix a bug in edge driver version evaluation.

## 2024/06/24:TomislavMatas: Version "1.126.1"
* Update "Selenium" libs to version "4.22.0".
* Update "chromedriver.exe" to version "126.0.6478.115".
* Update "msedgedriver.exe" to version "126.0.2592.68".

## 2024/05/31:TomislavMatas: Version "1.126.0"
* Simplify log4net implementations.

## 2024/05/27:TomislavMatas: Version "1.126.0"
* Add handling of commandline argument "/close".
* Edit "EZAsesAutoType-UserManual.md": 
  Added section "Program startup arguments".

## 2024/05/26:TomislavMatas: Version "1.126.0"
* Increase default timeout values.

## 2024/05/15:TomislavMatas: Version "1.125.0":
* Update "chromedriver.exe" to version "125.0.6422.26".
* Update "MicrosoftWebDriver.exe" to version "125.0.2535.13".
* Update "log4net" libs to version "2.0.17".
* Update "Selenium" libs to version "4.20.0".
* Add handling of commandline argument "/run".

## 2024/05/02:TomislavMatas: Version "1.124.0"
* Update "chromedriver.exe" to version "124.0.6367.91".
* Update "MicrosoftWebDriver.exe" to version "124.0.2477.0".

## 2024/04/12:TomislavMatas: Version "1.123.4.0"
* Implement "second time pair" entry on main dialog.

## 2024/04/10:TomislavMatas: Version "1.123.3"
* Implement singleton pattern for "WorkerConfig" and "Worker"
  within "AppHandler". Now when hitting "Cancel", the already 
  loaded browser instance stays "active". Otherwise,
  when processing has not been canceled by user, the
  browser instance will be closed immediatly after "logout".

## 2024/04/04:TomislavMatas: Version "1.123.1"
* BugFix in "FormMain": Set WorkerSupportsCancellation = true.

## 2024/04/04:TomislavMatas: Version "1.0.124"
* Update "chromedriver.exe"       to version "124.0.6367.29".
* Update "MicrosoftWebDriver.exe" to version "124.0.2478.19".

## 2024/04/04:TomislavMatas: Version "1.0.123"
* Update "MicrosoftWebDriver.exe" to version "123.0.2420.65".
* Edit "App.config" defaults:
* - Use "log4net.File=${UserProfile}\.EZAsesAutoType\EZAsesAutoType.log".
* - Use "ASES.WaitBefore.Logout=2".
* BugFix in "FormMain.SaveUserSettings".
* Change product name from "EZAsesAutoType" to "EZ ASES AutoType".

## 2024/04/12:TomislavMatas: Version "1.123.4"
* Implement "second time pair" entry on main dialog.

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

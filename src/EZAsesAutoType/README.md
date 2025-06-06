# "EZAsesAutoType.csproj"
This project is used to build the assembly "EZAsesAutoType.exe".

The required Selenium framework wrappers are provided by "EZSeleniumLib.dll".

Please refer to "EZSeleniumLib" project for details.

# Revision History
## 2025/04/10:TomislavMatas: v1.135.0
* Update "Selenium.Support" to version "4.31.0".
* Update "Selenium.WebDriver" to version "4.31.0".
* Update "System.Management" to version "9.0.4".
* Update "System.Text.Json" to version "9.0.4".
* Update "log4net" to version "3.0.4".
* Update "chromedriver.exe" to version "135.0.7049.84".
* Update "msedgedriver.exe" to version "135.0.3179.54".
* Update "geckodriver.exe" to version "0.36.0".

## 2025/02/05:TomislavMatas: v1.133.0
* Update "EZSeleniumLib" to version "4.26.2".
* Update "chromedriver.exe" to version "133.0.6943.53".
* Update "msedgedriver.exe" to version "133.0.3065.39".

## 2024/11/22:TomislavMatas: v1.131.2
* Prevent browser teardown if an exception occoured during processing.
* Custom handling for Client="24-Cargo Zentrale".

## 2024/11/20:TomislavMatas: v1.131.1
* Update "EZSeleniumLib" to version "4.26.1".
* Adding "PlatformTarget=AnyCPU" explicitly solved the MSIX
  packaging error "MSB3270".
* WIP: Prototyping "EZAsesAutoTypeMSIX.wapproj".

## 2024/11/19:TomislavMatas: v1.131.0
* Update "EZSeleniumLib" to version "4.25.1".
* Update "chromedriver.exe" to version "131.0.6778.69".
* Update "msedgedriver.exe" to version "131.0.2903.51".
* Update "log4net" to version "3.0.3".

## 2024/09/26:TomislavMatas: v1.129.0
* Edit "RegistryHelper.cs": Make sure that version independent values from 
  registry supersede/override version dependent values from "App.config" and 
  implement more distinct handling of time pairs.
* Update "EZSeleniumLib" to version "4.25.0".
* Update "chromedriver.exe" to version "129.0.6668.71".
* Update "msedgedriver.exe" to version "129.0.2792.52".
* Update "geckodriver.exe"  to version "0.35.0".
* Update "log4net" to version "3.0.0".

## 2024/08/07:TomislavMatas: v1.127.2
* Assure that canvas has been sorted by date ascending before punching.
* Fix a bug while writing version independent UserSettings in windows registry.

## 2024/08/06:TomislavMatas: v1.127.1
* Implement management of version independent UserSettings in windows registry. 

## 2024/08/05:TomislavMatas: v1.127.1
* Add new UserSetting "ASESPunchDeviation".

## 2024/07/31:TomislavMatas: v1.127.0
* Add "--search-engine-choice-country" to "EZSeleniumLib.Browser.AdditionalOptions.Chrome"
  to avoid the nagging popup that appears after upgrading to Chrome version 127.
* Update "chromedriver.exe" to version "127.0.6533.88".
* Update "msedgedriver.exe" to version "127.0.2651.74".
* Use EZSeleniumLib version "4.22.4".

## 2024/07/06:TomislavMatas: v1.127.0
* Update "chromedriver.exe" to version "127.0.6533.26".
* Update "msedgedriver.exe" to version "127.0.2651.31".

## 2024/07/08:TomislavMatas: v1.126.4
* Add checkbox "Do Logout" to main window.
* Implement handling of new command line arguments "/DoLogout".
* Removed input.Clear() call, because it was interfering with other 
  implementations when punching in time pairs.

## 2024/06/24:TomislavMatas: v1.126.3
* Add input.Clear() call to improve editing of values already 
  present in time pair entry panel.

## 2024/06/24:TomislavMatas: v1.126.2
* Update "EZSeleniumLib" to version "4.22.2" to 
  mitigate "stale element reference" errors.
* Improve fault tolerance on time slice entry.
* Force "repaint" when app has been startet with "/run" argument in order
  to show the correct driver versions.
* Fix a bug in edge driver version evaluation.

## 2024/06/24:TomislavMatas: v1.126.1
* Update "Selenium" libs to version "4.22.0".
* Update "chromedriver.exe" to version "126.0.6478.115".
* Update "msedgedriver.exe" to version "126.0.2592.68".

## 2024/05/31:TomislavMatas: v1.126.0
* Simplify log4net implementations.

## 2024/05/27:TomislavMatas: v1.126.0
* Add handling of commandline argument "/close".
* Edit "EZAsesAutoType-UserManual.md": 
  Added section "Program startup arguments".

## 2024/05/26:TomislavMatas: v1.126.0
* Increase default timeout values.

## 2024/05/15:TomislavMatas: v1.125.0
* Update "chromedriver.exe" to version "125.0.6422.26".
* Update "MicrosoftWebDriver.exe" to version "125.0.2535.13".
* Update "log4net" libs to version "2.0.17".
* Update "Selenium" libs to version "4.20.0".
* Add handling of commandline argument "/run".

## 2024/05/02:TomislavMatas: v1.124.0
* Update "chromedriver.exe" to version "124.0.6367.91".
* Update "MicrosoftWebDriver.exe" to version "124.0.2477.0".

## 2024/04/12:TomislavMatas: v1.123.4.0
* Implement "second time pair" entry on main dialog.

## 2024/04/10:TomislavMatas: v1.123.3
* Implement singleton pattern for "WorkerConfig" and "Worker"
  within "AppHandler". Now when hitting "Cancel", the already 
  loaded browser instance stays "active". Otherwise,
  when processing has not been canceled by user, the
  browser instance will be closed immediatly after "logout".

## 2024/04/04:TomislavMatas: v1.123.1
* BugFix in "FormMain": Set WorkerSupportsCancellation = true.

## 2024/04/04:TomislavMatas: v1.0.124
* Update "chromedriver.exe"       to version "124.0.6367.29".
* Update "MicrosoftWebDriver.exe" to version "124.0.2478.19".

## 2024/04/04:TomislavMatas: v1.0.123
* Update "MicrosoftWebDriver.exe" to version "123.0.2420.65".
* Edit "App.config" defaults:
* - Use "log4net.File=${UserProfile}\.EZAsesAutoType\EZAsesAutoType.log".
* - Use "ASES.WaitBefore.Logout=2".
* BugFix in "FormMain.SaveUserSettings".
* Change product name from "EZAsesAutoType" to "EZ ASES AutoType".

## 2024/04/12:TomislavMatas: v1.123.4
* Implement "second time pair" entry on main dialog.

## 2024/04/10:TomislavMatas: v1.123.3
* Implement singleton pattern for "WorkerConfig" and "Worker"
  within "AppHandler". Now when hitting "Cancel", the already 
  loaded browser instance stays "active". Otherwise,
  when processing has not been canceled by user, the
  browser instance will be closed immediatly after "logout".

## 2024/04/04:TomislavMatas: v1.123.1
* BugFix in "FormMain": Set WorkerSupportsCancellation = true.

## 2024/04/04:TomislavMatas: v1.0.123
* Update "MicrosoftWebDriver.exe" to version "123.0.2420.65".
* Edit "App.config" defaults:
* - Use "log4net.File=${UserProfile}\.EZAsesAutoType\EZAsesAutoType.log".
* - Use "ASES.WaitBefore.Logout=2".
* BugFix in "FormMain.SaveUserSettings".
* Change product name from "EZAsesAutoType" to "EZ ASES AutoType".

## 2024/04/04:TomislavMatas: v1.0.0
* Initial Version.

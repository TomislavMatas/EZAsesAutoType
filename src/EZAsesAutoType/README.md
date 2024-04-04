# "EZAsesAutoType.csproj"
This project is used to build the assembly "EZAsesAutoType.exe".

The required Selenium framework wrappers are provided by "EZSeleniumLib.dll".

Please refer to "EZSeleniumLib" project for details.

# Revision History
## 2024/04/04:TomislavMatas: Version "1.0.124"
* Update "chromedriver.exe"       to version "124.0.6367.29".
* Update "MicrosoftWebDriver.exe" to version "124.0.2478.19".
* Edit "App.config" defaults:
* - Use "log4net.File=${UserProfile}\.EZAsesAutoType\EZAsesAutoType.log".
* - Use "ASES.WaitBefore.Logout=2".
* BugFix in "FormMain.SaveUserSettings".
* Change product name from "EZAsesAutoType" to "EZ ASES AutoType".

## 2024/04/04:TomislavMatas: Version "1.0.0"
* Initial Version.

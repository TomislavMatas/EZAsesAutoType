# "EZSeleniumLib.csproj"
This project is used to build the library "EZSeleniumLib.dll".

A new version of the "EZSeleniumLib.dll" potentially has to 
be deployed with every major browser version update.
Resulting in a patch needed apx. every month on "Stable release" 
channels of Chrome, Edge and Firefox.

The WebDriver major version must be the same as the browser major verion.

## Using Selenium WebDriver for Chrome
WebDriver download URL:
-->< https://googlechromelabs.github.io/chrome-for-testing/ >
-->< https://storage.googleapis.com/chrome-for-testing-public/ >

Google offers a bunch of API endpoints to enable automated build 
scripts based on Chrome for Testing release data, see:
-->< https://github.com/GoogleChromeLabs/chrome-for-testing#json-api-endpoints >

For example, you can use curl with API-URL
-->< https://googlechromelabs.github.io/chrome-for-testing/LATEST_RELEASE_STABLE > 
to answer the question: "What's the latest available Stable channel version?". 

The WebDriver executable has to be deployed with "EZSeleniumLib.dll".

## Using Selenium WebDriver for Edge 
WebDriver download URL:
-->< https://developer.microsoft.com/microsoft-edge/tools/webdriver >

For release schedule refer to
-->< https://docs.microsoft.com/de-de/deployedge/microsoft-edge-release-schedule >

For details please visit
-->< https://docs.microsoft.com/en-us/microsoft-edge/webdriver-chromium/?tabs=c-sharp >

Starting with Microsoft Edge version 94, Customers can opt into the 
"Extended Stable release cycle" option at any point using Group Policy
or through Intune. Resulting in an apx. 8 weekly release on even major version.
For more technical information, please see our updated Docs page: 
-->< https://docs.microsoft.com/deployedge/microsoft-edge-channels > .
As long as the windows and defender updates are applied on regular basis,
there is only a small risk by sticking with older Microsoft Edge Browser versions.
Assuming that the auto update feature of Microsoft Edge Browser has been disabled,
a specific Microsoft Edge Driver Version could be used "for ever", at least in theory.

# Revision History
## 2025/08/07:TomislavMatas: v4.34.0:
* Update "Selenium.Support" to version "4.34.0".
* Update "Selenium.WebDriver" to version "4.34.0".
* Update "chromedriver.exe" to version "139.0.7258.66".
* Update "msedgedriver.exe" to version "139.0.3405.83".
* Update "System.Management" to version="9.0.8".

## 2025/06/03:TomislavMatas: v4.33.0
* Update "Selenium.Support" to version "4.33.0".
* Update "Selenium.WebDriver" to version "4.33.0".
* Update "chromedriver.exe" to version "137.0.7151.68".
* Update "msedgedriver.exe" to version "137.0.3296.62".
* Update "log4net" to version "3.1.0".
* Update "System.Management" to version="9.0.5".

## 2025/04/10:TomislavMatas: v4.31.1
* Google finally decided to compile executable "chromedriver.exe"
  with version info resource, so read version from executable.

## 2025/04/10:TomislavMatas: v4.31.0
* Update "Selenium.Support" to version "4.31.0".
* Update "Selenium.WebDriver" to version "4.31.0".
* Update "System.Management" to version "9.0.4".
* Update "System.Text.Json" to version= 9.0.4".
* Update "log4net" to version "3.0.4".

## 2025/04/10:TomislavMatas: v4.28.0
* Update "chromedriver.exe" to version "135.0.7049.84".
* Update "msedgedriver.exe" to version "135.0.3179.54".
* Update "geckodriver.exe"  to version "0.36.0".

## 2025/02/05:TomislavMatas: v4.28.0
* Update "Selenium.Support" to version "4.28.0".
* Update "Selenium.WebDriver" to version="4.28.0".
* Update "System.Management" to version="9.0.1".
* Update "System.Text.Json" to version="9.0.1".
* Update "chromedriver.exe" to version "133.0.6943.53".
* Update "msedgedriver.exe" to version "133.0.3065.39".

## 2024/11/28:TomislavMatas: v4.26.2
* Update "chromedriver.exe" to version "131.0.6778.85".
* Update "msedgedriver.exe" to version "131.0.2903.70".

## 2024/11/20:TomislavMatas: v4.26.1
* Update "Selenium.Support" to version "4.26.1".
* Update "Selenium.WebDriver" to version="4.26.1".
* Update "System.Management" to version="9.0.0".
* Adding "PlatformTarget=AnyCPU" explicitly solved the MSIX
  packaging error "MSB3270".
* WIP: Prototyping "EZAsesAutoTypeMSIX.wapproj".

## 2024/11/19:TomislavMatas: v4.25.1
* Update "chromedriver.exe" to version "131.0.6778.69".
* Update "msedgedriver.exe" to version "131.0.2903.51".
* Update "log4net" to version "3.0.3".

## 2024/09/26:TomislavMatas: v4.25.0
* Update "chromedriver.exe" to version "129.0.6668.71".
* Update "msedgedriver.exe" to version "129.0.2792.52".
* Update "geckodriver.exe"  to version "0.35.0".
* Update "log4net" to version "3.0.0".

## 2024/08/05:TomislavMatas: v4.23.0
* Update "Selenium.Support" to version "4.23.0".
* Update "Selenium.WebDriver" to version "4.23.0".

## 2024/07/31:TomislavMatas: v4.22.2
* Implement handling of browser specific "App.config" settings
  "EZSeleniumLib.Browser.AdditionalOptions.Chrome",
  "EZSeleniumLib.Browser.AdditionalOptions.Edge" and
  "EZSeleniumLib.Browser.AdditionalOptions.Firefox".
* Update "chromedriver.exe" to version "127.0.6533.88".
* Update "msedgedriver.exe" to version "127.0.2651.74".

## 2024/06/29:TomislavMatas: v4.22.2
* Add new wrappers for "IWebElement.SendKeys()" and "IWebElement.Click()"
  to mitigate "stale element reference" errors.

## 2024/06/29:TomislavMatas: v4.22.1
* BugFix: Re~Add packaging of web driver binaries during build.

## 2024/06/24:TomislavMatas: v4.22.0
* Update "Selenium.Support" to version "4.22.0".
* Update "Selenium.WebDriver" to version "4.22.0".
* Update "chromedriver.exe" to version "126.0.6478.115".
* Update "msedgedriver.exe" to version "126.0.2592.68".

## 2024/05/31:TomislavMatas: v4.21.1
* Simplify log4net implementations.
* Update "chromedriver.exe" to version "126.0.6478.26".
* Update "msedgedriver.exe" to version "126.0.2592.24".

## 2024/05/29:TomislavMatas: v4.21.0
* Update "Selenium.Support" to version "4.21.0".
* Update "Selenium.WebDriver" to version "4.21.0".
* Update "chromedriver.exe" to version "125.0.6422.113".
* Update "msedgedriver.exe" to version "125.0.2535.67".
* Refactoring: Implement specific "SendKeys()" in descendants of "BrowserBase". 

## 2024/05/18:TomislavMatas: v4.20.1
* Update "chromedriver.exe" to version "125.0.6422.61".
* Update "msedgedriver.exe" to version "125.0.2535.51".

## 2024/05/05:TomislavMatas: v4.20.0
* Set product version to "4.20.0" reflecting the referenced selenium package version.
* Upgrade to .NET version 8.

## 2024/05/02:TomislavMatas: v1.124.0
* Use "chromedriver.exe" version "124.0.6367.91".
* Use "MicrosoftWebDriver.exe" version "124.0.2477.0".

## 2024/04/04:TomislavMatas: v1.0.123
* Update "MicrosoftWebDriver.exe" to version "123.0.2420.65".
* BugFix: Refactor "ms:edgeOptions" to work with selenium v4.
* Tidy~Up in "InitializeExtended": Change "Log.Info" to "Log.Debug".

## 2024/04/04:TomislavMatas: v1.0.0
* Using "chromedriver.exe" version "123.0.6312.58".
* Using "MicrosoftWebDriver.exe" version "122.0.2365.106".
* Using "geckodriver.exe" version "0.34.0".
* Initial version.

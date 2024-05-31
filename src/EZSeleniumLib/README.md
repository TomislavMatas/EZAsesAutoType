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
## 2024/05/31:TomislavMatas: Version "4.21.1"
* Simplify log4net implementations.
* Update "chromedriver.exe" to version "126.0.6478.26".
* Update "msedgedriver.exe" to version "126.0.2592.24".

## 2024/05/29:TomislavMatas: Version "4.21.0"
* Update "Selenium.Support" to version "4.21.0".
* Update "Selenium.WebDriver" to version "4.21.0".
* Update "chromedriver.exe" to version "125.0.6422.113".
* Update "msedgedriver.exe" to version "125.0.2535.67".
* Refactoring: Implement specific "SendKeys()" in descendants of "BrowserBase". 

## 2024/05/18:TomislavMatas: Version "4.20.1"
* Update "chromedriver.exe" to version "125.0.6422.61".
* Update "msedgedriver.exe" to version "125.0.2535.51".

## 2024/05/05:TomislavMatas: Version "4.20.0"
* Set product version to "4.20.0" reflecting the referenced selenium package version.
* Upgrade to .NET version 8.

## 2024/05/02:TomislavMatas: Version "1.124.0"
* Use "chromedriver.exe" version "124.0.6367.91".
* Use "MicrosoftWebDriver.exe" version "124.0.2477.0".

## 2024/04/04:TomislavMatas: Version "1.0.123"
* Update "MicrosoftWebDriver.exe" to version "123.0.2420.65".
* BugFix: Refactor "ms:edgeOptions" to work with selenium v4.
* Tidy~Up in "InitializeExtended": Change "Log.Info" to "Log.Debug".

## 2024/04/04:TomislavMatas: Version "1.0.0"
* Using "chromedriver.exe" version "123.0.6312.58".
* Using "MicrosoftWebDriver.exe" version "122.0.2365.106".
* Using "geckodriver.exe" version "0.34.0".
* Initial version.

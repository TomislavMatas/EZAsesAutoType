# "EZSeleniumLib.csproj"
This project is used to build the library "EZSeleniumLib.dll".

A new version of the "EZSeleniumLib.dll" potentially has to 
be deployed with every major browser version update.
Resulting in a patch needed apx. every month on "Stable release" 
channels of Chrome and Edge.

The WebDriver version must be the same (or higher) 
than the browser verion installed.

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
## 2024/03/24:TomislavMatas: Version "24.123.0.0"
* Added initial prototyping to -->< "https://github.com/TomislavMatas/AsesAutoType" >.
* Use "chromedriver.exe" version "123.0.6312.58".
* Use "MicrosoftWebDriver.exe" version "123.0.2420.53".
* Initial Version.

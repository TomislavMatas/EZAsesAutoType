# "EZSeleniumLib.csproj"
This project is used to build the library "EZSeleniumLib.dll".

## Using Selenium Edge / EdgeChromium WebDriver
WebDriver Download URL:
-->< https://developer.microsoft.com/microsoft-edge/tools/webdriver >

The Selenium WebDriver executable is has to be deployed with 
"EZSeleniumLib.dll", see "EZSeleniumLib.csproj".

For details please visit
-->< https://docs.microsoft.com/en-us/microsoft-edge/webdriver-chromium/?tabs=c-sharp >

The driver version must match the actual verion of Microsoft Edge Browser used.
For release schedule refer to
-->< https://docs.microsoft.com/de-de/deployedge/microsoft-edge-release-schedule >
Saying that, a new version of the "EZSeleniumLib.dll" potentially has to 
be deployed with everey major update "123.x", "124.x", "97.x" and so on.
Resulting in a patch need apx every month on "Stable release" channel by default.
Starting with Microsoft Edge version 94, Customers can opt into the 
"Extended Stable release cycle" option at any point using Group Policy
or through Intune. Resulting in an apx 8 weekly release on even major version.
For more technical information, please see our updated Docs page: 
-->< https://docs.microsoft.com/deployedge/microsoft-edge-channels > .
As long as the windows and defender updates are applied on regular basis,
there is only a small risk by sticking with older Microsoft Edge Browser versions.
Assuming that the auto update feature of Microsoft Edge Browser has been disabled,
the specific Microsoft Edge Driver Version can be used "for ever", in theory...
As with version "24.123.0.0" of this app, an update apx every eight (8) weeks is targeted,
resulting in planned major versions 123, 125, 127 and so on.

# Revision History
## 2024/03/22:TomislavMatas: Version "24.123.0.0"
* Added initial prototyping to -->< "https://github.com/TomislavMatas/AsesAutoType" >.
* Use "chromedriver.exe" version "123.0.6312.58".
* Use "MicrosoftWebDriver.exe" version "122.0.2365.106".
* Initial Version.

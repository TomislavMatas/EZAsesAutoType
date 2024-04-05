# "EZAsesAutoTypeSetup.vdproj"
The project is used to build the Setup MSI "EZAsesAutoTypeSetup.msi"
Requires Extension "Microsoft Visual Studio Installer Projects".
Always use platform "Any CPU" when building the setup project. 
For some reason, the generated Setup MSI can NOT be executed
when building with platform "x64". 

# Revision History
## 2024/04/05:TomislavMatas: Version "1.124.0"
* Explicitly add "EZAsesAutoType.exe" in order to make it available 
  for the "User's Programs Menu" shortcut.
* Add shortcut "EZ ASES AutoType" to the "User's Programs Menu".

## 2024/04/04:TomislavMatas: Version "1.0.123"
* Change product name from "EZAsesAutoType" to "EZ ASES AutoType".
* Add version info suffix to "OutputFilename".

## 2024/04/04:TomislavMatas: Version "1.0.0"
* Using "chromedriver.exe" version "123.0.6312.58".
* Using "MicrosoftWebDriver.exe" version "122.0.2365.106".
* Using "geckodriver.exe" version "0.34.0".
* Initial version.

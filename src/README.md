# Subdirectory "EZAsesAutoType" 
Contains sources required to build the assembly "EZAsesAutoType.exe".

# Subdirectory "EZSeleniumLib"
Contains sources required to build the assembly "EZSeleniumLib.dll".

# Subdirectory "EZAsesAutoTypeSetup" (legcy)
Contains sources required to build the setup package "EZAsesAutoTypeSetup.msi".

# Subdirectory "EZAsesAutoTypeMSIX"
Contains sources required to build the MSIX package.

# Revision History

## 2026/08/18:TomislavMatas: [v4.151.0]
* Removed legacy "EZAsesAutoTypeSetup.vdproj" from solution 
  "EZAsesAutoType-VS2026.sln". Use modern "EZAsesAutoTypeMSIX.wapproj" 
  which also supports automated builds for future builds and deployments.

## 2024/04/07:TomislavMatas: Version "1.124.0"
* Add subdirectory "EZAsesAutoTypeMSIX". MSIX deployment and publishing 
shall replace the legacy "EZAsesAutoTypeSetup.msi" deployment in future versions.

## 2024/04/04:TomislavMatas: Version "1.0.0"
* Initial version.

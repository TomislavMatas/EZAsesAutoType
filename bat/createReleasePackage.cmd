:: -- 
:: -- File: "createReleasePackage.cmd"
:: -- 
:: -- Arguments: %1 = VersionLabel (optional). If no value has been 
:: --            supplied, user will be prompted to enter a value.
:: -- 
:: -- Revision History
:: -- 2024/09/26:TomislavMatas: webdriver/129 Version "1.129.0"
:: -- * set "VersionLabelDefault=1.129.0".
:: -- * Implement usage of variable "VersionLabelDefault".
:: -- 2024/07/04:TomislavMatas: webdriver/126 Version "1.126.2"
:: -- * Initial version.
:: --

@echo off
setlocal
set "VersionLabelDefault=1.129.0"
  
:: cast batch file input arguments to variables with meaningfull name.
set "VersionLabel=%1"
if "%VersionLabel%" == "" (
	echo VersionLabel: ^(hit enter for default %VersionLabelDefault%^) :
	set /P "VersionLabel="
)

if "%VersionLabel%" == "" (
	set "VersionLabel=%VersionLabelDefault%"
)

set "PROJECT_ROOT=%~dp0.."
set "SIGNAGE_BAT=%PROJECT_ROOT%\bat\signReleaseBinaries.cmd"
set "PORTABLE_BIN_PATH=%PROJECT_ROOT%\src\EZAsesAutoType\bin\Release\net8.0-windows"
set "SETUP_MSI_BIN_FILE=%PROJECT_ROOT%\src\EZAsesAutoTypeSetup\bin\Release\EZAsesAutoType-setup.msi"
set "BIN_RELEASE_PATH=%PROJECT_ROOT%\bin\release"

if not exist "%SIGNAGE_BAT%" (
  echo "%SIGNAGE_BAT%" DOES NOT EXIST
  goto failure
)

if not exist "%PORTABLE_BIN_PATH%" (
  echo "%PORTABLE_BIN_PATH%" DOES NOT EXIST
  goto failure
)

if not exist "%SETUP_MSI_BIN_FILE%" (
  echo "%SETUP_MSI_BIN_FILE%" DOES NOT EXIST
  goto failure
)

mkdir "%BIN_RELEASE_PATH%" 1>nul 2>nul
if not exist "%BIN_RELEASE_PATH%" (
  echo "%BIN_RELEASE_PATH%" DOES NOT EXIST
  goto failure
)

:: -- make sure that the binaries have been signed
@call "%SIGNAGE_BAT%"
@echo off

:: -- The portable package is basically a zip file containing the build output.
:: -- Create the zip file in a dedicated release directory.
echo create portable package zip file ...
set "ZIP_FILE=%BIN_RELEASE_PATH%\EZAsesAutoType-portable-%VersionLabel%.zip"
set "ZIP_CMD=powershell.exe -command"
set "ZIP_ARG=Compress-Archive -CompressionLevel Optimal "
set "ZIP_ARG=%ZIP_ARG% -DestinationPath \"%ZIP_FILE%\" "
set "ZIP_ARG=%ZIP_ARG% -Path \"%PORTABLE_BIN_PATH%\" "
set "ZIP_ARG=%ZIP_ARG% -Force "
@echo on
%ZIP_CMD% %ZIP_ARG%
@if ERRORLEVEL 1 @(
  @echo creation of zip file "%ZIP_FILE%" FAILED
  @goto failure
)
@echo OK
@echo off

:: -- The Setup msi file should have been created during build 
:: -- _without_ any version label in it's file name.
:: -- Place a copy _with_ version label into dedicated release directory.
echo create setup msi file ...
set "MSI_FILE=%BIN_RELEASE_PATH%\EZAsesAutoType-setup-%VersionLabel%.msi"
@echo on
copy "%SETUP_MSI_BIN_FILE%" "%MSI_FILE%"
@if ERRORLEVEL 1 @(
  @echo creation of setup msi file "%MSI_FILE%" FAILED  
  @goto failure
)
@echo OK
@echo off


@goto done

:failure
 @pause

:done

:: -- 
:: -- File: "signReleaseBinaries.cmd"
:: -- 
:: -- For details see:
:: -- -->< https://learn.microsoft.com/de-de/windows/msix/package/create-certificate-package-signing >
:: -- 
:: -- Revision History
:: -- 2024/11/22:TomislavMatas: webdriver/131 Version "1.131.1"
:: -- * Change path from "net8.0-windows" to "net8.0-windows10.0.26100.0".
:: -- 2024/07/04:TomislavMatas: webdriver/126 Version "1.126.2"
:: -- * Renamed this file from "signReleaseMsi.cmd" 
:: --   to "signReleaseBinaries.cmd" for the sake of clarity.
:: -- 2024/06/24:TomislavMatas: 
:: -- * Add signage of exe file.
:: -- 2024/04/10:TomislavMatas: 
:: -- * Initial version
:: --

@echo off
setlocal

set "PROJECT_ROOT=%~dp0.."
set "SIGNTOOL_EXE=C:\Program Files (x86)\Windows Kits\10\App Certification Kit\signtool.exe"
set "EXE_FILE=%PROJECT_ROOT%\src\EZAsesAutoType\bin\Release\net8.0-windows10.0.26100.0\EZAsesAutoType.exe"
set "PFX_FILE=%PROJECT_ROOT%\cert\MatasConsultingSelfSigned.pfx"
set "MSI_PATH=%PROJECT_ROOT%\src\EZAsesAutoTypeSetup\bin\Release"
set /P "PFX_FILE_PASSWORD=pfx file password : "

if not exist "%SIGNTOOL_EXE%" (
  echo "%SIGNTOOL_EXE%" DOES NOT EXIST
  goto failure
)

if not exist "%EXE_FILE%" (
  echo "%EXE_FILE%" DOES NOT EXIST
  goto failure
)

if not exist "%MSI_PATH%" (
  echo "%MSI_PATH%" DOES NOT EXIST
  goto failure
)

if not exist "%PFX_FILE%" (
  echo "%PFX_FILE%" DOES NOT EXIST
  goto failure
)

if "%PFX_FILE_PASSWORD%" == "" (
  echo NO PASSWORD ENTERED
  goto failure
)

@echo on
"%SIGNTOOL_EXE%" sign /f "%PFX_FILE%" /p %PFX_FILE_PASSWORD% /fd SHA256 "%EXE_FILE%"
@if errorlevel 1 @(
  @echo code signage failed
  @goto failure
)
@echo OK

"%SIGNTOOL_EXE%" sign /f "%PFX_FILE%" /p %PFX_FILE_PASSWORD% /fd SHA256 "%MSI_PATH%\*.msi"
@if errorlevel 1 @(
  @echo code signage failed
  @goto failure
)
@echo OK
@goto done

:failure
 @pause

:done

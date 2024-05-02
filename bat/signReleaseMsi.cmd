:: -- 
:: -- File: "signReleaseMsi.cmd"
:: -- 
:: -- For details see:
:: -- -->< https://learn.microsoft.com/de-de/windows/msix/package/create-certificate-package-signing >
:: -- 
:: -- Revision History
:: -- 2024/04/10:TomislavMatas: 
:: -- * Initial version
:: --

@echo off
setlocal

set "PROJECT_ROOT=%~dp0.."
set "SIGNTOOL_EXE=C:\Program Files (x86)\Windows Kits\10\App Certification Kit\signtool.exe"
set "MSI_FILE_PATH=%PROJECT_ROOT%\src\EZAsesAutoTypeSetup\bin\Release"
set "PFX_FILE_PATH=%PROJECT_ROOT%\cert\MatasConsultingSelfSigned.pfx"
set /P "PFX_FILE_PASSWORD=pfx file password : "

if not exist "%SIGNTOOL_EXE%" (
  echo "%SIGNTOOL_EXE%" DOES NOT EXIST
  goto failure
)

if not exist "%MSI_FILE_PATH%" (
  echo "%MSI_FILE_PATH%" DOES NOT EXIST
  goto failure
)

if not exist "%PFX_FILE_PATH%" (
  echo "%PFX_FILE_PATH%" DOES NOT EXIST
  goto failure
)

if "%PFX_FILE_PASSWORD%" == "" (
  echo NO PASSWORD ENTERED
  goto failure
)

@echo on
"%SIGNTOOL_EXE%" sign /f "%PFX_FILE_PATH%" /p %PFX_FILE_PASSWORD% /fd SHA256 "%MSI_FILE_PATH%\*.msi"
@if errorlevel 1 @(
  @echo code signage failed
  @goto failure
)
@echo OK
@goto done

:failure
 @pause

:done

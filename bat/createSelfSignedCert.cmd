:: -- 
:: -- File: "createSelfSignedCert.cmd"
:: -- 
:: -- For details see:
:: -- -->< https://learn.microsoft.com/de-de/windows/msix/package/create-certificate-package-signing >
:: -- 
:: -- Revision History
:: -- 2024/04/12:TomislavMatas: Version "1.123.4"
:: -- * Renamed this file from "createNewSelfSignedCertificate.cmd" to "createSelfSignedCert.cmd".
:: -- 2024/04/10:TomislavMatas: 
:: -- * Initial version
:: --

@echo off
setlocal EnableExtensions EnableDelayedExpansion

set "PSLAUNCH=powershell -command"
set "PSSCRIPT=$ProgressPreference = \"SilentlyContinue\";"
set "PSSCRIPT=%PSSCRIPT% New-SelfSignedCertificate"
set "PSSCRIPT=%PSSCRIPT% -Type CodeSigningCert"
set "PSSCRIPT=%PSSCRIPT% -Subject \"CN=D5171930-ADB5-4375-9BD0-365C8D0E495E, O=matas consulting\""
set "PSSCRIPT=%PSSCRIPT% -KeyUsage DigitalSignature"
set "PSSCRIPT=%PSSCRIPT% -FriendlyName \"matas consulting selfsigned\""
set "PSSCRIPT=%PSSCRIPT% -CertStoreLocation \"Cert:\\CurrentUser\\My\""
set "PSSCRIPT=%PSSCRIPT% -TextExtension @(\"2.5.29.37={text}1.3.6.1.5.5.7.3.3\", \"2.5.29.19={text}\")"

@echo on
%PSLAUNCH% %PSSCRIPT%
@if errorlevel 1 @(
	@echo ERROR "%PSSCRIPT%" failed
	@goto done
)
@echo A new self signed certificate has been created in your personal certificate store.
@echo Export it as "MatasConsultingSelfSigned.pfx" with a secret password to the
@echo directory "%~dp0..\cert" to make it available for scripted code signage.

:done

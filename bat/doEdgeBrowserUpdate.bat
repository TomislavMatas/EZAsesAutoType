:: -- 
:: -- File: "doEdgeBrowserUpdate.bat"
:: -- 
:: -- Usage: 
:: -- ```
:: -- doEdgeBrowserUpdate[.bat]
:: -- ```
:: -- 
:: -- Example: doEdgeBrowserUpdate
:: -- ```
:: -- CMD> doEdgeBrowserUpdate
:: -- ```
:: --
:: -- IMPORTANT: Run as administrator.
:: -- 
:: -- Revision History
:: -- 2024/03/25:TomislavMatas: Version "1.0.0.0"
:: -- * Inital version.
:: --

@echo off
setlocal EnableExtensions EnableDelayedExpansion

set "PSCMD=powershell -command"
set "SCHEDTASKS=MicrosoftEdgeUpdateTaskMachineCore MicrosoftEdgeUpdateTaskMachineUA"
set "SERVICELIST=edgeupdate edgeupdatem"

echo kill WhatsAppWeb Proxy Tasks ...
taskkill /F /IM AsesAutoTypeApp.exe        /T 1>nul 2>nul
taskkill /F /IM MicrosoftWebDriver.exe     /T 1>nul 2>nul
taskkill /F /IM msedge.exe                 /T 1>nul 2>nul
echo kill WhatsAppWeb Proxy Tasks OK

echo enable scheduled tasks ...
for %%t in (%SCHEDTASKS%) do (
	set "tn=%%t" 
	set "script=$ProgressPreference = \"SilentlyContinue\"; schtasks /Change /Enable /Tn \"!tn!\" "	
	!pscmd! "!script!"
	if ERRORLEVEL 1 (
		echo ERROR enable scheduled task '!tn!' failed
		exit /b 1
	)	
)
echo enable scheduled tasks OK

echo enable services ...
@rem Die Dienste muessen auf "START= AUTO" gesetzt werden,
@rem aber nicht explizit gestartet werden. Die Dienste werden 
@rem dann vom Edge Browser selbst bei Bedarf gestartet.
for %%s in (%SERVICELIST%) do (
	set "sn=%%s" 
	SC CONFIG "!sn!" START= AUTO	
	if ERRORLEVEL 1 (
		echo ERROR enable service '!sn!' failed
		exit /b 2
	)	
)
echo enable services OK

@echo Das Starten des Browser sollte den Update starten.
@echo Im Zweifel, manuell "edge://help" in die Adressleiste eingeben
start "edge" microsoft-edge:"https://www.matas.de"
@echo Nach dem Update und Neustart des Edge Browser,
@echo [Enter] um fortzusetzen ...
@pause

echo disable services ...
@rem Die Dienste mit "START= DISABLED" wieder deaktivieren.
for %%s in (%SERVICELIST%) do (
	set "sn=%%s" 
	SC CONFIG "!sn!" START= DISABLED
	if ERRORLEVEL 1 (
		echo ERROR enable service '!sn!' failed
		exit /b 3
	)	
)
echo disable services OK

echo disable scheduled tasks ...
for %%t in (%SCHEDTASKS%) do (
	set "tn=%%t" 
	set "script=$ProgressPreference = \"SilentlyContinue\"; schtasks /Change /Disable /Tn \"!tn!\" "	
	!pscmd! "!script!"
	if ERRORLEVEL 1 (
		echo ERROR disable scheduled task '!tn!' failed
		exit /b 4
	)	
)
echo disable scheduled tasks OK

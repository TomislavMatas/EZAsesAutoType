:: -- 
:: -- File: "doChromeBrowserUpdate.bat"
:: -- 
:: -- Usage: 
:: -- ```
:: -- doChromeBrowserUpdate[.bat]
:: -- ```
:: -- 
:: -- Example:
:: -- ```
:: -- CMD> doChromeBrowserUpdate
:: -- ```
:: --
:: -- IMPORTANT: Run as administrator.
:: -- 
:: -- Revision History
:: -- 2024/03/22:TomislavMatas: Version "24.123.0.0"
:: -- * Inital version.
:: --

@echo off
setlocal EnableExtensions

set "TaskPartialName=GoogleUpdateTask"
set "ListTasksCmd=%SystemRoot%\System32\schtasks.exe /Query /FO CSV /NH 2^>NUL"
set "FilterNameCmd=%SystemRoot%\System32\find.exe /I "%TaskPartialName%""
set "ServiceNames=gupdate gupdatem"

echo Kill WhatsAppWeb Proxy Processes ...
taskkill /F /IM AsesAutoTypeApp.exe /T 1>nul 2>nul
taskkill /F /IM chromedriver.exe    /T 1>nul 2>nul
taskkill /F /IM chrome.exe          /T 1>nul 2>nul
echo Kill WhatsAppWeb Proxy Processes OK

echo Enable Chrome Update Scheduled Tasks ...
For /F Delims^=^" %%G In ('%ListTasksCmd% ^| %FilterNameCmd%' ) Do ( 
	echo Enable Task "%%G" ...
	schtasks /Change /Enable /TN "%%G" 
	if ERRORLEVEL 1 (
		echo Enable Task '%%G' FAILED
		exit /b 1
	)  
	echo Enable Task "%%G" OK
) 
echo Enable Chrome Update Scheduled Tasks OK

echo Enable Chrome Update Services ...
@rem Die Dienste muessen auf "START= AUTO" gesetzt werden,
@rem aber nicht explizit gestartet werden. Die Dienste werden 
@rem dann vom Chrome Browser selbst bei Bedarf gestartet.
for %%s in (%ServiceNames%) do (
	echo Enable Service "%%s" ...
	SC CONFIG "%%s" START= AUTO	
	if ERRORLEVEL 1 (
		echo Enable Service '%%s' FAILED
		exit /b 2
	)	
	echo Enable Service "%%s" OK
)
echo Enable Chrome Update Services OK

@echo Nachdem der Browser gestartet wurde, sollte den Update automatisch starten.
@echo Im Zweifel, manuell "chrome://help" in die Adressleiste eingeben.
start "chrome" "C:\Program Files\Google\Chrome\Application\chrome.exe" "https://www.matas.de"
@echo Nach dem Browser Update und Neustart des Browsers:
@echo [Enter] um fortzusetzen ...
@pause

echo Disable Chrome Update Services ...
@rem Die Dienste mit "START= DISABLED" wieder deaktivieren.
for %%s in (%ServiceNames%) do (
	echo Disable Service "%%s" ...
	SC CONFIG "%%s" START= DISABLED
	if ERRORLEVEL 1 (
		echo Disable Service '%%s' FAILED
		exit /b 3
	)	
	echo Disable Service "%%s" OK
)
echo Disable Chrome Update Services OK

echo Disable Chrome Update Scheduled Tasks ...
For /F Delims^=^" %%G In ('%ListTasksCmd% ^| %FilterNameCmd%' ) Do ( 
	echo Disable Task "%%G" ...
	schtasks /Change /Disable /TN "%%G" 
	if ERRORLEVEL 1 (
		echo Disable Task '%%G' FAILED
		exit /b 4
	)  
	echo Disable Task "%%G" OK
) 
echo Disable Chrome Update Scheduled Tasks OK

:success
exit /b 0

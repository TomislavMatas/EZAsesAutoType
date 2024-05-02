:: -- 
:: -- File: "updateWebdriverEdge.bat"
:: -- 
:: -- Usage: 
:: -- ```
:: -- updateWebdriverEdge[.bat] [version]
:: -- ```
:: -- 
:: -- Example:
:: -- ```
:: -- CMD> updateWebdriverEdge 125.0.2535.13
:: -- ```
:: --
:: -- Revision History
:: -- 2024/05/03:TomislavMatas: Version "1.125.0"
:: -- * Set "WEBDRIVER_VERSION_DEFAULT=125.0.2535.13"
:: -- 2024/05/02:TomislavMatas: Version "1.124.0"
:: -- * Set "WEBDRIVER_VERSION_DEFAULT=124.0.2477.0"
:: -- 2024/04/04:TomislavMatas: Version "1.0.123"
:: -- * Set "WEBDRIVER_VERSION_DEFAULT=123.0.2420.65"
:: -- * Tidy~Up commentz.
:: -- 2024/03/25:TomislavMatas: Version "1.0.0.0"
:: -- * Initial version with default webdriver version "122.0.2365.106".
:: --

@echo off
setlocal EnableExtensions EnableDelayedExpansion

set "WEBDRIVER_VERSION_DEFAULT=125.0.2535.13"

echo see browser release schedule at:
echo https://learn.microsoft.com/de-de/deployedge/microsoft-edge-release-schedule/

echo see available versions of webdriver at:
echo https://developer.microsoft.com/de-de/microsoft-edge/tools/webdriver/

pause

set "PROJECT_ROOT=%~dp0.."
@rem List of project directories under "%PROJECT_ROOT%\src\", 
@rem where the downloaded binaries shall be copied to.
set "PROJECT_LIST=EZSeleniumLib"

set "WEBDRIVER_HOMEPAGE=https://msedgedriver.azureedge.net"
set "WEBDRIVER_VERSION=%1"
if "%WEBDRIVER_VERSION%" == "" (
	set /P "WEBDRIVER_VERSION=Version eingeben oder [Enter] fuer Default '%WEBDRIVER_VERSION_DEFAULT%' : "
)
if "%WEBDRIVER_VERSION%" == "" (
	set "WEBDRIVER_VERSION=%WEBDRIVER_VERSION_DEFAULT%"
)

set "DOWNLOAD_URL=%WEBDRIVER_HOMEPAGE%/%WEBDRIVER_VERSION%"
set "DOWNLOAD_ROOT=%PROJECT_ROOT%\bin\WebDriver\Edge"
set "DOWNLOAD_DIR=%DOWNLOAD_ROOT%\%WEBDRIVER_VERSION%"
@rem die Liste der Dateien, die heruntergeladen werden sollen.
::set "FILELIST=edgedriver_win64.zip edgedriver_win32.zip edgedriver_linux64.zip edgedriver_arm64.zip"
set "FILELIST=edgedriver_win64.zip"
@rem Die Umbenennung ist nur fuer die Windows Treiber erforderlich, 
@rem weil aktuell noch ein veraltetes Selenium-Framework verwendet wird.
set "FILENAME_ORIGINAL=msedgedriver.exe"
set "FILENAME_CUSTOM=MicrosoftWebDriver.exe"
set "CURL_EXE=curl"
set "CURL_OPT=-k -sS --fail"

mkdir "%DOWNLOAD_DIR%" 1>nul 2>nul
if not exist "%DOWNLOAD_DIR%\" (
    echo "%DOWNLOAD_DIR%" does not exist
    exit /b 1
)

for %%f in (%FILELIST%) do (
	echo download "%DOWNLOAD_URL%/%%f" ...
	"%CURL_EXE%" %CURL_OPT% "%DOWNLOAD_URL%/%%f" --output "%DOWNLOAD_DIR%\%%f"
	if ERRORLEVEL 1 (
		echo ERROR download "%DOWNLOAD_URL%/%%f" failed
		exit /b 2
	)	
	echo download "%DOWNLOAD_URL%/%%f" OK
)

for %%f in (%FILELIST%) do (
	set "fn=%%f" 
	set "sx=!fn:edgedriver_=!"
	set "sx=!sx:.zip=!"
	set "sx=!sx:win=x!"
	set "zipfile=!DOWNLOAD_DIR!\!fn!"
	set "unzipedpath=!DOWNLOAD_DIR!_!sx!"
	set "pscmd=powershell -command"
	set "script=$ProgressPreference = \"SilentlyContinue\"; Expand-Archive \"!zipfile!\" -DestinationPath \"!unzipedpath!\" -Force "
	echo unzip "!fn!" ...
	!pscmd! "!script!"
	if ERRORLEVEL 1 (
		echo ERROR unzip "!fn!" failed
		exit /b 3
	)	
	echo unzip "!fn!" OK
	@rem Zip-Datei verschieben, weil erfolgreich verarbeitet
	echo move "!fn!" ... 
	del "!unzipedpath!\!fn!" 1>nul 2>nul
	move "!zipfile!" "!unzipedpath!" 1>nul
	if ERRORLEVEL 1 (
		echo ERROR move "!zipfile!" failed
		exit /b 4
	)	
	echo move "!fn!" OK
	@rem Loesche das nicht wirklich benoetigte Verzeichnis "Driver_Notes" aus dem entpackten Verzeichnis.
	@rem Ausnahme: Das Verzeichnis "Driver_Notes" gibt es in allen Paketen ausser fuer linux.
	if "x!fn:linux=!" == "x!fn!" ( 
		echo remove directory "Driver_Notes" ... 
		rd /s /q "!unzipedpath!\Driver_Notes" 1>nul
		if ERRORLEVEL 1 (
			echo remove directory "Driver_Notes" failed
			exit /b 5
		)	
		echo remove directory "Driver_Notes" OK
	)
	@rem Die Windows-Treiber umbenennen
	if not "x!fn:win=!" == "x!fn!" ( 
		echo rename "%FILENAME_ORIGINAL%" to "%FILENAME_CUSTOM%" ... 
		del "!unzipedpath!\%FILENAME_CUSTOM%" 1>nul 2>nul
		ren "!unzipedpath!\%FILENAME_ORIGINAL%" "%FILENAME_CUSTOM%" 
		if ERRORLEVEL 1 (
			echo ERROR rename "%FILENAME_ORIGINAL%" to "%FILENAME_CUSTOM%" failed
			exit /b 6
		)	
		echo rename "%FILENAME_ORIGINAL%" to "%FILENAME_CUSTOM%" OK
	)		
	@rem "Verteile" den win64 in die Projekt-Verzeichnisse, in denen dieser benoetigt wird.
	if not "x!fn:win64=!" == "x!fn!" ( 
		for %%p in (!PROJECT_LIST!) do (
			set "prj=%%p" 
			set "prjpath=%PROJECT_ROOT%\src\!prj!"
			echo copy "%FILENAME_CUSTOM%" to "!prj!" ...
			del "!prjpath!\%FILENAME_CUSTOM%" 1>nul 2>nul
			copy "!unzipedpath!\%FILENAME_CUSTOM%" "!prjpath!\%FILENAME_CUSTOM%" 1>nul
			if ERRORLEVEL 1 (
				echo ERROR copy "%FILENAME_CUSTOM%" to "!prj!" failed
				exit /b 7
			)	
			echo copy "%FILENAME_CUSTOM%" to "!prj!" OK
		)
	)
)

rd /s /q "%DOWNLOAD_DIR%" 1>nul
if ERRORLEVEL 1 (
	echo ERROR remove "%DOWNLOAD_DIR%" failed
	exit /b 7
)

explorer "%DOWNLOAD_ROOT%"

exit /b 0

:: -- 
:: -- File: "updateWebdriverChrome.bat"
:: -- 
:: -- Usage: 
:: -- ```
:: -- updateWebdriverChrome[.bat] [version]
:: -- ```
:: -- 
:: -- Example:
:: -- ```
:: -- CMD> updateWebdriverChrome 126.0.6478.115
:: -- ```
:: --
:: -- Notes:
:: -- * Google offers a bunch of API endpoints to enable automated build 
:: --   scripts based on Chrome for Testing release data, see:
:: --   -->< https://github.com/GoogleChromeLabs/chrome-for-testing#json-api-endpoints >
:: --   For example, to answer the question
:: --   "What's the latest available Stable channel version?"
:: --   you can use curl with API-URL
:: --   -->< https://googlechromelabs.github.io/chrome-for-testing/LATEST_RELEASE_STABLE > 
:: --
:: -- Revision History
:: -- 2025/02/05:TomislavMatas: Version "1.133.0"
:: -- * Set "WEBDRIVER_VERSION_DEFAULT=133.0.6943.53".
:: -- 2024/07/06:TomislavMatas: Version "1.127.0"
:: -- * Set "WEBDRIVER_VERSION_DEFAULT=127.0.6533.26".
:: -- 2024/06/24:TomislavMatas: Version "1.126.0"
:: -- * Set "WEBDRIVER_VERSION_DEFAULT=126.0.6478.115"
:: -- 2024/05/03:TomislavMatas: Version "1.125.0"
:: -- * Set "WEBDRIVER_VERSION_DEFAULT=125.0.6422.26".
:: -- 2024/05/02:TomislavMatas: Version "1.124.0"
:: -- * Set "WEBDRIVER_VERSION_DEFAULT=124.0.6367.91".
:: -- 2024/04/04:TomislavMatas: Version "1.0.124"
:: -- * Set "WEBDRIVER_VERSION_DEFAULT=124.0.6367.29"
:: -- 2024/04/04:TomislavMatas: Version "1.0.123"
:: -- * Tidy~Up commentz.
:: -- 2024/03/25:TomislavMatas: Version "1.0.0.0"
:: -- * Initial version with default webdriver version "123.0.6312.58"
:: --

@echo off
setlocal EnableExtensions EnableDelayedExpansion

set "WEBDRIVER_VERSION_DEFAULT=133.0.6943.53"

 
echo see current versions of webdriver at:
echo https://googlechromelabs.github.io/chrome-for-testing/

pause

set "PROJECT_ROOT=%~dp0.."
@rem List of project directories under "%PROJECT_ROOT%\src\", 
@rem where the downloaded binaries shall be copied to.
set "PROJEKT_LIST=EZSeleniumLib"

set "WEBDRIVER_HOMEPAGE=https://storage.googleapis.com/chrome-for-testing-public"
set "WEBDRIVER_VERSION=%1"
if "%WEBDRIVER_VERSION%" == "" (
	set /P "WEBDRIVER_VERSION=Version eingeben oder [Enter] fuer Default '%WEBDRIVER_VERSION_DEFAULT%' : "
)
if "%WEBDRIVER_VERSION%" == "" (
	set "WEBDRIVER_VERSION=%WEBDRIVER_VERSION_DEFAULT%"
)

set "DOWNLOAD_URL=%WEBDRIVER_HOMEPAGE%/%WEBDRIVER_VERSION%"
set "DOWNLOAD_ROOT=%PROJECT_ROOT%\bin\WebDriver\Chrome"
set "DOWNLOAD_DIR=%DOWNLOAD_ROOT%\%WEBDRIVER_VERSION%"
@rem die Liste der Platformen, fuer die jeweils ein WebDriver ZIP Archiv heruntergeladen werden sollen.
@rem set "PLATFORMLIST=win32"
set "PLATFORMLIST=win64"
@rem Beim ChromeDriver ist derzeit keine Umbenennung erforderlich, 
@rem darum custom name = original name.
set "FILENAME_ORIGINAL=chromedriver.exe"
set "FILENAME_CUSTOM=%FILENAME_ORIGINAL%"
set "CURL_EXE=curl"
set "CURL_OPT=-k -sS --fail"

mkdir "%DOWNLOAD_DIR%" 1>nul 2>nul
if not exist "%DOWNLOAD_DIR%\" (
    echo "%DOWNLOAD_DIR%" does not exist
    exit /b 1
)

for %%f in (%PLATFORMLIST%) do (
	set "fn=chromedriver-%%f.zip"
	set "url=%DOWNLOAD_URL%/%%f/!fn!"
	set "loc=%DOWNLOAD_DIR%\!fn!"
	echo download ...
	echo from: "!url!"
	echo to: "!loc!"
	"%CURL_EXE%" %CURL_OPT% "!url!" --output "!loc!"
	if ERRORLEVEL 1 (
		echo ERROR download failed
		exit /b 2
	)	
	echo download OK
)

for %%f in (%PLATFORMLIST%) do (
	set "fn=chromedriver-%%f.zip"
	set "sx=!fn:chromedriver-=!"
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
	@rem Die Windows-Treiber umbenennen
	if not "x!fn:win=!" == "x!fn!" ( 
		@rem ... aber nur falls ein abweichender "custom" name erforderlich sein sollte.
		if not "%FILENAME_ORIGINAL%" == "%FILENAME_CUSTOM%" (
			echo rename "%FILENAME_ORIGINAL%" to "%FILENAME_CUSTOM%" ... 
			del "!unzipedpath!\%FILENAME_CUSTOM%" 1>nul 2>nul
			ren "!unzipedpath!\%FILENAME_ORIGINAL%" "%FILENAME_CUSTOM%" 
			if ERRORLEVEL 1 (
				echo ERROR rename "%FILENAME_ORIGINAL%" to "%FILENAME_CUSTOM%" failed
				exit /b 6
			)	
			echo rename "%FILENAME_ORIGINAL%" to "%FILENAME_CUSTOM%" OK
		)
	)		
	for %%p in (!PROJEKT_LIST!) do (
		set "prj=%%p" 
		set "prjpath=%PROJECT_ROOT%\src\!prj!"
		echo copy "%FILENAME_CUSTOM%" to "!prj!" ...
		del "!prjpath!\%FILENAME_CUSTOM%" 1>nul 2>nul
		set  "subdir=chromedriver-%%f\"
		copy "!unzipedpath!\!subdir!%FILENAME_CUSTOM%" "!prjpath!\%FILENAME_CUSTOM%" 1>nul
		if ERRORLEVEL 1 (
			echo ERROR copy "%FILENAME_CUSTOM%" to "!prj!" failed
			exit /b 7
		)	
		echo copy "%FILENAME_CUSTOM%" to "!prj!" OK
	)
)

rd /s /q "%DOWNLOAD_DIR%" 1>nul
if ERRORLEVEL 1 (
	echo ERROR remove "%DOWNLOAD_DIR%" failed
	exit /b 7
)

explorer "%DOWNLOAD_ROOT%"

exit /b 0

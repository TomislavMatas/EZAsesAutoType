:: -- 
:: -- File: "doChromeWebdriverUpdate.bat"
:: -- 
:: -- Usage: 
:: -- ```
:: -- doChromeWebdriverUpdate[.bat] [version]
:: -- ```
:: -- 
:: -- Example:
:: -- ```
:: -- CMD> doChromeWebdriverUpdate 123.0.6312.58
:: -- ```
:: --
:: -- Revision History
:: -- 2024/03/22:TomislavMatas: Version "24.123.0.0"
:: -- * Inital version with default "123.0.6312.58"
:: --

@echo off
setlocal EnableExtensions EnableDelayedExpansion

set "WEBDRIVER_VERSION_DEFAULT=123.0.6312.58"
 
echo see current versions of webdriver at:
@rem 2023/08/24:TomislavMatas: next line disabled - legacy driver download hompage for version 114.x and lower.
@rem echo https://chromedriver.chromium.org/downloads/
@rem 2023/08/24:TomislavMatas: next line added - legacy driver download hompage for version 115.x (ff).
echo https://googlechromelabs.github.io/chrome-for-testing/

pause

@rem 2023/08/24:TomislavMatas: next line disabled - legacy driver download URLs for version 114.x and lower.
@rem set "WEBDRIVER_HOMEPAGE=https://chromedriver.storage.googleapis.com"
@rem 2023/08/24:TomislavMatas: next line added - new driver download URLs for Version 115.x (ff):
set "WEBDRIVER_HOMEPAGE=https://edgedl.me.gvt1.com/edgedl/chrome/chrome-for-testing"

set "WEBDRIVER_VERSION=%1"
if "%WEBDRIVER_VERSION%" == "" (
	set /P "WEBDRIVER_VERSION=Version eingeben oder [Enter] fuer Default '%WEBDRIVER_VERSION_DEFAULT%' : "
)
if "%WEBDRIVER_VERSION%" == "" (
	set "WEBDRIVER_VERSION=%WEBDRIVER_VERSION_DEFAULT%"
)

set "PROJEKT_ROOT=%~dp0.."

set "DOWNLOAD_URL=%WEBDRIVER_HOMEPAGE%/%WEBDRIVER_VERSION%"
set "DOWNLOAD_ROOT=%PROJEKT_ROOT%\src\common\Selenium\WebDriver\Chrome"
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
	@rem "Verteile" den treiber in die Projekt-Verzeichnisse, in denen dieser benoetigt wird.
	set "prjlist=SeleniumLib"
	for %%p in (!prjlist!) do (
		set "prj=%%p" 
		set "prjpath=%PROJEKT_ROOT%\src\!prj!"
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

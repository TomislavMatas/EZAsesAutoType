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
:: -- 2024/05/31:TomislavMatas: Version "1.126.0"
:: -- * Custom renaming from "msedgedriver.exe" to 
:: --   "MicrosoftWebDriver.exe" is obsolete 
:: --   when using Selenium version 4 (ff).
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

set "WEBDRIVER_HOMEPAGE=https://developer.microsoft.com/de-de/microsoft-edge/tools/webdriver"

echo see browser release schedule at:
echo https://learn.microsoft.com/de-de/deployedge/microsoft-edge-release-schedule/

echo see available versions of webdriver at:
echo %WEBDRIVER_HOMEPAGE%

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

set "PROJECT_ROOT=%~dp0.."
set "SOURCE_ROOT=%PROJECT_ROOT%\src"
set "DOWNLOAD_CDN=https://msedgedriver.azureedge.net"
set "DOWNLOAD_URL=%DOWNLOAD_CDN%/%WEBDRIVER_VERSION%"
set "DOWNLOAD_ROOT=%PROJECT_ROOT%\bin\WebDriver\Edge"
set "DOWNLOAD_DIR=%DOWNLOAD_ROOT%\%WEBDRIVER_VERSION%"
@rem die Liste der Dateien, die heruntergeladen werden sollen.
::set "FILELIST=edgedriver_win64.zip edgedriver_win32.zip edgedriver_linux64.zip edgedriver_arm64.zip"
set "FILELIST=edgedriver_win64.zip"
@rem Die Umbenennung fuer den msedgedriver Treiber war fuer Selenium 3 erforderlich.
@rem Ab Selenium 4 ist die Umbennung nicht mehr erforderlich,
@rem darum custom name = original name.
set "FILENAME_ORIGINAL=msedgedriver.exe"
@rem set "FILENAME_CUSTOM=MicrosoftWebDriver.exe"
set "FILENAME_CUSTOM=%FILENAME_ORIGINAL%"
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
		echo FAILURE
		exit /b 2
	)	
	echo OK
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
		echo FAILURE
		exit /b 3
	)	
	echo OK

	@rem Zip-Datei verschieben, weil erfolgreich verarbeitet
	echo move "!fn!" ... 
	del "!unzipedpath!\!fn!" 1>nul 2>nul
	move "!zipfile!" "!unzipedpath!" 1>nul
	if ERRORLEVEL 1 (
		echo FAILURE
		exit /b 4
	)	
	echo OK

	@rem Loesche das nicht wirklich benoetigte Verzeichnis "Driver_Notes" aus dem entpackten Verzeichnis.
	@rem Ausnahme: Das Verzeichnis "Driver_Notes" gibt es in allen Paketen ausser fuer linux.
	if "x!fn:linux=!" == "x!fn!" ( 
		echo remove directory "Driver_Notes" ... 
		rd /s /q "!unzipedpath!\Driver_Notes" 1>nul
		if ERRORLEVEL 1 (
			echo FAILURE
			exit /b 5
		)	
		echo OK
	)

@rem	echo copy "%FILENAME_ORIGINAL%" to "%SOURCE_ROOT%" ...
@rem	copy "!unzipedpath!\%FILENAME_ORIGINAL%" "%SOURCE_ROOT%\%FILENAME_ORIGINAL%" 1>nul
@rem	if ERRORLEVEL 1 (
@rem		echo FAILURE
@rem		exit /b 6
@rem	)	
@rem	echo OK

	@rem Eine umbenannte Kopie des Windows-Treiber bereitstellen, falls erfoderlich.
	if not "x!fn:win=!" == "x!fn!" ( 
		if not "%FILENAME_ORIGINAL%" == "%FILENAME_CUSTOM%" (
			echo copy "%FILENAME_CUSTOM%" to "%SOURCE_ROOT%" ...
			copy "!unzipedpath!\%FILENAME_CUSTOM%" "%SOURCE_ROOT%\%FILENAME_CUSTOM%" 1>nul
			if ERRORLEVEL 1 (
				echo FAILURE
				exit /b 7
			)	
			echo OK
		)
    @rem "Verteile" den win64 in die PROJECT-Verzeichnisse, in denen dieser benoetigt wird.
    if not "x!fn:win64=!" == "x!fn!" ( 
      for %%p in (!PROJECT_LIST!) do (
        set "prj=%%p" 
        set "prjpath=%SOURCE_ROOT%\!prj!"
        echo copy "%FILENAME_CUSTOM%" to "!prj!" ...
        del "!prjpath!\%FILENAME_CUSTOM%" 1>nul 2>nul
        copy "!unzipedpath!\%FILENAME_CUSTOM%" "!prjpath!\%FILENAME_CUSTOM%" 1>nul
        if ERRORLEVEL 1 (
          echo FAILURE
          exit /b 8
        )	
        echo OK
      )
    )    
	)
)

echo remove "%DOWNLOAD_DIR%" ...
rd /s /q "%DOWNLOAD_DIR%" 1>nul
if ERRORLEVEL 1 (
	echo FAILURE
	exit /b 9
)
echo OK

explorer "%DOWNLOAD_ROOT%"

exit /b 0

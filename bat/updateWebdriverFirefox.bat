:: -- 
:: -- File: "updateWebdriverFirefox.bat"
:: -- 
:: -- Usage: 
:: -- ```
:: -- updateWebdriverFirefox[.bat] [ version ]
:: -- ```
:: --
:: -- Example: 
:: -- ```
:: -- CMD> updateWebdriverFirefox 0.35.0
:: -- ```
:: --
:: -- Download-URL deeplink:
:: -- https://github.com/mozilla/geckodriver/releases/download/v0.35.0/geckodriver-v0.35.0-win64.zip
:: -- Obacht: Bei "github.com" muss bei der Verwendung von curl der 
:: -- Parameter -L verwendet werden, sonst klappt der Download nicht.
:: -- 
:: -- Mit der WebDriver Version "v0.35.0" koennen aktuell die
:: -- FireFox Versionen ab "115" ferngesteuert werden.
:: -- Mit der WebDriver Version "v0.33.0" konnten zuletzt die
:: -- FireFox Versionen von "102" bis "120" ferngesteuert werden.
:: -- Voraussetzung ist jeweils die Verwendung von Selenium Version >= 3.11".
:: -- Fuer eine vollstaendige Liste siehe 
:: -- "GeckoDriver, Selenium and Firefox Browser compatibility chart":
:: -- https://firefox-source-docs.mozilla.org/testing/geckodriver/Support.html
:: -- 
:: -- Revision History
:: -- 2024/09/26:TomislavMatas: Version "1.129.0"
:: -- * set "WEBDRIVER_VERSION_DEFAULT=0.35.0".
:: -- * BugFix: Propagation to projects should work now as expectd.
:: -- 2024/04/04:TomislavMatas: Version "1.0.123"
:: -- * Tidy~Up commentz.
:: -- 2024/03/22:TomislavMatas: Version 1.0.0.0
:: -- * Initial version with default "0.34.0".
:: --

@echo off
setlocal EnableExtensions EnableDelayedExpansion

set "WEBDRIVER_HOMEPAGE=https://github.com/mozilla/geckodriver/releases"
 
echo see current versions of webdriver at:
echo %WEBDRIVER_HOMEPAGE%

pause 

set "PROJECT_ROOT=%~dp0.."
@rem List of project directories under "%PROJECT_ROOT%\src\", 
@rem where the downloaded binaries shall be copied to.
set "PROJEKT_LIST=EZSeleniumLib"

set "WEBDRIVER_VERSION=%1"
set "WEBDRIVER_VERSION_DEFAULT=0.35.0"
if "%WEBDRIVER_VERSION%" == "" (
	set /P "WEBDRIVER_VERSION=Version eingeben oder [Enter] fuer Default '%WEBDRIVER_VERSION_DEFAULT%' : "
)
if "%WEBDRIVER_VERSION%" == "" (
	set "WEBDRIVER_VERSION=%WEBDRIVER_VERSION_DEFAULT%"
)


set "DOWNLOAD_URL=%WEBDRIVER_HOMEPAGE%"
set "DOWNLOAD_ROOT=%PROJECT_ROOT%\bin\WebDriver\FireFox"
set "DOWNLOAD_DIR=%DOWNLOAD_ROOT%\%WEBDRIVER_VERSION%"
@rem die Liste der Platformen, fuer die jeweils ein 
@rem WebDriver ZIP Archiv heruntergeladen werden soll.
@rem set "PLATFORMLIST=win64 win32"
set "PLATFORMLIST=win64"
@rem Beim geckodriver ist derzeit keine Umbenennung erforderlich, 
@rem darum custom name = original name.
set "FILENAME_ORIGINAL=geckodriver.exe"
set "FILENAME_CUSTOM=%FILENAME_ORIGINAL%"
set "CURL_EXE=curl"
set "CURL_OPT=-k -L -sS --fail"

mkdir "%DOWNLOAD_DIR%" 1>nul 2>nul
if not exist "%DOWNLOAD_DIR%\" (
    echo "%DOWNLOAD_DIR%" does not exist
    exit /b 1
)

for %%f in (%PLATFORMLIST%) do (
	set "fn=geckodriver-v%WEBDRIVER_VERSION%-%%f.zip"	
	set "url=%DOWNLOAD_URL%/download/v%WEBDRIVER_VERSION%/!fn!"
	set "loc=%DOWNLOAD_DIR%\!fn!"
	echo downloading ...
	echo from: "!url!"
	echo to: "!loc!"
    "%CURL_EXE%" %CURL_OPT% "!url!" --output "!loc!"
	if ERRORLEVEL 1 (
		echo ERROR download failed
		exit /b 2
	)	
	echo download OK
)

@rem Mit der Sequenz "!sx=..." soll aus dem Dateinamen
@rem des heruntergeladenen ZIP-Archivs die Version
@rem und die Architektur extrahiert werden.
@rem Dabei soll fuer die Architektur aus "win32" / "win64" die jeweilige
@rem Kurzform "x32" bzw "x64" ersetzt werden. 
for %%f in (%PLATFORMLIST%) do (
	set "fn=geckodriver-v%WEBDRIVER_VERSION%-%%f.zip"	
	set "sx=!fn:geckodriver-v%WEBDRIVER_VERSION%-=!"
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
	
	@rem Die Windows-Treiber umbenennen, falls erfoderlich
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
	for %%p in (!PROJEKT_LIST!) do (
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

rd /s /q "%DOWNLOAD_DIR%" 1>nul
if ERRORLEVEL 1 (
	echo ERROR remove "%DOWNLOAD_DIR%" failed
	exit /b 7
)

explorer "%DOWNLOAD_ROOT%"

exit /b 0

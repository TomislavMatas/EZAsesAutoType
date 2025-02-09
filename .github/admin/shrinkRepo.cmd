:: --
:: -- File: "shrinkRepo.cmd"
:: --
:: -- Utilize BFG Repo Cleaner to shrink repository.
:: --
:: -- See: 
:: -- -->< https://rtyley.github.io/bfg-repo-cleaner/ >
:: -- -->< https://stackoverflow.com/questions/73673092/how-to-use-bfg-repo-cleaner >
:: --
:: -- Revision History
:: -- 2024/11/24:TomislavMatas: webdriver/131 Version "1.131.3"
:: -- * Initial version.
:: --

@setlocal
@set "BFG_GIT=git@github.com:TomislavMatas/EZAsesAutoType.git"
@set "BFG_TMP=%TEMP%"
@set "BFG_MIR=%BFG_TMP%\EZAsesAutoType.mir"
@set "BFG_RPT=%BFG_MIR%.bfg-report"

:cleanupBefore
@rmdir /s/q "%BFG_RPT%" 1>nul 2>nul
@rmdir /s/q "%BFG_MIR%" 1>nul 2>nul

@mkdir "%BFG_TTMP%" 1>nul 2>nul
git clone --mirror "%BFG_GIT%" "%BFG_MIR%"
@if errorlevel 1 @pause

pushd "%BFG_MIR%"
@if errorlevel 1 @pause

java -jar "%~dp0bfg-1.14.0.jar" --delete-files EZAsesAutoType-{portable,setup}*.{zip,msi} "%BFG_MIR%"
@if errorlevel 1 @pause

git reflog expire --expire=now --all 
@if errorlevel 1 @pause

git gc --prune=now --aggressive
@if errorlevel 1 @pause

git push
@if errorlevel 1 @pause

:cleanupAfter
@rmdir /s/q "%BFG_RPT%" 1>nul 2>nul
@rmdir /s/q "%BFG_MIR%" 1>nul 2>nul

:done

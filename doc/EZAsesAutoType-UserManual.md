# "EZ ASES AutoType"
The Microsoft Windows Forms application "EZ ASES AutoType" is supposed
to automate the daily chore of utilizing the virtual punch card system
"ATOSS Staff Efficiency Suite" also known as "ASES".

## Installation 
Execute the setup msi file and follow the instructions. 
You might get prompted to enter admin account credentials during installation.

## Configuration
After installation the configuration file "EZAsesAutoType.dll.config" 
can be found in the same directory as the application's executable 
file "EZAsesAutoType.exe":

![ConfigFile](../res/img/Screenshot-WindowsExplorer-ConfigFile.png)

All configuration values are set to reasonable default values 
and usually there's no need to modify any of the values manually.

The user settings entered within the main dialog are stored within
an unencrypted file within user's profile. The location will vary
across release versions. It strongly recommended to not edit the
user settings file manually.

## Logging
The "EZ ASES AutoType" application utilizes the third party logger "log4net". 
All aspects are controlled by settings within the application's configuration 
file "EZAsesAutoType.dll.config". Logging is configured with reasonable default 
values and usually there's no need to modify any of the values manually.

NOTE: The default location for the log file is "%UserProfile%&#92;.EZAsesAutoType":

![LogFile](../res/img/Screenshot-WindowsExplorer-LogFile.png)

## Usage
Start the application "EZAsesAutoType.exe" from the installation location.
Set the desired values in the main dialog:

![MainDialog](../res/img/Screenshot-MainDialog-1.124.0.png)

Hit the "Run" button and "EZ ASES AutoType" will do the
following for you completely automatically:

1) Open a new browser instance.
2) Log in with provided credentials.
3) Open the time entry grid.
4) Position the cursor to the time entry grid's last row.
5) Bring up the time pair entry popup dialog.
6) Enter the time pairs.
7) Save the time entry grid.
8) Log out and close browser instance.

Be a bit patient after hitting the "Run" button. The browser instance
startup and initialization may take a few seconds.

You can hit the "Cancel" button at any time. Control will be passed back to
the main dialog as soon as technical possible.

The user settings will be stored within user's profile and reused
on subsequent starts of the application.

## Time entry shortcuts
* single dot (".") or "now" will evaluate to "now".
  
* "nine" will evaluate to 9am ("09:00").
  
* "five" will evaluate to 5pm ("17:00").
  
* Digits between "0" and "23" will evaluate to respective "hour", e.g.:   
   "7" --> "07:00"   
  "18" --> "18:00"     

* digits between "24" and "59" will evaluate to respective "minute" 
  of current "hour". For example when entered at 9am:   
  "30" --> "09:30"   

* Any entry consisting of three or four digits without the separator ":" 
  will be treated as a time entry in the form of "hhmm". For example:   
   "700" --> "07:00"   
  "1600" --> "16:00"   

All shortcut replacements are implemented within the 
function "FormMain.EvalTimeByFragment()".

# Revision History
## 2024/04/13:TomislavMatas: Version "1.123.4"
* Update screenshot of main dialog.

## 2024/04/10:TomislavMatas: Version "1.123.3"
* Initial version of this user manual.

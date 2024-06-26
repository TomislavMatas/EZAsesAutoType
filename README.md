# "EZ ASES AutoType"
The Microsoft Windows Forms application "EZ ASES AutoType" is supposed 
to automate the daily chore of utilizing the virtual punch card system 
"ATOSS Staff Efficiency Suite" also known as "ASES".

## Overview
Start the application and set the desired values in the main dialog:  

![MainDialog](res/img/Screenshot-MainDialog-v1.126.0.png)  

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

For more details please refer to the user's manual:
->< [doc/EZAsesAutoType-UserManual.md](doc/EZAsesAutoType-UserManual.md) >

## "bat" subfolder
Contains some helpful windows batch files.

## "bin" subfolder
Contains required third party binaries as well as the released 
versions of "EZ ASES AutoType".

## "cfg" subfolder
Templates for config files, which can be used with "EZAsesAutoType.exe".

## "doc" subfolder
Contains supplementary documentation about "EZ ASES AutoType".
Most noticeable: "EZAsesAutoType-UserManual.md"

## "src" subfolder
Contains all source code to build "EZAsesAutoType.exe".

# Revision History
## 2024/05/27:TomislavMatas: Version "1.126.0"  
* Update screenshot of main dialog.  
  
## 2024/04/10:TomislavMatas: Version "1.123.3"
* Add "Overview" section to this "README.md" file.

## 2024/04/04:TomislavMatas: Version "1.0.0"
* Initial version.

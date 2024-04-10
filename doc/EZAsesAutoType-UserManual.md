# "EZAsesAutoType"
The Microsoft Windows Forms application "EZAsesAutoType.exe" is supposed 
to automate the daily chore of utilizing the virtual punch card system 
"ATOSS Staff Efficiency Suite" also known as "ASES".

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

* Any entry consisting of three or four digits without the seperator ":" 
  will be treated as a time entry in the form of "hhmm". For example:
   "700" --> "07:00"
  "1600" --> "16:00"

All shortcut replacements are implemented within the 
function "FormMain.EvalTimeByFragment()".

# Revision History
## 2024/04/04:TomislavMatas: Version "1.123.3"
* Initial version of this user manual.

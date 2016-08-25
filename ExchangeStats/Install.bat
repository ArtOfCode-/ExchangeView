@ECHO OFF
ECHO Copying required files to AppData...
XCOPY /s /y "ResourceFiles" "%APPDATA%\ExchangeStats\Resources"
ECHO Install done, press any key to finish.
PAUSE
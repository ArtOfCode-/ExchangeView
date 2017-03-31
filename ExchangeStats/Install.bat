@ECHO OFF
ECHO Copying required files to AppData...
MKDIR "%APPDATA%\ExchangeStats"
MKDIR "%APPDATA%\ExchangeStats\Cache"
MKDIR "%APPDATA%\ExchangeStats\Resources"
XCOPY /s /y "ResourceFiles" "%APPDATA%\ExchangeStats\Resources"
ECHO Install done, press any key to finish.
PAUSE
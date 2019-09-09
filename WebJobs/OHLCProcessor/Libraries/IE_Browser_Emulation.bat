@ECHO OFF

reg add "HKLM\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION" /v "HelloUpstoxV2.exe" /t REG_DWORD /d 0x00008000 /f
reg add "HKCU\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION" /v "HelloUpstoxV2.exe" /t REG_DWORD /d 0x00008000 /f

PAUSE


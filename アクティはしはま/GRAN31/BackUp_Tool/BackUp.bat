net use Y: \\192.168.100.9\BackUp
ping 1.0.0.0 -w 5000 -n 1
cscript /b D:\GRAN31\BackUp_Tool\wday.vbs
if %errorlevel%==7 set WDAY=SAT
if %errorlevel%==6 set WDAY=FRI
if %errorlevel%==5 set WDAY=THU
if %errorlevel%==4 set WDAY=WED
if %errorlevel%==3 set WDAY=TUE
if %errorlevel%==2 set WDAY=MON
if %errorlevel%==1 set WDAY=SUN

pg_dump -Fc -h localhost -U root ACTY > Z:\BackUp\%WDAY%\ACTY.dmp
pg_dump -Fc -h localhost -U root ACTY > Y:\ACTY.dmp
pg_dump -Fc -h localhost -U root ACTY > D:\BackUp\%WDAY%\ACTY.dmp
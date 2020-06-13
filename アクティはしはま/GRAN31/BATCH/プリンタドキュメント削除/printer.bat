@echo off
 echo ＜印刷ジョブの削除！！＞
echo しばらくお待ちください・・
net stop spooler > nul
 net start spooler > nul
 net stop spooler > nul
 del %systemroot%\system32\spool\printers\*.shd
 del %systemroot%\system32\spool\printers\*.spl
 net start spooler > nul
 exit
@echo off
 echo ������W���u�̍폜�I�I��
echo ���΂炭���҂����������E�E
net stop spooler > nul
 net start spooler > nul
 net stop spooler > nul
 del %systemroot%\system32\spool\printers\*.shd
 del %systemroot%\system32\spool\printers\*.spl
 net start spooler > nul
 exit
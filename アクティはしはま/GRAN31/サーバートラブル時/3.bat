@echo off
 
REM PostgreSQL��bin�f�B���N�g��
set PGPATH=C:\Program Files\PostgreSQL\9.6\bin\
 
REM �ڑ�����
set HOST=192.168.6.10
set PORT=5432
set USER_ID=root
set DB_NAME=ACTY
set PGPASSWORD=assist
 
REM �X�N���v�g���s
"%PGPATH%psql" -h %HOST% -p %PORT% -U %USER_ID% -d %DB_NAME% -f D:\GRAN31\BATCH\seqtrn.sql

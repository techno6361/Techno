@echo on
netsh interface ipv4 set add name="イーサネット" source=static addr="192.168.24.21" mask="255.255.255.0"
netsh interface ipv4 set dnsservers name="イーサネット" source=dhcp
pause
exit

 
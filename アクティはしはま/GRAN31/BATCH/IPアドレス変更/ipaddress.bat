@echo on
netsh interface ipv4 set add name="�C�[�T�l�b�g" source=static addr="192.168.24.21" mask="255.255.255.0"
netsh interface ipv4 set dnsservers name="�C�[�T�l�b�g" source=dhcp
pause
exit

 
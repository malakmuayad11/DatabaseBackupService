# Database Backup Service
A Windows service that backs up
# Build Instructions (Release Mode):
1. Save the source code.
2. Build the solution (ctrl + shift + b) in release mode.
# Deployment Instructions:
### Installation (Using InstallUtil):
1. Open the solution's folder in File Explorer.
2. Open bin -> release -> copy file path **(service's file path)**.
3. Open the command prompt in **administrator mode**.
4. Change the current directory to the **service's file path**.
5. Use the following command:
   - for 64-bit systems: C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe FileMonitoringService.exe
   - for 32-bit systems: C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe FileMonitoringService.exe
### Start Service:
1. Open the command prompt in administrator mode.
2. Use this command: sc start FileMonitoringService
### Stop Service:
1. Open the command prompt in administrator mode.
2. Use this command: sc stop FileMonitoringService
# Uninstallation (Using IntallUtil):
1. Open command prompt in adminstrator mode.
2. Use the following command:
   - for 64-bit systems: C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe -u FileMonitoringService.exe
   - for 32-bit systems: C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe -u FileMonitoringService.exe

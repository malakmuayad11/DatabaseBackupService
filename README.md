# Database Backup Service
A Windows service that backs up a specified database at each specified interval.
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
   - for 64-bit systems: C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe DatabaseBackupService.exe
   - for 32-bit systems: C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe DatabaseBackupService.exe
### Start Service:
1. Open the command prompt in administrator mode.
2. Use this command: sc start DatabaseBackupService
### Stop Service:
1. Open the command prompt in administrator mode.
2. Use this command: sc stop DatabaseBackupService
# Uninstallation (Using IntallUtil):
1. Open the command prompt in administrator mode.
2. Use the following command:
   - for 64-bit systems: C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe -u DatabaseBackupService.exe
   - for 32-bit systems: C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe -u DatabaseBackupService.exe

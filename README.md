# BypassAV
A simple AV bypass with C#
Compile with .NET 4.0

C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /t:exe /out:Shell.exe .\reverse_shell.cs

Opening listen port on remote machine:

nc -nlvp 13338

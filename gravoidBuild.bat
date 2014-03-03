
REM *******************************************************
REM INITIALIZING VARIABLES
REM *******************************************************
set REPOSITORY=%CD%
set BUILDDIR=%CD%\gravoid_builds
set WINDIR=%BUILDDIR%\windows
set WIN64DIR=%BUILDDIR%\windows64
set LINDIR=%BUILDDIR%\linux
set LIN64DIR=%BUILDDIR%\linux64
set OSXDIR=%BUILDDIR%\osx
set BUILDWINDOWS= -buildWindowsPlayer %WINDIR%\gravoid.exe
set BUILDOSX= -buildOSXPlayer %OSXDIR%\gravoid.app 
set BUILDLINUX= -buildLinux32Player %LINDIR%\gravoid -buildLinux64Player %LIN64DIR%\gravoid
set BUILDCOMMANDS= -batchmode -quit -projectPath "%REPOSITORY%\gravoid_unity_project" %BUILDWINDOWS% %BUILDOSX% %BUILDLINUX%
set PACKEDFILENAME=gravoid_build
set PACKEDFILE=%PACKEDFILENAME%.zip

REM *******************************************************
REM INITIALIZING FOLDER STRUCURE
REM *******************************************************
rmdir /Q /S %BUILDDIR%
del /Q /F %PACKEDFILE%
mkdir %BUILDDIR%
mkdir %WINDIR%
mkdir %WIN64DIR%
mkdir %LINDIR%
mkdir %LIN64DIR%
mkdir %OSXDIR%

REM *******************************************************
REM COMPILING AND PACKING BUILD
REM *******************************************************
"C:\Program Files (x86)\Unity\Editor\Unity.exe" %BUILDCOMMANDS%
"C:\Program Files (x86)\7-Zip\7z.exe" a -tzip %REPOSITORY%\%PACKEDFILENAME% %BUILDDIR%\*

REM *******************************************************
REM *******************************************************
pause
rmdir /Q /S %BUILDDIR%
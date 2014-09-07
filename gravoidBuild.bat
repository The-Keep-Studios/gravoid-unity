
REM *******************************************************
REM INITIALIZING VARIABLES
REM *******************************************************
set REPOSITORY=%CD%
set BUILDDIR=%CD%\gravoid_builds
set WINDIR=%BUILDDIR%\windows
REM set WIN64DIR=%BUILDDIR%\windows64
REM set LINDIR=%BUILDDIR%\linux
REM set LIN64DIR=%BUILDDIR%\linux64
REM set OSXDIR=%BUILDDIR%\osx
set BUILDWINDOWS= -buildWindowsPlayer "%WINDIR%\gravoid.exe"
REM set BUILDOSX= -buildOSXPlayer "%OSXDIR%\gravoid.app"
REM set BUILDLINUX= -buildLinux32Player "%LINDIR%\gravoid -buildLinux64Player %LIN64DIR%\gravoid"
set BUILDCOMMANDS= -batchmode -quit -projectPath "%REPOSITORY%\" %BUILDWINDOWS%
set PACKEDFILENAME=gravoid_build
set PACKEDFILE=%PACKEDFILENAME%.zip

REM *******************************************************
REM INITIALIZING FOLDER STRUCURE
REM *******************************************************
rmdir /Q /S "%BUILDDIR%"
del /Q /F "%PACKEDFILE%"
mkdir "%BUILDDIR%"
mkdir "%WINDIR%"
REM mkdir "%WIN64DIR%"
REM mkdir "%LINDIR%"
REM mkdir "%LIN64DIR%"
REM mkdir "%OSXDIR%"

REM *******************************************************
REM COMPILING AND PACKING BUILD
REM *******************************************************
"C:\Program Files (x86)\Unity\Editor\Unity.exe" -buildWindowsPlayer "%WINDIR%\gravoid.exe"
"C:\Program Files (x86)\7-Zip\7z.exe" a -tzip %REPOSITORY%\%PACKEDFILENAME% %BUILDDIR%\*

REM *******************************************************
REM *******************************************************
pause
rmdir /Q /S "%BUILDDIR%"
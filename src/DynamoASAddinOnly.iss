; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define ProductPublisher "Autodesk, Inc."
#define ProductURL "http://www.autodesk.com/"
#define ProductName "Dynamo Extension for Autodesk� Advance Steel 2017"
#define ProductVersion "1.0.0"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{B63CB95B-78D5-4317-BF89-450F3C09BB1D}
AppName={#ProductName}
AppVersion={#ProductVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#ProductPublisher}
;AppPublisherURL={#ProductURL}
;AppSupportURL={#ProductURL}
;AppUpdatesURL={#ProductURL}
CreateAppDir=no
;DefaultDirName={pf64}\{#ProductName} {#ProductVersion} 
SourceDir=..\setup\installers
LicenseFile=Readme.rtf
;InfoBeforeFile=BeforeInstall.txt
OutputDir=..\
OutputBaseFilename=DynamoForAdvanceSteel2017
SetupIconFile=..\..\tools\Extra\W16_DYNADST_launch.ico
CreateUninstallRegKey=no
;UninstallFilesDir={tmp}\Uninstall
;UninstallDisplayIcon={tmp}\logo_square_32x32.ico
;UninstallDisplayName={#ProductName} {#ProductVersion}
Compression=lzma
SolidCompression=yes
SetupLogging=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

;Core Files
[Files]
;Source: DynamoCore.msi; DestDir: {tmp}; 
Source: DynamoForAdvanceSteel2017.msi; DestDir: {tmp}; 
;Source: IronPython-2.7.3.msi; DestDir: {tmp};  
;Source: fsharp_redist.exe; DestDir: {tmp}; 
;Flags: deleteafterinstall;
;Source: README.txt; DestDir: {tmp}\; Flags: onlyifdoesntexist isreadme ignoreversion;

;Icon
Source: ..\..\tools\Extra\W16_DYNADST_launch.ico; DestDir: {tmp}; Flags: ignoreversion overwritereadonly;

[Run]
;Filename: "msiexec.exe"; Parameters: "/i ""{tmp}\DynamoCore.msi"" /qn"; 
Filename: "msiexec.exe"; Parameters: "/i ""{tmp}\DynamoForAdvanceSteel2017.msi"" /qn";
;Filename: "msiexec.exe"; Parameters: "/i ""{tmp}\IronPython-2.7.3.msi"" /qn";
;Filename: "msiexec.exe"; Parameters: "/i ""{tmp}\fsharp_redist.exe"" /qn";     

;[UninstallRun]
;Filename: msiexec.exe; Parameters: "/x ""{code:GetSamplesID}"" /qn"; Check:CheckHasSamplesID(); Flags: runhidden; 
;Filename: "msiexec.exe"; Parameters: "/x ""{tmp}\IronPython-2.7.3.msi"" /qn"  ; Flags: runhidden;
;Filename: "{app}\IronPython-2.7.3.msi"; Parameters: -U
;Filename: "{tmp}\fsharp_redist.exe"; Parameters: "/uninstall ""{app}"""; Flags: runhidden;
;Filename: "{tmp}\DynamoAdvanceSteelInstall.msi"; Parameters: "/uninstall ""{app}"""; Flags: runhidden;
;Filename: "{tmp}\DynamoInstall.msi"; Parameters: "/uninstall ""{app}"""; Flags: runhidden;

[Code]
function InitializeSetup: Boolean;
begin
  // allow the setup to continue initially
  Result := True;
  if not RegKeyExists(HKLM, 'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Autodesk Advance Steel 2017') then
  begin
    // return False to prevent installation to continue
    Result := False;
    // and display a message box
    MsgBox('This application requires Advance Steel 2017. Please install Advance Steel 2017 then run this installer again.', mbError, MB_OK);
  end;
  if not (RegKeyExists(HKLM, 'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Dynamo Core 1.1') or RegKeyExists(HKLM64, 'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Dynamo Core 1.1'))  then
  begin
    // return False to prevent installation to continue
    Result := False;
    // and display a message box
    MsgBox('This application requires Dynamo Core 1.1. Please install Dynamo 1.1 then run this installer again.', mbError, MB_OK);
  end;
end;

; —————————————————————————————————————————————————————
; Script Inno Setup para CompiladorJava v1.0
; Self‑contained build en:
;   bin\Release\net9.0-windows\win-x64\publish\
; Compatible con Windows 8.1, 10 y 11
; —————————————————————————————————————————————————————

#define MyAppName    "CompiladorJava"
#define MyAppVersion "1.0"
#define MyAppPublisher "Benjamin Higueras"
#define MyAppURL     "https://www.example.com/"
#define MyAppExeName "CompilerUIWinForms.exe"

[Setup]
; Identificación única
AppId={{5AA3BDB5-CA24-4412-BA0E-50F6636C73EF}}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
UninstallDisplayIcon={app}\{#MyAppExeName}
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
DisableProgramGroupPage=yes
OutputBaseFilename=CompiladorSetup
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; — Ejecutable único self‑contained
Source: "E:\PROGRAMACION\Compilers\CompilersSolution\CompilerUIWinForms\bin\Release\net9.0-windows\win-x64\publish\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion

; — Otras dependencias que pudieran quedar
Source: "E:\PROGRAMACION\Compilers\CompilersSolution\CompilerUIWinForms\bin\Release\net9.0-windows\win-x64\publish\*.dll";       DestDir: "{app}"; Flags: ignoreversion
Source: "E:\PROGRAMACION\Compilers\CompilersSolution\CompilerUIWinForms\bin\Release\net9.0-windows\win-x64\publish\*.json";      DestDir: "{app}"; Flags: ignoreversion

; — Carpetas nativas
Source: "E:\PROGRAMACION\Compilers\CompilersSolution\CompilerUIWinForms\bin\Release\net9.0-windows\win-x64\publish\x86\*"; DestDir: "{app}\x86"; Flags: recursesubdirs createallsubdirs
Source: "E:\PROGRAMACION\Compilers\CompilersSolution\CompilerUIWinForms\bin\Release\net9.0-windows\win-x64\publish\x64\*"; DestDir: "{app}\x64"; Flags: recursesubdirs createallsubdirs

; — Icono personalizado para accesos directos
Source: "E:\PROGRAMACION\Compilers\CompilersSolution\CompilerUIWinForms\app_icon.ico"; DestDir: "{tmp}"; Flags: deleteafterinstall


[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#MyAppName}}"; Flags: nowait postinstall skipifsilent

[Code]
// — Chequeo opcional de .NET 9; si usas self‑contained puedes comentar todo esto
function IsDotNet9Installed(): Boolean;
var
  installKey, ver: string;
begin
  installKey := 'SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedhost';
  if RegQueryStringValue(HKLM, installKey, 'Version', ver) then
    Result := Copy(ver,1,2) = '9.'
  else
    Result := False;
end;

procedure InitializeWizard();
begin
  if not IsDotNet9Installed() then
    MsgBox('.NET 9 Runtime no encontrado. Por favor instálalo antes de continuar.'#13#10 +
           'Descarga: https://dotnet.microsoft.com/download', mbError, MB_OK);
end;

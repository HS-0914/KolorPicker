[Setup]
AppName=KolorPicker
AppVersion=1.0
DefaultDirName={pf}\KolorPicker
DefaultGroupName=KolorPicker
OutputDir=.\Output
OutputBaseFilename=KolorPickerSetup
Compression=lzma
SolidCompression=yes

[Files]
Source: "D:\workspace\c\HS-0914\KolorPicker\bin\Release\KolorPicker.exe"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\KolorPicker"; Filename: "{app}\KolorPicker.exe"
Name: "{userdesktop}\KolorPicker"; Filename: "{app}\KolorPicker.exe"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "바탕화면에 KolorPicker 바로가기 생성"; GroupDescription: "추가 아이콘:"

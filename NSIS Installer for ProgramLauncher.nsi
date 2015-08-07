; ProgramLauncher Installer NSIS Script
; get NSIS at http://nsis.sourceforge.net/Download
; As a program that all Power PC users should have, Notepad++ is recommended to edit this file

;Icon "My Project\document-properties.ico"
Caption "ProgramLauncher Installer"
Name "ProgramLauncher"
AutoCloseWindow true
ShowInstDetails show

LicenseBkColor /windows
LicenseData "LICENSE.md"
LicenseForceSelection checkbox "I have read and understand this notice"
LicenseText "Please read the notice below before installing ProgramLauncher. If you understand the notice, click the checkbox below and click Next."

InstallDir $PROGRAMFILES\WalkmanOSS

OutFile "bin\Release\ProgramLauncher-Installer.exe"

; Pages

Page license
Page components
Page directory
Page instfiles
UninstPage uninstConfirm
UninstPage instfiles

; Sections

Section "Executable & Uninstaller"
  SectionIn RO
  SetOutPath $INSTDIR
  File "bin\Release\ProgramLauncher.exe"
  WriteUninstaller "ProgramLauncher-Uninst.exe"
SectionEnd

Section "Start Menu Shortcuts"
  CreateDirectory "$SMPROGRAMS\WalkmanOSS"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\ProgramLauncher.lnk" "$INSTDIR\ProgramLauncher.exe" "" "$INSTDIR\ProgramLauncher.exe" "" "" "" "ProgramLauncher"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\Uninstall ProgramLauncher.lnk" "$INSTDIR\ProgramLauncher-Uninst.exe" "" "" "" "" "" "Uninstall ProgramLauncher"
  ;Syntax for CreateShortCut: link.lnk target.file [parameters [icon.file [icon_index_number [start_options [keyboard_shortcut [description]]]]]]
SectionEnd

Section "Desktop Shortcut"
  CreateShortCut "$DESKTOP\ProgramLauncher.lnk" "$INSTDIR\ProgramLauncher.exe" "" "$INSTDIR\ProgramLauncher.exe" "" "" "" "ProgramLauncher"
SectionEnd

Section "Quick Launch Shortcut"
  CreateShortCut "$QUICKLAUNCH\ProgramLauncher.lnk" "$INSTDIR\ProgramLauncher.exe" "" "$INSTDIR\ProgramLauncher.exe" "" "" "" "ProgramLauncher"
SectionEnd

Section "Add ProgramLauncher to context menu"
  ; File item
  WriteRegStr HKCR "*\shell\ProgramLauncher" "" "Properties..."
  WriteRegStr HKCR "*\shell\ProgramLauncher" "Icon" "$INSTDIR\ProgramLauncher.exe"
  WriteRegStr HKCR "*\shell\ProgramLauncher\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
SectionEnd

; Functions

Function .onInit
  MessageBox MB_YESNO "This will install ProgramLauncher. Do you wish to continue?" IDYES gogogo
    Abort
  gogogo:
  SetShellVarContext all
  SetAutoClose true
FunctionEnd

Function .onInstSuccess
    MessageBox MB_YESNO "Install Succeeded! Open ReadMe?" IDNO NoReadme
      ExecShell "open" "https://github.com/Walkman100/ProgramLauncher/blob/master/README.md#programlauncher-"
    NoReadme:
FunctionEnd

; Uninstaller

Section "Uninstall"
  Delete "$INSTDIR\ProgramLauncher-Uninst.exe" ; Remove Application Files
  Delete "$INSTDIR\ProgramLauncher.exe"
  RMDir "$INSTDIR"
  
  Delete "$SMPROGRAMS\WalkmanOSS\ProgramLauncher.lnk" ; Remove Start Menu Shortcuts & Folder
  Delete "$SMPROGRAMS\WalkmanOSS\Uninstall ProgramLauncher.lnk"
  RMDir "$SMPROGRAMS\WalkmanOSS"
  
  Delete "$DESKTOP\ProgramLauncher.lnk"     ; Remove Desktop      Shortcut
  Delete "$QUICKLAUNCH\ProgramLauncher.lnk" ; Remove Quick Launch Shortcut
  
  DeleteRegKey HKCR "*\shell\ProgramLauncher" ; Remove files context menu item
SectionEnd

; Uninstaller Functions

Function un.onInit
    MessageBox MB_YESNO "This will uninstall ProgramLauncher. Continue?" IDYES NoAbort
      Abort ; causes uninstaller to quit.
    NoAbort:
    SetShellVarContext all
    SetAutoClose true
FunctionEnd

Function un.onUninstFailed
    MessageBox MB_OK "Uninstall Cancelled"
FunctionEnd

Function un.onUninstSuccess
    MessageBox MB_OK "Uninstall Completed"
FunctionEnd

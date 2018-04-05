; ProgramLauncher Installer NSIS Script
; get NSIS at http://nsis.sourceforge.net/Download
; As a program that all Power PC users should have, Notepad++ is recommended to edit this file

Icon "My Project\1458111143_open.ico"
Caption "ProgramLauncher Installer"
Name "ProgramLauncher"
XPStyle on
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

Section "Set as default HTTP and HTTPS Protocol handler"
  WriteRegStr HKCR "http\shell\open\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
  WriteRegStr HKCR "https\shell\open\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
  WriteRegStr HKLM "SOFTWARE\Classes\http\shell\open\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
  WriteRegStr HKLM "SOFTWARE\Classes\https\shell\open\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
  WriteRegStr HKCU "Software\Classes\http\shell\open\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
  WriteRegStr HKCU "Software\Classes\https\shell\open\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
  
  ; Actual default browser (WinVista and up), thanks to
  ; https://newoldthing.wordpress.com/2007/03/23/how-does-your-browsers-know-that-its-not-the-default-browser/
  WriteRegStr HKCR "ProgramLauncherURL" "" "ProgramLauncher URL"
  WriteRegStr HKCR "ProgramLauncherURL" "FriendlyTypeName" "ProgramLauncher URL"
  WriteRegStr HKCR "ProgramLauncherURL" "URL Protocol" ""
    WriteRegStr HKCR "ProgramLauncherURL\DefaultIcon" "" "$INSTDIR\ProgramLauncher.exe"
    WriteRegStr HKCR "ProgramLauncherURL\shell" "" "open"
      WriteRegStr HKCR "ProgramLauncherURL\shell\open\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
      WriteRegStr HKCR "ProgramLauncherURL\shell\open\ddeexec" "" ""
  ; Now we have a custom ProgID, set it as default
  WriteRegStr HKCU "Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice" "Progid" "ProgramLauncherURL"
  WriteRegStr HKCU "Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice" "Progid" "ProgramLauncherURL"
  
  ; Actual default browser (Win10), thanks to
  ; https://superuser.com/a/1310912/244547
  WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe" "" "Program Launcher"
    WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities" "ApplicationIcon" "$INSTDIR\ProgramLauncher.exe"
    WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities" "ApplicationName" "Program Launcher"
    WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities" "ApplicationDescription" "A program that you start with an argument, that asks you which program you want to use"
    
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\FileAssociations" ".htm" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\FileAssociations" ".html" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\FileAssociations" ".xhtml" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\FileAssociations" ".xht" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\FileAssociations" ".shtml" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\FileAssociations" ".xml" "ProgramLauncherURL"
      
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\StartMenu" "StartMenuInternet" "ProgramLauncher.exe"
      
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\URLAssociations" "ftp" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\URLAssociations" "http" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\URLAssociations" "https" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\URLAssociations" "mailto" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\URLAssociations" "ldap" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\URLAssociations" "mms" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\URLAssociations" "read" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\URLAssociations" "res" "ProgramLauncherURL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities\URLAssociations" "tel" "ProgramLauncherURL"
      
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\DefaultIcon" "" "$INSTDIR\ProgramLauncher.exe"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\shell\open\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\""
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\shell\properties" "" "Edit ProgramLauncher Programs"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe\shell\properties\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\""
  ; Now we have a custom Capability class, add it to the registered application list
  WriteRegStr HKLM "SOFTWARE\RegisteredApplications" "ProgramLauncher" "Software\Clients\StartMenuInternet\ProgramLauncher.exe\Capabilities"
SectionEnd

SubSection "Context menu entry"
  Section "Add to context menu for all files"
    WriteRegStr HKCR "*\shell\ProgramLauncher" "" "ProgramLauncher..."
    WriteRegStr HKCR "*\shell\ProgramLauncher" "Icon" "$INSTDIR\ProgramLauncher.exe"
      WriteRegStr HKCR "*\shell\ProgramLauncher\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
  SectionEnd
  Section "Add to context menu for .url files"
    WriteRegStr HKCR "IE.AssocFile.URL\shell\ProgramLauncher" "" "ProgramLauncher..."
    WriteRegStr HKCR "IE.AssocFile.URL\shell\ProgramLauncher" "Icon" "$INSTDIR\ProgramLauncher.exe"
      WriteRegStr HKCR "IE.AssocFile.URL\shell\ProgramLauncher\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
  SectionEnd
  Section "Add to context menu for folders"
    WriteRegStr HKCR "Folder\shell\ProgramLauncher" "" "ProgramLauncher..."
    WriteRegStr HKCR "Folder\shell\ProgramLauncher" "Icon" "$INSTDIR\ProgramLauncher.exe"
      WriteRegStr HKCR "Folder\shell\ProgramLauncher\command" "" "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
  SectionEnd
SubSectionEnd

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

!include LogicLib.nsh ; For ${IF} logic
Section "Uninstall"
  Delete "$INSTDIR\ProgramLauncher-Uninst.exe" ; Remove Application Files
  Delete "$INSTDIR\ProgramLauncher.exe"
  RMDir "$INSTDIR"
  
  Delete "$SMPROGRAMS\WalkmanOSS\ProgramLauncher.lnk" ; Remove Start Menu Shortcuts & Folder
  Delete "$SMPROGRAMS\WalkmanOSS\Uninstall ProgramLauncher.lnk"
  RMDir "$SMPROGRAMS\WalkmanOSS"
  
  Delete "$DESKTOP\ProgramLauncher.lnk"     ; Remove Desktop      Shortcut
  Delete "$QUICKLAUNCH\ProgramLauncher.lnk" ; Remove Quick Launch Shortcut
  
  ; Remove HTTP/HTTPS handlers if they are still set to ProgramLauncher:
  ReadRegStr $0 HKCR "http\shell\open\command" ""
  ${IF} $0 == "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
    WriteRegStr HKCR "http\shell\open\command" "" ""
  ${ENDIF}
  
  ReadRegStr $0 HKCR "https\shell\open\command" ""
  ${IF} $0 == "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
    WriteRegStr HKCR "https\shell\open\command" "" ""
  ${ENDIF}
  
  ReadRegStr $0 HKLM "SOFTWARE\Classes\http\shell\open\command" ""
  ${IF} $0 == "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
    WriteRegStr HKLM "SOFTWARE\Classes\http\shell\open\command" "" ""
  ${ENDIF}
  
  ReadRegStr $0 HKLM "SOFTWARE\Classes\https\shell\open\command" ""
  ${IF} $0 == "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
    WriteRegStr HKLM "SOFTWARE\Classes\https\shell\open\command" "" ""
  ${ENDIF}
  
  ReadRegStr $0 HKCU "Software\Classes\http\shell\open\command" ""
  ${IF} $0 == "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
    WriteRegStr HKCU "Software\Classes\http\shell\open\command" "" ""
  ${ENDIF}
  
  ReadRegStr $0 HKCU "Software\Classes\https\shell\open\command" ""
  ${IF} $0 == "$\"$INSTDIR\ProgramLauncher.exe$\" $\"%1$\""
    WriteRegStr HKCU "Software\Classes\https\shell\open\command" "" ""
  ${ENDIF}
  
  ReadRegStr $0 HKCU "Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice" "Progid"
  ${IF} $0 == "ProgramLauncherURL"
    WriteRegStr HKCU "Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice" "Progid" ""
  ${ENDIF}
  
  ReadRegStr $0 HKCU "Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice" "Progid"
  ${IF} $0 == "ProgramLauncherURL"
    WriteRegStr HKCU "Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice" "Progid" ""
  ${ENDIF}
  
  DeleteRegKey HKCR "ProgramLauncherURL" ; Remove WinVista and up default browser ProgID entry
  DeleteRegKey HKLM "SOFTWARE\Clients\StartMenuInternet\ProgramLauncher.exe" ; Remove Win10 browser capabilities
  DeleteRegValue HKLM "SOFTWARE\RegisteredApplications" "ProgramLauncher" ; unregister program
  
  DeleteRegKey HKCR "*\shell\ProgramLauncher" ; Remove context entry item for all files
  DeleteRegKey HKCR "IE.AssocFile.URL\shell\ProgramLauncher" ; remove context entry for .url files
  DeleteRegKey HKCR "Folder\shell\ProgramLauncher" ; remove context entry for folders
  
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

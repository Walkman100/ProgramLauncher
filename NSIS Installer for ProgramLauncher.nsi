; ProgramLauncher Installer NSIS Script
; get NSIS at http://nsis.sourceforge.net/Download
; As a program that all Power PC users should have, Notepad++ is recommended to edit this file

!define ProgramName "ProgramLauncher"
Icon "My Project\1458111143_open.ico"

Name "${ProgramName}"
Caption "${ProgramName} Installer"
XPStyle on
ShowInstDetails show
AutoCloseWindow true

LicenseBkColor /windows
LicenseData "LICENSE.md"
LicenseForceSelection checkbox "I have read and understand this notice"
LicenseText "Please read the notice below before installing ${ProgramName}. If you understand the notice, click the checkbox below and click Next."

InstallDir $PROGRAMFILES\WalkmanOSS
OutFile "bin\Release\${ProgramName}-Installer.exe"

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
  File "bin\Release\${ProgramName}.exe"
  WriteUninstaller "${ProgramName}-Uninst.exe"
SectionEnd

Section "Start Menu Shortcuts"
  CreateDirectory "$SMPROGRAMS\WalkmanOSS"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\${ProgramName}.lnk" "$INSTDIR\${ProgramName}.exe" "" "$INSTDIR\${ProgramName}.exe" "" "" "" "${ProgramName}"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\Uninstall ${ProgramName}.lnk" "$INSTDIR\${ProgramName}-Uninst.exe" "" "" "" "" "" "Uninstall ${ProgramName}"
  ;Syntax for CreateShortCut: link.lnk target.file [parameters [icon.file [icon_index_number [start_options [keyboard_shortcut [description]]]]]]
SectionEnd

Section "Desktop Shortcut"
  CreateShortCut "$DESKTOP\${ProgramName}.lnk" "$INSTDIR\${ProgramName}.exe" "" "$INSTDIR\${ProgramName}.exe" "" "" "" "${ProgramName}"
SectionEnd

Section "Quick Launch Shortcut"
  CreateShortCut "$QUICKLAUNCH\${ProgramName}.lnk" "$INSTDIR\${ProgramName}.exe" "" "$INSTDIR\${ProgramName}.exe" "" "" "" "${ProgramName}"
SectionEnd

Section "Set as default HTTP and HTTPS Protocol handler"
  WriteRegStr HKCR "http\shell\open\command" "" "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
  WriteRegStr HKCR "https\shell\open\command" "" "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
  WriteRegStr HKLM "SOFTWARE\Classes\http\shell\open\command" "" "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
  WriteRegStr HKLM "SOFTWARE\Classes\https\shell\open\command" "" "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
  WriteRegStr HKCU "Software\Classes\http\shell\open\command" "" "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
  WriteRegStr HKCU "Software\Classes\https\shell\open\command" "" "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
  
  ; Actual default browser (WinVista and up), thanks to
  ; https://newoldthing.wordpress.com/2007/03/23/how-does-your-browsers-know-that-its-not-the-default-browser/
  WriteRegStr HKCR "${ProgramName}URL" "" "${ProgramName} URL"
  WriteRegStr HKCR "${ProgramName}URL" "FriendlyTypeName" "${ProgramName} URL"
  WriteRegStr HKCR "${ProgramName}URL" "URL Protocol" ""
    WriteRegStr HKCR "${ProgramName}URL\DefaultIcon" "" "$INSTDIR\${ProgramName}.exe"
    WriteRegStr HKCR "${ProgramName}URL\shell" "" "open"
      WriteRegStr HKCR "${ProgramName}URL\shell\open\command" "" "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
      WriteRegStr HKCR "${ProgramName}URL\shell\open\ddeexec" "" ""
  ; Now we have a custom ProgID, set it as default
  WriteRegStr HKCU "Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice" "Progid" "${ProgramName}URL"
  WriteRegStr HKCU "Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice" "Progid" "${ProgramName}URL"
  
  ; Actual default browser (Win10), thanks to
  ; https://superuser.com/a/1310912/244547
  WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe" "" "Program Launcher"
    WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities" "ApplicationIcon" "$INSTDIR\${ProgramName}.exe"
    WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities" "ApplicationName" "Program Launcher"
    WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities" "ApplicationDescription" "A program that you start with an argument, that asks you which program you want to use"
    
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\FileAssociations" ".htm" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\FileAssociations" ".html" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\FileAssociations" ".xhtml" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\FileAssociations" ".xht" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\FileAssociations" ".shtml" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\FileAssociations" ".xml" "${ProgramName}URL"
      
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\StartMenu" "StartMenuInternet" "${ProgramName}.exe"
      
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\URLAssociations" "ftp" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\URLAssociations" "http" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\URLAssociations" "https" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\URLAssociations" "mailto" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\URLAssociations" "ldap" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\URLAssociations" "mms" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\URLAssociations" "read" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\URLAssociations" "res" "${ProgramName}URL"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities\URLAssociations" "tel" "${ProgramName}URL"
      
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\DefaultIcon" "" "$INSTDIR\${ProgramName}.exe"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\shell\open\command" "" "$\"$INSTDIR\${ProgramName}.exe$\""
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\shell\properties" "" "Edit ${ProgramName} Programs"
      WriteRegStr HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe\shell\properties\command" "" "$\"$INSTDIR\${ProgramName}.exe$\""
  ; Now we have a custom Capability class, add it to the registered application list
  WriteRegStr HKLM "SOFTWARE\RegisteredApplications" "${ProgramName}" "Software\Clients\StartMenuInternet\${ProgramName}.exe\Capabilities"
SectionEnd

SubSection "Context menu entry"
  Section "Add to context menu for all files"
    WriteRegStr HKCR "*\shell\${ProgramName}" "" "${ProgramName}..."
    WriteRegStr HKCR "*\shell\${ProgramName}" "Icon" "$INSTDIR\${ProgramName}.exe"
      WriteRegStr HKCR "*\shell\${ProgramName}\command" "" "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
  SectionEnd
  Section "Add to context menu for .url files"
    WriteRegStr HKCR "IE.AssocFile.URL\shell\${ProgramName}" "" "${ProgramName}..."
    WriteRegStr HKCR "IE.AssocFile.URL\shell\${ProgramName}" "Icon" "$INSTDIR\${ProgramName}.exe"
      WriteRegStr HKCR "IE.AssocFile.URL\shell\${ProgramName}\command" "" "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
  SectionEnd
  Section "Add to context menu for folders"
    WriteRegStr HKCR "Folder\shell\${ProgramName}" "" "${ProgramName}..."
    WriteRegStr HKCR "Folder\shell\${ProgramName}" "Icon" "$INSTDIR\${ProgramName}.exe"
      WriteRegStr HKCR "Folder\shell\${ProgramName}\command" "" "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
  SectionEnd
SubSectionEnd

; Functions

Function .onInit
  MessageBox MB_YESNO "This will install ${ProgramName}. Do you wish to continue?" IDYES gogogo
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
  Delete "$INSTDIR\${ProgramName}-Uninst.exe" ; Remove Application Files
  Delete "$INSTDIR\${ProgramName}.exe"
  RMDir "$INSTDIR"
  
  Delete "$SMPROGRAMS\WalkmanOSS\${ProgramName}.lnk" ; Remove Start Menu Shortcuts & Folder
  Delete "$SMPROGRAMS\WalkmanOSS\Uninstall ${ProgramName}.lnk"
  RMDir "$SMPROGRAMS\WalkmanOSS"
  
  Delete "$DESKTOP\${ProgramName}.lnk"     ; Remove Desktop      Shortcut
  Delete "$QUICKLAUNCH\${ProgramName}.lnk" ; Remove Quick Launch Shortcut
  
  ; Remove HTTP/HTTPS handlers if they are still set to ${ProgramName}:
  ReadRegStr $0 HKCR "http\shell\open\command" ""
  ${IF} $0 == "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
    WriteRegStr HKCR "http\shell\open\command" "" ""
  ${ENDIF}
  
  ReadRegStr $0 HKCR "https\shell\open\command" ""
  ${IF} $0 == "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
    WriteRegStr HKCR "https\shell\open\command" "" ""
  ${ENDIF}
  
  ReadRegStr $0 HKLM "SOFTWARE\Classes\http\shell\open\command" ""
  ${IF} $0 == "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
    WriteRegStr HKLM "SOFTWARE\Classes\http\shell\open\command" "" ""
  ${ENDIF}
  
  ReadRegStr $0 HKLM "SOFTWARE\Classes\https\shell\open\command" ""
  ${IF} $0 == "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
    WriteRegStr HKLM "SOFTWARE\Classes\https\shell\open\command" "" ""
  ${ENDIF}
  
  ReadRegStr $0 HKCU "Software\Classes\http\shell\open\command" ""
  ${IF} $0 == "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
    WriteRegStr HKCU "Software\Classes\http\shell\open\command" "" ""
  ${ENDIF}
  
  ReadRegStr $0 HKCU "Software\Classes\https\shell\open\command" ""
  ${IF} $0 == "$\"$INSTDIR\${ProgramName}.exe$\" $\"%1$\""
    WriteRegStr HKCU "Software\Classes\https\shell\open\command" "" ""
  ${ENDIF}
  
  ReadRegStr $0 HKCU "Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice" "Progid"
  ${IF} $0 == "${ProgramName}URL"
    WriteRegStr HKCU "Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice" "Progid" ""
  ${ENDIF}
  
  ReadRegStr $0 HKCU "Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice" "Progid"
  ${IF} $0 == "${ProgramName}URL"
    WriteRegStr HKCU "Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice" "Progid" ""
  ${ENDIF}
  
  DeleteRegKey HKCR "${ProgramName}URL" ; Remove WinVista and up default browser ProgID entry
  DeleteRegKey HKLM "SOFTWARE\Clients\StartMenuInternet\${ProgramName}.exe" ; Remove Win10 browser capabilities
  DeleteRegValue HKLM "SOFTWARE\RegisteredApplications" "${ProgramName}" ; unregister program
  
  DeleteRegKey HKCR "*\shell\${ProgramName}" ; Remove context entry item for all files
  DeleteRegKey HKCR "IE.AssocFile.URL\shell\${ProgramName}" ; remove context entry for .url files
  DeleteRegKey HKCR "Folder\shell\${ProgramName}" ; remove context entry for folders
  
SectionEnd

; Uninstaller Functions

Function un.onInit
    MessageBox MB_YESNO "This will uninstall ${ProgramName}. Continue?" IDYES NoAbort
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

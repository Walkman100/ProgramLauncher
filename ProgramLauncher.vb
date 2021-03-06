Imports System.Linq
Imports System.Runtime.CompilerServices
Public Class ProgramLauncher
    
    Dim isProgramEditor As Boolean
    Dim configFileName As String = "ProgramLauncher.xml"
    Dim configFilePath As String = ""
    Dim fullArgument As String = ""
    
    Public Sub New()
        If My.Application.CommandLineArgs.Count = 0 Then
            isProgramEditor = True
            InitializeComponent()
            
            openFileDialogBrowse.InitialDirectory = Environment.GetEnvironmentVariable("ProgramFiles")
            
            If WalkmanLib.IsAdmin Then _
                Me.Text = "[Admin] Edit ProgramLauncher Programs" Else _
                Me.Text = "Edit ProgramLauncher Programs"
        Else
            isProgramEditor = False
            InitializeProgramSelectorComponents()
            
            'get CommandLineArgs and apply/run them
            For Each s As String In My.Application.CommandLineArgs
                fullArgument &= s & " "
            Next
            fullArgument = fullArgument.Remove(fullArgument.Length - 1) ' to get rid of the extra space at the end
            lblInstructions.Text = "Select a program to open """ & fullArgument & """ with:"
            
            If WalkmanLib.IsAdmin Then _
                Me.Text = "[Admin] Select a program to open """ & fullArgument & """ with:" Else _
                Me.Text = "Select a program to open """ & fullArgument & """ with:"
        End If
        lstPrograms.DoubleBuffered(True)
    End Sub
    
    Private Sub LoadProgramLauncher() Handles Me.Load
        lblVersion.Text = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build
        If Environment.GetEnvironmentVariable("OS") = "Windows_NT" Then
            If Not       Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS")) Then
                Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS"))
            End If
            configFilePath =              Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS", configFileName)
        Else
            If Not       Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS")) Then
                Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS"))
            End If
            configFilePath =              Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS", configFileName)
        End If
        
        If       File.Exists(Path.Combine(Application.StartupPath, configFileName)) Then
            configFilePath = Path.Combine(Application.StartupPath, configFileName)
        ElseIf File.Exists(configFileName) Then
            configFilePath = (New FileInfo(configFileName)).FullName
        End If
        
        If File.Exists(configFilePath) Then
            ReadConfig(configFilePath)
        Else
            LoadInitialList()
        End If
        
        CheckButtons
    End Sub
    
    Private Sub AddItem() Handles btnAdd.Click
        Dim tmpListViewItem As New ListViewItem(New String() {"Notepad", "notepad", """{0}"""})
        lstPrograms.SelectedItems.Clear() ' deselect existing items
        lstPrograms.Items.Add(tmpListViewItem).Selected = True
        tmpListViewItem.Focused = True
        
        Browse()
        CheckButtons(True)
    End Sub
    
    Private Sub RemoveItem() Handles btnRemove.Click
        If lstPrograms.SelectedItems.Count > 1 Then
            For Each item As ListViewItem In lstPrograms.SelectedItems
                item.Remove()
            Next
        Else
            lstPrograms.SelectedItems(0).Remove()
        End If
        CheckButtons(True)
    End Sub
    
    Private Sub btnMoveUp_Click() Handles btnMoveUp.Click
        Try
            If lstPrograms.SelectedItems.Count > 0 Then
                lstPrograms.Sorting = SortOrder.None
                lstPrograms.BeginUpdate()
                Dim totalItems As Integer = lstPrograms.Items.Count
                
                For Each selectedItem As ListViewItem In lstPrograms.SelectedItems
                    Dim itemIndex As Integer = selectedItem.Index
                    If itemIndex = 0 Then
                        lstPrograms.Items.Remove(selectedItem)
                        lstPrograms.Items.Insert(totalItems - 1, selectedItem)
                    Else
                        lstPrograms.Items.Remove(selectedItem)
                        lstPrograms.Items.Insert(itemIndex - 1, selectedItem)
                    End If
                Next
                
                lstPrograms.EndUpdate()
                CheckButtons(True)
            Else
                btnMoveUp.Enabled = False
            End If
        Catch ex As Exception
            MsgBox("There was an error moving the item: " & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    
    Private Sub btnMoveDown_Click() Handles btnMoveDown.Click
        Try
            If lstPrograms.SelectedItems.Count > 0 Then
                lstPrograms.Sorting = SortOrder.None
                lstPrograms.BeginUpdate()
                Dim totalItems As Integer = lstPrograms.Items.Count
                
                                        ' VB.Net declares arrays Index-based... (0 = 1 item)
                Dim selectedItemArray(lstPrograms.SelectedItems.Count - 1) As ListViewItem
                lstPrograms.SelectedItems.CopyTo(selectedItemArray, 0)
                
                For Each selectedItem As ListViewItem In selectedItemArray.Reverse()
                    Dim itemIndex As Integer = selectedItem.Index
                    If itemIndex = totalItems - 1 Then
                        lstPrograms.Items.Remove(selectedItem)
                        lstPrograms.Items.Insert(0, selectedItem)
                    Else
                        lstPrograms.Items.Remove(selectedItem)
                        lstPrograms.Items.Insert(itemIndex + 1, selectedItem)
                    End If
                Next
                
                lstPrograms.EndUpdate()
                CheckButtons(True)
            Else
                btnMoveDown.Enabled = False
            End If
        Catch ex As Exception
            MsgBox("There was an error moving the item: " & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    
    Private Sub btnEdit_Click() Handles btnEdit.Click
        If isProgramEditor Then
            Dim inputBoxText As String
            For Each item As ListViewItem In lstPrograms.SelectedItems
                inputBoxText = InputBox("Enter the arguments to start """ & item.SubItems.Item(1).Text & """ with:", "", item.SubItems.Item(2).Text)
                If inputBoxText <> "" Then item.SubItems.Item(2).Text = inputBoxText
            Next
            WriteConfig(configFilePath)
        Else
            Shell(Path.Combine(Application.StartupPath, Process.GetCurrentProcess.ProcessName & ".exe"), AppWinStyle.NormalFocus, True, 100000)
            lstPrograms.Items.Clear()
            ReadConfig(configFilePath)
        End If
    End Sub
    
    Private Sub Browse() Handles btnBrowse.Click
        If lstPrograms.SelectedItems.Count > 1 Then
            For Each item As ListViewItem In lstPrograms.SelectedItems
                openFileDialogBrowse.Title = "Select file to replace """ & item.SubItems.Item(1).Text & """ with:"
                If item.SubItems.Item(1).Text.Contains(Path.DirectorySeparatorChar) Then
                    openFileDialogBrowse.InitialDirectory = Path.GetDirectoryName(item.SubItems.Item(1).Text)
                Else
                    openFileDialogBrowse.InitialDirectory = Environment.GetEnvironmentVariable("ProgramFiles")
                End If
                If openFileDialogBrowse.ShowDialog() = DialogResult.OK Then
                    item.SubItems.Item(1).Text = openFileDialogBrowse.FileName
                End If
            Next
            WriteConfig(configFilePath)
        Else
            Dim selectedItemPath = lstPrograms.SelectedItems(0).SubItems.Item(1).Text
            openFileDialogBrowse.Title = "Select file to replace """ & selectedItemPath & """ with:"
            If selectedItemPath.Contains(Path.DirectorySeparatorChar) Then
                openFileDialogBrowse.InitialDirectory = Path.GetDirectoryName(selectedItemPath)
            Else
                openFileDialogBrowse.InitialDirectory = Environment.GetEnvironmentVariable("ProgramFiles")
            End If
            If openFileDialogBrowse.ShowDialog() = DialogResult.OK Then
                lstPrograms.SelectedItems(0).SubItems.Item(1).Text = openFileDialogBrowse.FileName
                WriteConfig(configFilePath)
            End If
        End If
    End Sub
    
    Private Sub RunSelectedEntry(sender As Object, e As EventArgs) Handles btnRun.Click, btnOpenOnly.Click
        If isProgramEditor Then
            For Each item As ListViewItem In lstPrograms.SelectedItems
                RunProgram(item)
            Next
        Else
            RunProgram(lstPrograms.SelectedItems(0), fullArgument)
            If sender.Equals(btnRun) Then CloseProgramLauncher()
        End If
    End Sub
    
    Sub lstPrograms_ItemActivate(sender As Object, e As EventArgs) Handles lstPrograms.ItemActivate
        If lstPrograms.SelectedItems.Count > 0 Then
            RunSelectedEntry(btnRun, e)
        End If
    End Sub
    
    Private Sub RunProgram(entry As ListViewItem, Optional argument As String = "")
        Dim programPath As String = entry.SubItems.Item(1).Text
        Dim programArgs As String = entry.SubItems.Item(2).Text
        programPath = Environment.ExpandEnvironmentVariables(programPath)
        programArgs = Environment.ExpandEnvironmentVariables(programArgs)
        If programArgs.Contains("{0}") Then
            programArgs = String.Format(programArgs, argument)
        Else
            programArgs = String.Concat(programArgs, argument)
        End If
        
        If programPath = "Copy to Clipboard" Then
            WalkmanLib.SafeSetText(programArgs)
        Else
            Try
                If programPath.EndsWith(":") Then ' launching a protocol is a bit different
                    Process.Start(programPath & programArgs)
                ElseIf programPath.StartsWith("elevate ") Or programPath.StartsWith("sudo ") Or programPath.StartsWith("runas ") Then
                    If programPath.StartsWith("elevate ") Then programPath = programPath.Substring(8)
                    If programPath.StartsWith("sudo ")    Then programPath = programPath.Substring(5)
                    If programPath.StartsWith("runas ")   Then programPath = programPath.Substring(6)
                    
                    WalkmanLib.RunAsAdmin(programPath, programArgs)
                Else
                    Process.Start(programPath, programArgs)
                End If
            Catch ex As Exception
                MsgBox("There was an error running the program """ & programPath & """ with """ & programArgs & """ args:" &
                    vbNewLine & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub
    
    Private Sub CloseProgramLauncher() Handles btnEnd.Click
        If isProgramEditor Then WriteConfig(configFilePath)
        Application.Exit()
    End Sub
    
    Private Sub lstPrograms_ItemSelectionChanged() Handles lstPrograms.ItemSelectionChanged
        CheckButtons()
    End Sub
    Private Sub lstPrograms_ListDataEdited() Handles lstPrograms.AfterLabelEdit, lstPrograms.ColumnReordered
        CheckButtons(True)
    End Sub
    
    Private Sub CheckButtons(Optional writeToConfig As Boolean = False)
        If lstPrograms.SelectedItems.Count = 0 Then
            If isProgramEditor Then
                btnRemove.Enabled = False
                btnMoveUp.Enabled = False
                btnMoveDown.Enabled = False
                btnEdit.Enabled = False
                btnBrowse.Enabled = False
                btnRun.Enabled = False
            Else
                btnRun.Enabled = False
                btnOpenOnly.Enabled = False
            End If
        Else
            If isProgramEditor Then
                btnRemove.Enabled = True
                btnMoveDown.Enabled = True
                btnMoveUp.Enabled = True
                btnEdit.Enabled = True
                btnBrowse.Enabled = True
                btnRun.Enabled = True
            Else
                btnRun.Enabled = True
                btnOpenOnly.Enabled = True
            End If
        End If
        If isProgramEditor And writeToConfig Then WriteConfig(configFilePath)
    End Sub
    
    Private Sub lstPrograms_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstPrograms.ColumnClick
        If e.Column = 0 Then
            lstPrograms.Sorting = IIf(lstPrograms.Sorting = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending)
        Else
            'lstPrograms.Sort(e.Column)
        End If
    End Sub
    
    Private Sub ResizeByHeader(sender As Object, e As EventArgs) Handles contextCommandsResizeNameHeader.Click, contextCommandsResizePathHeader.Click, contextCommandsResizeArgsHeader.Click
        lstPrograms.AutoResizeColumn(sender.Tag, ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub
    
    Private Sub ResizeByContent(sender As Object, e As EventArgs) Handles contextCommandsResizeNameContent.Click, contextCommandsResizePathContent.Click, contextCommandsResizeArgsContent.Click
        lstPrograms.AutoResizeColumn(sender.Tag, ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub
    
    Private Sub ResizeAllByHeader() Handles contextCommandsResizeAllHeader.Click
        lstPrograms.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub
    
    Private Sub ResizeAllByContent() Handles contextCommandsResizeAllContent.Click
        lstPrograms.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub
    
    Private Sub lstPrograms_DragEnter(sender As Object, e As DragEventArgs) Handles lstPrograms.DragEnter
        If e.Data.GetDataPresent(DataFormats.Text) Or e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    
    Private Sub lstPrograms_DragDrop(sender As Object, e As DragEventArgs) Handles lstPrograms.DragDrop
        If e.Data.GetDataPresent(DataFormats.Text) Then
            Dim tmpListViewItem As New ListViewItem(New String() {"", e.Data.GetData(DataFormats.Text).ToString, " "})
            lstPrograms.SelectedItems.Clear() ' deselect existing items
            lstPrograms.Items.Add(tmpListViewItem).Selected = True
            tmpListViewItem.Focused = True
        ElseIf e.Data.GetDataPresent(DataFormats.FileDrop) AndAlso TypeOf(e.Data.GetData(DataFormats.FileDrop)) Is String() Then
            lstPrograms.SelectedItems.Clear() ' deselect existing items
            For Each filePath In DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
                Dim tmpListViewItem As New ListViewItem(New String() {"", filePath, " "})
                lstPrograms.Items.Add(tmpListViewItem).Selected = True
                tmpListViewItem.Focused = True
            Next
        End If
    End Sub
    
    Private Sub LoadInitialList()
        Dim item1 = New ListViewItem({"Open in Explorer", "%WinDir%\explorer.exe", """{0}"""})
        Dim item2 = New ListViewItem({"Show in Explorer", "%WinDir%\explorer.exe", "/select, ""{0}"""})
        Dim item3 = New ListViewItem({"Notepad", "%WinDir%\notepad.exe", """{0}"""})
        Dim item4 = New ListViewItem({"Notepad (Admin)", "elevate %WinDir%\notepad.exe", """{0}"""})
        Dim item5 = New ListViewItem({"Open CMD at path", "%WinDir%\System32\cmd.exe", "/k cd /d ""{0}"""})
        Dim item6 = New ListViewItem({"Copy to Clipboard", "Copy to Clipboard", "{0}"})
        Dim item7 = New ListViewItem({"Microsoft Edge", "microsoft-edge:", "{0}"})
        Dim item8 = New ListViewItem({"BasicBrowser", "%ProgramFiles%\WalkmanOSS\BasicBrowser.exe", """{0}"""})
        Dim item9 = New ListViewItem({"DirectoryImage", "%ProgramFiles%\WalkmanOSS\DirectoryImage.exe", """{0}"""})
        Dim item10 = New ListViewItem({"PropertiesDotNet", "%ProgramFiles%\WalkmanOSS\PropertiesDotNet.exe", """{0}"""})
        lstPrograms.Items.AddRange({item1, item2, item3, item4, item5, item6, item7, item8, item9, item10})
        
        'Me.Height = 240 (disabled because the default form size is big enough)
        'colheadName.Width = 
        'colheadPath.Width = 292
        'colheadProgramArgs.Width = 151
    End Sub
    
    Private Sub ReadConfig(path As String)
        Using reader As XmlReader = XmlReader.Create(path)
            Try
                reader.Read()
            Catch ex As XmlException
                Exit Sub
            End Try
            
            Dim elementAttribute As String
            If reader.IsStartElement() AndAlso reader.Name = "ProgramLauncher" Then
                If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "ProgramList" Then
                    While reader.IsStartElement
                        If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "Program" Then
                            Dim tmpListViewItem As New ListViewItem(New String() {"Notepad", "notepad", """{0}"""})
                            
                            elementAttribute = reader("name")
                            If elementAttribute IsNot Nothing Then
                                tmpListViewItem.Text = elementAttribute
                            End If
                            
                            elementAttribute = reader("path")
                            If elementAttribute IsNot Nothing Then
                                tmpListViewItem.SubItems.Item(1).Text = elementAttribute
                            End If
                            
                            elementAttribute = reader("args")
                            If elementAttribute IsNot Nothing Then
                                tmpListViewItem.SubItems.Item(2).Text = elementAttribute
                            End If
                            
                            lstPrograms.Items.Add(tmpListViewItem)
                        End If
                    End While
                End If
                If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "Settings" Then
                    If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "ColumnSettings" Then
                        While reader.IsStartElement
                            If reader.Read AndAlso reader.IsStartElement() Then
                                If reader.Name = "NameColumn" Then
                                    elementAttribute = reader("index")
                                    If elementAttribute IsNot Nothing Then
                                        colheadName.DisplayIndex = elementAttribute
                                    End If
                                    
                                    elementAttribute = reader("width")
                                    If elementAttribute IsNot Nothing Then
                                        colheadName.Width = elementAttribute
                                    End If
                                ElseIf reader.Name = "PathColumn" Then
                                    elementAttribute = reader("index")
                                    If elementAttribute IsNot Nothing Then
                                        colheadPath.DisplayIndex = elementAttribute
                                    End If
                                    
                                    elementAttribute = reader("width")
                                    If elementAttribute IsNot Nothing Then
                                        colheadPath.Width = elementAttribute
                                    End If
                                ElseIf reader.Name = "ArgColumn" Then
                                    elementAttribute = reader("index")
                                    If elementAttribute IsNot Nothing Then
                                        colheadProgramArgs.DisplayIndex = elementAttribute
                                    End If
                                    
                                    elementAttribute = reader("width")
                                    If elementAttribute IsNot Nothing Then
                                        colheadProgramArgs.Width = elementAttribute
                                    End If
                                End If
                            End If
                        End While
                    End If
                    If reader.Read AndAlso reader.IsStartElement AndAlso reader.Name = "WindowSize" Then
                        elementAttribute = reader("width")
                        If elementAttribute IsNot Nothing Then
                            If isProgramEditor Then
                                Me.Width = elementAttribute
                            Else
                                Me.Width = elementAttribute - 67
                            End If
                            Me.Location = New Drawing.Point(My.Computer.Screen.WorkingArea.Width / 2 - Me.Width / 2, Me.Location.Y)
                        End If
                        
                        elementAttribute = reader("height")
                        If elementAttribute IsNot Nothing Then
                            If isProgramEditor Then
                                Me.Height = elementAttribute
                            Else
                                Me.Height = elementAttribute + 29
                            End If
                            Me.Location = New Drawing.Point(Me.Location.X, My.Computer.Screen.WorkingArea.Height / 2 - Me.Height / 2)
                        End If
                    End If
                End If
            End If
        End Using
    End Sub
    
    Private Sub WriteConfig(path As String)
        Using writer As XmlWriter = XmlWriter.Create(path, New XmlWriterSettings With {.Indent = True})
            writer.WriteStartDocument()
            writer.WriteStartElement("ProgramLauncher")
            
            writer.WriteStartElement("ProgramList")
            For Each item In lstPrograms.Items
                writer.WriteStartElement("Program")
                writer.WriteAttributeString("name", item.Text)
                writer.WriteAttributeString("path", item.SubItems.Item(1).Text)
                writer.WriteAttributeString("args", item.SubItems.Item(2).Text)
                writer.WriteEndElement()
            Next
            writer.WriteEndElement()
            
            writer.WriteStartElement("Settings")
                writer.WriteStartElement("ColumnSettings")
                    writer.WriteStartElement("NameColumn")
                        writer.WriteAttributeString("index", colheadName.DisplayIndex)
                        writer.WriteAttributeString("width", colheadName.Width)
                    writer.WriteEndElement()
                    writer.WriteStartElement("PathColumn")
                        writer.WriteAttributeString("index", colheadPath.DisplayIndex)
                        writer.WriteAttributeString("width", colheadPath.Width)
                    writer.WriteEndElement()
                    writer.WriteStartElement("ArgColumn")
                        writer.WriteAttributeString("index", colheadProgramArgs.DisplayIndex)
                        writer.WriteAttributeString("width", colheadProgramArgs.Width)
                    writer.WriteEndElement()
                writer.WriteEndElement()
                writer.WriteStartElement("WindowSize")
                    writer.WriteAttributeString("width", Me.Width)
                    writer.WriteAttributeString("height", Me.Height)
                writer.WriteEndElement()
            writer.WriteEndElement()
            
            writer.WriteEndElement()
            writer.WriteEndDocument()
        End Using
    End Sub
End Class

Module ControlExtensions ' thanks to https://stackoverflow.com/a/15268338/2999220
    <Extension()>
    Public Sub DoubleBuffered(control As Control, enable As Boolean)
        Dim doubleBufferPropertyInfo = control.[GetType]().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
        doubleBufferPropertyInfo.SetValue(control, enable, Nothing)
    End Sub
End Module
Imports System.Runtime.CompilerServices
Public Class ProgramLauncher
    
    Dim isProgramEditor As Boolean
    Dim configFileName As String = "ProgramLauncher.xml"
    Dim configFilePath As String = Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS", configFileName)
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
        If Not Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS")) Then
            Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS"))
        End If
        lblVersion.Text = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build
        
        If File.Exists(Path.Combine(Application.StartupPath, configFileName)) Then
            configFilePath = Path.Combine(Application.StartupPath, configFileName)
            ReadConfig(configFilePath)
        ElseIf File.Exists(configFileName) Then
            configFilePath = New IO.FileInfo(configFileName).FullName
            ReadConfig(configFilePath)
        ElseIf File.Exists(configFilePath) Then
            ReadConfig(configFilePath)
        Else
            LoadInitialList()
        End If
        
        CheckButtons
    End Sub
    
    Private Sub AddItem() Handles btnAdd.Click
        Dim tmpListViewItem As New ListViewItem(New String() {"notepad", """{0}"""})
        lstPrograms.Items.Add(tmpListViewItem).Selected = True
        tmpListViewItem.Focused = True
        
        Browse()
        CheckButtons(True)
    End Sub
    
    Private Sub RemoveItem() Handles btnRemove.Click
        If lstPrograms.SelectedItems.Count > 1 Then
            For Each item As ListViewItem In lstPrograms.SelectedItems
                item.Remove
            Next
        Else
            lstPrograms.SelectedItems(0).Remove
        End If
        CheckButtons(True)
    End Sub
    
    Private Sub btnMoveUp_Click() Handles btnMoveUp.Click
        Try
            If lstPrograms.SelectedItems.Count > 0 Then
                lstPrograms.Sorting = SortOrder.None
                Dim selected As ListViewItem = lstPrograms.SelectedItems(0)
                Dim selectedIndex As Integer = selected.Index
                Dim totalItems As Integer = lstPrograms.Items.Count
                
                lstPrograms.BeginUpdate()
                If selectedIndex = 0 Then
                    lstPrograms.Items.Remove(selected)
                    lstPrograms.Items.Insert(totalItems - 1, selected)
                Else
                    lstPrograms.Items.Remove(selected)
                    lstPrograms.Items.Insert(selectedIndex - 1, selected)
                End If
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
                Dim selected As ListViewItem = lstPrograms.SelectedItems(0)
                Dim selectedIndex As Integer = selected.Index
                Dim totalItems As Integer = lstPrograms.Items.Count
                
                lstPrograms.BeginUpdate()
                If selectedIndex = totalItems - 1 Then
                    lstPrograms.Items.Remove(selected)
                    lstPrograms.Items.Insert(0, selected)
                Else
                    lstPrograms.Items.Remove(selected)
                    lstPrograms.Items.Insert(selectedIndex + 1, selected)
                End If
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
            If lstPrograms.SelectedItems.Count > 1 Then
                For Each item As ListViewItem In lstPrograms.SelectedItems
                    inputBoxText = InputBox("Enter the arguments to start """ & item.Text & """ with:", "", item.SubItems.Item(1).Text)
                    If inputBoxText <> "" Then item.SubItems.Item(1).Text = inputBoxText
                Next
            Else
                inputBoxText = InputBox("Enter the arguments to start """ & lstPrograms.SelectedItems(0).Text & """ with:", "", lstPrograms.SelectedItems(0).SubItems.Item(1).Text)
                If inputBoxText <> "" Then lstPrograms.SelectedItems(0).SubItems.Item(1).Text = inputBoxText
            End If
            WriteConfig(configFilePath)
        Else
            Shell(Application.StartupPath & "\" & Process.GetCurrentProcess.ProcessName & ".exe", AppWinStyle.NormalFocus, True, 100000)
            lstPrograms.Items.Clear()
            ReadConfig(configFilePath)
        End If
    End Sub
    
    Private Sub Browse() Handles btnBrowse.Click
        If lstPrograms.SelectedItems.Count > 1 Then
            For Each item As ListViewItem In lstPrograms.SelectedItems
                openFileDialogBrowse.Title = "Select file to replace """ & item.Text & """ with:"
                If item.Text.Contains("\") Then
                    openFileDialogBrowse.InitialDirectory = item.Text.Remove(item.Text.LastIndexOf("\"))
                Else
                    openFileDialogBrowse.InitialDirectory = Environment.GetEnvironmentVariable("ProgramFiles")
                End If
                If openFileDialogBrowse.ShowDialog() = DialogResult.OK Then
                    item.Text = openFileDialogBrowse.FileName
                End If
            Next
            WriteConfig(configFilePath)
        Else
            openFileDialogBrowse.Title = "Select file to replace """ & lstPrograms.SelectedItems(0).Text & """ with:"
            If lstPrograms.SelectedItems(0).Text.Contains("\") Then
                openFileDialogBrowse.InitialDirectory = lstPrograms.SelectedItems(0).Text.Remove(lstPrograms.SelectedItems(0).Text.LastIndexOf("\"))
            Else
                openFileDialogBrowse.InitialDirectory = Environment.GetEnvironmentVariable("ProgramFiles")
            End If
            If openFileDialogBrowse.ShowDialog() = DialogResult.OK Then
                lstPrograms.SelectedItems(0).Text = openFileDialogBrowse.FileName
                WriteConfig(configFilePath)
            End If
        End If
    End Sub
    
    Private Sub RunSelectedEntry(sender As Object, e As EventArgs) Handles btnRun.Click, btnOpenOnly.Click
        If isProgramEditor Then
            If lstPrograms.SelectedItems.Count > 1 Then
                For Each item As ListViewItem In lstPrograms.SelectedItems
                    RunProgram(item)
                Next
            Else
                RunProgram(lstPrograms.SelectedItems(0))
            End If
        Else
            RunProgram(lstPrograms.SelectedItems(0), fullArgument)
            If sender.Equals(btnRun) Then CloseProgramLauncher
        End If
    End Sub
    
    Sub lstPrograms_DoubleClick(sender As Object, e As EventArgs) Handles lstPrograms.DoubleClick
        If lstPrograms.SelectedItems.Count > 0 Then
            RunSelectedEntry(btnRun, e)
        End If
    End Sub
    
    Private Sub RunProgram(entry As ListViewItem, Optional argument As String = "")
        If entry.Text = "Copy to Clipboard" Then
            If entry.SubItems.Item(1).Text.Contains("{0}") Then
                WalkmanLib.SafeSetText(String.Format(entry.SubItems.Item(1).Text, argument))
            Else
                WalkmanLib.SafeSetText(entry.SubItems.Item(1).Text & argument)
            End If
            
            Exit Sub
        End If
        
        Try
            If entry.Text.EndsWith(":") Then ' launching a protocol is a bit different
                If entry.SubItems.Item(1).Text.Contains("{0}") Then
                    Process.Start(entry.Text & String.Format(entry.SubItems.Item(1).Text, argument))
                Else
                    Process.Start(entry.Text &               entry.SubItems.Item(1).Text & argument)
                End If
                
                Exit Sub
            End If
            
            If entry.Text.StartsWith("elevate ") Or entry.Text.StartsWith("sudo ") Or entry.Text.StartsWith("runas ") Then
                Dim programString As String = ""
                If entry.Text.StartsWith("elevate ") Then programString = entry.Text.Substring(8)
                If entry.Text.StartsWith("sudo ") Then programString = entry.Text.Substring(5)
                If entry.Text.StartsWith("runas ") Then programString = entry.Text.Substring(6)
                
                If entry.SubItems.Item(1).Text.Contains("{0}") Then
                    WalkmanLib.RunAsAdmin(programString, String.Format(entry.SubItems.Item(1).Text, argument))
                Else
                    WalkmanLib.RunAsAdmin(programString,               entry.SubItems.Item(1).Text & argument)
                End If
                
                Exit Sub
            End If
            
            If entry.SubItems.Item(1).Text.Contains("{0}") Then
                Process.Start(entry.Text, String.Format(entry.SubItems.Item(1).Text, argument))
            Else
                Process.Start(entry.Text,               entry.SubItems.Item(1).Text & argument)
            End If
        Catch ex As Exception
            Try
                MsgBox("There was an error running the program """ & entry.Text & """ with """ & entry.SubItems.Item(1).Text & """ args!", MsgBoxStyle.Critical)
            Catch ex2 As Exception
                MsgBox("Error finding the data to run the program! Error was:" & vbNewLine & ex2.Message, MsgBoxStyle.Critical)
            End Try
        End Try
    End Sub
    
    Private Sub CloseProgramLauncher() Handles btnEnd.Click
        If isProgramEditor Then WriteConfig(configFilePath)
        Application.Exit()
    End Sub
    
    Private Sub lstPrograms_ItemSelectionChanged() Handles lstPrograms.ItemSelectionChanged
        CheckButtons
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
                btnMoveDown.Enabled = (lstPrograms.SelectedItems(0).Index <> lstPrograms.Items.Count -1)
                btnMoveUp.Enabled = (lstPrograms.SelectedItems(0).Index <> 0)
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
    
    Private Sub ResizeByHeader(sender As Object, e As EventArgs) Handles contextCommandsResizePathHeader.Click, contextCommandsResizeArgsHeader.Click
        lstPrograms.AutoResizeColumn(sender.Tag, ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub
    
    Private Sub ResizeByContent(sender As Object, e As EventArgs) Handles contextCommandsResizePathContent.Click, contextCommandsResizeArgsContent.Click
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
            Dim tmpListViewItem As New ListViewItem(New String() {e.Data.GetData(DataFormats.Text).ToString, " ", "draggedFile"})
            lstPrograms.Items.Add(tmpListViewItem).Selected = True
            tmpListViewItem.Focused = True
        ElseIf e.Data.GetDataPresent(DataFormats.FileDrop)
            For i = 0 To Integer.MaxValue
                If (e.Data.GetData(DataFormats.FileDrop)(i) <> Nothing) Then
                    Dim tmpListViewItem As New ListViewItem(New String() {e.Data.GetData(DataFormats.FileDrop)(i), " ", "draggedFile"})
                    lstPrograms.Items.Add(tmpListViewItem).Selected = True
                    tmpListViewItem.Focused = True
                Else
                    Exit For
                End If
            Next
        End If
    End Sub
    
    Private Sub LoadInitialList()
        Dim item1 = New String() {"C:\Windows\explorer.exe", """{0}"""}
        Dim item2 = New String() {"C:\Windows\explorer.exe", "/select, ""{0}"""}
        Dim item3 = New String() {"C:\Windows\notepad.exe", """{0}"""}
        Dim item4 = New String() {"elevate C:\Windows\notepad.exe", """{0}"""}
        Dim item5 = New String() {"C:\Windows\System32\cmd.exe", "/k cd /d ""{0}"""}
        Dim item6 = New String() {"Copy to Clipboard", """{0}"""}
        Dim item7 = New String() {"microsoft-edge:", "{0}"}
        Dim item8 = New String() {Environment.GetEnvironmentVariable("ProgramFiles") & "\WalkmanOSS\BasicBrowser.exe", """{0}"""}
        Dim item9 = New String() {Environment.GetEnvironmentVariable("ProgramFiles") & "\WalkmanOSS\DirectoryImage.exe", """{0}"""}
        Dim item10 = New String() {Environment.GetEnvironmentVariable("ProgramFiles") & "\WalkmanOSS\PropertiesDotNet.exe", """{0}"""}
        For Each item As String() In {item1, item2, item3, item4, item5, item6, item7, item8, item9, item10}
            lstPrograms.Items.Add(New ListViewItem(item))
        Next
        
        'Me.Height = 240 (disabled because the default form size is big enough)
        colheadPath.Width = 292
        colheadProgramArgs.Width = 151
    End Sub
    
    Private Sub ReadConfig(path As String)
        Dim reader As XmlReader = XmlReader.Create(path)
        Try
            reader.Read()
        Catch ex As XmlException
            reader.Close
            Exit Sub
        End Try
        
        Dim elementAttribute As String
        If reader.IsStartElement() AndAlso reader.Name = "ProgramLauncher" Then
            If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "ProgramList" Then
                While reader.IsStartElement
                    If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "Program" Then
                        Dim tmpListViewItem As New ListViewItem(New String() {"notepad", """{0}"""})
                        
                        elementAttribute = reader("path")
                        If elementAttribute IsNot Nothing Then
                            tmpListViewItem.Text = elementAttribute
                        End If
                        
                        elementAttribute = reader("args")
                        If elementAttribute IsNot Nothing Then
                            tmpListViewItem.SubItems.Item(1).Text = elementAttribute
                        End If
                        
                        lstPrograms.Items.Add(tmpListViewItem)
                    End If
                End While
            End If
            If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "Settings" Then
                If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "ColumnSettings" Then
                    While reader.IsStartElement
                        If reader.Read AndAlso reader.IsStartElement() Then
                            If reader.Name = "PathColumn" Then
                                elementAttribute = reader("index")
                                If elementAttribute IsNot Nothing Then
                                    colheadPath.DisplayIndex = elementAttribute
                                End If
                                
                                elementAttribute = reader("width")
                                If elementAttribute IsNot Nothing Then
                                    colheadPath.Width = elementAttribute
                                End If
                            ElseIf reader.Name = "ArgColumn"
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
                            Me.Width = elementAttribute -67
                        End If
                        Me.Location = New Drawing.Point(My.Computer.Screen.WorkingArea.Width/2 - Me.Width/2, Me.Location.Y)
                    End If
                    
                    elementAttribute = reader("height")
                    If elementAttribute IsNot Nothing Then
                        If isProgramEditor Then
                            Me.Height = elementAttribute
                        Else
                            Me.Height = elementAttribute +29
                        End If
                        Me.Location = New Drawing.Point(Me.Location.X, My.Computer.Screen.WorkingArea.Height/2 - Me.Height/2)
                    End If
                End If
            End If
        End If
        
        reader.Close
    End Sub
    
    Private Sub WriteConfig(path As String)
        Dim XMLwSettings As New XmlWriterSettings()
        XMLwSettings.Indent = True
        Dim writer As XmlWriter = XmlWriter.Create(path, XMLwSettings)
        
        Try
            writer.WriteStartDocument()
            writer.WriteStartElement("ProgramLauncher")
            
            writer.WriteStartElement("ProgramList")
            For Each item In lstPrograms.Items
                writer.WriteStartElement("Program")
                writer.WriteAttributeString("path", item.Text)
                writer.WriteAttributeString("args", item.SubItems.Item(1).Text)
                writer.WriteEndElement()
            Next
            writer.WriteEndElement()
            
            writer.WriteStartElement("Settings")
                writer.WriteStartElement("ColumnSettings")
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
        Finally
            writer.Close
        End Try
    End Sub
End Class

Module ControlExtensions ' thanks to https://stackoverflow.com/a/15268338/2999220
    <Extension()>
    Public Sub DoubleBuffered(control As Control, enable As Boolean)
        Dim doubleBufferPropertyInfo = control.[GetType]().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
        doubleBufferPropertyInfo.SetValue(control, enable, Nothing)
    End Sub
End Module
Public Class ProgramLauncher
    
    Dim isProgramEditor As Boolean
    Dim configFilePath As String = Environment.GetEnvironmentVariable("AppData") & "\WalkmanOSS\ProgramLauncher.xml"
    Dim fullArgument As String = ""
    
    Public Sub New()
        If My.Application.CommandLineArgs.Count = 0 Then
            isProgramEditor = True
            InitializeComponent()
            
            openFileDialogBrowse.InitialDirectory = Environment.GetEnvironmentVariable("ProgramFiles")
        Else
            isProgramEditor = False
            InitializeProgramSelectorComponents()
            
            'get CommandLineArgs and apply/run them
            For Each s As String In My.Application.CommandLineArgs
                fullArgument &= s
            Next
            lblInstructions.Text = "Select a program to open """ & fullArgument & """ with:"
        End If
    End Sub
    
    Private Sub LoadProgramLauncher() Handles Me.Load
        If Not Directory.Exists(Environment.GetEnvironmentVariable("AppData") & "\WalkmanOSS") Then
            Directory.CreateDirectory(Environment.GetEnvironmentVariable("AppData") & "\WalkmanOSS")
        End If
        
        If File.Exists(Application.StartupPath & "\ProgramLauncher.xml") Then
            configFilePath = Application.StartupPath & "\ProgramLauncher.xml"
            ReadConfig(configFilePath)
        ElseIf File.Exists("SteamPlaceholder.xml") Then
            configFilePath = (New IO.FileInfo("ProgramLauncher.xml")).FullName
            ReadConfig(configFilePath)
        ElseIf File.Exists(configFilePath) Then
            ReadConfig(configFilePath)
        End If
        
        CheckButtons
    End Sub
    
    Private Sub AddItem() Handles btnAdd.Click
        Dim tmpListViewItem As New ListViewItem(New String() {"notepad", """{0}"""})
        lstPrograms.FocusedItem = lstPrograms.Items.Add(tmpListViewItem)
        Browse()
        CheckButtons
    End Sub
    
    Private Sub RemoveItem() Handles btnRemove.Click
        If lstPrograms.SelectedItems.Count > 1 Then
            For Each item As ListViewItem In lstPrograms.SelectedItems
                item.Remove
            Next
        Else
            lstPrograms.FocusedItem.Remove
        End If
        CheckButtons
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
                inputBoxText = InputBox("Enter the arguments to start """ & lstPrograms.FocusedItem.Text & """ with:", "", lstPrograms.FocusedItem.SubItems.Item(1).Text)
                If inputBoxText <> "" Then lstPrograms.FocusedItem.SubItems.Item(1).Text = inputBoxText
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
            openFileDialogBrowse.Title = "Select file to replace """ & lstPrograms.FocusedItem.Text & """ with:"
            If lstPrograms.FocusedItem.Text.Contains("\") Then
                openFileDialogBrowse.InitialDirectory = lstPrograms.FocusedItem.Text.Remove(lstPrograms.FocusedItem.Text.LastIndexOf("\"))
            Else
                openFileDialogBrowse.InitialDirectory = Environment.GetEnvironmentVariable("ProgramFiles")
            End If
            If openFileDialogBrowse.ShowDialog() = DialogResult.OK Then
                lstPrograms.FocusedItem.Text = openFileDialogBrowse.FileName
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
                RunProgram(lstPrograms.FocusedItem)
            End If
        Else
            RunProgram(lstPrograms.FocusedItem, fullArgument)
            If sender.Equals(btnRun) Then CloseProgramLauncher
        End If
    End Sub
    
    Sub lstPrograms_DoubleClick() Handles lstPrograms.DoubleClick
        If lstPrograms.SelectedItems.Count > 0 Then
            RunSelectedEntry(btnRun, New System.Windows.Forms.MouseEventArgs(MouseButtons.Left,2,0,0,0))
        End If
    End Sub
    
    Private Sub RunProgram(entry As ListViewItem, Optional argument As String = "")
        If entry.Text = "Copy to Clipboard" Then
            Clipboard.SetText(argument, TextDataFormat.UnicodeText)
            Exit Sub
        End If
        Try
            If entry.SubItems.Item(1).Text.Contains("{0}") Then
                Process.Start(entry.Text, String.Format(entry.SubItems.Item(1).Text, argument))
            Else
                Process.Start(entry.Text, entry.SubItems.Item(1).Text & argument)
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
    
    Private Sub CheckButtons() Handles lstPrograms.Click, lstPrograms.SelectedIndexChanged, lstPrograms.AfterLabelEdit, lstPrograms.ColumnReordered
        If IsNothing(lstPrograms.FocusedItem) Then
            If isProgramEditor Then
                btnRemove.Enabled = False
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
                btnEdit.Enabled = True
                btnBrowse.Enabled = True
                btnRun.Enabled = True
            Else
                btnRun.Enabled = True
                btnOpenOnly.Enabled = True
            End If
        End If
        If isProgramEditor Then WriteConfig(configFilePath)
    End Sub
    
    Private Sub lstPrograms_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstPrograms.ColumnClick
        lstPrograms.Sorting = IIf(lstPrograms.Sorting = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending)
        lstPrograms.Sort
        'lstPrograms.Sort(e.Column)
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
            lstPrograms.FocusedItem = lstPrograms.Items.Add(tmpListViewItem)
        ElseIf e.Data.GetDataPresent(DataFormats.FileDrop)
            For i = 0 To Integer.MaxValue
                If (e.Data.GetData(DataFormats.FileDrop)(i) <> Nothing) Then
                    Dim tmpListViewItem As New ListViewItem(New String() {e.Data.GetData(DataFormats.FileDrop)(i), " ", "draggedFile"})
                    lstPrograms.FocusedItem = lstPrograms.Items.Add(tmpListViewItem)
                Else
                    Exit For
                End If
            Next
        End If
    End Sub
    
    Private Sub ReadConfig(path As String)
        Dim reader As XmlReader = XmlReader.Create(path)
        Try
            reader.Read()
        Catch ex As XmlException
            reader.Close
            Exit Sub
        End Try
        
        If reader.IsStartElement() AndAlso reader.Name = "ProgramLauncher" Then
            If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "ProgramList" Then
                While reader.IsStartElement
                    If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "Program" Then
                        Dim tmpListViewItem As New ListViewItem(New String() {"notepad", " "})
                        
                        Dim attribute As String = reader("path")
                        If attribute IsNot Nothing Then
                            tmpListViewItem.Text = attribute
                        End If
                        
                        attribute = reader("args")
                        If attribute IsNot Nothing Then
                            tmpListViewItem.SubItems.Item(1).Text = attribute
                        End If
                        
                        lstPrograms.Items.Add(tmpListViewItem)
                    End If
                End While
            End If
            If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "Settings" Then
                If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "ColumnSettings" Then
                    Dim attribute As String
                    While reader.IsStartElement
                        If reader.Read AndAlso reader.IsStartElement() Then
                            If reader.Name = "PathColumn" Then
                                attribute = reader("index")
                                If attribute IsNot Nothing Then
                                    colheadPath.DisplayIndex = attribute
                                End If
                                
                                attribute = reader("width")
                                If attribute IsNot Nothing Then
                                    colheadPath.Width = attribute
                                End If
                            ElseIf reader.Name = "ArgColumn"
                                attribute = reader("index")
                                If attribute IsNot Nothing Then
                                    colheadProgramArgs.DisplayIndex = attribute
                                End If
                                
                                attribute = reader("width")
                                If attribute IsNot Nothing Then
                                    colheadProgramArgs.Width = attribute
                                End If
                            End If
                        End If
                    End While
                End If
            End If
        End If
        
        reader.Close
    End Sub
    
    Private Sub WriteConfig(path As String)
        Dim XMLwSettings As New XmlWriterSettings()
        XMLwSettings.Indent = True
        Dim writer As XmlWriter = XmlWriter.Create(path, XMLwSettings)
        
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
        writer.WriteEndElement()
        
        writer.WriteEndElement()
        writer.WriteEndDocument()
        
        writer.Close
    End Sub
End Class

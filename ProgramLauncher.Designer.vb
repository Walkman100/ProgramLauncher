﻿Partial Class ProgramLauncher
    Inherits System.Windows.Forms.Form
    
    ''' <summary>
    ''' Disposes resources used by the form.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    
    ''' <summary>
    ''' Designer variable used to keep track of non-visual components.
    ''' </summary>
    Private components As System.ComponentModel.IContainer
    
    ''' <summary>
    ''' This method is required for Windows Forms designer support.
    ''' Do not change the method contents inside the source code editor. The Forms designer might
    ''' not be able to load this method if it was changed manually.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.lstPrograms = New System.Windows.Forms.ListView()
        Me.colheadName = New System.Windows.Forms.ColumnHeader()
        Me.colheadPath = New System.Windows.Forms.ColumnHeader()
        Me.colheadProgramArgs = New System.Windows.Forms.ColumnHeader()
        Me.contextCommands = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.contextCommandsResizePathHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextCommandsResizePathContent = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextCommandsSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.contextCommandsResizeArgsHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextCommandsResizeArgsContent = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextCommandsSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.contextCommandsResizeAllHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextCommandsResizeAllContent = New System.Windows.Forms.ToolStripMenuItem()
        Me.openFileDialogBrowse = New System.Windows.Forms.OpenFileDialog()
        Me.lblInstructions = New System.Windows.Forms.Label()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.contextCommandsSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.contextCommandsResizeNameHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextCommandsResizeNameContent = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextCommands.SuspendLayout
        Me.SuspendLayout
        '
        'btnRun
        '
        Me.btnRun.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnRun.Location = New System.Drawing.Point(466, 186)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(61, 23)
        Me.btnRun.TabIndex = 7
        Me.btnRun.Text = "Run"
        Me.btnRun.UseVisualStyleBackColor = true
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnEdit.Location = New System.Drawing.Point(466, 128)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(61, 23)
        Me.btnEdit.TabIndex = 5
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = true
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnRemove.Location = New System.Drawing.Point(466, 41)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(61, 23)
        Me.btnRemove.TabIndex = 2
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = true
        '
        'btnBrowse
        '
        Me.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnBrowse.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnBrowse.Location = New System.Drawing.Point(466, 157)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(61, 23)
        Me.btnBrowse.TabIndex = 6
        Me.btnBrowse.Text = "Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = true
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnAdd.Location = New System.Drawing.Point(466, 12)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(61, 23)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = true
        '
        'btnEnd
        '
        Me.btnEnd.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEnd.Location = New System.Drawing.Point(466, 215)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(61, 23)
        Me.btnEnd.TabIndex = 8
        Me.btnEnd.Text = "Exit"
        Me.btnEnd.UseVisualStyleBackColor = true
        '
        'lstPrograms
        '
        Me.lstPrograms.AllowColumnReorder = true
        Me.lstPrograms.AllowDrop = true
        Me.lstPrograms.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
                        Or System.Windows.Forms.AnchorStyles.Left)  _
                        Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lstPrograms.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colheadName, Me.colheadPath, Me.colheadProgramArgs})
        Me.lstPrograms.ContextMenuStrip = Me.contextCommands
        Me.lstPrograms.FullRowSelect = true
        Me.lstPrograms.GridLines = true
        Me.lstPrograms.HideSelection = false
        Me.lstPrograms.LabelEdit = true
        Me.lstPrograms.Location = New System.Drawing.Point(12, 12)
        Me.lstPrograms.Name = "lstPrograms"
        Me.lstPrograms.Size = New System.Drawing.Size(448, 213)
        Me.lstPrograms.TabIndex = 0
        Me.lstPrograms.UseCompatibleStateImageBehavior = false
        Me.lstPrograms.View = System.Windows.Forms.View.Details
        '
        'colheadName
        '
        Me.colheadName.Text = "Program Name"
        Me.colheadName.Width = 98
        '
        'colheadPath
        '
        Me.colheadPath.Text = "Program Path"
        Me.colheadPath.Width = 242
        '
        'colheadProgramArgs
        '
        Me.colheadProgramArgs.Text = "Program Arguments"
        Me.colheadProgramArgs.Width = 104
        '
        'contextCommands
        '
        Me.contextCommands.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.contextCommandsResizeNameHeader, Me.contextCommandsResizeNameContent, Me.contextCommandsSeparator1, Me.contextCommandsResizePathHeader, Me.contextCommandsResizePathContent, Me.contextCommandsSeparator2, Me.contextCommandsResizeArgsHeader, Me.contextCommandsResizeArgsContent, Me.contextCommandsSeparator3, Me.contextCommandsResizeAllHeader, Me.contextCommandsResizeAllContent})
        Me.contextCommands.Name = "contextMenuStripCommands"
        Me.contextCommands.Size = New System.Drawing.Size(248, 198)
        '
        'contextCommandsResizePathHeader
        '
        Me.contextCommandsResizePathHeader.AutoToolTip = true
        Me.contextCommandsResizePathHeader.Name = "contextCommandsResizePathHeader"
        Me.contextCommandsResizePathHeader.Size = New System.Drawing.Size(247, 22)
        Me.contextCommandsResizePathHeader.Tag = "1"
        Me.contextCommandsResizePathHeader.Text = "Resize Path column by Header"
        '
        'contextCommandsResizePathContent
        '
        Me.contextCommandsResizePathContent.AutoToolTip = true
        Me.contextCommandsResizePathContent.Name = "contextCommandsResizePathContent"
        Me.contextCommandsResizePathContent.Size = New System.Drawing.Size(247, 22)
        Me.contextCommandsResizePathContent.Tag = "1"
        Me.contextCommandsResizePathContent.Text = "Resize Path column by Content"
        '
        'contextCommandsSeparator2
        '
        Me.contextCommandsSeparator2.Name = "contextCommandsSeparator2"
        Me.contextCommandsSeparator2.Size = New System.Drawing.Size(244, 6)
        '
        'contextCommandsResizeArgsHeader
        '
        Me.contextCommandsResizeArgsHeader.AutoToolTip = true
        Me.contextCommandsResizeArgsHeader.Name = "contextCommandsResizeArgsHeader"
        Me.contextCommandsResizeArgsHeader.Size = New System.Drawing.Size(247, 22)
        Me.contextCommandsResizeArgsHeader.Tag = "2"
        Me.contextCommandsResizeArgsHeader.Text = "Resize Args column by Header"
        '
        'contextCommandsResizeArgsContent
        '
        Me.contextCommandsResizeArgsContent.AutoToolTip = true
        Me.contextCommandsResizeArgsContent.Name = "contextCommandsResizeArgsContent"
        Me.contextCommandsResizeArgsContent.Size = New System.Drawing.Size(247, 22)
        Me.contextCommandsResizeArgsContent.Tag = "2"
        Me.contextCommandsResizeArgsContent.Text = "Resize Args column by Content"
        '
        'contextCommandsSeparator3
        '
        Me.contextCommandsSeparator3.Name = "contextCommandsSeparator3"
        Me.contextCommandsSeparator3.Size = New System.Drawing.Size(244, 6)
        '
        'contextCommandsResizeAllHeader
        '
        Me.contextCommandsResizeAllHeader.AutoToolTip = true
        Me.contextCommandsResizeAllHeader.Name = "contextCommandsResizeAllHeader"
        Me.contextCommandsResizeAllHeader.Size = New System.Drawing.Size(247, 22)
        Me.contextCommandsResizeAllHeader.Text = "Resize all by Column Header"
        '
        'contextCommandsResizeAllContent
        '
        Me.contextCommandsResizeAllContent.AutoToolTip = true
        Me.contextCommandsResizeAllContent.Name = "contextCommandsResizeAllContent"
        Me.contextCommandsResizeAllContent.Size = New System.Drawing.Size(247, 22)
        Me.contextCommandsResizeAllContent.Text = "Resize all by Column Content"
        '
        'openFileDialogBrowse
        '
        Me.openFileDialogBrowse.DefaultExt = "exe"
        Me.openFileDialogBrowse.Filter = "Executables|*.exe; *.bat; *.com; *.vbs; *.cpl|All files|*.*"
        Me.openFileDialogBrowse.ReadOnlyChecked = true
        Me.openFileDialogBrowse.Title = "Select a program"
        '
        'lblInstructions
        '
        Me.lblInstructions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblInstructions.AutoSize = true
        Me.lblInstructions.Location = New System.Drawing.Point(12, 228)
        Me.lblInstructions.Name = "lblInstructions"
        Me.lblInstructions.Size = New System.Drawing.Size(447, 13)
        Me.lblInstructions.TabIndex = 9
        Me.lblInstructions.Text = """{0}"" will be replaced with the argument. You can use ""Copy to Clipboard"" as a program path."
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnMoveUp.Location = New System.Drawing.Point(466, 70)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(61, 23)
        Me.btnMoveUp.TabIndex = 3
        Me.btnMoveUp.Text = "Move ▲"
        Me.btnMoveUp.UseVisualStyleBackColor = true
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnMoveDown.Location = New System.Drawing.Point(466, 99)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(61, 23)
        Me.btnMoveDown.TabIndex = 4
        Me.btnMoveDown.Text = "Move ▼"
        Me.btnMoveDown.UseVisualStyleBackColor = true
        '
        'lblVersion
        '
        Me.lblVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblVersion.AutoSize = true
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 6!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblVersion.Location = New System.Drawing.Point(515, 239)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(21, 9)
        Me.lblVersion.TabIndex = 10
        Me.lblVersion.Text = "1.0.0"
        '
        'contextCommandsSeparator1
        '
        Me.contextCommandsSeparator1.Name = "contextCommandsSeparator1"
        Me.contextCommandsSeparator1.Size = New System.Drawing.Size(244, 6)
        '
        'contextCommandsResizeNameHeader
        '
        Me.contextCommandsResizeNameHeader.AutoToolTip = true
        Me.contextCommandsResizeNameHeader.Name = "contextCommandsResizeNameHeader"
        Me.contextCommandsResizeNameHeader.Size = New System.Drawing.Size(247, 22)
        Me.contextCommandsResizeNameHeader.Tag = "0"
        Me.contextCommandsResizeNameHeader.Text = "Resize Name column by Header"
        '
        'contextCommandsResizeNameContent
        '
        Me.contextCommandsResizeNameContent.AutoToolTip = true
        Me.contextCommandsResizeNameContent.Name = "contextCommandsResizeNameContent"
        Me.contextCommandsResizeNameContent.Size = New System.Drawing.Size(247, 22)
        Me.contextCommandsResizeNameContent.Tag = "0"
        Me.contextCommandsResizeNameContent.Text = "Resize Name column by Content"
        '
        'ProgramLauncher
        '
        Me.AcceptButton = Me.btnBrowse
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnEnd
        Me.ClientSize = New System.Drawing.Size(539, 250)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.btnMoveDown)
        Me.Controls.Add(Me.btnMoveUp)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.lstPrograms)
        Me.Controls.Add(Me.lblInstructions)
        Me.Icon = Global.ProgramLauncher.My.Resources.Resources._1458111143_open
        Me.Name = "ProgramLauncher"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit ProgramLauncher Programs"
        Me.contextCommands.ResumeLayout(false)
        Me.ResumeLayout(false)
        Me.PerformLayout
    End Sub
    Private contextCommandsSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents contextCommandsResizeNameContent As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents contextCommandsResizeNameHeader As System.Windows.Forms.ToolStripMenuItem
    Private contextCommandsSeparator2 As System.Windows.Forms.ToolStripSeparator
    Private contextCommandsSeparator3 As System.Windows.Forms.ToolStripSeparator
    Private colheadName As System.Windows.Forms.ColumnHeader
    Private lblVersion As System.Windows.Forms.Label
    Private WithEvents btnMoveDown As System.Windows.Forms.Button
    Private WithEvents btnMoveUp As System.Windows.Forms.Button
    Private WithEvents contextCommandsResizeAllContent As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents contextCommandsResizeAllHeader As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents contextCommandsResizeArgsContent As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents contextCommandsResizeArgsHeader As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents contextCommandsResizePathContent As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents contextCommandsResizePathHeader As System.Windows.Forms.ToolStripMenuItem
    Private contextCommands As System.Windows.Forms.ContextMenuStrip
    Private lblInstructions As System.Windows.Forms.Label
    Private openFileDialogBrowse As System.Windows.Forms.OpenFileDialog
    Private colheadProgramArgs As System.Windows.Forms.ColumnHeader
    Private colheadPath As System.Windows.Forms.ColumnHeader
    Private WithEvents lstPrograms As System.Windows.Forms.ListView
    Private WithEvents btnEnd As System.Windows.Forms.Button
    Private WithEvents btnAdd As System.Windows.Forms.Button
    Private WithEvents btnBrowse As System.Windows.Forms.Button
    Private WithEvents btnRemove As System.Windows.Forms.Button
    Private WithEvents btnEdit As System.Windows.Forms.Button
    Private WithEvents btnOpenOnly As System.Windows.Forms.Button
    Private WithEvents btnRun As System.Windows.Forms.Button
End Class

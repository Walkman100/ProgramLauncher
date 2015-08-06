<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgramLauncher
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.lstPrograms = New System.Windows.Forms.ListView()
        Me.colheadPath = New System.Windows.Forms.ColumnHeader()
        Me.colheadProgramArgs = New System.Windows.Forms.ColumnHeader()
        Me.browseProgram = New System.Windows.Forms.OpenFileDialog()
        Me.lblInstructions = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'btnRun
        '
        Me.btnRun.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnRun.Location = New System.Drawing.Point(466, 128)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(61, 23)
        Me.btnRun.TabIndex = 39
        Me.btnRun.Text = "Run"
        Me.btnRun.UseVisualStyleBackColor = true
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnEdit.Location = New System.Drawing.Point(466, 70)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(61, 23)
        Me.btnEdit.TabIndex = 40
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = true
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnRemove.Location = New System.Drawing.Point(466, 41)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(61, 23)
        Me.btnRemove.TabIndex = 38
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = true
        '
        'btnBrowse
        '
        Me.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnBrowse.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnBrowse.Location = New System.Drawing.Point(466, 99)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(61, 23)
        Me.btnBrowse.TabIndex = 37
        Me.btnBrowse.Text = "Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = true
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAdd.Location = New System.Drawing.Point(466, 12)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(61, 23)
        Me.btnAdd.TabIndex = 36
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = true
        '
        'btnEnd
        '
        Me.btnEnd.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEnd.Location = New System.Drawing.Point(466, 157)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(61, 23)
        Me.btnEnd.TabIndex = 35
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
        Me.lstPrograms.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colheadPath, Me.colheadProgramArgs})
        Me.lstPrograms.FullRowSelect = true
        Me.lstPrograms.GridLines = true
        Me.lstPrograms.HideSelection = false
        Me.lstPrograms.LabelEdit = true
        Me.lstPrograms.Location = New System.Drawing.Point(12, 12)
        Me.lstPrograms.Name = "lstPrograms"
        Me.lstPrograms.Size = New System.Drawing.Size(448, 155)
        Me.lstPrograms.TabIndex = 34
        Me.lstPrograms.UseCompatibleStateImageBehavior = false
        Me.lstPrograms.View = System.Windows.Forms.View.Details
        '
        'colheadPath
        '
        Me.colheadPath.Text = "Program path"
        Me.colheadPath.Width = 239
        '
        'colheadProgramArgs
        '
        Me.colheadProgramArgs.Text = "Program Arguments"
        Me.colheadProgramArgs.Width = 202
        '
        'browseProgram
        '
        Me.browseProgram.DefaultExt = "exe"
        Me.browseProgram.FileName = "openFileDialog1"
        Me.browseProgram.Filter = "Executables|*.exe; *.bat; *.com; *.vbs; *.cpl|All files|*.*"
        Me.browseProgram.ReadOnlyChecked = true
        Me.browseProgram.Title = "Select a program"
        '
        'lblInstructions
        '
        Me.lblInstructions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.lblInstructions.AutoSize = true
        Me.lblInstructions.Location = New System.Drawing.Point(12, 170)
        Me.lblInstructions.Name = "lblInstructions"
        Me.lblInstructions.Size = New System.Drawing.Size(197, 13)
        Me.lblInstructions.TabIndex = 41
        Me.lblInstructions.Text = """{0}"" Will be replaced with the argument"
        '
        'ProgramLauncher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(539, 192)
        Me.Controls.Add(Me.lblInstructions)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.lstPrograms)
        Me.Name = "ProgramLauncher"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit ProgramLauncher Programs"
        Me.ResumeLayout(false)
        Me.PerformLayout
    End Sub
    Private lblInstructions As System.Windows.Forms.Label
    Private browseProgram As System.Windows.Forms.OpenFileDialog
    Private colheadProgramArgs As System.Windows.Forms.ColumnHeader
    Private colheadPath As System.Windows.Forms.ColumnHeader
    Private lstPrograms As System.Windows.Forms.ListView
    Private btnEnd As System.Windows.Forms.Button
    Private btnAdd As System.Windows.Forms.Button
    Private btnBrowse As System.Windows.Forms.Button
    Private btnRemove As System.Windows.Forms.Button
    Private btnEdit As System.Windows.Forms.Button
    Private btnRun As System.Windows.Forms.Button

End Class

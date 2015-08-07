Partial Class ProgramLauncher
    Private Sub InitializeProgramSelectorComponents()
        Me.lblInstructions = New System.Windows.Forms.Label()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.lstPrograms = New System.Windows.Forms.ListView()
        Me.colheadPath = New System.Windows.Forms.ColumnHeader()
        Me.colheadProgramArgs = New System.Windows.Forms.ColumnHeader()
        Me.btnOpenOnly = New System.Windows.Forms.Button()
        Me.SuspendLayout
        'lblInstructions
        Me.lblInstructions.AutoSize = true
        Me.lblInstructions.Location = New System.Drawing.Point(12, 9)
        Me.lblInstructions.Size = New System.Drawing.Size(164, 13)
        Me.lblInstructions.Text = "Select a program to open """" with:"
        'btnRun
        Me.btnRun.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnRun.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnRun.Location = New System.Drawing.Point(239, 186)
        Me.btnRun.Size = New System.Drawing.Size(86, 23)
        Me.btnRun.Text = "Open And Exit"
        Me.btnRun.UseVisualStyleBackColor = true
        'btnEdit
        Me.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnEdit.Location = New System.Drawing.Point(55, 186)
        Me.btnEdit.Size = New System.Drawing.Size(86, 23)
        Me.btnEdit.Text = "Edit Programs"
        Me.btnEdit.UseVisualStyleBackColor = true
        'btnEnd
        Me.btnEnd.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEnd.Location = New System.Drawing.Point(331, 186)
        Me.btnEnd.Size = New System.Drawing.Size(86, 23)
        Me.btnEnd.Text = "Cancel"
        Me.btnEnd.UseVisualStyleBackColor = true
        'lstPrograms
        Me.lstPrograms.AllowColumnReorder = true
        Me.lstPrograms.AllowDrop = true
        Me.lstPrograms.Anchor = _
          CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or _
            System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lstPrograms.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colheadPath, Me.colheadProgramArgs})
        Me.lstPrograms.FullRowSelect = true
        Me.lstPrograms.GridLines = true
        Me.lstPrograms.HideSelection = false
        Me.lstPrograms.LabelEdit = true
        Me.lstPrograms.Location = New System.Drawing.Point(12, 25)
        Me.lstPrograms.MultiSelect = false
        Me.lstPrograms.Size = New System.Drawing.Size(448, 155)
        Me.lstPrograms.UseCompatibleStateImageBehavior = false
        Me.lstPrograms.View = System.Windows.Forms.View.Details
        'colheadPath
        Me.colheadPath.Text = "Program path"
        'colheadProgramArgs
        Me.colheadProgramArgs.Text = "Program Arguments"
        'btnOpenOnly
        Me.btnOpenOnly.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOpenOnly.Location = New System.Drawing.Point(147, 186)
        Me.btnOpenOnly.Size = New System.Drawing.Size(86, 23)
        Me.btnOpenOnly.Text = "Open"
        Me.btnOpenOnly.UseVisualStyleBackColor = true
        'ProgramSelector
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(472, 221)
        Me.AcceptButton = Me.btnRun
        Me.CancelButton = Me.btnEnd
        Me.Controls.Add(Me.btnOpenOnly)
        Me.Controls.Add(Me.lblInstructions)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.lstPrograms)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select a Program"
        Me.ResumeLayout(false)
        Me.PerformLayout
    End Sub
End Class

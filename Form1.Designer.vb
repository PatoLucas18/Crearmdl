<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.BtnConvert = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TB3ds = New System.Windows.Forms.TextBox()
        Me.TBmdl = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.OFD3ds = New System.Windows.Forms.OpenFileDialog()
        Me.SFDmdl = New System.Windows.Forms.SaveFileDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtnConvert
        '
        Me.BtnConvert.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnConvert.Location = New System.Drawing.Point(79, 81)
        Me.BtnConvert.Name = "BtnConvert"
        Me.BtnConvert.Size = New System.Drawing.Size(285, 35)
        Me.BtnConvert.TabIndex = 2
        Me.BtnConvert.Text = "Convert"
        Me.BtnConvert.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(358, 233)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(87, 27)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "ply"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TB3ds
        '
        Me.TB3ds.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB3ds.Location = New System.Drawing.Point(79, 22)
        Me.TB3ds.Name = "TB3ds"
        Me.TB3ds.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TB3ds.Size = New System.Drawing.Size(242, 23)
        Me.TB3ds.TabIndex = 3
        '
        'TBmdl
        '
        Me.TBmdl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TBmdl.Location = New System.Drawing.Point(79, 52)
        Me.TBmdl.Name = "TBmdl"
        Me.TBmdl.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TBmdl.Size = New System.Drawing.Size(242, 23)
        Me.TBmdl.TabIndex = 4
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(327, 22)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(37, 27)
        Me.Button3.TabIndex = 0
        Me.Button3.Text = "..."
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Location = New System.Drawing.Point(327, 48)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(37, 27)
        Me.Button4.TabIndex = 1
        Me.Button4.Text = "..."
        Me.Button4.UseVisualStyleBackColor = True
        '
        'OFD3ds
        '
        Me.OFD3ds.Filter = "3ds Files|*.3ds|ply Files|*.ply|All Files|*.*"
        '
        'SFDmdl
        '
        Me.SFDmdl.Filter = "bin [*.bin]|*.bin"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 45)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Open 3d" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Save Bin"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(382, 135)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.TBmdl)
        Me.Controls.Add(Me.TB3ds)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.BtnConvert)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(900, 173)
        Me.MinimumSize = New System.Drawing.Size(398, 173)
        Me.Name = "Form1"
        Me.Text = "Ball Converter"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnConvert As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TB3ds As System.Windows.Forms.TextBox
    Friend WithEvents TBmdl As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents OFD3ds As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SFDmdl As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class

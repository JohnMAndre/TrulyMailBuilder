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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btnRun = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.chkBuildPortable = New System.Windows.Forms.CheckBox()
        Me.txtVersion = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkBackupSource = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.llblClearOutput = New System.Windows.Forms.LinkLabel()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.lblOverallProgress = New System.Windows.Forms.Label()
        Me.ProgressBar2 = New MontgomerySoftware.Controls.ProgressBar()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRun
        '
        Me.btnRun.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnRun.Location = New System.Drawing.Point(266, 550)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(75, 23)
        Me.btnRun.TabIndex = 0
        Me.btnRun.Text = "&Build"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCancel.Enabled = False
        Me.btnCancel.Location = New System.Drawing.Point(347, 550)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'chkBuildPortable
        '
        Me.chkBuildPortable.AutoSize = True
        Me.chkBuildPortable.Checked = True
        Me.chkBuildPortable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBuildPortable.Location = New System.Drawing.Point(6, 43)
        Me.chkBuildPortable.Name = "chkBuildPortable"
        Me.chkBuildPortable.Size = New System.Drawing.Size(110, 17)
        Me.chkBuildPortable.TabIndex = 3
        Me.chkBuildPortable.Text = "TrulyMail Portable"
        Me.chkBuildPortable.UseVisualStyleBackColor = True
        '
        'txtVersion
        '
        Me.txtVersion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVersion.Location = New System.Drawing.Point(6, 19)
        Me.txtVersion.Name = "txtVersion"
        Me.txtVersion.Size = New System.Drawing.Size(368, 20)
        Me.txtVersion.TabIndex = 5
        Me.txtVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkBuildPortable)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(128, 71)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "TrulyMail Editions"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.chkBackupSource)
        Me.GroupBox2.Controls.Add(Me.txtVersion)
        Me.GroupBox2.Location = New System.Drawing.Point(294, 16)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(380, 71)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Version"
        '
        'chkBackupSource
        '
        Me.chkBackupSource.AutoSize = True
        Me.chkBackupSource.Location = New System.Drawing.Point(23, 45)
        Me.chkBackupSource.Name = "chkBackupSource"
        Me.chkBackupSource.Size = New System.Drawing.Size(98, 17)
        Me.chkBackupSource.TabIndex = 6
        Me.chkBackupSource.Text = "Backup source"
        Me.chkBackupSource.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.llblClearOutput)
        Me.GroupBox3.Controls.Add(Me.txtOutput)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 93)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(662, 420)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Statistics"
        '
        'llblClearOutput
        '
        Me.llblClearOutput.AutoSize = True
        Me.llblClearOutput.Location = New System.Drawing.Point(69, 0)
        Me.llblClearOutput.Name = "llblClearOutput"
        Me.llblClearOutput.Size = New System.Drawing.Size(64, 13)
        Me.llblClearOutput.TabIndex = 6
        Me.llblClearOutput.TabStop = True
        Me.llblClearOutput.Text = "Clear output"
        '
        'txtOutput
        '
        Me.txtOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutput.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutput.Location = New System.Drawing.Point(6, 19)
        Me.txtOutput.Multiline = True
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOutput.Size = New System.Drawing.Size(650, 395)
        Me.txtOutput.TabIndex = 5
        '
        'lblOverallProgress
        '
        Me.lblOverallProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOverallProgress.AutoSize = True
        Me.lblOverallProgress.Location = New System.Drawing.Point(15, 527)
        Me.lblOverallProgress.Name = "lblOverallProgress"
        Me.lblOverallProgress.Size = New System.Drawing.Size(51, 13)
        Me.lblOverallProgress.TabIndex = 13
        Me.lblOverallProgress.Text = "Progress:"
        Me.lblOverallProgress.Visible = False
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ProgressBar2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProgressBar2.ForeColor = System.Drawing.Color.Blue
        Me.ProgressBar2.HighLowCutoff = CType(90, Byte)
        Me.ProgressBar2.HighTextDecimalPlaces = CType(0, Byte)
        Me.ProgressBar2.Location = New System.Drawing.Point(72, 519)
        Me.ProgressBar2.LowTextDecimalPlaces = CType(0, Byte)
        Me.ProgressBar2.Maximum = CType(100, Long)
        Me.ProgressBar2.Minimum = CType(0, Long)
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(596, 25)
        Me.ProgressBar2.TabIndex = 11
        Me.ProgressBar2.Value = CType(0, Long)
        Me.ProgressBar2.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Location = New System.Drawing.Point(146, 16)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(142, 71)
        Me.GroupBox4.TabIndex = 8
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Options"
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button1.Location = New System.Drawing.Point(126, 550)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(116, 23)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "Quick Portable"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 585)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.lblOverallProgress)
        Me.Controls.Add(Me.ProgressBar2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnRun)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "TrulyMail 7 Builder"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chkBuildPortable As System.Windows.Forms.CheckBox
    Friend WithEvents txtVersion As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtOutput As System.Windows.Forms.TextBox
    Friend WithEvents lblOverallProgress As System.Windows.Forms.Label
    Friend WithEvents ProgressBar2 As MontgomerySoftware.Controls.ProgressBar
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents chkBackupSource As System.Windows.Forms.CheckBox
    Friend WithEvents llblClearOutput As LinkLabel
    Friend WithEvents Button1 As Button
End Class

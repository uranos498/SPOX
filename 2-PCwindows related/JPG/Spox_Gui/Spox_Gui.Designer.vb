<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Spox_Gui
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Spox_Gui))
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.GroupLed = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Led2 = New System.Windows.Forms.PictureBox()
        Me.Led3 = New System.Windows.Forms.PictureBox()
        Me.Led1 = New System.Windows.Forms.PictureBox()
        Me.TrigLed2 = New System.Windows.Forms.Button()
        Me.TrigLed1 = New System.Windows.Forms.Button()
        Me.Analog = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Trigger = New System.Windows.Forms.NumericUpDown()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Isdark = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Analog1 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupLed.SuspendLayout()
        CType(Me.Led2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Led3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Led1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Trigger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Analog1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.AllowDrop = True
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(6, 6)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.ScrollAlwaysVisible = True
        Me.ListBox1.Size = New System.Drawing.Size(85, 17)
        Me.ListBox1.TabIndex = 0
        '
        'GroupLed
        '
        Me.GroupLed.BackColor = System.Drawing.SystemColors.Control
        Me.GroupLed.Controls.Add(Me.Label4)
        Me.GroupLed.Controls.Add(Me.Label3)
        Me.GroupLed.Controls.Add(Me.Label2)
        Me.GroupLed.Controls.Add(Me.Led2)
        Me.GroupLed.Controls.Add(Me.Led3)
        Me.GroupLed.Controls.Add(Me.Led1)
        Me.GroupLed.Enabled = False
        Me.GroupLed.Location = New System.Drawing.Point(6, 18)
        Me.GroupLed.Name = "GroupLed"
        Me.GroupLed.Size = New System.Drawing.Size(170, 98)
        Me.GroupLed.TabIndex = 3
        Me.GroupLed.TabStop = False
        Me.GroupLed.Text = "Control"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(68, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Alarm"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(130, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Flat"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Calib"
        '
        'Led2
        '
        Me.Led2.Image = CType(resources.GetObject("Led2.Image"), System.Drawing.Image)
        Me.Led2.Location = New System.Drawing.Point(124, 19)
        Me.Led2.Name = "Led2"
        Me.Led2.Size = New System.Drawing.Size(30, 30)
        Me.Led2.TabIndex = 1
        Me.Led2.TabStop = False
        '
        'Led3
        '
        Me.Led3.Image = CType(resources.GetObject("Led3.Image"), System.Drawing.Image)
        Me.Led3.Location = New System.Drawing.Point(71, 49)
        Me.Led3.Name = "Led3"
        Me.Led3.Size = New System.Drawing.Size(30, 30)
        Me.Led3.TabIndex = 2
        Me.Led3.TabStop = False
        '
        'Led1
        '
        Me.Led1.Image = CType(resources.GetObject("Led1.Image"), System.Drawing.Image)
        Me.Led1.Location = New System.Drawing.Point(19, 19)
        Me.Led1.Name = "Led1"
        Me.Led1.Size = New System.Drawing.Size(30, 30)
        Me.Led1.TabIndex = 0
        Me.Led1.TabStop = False
        '
        'TrigLed2
        '
        Me.TrigLed2.Location = New System.Drawing.Point(89, 62)
        Me.TrigLed2.Name = "TrigLed2"
        Me.TrigLed2.Size = New System.Drawing.Size(51, 27)
        Me.TrigLed2.TabIndex = 4
        Me.TrigLed2.Text = "Trig2"
        Me.TrigLed2.UseVisualStyleBackColor = True
        '
        'TrigLed1
        '
        Me.TrigLed1.Location = New System.Drawing.Point(6, 62)
        Me.TrigLed1.Name = "TrigLed1"
        Me.TrigLed1.Size = New System.Drawing.Size(52, 27)
        Me.TrigLed1.TabIndex = 3
        Me.TrigLed1.Text = "Trig1"
        Me.TrigLed1.UseVisualStyleBackColor = True
        '
        'Analog
        '
        Me.Analog.Location = New System.Drawing.Point(6, 19)
        Me.Analog.Name = "Analog"
        Me.Analog.Size = New System.Drawing.Size(52, 22)
        Me.Analog.TabIndex = 6
        Me.Analog.Text = "Analog"
        Me.Analog.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'Trigger
        '
        Me.Trigger.Location = New System.Drawing.Point(89, 22)
        Me.Trigger.Maximum = New Decimal(New Integer() {1023, 0, 0, 0})
        Me.Trigger.Name = "Trigger"
        Me.Trigger.Size = New System.Drawing.Size(51, 20)
        Me.Trigger.TabIndex = 7
        Me.Trigger.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(3, 1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(189, 177)
        Me.TabControl1.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Isdark)
        Me.TabPage1.Controls.Add(Me.GroupLed)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(181, 151)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Spox"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Isdark
        '
        Me.Isdark.AutoSize = True
        Me.Isdark.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Isdark.Location = New System.Drawing.Point(49, 119)
        Me.Isdark.Name = "Isdark"
        Me.Isdark.Size = New System.Drawing.Size(82, 13)
        Me.Isdark.TabIndex = 4
        Me.Isdark.Text = "Dark position"
        Me.Isdark.Visible = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Analog1)
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.ListBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(181, 151)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Config"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Analog1
        '
        Me.Analog1.BackColor = System.Drawing.Color.Transparent
        Me.Analog1.Controls.Add(Me.Analog)
        Me.Analog1.Controls.Add(Me.Trigger)
        Me.Analog1.Controls.Add(Me.TrigLed2)
        Me.Analog1.Controls.Add(Me.TrigLed1)
        Me.Analog1.Enabled = False
        Me.Analog1.Location = New System.Drawing.Point(10, 50)
        Me.Analog1.Name = "Analog1"
        Me.Analog1.Size = New System.Drawing.Size(165, 95)
        Me.Analog1.TabIndex = 9
        Me.Analog1.TabStop = False
        Me.Analog1.Text = "Analog"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(99, 7)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(71, 26)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Connect"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.PictureBox2)
        Me.TabPage3.Controls.Add(Me.Label1)
        Me.TabPage3.Controls.Add(Me.PictureBox1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(181, 151)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Credits"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox2.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureBox2.Image = Global.Spox_Gui.My.Resources.Resources.Bug72T_sm
        Me.PictureBox2.Location = New System.Drawing.Point(131, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(47, 49)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 2
        Me.PictureBox2.TabStop = False
        Me.PictureBox2.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 52)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Software by:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Steve Barkes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Jean-Baptiste Butet" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Jean-Paul Godard" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Spox_Gui.My.Resources.Resources.sheliack
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(122, 48)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Spox_Gui
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(196, 185)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Spox_Gui"
        Me.Text = "Spox_Gui"
        Me.GroupLed.ResumeLayout(False)
        Me.GroupLed.PerformLayout()
        CType(Me.Led2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Led3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Led1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Trigger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.Analog1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents GroupLed As System.Windows.Forms.GroupBox
    Friend WithEvents Led3 As System.Windows.Forms.PictureBox
    Friend WithEvents Led2 As System.Windows.Forms.PictureBox
    Friend WithEvents Led1 As System.Windows.Forms.PictureBox
    Friend WithEvents Analog As System.Windows.Forms.Button
    Friend WithEvents TrigLed2 As System.Windows.Forms.Button
    Friend WithEvents TrigLed1 As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Trigger As System.Windows.Forms.NumericUpDown
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Analog1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Isdark As System.Windows.Forms.Label

End Class

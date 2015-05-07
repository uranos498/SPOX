<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Spox_Test
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
        Me.labelDriverId = New System.Windows.Forms.Label()
        Me.buttonConnect = New System.Windows.Forms.Button()
        Me.buttonChoose = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.TsStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Tab_Board = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Isdark = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Buttons = New System.Windows.Forms.GroupBox()
        Me.PB1 = New System.Windows.Forms.Button()
        Me.PB2 = New System.Windows.Forms.Button()
        Me.Led3 = New System.Windows.Forms.PictureBox()
        Me.Led1 = New System.Windows.Forms.PictureBox()
        Me.Led2 = New System.Windows.Forms.PictureBox()
        Me.Tab_Options = New System.Windows.Forms.TabPage()
        Me.AutoConnect = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Timer_blink = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1.SuspendLayout()
        Me.Tab_Board.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Buttons.SuspendLayout()
        CType(Me.Led3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Led1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Led2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Tab_Options.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'labelDriverId
        '
        Me.labelDriverId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.labelDriverId.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.ASCOM.Spox.My.MySettings.Default, "DriverId", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.labelDriverId.Location = New System.Drawing.Point(9, 142)
        Me.labelDriverId.Name = "labelDriverId"
        Me.labelDriverId.Size = New System.Drawing.Size(193, 21)
        Me.labelDriverId.TabIndex = 5
        Me.labelDriverId.Text = Global.ASCOM.Spox.My.MySettings.Default.DriverId
        Me.labelDriverId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'buttonConnect
        '
        Me.buttonConnect.Enabled = False
        Me.buttonConnect.Location = New System.Drawing.Point(188, 35)
        Me.buttonConnect.Name = "buttonConnect"
        Me.buttonConnect.Size = New System.Drawing.Size(72, 23)
        Me.buttonConnect.TabIndex = 4
        Me.buttonConnect.Text = "Connect"
        Me.buttonConnect.UseVisualStyleBackColor = True
        '
        'buttonChoose
        '
        Me.buttonChoose.Location = New System.Drawing.Point(188, 6)
        Me.buttonChoose.Name = "buttonChoose"
        Me.buttonChoose.Size = New System.Drawing.Size(72, 23)
        Me.buttonChoose.TabIndex = 3
        Me.buttonChoose.Text = "Choose"
        Me.buttonChoose.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TsStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 211)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(278, 22)
        Me.StatusStrip1.TabIndex = 6
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'TsStatus
        '
        Me.TsStatus.Name = "TsStatus"
        Me.TsStatus.Size = New System.Drawing.Size(61, 17)
        Me.TsStatus.Text = "                  "
        '
        'Tab_Board
        '
        Me.Tab_Board.Controls.Add(Me.TabPage1)
        Me.Tab_Board.Controls.Add(Me.Tab_Options)
        Me.Tab_Board.Location = New System.Drawing.Point(0, 2)
        Me.Tab_Board.Name = "Tab_Board"
        Me.Tab_Board.SelectedIndex = 0
        Me.Tab_Board.Size = New System.Drawing.Size(275, 206)
        Me.Tab_Board.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Isdark)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Buttons)
        Me.TabPage1.Controls.Add(Me.Led3)
        Me.TabPage1.Controls.Add(Me.Led1)
        Me.TabPage1.Controls.Add(Me.Led2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(267, 180)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Board"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Isdark
        '
        Me.Isdark.AutoSize = True
        Me.Isdark.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Isdark.Location = New System.Drawing.Point(90, 150)
        Me.Isdark.Name = "Isdark"
        Me.Isdark.Size = New System.Drawing.Size(82, 13)
        Me.Isdark.TabIndex = 14
        Me.Isdark.Text = "Dark position"
        Me.Isdark.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(124, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Alarm"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(188, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Flat"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(60, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Calib"
        '
        'Buttons
        '
        Me.Buttons.Controls.Add(Me.PB1)
        Me.Buttons.Controls.Add(Me.PB2)
        Me.Buttons.Location = New System.Drawing.Point(8, 80)
        Me.Buttons.Name = "Buttons"
        Me.Buttons.Size = New System.Drawing.Size(220, 57)
        Me.Buttons.TabIndex = 10
        Me.Buttons.TabStop = False
        Me.Buttons.Text = "Please Press Buttons"
        '
        'PB1
        '
        Me.PB1.Location = New System.Drawing.Point(53, 19)
        Me.PB1.Name = "PB1"
        Me.PB1.Size = New System.Drawing.Size(42, 25)
        Me.PB1.TabIndex = 0
        Me.PB1.Text = "Calib"
        Me.PB1.UseVisualStyleBackColor = True
        '
        'PB2
        '
        Me.PB2.Location = New System.Drawing.Point(168, 19)
        Me.PB2.Name = "PB2"
        Me.PB2.Size = New System.Drawing.Size(42, 25)
        Me.PB2.TabIndex = 1
        Me.PB2.Text = "Flat"
        Me.PB2.UseVisualStyleBackColor = True
        '
        'Led3
        '
        Me.Led3.BackColor = System.Drawing.SystemColors.Control
        Me.Led3.Image = Global.ASCOM.Spox.My.Resources.Resources.off
        Me.Led3.Location = New System.Drawing.Point(127, 31)
        Me.Led3.Name = "Led3"
        Me.Led3.Size = New System.Drawing.Size(30, 30)
        Me.Led3.TabIndex = 5
        Me.Led3.TabStop = False
        '
        'Led1
        '
        Me.Led1.BackColor = System.Drawing.SystemColors.Control
        Me.Led1.Image = Global.ASCOM.Spox.My.Resources.Resources.off
        Me.Led1.Location = New System.Drawing.Point(61, 16)
        Me.Led1.Name = "Led1"
        Me.Led1.Size = New System.Drawing.Size(30, 30)
        Me.Led1.TabIndex = 3
        Me.Led1.TabStop = False
        '
        'Led2
        '
        Me.Led2.BackColor = System.Drawing.SystemColors.Control
        Me.Led2.Image = Global.ASCOM.Spox.My.Resources.Resources.off
        Me.Led2.Location = New System.Drawing.Point(188, 16)
        Me.Led2.Name = "Led2"
        Me.Led2.Size = New System.Drawing.Size(30, 30)
        Me.Led2.TabIndex = 4
        Me.Led2.TabStop = False
        '
        'Tab_Options
        '
        Me.Tab_Options.Controls.Add(Me.AutoConnect)
        Me.Tab_Options.Controls.Add(Me.labelDriverId)
        Me.Tab_Options.Controls.Add(Me.Label1)
        Me.Tab_Options.Controls.Add(Me.PictureBox2)
        Me.Tab_Options.Controls.Add(Me.buttonChoose)
        Me.Tab_Options.Controls.Add(Me.buttonConnect)
        Me.Tab_Options.Controls.Add(Me.PictureBox1)
        Me.Tab_Options.Location = New System.Drawing.Point(4, 22)
        Me.Tab_Options.Name = "Tab_Options"
        Me.Tab_Options.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab_Options.Size = New System.Drawing.Size(267, 180)
        Me.Tab_Options.TabIndex = 1
        Me.Tab_Options.Text = "Options"
        Me.Tab_Options.UseVisualStyleBackColor = True
        '
        'AutoConnect
        '
        Me.AutoConnect.AutoSize = True
        Me.AutoConnect.Enabled = False
        Me.AutoConnect.Location = New System.Drawing.Point(172, 64)
        Me.AutoConnect.Name = "AutoConnect"
        Me.AutoConnect.Size = New System.Drawing.Size(88, 17)
        Me.AutoConnect.TabIndex = 6
        Me.AutoConnect.Text = "AutoConnect"
        Me.AutoConnect.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 83)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(203, 39)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Spox by Shelyak." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A box to interface the calibration module" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "for Alpy, LhiresIII " & _
    "and Lisa spectroscopes."
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = Global.ASCOM.Spox.My.Resources.Resources.sheliack
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(160, 52)
        Me.PictureBox2.TabIndex = 8
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.ASCOM.Spox.My.Resources.Resources.Bug72T_sm
        Me.PictureBox1.Location = New System.Drawing.Point(213, 113)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(47, 61)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'Timer_blink
        '
        '
        'Timer1
        '
        '
        'Spox_Test
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(278, 233)
        Me.Controls.Add(Me.Tab_Board)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Name = "Spox_Test"
        Me.Text = "Spox Command Panel "
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Tab_Board.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Buttons.ResumeLayout(False)
        CType(Me.Led3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Led1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Led2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Tab_Options.ResumeLayout(False)
        Me.Tab_Options.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents labelDriverId As System.Windows.Forms.Label
    Private WithEvents buttonConnect As System.Windows.Forms.Button
    Private WithEvents buttonChoose As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Tab_Board As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents PB2 As System.Windows.Forms.Button
    Friend WithEvents PB1 As System.Windows.Forms.Button
    Friend WithEvents Tab_Options As System.Windows.Forms.TabPage
    Friend WithEvents AutoConnect As System.Windows.Forms.CheckBox
    Friend WithEvents Led3 As System.Windows.Forms.PictureBox
    Friend WithEvents Led2 As System.Windows.Forms.PictureBox
    Friend WithEvents Led1 As System.Windows.Forms.PictureBox
    Friend WithEvents TsStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Timer_blink As System.Windows.Forms.Timer
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Buttons As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Isdark As System.Windows.Forms.Label

End Class

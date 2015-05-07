Imports System.Collections.Generic
Imports ASCOM.DriverAccess
Public Class Spox_Test
    Public Const psep As String = "|"
    Private driver As ASCOM.DriverAccess.Switch

    ''' <summary>
    ''' This event is where the driver is choosen. The device ID will be saved in the settings.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub buttonChoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonChoose.Click
        If Not String.IsNullOrEmpty(My.Settings.DriverId) Then
            My.Settings.DriverId = ASCOM.DriverAccess.Switch.Choose(My.Settings.DriverId)
        Else
            My.Settings.DriverId = ASCOM.DriverAccess.Switch.Choose("Ascom.spox.Switch")
        End If
        SetUIState()
    End Sub

    ''' <summary>
    ''' Connects to the device to be tested.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub buttonConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonConnect.Click
        If (IsConnected) Then
            driver.Connected = False
        Else
            Try
                driver = New ASCOM.DriverAccess.Switch(My.Settings.DriverId)
                driver.Connected = True
            Catch ex As Exception
                driver.Connected = False
            End Try
        End If
        SetUIState()
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If IsConnected Then
            driver.Connected = False
        End If
        ' the settings are saved automatically when this application is closed.
    End Sub

    ''' <summary>
    ''' Sets the state of the UI depending on the device state
    ''' </summary>
    Private Sub SetUIState()
        buttonConnect.Enabled = Not String.IsNullOrEmpty(My.Settings.DriverId)
        buttonChoose.Enabled = Not IsConnected
        buttonConnect.Text = IIf(IsConnected, "Disconnect", "Connect")
        Buttons.Enabled = IsConnected
    End Sub

    ''' <summary>
    ''' Gets a value indicating whether this instance is connected.
    ''' </summary>
    ''' <value>
    ''' 
    ''' <c>true</c> if this instance is connected; otherwise, <c>false</c>.
    ''' 
    ''' </value>
    Private ReadOnly Property IsConnected() As Boolean
        Get
            If Me.driver Is Nothing Then Return False
            Return driver.Connected
        End Get
    End Property

    ' TODO: Add additional UI and controls to test more of the driver being tested.
    Dim xled1 As led
    Dim xled2 As led
    Dim xled3 As led
    Dim mylist As New List(Of led)
    Private Sub Spox_Test_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mylist.Clear()
        ' Création des led éteintes
        xled1 = New led("Led1", Led1, "1")
        mylist.Add(xled1)
        xled2 = New led("Led2", Led2, "2")
        mylist.Add(xled2)
        xled3 = New led("Led3", Led3, "3")
        'mylist.Add(xled3)
        '
        '
        SetBoardState()
        Timer_blink.Interval = 100
        Timer_blink.Enabled = True

        Timer1.Interval = 1000
        Timer1.Enabled = True
        Buttons.Enabled = IsConnected

    End Sub
    Private Sub SetBoardState()
        Isdark.Visible = False
        TsStatus.Text = "Led1 and Led2 Off"
        If xled1.is_on Then
            TsStatus.Text = "Led1 on"
            If xled2.is_on Then
                TsStatus.Text = "Led1 and Led2 on"
                Isdark.Visible = True
                Mylist_blinking(True)
            Else
                Mylist_blinking(False)
                Isdark.Visible = False
            End If
        Else
            Mylist_blinking(False)
            If xled2.is_on Then
                TsStatus.Text = "Led2 on"
            End If
        End If
        '
    End Sub
    Private Sub Mylist_blinking(val As Boolean)
        For Each l As led In mylist
            If val Then
                l.setblink()
            Else
                l.offblink()
            End If
        Next
    End Sub
    Private Sub Mylist_Alarm(val As Boolean)
        For Each l As led In mylist
            l.Alarm_set(val)
        Next
    End Sub
    Private Sub PB1_Click(sender As Object, e As EventArgs) Handles PB1.Click
        If xled1.is_on Then
            driver.SetSwitch(0, False)
        Else
            driver.SetSwitch(0, True)
        End If
        SetBoardState()
    End Sub

    Private Sub PB2_Click(sender As Object, e As EventArgs) Handles PB2.Click
        If xled2.is_on Then
            driver.SetSwitch(1, False)
            'xled2.SetOff()
            'xled2.offblink()
            'Mylist_blinking(False)
        Else
            driver.SetSwitch(1, True)
            'xled2.SetOn()
        End If
        SetBoardState()
    End Sub

    Private Sub Timer_blink_Tick(sender As Object, e As EventArgs) Handles Timer_blink.Tick
        Dim val As Boolean = False
        If Timer_blink.Interval = 100 Then
            Timer_blink.Interval = 900
        Else
            Timer_blink.Interval = 100
            val = True
        End If
        For Each l As led In mylist
            l.blink(val)
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim rep As String
        If Not (IsConnected) Then
            Return
        Else
            Try
                rep = driver.Action("Spox:READALARM", "xx")
                If rep = "1" Then
                    Led3.Image = My.Resources.ResourceManager.GetObject("ona")
                Else
                    Led3.Image = My.Resources.ResourceManager.GetObject("off")
                End If
            Catch ex As Exception
                Timer1.Enabled = False
                MessageBox.Show("Connexion failed", "Fatal error")
                Me.Close()
                Return
            End Try

            If driver.GetSwitch(0) Then
                xled1.SetOn()
            Else
                xled1.SetOff()
            End If
            If driver.GetSwitch(1) Then
                xled2.SetOn()
            Else
                xled2.SetOff()
            End If
            SetBoardState()
        End If
    End Sub

    Private Sub ChangeComPort()
        'If MyCom IsNot Nothing Then MyCom.Close()
        'Try
        '    Dim wrk As String = ListBox1.SelectedValue
        '    If IsNothing(wrk) Then Return
        '    MyCom = My.Computer.Ports.OpenSerialPort(wrk, _
        '                                             9600, _
        '                                             IO.Ports.Parity.None, _
        '                                             8, _
        '                                              IO.Ports.StopBits.One)
        '    MyCom.ReadTimeout = 1000
        'Catch ex As Exception

        'End Try
    End Sub


End Class

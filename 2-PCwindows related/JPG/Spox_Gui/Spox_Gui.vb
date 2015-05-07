Imports Spox_Dll
Public Class Spox_Gui
    Dim MyCom As IO.Ports.SerialPort = Nothing
    Dim spox As Spox_Dll.Spox_Dll
    Dim xled1 As Ledx
    Dim xled2 As Ledx
    Dim xled3 As Ledx
    '
    Dim connected As Boolean = False
    Dim portlastcount = 0
    '
    'DLL interface
    '
    '1- function toggleLed(com,led#)    returns led# state(on/off) if led#=0 all leds
    '2- function GetAnalog(com)         returns analog value read on pin A0
    '3- function SetTrigger(Com,led#, TriggerValue)  Trigger value [0-1023] returns  

    Private Sub Spox_Gui_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If MyCom IsNot Nothing Then MyCom.Close()
    End Sub
    Private Sub Spox_Gui_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        spox = New Spox_Dll.Spox_Dll
        Timer1.Interval = 1000
        Timer1.Enabled = True
        xled1 = New Ledx("Led1", Led1, "1")
        xled2 = New Ledx("Led2", Led2, "2")
        xled3 = New Ledx("Led3", Led3, "3")
        connected = False
        GetSerialPortNames()
        Isdark.Visible = False
    End Sub
    Sub GetSerialPortNames()
        ' Show all available COM ports.
        ListBox1.Items.Clear()
        portlastcount = My.Computer.Ports.SerialPortNames.Count
        If portlastcount = 0 Then Return
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ListBox1.Items.Add(sp)
        Next
        autoselect()
        'portlastcount = My.Computer.Ports.SerialPortNames.Count
    End Sub
    Private Sub autoselect()
        Dim wrk As String = ListBox1.Items(0)
        If IsNothing(wrk) Then Return
        ListBox1.SetSelected(0, True)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If connected Then  ' Disconnect
            Button1.Text = "Connect"
            MyCom.Close()
            MyCom = Nothing
            connected = False
            setUI(connected)
        Else
            If IsNothing(ListBox1.SelectedItem) Then Return
            Button1.Text = "Disconnect"
            connected = connect2selected()
            setUI(connected)
        End If
    End Sub
    Private Sub setUI(val As Boolean)
        GroupLed.Enabled = val
        Analog1.Enabled = val
    End Sub
    Private Function connect2selected() As Boolean
        Dim wrk As String = ListBox1.SelectedItem
        If IsNothing(wrk) Then Return False
        Try
            MyCom = My.Computer.Ports.OpenSerialPort(wrk, _
                                                     9600, _
                                                     IO.Ports.Parity.None, _
                                                     8, _
                                                     IO.Ports.StopBits.One)
        Catch ex As Exception
            MessageBox.Show("Port Com *" & wrk & "* cannot be activated")
            Return False
        End Try
        '
        purgeinit()
        Return True
    End Function
    Private Sub purgeinit()
        Try
            MyCom.DiscardInBuffer()
            MyCom.DiscardOutBuffer()
            MyCom.ReadTimeout = 5000
            Dim init As String = MyCom.ReadLine()   ' purge du message d'init
            MyCom.ReadTimeout = 1000
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub
    Private Sub Analog_Click(sender As Object, e As EventArgs) Handles Analog.Click
        Trigger.Value = spox.GetAnalog(MyCom)
    End Sub

    Private Sub TrigLed1_Click(sender As Object, e As EventArgs) Handles TrigLed1.Click
        Dim rep As Integer
        rep = spox.SetTrigger(MyCom, "1", Trigger.Value)
    End Sub

    Private Sub TrigLed2_Click(sender As Object, e As EventArgs) Handles TrigLed2.Click
        Dim rep As Integer
        rep = spox.SetTrigger(MyCom, "2", Trigger.Value)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim rep As Boolean
        Dim nb As Integer = My.Computer.Ports.SerialPortNames.Count
        If IsNothing(MyCom) Or connected = False Then
            If nb <> portlastcount Then GetSerialPortNames()
            Return
        Else
            rep = spox.ReadAlarm(MyCom)
            If rep Then
                Led3.Image = My.Resources.ResourceManager.GetObject("Ona")
            Else
                Led3.Image = My.Resources.ResourceManager.GetObject("Off")
            End If
            Isdark.Visible = False
            If spox.GetLed(MyCom, "1") Then
                xled1.SetOn()
                If xled2.ison Then Isdark.Visible = True
            Else
                xled1.SetOff()
                Isdark.Visible = False
            End If
            If spox.GetLed(MyCom, "2") Then
                xled2.SetOn()
                If xled1.ison Then Isdark.Visible = True
            Else
                xled2.SetOff()
                Isdark.Visible = False
            End If
        End If
    End Sub

    Private Sub Led1_Click(sender As Object, e As EventArgs) Handles Led1.Click
        Dim rep As String
        If xled1.ison Then
            rep = spox.SetLed(MyCom, "1", False)
        Else
            rep = spox.SetLed(MyCom, "1", True)
        End If
    End Sub

    Private Sub Led2_Click(sender As Object, e As EventArgs) Handles Led2.Click
        Dim rep As String
        If xled2.ison Then
            rep = spox.SetLed(MyCom, "2", False)
        Else
            rep = spox.SetLed(MyCom, "2", True)
        End If
    End Sub
End Class

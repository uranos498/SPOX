Imports System.Windows.forms
<ComClass(Spox_Dll.ClassId, Spox_Dll.InterfaceId, Spox_Dll.EventsId)> _
Public Class Spox_Dll
    ' Description:	This is the Dll Class used to speak to the Arduino part of the Spox Product
    '               Spox is a prodcut of "Sheliak Instruments"
    '               The Spox box produced by Sheliack to automatize calibration operation 
    '               with the calibration addon for Alpy or LyresIII spectrograths
    '
    'DLL interface
    '
    '1- function SetLed(com,led#, state)returns led# state(on/off) if led#=0 all leds
    '2- function GetAnalog(com)         returns analog value read on pin A0
    '3- function SetTrigger(Com,led#, TriggerValue)  Trigger value [0-1023] returns  
    '4- function ReadAlarm(com)         returns Alarm state (on/off)
    '5- function ReadLed(Com,led#)      returns led# state(on/off) if led#=0 all leds
    '
    '
    'Basic (to Arduino)
    '    n0+rclf : Contact n to Off ; return n0+rclf / error                x
    '    n1+rclf : Contact n to On ; return n1+rclf / error                 x
    'Extensions:
    '    n?+rclf  : Query state for contact n. ; return nx+rclf / error
    '    0A+rclf  : Query state of analog Pin (0-1023)
    '    nAdddd+rclf : set alarm level (dddd) for way n
    '    0X+rclf  : Query state of Alarm Led (0-1023)    reply X0 or X1
    '    00+rclf  : All contacts off ; return 00+rclf / error               x
    'Error/identification
    '    SPOX+rclf  is returned by Arduino ie device name is given on invalid query
    '  
    ' Implements:	Standart .net Interface and proprietary protocol
    ' Author:		(JPG) Jean-Paul GODARD <Jean.Paul.Godard@gmail.com>
    '
    ' Edit Log:
    '
    ' Date			Who	Vers	Description
    ' -----------	---	-----	-------------------------------------------------------
    ' 30-03-2015	JPG	1.0.0	Initial edit, from Switch template
    ' ---------------------------------------------------------------------------------

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "8f3f18a7-9a39-40cb-9c38-808dd88bf902"
    Public Const InterfaceId As String = "2a7e6e77-6d75-40fd-8812-c5406d56f861"
    Public Const EventsId As String = "5f88071b-a6c9-49c0-ad6c-ca5826ec3b24"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub
    ''' <summary>
    ''' Set the referenced Led.
    ''' </summary>
    ''' <param name="com">The connected com port.</param>
    ''' <param name="led">The # on the concerned Led [1,2].</param>
    ''' <param name="State">The state to set [On=true, Off=False].</param>
    Public Function SetLed(ByRef com As IO.Ports.SerialPort, ByRef led As Char, ByRef state As Boolean) As Boolean
        Dim ret As Boolean = False
        Dim data As String = (led)
        Dim vstat As String = "0"
        If state Then vstat = "1"
        data = data & vstat & "#"
        Try
            SendSerialData(com, data)
            data = ReceiveSerialData(com)
        Catch ex As Exception
            MessageBox.Show("SetLed.Spox.dll" & vbCrLf & ex.Message & vbCrLf & ex.HelpLink, "Error")
        End Try
        If data.ElementAt(1) = "1" Then ret = True
        Return ret
    End Function
    ''' <summary>
    ''' Get the state of the referenced Led.
    ''' </summary>
    ''' <param name="com">The connected com port.</param>
    ''' <param name="led">The # on the concerned Led [1,2].</param>
    Public Function GetLed(ByRef com As IO.Ports.SerialPort, ByRef led As Char) As Boolean
        Dim ret As Boolean = False
        Dim data As String = (led)
        data = data & "?#"
        Try
            SendSerialData(com, data)
            data = ReceiveSerialData(com)
        Catch ex As Exception
            MessageBox.Show("GetLed.Spox.dll" & vbCrLf & ex.Message & vbCrLf & ex.HelpLink, "Error")
        End Try
        If data.ElementAt(1) = "1" Then ret = True
        Return ret
    End Function
    ''' <summary>
    ''' Get the current Analog Value.
    ''' </summary>
    ''' <param name="com">The connected com port.</param>
    Public Function GetAnalog(ByRef com As IO.Ports.SerialPort) As Integer
        '    0A+rclf  : Query state of analog Pin (0-1023)
        Dim ret As Integer = -1
        Dim data As String = "0A"
        Try
            SendSerialData(com, data)
            data = ReceiveSerialData(com)
        Catch ex As Exception
            MessageBox.Show("GetAnalog.Spox.dll" & vbCrLf & ex.Message & vbCrLf & ex.HelpLink, "Error")
        End Try
        ret = Convert.ToInt16(data.Remove(0, 2))        ' remove leading characters
        Return ret
    End Function
    ''' <summary>
    ''' Set the trigger value for led#.
    ''' </summary>
    ''' <param name="com">The connected com port.</param>
    ''' <param name="led">The # on the concerned Led [1,2].</param>
    ''' <param name="TriggerValue">The The value of the Alarl Trigger on this way.</param>
    Public Function SetTrigger(ByRef Com As IO.Ports.SerialPort, ByRef led As Char, ByRef TriggerValue As Integer) As Integer
        '    nAdddd+rclf : set alarm level (dddd) for way n
        Dim data As String
        Dim ret As Integer = -1
        data = (led) & "A" & TriggerValue.ToString("0000")  ' sur 4 chiffres
        Try
            SendSerialData(Com, data)
            data = ReceiveSerialData(Com)
        Catch ex As Exception
            MessageBox.Show("SetTrigger.Spox.dll" & vbCrLf & ex.Message & vbCrLf & ex.HelpLink, "Error")
            Return ret
        End Try
        ret = 1
        Return ret
    End Function
    ''' <summary>
    ''' Read the alarm State (boolean).
    ''' </summary>
    ''' <param name="com">The connected com port.</param>
    Public Function ReadAlarm(ByRef com As IO.Ports.SerialPort) As Boolean
        Dim ret As Boolean = False
        Dim data As String = "0X"
        Try
            SendSerialData(com, data)
            data = ReceiveSerialData(com)
        Catch ex As Exception
            MessageBox.Show("ReadAlarm.Spox.dll" & vbCrLf & ex.Message & vbCrLf & ex.HelpLink, "Error")
        End Try
        If data.ElementAt(1) = "1" Then ret = True
        Return ret
    End Function
    Private Sub checkom(com)
        If com Is Nothing Then
            MessageBox.Show("Erreur COM Inex", "Error")
        End If
    End Sub
    Private Function ReceiveSerialData(com As IO.Ports.SerialPort) As String
        ' Receive strings from a serial port.
        Dim returnStr As String = ""
        Try
            'Do
            Dim Incoming As String = com.ReadLine()
            returnStr = Incoming
        Catch ex As TimeoutException
            returnStr = "Error: Serial Port read timed out."
        Finally
            'If MyCom IsNot Nothing Then MyCom.Close()
        End Try

        Return returnStr & vbCrLf
    End Function
    Private Sub SendSerialData(ByRef com As IO.Ports.SerialPort, ByVal data As String)
        ' Send strings to a serial port.
        com.DiscardInBuffer()
        com.DiscardOutBuffer()
        com.WriteLine(data)
    End Sub
End Class



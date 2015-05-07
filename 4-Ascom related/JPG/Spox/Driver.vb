'tabs=4
' --------------------------------------------------------------------------------
' TODO fill in this information for your driver, then remove this line!
'
' ASCOM Switch driver for Spox
'
' Description:	Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam 
'				nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
'				erat, sed diam voluptua. At vero eos et accusam et justo duo 
'				dolores et ea rebum. Stet clita kasd gubergren, no sea takimata 
'				sanctus est Lorem ipsum dolor sit amet.
'
' Implements:	ASCOM Switch interface version: 1.0
' Author:		(XXX) Your N. Here <your@email.here>
'
' Edit Log:
'
' Date			Who	Vers	Description
' -----------	---	-----	-------------------------------------------------------
' dd-mmm-yyyy	XXX	1.0.0	Initial edit, from Switch template
' ---------------------------------------------------------------------------------
'
'
' Your driver's ID is ASCOM.Spox.Switch
'
' The Guid attribute sets the CLSID for ASCOM.DeviceName.Switch
' The ClassInterface/None addribute prevents an empty interface called
' _Switch from being created and used as the [default] interface
'

' This definition is used to select code that's only applicable for one device type
#Const Device = "Switch"

Imports ASCOM
Imports ASCOM.Astrometry
Imports ASCOM.Astrometry.AstroUtils
Imports ASCOM.DeviceInterface
Imports ASCOM.Utilities

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Text

<Guid("ac4777df-7b8a-4d81-9b48-0ca123c64396")> _
<ClassInterface(ClassInterfaceType.None)> _
Public Class Switch

    ' The Guid attribute sets the CLSID for ASCOM.Spox.Switch
    ' The ClassInterface/None addribute prevents an empty interface called
    ' _Spox from being created and used as the [default] interface

    ' TODO Replace the not implemented exceptions with code to implement the function or
    ' throw the appropriate ASCOM exception.
    '
    Implements ISwitchV2

    '
    ' Driver ID and descriptive string that shows in the Chooser
    '
    Friend Shared driverID As String = "ASCOM.Spox.Switch"
    Private Shared driverDescription As String = "Spox Switch"

    Friend Shared comPortProfileName As String = "COM Port" 'Constants used for Profile persistence
    Friend Shared traceStateProfileName As String = "Trace Level"
    Friend Shared comPortDefault As String = "COM1"
    Friend Shared traceStateDefault As String = "False"

    Friend Shared comPort As String ' Variables to hold the currrent device configuration
    Friend Shared traceState As Boolean

    Private connectedState As Boolean ' Private variable to hold the connected state
    Private utilities As Util ' Private variable to hold an ASCOM Utilities object
    Private astroUtilities As AstroUtils ' Private variable to hold an AstroUtils object to provide the Range method
    Private TL As TraceLogger ' Private variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
    '
    Public Const psep As String = "|"
    Private MyCom As ASCOM.Utilities.Serial
    '
    ' Constructor - Must be public for COM registration!
    '
    Public Sub New()

        ReadProfile() ' Read device configuration from the ASCOM Profile store
        TL = New TraceLogger("", "Spox")
        TL.Enabled = traceState
        TL.LogMessage("Switch", "Starting initialisation")

        connectedState = False ' Initialise connected to false
        utilities = New Util() ' Initialise util object
        astroUtilities = New AstroUtils 'Initialise new astro utiliites object

        'TODO: Implement your additional construction here

        TL.LogMessage("Switch", "Completed initialisation")
    End Sub

    '
    ' PUBLIC COM INTERFACE ISwitchV2 IMPLEMENTATION
    '

#Region "Common properties and methods"
    ''' <summary>
    ''' Displays the Setup Dialog form.
    ''' If the user clicks the OK button to dismiss the form, then
    ''' the new settings are saved, otherwise the old values are reloaded.
    ''' THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
    ''' </summary>
    Public Sub SetupDialog() Implements ISwitchV2.SetupDialog
        ' consider only showing the setup dialog if not connected
        ' or call a different dialog if connected
        If IsConnected Then
            System.Windows.Forms.MessageBox.Show("Already connected, just press OK")
        End If

        Using F As SetupDialogForm = New SetupDialogForm()
            Dim result As System.Windows.Forms.DialogResult = F.ShowDialog()
            If result = DialogResult.OK Then
                WriteProfile() ' Persist device configuration values to the ASCOM Profile store
            End If
        End Using
    End Sub

    Public ReadOnly Property SupportedActions() As ArrayList Implements ISwitchV2.SupportedActions
        Get
            TL.LogMessage("SupportedActions Get", "Returning empty arraylist")
            Return New ArrayList({"GETANALOG", "SETTRIGGER", "READALARM"})
        End Get
    End Property

    Public Function Action(ByVal ActionName As String, ByVal ActionParameters As String) As String Implements ISwitchV2.Action
        CheckConnected("Action")
        Dim params As System.String()
        params = Split(ActionName & ":", ":")
        Dim chi As String()
        chi = Split(ActionParameters & psep & psep & psep & psep, psep)
        Dim rest As String = ""
        Dim rebo As Boolean = False
        Select Case params(1)
            Case "GETANALOG"
                rest = GetAnalog()
                Return rest
            Case "SETTRIGGER"
                SetTrigger(chi(0), chi(1))
                Return rest
            Case "READALARM"
                rebo = ReadAlarm()
                If rebo Then Return "1" Else Return "0"
            Case Else
                Throw New ActionNotImplementedException("Action " & ActionName & " is not supported by this driver")
        End Select
    End Function

    Public Sub CommandBlind(ByVal Command As String, Optional ByVal Raw As Boolean = False) Implements ISwitchV2.CommandBlind
        CheckConnected("CommandBlind")
        ' Call CommandString and return as soon as it finishes
        Me.CommandString(Command, Raw)
        ' or
        Throw New MethodNotImplementedException("CommandBlind")
    End Sub

    Public Function CommandBool(ByVal Command As String, Optional ByVal Raw As Boolean = False) As Boolean _
        Implements ISwitchV2.CommandBool
        CheckConnected("CommandBool")
        Dim ret As String = CommandString(Command, Raw)
        ' TODO decode the return string and return true or false
        ' or
        Throw New MethodNotImplementedException("CommandBool")
    End Function

    Public Function CommandString(ByVal Command As String, Optional ByVal Raw As Boolean = False) As String _
        Implements ISwitchV2.CommandString
        CheckConnected("CommandString")
        ' it's a good idea to put all the low level communication with the device here,
        ' then all communication calls this function
        ' you need something to ensure that only one command is in progress at a time
        Throw New MethodNotImplementedException("CommandString")
    End Function

    Public Property Connected() As Boolean Implements ISwitchV2.Connected
        Get
            TL.LogMessage("Connected Get", IsConnected.ToString())
            Return IsConnected
        End Get
        Set(value As Boolean)
            TL.LogMessage("Connected Set", value.ToString())
            If value = IsConnected Then
                Return
            End If

            If value Then
                connectedState = True
                TL.LogMessage("Connected Set", "Connecting to port " + comPort)
                ' TODO connect to the device
                Try
                    'Create the serial port
                    MyCom = New ASCOM.Utilities.Serial
                    MyCom.PortName = Switch.comPort
                    MyCom.Speed = ASCOM.Utilities.SerialSpeed.ps9600 'Set to baud rate 9600, choose an appropriate value for your system
                    MyCom.ReceiveTimeoutMs = 5000 'Set the time (in milli-seconds) that the receive commands will wait before returning a timeout exception

                    'Set serial format and serial line parameters as required
                    'Following values are the defaults so you don't need to set them if they work OK with your device
                    MyCom.DataBits = 8 ' Set 8 data bits
                    MyCom.Parity = IO.Ports.Parity.None 'Set no parity bit
                    MyCom.StopBits = IO.Ports.StopBits.One 'Set one stop bit
                    'MyCom.DTREnable = True 'Set the DTR line high
                    MyCom.Handshake = IO.Ports.Handshake.None 'Don't use hardware or software flow control on the serial line
                    'Create the serial port and ready it for use
                    MyCom.Connected = True
                    MyCom.ClearBuffers()
                    MyCom.ReceiveTimeoutMs = 5000 'Set the time (in milli-seconds) that the receive commands will wait before returning a timeout exception
                    Dim Incoming As String = MyCom.ReceiveTerminated(vbLf)  ' Reception du message d'init Spox
                    MyCom.ReceiveTimeoutMs = 1000 'Set the time (in milli-seconds) that the receive commands will wait before returning a timeout exception
                Catch ex As Exception

                End Try

            Else
                connectedState = False
                TL.LogMessage("Connected Set", "Disconnecting from port " + comPort)
                'Clean up and release the serial port
                MyCom.Connected = False 'Disconnect the serial port
                MyCom.Dispose() 'Release serial port resources
                MyCom = Nothing
            End If
        End Set
    End Property

    Public ReadOnly Property Description As String Implements ISwitchV2.Description
        Get
            ' this pattern seems to be needed to allow a public property to return a private field
            Dim d As String = driverDescription
            TL.LogMessage("Description Get", d)
            Return d
        End Get
    End Property

    Public ReadOnly Property DriverInfo As String Implements ISwitchV2.DriverInfo
        Get
            Dim m_version As Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
            ' TODO customise this driver description
            Dim s_driverInfo As String = "Information about the driver itself. Version: " + m_version.Major.ToString() + "." + m_version.Minor.ToString()
            TL.LogMessage("DriverInfo Get", s_driverInfo)
            Return s_driverInfo
        End Get
    End Property

    Public ReadOnly Property DriverVersion() As String Implements ISwitchV2.DriverVersion
        Get
            ' Get our own assembly and report its version number
            TL.LogMessage("DriverVersion Get", Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString(2))
            Return Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString(2)
        End Get
    End Property

    Public ReadOnly Property InterfaceVersion() As Short Implements ISwitchV2.InterfaceVersion
        Get
            TL.LogMessage("InterfaceVersion Get", "2")
            Return 2
        End Get
    End Property

    Public ReadOnly Property Name As String Implements ISwitchV2.Name
        Get
            Dim s_name As String = "Spox Driver"
            TL.LogMessage("Name Get", s_name)
            Return s_name
        End Get
    End Property

    Public Sub Dispose() Implements ISwitchV2.Dispose
        ' Clean up the tracelogger and util objects
        TL.Enabled = False
        TL.Dispose()
        TL = Nothing
        utilities.Dispose()
        utilities = Nothing
        astroUtilities.Dispose()
        astroUtilities = Nothing
    End Sub

#End Region

#Region "ISwitchV2 Implementation"

    Dim numSwitches As Short = 2

    ''' <summary>
    ''' The number of switches managed by this driver
    ''' </summary>
    Public ReadOnly Property MaxSwitch As Short Implements ISwitchV2.MaxSwitch
        Get
            TL.LogMessage("MaxSwitch Get", numSwitches.ToString())
            Return numSwitches
        End Get
    End Property

    ''' <summary>
    ''' Return the name of switch n
    ''' </summary>
    ''' <param name="id">The switch number to return</param>
    ''' <returns>The name of the switch</returns>
    Public Function GetSwitchName(id As Short) As String Implements ISwitchV2.GetSwitchName
        Validate("GetSwitchName", id)
        If id = 0 Then Return "1"
        If id = 1 Then Return "2"
        TL.LogMessage("GetSwitchName", "Not Implemented")
        Throw New ASCOM.MethodNotImplementedException("GetSwitchName")
    End Function

    ''' <summary>
    ''' Sets a switch name to a specified value
    ''' </summary>
    ''' <param name="id">The number of the switch whose name is to be set</param>
    ''' <param name="name">The name of the switch</param>
    Sub SetSwitchName(id As Short, name As String) Implements ISwitchV2.SetSwitchName
        Validate("SetSwitchName", id)
        TL.LogMessage("SetSwitchName", "Not Implemented")
        Throw New ASCOM.MethodNotImplementedException("SetSwitchName")
    End Sub

    ''' <summary>
    ''' Gets the description of the specified switch. This is to allow a fuller description of
    ''' the switch to be returned, for example for a tool tip.
    ''' </summary>
    ''' <param name="id">The number of the switch whose description is to be returned</param><returns></returns>
    ''' <exception cref="MethodNotImplementedException">If the method is not implemented</exception>
    ''' <exception cref="InvalidValueException">If id is outside the range 0 to MaxSwitch - 1</exception>
    Public Function GetSwitchDescription(id As Short) As String Implements ISwitchV2.GetSwitchDescription
        Validate("GetSwitchDescription", id)
        If id = 0 Then Return "1- Calibration"
        If id = 1 Then Return "2- Flat Exposure"
        TL.LogMessage("GetSwitchDescription", "Not Implemented")
        Throw New ASCOM.MethodNotImplementedException("GetSwitchDescription")
    End Function

    ''' <summary>
    ''' Reports if the specified switch can be written to.
    ''' This is false if the switch cannot be written to, for example a limit switch or a sensor.
    ''' The default is true.
    ''' </summary>
    ''' <param name="id">The number of the switch whose write state is to be returned</param><returns>
    '''   <c>true</c> if the switch can be set, otherwise <c>false</c>.
    ''' </returns>
    ''' <exception cref="MethodNotImplementedException">If the method is not implemented</exception>
    ''' <exception cref="InvalidValueException">If id is outside the range 0 to MaxSwitch - 1</exception>
    Public Function CanWrite(id As Short) As Boolean Implements ISwitchV2.CanWrite
        Validate("CanWrite", id)
        TL.LogMessage("CanWrite", "Default true")
        Return True
    End Function

#Region "boolean members"
    ''' <summary>
    ''' Return the state of switch n as a boolean
    ''' A multi-value switch must throw a MethodNotImplementedException.
    ''' </summary>
    ''' <param name="id">The switch number to return</param>
    ''' <returns>True or false</returns>
    Function GetSwitch(id As Short) As Boolean Implements ISwitchV2.GetSwitch
        Validate("GetSwitch", id, True)
        If id = 0 Then Return GetLed("1")
        If id = 1 Then Return GetLed("2")
        TL.LogMessage("GetSwitch", "Not Implemented")
        Throw New ASCOM.MethodNotImplementedException("GetSwitch")
    End Function

    ''' <summary>
    ''' Sets a switch to the specified state, true or false.
    ''' If the switch cannot be set then throws a MethodNotImplementedException.
    ''' A multi-value switch must throw a MethodNotImplementedException.
    ''' </summary>
    ''' <param name="ID">The number of the switch to set</param>
    ''' <param name="State">The required switch state</param>
    Sub SetSwitch(id As Short, state As Boolean) Implements ISwitchV2.SetSwitch
        Validate("SetSwitch", id, True)
        If id = 0 Then
            SetLed("1", state)
            Return
        End If

        If id = 1 Then
            SetLed("2", state)
            Return
        End If
        TL.LogMessage("SetSwitch", "Not Implemented")
        Throw New ASCOM.MethodNotImplementedException("SetSwitch")
    End Sub

#End Region

#Region "Analogue members"
    ''' <summary>
    ''' returns the maximum analogue value for this switch
    ''' boolean switches must return 1.0
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function MaxSwitchValue(id As Short) As Double Implements ISwitchV2.MaxSwitchValue
        Validate("MaxSwitchValue", id)
        If id = 0 Then Return 1.0
        If id = 1 Then Return 1.0
        TL.LogMessage("MaxSwitchValue", "Not Implemented")
        Throw New ASCOM.MethodNotImplementedException("MaxSwitchValue")
    End Function

    ''' <summary>
    ''' returns the minimum analogue value for this switch
    ''' boolean switches must return 0.0
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function MinSwitchValue(id As Short) As Double Implements ISwitchV2.MinSwitchValue
        Validate("MinSwitchValue", id)
        If id = 0 Then Return 0.0
        If id = 1 Then Return 0.0
        TL.LogMessage("MinSwitchValue", "Not Implemented")
        Throw New ASCOM.MethodNotImplementedException("MinSwitchValue")
    End Function

    ''' <summary>
    ''' returns the step size that this switch supports. This gives the difference between
    ''' successive values of the switch.
    ''' The number of values is ((MaxSwitchValue - MinSwitchValue) / SwitchStep) + 1
    ''' boolean switches must return 1.0, giving two states.
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function SwitchStep(id As Short) As Double Implements ISwitchV2.SwitchStep
        Validate("SwitchStep", id)
        If id = 0 Then Return 1.0
        If id = 1 Then Return 1.0
        TL.LogMessage("SwitchStep", "Not Implemented")
        Throw New ASCOM.MethodNotImplementedException("SwitchStep")
    End Function

    ''' <summary>
    ''' returns the analogue switch value for switch id
    ''' boolean switches must throw a MethodNotImplementedException
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetSwitchValue(id As Short) As Double Implements ISwitchV2.GetSwitchValue
        Validate("GetSwitchValue", id, False)
        TL.LogMessage("GetSwitchValue", "Not Implemented")
        Throw New ASCOM.MethodNotImplementedException("GetSwitchValue")
    End Function

    ''' <summary>
    ''' set the analogue value for this switch.
    ''' If the switch cannot be set then throws a MethodNotImplementedException.
    ''' If the value is not between the maximum and minimum then throws an InvalidValueException
    ''' boolean switches must throw a MethodNotImplementedException
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="value"></param>
    Sub SetSwitchValue(id As Short, value As Double) Implements ISwitchV2.SetSwitchValue
        Validate("SetSwitchValue", id, value)
        If value < MinSwitchValue(id) Or value > MaxSwitchValue(id) Then
            Throw New InvalidValueException("", value.ToString(), String.Format("{0} to {1}", MinSwitchValue(id), MaxSwitchValue(id)))
        End If
        TL.LogMessage("SetSwitchValue", "Not Implemented")
        Throw New ASCOM.MethodNotImplementedException("SetSwitchValue")
    End Sub

#End Region
#End Region

    ''' <summary>
    ''' Checks that the switch id is in range and throws an InvalidValueException if it isn't
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="id">The id.</param>
    Private Sub Validate(message As String, id As Short)
        If (id < 0 Or id >= numSwitches) Then
            Throw New ASCOM.InvalidValueException(message, id.ToString(), String.Format("0 to {0}", numSwitches - 1))
        End If
    End Sub

    ''' <summary>
    ''' Checks that the number of states for the switch is correct and throws a methodNotImplemented exception if not.
    ''' Boolean switches must have 2 states and multi-value switches more than 2.
    ''' </summary>
    ''' <param name="message"></param>
    ''' <param name="id"></param>
    ''' <param name="expectBoolean"></param>
    Private Sub Validate(message As String, id As Short, expectBoolean As Boolean)
        Validate(message, id)
        Dim ns As Integer = (((MaxSwitchValue(id) - MinSwitchValue(id)) / SwitchStep(id)) + 1)
        If (expectBoolean And ns <> 2) Or (Not expectBoolean And ns <= 2) Then
            TL.LogMessage(message, String.Format("Switch {0} has the wriong number of states", id, ns))
            Throw New MethodNotImplementedException(String.Format("{0}({1})", message, id))
        End If
    End Sub

    ''' <summary>
    ''' Checks that the switch id and value are in range and throws an
    ''' InvalidValueException if they are not.
    ''' </summary>
    ''' <param name="message">The message.</param>
    ''' <param name="id">The id.</param>
    ''' <param name="value">The value.</param>
    Private Sub Validate(message As String, id As Short, value As Double)
        Validate(message, id, False)
        Dim min = MinSwitchValue(id)
        Dim max = MaxSwitchValue(id)
        If (value < min Or value > max) Then
            TL.LogMessage(message, String.Format("Value {1} for Switch {0} is out of the allowed range {2} to {3}", id, value, min, max))
            Throw New InvalidValueException(message, value.ToString(), String.Format("Switch({0}) range {1} to {2}", id, min, max))
        End If
    End Sub

#Region "My Functions"
    Private Sub SetLed(led, state)
        Dim ret As Boolean = False
        Dim data As String = (led)
        Dim vstat As String = "0"
        If state Then vstat = "1"
        data = data & vstat & "#"
        Try
            SendSerialData(data)
            data = ReceiveSerialData()
        Catch ex As Exception
            Throw New DriverAccessCOMException("Com Port not connected", 1004, ex)
            'MessageBox.Show("SetLed.Spox.dll" & vbCrLf & ex.Message & vbCrLf & ex.HelpLink, "Error")
        End Try
        If data.Substring(1, 1) = "1" Then ret = True
        'Return ret
    End Sub
    Private Function GetLed(led) As Boolean
        Dim ret As Boolean = False
        Dim data As String = (led)
        data = data & "?#"
        Try
            SendSerialData(data)
            data = ReceiveSerialData()
        Catch ex As Exception
            Throw New DriverAccessCOMException("Com Port not connected", 1002, ex)
            'MessageBox.Show("GetLed.Spox.dll" & vbCrLf & ex.Message & vbCrLf & ex.HelpLink, "Error")
        End Try
        If data.Substring(1, 1) = "1" Then ret = True
        Return ret
    End Function
    Private Function GetAnalog() As Integer
        '    0A+rclf  : Query state of analog Pin (0-1023)
        Dim ret As Integer = -1
        Dim data As String = "0A"
        Try
            SendSerialData(data)
            data = ReceiveSerialData()
        Catch ex As Exception
            Throw New DriverAccessCOMException("Com Port not connected", 1003, ex)
            'MessageBox.Show("GetAnalog.Spox.dll" & vbCrLf & ex.Message & vbCrLf & ex.HelpLink, "Error")
        End Try
        ret = Convert.ToInt16(data.Remove(0, 2))        ' remove leading characters
        Return ret
    End Function
    Private Function SetTrigger(led As Char, TriggerValue As Integer) As Boolean
        '    nAdddd+rclf : set alarm level (dddd) for way n
        Dim data As String
        Dim ret As Integer = -1
        data = (led) & "A" & TriggerValue.ToString("0000")  ' sur 4 chiffres
        Try
            SendSerialData(data)
            data = ReceiveSerialData()
        Catch ex As Exception
            MessageBox.Show("SetTrigger.Spox.dll" & vbCrLf & ex.Message & vbCrLf & ex.HelpLink, "Error")
            Return ret
        End Try
        ret = 1
        Return ret
    End Function
    Private Function ReadAlarm() As Boolean
        Dim ret As Boolean = False
        Dim data As String = "0X"
        Try
            SendSerialData(data)
            data = ReceiveSerialData()
        Catch ex As Exception
            Throw New DriverAccessCOMException("Com Port not connected", 1001, ex)
            'MessageBox.Show("ReadAlarm.Spox.dll" & vbCrLf & ex.Message & vbCrLf & ex.HelpLink, "Error")
        End Try
        If data.Substring(1, 1) = "1" Then ret = True
        Return ret
    End Function
    Private Function ReceiveSerialData() As String
        ' Receive strings from a serial port.
        Dim returnStr As String = ""
        Try
            'Do
            Dim Incoming As String = MyCom.ReceiveTerminated(vbLf)  ' le lf du vbrclf
            returnStr = Trimx(Incoming)
        Catch ex As TimeoutException
            returnStr = "Error: Serial Port read timed out."
        Finally
            'If MyCom IsNot Nothing Then MyCom.Close()
        End Try

        Return returnStr & vbCrLf
    End Function
    Private Function trimx(s As String) As String
        For Each c As Char In {"#", "\r", "\n"}
            s = s.Replace(c, "")
        Next
        Return s
    End Function
    Private Sub SendSerialData(ByVal data As String)
        ' Send strings to a serial port.
        MyCom.Transmit(data & vbCrLf)
    End Sub
#End Region

#Region "Private properties and methods"
    ' here are some useful properties and methods that can be used as required
    ' to help with

#Region "ASCOM Registration"

    Private Shared Sub RegUnregASCOM(ByVal bRegister As Boolean)

        Using P As New Profile() With {.DeviceType = "Switch"}
            If bRegister Then
                P.Register(driverID, driverDescription)
            Else
                P.Unregister(driverID)
            End If
        End Using

    End Sub

    <ComRegisterFunction()> _
    Public Shared Sub RegisterASCOM(ByVal T As Type)

        RegUnregASCOM(True)

    End Sub

    <ComUnregisterFunction()> _
    Public Shared Sub UnregisterASCOM(ByVal T As Type)

        RegUnregASCOM(False)

    End Sub

#End Region

    ''' <summary>
    ''' Returns true if there is a valid connection to the driver hardware
    ''' </summary>
    Private ReadOnly Property IsConnected As Boolean
        Get
            ' TODO check that the driver hardware connection exists and is connected to the hardware
            Return connectedState
        End Get
    End Property

    ''' <summary>
    ''' Use this function to throw an exception if we aren't connected to the hardware
    ''' </summary>
    ''' <param name="message"></param>
    Private Sub CheckConnected(ByVal message As String)
        If Not IsConnected Then
            Throw New NotConnectedException(message)
        End If
    End Sub

    ''' <summary>
    ''' Read the device configuration from the ASCOM Profile store
    ''' </summary>
    Friend Sub ReadProfile()
        Using driverProfile As New Profile()
            driverProfile.DeviceType = "Switch"
            traceState = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, String.Empty, traceStateDefault))
            comPort = driverProfile.GetValue(driverID, comPortProfileName, String.Empty, comPortDefault)
        End Using
    End Sub

    ''' <summary>
    ''' Write the device configuration to the  ASCOM  Profile store
    ''' </summary>
    Friend Sub WriteProfile()
        Using driverProfile As New Profile()
            driverProfile.DeviceType = "Switch"
            driverProfile.WriteValue(driverID, traceStateProfileName, traceState.ToString())
            driverProfile.WriteValue(driverID, comPortProfileName, comPort.ToString())
        End Using

    End Sub

#End Region

End Class

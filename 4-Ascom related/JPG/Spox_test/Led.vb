Public Class led
    'classe permettant de faire des allumages extinction de leds en fonction des appuis boutons
    'The led Object is defined for an OO approac (Object Oriented)
    '

    Dim name As String              ' carry the name of the instancied object (otio)
    Dim pict As Object              ' the windows form container otio
    Dim PIs_On As Boolean           ' the primary state (on/off) otio
    Dim blinking As Boolean         ' the secondary state (blinking/stable) otio
    Dim alarm As Boolean            ' the tertiary state (blinking/stable) otio

    Public Sub New(ByRef myname As String, ByRef ThisObj As Object, ByRef code As Char)
        ' instanciation of an object
        ' required: Name, control name, color 
        name = myname
        pict = ThisObj
        PIs_On = False
        blinking = False
        alarm = False
        Me.SetOff()                     'force "Off" State
        ' copy some definition to the driver

    End Sub
    Public Sub SetOn()                  ' set display on
        Me.pict.Image = My.Resources.ResourceManager.GetObject("ona")
        Me.PIs_On = True                ' Memorize state
        ' ask the driver to act

    End Sub
    Public Sub setblink()               ' Ask for blinking the display
        Me.blinking = True              ' Memorize state
        ' ask the driver to act

    End Sub
    Public Sub offblink()               ' Ask for stable display
        Me.blinking = False             ' Ask for blinking the display
        ' ask the driver to act

    End Sub
    Public Sub Alarm_set(ByVal val As Boolean)
        Me.alarm = val                  ' Ask for blinking the display
        ' ask the driver to act

    End Sub
    Public Sub SetOff()
        Me.PIs_On = False
        Me.pict.Image = My.Resources.ResourceManager.GetObject("off")
        Me.blinking = False             ' Reset potential blinking
        Me.alarm = False
        ' ask the driver to act

    End Sub
    Public Sub blink(ByVal phase As Boolean)     ' receive phase of the Blinker and apply if req
        If Me.PIs_On And (Me.blinking Or Me.alarm) Then
            If phase Then
                Me.pict.Image = My.Resources.ResourceManager.GetObject("ona")
            Else
                Me.pict.Image = My.Resources.ResourceManager.GetObject("off")
            End If
        End If
        ' 
    End Sub
    Public ReadOnly Property is_on()        'Read primary state
        Get
            Return Me.PIs_On
        End Get
    End Property
    Public ReadOnly Property is_blinking()  'Read secondary state
        Get
            Return Me.blinking
        End Get
    End Property
    Protected Overrides Sub Finalize()
        ' rien de spécial
    End Sub
End Class

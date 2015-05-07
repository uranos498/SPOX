Public Class Ledx
    Dim name As String              ' carry the name of the instancied object (otio)
    Dim pict As PictureBox              ' the windows form container otio
    Public ison As Boolean
    Dim _code As Char

    Public Sub New(ByRef myname As String, ByRef ThisObj As PictureBox, ByRef code As Char)
        ' instanciation of an object
        ' required: Name, control name, color 
        name = myname
        pict = ThisObj
        ison = False
        ' copy some definition to the driver
    End Sub
    Public Sub SetOn()                  ' set display on
        Me.pict.Image = My.Resources.ResourceManager.GetObject("Ona")
        ' ask the driver to act
        ison = True
    End Sub
    Public Sub SetOff()                  ' set display on
        Me.pict.image = My.Resources.ResourceManager.GetObject("Off")
        ' ask the driver to act
        ison = False
    End Sub
End Class

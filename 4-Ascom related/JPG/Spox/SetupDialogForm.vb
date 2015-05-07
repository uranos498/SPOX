Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ASCOM.Utilities
Imports ASCOM.Spox

<ComVisible(False)> _
Public Class SetupDialogForm

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click ' OK button event handler
        ' Persist new values of user settings to the ASCOM profile
        Switch.comPort = ListBox1.SelectedItem ' Update the state variables with results from the dialogue
        Switch.traceState = chkTrace.Checked
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click 'Cancel button event handler
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ShowAscomWebPage(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick, PictureBox1.Click
        ' Click on ASCOM logo event handler
        Try
            System.Diagnostics.Process.Start("http://ascom-standards.org/")
        Catch noBrowser As System.ComponentModel.Win32Exception
            If noBrowser.ErrorCode = -2147467259 Then
                MessageBox.Show(noBrowser.Message)
            End If
        Catch other As System.Exception
            MessageBox.Show(other.Message)
        End Try
    End Sub

    Private Sub SetupDialogForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load ' Form load event handler
        ' Retrieve current values of user settings from the ASCOM Profile 
        ' Show all available COM ports.
        Dim j As Integer = 0
        OK_Button.Enabled = True
        If My.Computer.Ports.SerialPortNames.Count = 0 Then
            OK_Button.Enabled = False
        Else
            Me.Enabled = True
            For Each sp As String In My.Computer.Ports.SerialPortNames
                ListBox1.Items.Add(sp)
            Next
            For i = 0 To My.Computer.Ports.SerialPortNames.Count - 1
                If ListBox1.Items(i) = Switch.comPort Then
                    j = i
                    Exit For
                End If
            Next
            ListBox1.SetSelected(j, True)
            chkTrace.Checked = Switch.traceState
        End If
    End Sub


End Class

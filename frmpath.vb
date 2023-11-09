Imports System
Imports System.Windows.Forms
Imports System.IO


Public Class frmpath
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\edp", "Server", path_hr.Text)
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\edp", "LocalFolder", tempf.Text)
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\edp", "log", ComboBox1.Text)
            MsgBox("Berhasil setting")
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub frmpath_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim path, temp, logs As String
        path = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "Server", Nothing)
        temp = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "LocalFolder", Nothing)
        logs = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "log", Nothing)
        path_hr.Text = path
        tempf.Text = temp
        ComboBox1.SelectedItem = logs
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fbd As New FolderBrowserDialog
        fbd.SelectedPath = ""

        Dim result As DialogResult = fbd.ShowDialog

        If result = Windows.Forms.DialogResult.Cancel Then
            fbd.SelectedPath = Nothing
            path_hr.Text = ""
        Else
            path_hr.Text = fbd.SelectedPath.Trim
            Dim folderInfo As New IO.DirectoryInfo(path_hr.Text)
            Dim ss As String
            ss = path_hr.Text.ToString.Replace("\", "/")
            path_hr.Text = ss

            If Microsoft.VisualBasic.Right(path_hr.Text, 1) = "/" Then
                path_hr.Text = ss
            Else
                path_hr.Text = ss + "/"
            End If
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim fbd As New FolderBrowserDialog
        fbd.SelectedPath = ""

        Dim result As DialogResult = fbd.ShowDialog

        If result = Windows.Forms.DialogResult.Cancel Then
            fbd.SelectedPath = Nothing
            tempf.Text = ""
        Else
            tempf.Text = fbd.SelectedPath.Trim
            Dim folderInfo As New IO.DirectoryInfo(tempf.Text)
            Dim ss As String
            ss = tempf.Text.ToString.Replace("\", "/")
            tempf.Text = ss

            If Microsoft.VisualBasic.Right(tempf.Text, 1) = "/" Then
                tempf.Text = ss
            Else
                tempf.Text = ss + "/"
            End If
        End If

    End Sub

End Class
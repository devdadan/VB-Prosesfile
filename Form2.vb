Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Environment
Imports ComponentAce.Compression.ZipForge
Imports ComponentAce.Compression.Archiver
Imports System.Data.OleDb
Imports System.Data.Odbc
Public Class Form2
    Public conn As New MySql.Data.MySqlClient.MySqlConnection
    Dim connstr As String
    Dim jam, menit, detik As Integer
    Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByRef lParam As Object) As Integer
    Const WM_LBUTTONDOWN = &H201
    Const WM_LBUTTONUP = &H202
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not conn Is Nothing Then conn.Close()
        connstr = String.Format("server=192.168.85.226;user id=root; password=5CCNQV3rio/dI/iboPPnww9nzUHh8bpac=fU59bpWfE4; database=wrc;")
        Try
            conn = New MySqlConnection(connstr)
            conn.Open()

            Button1.Enabled = False
            Timer2.Stop()

            Button1.Enabled = True
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        jamlabel.Text = Format(TimeOfDay, "HH:mm:ss")
        If jamlabel.Text = "00:00:05" Then
            period.Text = DateAdd("d", -1, Date.Now)
            period2.Text = Date.Now
            If period2.Text.ToString.Substring(0, 2) = "01" Then
                period2.Text = DateAdd("d", -1, Date.Now)
            End If
            Application.DoEvents()
            'MDIParent1.Tooltgljam.Text = Format(Date.Today, "dd-MM-yyyy")
        End If
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        If CheckBox1.Checked = True Then
            Timer2.Start()
        Else
            Timer2.Stop()
        End If

        If jamlabel.Text > jam1.Text And jamlabel.Text < jam2.Text Then
            detik = interval.Text
            detik -= 1
            If detik < 0 Then
                detik = interval.Text
            End If

            'interval.Text = Format(jam, "00") & ":" & Format(menit, "00") & ":" & Format(detik, "00")
            interval.Text = Format(Format(detik, "0"))

            If detik = 0 Then
                Timer2.Stop()
                'interval
                Dim reg_interval As String
                reg_interval = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "Interval", Nothing)
                interval.Text = reg_interval

                'excute(otoclik)
                SendMessage(Button1.Handle, WM_LBUTTONDOWN, 0, 0)
                Application.DoEvents()
                Threading.Thread.Sleep(150)
                SendMessage(Button1.Handle, WM_LBUTTONUP, 0, 0)
                Button1.PerformClick()

                Timer2.Start()
            End If
        End If
    End Sub


End Class
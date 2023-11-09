Imports MySql.Data.MySqlClient
Public Class Formdb
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Harap isi semua !")
        Else
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Prosesfile", "server", TextBox1.Text)
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Prosesfile", "user", TextBox2.Text)
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Prosesfile", "db", TextBox3.Text)
            My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Prosesfile", "pass", TextBox4.Text)

            FrmLoad.Show()
            Me.Hide()

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim str As String = "Server=" + TextBox1.Text + ";user id=" + TextBox2.Text + ";password=" + TextBox4.Text + ";database=" + TextBox3.Text + " "
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
                'MsgBox("Connection Succesfull")
                conn.Close()
                MsgBox("Koneksi sukses")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Formdb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Prosesfile", "server", Nothing)
        TextBox2.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Prosesfile", "user", Nothing)
        TextBox3.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Prosesfile", "db", Nothing)
        TextBox4.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Prosesfile", "pass", Nothing)
    End Sub
End Class
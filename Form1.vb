Public Class Form1
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Label3.Text = Format(Now, "MMM, ddd d")
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Me.Text = "ProsesFile.exe v." & Application.ProductVersion
        Label4.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Prosesfile", "server", Nothing)
        Label5.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Prosesfile", "db", Nothing)
        Label8.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Prosesfile", "user", Nothing)
        Label9.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\Prosesfile", "pass", Nothing)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel3.Controls.Clear()

        Dim transaksi As New UCtransaksi
        Panel3.Controls.Add(transaksi)


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel3.Controls.Clear()
        frmpath.Show()

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Formdb.Close()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Panel3.Controls.Clear()
        Dim absen As New UCAbsen
        Panel3.Controls.Add(absen)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Frmpass.ShowDialog()
    End Sub
End Class

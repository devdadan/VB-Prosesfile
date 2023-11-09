Imports MySql.Data.MySqlClient
Module Mod_Conn
    Public query As String
    Public iptoko, passtoko As String
    Public cek_koneksi As Integer
    Public conntoko As New MySqlConnection("server=" & iptoko & "; uid=root; pwd=" & passtoko & "; database=pos;")
    Public IP_induk, IP_anak As String
    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public rd As MySqlDataReader
    Public da As MySqlDataAdapter
    Public ds As DataSet
    Public str As String
    Public connDB As New MySqlConnection
    Public comDB As New MySqlCommand
    Public comBuilderDB As New MySqlCommandBuilder
    Public rdDB As MySqlDataReader
    Public dt As New DataTable
    Public myError As MySqlError
    Public SQL As String
    Public Item As ListViewItem

    Public Sub koneksi()
        Try
            Dim str As String = "Server=" + Formdb.TextBox1.Text + ";user id=" + Formdb.TextBox2.Text + ";password=" + Formdb.TextBox4.Text + ";database=" + Formdb.TextBox3.Text + ""
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
                'MsgBox("ok")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Module

Imports MySql.Data.MySqlClient

Public Class UCAbsen
    Dim ds As New DataTable
    Dim t1 As MySqlConnection
    Dim da As MySqlDataAdapter
    Dim bln, tgl, thn As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        proses()

    End Sub
    Sub proses()
        bln = Microsoft.VisualBasic.Mid(dt.Text.Trim, 4, 2)
        tgl = Microsoft.VisualBasic.Right(dt.Text.Trim, 2)

        Try
            Dim str As String = "Server=" + Formdb.TextBox1.Text + ";user id=" + Formdb.TextBox2.Text + ";password=" + Formdb.TextBox4.Text + ";database=" + Formdb.TextBox3.Text + ""
            conn = New MySqlConnection(str)

            Dim SQL As String = "SELECT 'FILE DT' AS NAMAFILE,COUNT(KDTK) AS TOTAL,SUM(TG" + tgl + ") AS OK,COUNT(kdtk)-SUM(TG" + tgl + ") AS NOK FROM absen_dt042022 UNION
SELECT 'SALES PER STATION' AS NAMAFILE,COUNT(KDTK) AS TOTAL,SUM(TG" + tgl + ") AS OK,COUNT(kdtk)-SUM(TG" + tgl + ") AS NOK FROM absen_sts042022 UNION
SELECT 'REKAP SALES' AS NAMAFILE,COUNT(KDTK) AS TOTAL,SUM(TG" + tgl + ") AS OK,COUNT(kdtk)-SUM(TG" + tgl + ") AS NOK FROM absen_sls042022 UNION
SELECT 'FILE GLSLP' AS NAMAFILE,COUNT(KDTK) AS TOTAL,SUM(TG" + tgl + ") AS OK,COUNT(kdtk)-SUM(TG" + tgl + ") AS NOK FROM absenGLSLP042022 UNION
SELECT 'ISTORE' AS NAMAFILE,COUNT(KDTK) AS TOTAL,SUM(TG" + tgl + ") AS OK,COUNT(kdtk)-SUM(TG" + tgl + ") AS NOK FROM absen_istore042022 UNION
SELECT 'SLP' AS NAMAFILE,COUNT(KDTK) AS TOTAL,SUM(TG" + tgl + ") AS OK,COUNT(kdtk)-SUM(TG" + tgl + ") AS NOK FROM absen_slp042022 UNION
SELECT 'FILE APKA' AS NAMAFILE,COUNT(KDTK) AS TOTAL,SUM(TG" + tgl + ") AS OK,COUNT(kdtk)-SUM(TG" + tgl + ") AS NOK FROM absen_apka042022 UNION
SELECT 'FILE STO' AS NAMAFILE,COUNT(KDTK) AS TOTAL,SUM(TG" + tgl + ") AS OK,COUNT(kdtk)-SUM(TG" + tgl + ") AS NOK FROM absen_STO042022 UNION
SELECT 'FILE ST' AS NAMAFILE,COUNT(KDTK) AS TOTAL,SUM(TG" + tgl + ") AS OK,COUNT(kdtk)-SUM(TG" + tgl + ") AS NOK FROM absen_ST042022;"

            t1 = conn
            Try
                t1.Open()
                da = New MySqlDataAdapter(SQL, t1)
                da.Fill(ds)

                DataGridView1.DataSource = ds

                t1.Close()

            Catch ex As Exception

            End Try

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class

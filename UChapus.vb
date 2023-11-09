Imports MySql.Data.MySqlClient
Public Class UChapus
    Dim bln As String
    Dim thn As String
    Dim tgl As String

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub
    Public Shared Sub kon()
        Dim s As String = Form1.Label4.Text
        Dim u As String = Form1.Label8.Text
        Dim p As String = Form1.Label5.Text
        Dim d As String = Form1.Label9.Text

    End Sub

    Sub hapus()
        Try
            thn = Microsoft.VisualBasic.Left(DateTimePicker1.Text, 4)
            bln = Microsoft.VisualBasic.Mid(DateTimePicker1.Text, 6, 2)
            tgl = Microsoft.VisualBasic.Right(DateTimePicker1.Text, 2)
            Dim ff As String
            If ComboBox2.Text = "semua" Then
                Try
                    Dim str As String = "Server=192.168.85.208;user id=jkt2;password=rahasiajkt2;database=indomaret"
                    conn = New MySqlConnection(str)
                    conn.Open()
                    SQL = "DELETE FROM dt_" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "';"
                    Dim cmd As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmd.ExecuteNonQuery()

                    ProgressBar1.Value = 20
                    SQL = "DELETE FROM penjualan" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "';"
                    Dim cmda As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmda.ExecuteNonQuery()
                    ProgressBar1.Value = 40
                    SQL = "DELETE FROM rekapsls" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "';"
                    Dim cmdb As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdb.ExecuteNonQuery()
                    ProgressBar1.Value = 60
                    SQL = "DELETE FROM sales_net" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "';"
                    Dim cmdc As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdc.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM APKA_PENJUALAN" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "';"
                    Dim cmdca As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdca.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM APKA_REKAPSLS" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "';"
                    Dim cmdcb As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdcb.ExecuteNonQuery()
                    ProgressBar1.Value = 80


                    SQL = "DELETE FROM SLP WHERE tanggal='" + DateTimePicker1.Text + "';"
                    Dim cmdcc As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdcc.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM CUSTAB" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "';"
                    Dim cmdcd As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdcd.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM istore" + bln + thn + " WHERE tgl_data='" + DateTimePicker1.Text + "';"
                    Dim cmdce As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdce.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM GLSLP_" + thn + bln + " WHERE tanggal='" + DateTimePicker1.Text + "';"
                    Dim cmdcf As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdcf.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM sto_" + bln + thn + " WHERE tgl_stockout='" + DateTimePicker1.Text + "';"
                    Dim cmdcg As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdcg.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM st_" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "';"
                    Dim cmdch As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdch.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "UPDATE absen_dt" + bln + thn + " SET tg" + tgl + "='';"
                    Dim cmde As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmde.ExecuteNonQuery()

                    SQL = "UPDATE absen_sls" + bln + thn + " SET tg" + tgl + "='';"
                    Dim cmdf As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdf.ExecuteNonQuery()
                    ProgressBar1.Value = 100
                    SQL = "UPDATE absen_sts" + bln + thn + " SET tg" + tgl + "='';"
                    Dim cmdg As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdg.ExecuteNonQuery()

                    SQL = "UPDATE absen_sto" + bln + thn + " SET tg" + tgl + "='';"
                    Dim cmdl As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdl.ExecuteNonQuery()

                    SQL = "UPDATE absen_st" + bln + thn + " SET tg" + tgl + "='';"
                    Dim cmdm As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdm.ExecuteNonQuery()

                    SQL = "UPDATE absen_slp" + bln + thn + " SET tg" + tgl + "='';"
                    Dim cmdn As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdn.ExecuteNonQuery()


                    SQL = "UPDATE absen_apka" + bln + thn + " SET tg" + tgl + "='';"
                    Dim cmdh As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdh.ExecuteNonQuery()

                    SQL = "UPDATE absenglslp" + bln + thn + " SET tg" + tgl + "='';"
                    Dim cmdi As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdi.ExecuteNonQuery()

                    SQL = "UPDATE absen_istore" + bln + thn + " SET tg" + tgl + "='';"
                    Dim cmdj As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdj.ExecuteNonQuery()

                    SQL = "UPDATE absen_custab" + bln + thn + " SET tg" + tgl + "='';"
                    Dim cmdk As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdk.ExecuteNonQuery()
                    MsgBox("sukses hapus data")
                    ProgressBar1.Value = 0
                Catch exhps As Exception
                    MsgBox(exhps.StackTrace)
                End Try

            Else
                Try
                    Dim str As String = "Server=192.168.85.208;user id=jkt2;password=rahasiajkt2;database=indomaret"
                    conn = New MySqlConnection(str)
                    conn.Open()
                    SQL = "DELETE FROM dt_" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "' AND SHOP='" + ComboBox2.Text + "';"
                    Dim cmd As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmd.ExecuteNonQuery()

                    ProgressBar1.Value = 20
                    SQL = "DELETE FROM penjualan" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "' AND SHOP='" + ComboBox2.Text + "';"
                    Dim cmda As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmda.ExecuteNonQuery()
                    ProgressBar1.Value = 40
                    SQL = "DELETE FROM rekapsls" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "' AND SHOP='" + ComboBox2.Text + "';"
                    Dim cmdb As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdb.ExecuteNonQuery()
                    ProgressBar1.Value = 60
                    SQL = "DELETE FROM sales_net" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "' AND TOKO='" + ComboBox2.Text + "';"
                    Dim cmdc As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdc.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM APKA_PENJUALAN" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "' AND SHOP='" + ComboBox2.Text + "';"
                    Dim cmdca As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdca.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM APKA_REKAPSLS" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "' AND SHOP='" + ComboBox2.Text + "';"
                    Dim cmdcb As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdcb.ExecuteNonQuery()
                    ProgressBar1.Value = 80


                    SQL = "DELETE FROM SLP WHERE tanggal='" + DateTimePicker1.Text + "' AND TOKO='" + ComboBox2.Text + "';"
                    Dim cmdcc As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdcc.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM CUSTAB" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "' AND SHOP='" + ComboBox2.Text + "';"
                    Dim cmdcd As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdcd.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM istore" + bln + thn + " WHERE tgl_data='" + DateTimePicker1.Text + "' AND kdtk='" + ComboBox2.Text + "';"
                    Dim cmdce As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdce.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM GLSLP_" + thn + bln + " WHERE tanggal='" + DateTimePicker1.Text + "' AND KODE_TOKO='" + ComboBox2.Text + "';"
                    Dim cmdcf As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdcf.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM sto_" + bln + thn + " WHERE tgl_stockout='" + DateTimePicker1.Text + "' AND TOKO='" + ComboBox2.Text + "';"
                    Dim cmdcg As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdcg.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "DELETE FROM st_" + bln + thn + " WHERE tanggal='" + DateTimePicker1.Text + "' AND KODE_TOKO='" + ComboBox2.Text + "';"
                    Dim cmdch As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdch.ExecuteNonQuery()
                    ProgressBar1.Value = 80

                    SQL = "UPDATE absen_dt" + bln + thn + " SET tg" + tgl + "='' where KDTK='" + ComboBox2.Text + "';"
                    Dim cmde As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmde.ExecuteNonQuery()

                    SQL = "UPDATE absen_sls" + bln + thn + " SET tg" + tgl + "='' where KDTK='" + ComboBox2.Text + "';"
                    Dim cmdf As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdf.ExecuteNonQuery()
                    ProgressBar1.Value = 100
                    SQL = "UPDATE absen_sts" + bln + thn + " SET tg" + tgl + "='' where KDTK='" + ComboBox2.Text + "';"
                    Dim cmdg As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdg.ExecuteNonQuery()

                    SQL = "UPDATE absen_sto" + bln + thn + " SET tg" + tgl + "='' where KDTK='" + ComboBox2.Text + "';"
                    Dim cmdl As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdl.ExecuteNonQuery()

                    SQL = "UPDATE absen_st" + bln + thn + " SET tg" + tgl + "='' where KDTK='" + ComboBox2.Text + "';"
                    Dim cmdm As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdm.ExecuteNonQuery()

                    SQL = "UPDATE absen_slp" + bln + thn + " SET tg" + tgl + "='' where KDTK='" + ComboBox2.Text + "';"
                    Dim cmdn As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdn.ExecuteNonQuery()


                    SQL = "UPDATE absen_apka" + bln + thn + " SET tg" + tgl + "='' WHERE KDTK='" + ComboBox2.Text + "';"
                    Dim cmdh As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdh.ExecuteNonQuery()

                    SQL = "UPDATE absenglslp" + bln + thn + " SET tg" + tgl + "='' WHERE KDTK='" + ComboBox2.Text + "';"
                    Dim cmdi As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdi.ExecuteNonQuery()

                    SQL = "UPDATE absen_istore" + bln + thn + " SET tg" + tgl + "='' WHERE KDTK='" + ComboBox2.Text + "';"
                    Dim cmdj As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdj.ExecuteNonQuery()

                    SQL = "UPDATE absen_custab" + bln + thn + " SET tg" + tgl + "='' WHERE KDTK='" + ComboBox2.Text + "';"
                    Dim cmdk As MySqlCommand = New MySqlCommand(SQL, conn)
                    cmdk.ExecuteNonQuery()


                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try
            End If


            MsgBox("sukses hapus data")
            ProgressBar1.Value = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Sub TOKO()
        Dim str As String = "Server=192.168.85.208;user id=jkt2;password=rahasiajkt2;database=indomaret"
        conn = New MySqlConnection(str)
        conn.Open()
        Dim stra As String
        stra = "SELECT KDTK FROM TOKO_CAB"
        cmd = New MySqlCommand(stra, conn)
        rd = cmd.ExecuteReader
        If rd.HasRows Then
            ComboBox2.Items.Add("semua")
            Do While rd.Read
                ComboBox2.Items.Add(rd("KDTK"))
            Loop
        Else
        End If
        conn.Close()
    End Sub

    Private Sub UChapus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False

        TOKO()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        hapus()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub


End Class

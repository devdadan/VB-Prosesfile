Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Environment
Imports ComponentAce.Compression.ZipForge
Imports ComponentAce.Compression.Archiver
Imports System.Data.OleDb
Imports System.Data.Odbc
Public Class UCtransaksi
    Dim jam, menit, detik As Integer
    Dim lbawal, lbakhir As String
    Dim jmlhr As String
    Dim server, user, pass, dbase As String
    Public conn As New MySql.Data.MySqlClient.MySqlConnection
    Dim connstr As String
    Dim msgr As MsgBoxResult

    Dim archiver As New ZipForge()

    Dim tanggal As String
    Dim tanggal2 As String

    Dim tanggal3 As String
    Dim ISI As String

    'tanggal data harian dri data
    Dim hr_dt As String
    Dim bln_dt As String
    Dim thn As String
    Dim thn2 As String
    Dim z As String
    Dim z2 As String
    Dim z3 As String
    Dim idhr As String
    Dim ptvfile As String
    Dim stkfile, ntbfile, sprfile, sbfile, dcpfile As String

    'flag
    Dim flag_hr As String
    Dim flag_dt As String
    Dim flag_pkm As String
    Dim flag_slp As String
    Dim flag_st As String
    Dim flag_wt As String
    Dim flag_ca As String
    Dim flag_plano As String
    Dim flag_glslp As String
    Dim flag_harian As String
    Dim flag_pr As String
    Dim flag_sb As String
    Dim flag_sto As String
    Dim flag_pbk As String

    Dim toko1 As String

    'namafile
    Dim kodetoko As String
    Dim namatoko As String
    Dim namafile As String
    Dim dataharian As String

    Dim k As String
    Dim dtk As String
    Dim ddmmyyyy, yymmdd, mmyyyy As String

    Dim objRDR As StreamReader
    Dim asa As String

    'path_err
    Dim err_dt As String = "err_dt.log"
    Dim err_glslp As String = "err_glslp.log"
    Dim err_st As String = "err_st.log"
    Dim err_sto As String = "err_sto.log"

    Dim err_hr As String = "err_hr.log"
    Dim err_toko As String = "err_toko.log"


    Dim flag_wt_err As String = "Y"

    Dim sql, sql1, sql2, sql3, sql4, sql5, sql6 As String

    'otoklik
    Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByRef lParam As Object) As Integer
    Const WM_LBUTTONDOWN = &H201
    Const WM_LBUTTONUP = &H202

    '######################## ABSEN ################################'
    Private Sub absen()

        Dim bln As String
        Dim thn As String
        bln = Microsoft.VisualBasic.Mid(Period.Text.Trim, 4, 2)
        thn = Microsoft.VisualBasic.Right(Period.Text.Trim, 4)

        Dim sql As String

        Try
            koneksi()

            'Buat absen STO
            sql = "create table if not exists prosesfile" + bln + thn + " like prosesfile"
            Dim cmd6 As MySqlCommand = New MySqlCommand(sql, conn)
            cmd6.ExecuteNonQuery()

            sql = "insert ignore into prosesfile" + bln + thn + "(kdtk,tipe,nama) select kdtk,'DT' as tipe,'Dtran' as nama FROM TOKO_CAB"
            Dim cmd6a As MySqlCommand = New MySqlCommand(sql, conn)
            cmd6a.ExecuteNonQuery()

            sql = "insert ignore into prosesfile" + bln + thn + "(kdtk,tipe,nama) select kdtk,'GL' as tipe,'GLSLP' as nama FROM TOKO_CAB"
            Dim cmd6b As MySqlCommand = New MySqlCommand(sql, conn)
            cmd6b.ExecuteNonQuery()

            sql = "insert ignore into prosesfile" + bln + thn + "(kdtk,tipe,nama) select kdtk,'ST' as tipe,'ST File' as nama FROM TOKO_CAB"
            Dim cmd6c As MySqlCommand = New MySqlCommand(sql, conn)
            cmd6c.ExecuteNonQuery()

            sql = "insert ignore into prosesfile" + bln + thn + "(kdtk,tipe,nama) select kdtk,'STO' as tipe,'STO File' as nama FROM TOKO_CAB"
            Dim cmd6d As MySqlCommand = New MySqlCommand(sql, conn)
            cmd6d.ExecuteNonQuery()



        Catch ex As Exception
            'MsgBox(ex.Message)
            ClsTolls.Tulislog("Eror di cek absen " & sql)
            'ClsTolls.LogMail(ex.Message.ToString, ex.StackTrace.ToString, kodetoko.ToString)
        End Try

    End Sub
    Private Sub cek_flag()
        Dim sql As String
        sql = "SELECT TG" + hr_dt + " from prosesfile" + bln_dt + thn + " WHERE KDTK='" + kodetoko.ToString + "' and tipe='GL'"
        Dim actglslp As New MySqlCommand(sql, conn)
        Dim bacaglslp As MySqlDataReader
        bacaglslp = actglslp.ExecuteReader
        If bacaglslp.Read Then
            flag_glslp = bacaglslp.Item(0).ToString
            Application.DoEvents()
        End If
        bacaglslp.Close()


        'cek flag dt
        sql = "SELECT TG" + hr_dt + " from prosesfile" + bln_dt + thn + " WHERE KDTK='" + kodetoko.ToString + "' and tipe='DT'"
        Dim actdt As New MySqlCommand(sql, conn)
        Dim bacadt As MySqlDataReader
        bacadt = actdt.ExecuteReader
        If bacadt.Read Then
            flag_dt = bacadt.Item(0).ToString
            Application.DoEvents()
        End If
        bacadt.Close()


        'cek flag ST
        sql = "SELECT TG" + hr_dt + " from prosesfile" + bln_dt + thn + " WHERE KDTK='" + kodetoko.ToString + "' and tipe='ST'"
        Dim actst As New MySqlCommand(sql, conn)
        Dim bacast As MySqlDataReader
        bacast = actst.ExecuteReader
        If bacast.Read Then
            flag_st = bacast.Item(0).ToString
            Application.DoEvents()
        End If
        bacast.Close()

        'cek flag sto
        sql = "SELECT TG" + hr_dt + " from prosesfile" + bln_dt + thn + " WHERE KDTK='" + kodetoko.ToString + "' and tipe='STO'"
        Dim actsto As New MySqlCommand(sql, conn)
        Dim bacasto As MySqlDataReader
        bacasto = actsto.ExecuteReader
        If bacasto.Read Then
            flag_sto = bacasto.Item(0).ToString
            Application.DoEvents()
        End If
        bacasto.Close()

        sql = "SELECT KDTK,NAMA FROM TOKO_CAB WHERE KDTK='" + kodetoko.ToString + "'"
        Dim act1 As New MySqlCommand(sql, conn)
        Dim baca1 As MySqlDataReader
        baca1 = act1.ExecuteReader
        If baca1.Read Then
            toko1 = baca1.Item(0).ToString
            namatoko = baca1.Item(1).ToString
            Application.DoEvents()
        End If
        baca1.Close()

    End Sub
    Private Sub log_toko_unregister()

        Dim tulis As StreamWriter
        If Not File.Exists(err_toko) Then
            tulis = File.CreateText(err_toko)
            tulis.WriteLine("-- REKAP DATA HARIAN TOKO UNREGISTER DI MASTER TOKO SAAT PROSES EIS --")
            tulis.WriteLine("----------------------------------------------------------------------")
            tulis.WriteLine(kodetoko.ToString + " ==> " + dataharian.ToString + " Process On " + Format(Date.Now, "dd-MM-yyyy") + " " + Format(TimeOfDay, "HH:mm:ss"))
            tulis.Flush()
            tulis.Close()
        Else
            tulis = File.AppendText(err_toko)
            tulis.WriteLine(kodetoko.ToString + " ==> " + dataharian.ToString + " Process On " + Format(Date.Now, "dd-MM-yyyy") + " " + Format(TimeOfDay, "HH:mm:ss"))
            tulis.Flush()
            tulis.Close()
        End If

    End Sub
    Private Sub list2()

        ListBox2.Items.Clear()
        Dim folderInfo As New IO.DirectoryInfo(TextBox2.Text)
        Dim arrFilesInFolder() As IO.FileInfo
        Dim fileInFolder As IO.FileInfo
        arrFilesInFolder = folderInfo.GetFiles("*.*")
        For Each fileInFolder In arrFilesInFolder
            ListBox2.Items.Add(fileInFolder.Name)
        Next

    End Sub
    Private Sub data_chek()
        If namafile.Substring(0, 5) = "GLSLP" Then

            hr_dt = Microsoft.VisualBasic.Mid(namafile.Trim, 10, 2)
            bln_dt = Microsoft.VisualBasic.Mid(namafile.Trim, 8, 2)
            thn = Microsoft.VisualBasic.Right(Period.Text.Trim, 4)
            thn2 = Microsoft.VisualBasic.Right(Period.Text.Trim, 2)

            Dim k As String
            k = Microsoft.VisualBasic.Mid(namafile.Trim, 12, 1).ToUpper
            Dim dtk As String
            dtk = Microsoft.VisualBasic.Right(namafile.Trim, 3).ToUpper

            kodetoko = k + dtk
            ddmmyyyy = hr_dt + "-" + bln_dt + "-" + thn
            tanggal = thn + "-" + bln_dt + "-" + hr_dt

        End If


        If namafile.Substring(0, 2) = "DT" Then

            hr_dt = Microsoft.VisualBasic.Mid(namafile.Trim, 5, 2)
            bln_dt = Microsoft.VisualBasic.Mid(namafile.Trim, 3, 2)
            thn = Microsoft.VisualBasic.Right(Period.Text.Trim, 4)

            Dim k As String
            k = Microsoft.VisualBasic.Mid(namafile.Trim, 7, 1).ToUpper
            Dim dtk As String
            dtk = Microsoft.VisualBasic.Right(namafile.Trim, 3).ToUpper

            kodetoko = k + dtk
            ddmmyyyy = hr_dt + "-" + bln_dt + "-" + thn
            tanggal = thn + "-" + bln_dt + "-" + hr_dt

        End If


        If namafile.Substring(0, 2) = "ST" Then

            hr_dt = Microsoft.VisualBasic.Mid(namafile.Trim, 5, 2)
            bln_dt = Microsoft.VisualBasic.Mid(namafile.Trim, 3, 2)
            thn = Microsoft.VisualBasic.Right(Period.Text.Trim, 4)

            Dim k As String
            k = Microsoft.VisualBasic.Mid(namafile.Trim, 7, 1).ToUpper
            Dim dtk As String
            dtk = Microsoft.VisualBasic.Right(namafile.Trim, 3).ToUpper

            kodetoko = k + dtk
            ddmmyyyy = hr_dt + "-" + bln_dt + "-" + thn
            tanggal = thn + "-" + bln_dt + "-" + hr_dt

        End If
        If namafile.Substring(0, 3) = "STO" Then

            hr_dt = Microsoft.VisualBasic.Mid(namafile.Trim, 6, 2)
            bln_dt = Microsoft.VisualBasic.Mid(namafile.Trim, 4, 2)
            thn = Microsoft.VisualBasic.Right(Period.Text.Trim, 4)

            Dim k As String
            k = Microsoft.VisualBasic.Mid(namafile.Trim, 8, 1).ToUpper
            Dim dtk As String
            dtk = Microsoft.VisualBasic.Right(namafile.Trim, 3).ToUpper

            kodetoko = k + dtk
            ddmmyyyy = hr_dt + "-" + bln_dt + "-" + thn
            tanggal = thn + "-" + bln_dt + "-" + hr_dt

        End If

        Dim sql As String
        sql = "SELECT KDTK,NAMA FROM TOKO_CAB WHERE KDTK='" + kodetoko.ToString + "'"
        Dim act1 As New MySqlCommand(sql, conn)
        Dim baca1 As MySqlDataReader
        baca1 = act1.ExecuteReader
        If baca1.Read Then
            namatoko = baca1.Item(1).ToString
            Application.DoEvents()
        End If
        baca1.Close()

    End Sub


    Private Sub fileDT()

        ProgressBar2.Maximum = 100
        ProgressBar2.Value = 0
        Try
            'ClsTolls.Tulislog("mulai proses file DT")
            ProgressBar2.Value = 20
            ProgressBar2.Value = 60

            Try

                sql = "load data local infile '" + TextBox2.Text + "/" + namafile.ToString + "' into table dt_" + bln_dt + thn + " fields terminated by '|' ignore 1 lines set tanggal='" + tanggal.ToString + "'"
                Dim cmddt1 As MySqlCommand = New MySqlCommand(sql, conn)
                cmddt1.ExecuteNonQuery()
                penjualan()
                sts()
                sls()
                custab()
                ProgressBar2.Value = 90
                apka()
            Catch exdt As Exception
                ClsTolls.Tulislog("Eror di load file DT toko " & sql)
                ClsTolls.LogMail(exdt.Message.ToString, exdt.StackTrace.ToString, kodetoko.ToString)
                'MsgBox(exdt.StackTrace)
            End Try

            sql3 = "update prosesfile" + bln_dt + thn + " set TG" + hr_dt + "='1' where kdtk='" + kodetoko.ToString + "' and tipe='DT'"
            Dim cmd5 As MySqlCommand = New MySqlCommand(sql3, conn)
            cmd5.ExecuteNonQuery()


            ProgressBar2.Value = 0

        Catch exa As Exception
            ClsTolls.Tulislog("Eror di absen DT")
            ClsTolls.LogMail(exa.Message, exa.StackTrace.ToString, kodetoko.ToString)
            'MsgBox(exa.StackTrace)
        End Try

    End Sub
    Private Sub glslp()
        ProgressBar2.Value = 30
        Try


            Try
                'ClsTolls.Tulislog("mulai load infile untuk toko " & kodetoko.ToString)
                sql = "load data local infile '" + TextBox2.Text + "/" + namafile.ToString + "' into table glslp_" + thn + bln_dt + " fields terminated by '|' ignore 1 lines set kode_toko='" + kodetoko.ToString + "',nama='" + namatoko.ToString + "',tanggal='" + thn + "-" + bln_dt + "-" + hr_dt + "';"
                Dim cmddt1 As MySqlCommand = New MySqlCommand(sql, conn)
                cmddt1.ExecuteNonQuery()
                ProgressBar2.Value = 60
                sql = "REPLACE INTO slp SELECT kode_toko AS toko,nama,tanggal,SUM(IF(kode='01',amount_cr,0)) sales,SUM(IF(kode='25',amount_dr,0)) slshpp,SUM(IF(kode='02',amount_cr,0)) ppn,'0' counter1,'0' counter2,'0' ppn_ctr2,SUM(IF(kode='06',amount_dr,0)) cash,'0' dbt_bca,'0' tn_bca,SUM(IF(kode='09' AND subkode='KPN',amount_dr,0)) kpn_idm,'0' kpn_nidm,SUM(IF(kode='11',amount_dr,0)) discount,SUM(IF(kode='12',amount_cr,0)) `variance`,SUM(IF(kode='13',REF_6,0)) jml_strk,MAX(IF(kode='14',SUBSTRING(`desc`,6),0)) buka,MAX(IF(kode='15',SUBSTRING(`desc`,7),0)) tutup,'0000-00-00' log_kirim,'0000-00-00' tgl_kirim,'00:00:00' jam_kirim,SUM(IF(kode='16',REF_6,0)) stk_out,SUM(IF(kode='17',REF_6,0)) brg_hlg,SUM(IF(kode='18',REF_6,0)) brg_rsk,'0' brg_pts,'0000-00-00' tglrrak,'0' telepon,'0' listrik,'0' ktg_kcl,'0' ktg_sdg,'0' ktg_bsr,'0' sls14061,'0' ppn14601,SUM(IF(kode='33' AND subkode='999',amount_dr,0)) kredit,'0' penggantian,'0' ppn_bbs,'0' nkhpp,'0' nlhpp,'0' nkprice,'0' nlprice FROM glslp_" + thn + bln_dt + " WHERE kode_toko='" + kodetoko.ToString + "' and tanggal='" + thn + "-" + bln_dt + "-" + hr_dt + "' GROUP BY kode_toko;"
                Dim cmddt2 As MySqlCommand = New MySqlCommand(sql, conn)
                cmddt2.ExecuteNonQuery()
                istore()
                updsls()
                ProgressBar2.Value = 100
            Catch exgl As Exception
                'log_glslp()
                ClsTolls.Tulislog("eror load toko " & sql)
                ClsTolls.LogMail(exgl.Message.ToString, exgl.StackTrace, kodetoko.ToString)
                'MsgBox(exgl.StackTrace)
            End Try

            If flag_wt_err = "Y" Then
                sql = "update prosesfile" + bln_dt + thn + " set TG" + hr_dt + "='1' where kdtk='" + kodetoko.ToString + "' and tipe='GL'"
                Dim cmd9 As MySqlCommand = New MySqlCommand(sql, conn)
                cmd9.ExecuteNonQuery()
            End If


            If File.Exists(TextBox1.Text + "/" + namafile) Then
                File.Delete(TextBox1.Text + "/" + namafile)
            Else
            End If

        Catch exb As Exception
            'log_glslp()
            ClsTolls.Tulislog("error di try glslp " & sql)
            ClsTolls.LogMail(exb.Message.ToString, exb.StackTrace, kodetoko.ToString)
            'MsgBox(exb.StackTrace)
        End Try

    End Sub
    Private Sub st()

        Try


            Try
                sql = "load data local infile '" + TextBox2.Text + "/" + namafile.ToString + "' into table st_" + bln_dt + thn + " fields terminated by '|' ignore 1 lines set kode_toko='" + kodetoko.ToString + "',tanggal='" + tanggal.ToString + "'"
                Dim cmddt1 As MySqlCommand = New MySqlCommand(sql, conn)
                cmddt1.ExecuteNonQuery()
            Catch ex As Exception

            End Try


            If File.Exists(TextBox1.Text + "/" + namafile) Then
                File.Delete(TextBox1.Text + "/" + namafile)
            Else
            End If

        Catch exst As Exception
            'log_st()
            ClsTolls.Tulislog("eror pada proses st" & sql)
            ClsTolls.LogMail(exst.Message.ToString, exst.StackTrace, kodetoko.ToString)
            'MsgBox(exst.StackTrace)
        End Try

    End Sub

    Private Sub sto()

        Try



            Try
                sql = "load data local infile '" + TextBox2.Text + "/" + namafile.ToString + "' into table sto_" + bln_dt + thn + " fields terminated by '|' ignore 1 lines"
                Dim cmddt1 As MySqlCommand = New MySqlCommand(sql, conn)
                cmddt1.ExecuteNonQuery()
            Catch ex As Exception

            End Try


            If File.Exists(TextBox1.Text + "/" + namafile) Then
                File.Delete(TextBox1.Text + "/" + namafile)
            Else
            End If

        Catch exsto As Exception
            'log_sto()
            ClsTolls.Tulislog("error pada proses sto " & sql)
            ClsTolls.LogMail(exsto.Message.ToString, exsto.StackTrace.ToString, kodetoko.ToString)
            'MsgBox(exsto.StackTrace)
        End Try

    End Sub
    Private Sub log_glslp()
        Dim flag As Boolean = Not File.Exists(Me.err_glslp)
        If flag Then
            Dim tulis As StreamWriter = File.CreateText(Me.err_glslp)
            tulis.WriteLine("-- LOG GLSLP --")
            tulis.WriteLine("-------------")
            tulis.WriteLine("Query :")
            tulis.WriteLine(Me.sql)
            tulis.Flush()
            tulis.Close()
        Else
            Dim tulis As StreamWriter = File.AppendText(Me.err_glslp)
            tulis.WriteLine("Query :")
            tulis.WriteLine(Me.sql)
            tulis.Flush()
            tulis.Close()
        End If
    End Sub

    Private Sub log_dt()
        Dim tulis As StreamWriter
        If Not File.Exists(err_dt) Then
            tulis = File.CreateText(err_dt)
            tulis.WriteLine("-- LOG DT --")
            tulis.WriteLine("-------------")
            tulis.WriteLine("Query :")
            tulis.WriteLine(sql)
            tulis.WriteLine(sql2)
            tulis.WriteLine(sql3)
            tulis.WriteLine(sql4)
            tulis.Flush()
            tulis.Close()
        Else
            tulis = File.AppendText(err_dt)
            tulis.WriteLine("Query :")
            tulis.WriteLine(sql)
            tulis.WriteLine(sql2)
            tulis.WriteLine(sql3)
            tulis.WriteLine(sql4)
            tulis.Flush()
            tulis.Close()
        End If
    End Sub
    Private Sub log_st()
        Dim tulis As StreamWriter
        If Not File.Exists(err_st) Then
            tulis = File.CreateText(err_st)
            tulis.WriteLine("-- LOG ST --")
            tulis.WriteLine("-------------")
            tulis.WriteLine("Query :")
            tulis.WriteLine(sql)
            tulis.Flush()
            tulis.Close()
        Else
            tulis = File.AppendText(err_st)
            tulis.WriteLine("Query :")
            tulis.WriteLine(sql)
            tulis.Flush()
            tulis.Close()
        End If
    End Sub
    Private Sub log_sto()
        Dim tulis As StreamWriter
        If Not File.Exists(err_sto) Then
            tulis = File.CreateText(err_sto)
            tulis.WriteLine("-- LOG STO --")
            tulis.WriteLine("-------------")
            tulis.WriteLine("Query :")
            tulis.WriteLine(sql)
            tulis.Flush()
            tulis.Close()
        Else
            tulis = File.AppendText(err_sto)
            tulis.WriteLine("Query :")
            tulis.WriteLine(sql)
            tulis.Flush()
            tulis.Close()
        End If
    End Sub




    Private Sub extraK()
        z = Microsoft.VisualBasic.Mid(Period.Text.Trim, 4, 2) + Microsoft.VisualBasic.Mid(dataharian.ToString.Trim, 7, 2)
        z2 = Microsoft.VisualBasic.Right(Period.Text.Trim, 2) + Microsoft.VisualBasic.Mid(Period.Text.Trim, 4, 2) + Microsoft.VisualBasic.Mid(dataharian.ToString.Trim, 7, 2)
        z3 = Microsoft.VisualBasic.Right(Period.Text.Trim, 2) + Microsoft.VisualBasic.Mid(Period.Text.Trim, 4, 2)
        Try
            Try
                archiver.FileName = TextBox1.Text + dataharian.ToString()  'data yg akan di extrak'
                archiver.OpenArchive(System.IO.FileMode.Open)
                archiver.BaseDir = TextBox2.Text 'di extrak ke'

                If flag_glslp = "1" Then
                Else
                    If xGL.Checked = True Then

                        Me.archiver.ExtractFiles("GLSLP" + Me.z2 + "*.*")

                    End If
                End If

                If flag_dt = "1" Then
                Else
                    If xDT.Checked = True Then
                        archiver.ExtractFiles("DT" + z + "*.*")
                    End If
                End If


                If flag_st = "1" Then
                Else
                    If xST.Checked = True Then
                        archiver.ExtractFiles("ST" + z + "*.*")
                    End If
                End If

                If flag_sto = "1" Then
                Else
                    If xSTO.Checked = True Then
                        archiver.ExtractFiles("STO" + z + "*.*")
                    End If
                End If

                list2()
                Dim c As Integer
                c = 0
                Dim ca As Integer
                ca = ListBox2.Items.Count.ToString

                While c <= ca
                    If c = ca Then
                        Exit While
                    Else
                        namafile = ListBox2.Items.Item(0)

                        data_chek()
                        Label10.Text = "Proses Harian : " + dataharian + " :: " + kodetoko + " : " + namatoko
                        Label11.Text = "Proses Data : " + namafile + " Tgl : " + ddmmyyyy


                        If namafile.Substring(0, 5) = "GLSLP" Then
                            If flag_glslp = "1" Then
                            Else
                                If xGL.Checked = True Then
                                    glslp()
                                Else

                                End If

                            End If
                        End If


                        If namafile.Substring(0, 2) = "DT" Then
                            If flag_dt = "1" Then
                            Else
                                If xDT.Checked = True Then
                                    fileDT()
                                Else

                                End If

                            End If
                        End If



                        If namafile.Substring(0, 2) = "ST" Then
                            If flag_st = "1" Then
                            Else
                                If xST.Checked = True Then
                                    st()
                                Else

                                End If

                            End If
                        End If

                        If namafile.Substring(0, 3) = "STO" Then
                            If flag_sto = "1" Then
                            Else
                                If xSTO.Checked = True Then
                                    sto()
                                Else

                                End If

                            End If
                        End If


                        If File.Exists(TextBox2.Text + "/" + namafile.ToString) Then
                            File.Delete(TextBox2.Text + "/" + namafile.ToString)
                        End If

                        list2()
                    End If
                    c = c + 1
                    System.Threading.Thread.Sleep(100)
                    Application.DoEvents()
                End While

            Catch ex2 As Exception
                'MsgBox(Err.Description)
                'log_hr()
                ClsTolls.Tulislog("error pada proses ekstrak  file : " & namafile.ToString)
                'ClsTolls.LogMail(ex2.Message.ToString, ex2.StackTrace.ToString, kodetoko.ToString)
                'MsgBox(ex2.Message)
            End Try
            archiver.CloseArchive()
        Catch ex As Exception
            ClsTolls.Tulislog("eror pada try ekstrak pertama")
            'ClsTolls.LogMail(ex.Message, ex.StackTrace, kodetoko.ToString)
            'MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            xDT.Checked = True
            xGL.Checked = True
            xST.Checked = True
            xSTO.Checked = True

        Else
            xDT.Checked = False
            xGL.Checked = False
            xST.Checked = False
            xSTO.Checked = False

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            Dim str As String = "Server=" + Formdb.TextBox1.Text + ";user id=" + Formdb.TextBox2.Text + ";password=" + Formdb.TextBox4.Text + ";database=" + Formdb.TextBox3.Text + ""
            conn = New MySqlConnection(str)
            conn.Open()

            Button1.Enabled = False
            Timer2.Stop()
            openf()
            konsep_ulang()

            Button1.Enabled = True
            conn.Close()
        Catch exbtn As Exception
            'MsgBox(ex.Message)
            ClsTolls.Tulislog("eror di tombol button")
            'ClsTolls.LogMail(exbtn.Message.ToString, exbtn.StackTrace, kodetoko.ToString)
            Exit Sub
        End Try
    End Sub


    Private Sub konsep_ulang()

        absen()
        Dim nom As Integer = 0
        ProgressBar1.Maximum = ListBox1.Items.Count + 1

        While 0 < ListBox1.Items.Count

            If nom = ListBox1.Items.Count Then
                ProgressBar1.Value = 0
                ProgressBar2.Value = 0
                Label11.Text = ""
                Label10.Text = ""
                Button1.Enabled = True
                Exit While

            Else

                dataharian = ListBox1.Items.Item(nom).ToString

                hr_dt = Microsoft.VisualBasic.Mid(dataharian.ToString.Trim, 7, 2)
                bln_dt = Microsoft.VisualBasic.Mid(dataharian.ToString.Trim, 5, 2)
                thn = Microsoft.VisualBasic.Right(Period.Text.Trim, 4)

                If Microsoft.VisualBasic.Left(dataharian.ToString.Trim, 2).ToUpper = "HR" Then
                    kodetoko = "T" + Microsoft.VisualBasic.Right(dataharian.ToString.Trim, 3).ToUpper
                End If
                If Microsoft.VisualBasic.Left(dataharian.ToString.Trim, 2).ToUpper = "FR" Then
                    kodetoko = "F" + Microsoft.VisualBasic.Right(dataharian.ToString.Trim, 3).ToUpper
                End If
                If Microsoft.VisualBasic.Left(dataharian.ToString.Trim, 2).ToUpper = "CR" Then
                    kodetoko = "R" + Microsoft.VisualBasic.Right(dataharian.ToString.Trim, 3).ToUpper
                End If

                cek_flag()

                'Cek Data Toko sesuai Master Cabang
                If toko1 = "" Then
                    'log_toko_unregister()
                    ClsTolls.LogMail2(kodetoko.ToString + " ==> " + dataharian.ToString + " Process On " + Format(Date.Now, "dd-MM-yyyy") + " " + Format(TimeOfDay, "HH:mm:ss"))
                Else
                    tanggal = thn + "-" + bln_dt + "-" + hr_dt
                    Label10.Text = "Check Harian : " + dataharian + " Toko : " + kodetoko + "-" + namatoko

                    If flag_hr = "1" Then
                    Else
                        extraK()
                    End If


                    ProgressBar1.Value = nom
                End If
            End If

            nom = nom + 1
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
        End While


        Try
            Dim aa As String
            aa = jam1.Text.Substring(0, 2).ToString

            Dim jj As String
            jj = jam2.Text.Substring(0, 2).ToString

            Dim za As String
            za = jamlabel.Text.Substring(0, 2).ToString

            If za >= aa And za < jj Then
                If CheckBox1.Checked = True Then
                    Timer2.Start()
                Else
                    Timer2.Stop()
                End If
            Else
                Timer2.Stop()
            End If
        Catch extime As Exception
            ClsTolls.Tulislog("eror set timmer")
            'ClsTolls.LogMail(extime.Message.ToString, extime.StackTrace, kodetoko.ToString)
            MsgBox(extime.StackTrace)
        End Try

    End Sub

    Private Sub Rek_sts_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub log_hr()
        Dim tulis As StreamWriter
        If Not File.Exists(err_hr) Then
            tulis = File.CreateText(err_hr)
            tulis.WriteLine("-- REKAP DATA ERROR HR/FR/CR SAAT PROSES EIS --")
            tulis.WriteLine("-----------------------------------------------")
            tulis.WriteLine(kodetoko.ToString + " ==> " + dataharian.ToString + " On Process : " + Format(Date.Now, "dd-MM-yyyy") + " " + Format(TimeOfDay, "HH:mm:ss"))
            tulis.Flush()
            tulis.Close()
        Else
            tulis = File.AppendText(err_hr)
            tulis.WriteLine(kodetoko.ToString + " ==> " + dataharian.ToString + " On Process : " + Format(Date.Now, "dd-MM-yyyy") + " " + Format(TimeOfDay, "HH:mm:ss"))
            tulis.Flush()
            tulis.Close()
        End If
    End Sub




    Private Sub openf()

        Dim h As String
        Dim tg As String
        tg = Period.Text.ToString.Substring(0, 2)
        Dim tga As String
        tga = Period2.Text.ToString.Substring(0, 2)

        Dim a As Integer
        a = tg
        Dim b As Integer
        b = tga

        ListBox1.Items.Clear()
        Try
            If a <= b Then
                Do While a <= b

                    If a <= 9 Then
                        h = "0" & a
                    Else
                        h = a
                    End If

                    Dim thn As String
                    thn = Microsoft.VisualBasic.Right(Period.Text.Trim, 2)

                    Dim folderInfo As New IO.DirectoryInfo(TextBox1.Text)
                    Dim arrFilesInFolder() As IO.FileInfo
                    Dim fileInFolder As IO.FileInfo
                    arrFilesInFolder = folderInfo.GetFiles("?R" + Microsoft.VisualBasic.Left(lbawal.ToString.Trim, 4).ToString + h + ".???")
                    'arrFilesInFolder = folderInfo.GetFiles("?R" + thn + "*.???")
                    Dim f As String
                    f = lbawal.ToString.Substring(4, 2) + h
                    For Each fileInFolder In arrFilesInFolder
                        ListBox1.Items.Add(fileInFolder.Name)
                        Label9.Text = "Jumlah Data : " & ListBox1.Items.Count
                        jmlhr = ListBox1.Items.Count
                    Next
                    a = a + 1
                Loop
            End If
        Catch exopenf As Exception
            'MsgBox(Err.Description)
            ClsTolls.Tulislog("eror di openf")
            'ClsTolls.LogMail(exopenf.Message.ToString, exopenf.StackTrace, kodetoko.ToString)
            'MsgBox(exopenf.StackTrace)
        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        jamlabel.Text = Format(TimeOfDay, "HH:mm:ss")
        If jamlabel.Text = "00:00:05" Then
            Period.Text = DateAdd("d", -1, Date.Now)
            Period2.Text = Date.Now
            If Period2.Text.ToString.Substring(0, 2) = "01" Then
                Period2.Text = DateAdd("d", -1, Date.Now)
            End If
            Application.DoEvents()
            'MDIParent1.Tooltgljam.Text = Format(Date.Today, "dd-MM-yyyy")
        End If
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Try

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

        Catch exinterval As Exception
            ClsTolls.LogMail(exinterval.Message.ToString, exinterval.StackTrace, kodetoko.ToString)
            MsgBox(exinterval.StackTrace)
        End Try
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim h As Integer
        h = mnt.Text * 60

        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\edp", "interval", h)
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\edp", "jam_start", cbx_awal.Text)
        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\edp", "jam_end", cbx_akh.Text)

        MsgBox("Setting Interval Berhasil")

        Dim reg_interval As String
        reg_interval = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "Interval", Nothing)
        Me.interval.Text = reg_interval
        jam1.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "jam_start", Nothing)
        jam2.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "jam_end", Nothing)

        Me.Refresh()
        GroupBox5.Enabled = False
        settimer.CheckState = 0
    End Sub
    Public Sub sett()
        Dim reg_interval As String
        reg_interval = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "Interval", Nothing)
        TextBox1.Text = reg_interval / 60

        Dim reg_jam1 As String
        reg_jam1 = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "jam_start", Nothing)
        cbx_awal.Text = reg_jam1

        Dim reg_jam2 As String
        reg_jam2 = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "jam_end", Nothing)
        cbx_akh.Text = reg_jam2
    End Sub

    Private Sub UCtransaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim path, temp As String
        path = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "Server", Nothing)
        temp = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "LocalFolder", Nothing)
        jam1.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "jam_start", Nothing)
        jam2.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "jam_end", Nothing)
        interval.Text = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "interval", Nothing)

        Label11.Text = ""
        Label10.Text = ""
        TextBox1.Text = path
        TextBox2.Text = temp
        GroupBox5.Enabled = False
        Timer1.Start()
        Period.Text = DateAdd("d", -1, Date.Now)
        Period2.Text = Date.Now()
        If Period2.Text.ToString.Substring(0, 2) = "01" Then
            Period2.Text = DateAdd("d", -1, Date.Now)
        End If
        lbawal = Microsoft.VisualBasic.Right(Period.Text.Trim, 2) + Microsoft.VisualBasic.Mid(Period.Text.Trim, 4, 2) + Microsoft.VisualBasic.Left(Period.Text.Trim, 2)
        lbakhir = Microsoft.VisualBasic.Right(Period2.Text.Trim, 2) + Microsoft.VisualBasic.Mid(Period2.Text.Trim, 4, 2) + Microsoft.VisualBasic.Left(Period2.Text.Trim, 2)


        openf()
        list2()

        If CheckBox1.Checked = True Then
            Timer2.Start()
        Else
            Timer2.Stop()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Try
            If Button1.Enabled = True Then
                If CheckBox1.Checked = True Then
                    Timer2.Start()
                Else
                    Timer2.Stop()
                End If
            End If
        Catch exck As Exception
            ClsTolls.LogMail(exck.Message.ToString, exck.StackTrace, kodetoko.ToString)
        End Try

    End Sub

    Private Sub settimer_CheckedChanged(sender As Object, e As EventArgs) Handles settimer.CheckedChanged
        If settimer.Checked Then
            GroupBox5.Enabled = True
        Else
            GroupBox5.Enabled = False
        End If
    End Sub
    Sub sts()
        Try
            sql = "REPLACE   INTO `sales_net" + bln_dt + thn + "`  SELECT a.shop,a.TANGGAL,(SUM(IF(a.RTYPE='J' AND a.CAT_COD NOT IN (SELECT catcod FROM tblvir),a.GROSS-a.ppn,0))-SUM(IF(a.RTYPE='D' AND a.CAT_COD NOT IN (SELECT catcod FROM tblvir),a.GROSS-a.ppn,0))) AS salesnet,(SUM(IF(a.RTYPE='J' AND a.CAT_COD NOT IN (SELECT catcod FROM tblvir),a.HPP*a.QTY,0))-SUM(IF(a.RTYPE='D' AND a.CAT_COD NOT IN (SELECT catcod FROM tblvir),a.HPP*a.QTY,0))) AS saleshpp,COUNT(DISTINCT(CONCAT(shop,tanggal,docno,station,shift))) AS struk,station,shift FROM  DT_" + bln_dt + thn + " a    WHERE a.`tanggal`='" + tanggal.ToString + "' AND  a.shop='" + kodetoko.ToString + "'    AND a.PRDCD NOT IN(SELECT plu FROM plu_kecuali)     GROUP BY a.SHOP,a.TANGGAL,a.station,a.shift;"
            Dim cmddt3 As MySqlCommand = New MySqlCommand(sql, conn)
            cmddt3.ExecuteNonQuery()
        Catch exsts As Exception
            'log_dt()
            ClsTolls.Tulislog("eror di replace st" & sql)
            ClsTolls.LogMail(exsts.Message.ToString, exsts.StackTrace, kodetoko.ToString)
        End Try

    End Sub
    Sub penjualan()
        Try
            sql = "replace  into `penjualan" + bln_dt + thn + "`(SHOP,TANGGAL,RTYPE,`DIV`,`CAT_COD`,`PRDCD`,`sls_qty`,`sls_rp`,`sls_disc`,`sls_hpp`,`sls_brs`,`struk`,`ppn`) Select dt.SHOP , dt.TANGGAL, dt.RTYPE, dt.DIV, dt.CAT_COD,dt.PRDCD, sum(dt.QTY) as slsqty, sum(dt.GROSS) as slsrp, sum(dt.DISCOUNT) as slsdisc, sum(dt.QTY*dt.HPP) as slshpp, SUM(IF(BKP!='T' OR SUB_BKP!='Y',GROSS,GROSS-PPN)) as slsbrs, count(dt.PRDCD) as struk, sum(if(dt.BKP='T' and dt.SUB_BKP='Y',dt.PPN,0)) as slsppn from dt_" + bln_dt + thn + " as dt where dt.shop='" + kodetoko.ToString + "' and dt.tanggal='" + tanggal.ToString + "'  group by dt.SHOP, dt.TANGGAL, dt.RTYPE, dt.DIV, dt.CAT_COD, dt.PRDCD"
            Dim cmddt2 As MySqlCommand = New MySqlCommand(sql, conn)
            cmddt2.ExecuteNonQuery()


        Catch exdt As Exception
            'log_dt()
            ClsTolls.Tulislog("eror di replace penjualan " & sql)
            ClsTolls.LogMail(exdt.Message.ToString, exdt.StackTrace, kodetoko.ToString)
            'MsgBox(exdt.StackTrace)
        End Try
    End Sub
    Sub sls()
        Try
            sql = "replace   into `rekapsls" + bln_dt + thn + "`(shop,nama,tanggal,salesgross,salesnet,saleshpp,struk,tglupd)  SELECT a.shop,b.nama,a.TANGGAL,SUM(if(a.RTYPE='J',a.GROSS,(a.GROSS)*-1)) AS salesgross,SUM(if(a.RTYPE='J',a.GROSS-a.ppn,(a.gross-a.ppn)*-1)) AS salesnet,SUM(if(a.RTYPE='J',a.HPP*a.QTY,a.HPP*a.QTY*-1)) AS saleshpp,'' AS struk,CURDATE() FROM  DT_" + bln_dt + thn + " a LEFT JOIN shop b ON a.shop=b.kode  WHERE a.`tanggal`='" + tanggal.ToString + "' and  a.shop='" + kodetoko.ToString + "'  AND a.CAT_COD not in (select catcod from tblvir)  AND PRDCD NOT IN(select plu from plu_kecuali)  GROUP BY a.SHOP,a.TANGGAL;"
            Dim cmddt4 As MySqlCommand = New MySqlCommand(sql, conn)
            cmddt4.ExecuteNonQuery()

        Catch exsls As Exception
            'log_dt()
            ClsTolls.Tulislog("eror di replace sls" & sql)
            ClsTolls.LogMail(exsls.Message.ToString, exsls.StackTrace, kodetoko.ToString)
            'MsgBox(exsls.StackTrace)
        End Try
    End Sub

    Sub apka()
        Try

            sql = "create table if not exists absen_apka" + bln_dt + thn + " like absen"
            Dim apka As MySqlCommand = New MySqlCommand(sql, conn)
            apka.ExecuteNonQuery()


            sql = "create table if not exists apka_penjualan" + bln_dt + thn + " like apka_penjualan"
            Dim apk As MySqlCommand = New MySqlCommand(sql, conn)
            apk.ExecuteNonQuery()

            sql = "create table if not exists apka_rekapsls" + bln_dt + thn + " like apka_rekapsls"
            Dim apk1a As MySqlCommand = New MySqlCommand(sql, conn)
            apk1a.ExecuteNonQuery()

            sql = "create table if not exists tmp_apka_penjualan like apka_tmp"
            Dim apk1 As MySqlCommand = New MySqlCommand(sql, conn)
            apk1.ExecuteNonQuery()

            sql = "insert ignore into apka_penjualan" + bln_dt + thn + "(SHOP,TANGGAL,RTYPE,`DIV`,CAT_COD,PRDCD,SLS_QTY,SLS_RP,SLS_DISC,SLS_HPP,SLS_BRS,STRUK,PPN,NO_PESANAN) SELECT SHOP,TANGGAL,RTYPE,`DIV`,CAT_COD,PRDCD,SUM(QTY) AS QTY,SUM(GROSS) AS GROSS,DISCOUNT,SUM(HPP*QTY) AS HPP,SUM(GROSS-PPN) AS SALES_BERSIH,COUNT(DOCNO) AS STRUK,PPN,NOPESANAN FROM dt_" + bln_dt + thn + " WHERE nopesanan!='0' and tanggal='" + tanggal.ToString + "' and shop='" + kodetoko.ToString + "' GROUP BY SHOP,PRDCD,RTYPE ORDER BY PRDCD"
            Dim apk2 As MySqlCommand = New MySqlCommand(sql, conn)
            apk2.ExecuteNonQuery()

            sql = "create table if not exists APKA_struk_tmp select shop,tanggal,1 as struk from DT_" + bln_dt + thn + " where nopesanan!='0' and tanggal='" + tanggal.ToString + "' and shop='" + kodetoko.ToString + "' group by docno,shift,station"
            Dim cmd2b As MySqlCommand = New MySqlCommand(sql, conn)
            cmd2b.ExecuteNonQuery()

            sql = "insert ignore into apka_rekapsls" + bln_dt + thn + "(SHOP,NAMA,TANGGAL,SALESNET,SALESHPP,STRUK,UPD_DATE) select shop,'" + namatoko.ToString.ToUpper + "',tanggal,(select sum(if(rtype='j',gross-ppn,0))-sum(if(rtype='d',gross-ppn,0)) from dt_" + bln_dt + thn + " where cat_cod!='54005' and nopesanan!='0' and tanggal='" + tanggal.ToString + "' and shop='" + kodetoko.ToString + "'),(select sum(if(rtype='j',hpp*qty,0))-sum(if(rtype='d',hpp*qty,0)) from dt_" + bln_dt + thn + " where cat_cod!='54005' and nopesanan!='0' and tanggal='" + tanggal.ToString + "' and shop='" + kodetoko.ToString + "'),(select sum(struk) from APKA_struk_tmp),'" + tanggal.ToString + "' from dt_" + bln_dt + thn + " where nopesanan!='0' and tanggal='" + tanggal.ToString + "' and shop='" + kodetoko.ToString + "' group by shop"
            Dim cmd2d As MySqlCommand = New MySqlCommand(sql, conn)
            cmd2d.ExecuteNonQuery()

            sql = "drop table if exists APKA_struk_tmp"
            Dim apka3 As MySqlCommand = New MySqlCommand(sql, conn)
            apka3.ExecuteNonQuery()

            sql = "update absen_apka" + bln_dt + thn + " set TG" + hr_dt + "='1' where kdtk='" + kodetoko.ToString + "'"
            Dim apka5 As MySqlCommand = New MySqlCommand(sql, conn)
            apka5.ExecuteNonQuery()

        Catch exapka As Exception
            'MsgBox(ex.Message)
            ClsTolls.Tulislog("eror di replace proses apka" & sql)
            ClsTolls.LogMail(exapka.Message.ToString, exapka.StackTrace, kodetoko.ToString)
            'MsgBox(exapka.StackTrace)
        End Try

    End Sub

    Sub istore()
        Try
            sql = "CREATE TABLE IF NOT EXISTS `istore" + bln_dt + thn + "` like _m_istore ;"
            Dim istr As MySqlCommand = New MySqlCommand(sql, conn)
            istr.ExecuteNonQuery()

            sql = "DELETE FROM _M_ISTORE"
            Dim istr1 As MySqlCommand = New MySqlCommand(sql, conn)
            istr1.ExecuteNonQuery()

            sql = "DELETE FROM _M_ISTORE_STRUK"
            Dim istr2 As MySqlCommand = New MySqlCommand(sql, conn)
            istr2.ExecuteNonQuery()

            sql = "INSERT INTO _m_istore SELECT kode_toko AS kdtk,nama,tanggal AS tgl_data, SUM(IF(kode='01' AND subkode='02' OR kode='01' AND subkode='07',AMOUNT_CR,0)) slsnet,SUM(IF(kode='25' AND subkode='02',AMOUNT_DR,0)) slshpp,SUM(IF(kode='02' AND subkode='02' OR kode='02' AND subkode='07',AMOUNT_CR,0)) slsppn,'0' struk FROM glslp_" + thn + bln_dt + " where tanggal='" + tanggal.ToString + "' and kode_toko='" + kodetoko.ToString + "' GROUP BY kode_toko;"
            Dim istr3 As MySqlCommand = New MySqlCommand(sql, conn)
            istr3.ExecuteNonQuery()

            sql = "INSERT INTO _m_istore_struk SELECT kode_toko AS kdtk,tanggal AS tgl_data,COUNT(kode) struk FROM glslp_" + thn + bln_dt + " where tanggal='" + tanggal.ToString + "' and kode_toko='" + kodetoko.ToString + "' and kode IN(70) AND AMOUNT_DR > 0 GROUP BY kode_toko;"
            Dim istr4 As MySqlCommand = New MySqlCommand(sql, conn)
            istr4.ExecuteNonQuery()

            sql = "UPDATE _m_istore a,_m_istore_struk b SET a.`struk`=b.`struk` WHERE a.`kdtk`=b.`kdtk` AND a.`tgl_data`=b.`tgl_data`"
            Dim istr5 As MySqlCommand = New MySqlCommand(sql, conn)
            istr5.ExecuteNonQuery()

            sql = "INSERT IGNORE INTO istore" + bln_dt + thn + " SELECT * FROM _m_istore WHERE slsnet > 0"
            Dim istr6 As MySqlCommand = New MySqlCommand(sql, conn)
            istr6.ExecuteNonQuery()

            sql = "DELETE from _m_istore;DELETE from _m_istore_struk"
            Dim istr7 As MySqlCommand = New MySqlCommand(sql, conn)
            istr7.ExecuteNonQuery()

        Catch existore As Exception
            'log_glslp()
            ClsTolls.Tulislog("eror di proses istore" & sql)
            ClsTolls.LogMail(existore.Message.ToString, existore.StackTrace, kodetoko.ToString)
            'MsgBox(existore.StackTrace)
        End Try


    End Sub
    Sub custab()
        Try
            Dim ary As String() = New String() {"00:00:00", "01:00:00", "02:00:00", "03:00:00", "04:00:00", "05:00:00", "06:00:00", "07:00:00", "08:00:00", "09:00:00", "10:00:00", "11:00:00", "12:00:00", "13:00:00", "14:00:00", "15:00:00", "16:00:00", "17:00:00", "18:00:00", "19:00:00", "20:00:00", "21:00:00", "22:00:00", "23:00:00"}
            Dim ary2 As String() = New String() {"00:59:59", "01:59:59", "02:59:59", "03:59:59", "04:59:59", "05:59:59", "06:59:59", "07:59:59", "08:59:59", "09:59:59", "10:59:59", "11:59:59", "12:59:59", "13:59:59", "14:59:59", "15:59:59", "16:59:59", "17:59:59", "18:59:59", "19:59:59", "20:59:59", "21:59:59", "22:59:59", "23:59:59"}

            For ii As Integer = 0 To ary.Length - 1
                sql = "insert ignore into custab" + bln_dt + thn + " select shop, tanggal, rtype, station, shift, '" + ary(ii) + "' as jamsls, sum(qty) as slsqty, sum(gross) as salesrp, sum(QTY*hpp) as saleshpp, sum(gross-ppn) as salesnet, count(distinct(docno))as struk, sum(ppn) as slsppn from dt_" + bln_dt + thn + " where tanggal='" + tanggal.ToString + "' and cat_cod not in (select catcod from tblvir) and prdcd not in (select plu from plu_kecuali) and jam between '" + ary(ii) + "' and '" + ary2(ii) + "' and shop='" + kodetoko.ToString + "' group by rtype,station,shift,shop;"
                Dim cmdzx As MySqlCommand = New MySqlCommand(sql, conn)
                cmdzx.ExecuteNonQuery()
            Next
        Catch excstb As Exception
            ClsTolls.Tulislog("eror di proses custab" & sql)
            ClsTolls.LogMail(excstb.Message.ToString, excstb.StackTrace, kodetoko.ToString)
            'MsgBox(excstb.StackTrace)
        End Try
    End Sub

    Sub updsls()
        Try
            sql = "UPDATE rekapsls" + bln_dt + thn + " a,slp b SET a.struk=b.jml_strk WHERE a.shop=b.toko AND a.tanggal=b.`tanggal` AND a.shop='" + kodetoko.ToString + "' AND a.`tanggal`='" + tanggal.ToString + "';"
            Dim cmslp As MySqlCommand = New MySqlCommand(sql, conn)
            cmslp.ExecuteNonQuery()
        Catch exupdsls As Exception
            ClsTolls.Tulislog("eror di updatesls" & sql)
            ClsTolls.LogMail(exupdsls.Message.ToString, exupdsls.StackTrace, kodetoko.ToString)
            'MsgBox(exupdsls.StackTrace)
        End Try

    End Sub

    Private Sub period_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Period.ValueChanged
        lbawal = Microsoft.VisualBasic.Right(Period.Text.Trim, 2) + Microsoft.VisualBasic.Mid(Period.Text.Trim, 4, 2) + Microsoft.VisualBasic.Left(Period.Text.Trim, 2)
    End Sub
    Private Sub period2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Period2.ValueChanged
        lbakhir = Microsoft.VisualBasic.Right(Period2.Text.Trim, 2) + Microsoft.VisualBasic.Mid(Period2.Text.Trim, 4, 2) + Microsoft.VisualBasic.Left(Period2.Text.Trim, 2)
    End Sub
End Class
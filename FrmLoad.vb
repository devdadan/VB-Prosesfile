Imports System
Imports MySql.Data.MySqlClient
Imports System.IO
Public Class FrmLoad
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Form1.GroupBox1.Enabled = False
        Pb_Loading.Value = 0
        Try
            Dim str As String = "Server=" + Formdb.TextBox1.Text + ";user id=" + Formdb.TextBox2.Text + ";password=" + Formdb.TextBox4.Text + ";database=" + Formdb.TextBox3.Text + ""
            conn = New MySqlConnection(str)
            conn.Open()

            SQL = "CREATE TABLE IF NOT EXISTS `glslp_new` (
  `KODE` varchar(6) DEFAULT NULL,
  `SUBKODE` varchar(15) DEFAULT NULL,
  `PLU` varchar(24) DEFAULT NULL,
  `BIN` varchar(90) DEFAULT NULL,
  `DESC` varchar(100) DEFAULT NULL,
  `AMOUNT_DR` decimal(14,3) DEFAULT '0.000',
  `FEE_DPP_DR` decimal(14,3) DEFAULT '0.000',
  `FEE_DR_PPN` decimal(14,3) DEFAULT '0.000',
  `AMOUNT_CR` decimal(14,3) DEFAULT '0.000',
  `FEE_DPP_CR` decimal(14,3) DEFAULT '0.000',
  `FEE_CR_PPN` decimal(14,3) DEFAULT '0.000',
  `QTY` int(11) DEFAULT '0',
  `NO_KSM` varchar(400) DEFAULT NULL,
  `DOCNO` varchar(100) DEFAULT NULL,
  `REF_ID_TOKO` varchar(100) DEFAULT NULL,
  `PROMO_CODE` varchar(100) DEFAULT NULL,
  `PERIODE_CICILAN` varchar(60) DEFAULT NULL,
  `PERSENTASE_MDR` varchar(60) DEFAULT NULL,
  `FLAG_EWL` varchar(100) DEFAULT NULL,
  `REF_1` varchar(200) DEFAULT NULL,
  `REF_2` varchar(200) DEFAULT NULL,
  `REF_3` varchar(200) DEFAULT NULL,
  `REF_4` varchar(200) DEFAULT NULL,
  `REF_5` varchar(200) DEFAULT NULL,
  `REF_6` varchar(200) DEFAULT NULL,
  `REF_7` varchar(200) DEFAULT NULL,
  `REF_8` varchar(200) DEFAULT NULL,
  `REF_9` varchar(200) DEFAULT NULL,
  `REF_10` varchar(200) DEFAULT NULL,
  `KODE_TOKO` varchar(4) DEFAULT NULL,
  `NAMA` varchar(50) DEFAULT NULL,
  `TANGGAL` date DEFAULT NULL,
  KEY `Index_KODETOKO` (`KODE_TOKO`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1
;"
            Dim cmd As MySqlCommand = New MySqlCommand(SQL, conn)
            cmd.ExecuteNonQuery()

            SQL = "CREATE TABLE IF NOT EXISTS `_m_istore` (`kdtk` varchar(4) NOT NULL DEFAULT '',`nama` varchar(100) NOT NULL DEFAULT '',`tgl_data` varchar(15) NOT NULL DEFAULT '',`slsnet` decimal(34,2) DEFAULT NULL,`slshpp` decimal(34,2) DEFAULT NULL,`slsppn` decimal(34,2) DEFAULT NULL,`struk` decimal(34,0) NOT NULL DEFAULT '0', PRIMARY KEY (`kdtk`,`tgl_data`)) ENGINE=InnoDB DEFAULT CHARSET=latin1;"
            Dim cmda As MySqlCommand = New MySqlCommand(SQL, conn)
            cmda.ExecuteNonQuery()

            SQL = "CREATE TABLE IF NOT EXISTS `_m_istore_struk` (`kdtk` varchar(4) NOT NULL DEFAULT '',`tgl_data` varchar(15) NOT NULL DEFAULT '',`struk` decimal(34,0) NOT NULL DEFAULT '0', PRIMARY KEY (`kdtk`,`tgl_data`)) ENGINE=InnoDB DEFAULT CHARSET=latin1;"
            Dim cmdb As MySqlCommand = New MySqlCommand(SQL, conn)
            cmdb.ExecuteNonQuery()
            'Pb_Loading.Value = 40
            SQL = "CREATE TABLE IF NOT EXISTS `dt_new` (
  `RECID` varchar(100) DEFAULT NULL,
  `STATION` varchar(100) DEFAULT NULL,
  `SHIFT` varchar(100) DEFAULT NULL,
  `RTYPE` varchar(100) DEFAULT NULL,
  `DOCNO` varchar(100) DEFAULT NULL,
  `SEQNO` varchar(100) DEFAULT NULL,
  `DIV` varchar(100) DEFAULT NULL,
  `PRDCD` varchar(100) DEFAULT NULL,
  `QTY` varchar(100) DEFAULT NULL,
  `PRICE` varchar(100) DEFAULT NULL,
  `GROSS` varchar(100) DEFAULT NULL,
  `DISCOUNT` varchar(100) DEFAULT NULL,
  `SHOP` varchar(100) DEFAULT NULL,
  `TANGGAL` varchar(100) DEFAULT NULL,
  `JAM` varchar(100) DEFAULT NULL,
  `KONS` varchar(100) DEFAULT NULL,
  `TTYPE` varchar(100) DEFAULT NULL,
  `HPP` varchar(100) DEFAULT NULL,
  `PROMOSI` varchar(100) DEFAULT NULL,
  `PPN` varchar(100) DEFAULT NULL,
  `PTAG` varchar(100) DEFAULT NULL,
  `CAT_COD` varchar(100) DEFAULT NULL,
  `NM_VCR` varchar(100) DEFAULT NULL,
  `NO_VCR` varchar(100) DEFAULT NULL,
  `BKP` varchar(100) DEFAULT NULL,
  `SUB_BKP` varchar(100) DEFAULT NULL,
  `PLUMD` varchar(100) DEFAULT NULL,
  `NOPESANAN` varchar(100) DEFAULT NULL,
  `BERBAYAR` char(10) DEFAULT NULL,
  `NOACC` varchar(100) DEFAULT NULL,
  `HIGHIMPACT` double DEFAULT '0',
  `PPN_RATE` decimal(10,0) DEFAULT NULL,
  KEY `Index_TOKO` (`SHOP`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;"

            Dim cmdc As MySqlCommand = New MySqlCommand(SQL, conn)
            cmdc.ExecuteNonQuery()
            'Pb_Loading.Value = 70
            SQL = "CREATE TABLE IF NOT EXISTS `apka_penjualan` (
  `shop` varchar(4) NOT NULL DEFAULT '',
  `tanggal` date NOT NULL DEFAULT '0000-00-00',
  `rtype` char(1) NOT NULL DEFAULT '',
  `div` char(2) NOT NULL DEFAULT '',
  `cat_cod` varchar(6) DEFAULT '',
  `PRDCD` varchar(8) NOT NULL DEFAULT '',
  `sls_qty` double DEFAULT '0',
  `sls_rp` double DEFAULT '0',
  `sls_disc` double DEFAULT '0',
  `sls_hpp` double DEFAULT '0',
  `sls_brs` double DEFAULT '0',
  `struk` double DEFAULT '0',
  `stk_qty` double DEFAULT '0',
  `stk_hpp` double DEFAULT '0',
  `ppn` double DEFAULT '0',
  `struk_toko` double DEFAULT '0',
  `sales_type` char(1) DEFAULT '0',
  `pkm_min` double DEFAULT '0',
  `pkm_max` double DEFAULT '0',
  `n_plus` double DEFAULT '0',
  `n_kali` double DEFAULT '0',
  `minor` double DEFAULT '0',
  `bkp` tinyint(1) DEFAULT '0',
  `sub_bkp` char(1) DEFAULT '',
  `no_pesanan` varchar(25) DEFAULT NULL,
  PRIMARY KEY (`shop`,`tanggal`,`rtype`,`div`,`PRDCD`),
  KEY `kunciidx2` (`tanggal`),
  KEY `kunciidx3` (`rtype`),
  KEY `kunciidx4` (`div`),
  KEY `kunciidx5` (`PRDCD`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;"

            Dim cmdd As MySqlCommand = New MySqlCommand(SQL, conn)
            cmdd.ExecuteNonQuery()

            SQL = "CREATE TABLE IF NOT EXISTS `apka_rekapsls` (
  `SHOP` varchar(4) NOT NULL,
  `NAMA` varchar(50) DEFAULT NULL,
  `TANGGAL` date NOT NULL DEFAULT '0000-00-00',
  `SALESNET` double DEFAULT '0',
  `SALESHPP` double DEFAULT '0',
  `STRUK` int(11) DEFAULT '0',
  `UPD_DATE` date DEFAULT '0000-00-00',
  PRIMARY KEY (`SHOP`,`TANGGAL`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;"

            Dim cmde As MySqlCommand = New MySqlCommand(SQL, conn)
            cmde.ExecuteNonQuery()
            'Pb_Loading.Value = 95
            SQL = "CREATE TABLE IF NOT EXISTS `apka_tmp` (
  `shop` varchar(4) NOT NULL DEFAULT '',
  `station` varchar(2) NOT NULL,
  `shift` varchar(2) NOT NULL,
  `tanggal` date NOT NULL DEFAULT '0000-00-00',
  `rtype` char(1) NOT NULL DEFAULT '',
  `docno` varchar(9) NOT NULL,
  `div` char(2) NOT NULL DEFAULT '',
  `cat_cod` varchar(6) DEFAULT '',
  `PRDCD` varchar(8) NOT NULL DEFAULT '',
  `sls_qty` double DEFAULT '0',
  `sls_rp` double DEFAULT '0',
  `sls_disc` double DEFAULT '0',
  `sls_hpp` double DEFAULT '0',
  `sls_brs` double DEFAULT '0',
  `struk` double DEFAULT '0',
  `stk_qty` double DEFAULT '0',
  `stk_hpp` double DEFAULT '0',
  `ppn` double DEFAULT '0',
  `struk_toko` double DEFAULT '0',
  `sales_type` char(1) DEFAULT '0',
  `pkm_min` double DEFAULT '0',
  `pkm_max` double DEFAULT '0',
  `n_plus` double DEFAULT '0',
  `n_kali` double DEFAULT '0',
  `minor` double DEFAULT '0',
  `bkp` tinyint(1) DEFAULT '0',
  `sub_bkp` char(1) DEFAULT '',
  `no_pesanan` varchar(25) DEFAULT NULL,
  PRIMARY KEY (`shop`,`station`,`shift`,`tanggal`,`rtype`,`docno`,`div`,`PRDCD`),
  KEY `kunciidx2` (`tanggal`),
  KEY `kunciidx3` (`rtype`),
  KEY `kunciidx4` (`div`),
  KEY `kunciidx5` (`PRDCD`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1
;"
            Dim cmdf As MySqlCommand = New MySqlCommand(SQL, conn)
            cmdf.ExecuteNonQuery()

            SQL = "CREATE TABLE IF NOT EXISTS `sto_new` (
  `tgl_stockout` date DEFAULT NULL,
  `prdcd` char(10) DEFAULT '',
  `jenis_stockout` char(3) DEFAULT NULL,
  `toko` char(4) DEFAULT NULL,
  `ptag` char(2) DEFAULT NULL,
  `sales` char(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;"
            Dim cmdg As MySqlCommand = New MySqlCommand(SQL, conn)
            cmdg.ExecuteNonQuery()

            SQL = "CREATE TABLE IF NOT EXISTS `st_new` (
  `RECID` varchar(100) DEFAULT NULL,
  `PRDCD` varchar(100) DEFAULT NULL,
  `CAT_COD` varchar(100) DEFAULT NULL,
  `BEGBAL` varchar(100) DEFAULT NULL,
  `QTY` decimal(10,0) DEFAULT '0',
  `MAX` double DEFAULT '0',
  `PKM_G` double DEFAULT '0',
  `PKM_N` varchar(100) DEFAULT NULL,
  `PKM_P` varchar(100) DEFAULT NULL,
  `PKM_K` varchar(100) DEFAULT NULL,
  `PKM_J` varchar(100) DEFAULT NULL,
  `PRICE` varchar(100) DEFAULT NULL,
  `MIN_DISP` varchar(100) DEFAULT NULL,
  `KAP_DISP` varchar(100) DEFAULT NULL,
  `AQTY` decimal(10,0) DEFAULT '0',
  `BQTY` decimal(10,0) DEFAULT '0',
  `CQTY` decimal(10,0) DEFAULT '0',
  `DQTY` decimal(10,0) DEFAULT '0',
  `EQTY` decimal(10,0) DEFAULT '0',
  `FQTY` decimal(10,0) DEFAULT '0',
  `STO1` varchar(100) DEFAULT NULL,
  `STO2` varchar(100) DEFAULT NULL,
  `STO3` varchar(100) DEFAULT NULL,
  `STO4` varchar(100) DEFAULT NULL,
  `STO5` varchar(100) DEFAULT NULL,
  `STO6` varchar(100) DEFAULT NULL,
  `AHJ` varchar(100) DEFAULT NULL,
  `BHJ` varchar(100) DEFAULT NULL,
  `CHJ` varchar(100) DEFAULT NULL,
  `DHJ` varchar(100) DEFAULT NULL,
  `EHJ` varchar(100) DEFAULT NULL,
  `FHJ` varchar(100) DEFAULT NULL,
  `ARP` varchar(100) DEFAULT NULL,
  `BRP` varchar(100) DEFAULT NULL,
  `CRP` varchar(100) DEFAULT NULL,
  `DRP` varchar(100) DEFAULT NULL,
  `ERP` varchar(100) DEFAULT NULL,
  `FRP` varchar(100) DEFAULT NULL,
  `SPD` varchar(100) DEFAULT NULL,
  `ACOST` double DEFAULT '0',
  `LCOST` double DEFAULT '0',
  `PLUMD` varchar(100) DEFAULT NULL,
  `PRCOST` varchar(100) DEFAULT NULL,
  `PLCOST` varchar(100) DEFAULT NULL,
  `TP_GDG` varchar(100) DEFAULT NULL,
  `STAUT` varchar(100) DEFAULT NULL,
  `RP_ADJ_K` varchar(100) DEFAULT NULL,
  `RP_ADJ_L` varchar(100) DEFAULT NULL,
  `BA` varchar(100) DEFAULT NULL,
  `BS` varchar(100) DEFAULT NULL,
  `KODE_TOKO` varchar(100) DEFAULT NULL,
  `TANGGAL` date DEFAULT NULL,
  KEY `Index_KODETOKO` (`KODE_TOKO`,`PRDCD`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;"
            'Pb_Loading.Value = 100
            Dim cmdh As MySqlCommand = New MySqlCommand(SQL, conn)
            cmdh.ExecuteNonQuery()
            Form1.Show()



        Catch ex As Exception
            MsgBox(ex.StackTrace)
        End Try
    End Sub
    Dim bln As String
    Dim thn As String
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Pb_Loading.Value < 100 Then
            Pb_Loading.Value += 2
        ElseIf Pb_Loading.Value = 100 Then
            Timer1.Stop()
            Me.Close()
            Form1.GroupBox1.Enabled = True
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Pb_Loading_Click(sender As Object, e As EventArgs) Handles Pb_Loading.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub
End Class
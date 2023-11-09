Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Environment
Imports System.Net.Mail
Imports System.Text
Imports System.Net
Imports System.Data
Public Class ClsTolls
    Public Shared Sub Tulislog(slog As String)
        Try
            Dim thn As String = Format(Now, "yyyy-MM-dd")
            Dim th As String = Format(Now, "yyyyMMdd")
            Dim thnn As String = Format(Now, "yyyy-MM-dd HH:mm:ss")
            Dim tulis As StreamWriter
            If Not File.Exists(Application.StartupPath & "\LOG_" + th + ".txt") Then
                tulis = File.CreateText(Application.StartupPath & "\LOG_" + th + ".txt")
                tulis.WriteLine("##### LOG PROGRAM #####")
                tulis.WriteLine(thnn & " : " & slog)
                tulis.Flush()
                tulis.Close()
            Else
                tulis = File.AppendText(Application.StartupPath & "\LOG_" + th + ".txt")
                tulis.WriteLine(thnn & " : " & slog)
                tulis.Flush()
                tulis.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Shared Sub LogMail(xini As String, xini2 As String, toko As String)

        Dim str As String = "edp_adm_simulasi_2@jkt2.indomaret.co.id;sahmadan1997@gmail.com"
        Dim builder As New StringBuilder
            builder.Append(" <html><head><style>table, td, th{ border:1px solid green;font-family: 'Cambria';font-size: 12px;}th{background-color:green;color:white; }body{ color:#333;font-family: 'Trebuchet MS';font-size: 14px;}</style></head><body>")
            builder.Append(String.Concat(New String() {"<p>Hai ..</p></br>"}))
            builder.Append("Tolong cek program ProsesFile.exe di server 100 </br>")
            builder.Append("<p>Karena ada Eror dibawah ini<p></br>")
            builder.Append("<p>##########################################<p></br>")
            builder.Append("<p>Toko : " + toko + "<p></br>")
            builder.Append("<p>Message : " + xini + "<p></br>")
            builder.Append("<p>Stack Trace : " + xini2 + "<p></br>")

            builder.Append("</em>Salam<br>System ProsesFile.exe</em>")
            Try
                Dim message As New MailMessage
                message.From = New MailAddress("edp_adm_simulasi_1@jkt2.indomaret.co.id", "SYSTEM PROSESFILE.EXE")
                Dim strArray As String() = str.Split(New Char() {";"c})
                Dim i As Integer
                For i = 0 To strArray.GetLength(0) - 1
                    message.To.Add(strArray(i))
                Next i
                message.Subject = ("LOG ERROR PROSESFILE.EXE")
                message.Body = builder.ToString
                message.IsBodyHtml = True
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                Dim client As New SmtpClient("192.168.85.201")
                client.UseDefaultCredentials = True
                client.Credentials = New NetworkCredential("edp_adm_simulasi_1", "password")
                client.DeliveryMethod = SmtpDeliveryMethod.Network
                client.EnableSsl = False
                client.Send(message)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


    End Sub

    Public Shared Sub LogMail2(Cini As String)
        Dim str As String = "edp_adm_simulasi_2@jkt2.indomaret.co.id;sahmadan1997@gmail.com"
        Dim builder As New StringBuilder
        builder.Append(" <html><head><style>table, td, th{ border:1px solid green;font-family: 'Cambria';font-size: 12px;}th{background-color:green;color:white; }body{ color:#333;font-family: 'Trebuchet MS';font-size: 14px;}</style></head><body>")
        builder.Append(String.Concat(New String() {"<p>Hai ..</p></br>"}))
        builder.Append("LOG TOKO UNREGISTER</br>")
        builder.Append("<p>----------------------------</br>")
        builder.Append("<p>" + Cini + "<p></br>")
        builder.Append("</em>Salam<br>System ProsesFile.exe</em>")
        Try
            Dim message As New MailMessage
            message.From = New MailAddress("edp_adm_simulasi_1@jkt2.indomaret.co.id", "LOG TOKO UNREGISTER PROSESFILE.EXE")
            Dim strArray As String() = str.Split(New Char() {";"c})
            Dim i As Integer
            For i = 0 To strArray.GetLength(0) - 1
                message.To.Add(strArray(i))
            Next i
            message.Subject = ("LOG TOKO UNREGISTER PROSESFILE.EXE")
            message.Body = builder.ToString
            message.IsBodyHtml = True
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
            Dim client As New SmtpClient("192.168.85.201")
            client.UseDefaultCredentials = True
            client.Credentials = New NetworkCredential("edp_adm_simulasi_1", "password")
            client.DeliveryMethod = SmtpDeliveryMethod.Network
            client.EnableSsl = False
            client.Send(message)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Sub filelog(inilog As String, initoko As String)
        Dim logs As String
        logs = My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\edp", "log", Nothing)
        If logs = "TXT" Then
            Try
                Dim thn As String = Format(Now, "yyyy-MM-dd")
                Dim th As String = Format(Now, "yyyyMMdd")
                Dim thnn As String = Format(Now, "yyyy-MM-dd HH:mm:ss")
                Dim tulis As StreamWriter
                If Not File.Exists(Application.StartupPath & "\LOG_" + th + ".txt") Then
                    tulis = File.CreateText(Application.StartupPath & "\LOG_" + th + ".txt")
                    tulis.WriteLine("##### LOG PROGRAM #####")
                    tulis.WriteLine(thnn & " : " & inilog)
                    tulis.Flush()
                    tulis.Close()
                Else
                    tulis = File.AppendText(Application.StartupPath & "\LOG_" + th + ".txt")
                    tulis.WriteLine(thnn & " : " & inilog)
                    tulis.Flush()
                    tulis.Close()
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        ElseIf logs = "EMAIL" Then
            Dim str As String = "edp_adm_simulasi_2@jkt2.indomaret.co.id;sahmadan1997@gmail.com"
            Dim builder As New StringBuilder
            builder.Append(" <html><head><style>table, td, th{ border:1px solid green;font-family: 'Cambria';font-size: 12px;}th{background-color:green;color:white; }body{ color:#333;font-family: 'Trebuchet MS';font-size: 14px;}</style></head><body>")
            builder.Append(String.Concat(New String() {"<p>Hai ..</p></br>"}))
            builder.Append("Tolong cek program ProsesFile.exe di server 100 </br>")
            builder.Append("<p>Karena ada Eror dibawah ini<p></br>")
            builder.Append("<p>##########################################<p></br>")
            builder.Append("<p>Toko : " + initoko + "<p></br>")
            builder.Append("<p>Message : " + inilog + "<p></br>")

            builder.Append("</em>Salam<br>System ProsesFile.exe</em>")
            Try
                Dim message As New MailMessage
                message.From = New MailAddress("edp_adm_simulasi_1@jkt2.indomaret.co.id", "SYSTEM PROSESFILE.EXE")
                Dim strArray As String() = str.Split(New Char() {";"c})
                Dim i As Integer
                For i = 0 To strArray.GetLength(0) - 1
                    message.To.Add(strArray(i))
                Next i
                message.Subject = ("LOG ERROR PROSESFILE.EXE")
                message.Body = builder.ToString
                message.IsBodyHtml = True
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                Dim client As New SmtpClient("192.168.85.201")
                client.UseDefaultCredentials = True
                client.Credentials = New NetworkCredential("edp_adm_simulasi_1", "password")
                client.DeliveryMethod = SmtpDeliveryMethod.Network
                client.EnableSsl = False
                client.Send(message)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        Else
        End If
    End Sub
End Class

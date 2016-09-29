using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;


/// <summary>
/// Summary description for Email
/// </summary>
public class Email
{
    public void SendEmail(List<string> toList, string subject, string body, List<string> cclist)
    {
        string to = "", cc = "";
        foreach (string temp_to in toList)
            to += temp_to + ";";
        foreach (string temp_cc in cclist)
            cc += temp_cc + ";";
        int exp = SendEmail("emp.logicu@gmail.com", "logic1234", 465, body, to, subject, cc);
        if (exp == 0)
            System.Diagnostics.Debug.WriteLine("Fail to send Email");
        else
            System.Diagnostics.Debug.WriteLine("Successfully to send Email");
    }
    public int SendEmail(string from, string password, int port, string body, string to, string subject, string cc)
    {
        //string ToEmail;
        bool fSSL = true;
        try
        {
            //Creating Message object

            System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", port);
            if (fSSL)
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", from);
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);

            //Preparing the message object....

            message.From = from;
            message.To = to;
            message.Cc = cc;
            message.Subject = subject;
            message.BodyFormat = System.Web.Mail.MailFormat.Html;
            message.Body = body;
            System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com";

            Thread email = new Thread(delegate()
            {
                System.Web.Mail.SmtpMail.Send(message);
            });

            email.IsBackground = true;
            email.Start();
            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
}
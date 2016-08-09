using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using SvitSoftTask.Models;


namespace SvitSoftTask.Helpers
{
    public class MailHelper
    {
        static private readonly string MailFrom = WebConfigurationManager.AppSettings["MailFrom"];
        static private readonly string SmtpServer = WebConfigurationManager.AppSettings["SmtpServer"];
        static private readonly string DisplayName = WebConfigurationManager.AppSettings["MailDisplayName"];
        static private readonly string SmtpUser = WebConfigurationManager.AppSettings["SmtpUser"];
        static private readonly string SmtpUserPassword = WebConfigurationManager.AppSettings["SmtpUserPassword"];
        static private readonly string SmtpPort = WebConfigurationManager.AppSettings["SmtpPort"];
        
        public static void SendMail(string mailTo, string subject, string body)
        {
            SendMail(MailFrom, DisplayName, mailTo, subject, body);
        }

        public static void SendMail(string mailFrom, string displayName, string mailTo, string subject, string body)
        {
            using (MailMessage msg = new MailMessage())
            {

                msg.From = new MailAddress(mailFrom, displayName);
                msg.To.Add(new MailAddress(mailTo));

                msg.Subject = subject;
                msg.SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8");

                msg.IsBodyHtml = true;
                msg.Body = body;
   
                SmtpClient smtpServer = new SmtpClient(SmtpServer)
                {
                    Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpUserPassword),
                    Port = int.Parse(SmtpPort),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
           
                };

                smtpServer.Send(msg);
            }
        }
    }
}
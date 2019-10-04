using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Stack.Models
{
    public class EmailModel
    {
        public static void NewUserRegistration()
        {
            string smtpAddress = "smtp.gmail.com";
            int portNumber = 587;

            string emailFrom = "tutorialsbasket.com@gmail.com";
            string password = "tasson35";
            string emailTo = "trsmca35@gmail.com";
            string subject = "Hello";
            //string body = "Hello, I'm just writing this to say Hi!";
            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            //using (StreamReader reader = new StreamReader(Server.MapPath("~/Templates/NewRegistration.html")))
            //{

            //    body = reader.ReadToEnd();

            //}
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.

                mail.Attachments.Add(new Attachment("D:\\license.htm"));
                mail.Attachments.Add(new Attachment("D:\\AdminDeployment.xml"));

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
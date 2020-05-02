using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Stack.Helpers
{
    public class EmailHelper
    {
        public static void SendForgotPasswordEmail(string subject, string emailToAddress, string emailTemplate, string logo, string bodyImage, string guid)
        {
            string body;
            using (var reader = new StreamReader(emailTemplate))
            {
                body = reader.ReadToEnd();
            }
            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(Constants.emailFromAddress);
                mail.To.Add(emailToAddress);
                mail.Subject = subject;
                body = body.Replace("##resetpasswordlink##", "http://localhost:14481/Account/ResetPassword?id=" + guid + "");
                mail.Body = body;
                mail.IsBodyHtml = true;

                var htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                var res = new LinkedResource(logo) { ContentId = "logo" };
                htmlView.LinkedResources.Add(res);
                mail.AlternateViews.Add(htmlView);

                LinkedResource res1 = new LinkedResource(bodyImage) { ContentId = "bodyImage" };
                htmlView.LinkedResources.Add(res1);
                mail.AlternateViews.Add(htmlView);

                using (var smtp = new SmtpClient(Constants.smtpAddress, Constants.portNumber))
                {
                    smtp.Credentials = new NetworkCredential(Constants.emailFromAddress, Constants.password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
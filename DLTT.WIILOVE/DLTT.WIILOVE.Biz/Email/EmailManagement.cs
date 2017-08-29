using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DLTT.WIILOVE.Biz.Email
{
    class EmailManagement
    {
        string smtpAddress = "smtp.gmail.com";
        int portNumber = 587;
        bool enableSSL = true;

        string emailFrom = "wiilovedltt@gmail.com";
        string password = "tiennguyen2nt";

        public void SendEmail(string emailTo, string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.
                //Đính kèm file
                //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                //To embed images in email
                
                AlternateView av =
                    AlternateView.CreateAlternateViewFromString(body,
                        null, MediaTypeNames.Text.Html);
               
              
                mail.AlternateViews.Add(av);



                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }
}

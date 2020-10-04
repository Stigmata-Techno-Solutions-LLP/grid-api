using System;
using Serilog;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Serilog;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace GridManagement.common
{

    public static class Util
    {
        
        public static void LogError(Exception ex) {
Log.Logger.Error( ex.Message  + "\n"  +(ex.InnerException == null ? "": ex.InnerException.Message) + "\n" + ex.StackTrace);
        }

        
                public static bool SendMail(string subject, string bodyHtml, string toEmail, string fromMail, string pwd)
        {
            bool isEMailSent = false;
            try
            {
                // var email = new MimeMessage();
                // email.Sender = MailboxAddress.Parse(_appSettings.FromEmail);
                // email.To.Add(MailboxAddress.Parse(toEmail));
                // email.Subject = subject;
                // email.Body = new TextPart(TextFormat.Html) { Text = bodyHtml };

                // // send email
                // using var smtp = new SmtpClient();
                // smtp.Connect(_appSettings.Server, Convert.ToInt32(_appSettings.Port), SecureSocketOptions.StartTls);
                // smtp.Authenticate(_appSettings.Username, _appSettings.Password);
                // smtp.Send(email);
                // smtp.Disconnect(true);

            var client = new SendGridClient(pwd);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromMail, "L&T GridManagement"),
                Subject = subject,
                HtmlContent = bodyHtml
            };

            
            msg.AddTo(new EmailAddress(toEmail));        
            Task response = client.SendEmailAsync(msg);
                isEMailSent = true;
                return isEMailSent;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
                Log.Logger.Information(ex.Message);
                return false;
            }
        }
          public static string Base64Decode(string base64EncodedData) {
            try {

        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            } catch(Exception ex) {
                throw  new ValueNotFoundException("password is incorrect");
            }
        }
    }
}
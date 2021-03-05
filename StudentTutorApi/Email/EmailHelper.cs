using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace StudentTutorApi.Controllers
{
    public class EmailHelper
    {
        public bool SendEmail(string userEmail, string confirmationLink, string password)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["email"], "Student Tutor");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;
            mailMessage.BodyEncoding = Encoding.UTF8;

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email"], password);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Port = 587;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                var error = ex;
                // log exception
            }
            return false;
        }
    }
}
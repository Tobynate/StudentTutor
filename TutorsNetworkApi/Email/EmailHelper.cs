using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TutorsNetworkApi.Email
{
    public class EmailHelper
    {
        private readonly IConfiguration _config;

        public EmailHelper(IConfiguration configuration)
        {
            this._config = configuration;
        }
        public bool SendEmail(string userEmail, string confirmationLink, string password)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_config.GetValue<string>("email"), "Student Tutor");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;
            mailMessage.BodyEncoding = Encoding.UTF8;

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_config.GetValue<string>("email"), password);
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

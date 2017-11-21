using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace VolunteerSystem
{
    public class Notifier
    {


        public static void InformAdmin()
        {
            throw new NotImplementedException();
        }

        public static void InformVolunteer(Volunteer volunteer)
        {
            var fromAddress = new MailAddress("cazzcasper@gmail.com", "From Name");
            var toAddress = new MailAddress(volunteer.Email, volunteer.Name);
            string fromPassword = "";
            string subject = "Subject";
            string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

    }
}

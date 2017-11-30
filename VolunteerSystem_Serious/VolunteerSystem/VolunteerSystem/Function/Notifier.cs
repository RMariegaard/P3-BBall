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

        public static void InformVolunteer(Volunteer volunteer, string messageContent)
        {
            var fromAddress = new MailAddress("hackermark1234@gmail.com", "Aarhus Basketball Festival");
            var toAddress = new MailAddress(volunteer.Email, volunteer.Name);
            string fromPassword = "1234Hackermark";
            string subject = "Changes to your shift";
            string body = messageContent;

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

        public static void InformVolunteer(Volunteer volunteer, Shift oldShift, Shift newShift, string[] changes)
        {
            string message = $"Dear {volunteer.Name},\nThe following changes has happen to the {oldShift.Task} shift at {oldShift.StartTime}:\n";
            foreach(string change in changes)
            {
                object oldAt = findAttribute(oldShift, change);
                object newAt = findAttribute(newShift, change);
                if ((oldAt == null || newAt == null))
                {
                    //skip
                }
                else
                {
                    message += $"The {change} has been changed from {oldAt} to {newAt}.\n";
                }

            }
            message += "Aarhus Basketball Festival";
            InformVolunteer(volunteer, message);
        }

        public static void InformVolunteer(Volunteer volunteer, Shift shift, InformShiftCommand command)
        {
            switch (command)
            {
                case InformShiftCommand.Delete:
                    InfromVolunteerDeletedShift(volunteer, shift);
                    break;
                case InformShiftCommand.Accept:
                    InformVolunteerAcceptedShift(volunteer, shift);
                    break;
                case InformShiftCommand.Deny:
                    InformVolunteerDeniedShift(volunteer, shift);
                    break;
                default:
                    break;
                    
            }
        }

        private static void InformVolunteerDeniedShift(Volunteer volunteer, Shift shift)
        {

            string message = $"Dear {volunteer.Name},\n Your request for the {shift.Task} shift at {shift.StartTime} has been denied.\nAarhus Basketball Festival.";
            InformVolunteer(volunteer, message);
        }

        private static void InformVolunteerAcceptedShift(Volunteer volunteer, Shift shift)
        {

            string message = $"Dear {volunteer.Name},\n Your request for the {shift.Task} shift at {shift.StartTime} has been accepted.\n The shift ends at {shift.EndTime}.\nAarhus Basketball Festival.";
            InformVolunteer(volunteer, message);
        }

        private static void InfromVolunteerDeletedShift(Volunteer volunteer, Shift shift)
        {

            string message = $"Dear {volunteer.Name},\nThe {shift.Task} shift at {shift.StartTime} has been deleted and therefor you are not going to work at this time.\nAarhus Basketball Festival.";
            InformVolunteer(volunteer, message);
        }

        private static object findAttribute(Shift shift, string str)
        {
            object res; 
            switch (str)
            {
                case "Starttime":
                    res = shift.StartTime;
                    break;
                case "Endtime":
                    res = shift.EndTime;
                    break;
                case "Description":
                    res = shift.Description;
                    break;
                case "Task":
                    res = shift.Task;
                    break;
                default:
                    res = null;
                    break;
            }
            return res;
        }


    }
}

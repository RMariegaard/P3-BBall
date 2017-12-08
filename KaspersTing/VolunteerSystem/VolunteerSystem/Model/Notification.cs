using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VolunteerSystem
{
    public class Notification : AbstractNotification
    {
        [Key]
        public int NotificationID { get; set; }
        public string Body { get; private set; }

        public string Headder { get; private set; }
        
        public Notification(string headder, string body, NotificationImportance importance)
            : base(DateTime.Now, importance)
        {
            Body = headder;
           Headder = body;
        }
    }
}

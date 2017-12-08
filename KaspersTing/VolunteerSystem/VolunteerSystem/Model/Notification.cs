using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class Notification : AbstractNotification
    {
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

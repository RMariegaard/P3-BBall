using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class Notification : AbstractNotification
    {
        private string _body;
        public string Body
        {
            get
            {
                return _body;
            }
        }

        private string _headder;
        public string Headder
        {
            get
            {
                return _headder;
            }
        }

        private NotificationImportance _importance;
        public NotificationImportance Importance
        {
            get
            {
                return _importance;
            }
        }

        public Notification(string headder, string body, NotificationImportance importance)
            : base(DateTime.Now)
        {
            _headder = headder;
            _body = body;
            _importance = importance;
        }

    }
}

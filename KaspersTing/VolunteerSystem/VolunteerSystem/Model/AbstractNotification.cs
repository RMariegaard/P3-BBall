using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public abstract class AbstractNotification
    {
        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get
            {
                return _dateCreated;
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

        public AbstractNotification(DateTime DateCreated, NotificationImportance importance)
        {
            _importance = importance;
            _dateCreated = DateCreated;
        }
    }   
}

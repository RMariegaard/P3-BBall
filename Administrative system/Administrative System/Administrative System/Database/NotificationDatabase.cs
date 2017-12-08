using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystem.Database
{
    public class NotificationDatabase : DatabaseController<Notification>, INotificationDatabase
    {
        public NotificationDatabase(DatabaseContext context):base(context)
        {

        }
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}

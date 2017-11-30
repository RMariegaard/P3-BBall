using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystem.Database
{
    public class ScheduleController : DatabaseController<Schedule>, IScheduleController
    {
        public ScheduleController(DatabaseContext context):base(context)
        {

        }

    }
}

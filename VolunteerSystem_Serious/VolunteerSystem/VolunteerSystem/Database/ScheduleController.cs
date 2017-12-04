using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database
{
    public class ScheduleController : DatabaseController<TestSchedule>, IScheduleController
    {
        public ScheduleController(DatabaseContext context):base(context)
        {

        }

    }
}

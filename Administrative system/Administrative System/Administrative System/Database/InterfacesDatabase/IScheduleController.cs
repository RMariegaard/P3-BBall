using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database.InterfacesDatabase
{
    public interface IScheduleController : IDatabaseController<Schedule>
    {
        void UpdateSchedule(Schedule schedule);
        Schedule GetSchedule(int id);

        Schedule GetLatestSchedule();
    }

}

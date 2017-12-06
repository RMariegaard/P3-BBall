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

        public void Complete()
        {
            _context.SaveChanges();
        }
        public void UpdateSchedule(TestSchedule schedule)
        {
            _context.Entry(schedule).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

        }
        public TestSchedule GetSchedule(int id)
        {
            return _context.schedule.Include("ListOfShifts").Include("ListOfShifts.ListOfWorkers").Include("ListOfShifts.ListOfRequest").Single(x => x.ScheduleId == id);
        }
    }
}

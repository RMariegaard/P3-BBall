using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystemTests.Function
{
    class FakeScheduleDataController : IScheduleController
    {
        public void Add(Schedule entity)
        {
            
        }

        public void AddRange(IEnumerable<Schedule> entities)
        {
            
        }

        public Schedule Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Schedule> GetAll()
        {
            throw new NotImplementedException();
        }

        public Schedule GetLatestSchedule()
        {
            throw new NotImplementedException();
        }

        public Schedule GetSchedule(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Schedule entity)
        {
            
        }

        public void RemoveRange(IEnumerable<Schedule> entities)
        {
           
        }

        public void UpdateSchedule(Schedule schedule)
        {
            
        }
    }
}

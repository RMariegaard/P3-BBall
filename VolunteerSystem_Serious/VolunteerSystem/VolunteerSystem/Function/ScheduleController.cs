using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class ScheduleController
    {
        private Schedule _schedule;
        
        public void CreateSchedule()
        {
            throw new NotImplementedException();
        }

        public List<Shift> GetFullSchedule()
        {
            return _schedule.Shifts;
        }

        public List<String> GetAllTasks()
        {
            return _schedule.Tasks;
        }

        public void CreateShift()
        {
            throw new NotImplementedException();
        }

        public void CreateTask()
        {
            throw new NotImplementedException();
        }

        public void EditShift()
        {
            throw new NotImplementedException();
        }

        public void AddWorkerToShift()
        {
            throw new NotImplementedException();
        }

        public void RemoveWorkerFromShift()
        {
            throw new NotImplementedException();
        }

        public void ViewShiftInformation()
        {
            throw new NotImplementedException();
        }

        public List<Shift> ViewMyShifts(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }
        
        public void SendRequest()
        {
            throw new NotImplementedException();
        }
        public void ApproveRequest()
        {
            throw new NotImplementedException();
        }
        public void DenyRequest()
        {
            throw new NotImplementedException();
        }
    }
}

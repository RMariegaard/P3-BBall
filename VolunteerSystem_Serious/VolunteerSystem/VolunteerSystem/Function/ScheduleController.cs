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

        public ScheduleController(Schedule schedule)
        {
            _schedule = schedule;
            justForTesting();
        }
        
        public void CreateSchedule()
        {
            throw new NotImplementedException();
        }

        public List<Request> GetAllRequests()
        {
            List<Request> requests = new List<Request>();
            _schedule.Shifts.ForEach(x => requests.AddRange(x.Requests));
            return requests;
        }
        
        public List<Shift> GetAllShifts()
        {
            return _schedule.Shifts;
        }

        public List<String> GetAllTasks()
        {
            return _schedule.Tasks;
        }

        public void CreateShift(Shift shift)
        {
            _schedule.Shifts.Add(shift);
        }

        public void CreateTask(string task)
        {
            _schedule.Tasks.Add(task);
        }

        public void EditShift(int oldShiftID, Shift newShift)
        {
            Shift oldShift = FindSingleShift(x => x.ID == oldShiftID);
            string[] changes = FindShiftChanges(oldShift, newShift);
            foreach (var volunteer in oldShift.Workers.Where(x => x.GetType() == typeof(Volunteer))){
               // Notifier.InformVolunteer(volunteer, oldShift, newShift, changes);
            }
            oldShift.EditShift(newShift);

        }

        private string[] FindShiftChanges(Shift oldShift, Shift newShift)
        {
            List<string> changes = new List<string>();

            if(oldShift.StartTime != newShift.StartTime)
            {
                changes.Add("Starttime");
            }
            if(oldShift.EndTime != newShift.EndTime)
            {
                changes.Add("Endtime");
            }
            if(oldShift.Description != newShift.Description)
            {
                changes.Add("Description");
            }
            if(oldShift.GetNumberOfVolunteers != newShift.GetNumberOfVolunteers)
            {
                changes.Add("Number of volunteers");
            }
            if(oldShift.Task != newShift.Task)
            {
                changes.Add("Task");
            }


            return changes.ToArray();
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
        public void ApproveRequest(Request request)
        {
            GetAllShifts().Find(x => x.Requests.Contains(request)).ApproveRequest(request);
        }
        public void DenyRequest(Request request)
        {
            GetAllShifts().Find(x => x.Requests.Contains(request)).DenieRequest(request);
        }

        public Shift FindSingleShift(Predicate<Shift> predicate)
        {
            return _schedule.Shifts.Find(predicate);
        }

        private void justForTesting()
        {
            
            
            _schedule.Tasks.Add("Kitchen");
            _schedule.Tasks.Add("Accomadation");
            _schedule.Tasks.Add("Sleeping");
            _schedule.Tasks.Add("Security");

            _schedule.Shifts.Add(new Shift(new DateTime(2017, 6, 26, 8, 30, 0), new DateTime(2017, 6, 26, 12, 30, 0), _schedule.Tasks[0], 5, "Nope"));
            _schedule.Shifts.Add(new Shift(new DateTime(2017, 6, 27, 8, 0, 0), new DateTime(2017, 6, 27, 10, 30, 0), _schedule.Tasks[0], 5, "Nope"));
            _schedule.Shifts.Add(new Shift(new DateTime(2017, 6, 27, 12, 30, 0), new DateTime(2017, 6, 27, 18, 30, 0), _schedule.Tasks[1], 5, "Nope"));
            _schedule.Shifts.Add(new Shift(new DateTime(2017, 6, 27, 2, 0, 0), new DateTime(2017, 6, 27, 5, 30, 0), _schedule.Tasks[1], 5, "Nope"));
            _schedule.Shifts.Add(new Shift(new DateTime(2017, 6, 27, 18, 0, 0), new DateTime(2017, 6, 27, 22, 30, 0), _schedule.Tasks[3], 5, "Nope"));
            _schedule.Shifts.Add(new Shift(new DateTime(2017, 6, 27, 9, 20, 0), new DateTime(2017, 6, 27, 14, 30, 0), _schedule.Tasks[3], 5, "Nope"));
            _schedule.Shifts.Add(new Shift(new DateTime(2017, 6, 27, 17, 10, 0), new DateTime(2017, 6, 27, 23, 0, 0), _schedule.Tasks[2], 5, "Nope"));

            _schedule.Shifts[0].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[1].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[2].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[3].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[4].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[5].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[6].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[0].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[1].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[2].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[3].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[4].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[5].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));
            _schedule.Shifts[6].CreateRequest(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"));

        }
    }
}

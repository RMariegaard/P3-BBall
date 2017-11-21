﻿using System;
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
        }
        
        public void CreateSchedule()
        {
            throw new NotImplementedException();
        }

        public List<Request> GetAllRequests()
        {
            List<Request> requests = new List<Request>();
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));
            requests.Add(new Request(new Volunteer("Casper", "casnie16@student.aau.dk", "U12 drenge"), new Shift(new DateTime(2017, 6, 26), new DateTime(2017, 6, 27), "Kitchen", 5, "Nope")));

            return requests;
            //return _schedule.Requests;
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

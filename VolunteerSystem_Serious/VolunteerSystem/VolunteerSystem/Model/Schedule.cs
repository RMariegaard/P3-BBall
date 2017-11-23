﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class Schedule
    {
        private int _id;
        private int _year;
        private List<Shift> _shifts;
        public List<Shift> Shifts
        {
            get
            {
                return _shifts;
            }
        }
        private List<String> _tasks;
        public List<String> Tasks
        {
            get
            {
                return _tasks;
            }
        }
        
        public List<Request> Requests
        {
            get
            {
                List<Request> requests = new List<Request>();
                foreach(Shift shift in _shifts)
                {
                    foreach(Request request in shift.Requests)
                    {
                        requests.Add(request);
                    }
                }
                return requests;
            }
        }

        public void CreateNewShift(DateTime startTime, DateTime endTime, string task, int volunteersNeeded)
        {
            _shifts.Add(new Shift(startTime, endTime, task, volunteersNeeded, ""));
        }

        public Schedule(int year)
        {
            //Assign ID??
            _shifts = new List<Shift>();
            _tasks = new List<string>();
            _id = year;
            _year = year;
            _shifts = new List<Shift>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VolunteerSystem
{
    public class Schedule
    {
        [Key]
        public int id { get; set; }
        private int _year;
        public int Year { get { return _year; } }
        public List<DateTime> Days;
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
        
        public void CreateNewShift(DateTime startTime, DateTime endTime, string task, int volunteersNeeded)
        {
            _shifts.Add(new Shift(startTime, endTime, task, volunteersNeeded, ""));
        }

        public Schedule(int year)
        {
            //Assign ID??
            _shifts = new List<Shift>();
            _tasks = new List<string>();
            _year = year;
            _shifts = new List<Shift>();
            Days = new List<DateTime>();
        }

        //Test Constructer only used for unit test does not connect to database
        public Schedule(bool test, int year)
        {
            //Assign ID??
            _shifts = new List<Shift>();
            _tasks = new List<string>();
            _year = year;
            _shifts = new List<Shift>();
        }
    }
}

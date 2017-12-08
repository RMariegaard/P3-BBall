using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerSystem
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        public List<Shift> ListOfShifts { get; set; }

        public int Year {get;set;}


        private List<String> _tasks;
        [NotMapped]
        public List<String> Tasks
        {
            get
            {
                return _tasks;
            }
        }

        public void CreateNewShift(DateTime startTime, DateTime endTime, string task, int volunteersNeeded)
        {
            ListOfShifts.Add(new Shift(startTime, endTime, task, volunteersNeeded, ""));
        }

        public Schedule(int year)
        {
            //Assign ID??
            ListOfShifts = new List<Shift>();
            _tasks = new List<string>();
            Year = year;
        }
        public Schedule()
        {
            _tasks = new List<string>();
            ListOfShifts = new List<Shift>();
        }


        ////Test Constructer only used for unit test does not connect to database
        //public Schedule(bool test, int year)
        //{
        //    //Assign ID??
        //    _ListOfShifts = new List<Shift>();
        //    _tasks = new List<string>();
        //    _year = year;
        //    _ListOfShifts = new List<Shift>();
        //}

    }
}

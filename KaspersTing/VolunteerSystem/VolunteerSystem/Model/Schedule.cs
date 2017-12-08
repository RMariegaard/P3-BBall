﻿using System;
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



        private List<string> _tasks;
        public List<string> Tasks { get; set; }
        public string TasksString
        {
            get { return string.Join(",", Tasks); }
            set
            {
                if (value.Split(',').ToList().First() == "")
                {
                    List<string> temp = value.Split(',').ToList();
                    temp.Remove(temp.First());
                    Tasks = temp;
                }
                else
                    Tasks = value.Split(',').ToList();
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
            Tasks = new List<string>();
            Year = year;
        }
        public Schedule()
        {
            Tasks = new List<string>();
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

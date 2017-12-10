﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerSystem.Model;

namespace VolunteerSystem
{
    public class Volunteer : Worker
    {
  
        //Constructors 
        public Volunteer(string name, string email, string accosi) : base(name, email)
        {
            this.DateCreated = DateTime.Now;
            this.Association = accosi;

            YearsWorked = new List<int>();
        }
        public Volunteer(string name, string email, string accosi, int phonenumber, string password) : base(name, email)
        {
            this.DateCreated = DateTime.Now;
            this.Association = accosi;
            this.Phonenumber = phonenumber;
            this.HashPassworkd = password;
            YearsWorked = new List<int>();
        }

        public void Update(string name, string email, string accosi, int phonenumber, string password){
            base.Update(name, email);
            this.Association = accosi;
            this.Phonenumber = phonenumber;
            this.HashPassworkd = password;
        }

        public Volunteer()
        {
            YearsWorked = new List<int>();
        }

        public List<Request> ListOfRequests { get; set; }
        public string HashPassworkd { get; set; }

        public string Association { get; private set; }
        public DateTime DateCreated { get; private set; }
        public int? Phonenumber { get; set; }
 

        public List<int> YearsWorked { get; set; }
        public string YearsWorkedString
        {
            get { return string.Join(",", YearsWorked); }
            set
            {
                if (value != "")
                {
                    if (value.Contains(',')){
                        YearsWorked = value.Split(',').Select(x => int.Parse(x)).ToList();
                    }
                    else
                    {
                        YearsWorked.Add(int.Parse(value));
                    }                   
                 
                }
            }
        }
        
        


        public override string ToString()
        {
            return Association + " " + Name + " " + Email;
        }






        public void AddYearWorked(int year)
        {
            if (!YearsWorked.Contains(year))
            {
                YearsWorked.Add(year);
                //update database på en måde
            }
        }


        public Volunteer(string name, string email, string Association, int Phonenumber) : base(name, email)
        {
            this.DateCreated = DateTime.Now;
            this.Association = Association;
            this.YearsWorked = new List<int>();
            this.Phonenumber = Phonenumber;

        }
        //public Volunteer(int id, string name, string email, string Association, DateTime dateCreated, List<int> yearsworked) : base(name, email)
        //{
        //    this.ID = id;
        //    this._dateCreated = dateCreated;
        //    this._Association = Association;
        //    this._yearsWorked = yearsworked;
        //}

        //Test Consctructor that does not connect to the database
        //public Volunteer(bool test, string name, string email, string Association) : base(name, email)
        //{
        //    this._dateCreated = DateTime.Now;
        //    this._Association = Association;
        //    this._yearsWorked = new List<int>();

        //}



        public bool IsValidForSeasonTickets()
        {
            //problem if the program is used before newyears....or is it ??
            //When would you search for this ? right after the tournament it is correct..
            //When should the validation reset ? when a new schedule is created ??
            int thisYear = DateTime.Now.Year;
            int lastYear = thisYear - 1;
            if (YearsWorked.Count >= 2)
                return thisYear == YearsWorked.Last() && lastYear == YearsWorked[YearsWorked.Count - 2];
            else
                return false;
        }
        //i would do it this way maybe
        public bool IsValidForSeasonTickets(int scheduleYear)
        {
            int lastYear = scheduleYear - 1;
            if (YearsWorked.Count >= 2)
                return scheduleYear == YearsWorked.Last() && lastYear == YearsWorked[YearsWorked.Count - 2];
            else
                return false;
        }




        public void TempAddYearWorked(int year)
        {
            YearsWorked.Add(year);
        }


    }
}

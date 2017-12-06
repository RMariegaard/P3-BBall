using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerSystem.Model
{
    public class TestVolunteer : TestWorker
    {
        //Database Connection
        public VolunteerController volunteerDatabase = new VolunteerController(new DatabaseContext(SqlDataConnecter.CnnVal("DatabaseCS")));

        //Constructors 
        public TestVolunteer(string name, string email, string accosi):base(name, email)
        {
            this.Association = accosi;
           volunteerDatabase.Add(this);
           volunteerDatabase.Complete();
            
        }
        public TestVolunteer()
        {
        }

        public List<TestRequest> ListOfRequest { get; set; }

        public string Association { get; private set; }
        public DateTime? DateCreated { get; private set; }
        public int? Phonenumber { get; set; }
        [NotMapped]
        public List<int> YearsWorked { get; set; }

        
                               
        public override string ToString()
        {
            return Association + " " + Name + " " + Email;
        }






        public void AddYearWorked(int year)
        {
            //if (!YearsWorked.Contains(year))
            {
               // YearsWorked.Add(year);
                //update database på en måde
            }
        }


        public TestVolunteer(string name, string email, string assosiation, int phonenumber) : base(name, email)
        {
            this.DateCreated = DateTime.Now;
            this.Association = assosiation;
            this.YearsWorked = new List<int>();
            this.Phonenumber = phonenumber;

            volunteerDatabase.Add(this);
            volunteerDatabase.Complete();


        }
        //public Volunteer(int id, string name, string email, string assosiation, DateTime dateCreated, List<int> yearsworked) : base(name, email)
        //{
        //    this.ID = id;
        //    this._dateCreated = dateCreated;
        //    this._assosiation = assosiation;
        //    this._yearsWorked = yearsworked;
        //}

        //Test Consctructor that does not connect to the database
        //public Volunteer(bool test, string name, string email, string assosiation) : base(name, email)
        //{
        //    this._dateCreated = DateTime.Now;
        //    this._assosiation = assosiation;
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

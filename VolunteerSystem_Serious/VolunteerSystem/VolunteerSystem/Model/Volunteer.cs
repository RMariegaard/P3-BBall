using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class Volunteer:Worker
    {
        private string _assosiation;
        public string Assosiation
        {
            get
            {
                return _assosiation;
            }
        }

        private int _phoneNumber;
        public int PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
            }
        }

        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get
            {
                return _dateCreated;
            }
        }

        private List<int> _yearsWorked;
        public List<int> YearsWorked
        {
            get
            {
                return _yearsWorked;
            }
        }
        public void AddYearWorked(int year)
        {
            if (!_yearsWorked.Contains(year))
            {
                _yearsWorked.Add(year);
            }
        }


        public Volunteer(string name, string email, string assosiation) : base(name, email)
        {
            this._dateCreated = DateTime.Now;
            this._assosiation = assosiation;
            this._yearsWorked = new List<int>();
        }
        public Volunteer(int id, string name, string email, string assosiation, DateTime dateCreated, List<int> yearsworked) : base(name, email)
        {
            this.ID = id;
            this._dateCreated = dateCreated;
            this._assosiation = assosiation;
            this._yearsWorked = yearsworked;
        }



        public bool IsValidForSeasonTickets()
        {
            //problem if the program is used before newyears.... or is it??
            //When would you search for this? right after the tournament it is correct..
            //When should the validation reset? when a new schedule is created??
            int thisYear = DateTime.Now.Year;
            int lastYear = thisYear - 1;
            if (YearsWorked.Count >= 2)
                return thisYear == YearsWorked.Last() && lastYear == YearsWorked[YearsWorked.Count - 2];
            else
                return false;
        }
        // i would do it this way maybe
        public bool IsValidForSeasonTickets(int scheduleYear)
        {
            int lastYear = scheduleYear - 1;
            if (YearsWorked.Count >= 2)
                return scheduleYear == YearsWorked.Last() && lastYear == YearsWorked[YearsWorked.Count - 2];
            else
                return false;
        }

        public override string ToString()
        {
            return Assosiation + " " + Name + " " + Email;
        }


        public void TempAddYearWorked(int year)
        {
            _yearsWorked.Add(year);
        }


    }
}

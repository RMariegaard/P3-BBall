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

        public Volunteer(string name, string email, string assosiation) : base(name, email)
        {
            this._dateCreated = DateTime.Now;
            this._assosiation = assosiation;
            this._yearsWorked = new List<int>();
        }
 

        public bool IsValidForSeasonTickets()
        {
            int thisYear = DateTime.Now.Year;
            int lastYear = thisYear - 1;

            return thisYear == YearsWorked.Last() && lastYear == YearsWorked[YearsWorked.Count - 1];
            
        }

        public override string ToString()
        {
            return Assosiation + " " + Name + " " + Email;
        }

    }
}

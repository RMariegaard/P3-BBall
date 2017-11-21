﻿using System;
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
            this._assosiation = assosiation;
            this._yearsWorked = new List<int>();
        }

        public bool IsValidForSeasonTickets() => throw new NotImplementedException();
        
        
    }
}

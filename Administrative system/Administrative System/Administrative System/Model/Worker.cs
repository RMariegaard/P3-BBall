using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerSystem.Exceptions;

namespace VolunteerSystem
{
    public abstract class Worker
    {
        public Worker(string name, string email)
        {
            this.Email = email;
            this.Name = name;
            ListOfShifts = new List<Shift>();
        }
        public void Update(string name, string email)
        {
            this.Email = email;
            this.Name = name;

        }

        public Worker()
        {
            ListOfShifts = new List<Shift>();
        }
        [Key]
        public int WorkerId { get; set; }
        public string Name { get; private set; }

        public List<Shift> ListOfShifts {get;set;}

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;

            }
        }



        public string GetInformation()
        {
            return Name + "\t" + Email;
        }

        public override string ToString()
        {
            return Name + " " + Email;
        }

    }
}

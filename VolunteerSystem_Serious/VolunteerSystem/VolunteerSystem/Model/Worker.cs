using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VolunteerSystem
{
    public abstract class Worker
    {
        protected string _name;
        protected string _email;

        public string Email { get { return _email; } }
        public string Name { get { return _name; } }

        
        public Worker(string name, string email)
        {
            this._name = name;
            this._email = email;
        }

        [Key]
        public int ID { get; set; }


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

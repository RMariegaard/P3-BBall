using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public abstract class Worker
    {
        protected int _id;
        protected string _name;
        protected string _email;

        public string Email { get { return _email; } }
        public string Name { get { return _name; } }

        
        public Worker(string name, string email)
        {
            this._name = name;
            this._email = email;
        }

        public string GetInformation()
        {
            return Name + "\t" + Email;
        }
    }
}

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

        public Worker(string name, string email)
        {
            this._name = name;
            this._email = email;
        }

        public string GetInformation() {

            throw new NotImplementedException();
        }
    }
}

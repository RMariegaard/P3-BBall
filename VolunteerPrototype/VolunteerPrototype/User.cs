using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace VolunteerPrototype
{
    public class User
    {
        private int _volunteerId;
        private string _name;
        public string Name { get { return _name; } }
        public int VolunteerId { get { return _volunteerId; } }
        public User(string email, string hashedPassword)
        {
            _name = email;
           // GetVolunteerID(email, hashedPassword);
        }

    }
}

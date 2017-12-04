using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class ExternalWorker : Worker
    {
        public ExternalWorker(string name, string email) : base(name, email)
        {

        }

        //Test Constructer only used for unit test does not connect to database
        public ExternalWorker(bool test, string name, string email) : base(name, email)
        {

        }
    }
}

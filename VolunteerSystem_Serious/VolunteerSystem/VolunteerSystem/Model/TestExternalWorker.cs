using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem.Model
{
    public class TestExternalWorker : TestWorker
    {
        public TestExternalWorker(string name, string email) :base(name,email)
        {

        }
        public TestExternalWorker()
        {

        }
    }
}

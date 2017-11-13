using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretP3
{
    public class Request
    {
        public Volunteer Volunteer;

        public Request(Volunteer volunteer)
        {
            this.Volunteer = volunteer;
        }

    }
}

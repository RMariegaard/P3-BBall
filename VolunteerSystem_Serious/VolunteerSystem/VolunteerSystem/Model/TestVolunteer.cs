﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem;

namespace VolunteerSystem.Model
{
    public class TestVolunteer : TestWorker
    { 
       public TestVolunteer(string name, string email, string accosi):base(name, email)
        {
            this.accosiation = accosi;
        }
       public string accosiation { get; private set; }
       public DateTime? dateCreated { get; private set; }
       public int? volunterId { get; set; }
    

    }
}

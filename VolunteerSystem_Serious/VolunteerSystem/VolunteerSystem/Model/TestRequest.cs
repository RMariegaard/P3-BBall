using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerSystem.Model
{
    public class TestRequest
    {
        public int id { get; set; }

        [ForeignKey("TestVolunteer")]
        public int? volunterId { get; set; }
        public TestVolunteer TestVolunteer { get; set; }

    }
}

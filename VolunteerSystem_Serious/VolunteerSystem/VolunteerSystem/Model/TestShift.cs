using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerSystem.Model
{
    public class TestShift
    {
        public TestShift(string task, string descrip)
        {
            this.task = task;
            this.descripstion = descrip;
        }

        [ForeignKey("TestVolunteer")]
        public int? volunterId { get; set; }
        public TestVolunteer TestVolunteer { get; set; }

        public List<TestVolunteer> listOfVolunteers { get; set; }

        [Key]
        public int id { get; private set; }
        public string task { get; private set; }
        public string descripstion { get; private set; }
        public DateTime? startTime { get; private set; }
        public DateTime? endTime { get; private set; }
    }
}

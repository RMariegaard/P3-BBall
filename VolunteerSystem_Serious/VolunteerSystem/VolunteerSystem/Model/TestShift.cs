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
            listOfVolunteers = new List<TestVolunteer>();
        }
        public TestShift()
        {
            listOfVolunteers = new List<TestVolunteer>();
        }


        public IList<TestVolunteer> listOfVolunteers { get; set; }

        [Key]
        public int shiftId { get; private set; }
        public string task { get; private set; }
        public string descripstion { get; private set; }
        public DateTime? startTime { get; private set; }
        public DateTime? endTime { get; private set; }
    }
}

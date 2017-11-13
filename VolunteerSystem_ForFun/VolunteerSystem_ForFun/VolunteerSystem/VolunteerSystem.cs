using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem_ForFun;

namespace SecretP3
{
    public class VolunteerSystem : IVolunteerSystem
    {
        List<Shift> AllShifts = new List<Shift>();
        List<String> Tasks = new List<string>();

        public VolunteerSystem()
        {
            Shift test = new Shift(new DateTime(2017, 5, 15, 3, 0, 0), 7, "Kitchen");
            test.Description = "Dette er ikke bare for sjov til en beskivelse af en shift";
            test.LengthInHours = 4;
            AllShifts.Add(test);

            Tasks.Add("Kitchen");
            Tasks.Add("Accomadation");
            Tasks.Add("Security");
        }

        public List<Shift> GetShifts()
        {
            return AllShifts;
        }

        public List<string> GetTasks()
        {
            return Tasks;
        }
    }
}

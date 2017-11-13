using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretP3
{
    public class Schedule
    {
        public List<Shift> Shifts { get; private set; }

        public int Year { get; private set; }

        public Schedule()
        {
            Shifts = new List<Shift>();
            Year = DateTime.Now.Year;
        }

       public void CreateShift(DateTime time, int volunteersNeeded, string task)
        {
            Shifts.Add(new Shift(time, volunteersNeeded, task));
        }



    }
}

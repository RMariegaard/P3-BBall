using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class Schedule
    {
        private int _id;
        private int _year;
        private List<Shift> _shifts;

        public void CreateNewShift(DateTime time, string task, int volunteersNeeded)
        {
            _shifts.Add(new Shift(time, task, volunteersNeeded));
        }

        public Schedule(int year)
        {
            //Assign ID??
            throw new NotImplementedException();
            _shifts = new List<Shift>();
        }


    }
}

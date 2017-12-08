using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class WorkerShiftPair
    {
        Worker _worker;
        public Worker Worker
        {
            get
            {
                return _worker;
            }
        }

        Shift _shift;
        public Shift Shift
        {
            get
            {
                return _shift;
            }
        }

        public WorkerShiftPair(Worker worker, Shift shift)
        {
            _shift = shift;
            _worker = worker;
        }
    }
}

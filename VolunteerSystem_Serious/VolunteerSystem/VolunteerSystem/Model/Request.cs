using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class Request
    {
        private int _id;
        private DateTime _timeSent;
        public DateTime TimeSent
        {
            get
            {
                return _timeSent;
            }
        }

        private Shift _shift;
        public Shift Shift
        {
            get
            {
                return _shift;
            }
        }

        private Worker _worker;
        public Worker Worker
        {
            get
            {
                return _worker;
            }
        }

        public Request(Worker worker, Shift shift)
        {
            _timeSent = DateTime.Now;
            _worker = worker;
            _shift = shift;
        }

        public void ApproveRequest()
        {
            Shift.Workers.Add(_worker);
            Shift.RemoveRequest(this);
        }
        public void DenieRequest()
        {
            Shift.RemoveRequest(this);
        }
    }
}

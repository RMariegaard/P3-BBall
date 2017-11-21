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

        private Volunteer _volunteer;
        public Volunteer Volunteer
        {
            get
            {
                return _volunteer;
            }
        }

        public Request(Volunteer volunteer, Shift shift)
        {
            _volunteer = volunteer;
            _shift = shift;
        }

        public void ApproveRequest()
        {
            throw new NotImplementedException();
        }
        public void DenieRequest()
        {
            throw new NotImplementedException();
        }
    }
}

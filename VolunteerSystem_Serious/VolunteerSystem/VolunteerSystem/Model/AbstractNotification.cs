using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public abstract class AbstractNotification
    {
        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get
            {
                return _dateCreated;
            }
        }

        public AbstractNotification(DateTime DateCreated)
        {
            _dateCreated = DateCreated;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VolunteerSystem
{
    public class Request : AbstractNotification
    {
        [Key]
        public int id { get; set; }

        private DateTime _timeSent;
        public DateTime TimeSent
        {
            get
            {
                return _timeSent;
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

        public Request(Worker worker)
            : base(DateTime.Now, NotificationImportance.MediumImportance)
        {
            _timeSent = DateTime.Now;
            _worker = worker;
        }

        //Test Constructer only used for unit test does not connect to database
        public Request(bool test, Worker worker)
            : base(DateTime.Now, NotificationImportance.MediumImportance)
        {
            _timeSent = DateTime.Now;
            _worker = worker;
        }
    }
}

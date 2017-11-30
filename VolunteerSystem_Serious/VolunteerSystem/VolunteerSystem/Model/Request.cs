using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VolunteerSystem
{
    public class Request
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
        {
            _timeSent = DateTime.Now;
            _worker = worker;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerSystem.Database;
using VolunteerSystem;
using VolunteerSystem.Model;

namespace VolunteerSystem
{
    public class Request : AbstractNotification
    {
        public RequestController RequestDatabase = new RequestController(new DatabaseContext(SqlDataConnecter.CnnVal("DatabaseCS")));

        [Key]
        public int RequestId { get; set; }
        public DateTime TimeSent { get; set; }

        [ForeignKey("Shift")]
        public int? ShiftId { get; set; }
        public Shift Shift { get; set; }

        [ForeignKey("Volunteer")]
        public int? WorkerId { get; set; }
        public Volunteer Volunteer { get; set; }



        public Request(Volunteer volunteer, NotificationImportance hey)
            : base(DateTime.Now, hey)
        {
            TimeSent = DateTime.Now;
            Volunteer = volunteer;
        }

        public Request(Volunteer volunteer)
            : base(DateTime.Now)
        {
            TimeSent = DateTime.Now;
            Volunteer = volunteer;
        }


        public Request()
        {

        }

        ////Test Constructer only used for unit test does not connect to database
        //public Request(bool test, Worker worker)
        //    : base(DateTime.Now)
        //{
        //    _timeSent = DateTime.Now;
        //    _worker = worker;
        //}


    }
}

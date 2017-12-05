using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerSystem.Database;
using VolunteerSystem;

namespace VolunteerSystem.Model
{
    public class TestRequest : AbstractNotification
    {
        public RequestController RequestDatabase = new RequestController(new DatabaseContext(SqlDataConnecter.CnnVal("DatabaseCS")));

        [Key]
        public int RequestId { get; set; }
        public DateTime? TimeSent { get; set; }
        [ForeignKey("TestShift")]
        public int ShiftId { get; set; }
        public TestShift TestShift { get; set; }

        [ForeignKey("TestVolunteer")]
        public int WorkerId { get; set; }
        public TestVolunteer TestVolunteer { get; set; }



        public TestRequest(TestVolunteer volunteer, NotificationImportance hey)
            : base(DateTime.Now, hey)
        {
            TimeSent = DateTime.Now;
            TestVolunteer = volunteer;
        }

        public TestRequest(TestVolunteer volunteer)
            : base(DateTime.Now)
        {
            TimeSent = DateTime.Now;
            TestVolunteer = volunteer;
        }


        public TestRequest()
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

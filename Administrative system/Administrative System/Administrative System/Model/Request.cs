﻿using System;
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

        [Key]
        public int RequestId { get; set; }
        public DateTime TimeSent { get; set; }

        [ForeignKey("Shift")]
        public int? ShiftId { get; set; }
        public Shift Shift { get; set; }

        [ForeignKey("Volunteer")]
        public int? WorkerId { get; set; }
        public Volunteer Volunteer { get; set; }

        
        public Request(Volunteer volunteer)
            : base(DateTime.Now, NotificationImportance.MediumImportance)
        {
            TimeSent = DateTime.Now;
            Volunteer = volunteer;
        }
        
        public Request():base(DateTime.Now, NotificationImportance.MediumImportance)
        {

        }

        public override string ToString()
        {
            return $"{TimeSent.ToString("dd/MM/yyyy HH:mm")} - {Volunteer.Name} - {Volunteer.Email}";
        }

    }
}

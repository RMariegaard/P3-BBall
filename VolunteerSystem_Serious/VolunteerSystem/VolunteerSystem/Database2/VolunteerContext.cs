using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem.Database2
{
    class VolunteerContext : DbContext
    {
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VolunteerSystem.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<VolunteerSystem.ExternalWorker> externalWorker {get;set;}
        public DbSet<VolunteerSystem.Volunteer> volunteer { get; set; }
        public DbSet<VolunteerSystem.Schedule> schedule { get; set; }
        public DbSet<VolunteerSystem.Request> request { get; set; }
        public DbSet<VolunteerSystem.Shift> shift { get; set; }

        public DatabaseContext(string connectionString)
        {
            this.Database.Connection.ConnectionString = connectionString;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TestExternalWorker> externalWorker {get;set;}
        public DbSet<TestVolunteer> volunteer { get; set; }
        public DbSet<TestSchedule> schedule { get; set; }
        public DbSet<TestRequest> request { get; set; }
        public DbSet<TestShift> shift { get; set; }

        public DatabaseContext(string connectionString)
        {
            this.Database.Connection.ConnectionString = connectionString;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            modelBuilder.Entity<TestShift>().HasMany(p => p.listOfVolunteers).WithMany();
        }
    }
}

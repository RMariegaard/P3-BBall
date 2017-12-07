using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using VolunteerSystem.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteerSystem;

namespace VolunteerSystem.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ExternalWorker> externalWorker {get;set;}
        public DbSet<Volunteer> volunteer { get; set; }
        public DbSet<Schedule> schedule { get; set; }
        public DbSet<Request> request { get; set; }
        public DbSet<Shift> shift { get; set; }

        public DatabaseContext(string connectionString)
        {
            this.Database.Connection.ConnectionString = connectionString;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            modelBuilder.Entity<Shift>().HasMany(p => p.ListOfWorkers).WithMany(p => p.ListOfShifts).
                Map(m =>
                {
                    m.ToTable("ShiftAndVolunteers");
                    m.MapRightKey("WorkerId");
                    m.MapLeftKey("ShiftId");

                }
                );
            //modelBuilder.Entity<TestShift>().HasMany(p => p.ListOfRequests).WithOptional(p => p.TestShift);
                
            
        }
    }
}

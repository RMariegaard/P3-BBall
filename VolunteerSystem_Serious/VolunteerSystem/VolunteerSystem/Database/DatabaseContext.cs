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
            modelBuilder.Entity<TestShift>().HasMany(p => p.ListOfWorkers).WithMany(p => p.ListofShifts).
                Map(m =>
                {
                    m.ToTable("ShiftAndVolunteers");
                    m.MapRightKey("WorkerId");
                    m.MapLeftKey("ShiftId");

                }
                );

                
            
        }
    }
}

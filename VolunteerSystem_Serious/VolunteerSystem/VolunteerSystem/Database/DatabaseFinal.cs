using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.Interfaces;
using VolunteerSystem.Model;
using System.Data.Entity;
using System.Linq.Expressions;
using VolunteerSystem.Database;


namespace VolunteerSystem.Database
{
    class DatabaseFinal : IDatabase
    {
        private readonly DBConnection _connection;

        public DatabaseFinal(DBConnection context)
        {
            
        }
        public IDataAccesss DataAccess { get; private set; }

        public int Complete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

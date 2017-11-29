using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.Linq;

namespace VolunteerSystem.Model
{
    public class DataAccess 
    {
        public List<Shift> GetShifts()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(SqlDataConnecter.CnnVal("DatabaseCS")))
            {
                 
                return connection.Query<Shift>($"select * from Shifts2").ToList();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace VolunteerSystem.Database
{
    public class DBConnection
    {


        private static string _connectionString = "Server=tcp:hackermarksdisciple.database.windows.net,1433;Initial Catalog=P3-Skovbakken;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private static SqlConnection _sqlConnection = new SqlConnection(_connectionString);


        public static SqlConnection Connection { get { return _sqlConnection; } }

        private static DataContext dataContext = new DataContext(_sqlConnection);
        public static DataContext DataContext { get { return dataContext; } }

    }
}

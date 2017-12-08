using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace VolunteerSystem.Database2
{
    public static class DBConnection
    {
        public static string ConnectionString { get { return "Server=tcp:hackermarksdisciple.database.windows.net;Database=P3-Skovbakken;User ID=hackermarkadmin@hackermarksdisciple;Password=1234Hackermark;Trusted_Connection=False;Encrypt=True;"; } }


        private static SqlConnection _sqlConnection = new SqlConnection(ConnectionString);
        public static SqlConnection SqlConnection { get{return _sqlConnection;} }



    }
}

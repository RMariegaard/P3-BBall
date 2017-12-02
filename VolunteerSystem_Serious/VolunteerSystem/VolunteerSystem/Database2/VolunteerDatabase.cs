using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem.Database2
{
    public class VolunteerDatabase
    {
        public void Add(Volunteer volunteer)
        {
            var command = DBConnection.SqlConnection.CreateCommand();
            DBConnection.SqlConnection.Open();
            command.CommandText = $"INSERT INTO VolunteerTable values ('{volunteer.Name}', '{volunteer.Name}', '{volunteer.Email}', '{string.Join(",", volunteer.YearsWorked)}', '{volunteer.Assosiation}', '{volunteer.DateCreated}');";
            command.ExecuteNonQuery();
            DBConnection.SqlConnection.Close();
        }


        public Volunteer Get(int id)
        {
            Volunteer volunteer = null;
            var command = DBConnection.SqlConnection.CreateCommand();
            DBConnection.SqlConnection.Open();
            command.CommandText = $"SELECT * FROM VolunteerTable WHERE Id = '{id}';";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                
                if (reader.Read())
                {
                    string years = (string)reader["YearsWorked"];
                    var years2 = years.Split(',');
                    var years3 = new List<int>();
                    foreach (var str in years2)
                    {
                        years3.Add(int.Parse(str));
                    }

                    volunteer = new Volunteer((int)reader["Id"], (string)reader["FirstName"], (string)reader["Email"], (string)reader["Assoistation"], DateTime.Parse((string)reader["DateCreated"]), years3);
                }
            }

            DBConnection.SqlConnection.Close();
            return volunteer;

        }

        public void UpdateYearsWorked(int Year)
        {

        }
    }
}

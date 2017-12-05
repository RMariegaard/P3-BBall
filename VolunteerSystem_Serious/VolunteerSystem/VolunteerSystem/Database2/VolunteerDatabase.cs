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
                        if (str != "")
                        {
                            years3.Add(int.Parse(str));
                        }
                    }

                    volunteer = new Volunteer((int)reader["Id"], (string)reader["FirstName"], (string)reader["Email"], (string)reader["Assoistation"], DateTime.Parse((string)reader["DateCreated"]), years3);
                }
            }

            DBConnection.SqlConnection.Close();
            return volunteer;

        }

        public void Update(Volunteer volunteer)
        {
            var command = DBConnection.SqlConnection.CreateCommand();
            DBConnection.SqlConnection.Open();
            command.CommandText = $"UPDATE VolunteerTable SET FirstName = '{volunteer.Name}', LastName = '{volunteer.Name}', Email = '{volunteer.Email}', YearsWorked = '{string.Join(",", volunteer.YearsWorked)}', Assoistation = '{volunteer.Assosiation}', DateCreated = '{volunteer.DateCreated}' WHERE Id = '{volunteer.ID}';";
            command.ExecuteNonQuery();
            DBConnection.SqlConnection.Close();
        }

        public void Remove(Volunteer volunteer)
        {
            var command = DBConnection.SqlConnection.CreateCommand();
            DBConnection.SqlConnection.Open();
            command.CommandText = $"DELETE VolunteerTable WHERE Id ='{volunteer.ID}';";
            command.ExecuteNonQuery();
            DBConnection.SqlConnection.Close();
        }

        public int GetLatestID()
        {
            int res = -1;

            var command = DBConnection.SqlConnection.CreateCommand();
            DBConnection.SqlConnection.Open();
            command.CommandText = $"SELECT TOP 1 Id FROM VolunteerTable ORDER BY Id DESC;";
            using (SqlDataReader reader = command.ExecuteReader())
            {

                if (reader.Read())
                {
                    res = (int)reader["Id"];
                }
            }
            DBConnection.SqlConnection.Close();

            return res;
        }

        public List<Volunteer> GetAll()
        {
            var res = new List<Volunteer>();
            int last = GetLatestID();

            for(int i = 0; i <= last; i++)
            {
                    res.Add(Get(i));
                
            }

            return res;

        }
    }
}

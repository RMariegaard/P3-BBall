using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretP3.Database
{
   public  class Database
    {
        private DBConnection _db;


        public Database()
        {
            _db = DBConnection.Instance();
            _db.DatabaseName = "test";
        }

        public bool IsConnected() => _db.IsConnect();
        
        public void InsertVolunteer(Volunteer volunteer)
        {
            var contactinfo = volunteer.ContactInformation;
            if (IsConnected())
            {
                string q = $"INSERT INTO `users`(`FirstName`, `LastName`, `Age`, `Gender`, `Email`, `Phone`, `Team`, `Password`) VALUES " +
                    $"('{contactinfo.fname}', '{contactinfo.lname}','{contactinfo.age}', '{contactinfo.gender}','{contactinfo.email}','{contactinfo.phone}','{contactinfo.team}','jek')";
                var cmd = new MySql.Data.MySqlClient.MySqlCommand(q, _db.Connection);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Volunteer> GetAllVolunteers()
        {
            List<Volunteer> list = new List<Volunteer>();

            if (IsConnected())
            {

                string q = "SELECT `ID`, `FirstName`, `LastName`, `Age`, `Gender`, `Email`, `Phone`, `Team`, `Password` FROM `users` WHERE 1";
                var cmd = new MySql.Data.MySqlClient.MySqlCommand(q, _db.Connection);
                var reader = cmd.ExecuteReader();
                // Rows starting from 1: `FirstName`, `LastName`, `Age` (value), `Gender`, `Email`, `Phone` (value), `Team`, `Password`
                //row 0 is ID
                while (reader.Read())
                {
                    string fname = reader.GetString(1);
                    string lname = reader.GetString(2);
                    int age = reader.GetInt32(3);
                    string gender = reader.GetString(4);
                    string email = reader.GetString(5);
                    int phone = reader.GetInt32(6);
                    string team = reader.GetString(7);
                    string Password = reader.GetString(8);
                    list.Add(new Volunteer(new ContactInfo(fname, gender, age, email, phone, team)));
                }
            }

            return list;
        }


    }
}

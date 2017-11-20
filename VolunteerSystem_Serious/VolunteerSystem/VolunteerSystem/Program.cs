using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Database.DBConnection.Connection.Database);
            Console.Read();
        }
    }
}

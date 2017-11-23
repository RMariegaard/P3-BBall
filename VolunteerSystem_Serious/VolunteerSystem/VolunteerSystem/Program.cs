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
            /*
            Console.WriteLine(Database.DBConnection.Connection.Database);
            Notifier.InformVolunteer();
            Console.Read();
            */

            Schedule schedule = new Schedule(2018);
            ScheduleController scheduleController = new ScheduleController(schedule);
            WorkerController workerController = new WorkerController();
            UserInterface.TheMainWindow Ui = new UserInterface.TheMainWindow(scheduleController, workerController);
            
            Ui.Start();
        }
    }
}

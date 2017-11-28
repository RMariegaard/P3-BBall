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

            workerController.Workers.Add(new Volunteer("meget langt navn der ikke giver mening ", "AnEmail@gmail.com", "U12 Drenge"));
            workerController.Workers.Add(new Volunteer("Kasper", "AnEmail@gmail.com", "U12 Drenge"));
            workerController.Workers.Add(new Volunteer("Casper", "AnEmail@gmail.com", "U12 Drenge"));
            workerController.Workers.Add(new Volunteer("Mark", "AnEmail@gmail.com", "U12 Drenge"));
            workerController.Workers.Add(new Volunteer("Mustafa", "AnEmail@gmail.com", "U12 Drenge"));
            workerController.Workers.Add(new Volunteer("Emil", "AnEmail@gmail.com", "U12 Drenge"));
            workerController.Workers.Add(new Volunteer("Rasmus", "JegKanIkkeLideSex@NarrePik.fuck", "U12 Drenge"));
            workerController.Workers.Add(new Volunteer("Peter", "AnEmail@gmail.com", "U12 Drenge"));
            workerController.Workers.Add(new Volunteer("Søren", "AnEmail@gmail.com", "U12 Drenge"));
            workerController.Workers.Add(new Volunteer("Krisjan", "AnEmail@gmail.com", "U12 Drenge"));
            workerController.Workers.Add(new Volunteer("Mikkel", "AnEmail@gmail.com", "U12 Drenge"));

            scheduleController.CreateTask("Kitchen");
            scheduleController.CreateTask("Accomadation");
            scheduleController.CreateTask("Koisk");
            scheduleController.CreateTask("Dinning Hall");
            scheduleController.CreateTask("DishWash");
            scheduleController.CreateTask("Car");

            scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 6, 15, 0), new DateTime(2017, 4, 13, 10, 00, 0), scheduleController.GetAllTasks()[0], 7, "Nope"));
            scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 11, 00, 0), new DateTime(2017, 4, 13, 15, 00, 0), scheduleController.GetAllTasks()[0], 7, "Nope"));
            scheduleController.CreateShift(new Shift(new DateTime(2017, 6, 27, 12, 30, 0), new DateTime(2017, 6, 27, 18, 30, 0), scheduleController.GetAllTasks()[1], 5, "Nope"));
            scheduleController.CreateShift(new Shift(new DateTime(2017, 6, 27, 2, 0, 0), new DateTime(2017, 6, 27, 5, 30, 0), scheduleController.GetAllTasks()[1], 5, "Nope"));
            scheduleController.CreateShift(new Shift(new DateTime(2017, 6, 27, 18, 0, 0), new DateTime(2017, 6, 27, 22, 30, 0), scheduleController.GetAllTasks()[3], 5, "Nope"));
            scheduleController.CreateShift(new Shift(new DateTime(2017, 6, 27, 9, 20, 0), new DateTime(2017, 6, 27, 14, 30, 0), scheduleController.GetAllTasks()[3], 5, "Nope"));
            scheduleController.CreateShift(new Shift(new DateTime(2017, 6, 27, 17, 10, 0), new DateTime(2017, 6, 27, 23, 0, 0), scheduleController.GetAllTasks()[2], 5, "Nope"));
            
            scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
            scheduleController.GetAllShifts()[1].CreateRequest((Volunteer)workerController.Workers[1]);
            scheduleController.GetAllShifts()[2].CreateRequest((Volunteer)workerController.Workers[2]);
            scheduleController.GetAllShifts()[3].CreateRequest((Volunteer)workerController.Workers[3]);
            scheduleController.GetAllShifts()[4].CreateRequest((Volunteer)workerController.Workers[4]);
            scheduleController.GetAllShifts()[5].CreateRequest((Volunteer)workerController.Workers[5]);
            scheduleController.GetAllShifts()[6].CreateRequest((Volunteer)workerController.Workers[6]);
            scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[7]);
            scheduleController.GetAllShifts()[1].CreateRequest((Volunteer)workerController.Workers[8]);
            scheduleController.GetAllShifts()[2].CreateRequest((Volunteer)workerController.Workers[9]);
            scheduleController.GetAllShifts()[3].CreateRequest((Volunteer)workerController.Workers[10]);
            scheduleController.GetAllShifts()[4].CreateRequest((Volunteer)workerController.Workers[0]);
            scheduleController.GetAllShifts()[5].CreateRequest((Volunteer)workerController.Workers[1]);
            scheduleController.GetAllShifts()[6].CreateRequest((Volunteer)workerController.Workers[2]);
            scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
            scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
            scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
            scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
            scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
            scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
            scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
            scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);


            UserInterface.TheMainWindow Ui = new UserInterface.TheMainWindow(scheduleController, workerController);
            
            Ui.Start();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database;
using Dapper;
using System.Data.Entity;
using VolunteerSystem.Model;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace VolunteerSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            FinalController db2 = new FinalController(new DatabaseContext(SqlDataConnecter.CnnVal("DatabaseCS")));

            //db2.schedule.Add(new Schedule(2018));

            //Schedule schedule = new Schedule(2018);
            //schedule.Tasks.Add("Kitchen");
            //db2.schedule.Add(schedule);
            ScheduleController scheduleController = new ScheduleController(db2.schedule.GetSchedule(1), db2);
            WorkerController workerController = new WorkerController(db2);

            //scheduleController.CreateTask("Kitchen");
            //scheduleController.CreateTask("Accomadation");
            //scheduleController.CreateTask("Kiosk");
            //scheduleController.CreateTask("Dinning Hall");
            //scheduleController.CreateTask("DishWash");
            //scheduleController.CreateTask("Car");


            //try
            //{
            //    workerController.CreateWorker(new Volunteer("Denne dude er validated for season tickets ", "nej@ja.maybe", "U12 Drenge"));
            //}
            //catch (DbEntityValidationException dbEx)
            //{
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            Trace.TraceInformation("Property: {0} Error: {1}",
            //                                    validationError.PropertyName,
            //                                    validationError.ErrorMessage);
            //        }
            //    }
            //}
            //((Volunteer)workerController.ListOfWorkers.First()).TempAddYearWorked(2017);
            //((Volunteer)workerController.ListOfWorkers.First()).TempAddYearWorked(2018);
            //workerController.CreateWorker(new Volunteer("Kasper", "AnEmail@domainFindesIkke.dk", "U8 Drenge"));
            //((Volunteer)workerController.ListOfWorkers[1]).TempAddYearWorked(2017);
            //workerController.CreateWorker(new Volunteer("Casper", "AnEmail@domainFindesIkke.dk", "U16 Drenge"));
            //workerController.CreateWorker(new Volunteer("Mark", "AnEmail@domainFindesIkke.dk", "U12 Drenge"));
            //workerController.CreateWorker(new Volunteer("Mustafa", "AnEmail@domainFindesIkke.dk", "U12 Drenge"));
            //workerController.CreateWorker(new Volunteer("Emil", "AnEmail@domainFindesIkke.dk", "U12 Drenge"));
            //workerController.CreateWorker(new Volunteer("Rasmus", "JegKanIkkeLideSex@NarrePik.fuck", "U12 Drenge"));
            //workerController.CreateWorker(new Volunteer("Peter", "anEmail@domainFindesIkke.dk", "U12 Drenge"));
            //workerController.CreateWorker(new Volunteer("Søren", "AnEmail@domainFindesIkke.dk", "U14 Drenge"));
            //workerController.CreateWorker(new Volunteer("Krisjan", "AnEmail@domainFindesIkke.dk", "U12 Drenge"));
            //workerController.CreateWorker(new Volunteer("Mikkel", "AnEmail@domainFindesIkke.dk", "U12 Piger"));


            //Day one dining hall
            // scheduleController.CreateShift(new Shift(new DateTime(2016, 4, 13, 6, 15, 0), new DateTime(2017, 4, 13, 10, 00, 0), scheduleController.GetAllTasks()[3], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 6, 15, 0), new DateTime(2017, 4, 13, 10, 00, 0), scheduleController.GetAllTasks()[3], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 11, 00, 0), new DateTime(2017, 4, 13, 15, 00, 0), scheduleController.GetAllTasks()[3], 5, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 17, 00, 0), new DateTime(2017, 4, 13, 21, 00, 0), scheduleController.GetAllTasks()[3], 5, "Nope"));

            //Day one dishes
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 7, 30, 0), new DateTime(2017, 4, 13, 10, 30, 0), scheduleController.GetAllTasks()[4], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 11, 30, 0), new DateTime(2017, 4, 13, 15, 30, 0), scheduleController.GetAllTasks()[4], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 17, 00, 0), new DateTime(2017, 4, 13, 21, 30, 0), scheduleController.GetAllTasks()[4], 7, "Nope"));

            //Day one car
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 11, 30, 0), new DateTime(2017, 4, 13, 14, 00, 0), scheduleController.GetAllTasks()[5], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 17, 30, 0), new DateTime(2017, 4, 13, 20, 00, 0), scheduleController.GetAllTasks()[5], 7, "Nope"));

            //Day one kitchen
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 7, 30, 0), new DateTime(2017, 4, 13, 12, 00, 0), scheduleController.GetAllTasks()[0], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 15, 0, 0), new DateTime(2017, 4, 13, 20, 30, 0), scheduleController.GetAllTasks()[0], 7, "Nope"));

            //Day one kiosk
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 8, 0, 0), new DateTime(2017, 4, 13, 11, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 11, 0, 0), new DateTime(2017, 4, 13, 14, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 14, 0, 0), new DateTime(2017, 4, 13, 17, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 17, 0, 0), new DateTime(2017, 4, 13, 20, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 20, 0, 0), new DateTime(2017, 4, 13, 22, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
            //day one accommodation
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 8, 0, 0), new DateTime(2017, 4, 13, 12, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 12, 0, 0), new DateTime(2017, 4, 13, 16, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 16, 0, 0), new DateTime(2017, 4, 13, 20, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 20, 0, 0), new DateTime(2017, 4, 13, 23, 59, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 00, 0, 0), new DateTime(2017, 4, 13, 8, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));

            //Day one dining hall
            // scheduleController.CreateShift(new Shift(new DateTime(2016, 4, 13, 6, 15, 0), new DateTime(2017, 4, 13, 10, 00, 0), scheduleController.GetAllTasks()[3], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 6, 15, 0), new DateTime(2017, 4, 14, 10, 00, 0), scheduleController.GetAllTasks()[3], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 11, 00, 0), new DateTime(2017, 4, 14, 15, 00, 0), scheduleController.GetAllTasks()[3], 5, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 17, 00, 0), new DateTime(2017, 4, 14, 21, 00, 0), scheduleController.GetAllTasks()[3], 5, "Nope"));

            //Day one dishes
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 7, 30, 0), new DateTime(2017, 4, 14, 10, 30, 0), scheduleController.GetAllTasks()[4], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 11, 30, 0), new DateTime(2017, 4, 14, 15, 30, 0), scheduleController.GetAllTasks()[4], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 17, 00, 0), new DateTime(2017, 4, 14, 21, 30, 0), scheduleController.GetAllTasks()[4], 7, "Nope"));

            //Day one car
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 11, 30, 0), new DateTime(2017, 4, 14, 14, 00, 0), scheduleController.GetAllTasks()[5], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 17, 30, 0), new DateTime(2017, 4, 14, 20, 00, 0), scheduleController.GetAllTasks()[5], 7, "Nope"));

            //Day one kitchen
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 7, 30, 0), new DateTime(2017, 4, 14, 12, 00, 0), scheduleController.GetAllTasks()[0], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 15, 0, 0), new DateTime(2017, 4, 14, 20, 30, 0), scheduleController.GetAllTasks()[0], 7, "Nope"));

            //Day one kiosk
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 8, 0, 0), new DateTime(2017, 4, 14, 11, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 11, 0, 0), new DateTime(2017, 4, 14, 14, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 14, 0, 0), new DateTime(2017, 4, 14, 17, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 17, 0, 0), new DateTime(2017, 4, 14, 20, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 20, 0, 0), new DateTime(2017, 4, 14, 22, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
            //day one accommodation
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 8, 0, 0), new DateTime(2017, 4, 14, 12, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 12, 0, 0), new DateTime(2017, 4, 14, 16, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 16, 0, 0), new DateTime(2017, 4, 14, 20, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 20, 0, 0), new DateTime(2017, 4, 14, 23, 59, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
            //scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 00, 0, 0), new DateTime(2017, 4, 14, 8, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));

            //for (int i = 1; i <= 7; i++)
            //{
            //    scheduleController.AddWorkerToShift(scheduleController.FindSingleShift(x => x.Task == "Kiosk"), workerController.ListOfWorkers[i]);
            //}

            //for (int i = 1; i <= 7; i++)
            //{
            //    scheduleController.AddWorkerToShift(scheduleController.GetAllListOfShifts().FindAll(y => y.Task == "Kiosk")[2], workerController.ListOfWorkers[i]);
            //}
            //scheduleController.GetAllListOfShifts()[0].CreateRequest((Volunteer)workerController.ListOfWorkers[0]);
            //scheduleController.GetAllListOfShifts()[1].CreateRequest((Volunteer)workerController.ListOfWorkers[1]);
            //scheduleController.GetAllListOfShifts()[2].CreateRequest((Volunteer)workerController.ListOfWorkers[2]);
            //scheduleController.GetAllListOfShifts()[3].CreateRequest((Volunteer)workerController.ListOfWorkers[3]);
            //scheduleController.GetAllListOfShifts()[4].CreateRequest((Volunteer)workerController.ListOfWorkers[4]);
            //scheduleController.GetAllListOfShifts()[5].CreateRequest((Volunteer)workerController.ListOfWorkers[5]);
            //scheduleController.GetAllListOfShifts()[6].CreateRequest((Volunteer)workerController.ListOfWorkers[6]);
            //scheduleController.GetAllListOfShifts()[0].CreateRequest((Volunteer)workerController.ListOfWorkers[7]);
            //scheduleController.GetAllListOfShifts()[1].CreateRequest((Volunteer)workerController.ListOfWorkers[8]);
            //scheduleController.GetAllListOfShifts()[2].CreateRequest((Volunteer)workerController.ListOfWorkers[9]);
            //scheduleController.GetAllListOfShifts()[3].CreateRequest((Volunteer)workerController.ListOfWorkers[10]);
            //scheduleController.GetAllListOfShifts()[4].CreateRequest((Volunteer)workerController.ListOfWorkers[0]);
            //scheduleController.GetAllListOfShifts()[5].CreateRequest((Volunteer)workerController.ListOfWorkers[1]);
            //scheduleController.GetAllListOfShifts()[6].CreateRequest((Volunteer)workerController.ListOfWorkers[2]);
            //scheduleController.GetAllListOfShifts()[0].CreateRequest((Volunteer)workerController.ListOfWorkers[1]);
            //scheduleController.GetAllListOfShifts()[0].CreateRequest((Volunteer)workerController.ListOfWorkers[2]);
            //scheduleController.GetAllListOfShifts()[0].CreateRequest((Volunteer)workerController.ListOfWorkers[3]);
            //scheduleController.GetAllListOfShifts()[0].CreateRequest((Volunteer)workerController.ListOfWorkers[4]);
            //scheduleController.GetAllListOfShifts()[0].CreateRequest((Volunteer)workerController.ListOfWorkers[5]);
            //scheduleController.GetAllListOfShifts()[0].CreateRequest((Volunteer)workerController.ListOfWorkers[6]);
            //scheduleController.GetAllListOfShifts()[0].CreateRequest((Volunteer)workerController.ListOfWorkers[8]);
            //scheduleController.GetAllListOfShifts()[0].CreateRequest((Volunteer)workerController.ListOfWorkers[9]);

            db2.Complete();


            Notifier.AllNotifications.Add(new Notification("High Importance: Volunteer droped out", "Unfortunetly Casper has dropped his shift in the kitchen friday 22-01-2017", NotificationImportance.HighImportance));
            Notifier.AllNotifications.Add(new Notification("Medium Importance: En lang headder for at se hvad der sker når den bliver lang",
                "Og den skal selvfølgelig have en mindst ligeså lang body for at se hvad der sker her ovre. Men hvor langt er for langt? Måske lige en sætning mere",
                NotificationImportance.MediumImportance));
            Notifier.AllNotifications.Add(new Notification("Low importance: Headder", "Boddy, denne er ikke så vigtig", NotificationImportance.LowImportance));


            UserInterface.TheMainWindow Ui = new UserInterface.TheMainWindow(scheduleController, workerController);

            Ui.Start();

        }
    }
}

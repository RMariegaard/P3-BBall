﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database;
using Dapper;
using System.Data.Entity;
using VolunteerSystem.Model;

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

            FinalController db2 = new FinalController(new DatabaseContext(SqlDataConnecter.CnnVal("DatabaseCS"))); 
            
                db2.externalWorker.Add(new TestExternalWorker("asd", "addsd"));
                db2.externalWorker.Add(new TestExternalWorker("dasd", "adddssd"));
                db2.externalWorker.Add(new TestExternalWorker("asasdd", "addasdsd"));
            db2.volunteer.Add(new TestVolunteer("asd", "asd","asdd"));
            db2.volunteer.Add(new TestVolunteer("asd", "asd", "asdd"));

            db2.volunteer.Add(new TestVolunteer("asd", "asd", "asdd"));
            db2.volunteer.Add(new TestVolunteer("asd", "asd", "asdd"));
          

           TestShift asd = new TestShift("hey", "asd");

                asd.volunterId = 3;
            TestVolunteer klasu = new TestVolunteer("asd", "asdasd", "asda");
            klasu.volunterId = 2;
            asd.listOfVolunteers = new List<TestVolunteer>();
            asd.listOfVolunteers.Add(klasu);

            db2.shift.Add(asd);


                db2.Complete();

            



            System.Windows.Forms.Application.Exit();

           // Schedule schedule = new Schedule(2018);
           // ScheduleController scheduleController = new ScheduleController(schedule);
           // WorkerController workerController = new WorkerController();

          

           // workerController.Workers.Add(new Volunteer("Denne dude er validated for season tickets ", "AnEmail@gmail.com", "U12 Drenge"));
           // ((Volunteer)workerController.Workers.First()).TempAddYearWorked(2017);
           // ((Volunteer)workerController.Workers.First()).TempAddYearWorked(2018);
           // workerController.Workers.Add(new Volunteer("Kasper", "AnEmail@domainFindesIkke.dk", "U8 Drenge"));
           // ((Volunteer)workerController.Workers[1]).TempAddYearWorked(2017);
           // workerController.Workers.Add(new Volunteer("Casper", "AnEmail@domainFindesIkke.dk", "U16 Drenge"));
           // workerController.Workers.Add(new Volunteer("Mark", "AnEmail@domainFindesIkke.dk", "U12 Drenge"));
           // workerController.Workers.Add(new Volunteer("Mustafa", "AnEmail@domainFindesIkke.dk", "U12 Drenge"));
           // workerController.Workers.Add(new Volunteer("Emil", "AnEmail@domainFindesIkke.dk", "U12 Drenge"));
           // workerController.Workers.Add(new Volunteer("Rasmus", "JegKanIkkeLideSex@NarrePik.fuck", "U12 Drenge"));
           // workerController.Workers.Add(new Volunteer("Peter", "AnEmail@domainFindesIkke.dk", "U12 Drenge"));
           // workerController.Workers.Add(new Volunteer("Søren", "AnEmail@domainFindesIkke.dk", "U14 Drenge"));
           // workerController.Workers.Add(new Volunteer("Krisjan", "AnEmail@domainFindesIkke.dk", "U12 Drenge"));
           // workerController.Workers.Add(new Volunteer("Mikkel", "AnEmail@domainFindesIkke.dk", "U12 Piger") { PhoneNumber = 11223344 });

           // scheduleController.CreateTask("Kitchen");
           // scheduleController.CreateTask("Accomadation");
           // scheduleController.CreateTask("Koisk");
           // scheduleController.CreateTask("Dinning Hall");
           // scheduleController.CreateTask("DishWash");
           // scheduleController.CreateTask("Car");

           // //Day one dining hall
           //// scheduleController.CreateShift(new Shift(new DateTime(2016, 4, 13, 6, 15, 0), new DateTime(2017, 4, 13, 10, 00, 0), scheduleController.GetAllTasks()[3], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 6, 15, 0), new DateTime(2017, 4, 13, 10, 00, 0), scheduleController.GetAllTasks()[3], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 11, 00, 0), new DateTime(2017, 4, 13, 15, 00, 0), scheduleController.GetAllTasks()[3], 5, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 17, 00, 0), new DateTime(2017, 4, 13, 21, 00, 0), scheduleController.GetAllTasks()[3], 5, "Nope"));

           // //Day one dishes
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 7, 30, 0), new DateTime(2017, 4, 13, 10, 30, 0), scheduleController.GetAllTasks()[4], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 11, 30, 0), new DateTime(2017, 4, 13, 15, 30, 0), scheduleController.GetAllTasks()[4], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 17, 00, 0), new DateTime(2017, 4, 13, 21, 30, 0), scheduleController.GetAllTasks()[4], 7, "Nope"));

           // //Day one car 
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 11, 30, 0), new DateTime(2017, 4, 13, 14, 00, 0), scheduleController.GetAllTasks()[5], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 17, 30, 0), new DateTime(2017, 4, 13, 20, 00, 0), scheduleController.GetAllTasks()[5], 7, "Nope"));

           // //Day one kitchen 
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 7, 30, 0), new DateTime(2017, 4, 13, 12, 00, 0), scheduleController.GetAllTasks()[0], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 15, 0, 0), new DateTime(2017, 4, 13, 20, 30, 0), scheduleController.GetAllTasks()[0], 7, "Nope"));

           // //Day one kiosk
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 8, 0, 0), new DateTime(2017, 4, 13, 11, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 11, 0, 0), new DateTime(2017, 4, 13, 14, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 14, 0, 0), new DateTime(2017, 4, 13, 17, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 17, 0, 0), new DateTime(2017, 4, 13, 20, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 20, 0, 0), new DateTime(2017, 4, 13, 22, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
           // //day one accommodation 
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 8, 0, 0), new DateTime(2017, 4, 13, 12, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 12, 0, 0), new DateTime(2017, 4, 13, 16, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 16, 0, 0), new DateTime(2017, 4, 13, 20, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 20, 0, 0), new DateTime(2017, 4, 13, 23, 59, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 13, 00, 0, 0), new DateTime(2017, 4, 13, 8, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));

           // //Day one dining hall
           // // scheduleController.CreateShift(new Shift(new DateTime(2016, 4, 13, 6, 15, 0), new DateTime(2017, 4, 13, 10, 00, 0), scheduleController.GetAllTasks()[3], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 6, 15, 0), new DateTime(2017, 4, 14, 10, 00, 0), scheduleController.GetAllTasks()[3], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 11, 00, 0), new DateTime(2017, 4, 14, 15, 00, 0), scheduleController.GetAllTasks()[3], 5, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 17, 00, 0), new DateTime(2017, 4, 14, 21, 00, 0), scheduleController.GetAllTasks()[3], 5, "Nope"));

           // //Day one dishes
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 7, 30, 0), new DateTime(2017, 4, 14, 10, 30, 0), scheduleController.GetAllTasks()[4], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 11, 30, 0), new DateTime(2017, 4, 14, 15, 30, 0), scheduleController.GetAllTasks()[4], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 17, 00, 0), new DateTime(2017, 4, 14, 21, 30, 0), scheduleController.GetAllTasks()[4], 7, "Nope"));

           // //Day one car 
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 11, 30, 0), new DateTime(2017, 4, 14, 14, 00, 0), scheduleController.GetAllTasks()[5], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 17, 30, 0), new DateTime(2017, 4, 14, 20, 00, 0), scheduleController.GetAllTasks()[5], 7, "Nope"));

           // //Day one kitchen 
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 7, 30, 0), new DateTime(2017, 4, 14, 12, 00, 0), scheduleController.GetAllTasks()[0], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 15, 0, 0), new DateTime(2017, 4, 14, 20, 30, 0), scheduleController.GetAllTasks()[0], 7, "Nope"));

           // //Day one kiosk
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 8, 0, 0), new DateTime(2017, 4, 14, 11, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 11, 0, 0), new DateTime(2017, 4, 14, 14, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 14, 0, 0), new DateTime(2017, 4, 14, 17, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 17, 0, 0), new DateTime(2017, 4, 14, 20, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 20, 0, 0), new DateTime(2017, 4, 14, 22, 00, 0), scheduleController.GetAllTasks()[2], 7, "Nope"));
           // //day one accommodation 
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 8, 0, 0), new DateTime(2017, 4, 14, 12, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 12, 0, 0), new DateTime(2017, 4, 14, 16, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 16, 0, 0), new DateTime(2017, 4, 14, 20, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 20, 0, 0), new DateTime(2017, 4, 14, 23, 59, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));
           // scheduleController.CreateShift(new Shift(new DateTime(2017, 4, 14, 00, 0, 0), new DateTime(2017, 4, 14, 8, 00, 0), scheduleController.GetAllTasks()[1], 7, "Nope"));

           // for (int i = 1; i <= 7; i++)
           // {
           //     scheduleController.AddWorkerToShift(scheduleController.FindSingleShift(x => x.ID == 12), workerController.Workers[i]);
           // }

           // for (int i = 1; i <= 7; i++)
           // {
           //     scheduleController.AddWorkerToShift(scheduleController.FindSingleShift(x => x.ID == 15), workerController.Workers[i]);
           // }
           // scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
           // scheduleController.GetAllShifts()[1].CreateRequest((Volunteer)workerController.Workers[1]);
           // scheduleController.GetAllShifts()[2].CreateRequest((Volunteer)workerController.Workers[2]);
           // scheduleController.GetAllShifts()[3].CreateRequest((Volunteer)workerController.Workers[3]);
           // scheduleController.GetAllShifts()[4].CreateRequest((Volunteer)workerController.Workers[4]);
           // scheduleController.GetAllShifts()[5].CreateRequest((Volunteer)workerController.Workers[5]);
           // scheduleController.GetAllShifts()[6].CreateRequest((Volunteer)workerController.Workers[6]);
           // scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[7]);
           // scheduleController.GetAllShifts()[1].CreateRequest((Volunteer)workerController.Workers[8]);
           // scheduleController.GetAllShifts()[2].CreateRequest((Volunteer)workerController.Workers[9]);
           // scheduleController.GetAllShifts()[3].CreateRequest((Volunteer)workerController.Workers[10]);
           // scheduleController.GetAllShifts()[4].CreateRequest((Volunteer)workerController.Workers[0]);
           // scheduleController.GetAllShifts()[5].CreateRequest((Volunteer)workerController.Workers[1]);
           // scheduleController.GetAllShifts()[6].CreateRequest((Volunteer)workerController.Workers[2]);
           // scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
           // scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
           // scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
           // scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
           // scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
           // scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
           // scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);
           // scheduleController.GetAllShifts()[0].CreateRequest((Volunteer)workerController.Workers[0]);


           // var db = new Database2.VolunteerDatabase();
           // db.Add(workerController.Workers[0] as Volunteer);
           // var v = workerController.Workers[0] as Volunteer;
           // v.TempAddYearWorked(2019);
           // v.ID = 5;
           // db.Update(v);
           // db.Remove(v);


            //UserInterface.TheMainWindow Ui = new UserInterface.TheMainWindow(scheduleController, workerController);
            
            //Ui.Start();
        }
    }
}

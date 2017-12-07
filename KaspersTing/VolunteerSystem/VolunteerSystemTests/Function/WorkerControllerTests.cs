﻿using NUnit.Framework;
using VolunteerSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem.Tests
{
    [TestFixture()]
    public class WorkerControllerTests
    {
        WorkerController workerController;
        [SetUp]
        public void init()
        {
            workerController = new WorkerController();
        }

        [Test()]
        public void CreateVolunteerTest()
        {
            Volunteer volunteer = new Volunteer(true, "test", "email@notAEmail.Test", "U12 Boys");

            workerController.CreateWorker(volunteer);
            Assert.IsTrue(workerController.ListOfWorkers.Contains(volunteer));
        }
        [Test()]
        public void CreateExternalWorkerTest()
        {
            ExternalWorker worker = new ExternalWorker(true, "test", "email@notAEmail.Test");

            workerController.CreateWorker(worker);
            Assert.IsTrue(workerController.ListOfWorkers.Contains(worker));
        }

        [Test()]
        public void SearchListOfWorkersFindSameAssociationTest()
        {
            Volunteer volunteer = new Volunteer(true, "test", "email@notAEmail.Test", "U12 Boys");
            Volunteer volunteer2 = new Volunteer(true, "test2", "email@notAEmail.Test", "U12 Boys");
            Volunteer volunteer3 = new Volunteer(true, "test3", "email@notAEmail.Test", "U12 Boys");
            Volunteer volunteer4 = new Volunteer(true, "test4", "email@notAEmail.Test", "U10Boys");
            workerController.CreateWorker(volunteer);
            workerController.CreateWorker(volunteer2);
            workerController.CreateWorker(volunteer3);
            workerController.CreateWorker(volunteer4);

            Assert.AreEqual(workerController.SearchListOfWorkers(x => ((Volunteer)x).Association == "U12 Boys").Count(), 3);
        }
        public void SearchListOfWorkersVolunteeringThisYearTest()
        {
            Volunteer volunteer = new Volunteer(true, "test", "email@notAEmail.Test", "U12 Boys");
            Volunteer volunteer2 = new Volunteer(true, "test2", "email@notAEmail.Test", "U12 Boys");
            Volunteer volunteer3 = new Volunteer(true, "test3", "email@notAEmail.Test", "U12 Boys");
            Volunteer volunteer4 = new Volunteer(true, "test4", "email@notAEmail.Test", "U10Boys");
            workerController.CreateWorker(volunteer);
            workerController.CreateWorker(volunteer2);
            workerController.CreateWorker(volunteer3);
            workerController.CreateWorker(volunteer4);
            Shift shift = new Shift(true, new DateTime(), new DateTime(), "TaskTest", 9, "DescriptionTest");
            ScheduleController scheduleController = new ScheduleController(new Schedule(2018));
            scheduleController.AddWorkerToShift(shift, volunteer);
            scheduleController.AddWorkerToShift(shift, volunteer2);

            Assert.AreEqual(workerController.SearchListOfWorkers(x => ((Volunteer)x).YearsWorked.LastOrDefault() == 2018).Count(), 2);
        }

        //[Test()]
        //public void ViewWorkerInformationTest()
        //{
        //    Assert.Fail();
        //}

        [Test()]
        public void GetAllTeamsTest()
        {
            Volunteer volunteer = new Volunteer(true, "test", "email@notAEmail.Test", "U12 Boys");
            Volunteer volunteer2 = new Volunteer(true, "test2", "email@notAEmail.Test", "U10 Boys");
            Volunteer volunteer3 = new Volunteer(true, "test3", "email@notAEmail.Test", "U12 Girls");
            Volunteer volunteer4 = new Volunteer(true, "test4", "email@notAEmail.Test", "U10 Boys");
            workerController.CreateWorker(volunteer);
            workerController.CreateWorker(volunteer2);
            workerController.CreateWorker(volunteer3);
            workerController.CreateWorker(volunteer4);

            Assert.AreEqual(workerController.GetAllTeams().Count(), 3);
        }
    }
}
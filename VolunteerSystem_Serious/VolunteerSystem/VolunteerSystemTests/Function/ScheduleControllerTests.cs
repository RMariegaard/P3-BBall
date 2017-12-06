using NUnit.Framework;
using VolunteerSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem.Tests
{
    [TestFixture()]
    public class ScheduleControllerTests
    {

        Schedule schedule;
        ScheduleController scheduleController;

        [SetUp]
        public void init()
        {
            schedule = new Schedule(true, 2018);
            scheduleController = new ScheduleController(schedule);
        }

        //[Test()]
        //public void CreateScheduleTest()
        //{
        //    Assert.Fail();
        //}

        [TestCase(2018)]
        [TestCase(2017)]
        [TestCase(2019)]
        [TestCase(0)]
        public void ScheduleYearTest(int year)
        {
            Schedule scheduletest = new Schedule(true, year);
            ScheduleController controller = new ScheduleController(scheduletest);

            Assert.AreEqual(controller.ScheduleYear(), scheduletest.Year);
        }

        //    [Test()]
        //    public void GetAllRequestsTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void GetAllShiftsTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void GetAllTasksTest()
        //    {
        //        Assert.Fail();
        //    }

        [Test()]
        public void CreateShiftTest()
        {
            Shift shift = new Shift(true, new DateTime(), new DateTime(), "taski", 9, "test");

            scheduleController.CreateShift(shift);
            Assert.IsTrue(schedule.Shifts.Contains(shift));

        }

        [Test()]
        public void CreateTaskTest()
        {
            string task = "testTask";
            scheduleController.CreateTask(task);

            Assert.IsTrue(schedule.Tasks.Contains(task));

        }

        [Test()]
        public void RemoveTaskTest()
        {
            string task = "testTask";
            schedule.Tasks.Add(task);
            scheduleController.RemoveTask(task);

            Assert.IsFalse(schedule.Tasks.Contains(task));

        }

        [Test()]
        public void EditTaskOnShiftTest()
        {
            Shift oldShift = new Shift(true, new DateTime(), new DateTime(), "testTask", 10, "Description");
            Shift newShift = new Shift(true, new DateTime(), new DateTime(), "newTestTask", 10, "Description");
            scheduleController.EditShift(oldShift, newShift);

            Assert.AreEqual(oldShift.Task, newShift.Task);

        }
        [Test()]
        public void EditDesciptionOnShiftTest()
        {
            Shift oldShift = new Shift(true, new DateTime(), new DateTime(), "testTask", 10, "Description");
            Shift newShift = new Shift(true, new DateTime(), new DateTime(), "TestTask", 10, "NewTestDescription");
            scheduleController.EditShift(oldShift, newShift);

            Assert.AreEqual(oldShift.Description, newShift.Description);

        }
        [Test()]
        public void EditStartTimeOnShiftTest()
        {
            Shift oldShift = new Shift(true, new DateTime(), new DateTime(), "testTask", 10, "Description");
            Shift newShift = new Shift(true, new DateTime(2018, 04, 19), new DateTime(), "TestTask", 10, "Description");
            scheduleController.EditShift(oldShift, newShift);

            Assert.AreEqual(oldShift.StartTime, newShift.StartTime);
        }
        [Test()]
        public void EditEndTimeOnShiftTest()
        {
            Shift oldShift = new Shift(true, new DateTime(), new DateTime(), "testTask", 10, "Description");
            Shift newShift = new Shift(true, new DateTime(), new DateTime(2018, 04, 20), "TestTask", 10, "Description");
            scheduleController.EditShift(oldShift, newShift);

            Assert.AreEqual(oldShift.EndTime, newShift.EndTime);
        }
        [Test()]
        public void EditEverythingOnShiftTest()
        {
            Shift oldShift = new Shift(true, new DateTime(), new DateTime(), "testTask", 10, "Description");
            Shift newShift = new Shift(true, new DateTime(2018, 04, 19), new DateTime(2018, 04, 20), "newTestTask", 10, "NewTestDescription");
            scheduleController.EditShift(oldShift, newShift);

            Assert.AreEqual(oldShift.Task, newShift.Task);
            Assert.AreEqual(oldShift.Description, newShift.Description);
            Assert.AreEqual(oldShift.StartTime, newShift.StartTime);
            Assert.AreEqual(oldShift.EndTime, newShift.EndTime);
        }


        [Test()]
        public void DeleteShiftTest()
        {
            Shift shift = new Shift(true, new DateTime(), new DateTime(), "taski", 9, "test");
            schedule.Shifts.Add(shift);

            scheduleController.DeleteShift(shift);

            Assert.IsFalse(schedule.Shifts.Contains(shift));

        }

        [Test()]
        public void AddVolunteerToShiftTest()
        {
            Shift shift = new Shift(true, new DateTime(), new DateTime(), "taski", 9, "test");
            Volunteer volunteer = new Volunteer(true,"TestName", "TestEmail@Email.Email", "TestAssosiation");

            scheduleController.AddWorkerToShift(shift, volunteer);

            Assert.IsTrue(shift.Workers.Contains(volunteer));
        }
        [Test()]
        public void AddExternalWorkerToShiftTest()
        {
            Shift shift = new Shift(true, new DateTime(), new DateTime(), "taski", 9, "test");
            ExternalWorker externalWorker = new ExternalWorker(true, "externalWorker", "TestEmail@Email.Email");

            scheduleController.AddWorkerToShift(shift, externalWorker);

            Assert.IsTrue(shift.Workers.Contains(externalWorker));
        }
        [Test()]
        public void RemoveVolunteerFromShiftTest()
        {
            Shift shift = new Shift(true, new DateTime(), new DateTime(), "taski", 9, "test");
            Volunteer volunteer = new Volunteer(true, "TestName", "TestEmail@Email.Email", "TestAssosiation");

            shift.AddWorker(volunteer, 2018);

            scheduleController.RemoveWorkerFromShift(volunteer, shift);

            Assert.IsFalse(shift.Workers.Contains(volunteer));
        }
        [Test()]
        public void RemoveExternalWorkerToShiftTest()
        {
            Shift shift = new Shift(true, new DateTime(), new DateTime(), "taski", 9, "test");
            ExternalWorker externalWorker = new ExternalWorker(true, "externalWorker", "TestEmail@Email.Email");

            shift.AddWorker(externalWorker, 2018);
            scheduleController.RemoveWorkerFromShift(externalWorker, shift);

            Assert.IsFalse(shift.Workers.Contains(externalWorker));
        }


        [Test()]
        public void RemoveWorkerFromAllHisShiftsTest()
        {
            Shift shift = new Shift(true, new DateTime(), new DateTime(), "taski", 9, "test");
            Shift shift2 = new Shift(true, new DateTime(), new DateTime(), "taski2", 9, "test2");
            ExternalWorker externalWorker = new ExternalWorker(true, "externalWorker", "TestEmail@Email.Email");
            scheduleController.CreateShift(shift);
            scheduleController.CreateShift(shift2);

            shift.AddWorker(externalWorker, 2018);
            shift2.AddWorker(externalWorker, 2018);

            scheduleController.RemoveWorkerFromAllHisShifts(externalWorker);

            Assert.IsFalse(shift.Workers.Contains(externalWorker));
            Assert.IsFalse(shift2.Workers.Contains(externalWorker));
        }

        [Test()]
        public void RemoveAllRequestsForAWorkerTest()
        {
            Shift shift = new Shift(true, new DateTime(), new DateTime(), "taski", 9, "test");
            Shift shift2 = new Shift(true, new DateTime(), new DateTime(), "taski", 9, "test");
            scheduleController.CreateShift(shift);
            scheduleController.CreateShift(shift2);
            Volunteer volunteer = new Volunteer(true, "TestName", "TestEmail@Email.Email", "TestAssosiation");
            shift.CreateRequest(volunteer);
            shift2.CreateRequest(volunteer);

            scheduleController.RemoveAllRequestsForAWorker(volunteer);

            Assert.IsFalse(shift.Requests.Any(x => x.Worker == volunteer));
            Assert.IsFalse(shift2.Requests.Any(x => x.Worker == volunteer));

        }

        [Test()]
        public void RemoveRequestTest()
        {
            Shift shift = new Shift(true, new DateTime(), new DateTime(), "taski", 9, "test");
            scheduleController.CreateShift(shift);
            Request request = new Request(true, new Volunteer(true, "TestName", "TestEmail@Email.Email", "TestAssosiation"));

            shift.Requests.Add(request);
            scheduleController.RemoveRequest(request);

            Assert.IsFalse(shift.Requests.Contains(request));
        }

        //    [Test()]
        //    public void ViewShiftInformationTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void ViewMyShiftsTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void SendRequestTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void ApproveRequestTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void DenyRequestTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void FindSingleShiftTest()
        //    {
        //        Assert.Fail();
        //    }
    }
}
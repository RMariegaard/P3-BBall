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
            Schedule schedule = new Schedule(true, year);
            ScheduleController controller = new ScheduleController(schedule);

            Assert.AreEqual(controller.ScheduleYear(), schedule.Year);
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
            Schedule schedule = new Schedule(2018);
            ScheduleController scheduleController = new ScheduleController(schedule);
            Shift shift = new Shift(new DateTime(), new DateTime(), "taski", 9, "test");

            scheduleController.CreateShift(shift);
            Assert.IsTrue(schedule.Shifts.Contains(shift));

        }

        //    [Test()]
        //    public void CreateTaskTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void RemoveTaskTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void EditShiftTest()
        //    {
        //        Assert.Fail();
        //    }

        [Test()]
        public void DeleteShiftTest()
        {
            Schedule schedule = new Schedule(2018);
            ScheduleController scheduleController = new ScheduleController(schedule);
            Shift shift = new Shift(new DateTime(), new DateTime(), "taski", 9, "test");
            schedule.Shifts.Add(shift);

            scheduleController.DeleteShift(shift);

            Assert.IsFalse(schedule.Shifts.Contains(shift));

        }

        //    [Test()]
        //    public void AddWorkerToShiftTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void RemoveWorkerFromShiftTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void RemoveWorkerFromAllHisShiftsTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void RemoveAllRequestsForAWorkerTest()
        //    {
        //        Assert.Fail();
        //    }

        //    [Test()]
        //    public void RemoveRequestTest()
        //    {
        //        Assert.Fail();
        //    }

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
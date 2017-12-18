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
    public class ShiftTests
    {
        [Test()]
        public void ShiftTest()
        {
            DateTime startTime = DateTime.Now;
            string task = "Sleeping";
            int volunteersNeeded = 5;
            string description = "this is a descripotion";
            DateTime endTime = DateTime.Now;

            Shift shift = new Shift( startTime, endTime, task, volunteersNeeded, description);
            Assert.AreEqual(shift.StartTime, startTime);
            Assert.AreEqual(shift.EndTime, endTime);
            Assert.AreEqual(shift.Task, task);
            Assert.AreEqual(shift.VolunteersNeeded, volunteersNeeded);
            Assert.AreEqual(shift.Description, description);



        }
        
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(1000)]
        public void FalseIsFilledTest(int numberOfVolunteersNeeded)
        {
            Shift shift = new Shift( DateTime.Now, DateTime.Now, "", numberOfVolunteersNeeded, "");

            Assert.IsFalse(shift.IsFilled());
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(1000)]
        public void TrueIsFilledTest(int volunteersNeeded)
        {
            Shift shift = new Shift( DateTime.Now, DateTime.Now, "", volunteersNeeded, "");

            for(int i = 0; i<volunteersNeeded; i++)
            {
                shift.AddWorker(new ExternalWorker( "", "test@test.dk"), 2018);
            }

            Assert.IsTrue(shift.IsFilled());
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(1000)]
        public void NumberOfVolunteersTest(int numberOfVolunteers)
        {
            Shift shift = new Shift( DateTime.Now, DateTime.Now, "", 25, "");

            for (int i = 0; i < numberOfVolunteers; i++)
            {
                shift.AddWorker(new ExternalWorker( "", "test@test.dk"), 2018);
            }

            Assert.AreEqual(shift.NumberOfVolunteers(), numberOfVolunteers);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(1000)]
        public void NumberOfListOfRequestsTest(int numberOfListOfRequests)
        {
            Shift shift = new Shift( DateTime.Now, DateTime.Now, "", 25, "");

            for (int i = 0; i < numberOfListOfRequests; i++)
            {
                Volunteer volunteer = new Volunteer("", "test@test.dk", "");
                shift.CreateRequest(volunteer);
            }
            Assert.AreEqual(shift.NumberOfListOfRequests(), numberOfListOfRequests);

        }

        [Test()]
        public void EditShiftTest()
        {
            Shift shift = new Shift( DateTime.Now, DateTime.Now, "", 25, "");
            Shift newShift = new Shift( DateTime.Now, DateTime.Now, "task", 21, "newDescription");

            shift.EditShift(newShift);

            Assert.AreEqual(shift.StartTime, newShift.StartTime);
            Assert.AreEqual(shift.EndTime, newShift.EndTime);
            Assert.AreEqual(shift.Task, newShift.Task);
            Assert.AreEqual(shift.VolunteersNeeded, newShift.VolunteersNeeded);
            Assert.AreEqual(shift.Description, newShift.Description);

        }

        [Test()]
        public void RemoveExternalWorkerTest()
        {
            Shift shift = new Shift( DateTime.Now, DateTime.Now, "", 25, "");
            Worker worker = new ExternalWorker( "peter", "peter@test.dk");

            shift.AddWorker(worker, 2018);

            for (int i = 0; i < 5; i++)
            {
                shift.AddWorker(new ExternalWorker( "", "test@test.dk"), 2018);
                shift.AddWorker(new Volunteer( "", "test@test.dk", ""), 2018);
            }

            shift.RemoveWorker(worker);
            Assert.IsFalse(shift.ListOfWorkers.Contains(worker));
        }
        [Test()]
        public void RemoveVolunteerWorkerTest()
        {
            Shift shift = new Shift( DateTime.Now, DateTime.Now, "", 25, "");
            Worker worker = new Volunteer( "Peter", "Peter@email.dk", "U12 Drenge");

            shift.AddWorker(worker, 2018);

            for (int i = 0; i < 5; i++)
            {
                shift.AddWorker(new ExternalWorker( "", "test@test.dk"), 2018);
                shift.AddWorker(new Volunteer("", "test@test.dk", ""), 2018);
            }

            shift.RemoveWorker(worker);
            Assert.IsFalse(shift.ListOfWorkers.Contains(worker));
        }


        [Test()]
        public void AddExternalWorkerTest()
        {
            Shift shift = new Shift(DateTime.Now, DateTime.Now, "", 25, "");
            Worker worker = new ExternalWorker( "peter", "peter@test.dk");

            shift.AddWorker(worker, 2018);

            Assert.IsTrue(shift.ListOfWorkers.Contains(worker));
        }

        [Test()]
        public void AddVolunteerWorkerTest()
        {
            Shift shift = new Shift( DateTime.Now, DateTime.Now, "", 25, "");
            Worker worker = new Volunteer( "peter", "peter@test.dk", "");

            shift.AddWorker(worker, 2018);

            Assert.IsTrue(shift.ListOfWorkers.Contains(worker));
        }

        [Test()]
        public void CreateRequestTest()
        {
            Shift shift = new Shift( DateTime.Now, DateTime.Now, "", 25, "");
            Volunteer volunteer = new Volunteer( "peter", "peter@test.dk", "");

            shift.CreateRequest(volunteer);

            Assert.IsTrue(shift.ListOfRequests.Exists(x => x.Volunteer == volunteer));
        }

        [Test()]
        public void RemoveRequestTest()
        {
            Shift shift = new Shift( DateTime.Now, DateTime.Now, "", 25, "");
            Volunteer volunteer = new Volunteer( "peter", "peter@test.dk", "");
            Request request = new Request( volunteer);

            shift.ListOfRequests.Add(request);
            for (int i = 0; i < 5; i++)
            {
                shift.CreateRequest(new Volunteer( "", "test@test.dk", ""));
            }
            shift.RemoveRequest(request);
            Assert.IsFalse(shift.ListOfRequests.Contains(request));
        }

        //[Test()]
        //public void GetInformationTest()
        //{
        //    Assert.Fail();
        //}

        [Test()]
        public void ApproveRequestTest()
        {
            Shift shift = new Shift( DateTime.Now, DateTime.Now, "", 25, "");
            Volunteer volunteer = new Volunteer( "peter", "peter@test.dk", "");
            Request request = new Request( volunteer);

            for (int i = 0; i < 5; i++)
            {
                shift.CreateRequest(new Volunteer( "", "test@test.dk", ""));
            }

            shift.ApproveRequest(request, 2018);
            Assert.IsTrue(shift.ListOfWorkers.Contains(volunteer));
        }

        [Test()]
        public void DenieRequestTest()
        {
            Shift shift = new Shift( DateTime.Now, DateTime.Now, "", 25, "");
            Volunteer volunteer = new Volunteer( "peter", "peter@test.dk", "");
            Request request = new Request( volunteer);

            for (int i = 0; i < 5; i++)
            {
                shift.CreateRequest(new Volunteer( "", "test@test.dk", ""));
            }

            shift.DenieRequest(request);

            Assert.IsFalse(shift.ListOfRequests.Contains(request));
        }
    }
}
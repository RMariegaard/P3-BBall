using NUnit.Framework;
using VolunteerSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystemTests.Function;
using VolunteerSystem.Exceptions;

namespace VolunteerSystem.Tests
{
    [TestFixture()]
    public class WorkerControllerTests
    {
        WorkerController workerController;
        [SetUp]
        public void init()
        {
            workerController = new WorkerController(new FakeDatabase());
        }

        [Test()]
        public void CreateVolunteerTest()
        {
            Volunteer volunteer = new Volunteer( "test", "email@notAEmail.Test", "U12 Boys");

            workerController.CreateWorker(volunteer);
            Assert.IsTrue(workerController.ListOfWorkers.Contains(volunteer));
        }
        [Test()]
        public void CreateExternalWorkerTest()
        {
            ExternalWorker worker = new ExternalWorker( "test", "email@notAEmail.Test");

            workerController.CreateWorker(worker);
            Assert.IsTrue(workerController.ListOfWorkers.Contains(worker));
        }

        [Test()]
        public void SearchListOfWorkersFindSameAssociationTest()
        {
            Volunteer volunteer = new Volunteer( "test", "email@notAEmail.Test", "U12 Boys");
            Volunteer volunteer2 = new Volunteer( "test2", "email@notAEmail1.Test", "U12 Boys");
            Volunteer volunteer3 = new Volunteer( "test3", "email@notAEmail2.Test", "U12 Boys");
            Volunteer volunteer4 = new Volunteer("test4", "email@notAEmail3.Test", "U10Boys");
            workerController.CreateWorker(volunteer);
            workerController.CreateWorker(volunteer2);
            workerController.CreateWorker(volunteer3);
            workerController.CreateWorker(volunteer4);

            Assert.AreEqual(workerController.SearchListOfWorkers(x => ((Volunteer)x).Association == "U12 Boys").Count(), 3);
        }
        [Test()]
        public void SearchListOfWorkersVolunteeringThisYearTest()
        {
            Volunteer volunteer = new Volunteer( "test", "email@notAEmail1.Test", "U12 Boys");
            Volunteer volunteer2 = new Volunteer( "test2", "email@notAEmail2.Test", "U12 Boys");
            Volunteer volunteer3 = new Volunteer( "test3", "email@notAEmail3.Test", "U12 Boys");
            Volunteer volunteer4 = new Volunteer( "test4", "email@notAEmail4.Test", "U10Boys");
            workerController.CreateWorker(volunteer);
            workerController.CreateWorker(volunteer2);
            workerController.CreateWorker(volunteer3);
            workerController.CreateWorker(volunteer4);
            Shift shift = new Shift( new DateTime(), new DateTime(), "TaskTest", 9, "DescriptionTest");
            ScheduleController scheduleController = new ScheduleController(new Schedule(2018), new  FakeDatabase());
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
            Volunteer volunteer = new Volunteer("test", "email@notAEmail.Test1", "U12 Boys");
            Volunteer volunteer2 = new Volunteer( "test2", "email@notAEmail.Test2", "U10 Boys");
            Volunteer volunteer3 = new Volunteer( "test3", "email@notAEmail.Test3", "U12 Girls");
            Volunteer volunteer4 = new Volunteer("test4", "email@notAEmail.Test4", "U10 Boys");
            workerController.CreateWorker(volunteer);
            workerController.CreateWorker(volunteer2);
            workerController.CreateWorker(volunteer3);
            workerController.CreateWorker(volunteer4);

            Assert.AreEqual(workerController.GetAllTeams().Count(), 3);
        }

        [TestCase("jens@")]
        [TestCase("jensEmail.Dk")]
        [TestCase("jens")]
        [TestCase("jens@jens")]
        [TestCase(".Mark@.dk")]
        [TestCase("@jens.Dk")]
        [TestCase("@")]
        [TestCase("mark@test.dk.")]
        public void WrongEmailFormatExceptionTest(string email)
        {
            Worker worker = new ExternalWorker("Hallo", email);
            Assert.Throws<EmailNotValidException>(() => workerController.CreateWorker(worker));
        }

        [TestCase("Mark@hej.dk")]
        [TestCase("Cooper@Love.Com.Uk")]
        [TestCase("Easy.Peasy@Lemon.Dk")]
        [TestCase("Mark_Underscore@Test.Test")]
        [TestCase("admin")]
        [TestCase("")]
        public void CorrectEmailTest(string email)
        {
            Worker worker = new ExternalWorker("Mark", email);
            Assert.DoesNotThrow(() => workerController.CreateWorker(worker));
        }

        [TestCase("Mark@hej.dk")]
        [TestCase("Cooper@Love.Com.Uk")]
        [TestCase("Easy.Peasy@Lemon.Dk")]
        [TestCase("Mark_Underscore@Test.Test")]
        [TestCase("admin")]
        public void EmailUsedBeforeExceptionTest(string email)
        {
            Worker worker = new ExternalWorker("Mark", email);
            workerController.CreateWorker(worker);
            Worker worker2 = new ExternalWorker("Lars", email);
            Assert.Throws<EmailUsedBeforeException>(() => workerController.CreateWorker(worker));
        }

        [TestCase("Lars")]
        [TestCase("Lars Peter Jensen With A Long Name")]
        [TestCase("")]
        public void ChangeVolunteerNameTest(string newName)
        {
            Volunteer volunteer = new Volunteer("Mark", "AnEmail@TestEmail.dk", "U10 Drenge", 12345678, "password");

            workerController.UpdateVolunteer(volunteer, newName, "AnEmail@TestEmail.dk", "U10 Drenge", 12345678);

            Assert.AreEqual(volunteer.Name,newName);
        }
        [TestCase("jens@")]
        [TestCase("jensEmail.Dk")]
        [TestCase("jens")]
        [TestCase("jens@jens")]
        [TestCase(".Mark@.dk")]
        [TestCase("@jens.Dk")]
        [TestCase("@")]
        [TestCase("mark@test.dk.")]
        public void ChangeVolunteerEmailFailsTest(string email)
        {
            Volunteer volunteer = new Volunteer("Mark", "AnEmail@TestEmail.dk", "U10 Drenge", 12345678, "password");
            
            Assert.Throws<EmailNotValidException>(() => workerController.UpdateVolunteer(volunteer, "Mark", email, "U10 Drenge", 12345678));
        }
        [TestCase("Mark@hej.dk")]
        [TestCase("Cooper@Love.Com.Uk")]
        [TestCase("Easy.Peasy@Lemon.Dk")]
        [TestCase("Mark_Underscore@Test.Test")]
        [TestCase("admin")]
        [TestCase("")]
        public void ChangeVolunteerEmailPassesTest(string email)
        {
            Volunteer volunteer = new Volunteer("Mark", "AnEmail@TestEmail.dk", "U10 Drenge", 12345678, "password");
            workerController.UpdateVolunteer(volunteer, "Mark", email, "U10 Drenge", 12345678);
            Assert.AreEqual(email, volunteer.Email);
        }
        [Test()]
        public void ChangeVolunteerAssociationTest()
        {
            Volunteer volunteer = new Volunteer("Mark", "AnEmail@TestEmail.dk", "U10 Drenge", 12345678, "password");
            workerController.UpdateVolunteer(volunteer, "Mark", "AnEmail@TestEmail.dk", "U15 Piger", 12345678);
            Assert.AreEqual("U15 Piger", volunteer.Association);
        }

        [Test()]
        public void ChangeVolunteerPhonenumberTest()
        {
            Volunteer volunteer = new Volunteer("Mark", "AnEmail@TestEmail.dk", "U10 Drenge", 12345678, "password");
            workerController.UpdateVolunteer(volunteer, "Mark", "AnEmail@TestEmail.dk", "U10 Drenge", 87654321);
            Assert.AreEqual(87654321, volunteer.Phonenumber);
        }

        [Test()]
        public void ChangeVolunteerPasswordTest()
        {
            Volunteer volunteer = new Volunteer("Mark", "AnEmail@TestEmail.dk", "U10 Drenge", 12345678, "password");
            workerController.UpdateVolunteer(volunteer, "Mark", "AnEmail@TestEmail.dk", "U10 Drenge", 12345678, "newPassword");
            Assert.AreEqual(WorkerController.GetHash("newPassword"), volunteer.HashPassworkd);
        }
    }
}
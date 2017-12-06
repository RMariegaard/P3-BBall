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
    public class WorkerTests
    {

        [TestCase("Peter", "mark@test.dk", "Peter\tmark@test.dk")]
        [TestCase("Jens", "jens@test.dk", "Jens\tjens@test.dk")]
        public void VolunteerConstructTest(string name, string email, string actual)
        {
            Worker worker = new Volunteer(true, name, email, "");

            Assert.AreEqual(worker.GetInformation(), actual);
        }


        
        [TestCase("jens@")]
        [TestCase("jensEmail.Dk")]
        [TestCase("jens")]
        [TestCase("jens@jens")]
        [TestCase(".Mark@.dk")]
        [TestCase("@jens.Dk")]
        [TestCase("@")]
        [TestCase("mark@test.dk.")]
        public void WrongEmailFormatExceptionTest( string email)
        {
            Worker worker;
            Assert.Throws<Exception>(() =>worker = new ExternalWorker(true, "Hallo", email));
        }

        [TestCase("Mark@hej.dk")]
        [TestCase("Cooper@Love.Com.Uk")]
        [TestCase("Easy.Peasy@Lemon.Dk")]
        [TestCase("Mark_Underscore@Test.Test")]
        public void CorrectEmailTest(string email)
        {
            Worker worker = new ExternalWorker(true, "Mark", email);
            Assert.AreEqual(worker.Email, email);
        }

    }
}
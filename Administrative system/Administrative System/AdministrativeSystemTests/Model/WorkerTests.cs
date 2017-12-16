using NUnit.Framework;
using VolunteerSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Exceptions;

namespace VolunteerSystem.Tests
{
    [TestFixture()]
    public class WorkerTests
    {

        [TestCase("Peter", "mark@test.dk", "Peter\tmark@test.dk")]
        [TestCase("Jens", "jens@test.dk", "Jens\tjens@test.dk")]
        public void VolunteerConstructTest(string name, string email, string actual)
        {
            Worker worker = new Volunteer( name, email, "");

            Assert.AreEqual(worker.GetInformation(), actual);
        }

    }
}
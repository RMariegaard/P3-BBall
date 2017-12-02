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

        //idk tror ikke denne klasse skal testes overhoved
        [TestCase("Peter", "mark@test.dk", "Peter\tmark@test.dk")]
        [TestCase("Jens", "jens@test.dk", "Jens\tjens@test.dk")]
        public void GetInformationTest(string name, string email, string actual)
        {
            Worker worker = new Volunteer(name, email, "");

            Assert.AreEqual(worker.GetInformation(), actual);
        }


    }
}
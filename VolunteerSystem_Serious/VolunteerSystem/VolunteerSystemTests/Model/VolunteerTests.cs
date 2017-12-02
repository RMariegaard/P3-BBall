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
    public class VolunteerTests
    {
        [TestCase("Peter", "mark@test.dk", "U12 drenge")]
        [TestCase("Jens", "jens@test.dk", "U1 piger")]
        public void VolunteerTest(string name, string email, string assosiation)
        {
            Volunteer volunteer = new Volunteer(name, email, assosiation);

            Assert.AreEqual(volunteer.Name, name);
            Assert.AreEqual(volunteer.Email, email);
            Assert.AreEqual(volunteer.Assosiation, assosiation);
        }


        [TestCase(2018)]
        [TestCase(2017)]
        [TestCase(2019)]
        [TestCase(0)]
        public void AddYearWorkedTest(int year)
        {
            Volunteer volunteer = new Volunteer("Peter", "mark@test.dk", "U12 drenge");
            volunteer.AddYearWorked(year);

            Assert.AreEqual(volunteer.YearsWorked.LastOrDefault(), year);
        }

        [TestCase(2018)]
        [TestCase(2017)]
        [TestCase(2019)]
        [TestCase(0)]
        public void AddSameYearWorkedMulitpleTimesTest(int year)
        {
            Volunteer volunteer = new Volunteer("Peter", "mark@test.dk", "U12 drenge");
            volunteer.AddYearWorked(2000);
            volunteer.AddYearWorked(year);
            volunteer.AddYearWorked(year);

            Assert.AreNotEqual(volunteer.YearsWorked[volunteer.YearsWorked.Count - 2], year);
        }


        //[Test()]
        //public void IsValidForSeasonTicketsTest()
        //{
        //    Assert.Fail();
        //}

        [TestCase(2017, 2018, 2018)]
        [TestCase(2000, 2001, 2001)]
        [TestCase(0, 1, 1)]
        public void TrueIsValidForSeasonTicketsTest(int year1, int year2, int scheduleYear)
        {
            Volunteer volunteer = new Volunteer("Peter", "mark@test.dk", "U12 drenge");
            volunteer.AddYearWorked(year1);
            volunteer.AddYearWorked(year2);

            Assert.IsTrue(volunteer.IsValidForSeasonTickets(scheduleYear));
        }

        [TestCase(2015, 2018, 2018)]
        [TestCase(2000, 2001, 2002)]
        [TestCase(1, 1, 1)]
        public void FalseIsValidForSeasonTicketsTest(int year1, int year2, int scheduleYear)
        {
            Volunteer volunteer = new Volunteer("Peter", "mark@test.dk", "U12 drenge");
            volunteer.AddYearWorked(year1);
            volunteer.AddYearWorked(year2);

            Assert.IsFalse(volunteer.IsValidForSeasonTickets(scheduleYear));
        }

        [TestCase("Peter", "mark@test.dk", "U12 drenge", "U12 drenge Peter mark@test.dk")]
        [TestCase("Jens", "jens@test.dk", "U1 piger", "U1 piger jens jens@test.dk")]
        public void ToStringTest(string name, string email, string assosiation, string actual)
        {
            Volunteer volunteer = new Volunteer(name, email, assosiation);

            Assert.AreEqual(volunteer.ToString(), actual);
        }

    }
}
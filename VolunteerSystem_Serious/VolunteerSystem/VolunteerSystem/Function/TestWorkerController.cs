using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database;
using VolunteerSystem.Model;

namespace VolunteerSystem.Function
{
    public class TestWorkerController
    {
        private FinalController _database;
        private List<TestWorker> _listOfWorkers;

        public TestWorkerController(FinalController database)
        {
            _database = database;
            _listOfWorkers = new List<TestWorker>();
            _listOfWorkers.AddRange(_database.externalWorker.GetExternalWorkers());
            _listOfWorkers.AddRange(_database.volunteer.getthembitches());

        }

        public void CreateWorker(TestWorker worker)
        {
            _listOfWorkers.Add(worker);
            //_database.Complete();
        }

        public List<TestWorker> SearchWorkers(Predicate<TestWorker> predicate)
        {
            List<TestWorker> list = _listOfWorkers.FindAll(predicate);
            return list;
        }

        public string ViewWorkerInformation(TestWorker worker)
        {
            return worker.GetInformation();
        }

        internal void RemoveWorker(TestWorker worker)
        {
            if(worker is TestVolunteer)
            {
                _database.volunteer.Remove(worker as TestVolunteer);
            }
            else { _database.externalWorker.Remove(worker as TestExternalWorker); }
           
        }

        //public List<string> GetAllTeams()
        //{
        //    var allTeams = new List<string>();
        //    foreach (Worker worker in this.Workers)
        //    {
        //        if (worker is Volunteer volunteer)
        //        {
        //            string team = volunteer.Assosiation;
        //            if (!allTeams.Contains(team))
        //            {
        //                allTeams.Add(team);
        //            }
        //        }
        //    }


        //    return allTeams;
        //}

    }
}

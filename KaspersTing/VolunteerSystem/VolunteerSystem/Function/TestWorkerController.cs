using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database;
using VolunteerSystem.Model;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystem
{
    public class WorkerController
    {
        private IFinalController _database;
        public List<Worker> ListOfWorkers;

        public WorkerController(IFinalController database)
        {
            _database = database;
            ListOfWorkers = UpdateListOfVolunteers();
        }

        
        public void CreateWorker(Worker worker)
        {
            ListOfWorkers.Add(worker);
            if (worker is Volunteer)
            {
                _database.volunteer.Add(worker as Volunteer);
            }
            else
            {
                _database.externalWorker.Add(worker as ExternalWorker);
            }
            _database.Complete();
        }

        public List<Worker> SearchListOfWorkers(Predicate<Worker> predicate)
        {
            List<Worker> list = ListOfWorkers.FindAll(predicate);
            return list;
        }

        public string ViewWorkerInformation(Worker worker)
        {
            return worker.GetInformation();
        }

        internal void RemoveWorker(Worker worker)
        {
            if(worker is Volunteer)
            {
                _database.volunteer.Remove(worker as Volunteer);
            }
            else { _database.externalWorker.Remove(worker as ExternalWorker); }

            _database.Complete();
            ListOfWorkers = UpdateListOfVolunteers();
        }

        private List<Worker> UpdateListOfVolunteers()
        {
            List<Worker> ListOfWorkers = new List<Worker>();
            ListOfWorkers.AddRange(_database.volunteer.getthembitches());
            //ListOfWorkers.AddRange(_database.externalWorker.GetExternalListOfWorkers());
            return ListOfWorkers;
        }

        public List<string> GetAllTeams()
        {
            var allTeams = new List<string>();
            foreach (Worker worker in this.ListOfWorkers)
            {
                if (worker is Volunteer volunteer)
                {
                    string team = volunteer.Association;
                    if (!allTeams.Contains(team))
                    {
                        allTeams.Add(team);
                    }
                }
            }


            return allTeams;
        }

    }
}

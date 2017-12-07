using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class WorkerController
    {
        private List<Worker> _ListOfWorkers;
        public List<Worker> ListOfWorkers
        {
            get
            {
                return _ListOfWorkers;
            }
        }

        public WorkerController()
        {
            _ListOfWorkers = new List<Worker>();
        }

        public void CreateWorker(Worker worker)
        {
            _ListOfWorkers.Add(worker);
        }

        public List<Worker> SearchListOfWorkers(Predicate<Worker> predicate)
        {
            List<Worker> list = _ListOfWorkers.FindAll(predicate);
            return list;
        }

        public string ViewWorkerInformation(Worker worker)
        {
            return worker.GetInformation();
        }

        internal void RemoveWorker(Worker worker)
        {
            _ListOfWorkers.Remove(worker);
        }

        public List<string> GetAllTeams()
        {
            var allTeams = new List<string>();
            foreach(Worker worker in this.ListOfWorkers)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database;
using VolunteerSystem.Model;
using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystem.Exceptions;

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
            if (emailNotUsedBefore(worker.Email) == true)
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
            else
                throw new EmailUsedBeforeException($"The email {worker.Email} is already in use with another account");
        }

        private bool emailNotUsedBefore(string email)
        {
            if (email == "")
                return true;
            else if (ListOfWorkers.Exists(x => x.Email == email))
            {
                return false;
            }
            else
                return true;
        }

        public void UpdateVolunteer(Volunteer volunteer, string name, string email, string accosi, int phonenumber, string password)
        {
            volunteer.Update(name, email, accosi, phonenumber, password);
            _database.volunteer.Update(volunteer);
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
            ListOfWorkers.AddRange(_database.volunteer.GetVolunteersFromDatabase());
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

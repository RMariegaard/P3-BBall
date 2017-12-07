﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database;
using VolunteerSystem.Model;

namespace VolunteerSystem.Function
{
    public class WorkerController
    {
        private FinalController _database;
        private List<Worker> _ListOfWorkers;

        public WorkerController(FinalController database)
        {
            _database = database;
            _ListOfWorkers = UpdateListOfVolunteers();

        }

        public void CreateWorker(Worker worker)
        {
            _ListOfWorkers.Add(worker);
            _database.Complete();
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
            if(worker is Volunteer)
            {
                _database.volunteer.Remove(worker as Volunteer);
            }
            else { _database.externalWorker.Remove(worker as ExternalWorker); }

            _database.Complete();
            _ListOfWorkers = UpdateListOfVolunteers();
        }

        private List<Worker> UpdateListOfVolunteers()
        {
            List<Worker> ListOfWorkers = new List<Worker>();
            ListOfWorkers.AddRange(_database.volunteer.getthembitches());
            ListOfWorkers.AddRange(_database.externalWorker.GetExternalListOfWorkers());
            return ListOfWorkers;
        }

        //public List<string> GetAllTeams()
        //{
        //    var allTeams = new List<string>();
        //    foreach (Worker worker in this.ListOfWorkers)
        //    {
        //        if (worker is Volunteer volunteer)
        //        {
        //            string team = volunteer.Association;
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
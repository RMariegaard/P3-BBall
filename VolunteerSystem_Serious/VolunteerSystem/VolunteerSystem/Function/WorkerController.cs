using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class WorkerController
    {
        private List<Worker> _wokers;
        public List<Worker> Workers
        {
            get
            {
                return _wokers;
            }
        }

        public WorkerController()
        {
            _wokers = new List<Worker>();

            _wokers.Add(new Volunteer("Casper", "AnEmail@gmail.com", "U12 Drenge"));
            _wokers.Add(new Volunteer("Kasper", "AnEmail@gmail.com", "U12 Drenge"));
            _wokers.Add(new Volunteer("Caspar", "AnEmail@gmail.com", "U12 Drenge"));
            _wokers.Add(new Volunteer("mark", "AnEmail@gmail.com", "U12 Drenge"));
            _wokers.Add(new Volunteer("Mustafa", "AnEmail@gmail.com", "U12 Drenge"));
            _wokers.Add(new Volunteer("Emil", "AnEmail@gmail.com", "U12 Drenge"));
            _wokers.Add(new Volunteer("Rasmus", "JegKanIkkeLideSex@NarrePik.fuck", "U12 Drenge"));
            _wokers.Add(new Volunteer("Peter", "AnEmail@gmail.com", "U12 Drenge"));
            _wokers.Add(new Volunteer("Søren", "AnEmail@gmail.com", "U12 Drenge"));
            _wokers.Add(new Volunteer("Krisjan", "AnEmail@gmail.com", "U12 Drenge"));
            _wokers.Add(new Volunteer("Mikkel", "AnEmail@gmail.com", "U12 Drenge"));
        }

        public void CreateWorker()
        {

            throw new NotImplementedException();
        }

        public List<Worker> SearchWorkers()
        {

            throw new NotImplementedException();
        }

        public string ViewWorkerInformation(Worker worker)
        {
            return worker.GetInformation();
        }
    }
}

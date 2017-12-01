using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class WorkerController
    {
        private List<Worker> _workers;
        public List<Worker> Workers
        {
            get
            {
                return _workers;
            }
        }

        public WorkerController()
        {
            _workers = new List<Worker>();
        }

        public void CreateWorker(Worker worker)
        {
            _workers.Add(worker);
        }

        public List<Worker> SearchWorkers(Predicate<Worker> predicate)
        {
            List<Worker> list = _workers.FindAll(predicate);
            return list;
        }

        public string ViewWorkerInformation(Worker worker)
        {
            return worker.GetInformation();
        }

        internal void RemoveWorker(Worker worker)
        {
            _workers.Remove(worker);
        }

        
    }
}

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
        }

        public void CreateWorker(Worker worker)
        {
            _wokers.Add(worker);
        }

        public List<Worker> SearchWorkers()
        {

            throw new NotImplementedException();
        }

        public string ViewWorkerInformation(Worker worker)
        {
            return worker.GetInformation();
        }

        internal void RemoveWorker(Worker worker)
        {
            _wokers.Remove(worker);
        }
    }
}

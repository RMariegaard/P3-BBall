using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem
{
    public class Shift
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private string _task;
        private int _volunteersNeeded;
        private List<Worker> _workers;
        private List<Request> _requests;
        private string _description;

        public bool IsFilled() => NumberOfVolunteers() >= _volunteersNeeded;
        public int NumberOfVolunteers() => _workers.Count;
        public int NumberOfRequests() => _requests.Count;
        

        public Shift(DateTime startTime, DateTime endTime, string task, int volunteersNeeded, string description)
        {
            this._startTime = startTime;
            this._endTime = endTime;
            this._task = task;
            this._volunteersNeeded = volunteersNeeded;
            this._description = description;
            _workers = new List<Worker>();
            _requests = new List<Request>();
        }

        public void EditShift()
        {
            throw new NotImplementedException();
        }

        public void RemoveWorker()
        {
            throw new NotImplementedException();
        }

        public void CreateRequest()
        {
            _requests.Add(new Request());
        }

        public string GetInformation()
        {
            throw new NotImplementedException();
        }
    }
}
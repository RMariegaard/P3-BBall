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
        public DateTime StartTime
        {
            get
            {
                return _startTime;
            }
        }
        private DateTime _endTime;
        public DateTime EndTime
        {
            get
            {
                return _endTime;
            }
        }
        private string _task;
        public string Task
        {
            get
            {
                return _task;
            }
        }
        private int _volunteersNeeded;
        public int VolunteersNeeded
        {
            get
            {
                return _volunteersNeeded;
            }
        }
        private List<Worker> _workers;
        public List<Worker> Workers
        {
            get
            {
                return _workers;
            }
        }

        private List<Request> _requests;
        public IEnumerable<Request> Requests
        {
            get
            {
                return _requests;
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
        }

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

        public void CreateRequest(Volunteer volunteer)
        {
            _requests.Add(new Request(volunteer, this));
        }

        public void RemoveRequest(Request request)
        {
            _requests.Remove(request);
        }

        public string GetInformation()
        {
            throw new NotImplementedException();
        }
        public void ApproveRequest(Request request)
        {
            this.Workers.Add(request.Worker);
            RemoveRequest(request);
        }
        public void DenieRequest(Request request)
        {
            RemoveRequest(request);
        }
    }
}
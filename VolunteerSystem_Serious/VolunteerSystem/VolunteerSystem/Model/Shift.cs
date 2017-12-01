using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace VolunteerSystem
{
    public class Shift : INotifyPropertyChanged
    {
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
        [Key]
        public int ID { get; set; }
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
        public List<Request> Requests
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
        public string GetNumberOfVolunteers { get { return _workers.Count.ToString()+"/"+_volunteersNeeded; } }


        public event PropertyChangedEventHandler PropertyChanged;

        public void EditShift(Shift newShift)
        {
            this._volunteersNeeded = newShift.VolunteersNeeded;
            this._task = newShift.Task;
            this._startTime = newShift._startTime;
            this._endTime = newShift._endTime;
            this._description = newShift.Description;
            PropertyChanged?.Invoke(GetNumberOfVolunteers, new PropertyChangedEventArgs("GetNumberOfVolunteers"));

        }

        public void RemoveWorker(Worker worker)
        {
            _workers.Remove(worker);
            PropertyChanged?.Invoke(GetNumberOfVolunteers, new PropertyChangedEventArgs("GetNumberOfVolunteers"));
        }
        public void AddWorker(Worker worker, int year)
        {
            _workers.Add(worker);
            if (worker is Volunteer)
            {
                ((Volunteer)worker).AddYearWorked(year);
            }
            PropertyChanged?.Invoke(GetNumberOfVolunteers, new PropertyChangedEventArgs("GetNumberOfVolunteers"));
        }
        public void CreateRequest(Volunteer volunteer)
        {
            _requests.Add(new Request(volunteer));
        }

        public void RemoveRequest(Request request)
        {
            _requests.Remove(request);
        }

        public string GetInformation()
        {
            throw new NotImplementedException();
        }
        public void ApproveRequest(Request request, int year)
        {
            Worker worker = request.Worker;
            _workers.Add(worker);

            if(worker is Volunteer)
            {
                ((Volunteer)worker).AddYearWorked(year);
            }
            RemoveRequest(request);
            
            PropertyChanged?.Invoke(GetNumberOfVolunteers, new PropertyChangedEventArgs("GetNumberOfVolunteers"));
        }
        public void DenieRequest(Request request)
        {
            RemoveRequest(request);
        }

        public string TimeInterval { get
            {
                return _startTime.ToString("HH\\:mm") + "-" + _endTime.ToString("HH\\:mm");
            }
        }


    }
}
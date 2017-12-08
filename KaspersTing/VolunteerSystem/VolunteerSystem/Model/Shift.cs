using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using VolunteerSystem.Database;


namespace VolunteerSystem
{
    public class Shift : INotifyPropertyChanged
    {
        public Shift(string task, string descrip, int volunteersneeded)
        {
            this.VolunteersNeeded = volunteersneeded;
            this.Task = task;
            this.Description = descrip;
            ListOfWorkers = new List<Worker>();
            ListOfRequests = new List<Request>();
           // ShiftDatabase.Add(this);
            //ShiftDatabase.Complete();
        }

        public Shift(DateTime startTime, DateTime endTime, string task, int volunteersNeeded, string description)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Task = task;
            this.VolunteersNeeded = volunteersNeeded;
            this.Description = description;
            this.ListOfWorkers = new List<Worker>();
            this.ListOfRequests = new List<Request>();
        }
        public Shift()
        {
            ListOfWorkers = new List<Worker>();
            //this.StartTime = DateTime.Parse(this.StringStartTime);
            //this.EndTime = DateTime.Parse(this.StringEndTime);
            ListOfRequests = new List<Request>();
            

        }


        [Key]
        public int ShiftId { get; private set; }

        [ForeignKey("Schedule")]
        public int? ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public string Task { get; private set; }
        public string Description { get; private set; }
        [NotMapped]
        public string StringStartTime { get; set; }
        [NotMapped]
        public string StringEndTime { get; set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public int VolunteersNeeded { get; set; }
        public List<Worker> ListOfWorkers { get; set; }
        public List<Request> ListOfRequests { get; set; }

        //not properties
        public int NumberOfVolunteers() => ListOfWorkers.Count;
        public bool IsFilled() => NumberOfVolunteers() >= VolunteersNeeded;
        public int NumberOfListOfRequests() => ListOfRequests.Count;

        [NotMapped]
        public string GetNumberOfVolunteers { get { return ListOfWorkers.Count.ToString() + "/" + VolunteersNeeded; } }

        public void EditShift(Shift newShift)
        {
            this.VolunteersNeeded = newShift.VolunteersNeeded;
            this.Task = newShift.Task;
            this.StartTime = newShift.StartTime;
            this.EndTime = newShift.EndTime;
            this.Description = newShift.Description;
            PropertyChanged?.Invoke(GetNumberOfVolunteers, new PropertyChangedEventArgs("GetNumberOfVolunteers"));

        }


        public void AddWorker(Worker worker, int year)
        {
            ListOfWorkers.Add(worker);
            //if (worker is Volunteer)
            //{
            //    ((Volunteer)worker).AddYearWorked(year);
            //}
            PropertyChanged?.Invoke(GetNumberOfVolunteers, new PropertyChangedEventArgs("GetNumberOfVolunteers"));
        }
        public void CreateRequest(Volunteer volunteer)
        {
            ListOfRequests.Add(new Request(volunteer));
        }

        public void RemoveRequest(Request request)
        {
            ListOfRequests.Remove(request);
            //ShiftDatabase._context.request.Attach(request);
            //ShiftDatabase._context.request.Remove(request);
            //ShiftDatabase.Complete();
        }

        public void RemoveWorker(Worker worker)
        {
            ListOfWorkers.Remove(worker);
            PropertyChanged?.Invoke(GetNumberOfVolunteers, new PropertyChangedEventArgs("GetNumberOfVolunteers"));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public string TimeInterval
        {
            get
            {
                return StartTime.ToString("HH\\:mm") + "-" + EndTime.ToString("HH\\:mm");
            }
        }

        public void ApproveRequest(Request request, int year)
        {
            Worker worker = request.Volunteer;
            this.ListOfWorkers.Add(worker);
            this.ListOfRequests.Remove(request);

            //ShiftDatabase.UpdateShift(this, worker);
            //ShiftDatabase.RemoveRequest(request);
            

            //if (worker is Volunteer)
            //{
            //    ((Volunteer)worker).AddYearWorked(year);
            //}

            PropertyChanged?.Invoke(GetNumberOfVolunteers, new PropertyChangedEventArgs("GetNumberOfVolunteers"));
        }
        public void DenieRequest(Request request)
        {
            RemoveRequest(request);
        }



    }




    //Test Constructer only used for unit test does not connect to database
    //public Shift(bool test, DateTime startTime, DateTime endTime, string task, int volunteersNeeded, string description)
    //{
    //    this._startTime = startTime;
    //    this._endTime = endTime;
    //    this._task = task;
    //    this._volunteersNeeded = volunteersNeeded;
    //    this._description = description;
    //    _ListOfWorkers = new List<Worker>();
    //    _ListOfRequests = new List<Request>();
    //}






    //public string GetInformation()
    //{
    //    throw new NotImplementedException();
    //}


}

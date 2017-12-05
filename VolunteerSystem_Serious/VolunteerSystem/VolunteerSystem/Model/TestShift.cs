using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using VolunteerSystem.Database;


namespace VolunteerSystem.Model
{
    public class TestShift : INotifyPropertyChanged
    {
        public static ShiftController ShiftDatabase = new ShiftController(new DatabaseContext(SqlDataConnecter.CnnVal("DatabaseCS")));
        public TestShift(string task, string descrip, int volunteersneeded)
        {
            this.VolunteersNeeded = volunteersneeded;
            this.Task = task;
            this.Descripstion = descrip;
            ListOfWorkers = new List<TestWorker>();
            ListOfRequest = new List<TestRequest>();
            ShiftDatabase.Add(this);
            ShiftDatabase.Complete();
        }
        public TestShift()
        {
            ListOfWorkers = new List<TestWorker>();
            //this.StartTime = DateTime.Parse(this.StringStartTime);
            //this.EndTime = DateTime.Parse(this.StringEndTime);
            ListOfRequest = new List<TestRequest>();
            

        }


        [Key]
        public int ShiftId { get; private set; }
        public string Task { get; private set; }
        public string Descripstion { get; private set; }
        [NotMapped]
        public string StringStartTime { get; set; }
        [NotMapped]
        public string StringEndTime { get; set; }
        [NotMapped]
        public DateTime StartTime { get; private set; }
        [NotMapped]
        public DateTime EndTime { get; private set; }
        public int VolunteersNeeded { get; set; }
        public List<TestWorker> ListOfWorkers { get; set; }
        public List<TestRequest> ListOfRequest { get; set; }

        //not properties
        public int NumberOfVolunteers() => ListOfWorkers.Count;
        public bool IsFilled() => NumberOfVolunteers() >= VolunteersNeeded;
        public int NumberOfRequests() => ListOfRequest.Count;

        [NotMapped]
        public string GetNumberOfVolunteers { get { return ListOfWorkers.Count.ToString() + "/" + VolunteersNeeded; } }

        public void EditShift(TestShift newShift)
        {
            this.VolunteersNeeded = newShift.VolunteersNeeded;
            this.Task = newShift.Task;
            this.StartTime = newShift.StartTime;
            this.EndTime = newShift.EndTime;
            this.Descripstion = newShift.Descripstion;
            ShiftDatabase.UpdateShift(this);
            ShiftDatabase.Remove(newShift);
            ShiftDatabase.Complete();
            PropertyChanged?.Invoke(GetNumberOfVolunteers, new PropertyChangedEventArgs("GetNumberOfVolunteers"));

        }


        public void AddWorker(TestWorker worker, int year)
        {
            ListOfWorkers.Add(worker);
            ShiftDatabase.UpdateShift(this);
            ShiftDatabase.Complete();
            if (worker is TestVolunteer)
            {
                ((TestVolunteer)worker).AddYearWorked(year);
            }
            PropertyChanged?.Invoke(GetNumberOfVolunteers, new PropertyChangedEventArgs("GetNumberOfVolunteers"));
        }
        public void CreateRequest(TestVolunteer volunteer)
        {
            ListOfRequest.Add(new TestRequest(volunteer));
            ShiftDatabase.UpdateShift(this);
            ShiftDatabase.Complete();
        }

        public void RemoveRequest(TestRequest request)
        {
            //ListOfRequest.Remove(request);
            //ShiftDatabase._context.request.Attach(request);
            //ShiftDatabase._context.request.Remove(request);
            //ShiftDatabase.Complete();
        }

        public void RemoveWorker(TestWorker worker)
        {
            ListOfWorkers.Remove(worker);
            ShiftDatabase.UpdateShift(this);
            ShiftDatabase.Complete();
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

        public void ApproveRequest(TestRequest request, int year)
        {
            TestWorker worker = request.TestVolunteer;
            this.ListOfWorkers.Add(worker);
            this.ListOfRequest.Remove(request);

            ShiftDatabase.UpdateShift(this);
            ShiftDatabase.Complete();
            //RemoveRequest(request);


            //if (worker is TestVolunteer)
            //{
            //    ((TestVolunteer)worker).AddYearWorked(year);
            //}

            PropertyChanged?.Invoke(GetNumberOfVolunteers, new PropertyChangedEventArgs("GetNumberOfVolunteers"));
        }
        public void DenieRequest(TestRequest request)
        {
            
            RemoveRequest(request);
        }



    }


    //public Shift(DateTime startTime, DateTime endTime, string task, int volunteersNeeded, string description)
    //{
    //    this._startTime = startTime;
    //    this._endTime = endTime;
    //    this._task = task;
    //    this._volunteersNeeded = volunteersNeeded;
    //    this._description = description;
    //    _workers = new List<Worker>();
    //    _requests = new List<Request>();
    //}

    //Test Constructer only used for unit test does not connect to database
    //public Shift(bool test, DateTime startTime, DateTime endTime, string task, int volunteersNeeded, string description)
    //{
    //    this._startTime = startTime;
    //    this._endTime = endTime;
    //    this._task = task;
    //    this._volunteersNeeded = volunteersNeeded;
    //    this._description = description;
    //    _workers = new List<Worker>();
    //    _requests = new List<Request>();
    //}






    //public string GetInformation()
    //{
    //    throw new NotImplementedException();
    //}


}

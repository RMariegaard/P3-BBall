using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database;
using VolunteerSystem.Model;

namespace VolunteerSystem.Function
{
    public class TestScheduleController
    {
        private TestSchedule _schedule;
        //public event UpdateRequestPanelEvent UpdateRequestPanel;
        private FinalController _database;

        public TestScheduleController(TestSchedule schedule, FinalController database)
        {
            _schedule = schedule;
            _database = database;
        }

        //public void CreateSchedule()
        //{
        //    throw new NotImplementedException();
        //}
        public int ScheduleYear()
        {
            return _schedule.Year;
        }

        public List<TestRequest> GetAllRequests()
        {
            List<TestRequest> requests = new List<TestRequest>();
            _schedule.ListOfShifts.ForEach(x => requests.AddRange(x.ListOfRequest));
            return requests;
        }

        //public List<AbstractNotification> GetAllRequestsAndNotifications()
        //{
        //    List<AbstractNotification> notifications = new List<AbstractNotification>();
        //    _schedule.Shifts.ForEach(x => notifications.AddRange(x.Requests));
        //    Notifier.AllNotifications.ForEach(x => notifications.Add(x));

        //    return notifications;
        //}

        //public void RemoveNotification(Notification notification)
        //{
        //    Notifier.AllNotifications.Remove(notification);
        //    //UpdateRequestPanel();
        //}

        public List<TestShift> GetAllShifts()
        {
            return _schedule.ListOfShifts;
        }

        //public List<String> GetAllTasks()
        //{
        //    return _schedule.Tasks;
        //}

        public void CreateShift(TestShift shift)
        {
            _schedule.ListOfShifts.Add(shift);
            _database.schedule.UpdateSchedule(_schedule);
        }

        public void CreateTask(string task)
        {
            _schedule.Tasks.Add(task);
            //_database.schedule.UpdateSchedule(_schedule);
        }
        public void RemoveTask(string task)
        {
            _schedule.Tasks.Remove(task);
           // _database.schedule.UpdateSchedule(_schedule);
        }

        public void EditShift(TestShift oldShift, TestShift newShift)
        {
            string[] changes = FindShiftChanges(oldShift, newShift);
            foreach (var volunteer in oldShift.ListOfWorkers.Where(x => x.GetType() == typeof(Volunteer)))
            {
                //Notifier.InformVolunteer(volunteer as Volunteer, oldShift, newShift, changes);
            }
            oldShift.EditShift(newShift);
            _database.schedule.UpdateSchedule(_schedule);

        }
        public void DeleteShift(TestShift shift)
        {
            foreach (var volunteer in shift.ListOfWorkers.Where(x => x.GetType() == typeof(TestVolunteer)))
            {
                //Notifier.InformVolunteer(volunteer as TestVolunteer, shift, InformShiftCommand.Delete);
            }
            _database.shift.Remove(shift);
            //UpdateRequestPanel();
        }

        private string[] FindShiftChanges(TestShift oldShift, TestShift newShift)
        {
            List<string> changes = new List<string>();

            if (oldShift.StartTime != newShift.StartTime)
            {
                changes.Add("Starttime");
            }
            if (oldShift.EndTime != newShift.EndTime)
            {
                changes.Add("Endtime");
            }
            if (oldShift.Descripstion != newShift.Descripstion)
            {
                changes.Add("Description");
            }
            if (oldShift.GetNumberOfVolunteers != newShift.GetNumberOfVolunteers)
            {
                changes.Add("Number of volunteers");
            }
            if (oldShift.Task != newShift.Task)
            {
                changes.Add("Task");
            }

            return changes.ToArray();
        }

        internal void RemoveTaskAndAssociateShifts(string taskName)
        {
            GetAllShifts().Where(x => x.Task == taskName).ToList().ForEach(y => DeleteShift(y));
            RemoveTask(taskName);
            _database.Complete();

        }

        public void AddWorkerToShift(TestShift shift, TestWorker worker)
        {
            shift?.AddWorker(worker, ScheduleYear());
            _database.schedule.UpdateSchedule(_schedule);
        }
        //virker
        public void RemoveWorkerFromShift(TestWorker worker, TestShift shift)
        {
            shift.RemoveWorker(worker);
           _database.schedule.UpdateSchedule(_schedule);
        }

        public void RemoveWorkerFromAllHisShifts(TestWorker worker)
        {
            GetAllShifts().ForEach(x => x.ListOfWorkers.RemoveAll(y => y == worker));
            _database.schedule.UpdateSchedule(_schedule);
            //UpdateRequestPanel();
        }

        public void RemoveAllRequestsForAWorker(TestWorker worker)
        {
            foreach (TestRequest request in GetAllRequests())
                if (request.TestVolunteer == worker)
                    RemoveRequest(request);
            //UpdateRequestPanel();
        }

        public void RemoveRequest(TestRequest request)
        {
            _database.request.RemoveRequest(request);
        }
        public void ViewShiftInformation()
        {
            throw new NotImplementedException();
        }

        public List<TestShift> ViewMyShifts(TestVolunteer volunteer)
        {
            throw new NotImplementedException();
        }

        //public void SendRequest()
        //{
        //    throw new NotImplementedException();
        //}

        public void ApproveRequest(TestRequest request)
        {
            _schedule.ListOfShifts.Find(x => x.ListOfRequest.Contains(request)).ApproveRequest(request, 2);
            _database.schedule.UpdateSchedule(_schedule);
            _database.request.Remove(request);
            

            // Notifier.InformVolunteer(request.TestVolunteer as TestVolunteer, shift, InformShiftCommand.Accept);
        }

        public void DenyRequest(TestRequest request)
        {
            _database.request.Remove(request);
            //Notifier.InformVolunteer(request.Worker as Volunteer, shift, InformShiftCommand.Deny);
        }

        //public Shift FindSingleShift(Predicate<Shift> predicate)
        //{
        //    return _schedule.Shifts.Find(predicate);
        //}
    }
}

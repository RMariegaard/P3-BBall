using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database;
using VolunteerSystem.Model;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystem
{
    public class ScheduleController
    {
        private Schedule _schedule;
        //public event UpdateRequestPanelEvent UpdateRequestPanel;
        private IFinalController  _database;

        public ScheduleController(Schedule schedule, IFinalController database)
        {
            _schedule = schedule;
            _database = database;
        }

        public ScheduleController(Schedule schedule)
        {
            _schedule = schedule;
        }

        public List<AbstractNotification> GetAllRequestsAndNotifications()
        {
            List<AbstractNotification> notifications = new List<AbstractNotification>();
           // _schedule.ListOfShifts.ForEach(x => notifications.AddRange(x.ListOfRequests));
           notifications.AddRange(_database.request.GetAllRequest());
            Notifier.AllNotifications.ForEach(x => notifications.Add(x));

            return notifications;
        }
        //public void CreateSchedule()
        //{
        //    throw new NotImplementedException();
        //}
        public int ScheduleYear()
        {
            return _schedule.Year;
        }

        public List<Request> GetAllListOfRequests()
        {
            List<Request> ListOfRequests = new List<Request>();
            _schedule.ListOfShifts.ForEach(x => ListOfRequests.AddRange(x.ListOfRequests));
            return ListOfRequests;
        }

        //public List<AbstractNotification> GetAllListOfRequestsAndNotifications()
        //{
        //    List<AbstractNotification> notifications = new List<AbstractNotification>();
        //    _schedule.ListOfShifts.ForEach(x => notifications.AddRange(x.ListOfRequests));
        //    Notifier.AllNotifications.ForEach(x => notifications.Add(x));

        //    return notifications;
        //}

        public void RemoveNotification(Notification notification)
        {
            Notifier.AllNotifications.Remove(notification);
            //UpdateRequestPanel();
        }

        public List<Shift> GetAllListOfShifts()
        {
            return _schedule.ListOfShifts;
        }

        public List<String> GetAllTasks()
        {
            return _schedule.Tasks;
        }

        public void CreateShift(Shift shift)
        {
            _schedule.ListOfShifts.Add(shift);
            _database.shift.Add(shift);
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

        public void EditShift(Shift oldShift, Shift newShift)
        {
            string[] changes = FindShiftChanges(oldShift, newShift);
            foreach (var volunteer in oldShift.ListOfWorkers.Where(x => x.GetType() == typeof(Volunteer)))
            {
                //Notifier.InformVolunteer(volunteer as Volunteer, oldShift, newShift, changes);
            }
            oldShift.EditShift(newShift);
            _database.schedule.UpdateSchedule(_schedule);

        }
        public void DeleteShift(Shift shift)
        {
            //Both Volunteers on the shift and volunteers who have requested the shift have to be informed!!!!!
            foreach (var volunteer in shift.ListOfWorkers.Where(x => x.GetType() == typeof(Volunteer)))
            {
                //Notifier.InformVolunteer(volunteer as Volunteer, shift, InformShiftCommand.Delete);
            }
            foreach(var volunteer in shift.ListOfRequests.Select(x => x.Volunteer))
            {
                //Notifier.InformVolunteer(volunteer, shift, InformShiftCommand.Deny)
            }
            _database.request.RemoveRange(shift.ListOfRequests);
            _database.shift.Remove(shift);
            //UpdateRequestPanel();
        }

        private string[] FindShiftChanges(Shift oldShift, Shift newShift)
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
            if (oldShift.Description != newShift.Description)
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

        internal void RemoveTaskAndAssociateListOfShifts(string taskName)
        {
            GetAllListOfShifts().Where(x => x.Task == taskName).ToList().ForEach(y => DeleteShift(y));
            RemoveTask(taskName);
            _database.Complete();

        }

        public void AddWorkerToShift(Shift shift, Worker worker)
        {
            shift?.AddWorker(worker, ScheduleYear());
            _database.schedule.UpdateSchedule(_schedule);
        }
        //virker
        public void RemoveWorkerFromShift(Worker worker, Shift shift)
        {
            shift.RemoveWorker(worker);
           _database.schedule.UpdateSchedule(_schedule);
        }

        public void RemoveWorkerFromAllHisListOfShifts(Worker worker)
        {
            if(worker is Volunteer)
            {
                _database.volunteer.Remove(worker as Volunteer);
            }
            //GetAllListOfShifts().ForEach(x => x.ListOfWorkers.RemoveAll(y => y == worker));
            _database.schedule.UpdateSchedule(_schedule);
            //UpdateRequestPanel();
        }

        public void RemoveAllListOfRequestsForAWorker(Worker worker)
        {


            foreach (Request request in GetAllListOfRequests())
                if (request.Volunteer == worker)
                    RemoveRequest(request);
            //UpdateRequestPanel();
        }

        public void RemoveRequest(Request request)
        {
            _database.request.RemoveRequest(request);
        }
        public void ViewShiftInformation()
        {
            throw new NotImplementedException();
        }

        public List<Shift> ViewMyListOfShifts(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }

        //public void SendRequest()
        //{
        //    throw new NotImplementedException();
        //}

        public void ApproveRequest(Request request)
        {
            _schedule.ListOfShifts.Find(x => x.ListOfRequests.Contains(request)).ApproveRequest(request, 2);
            _database.schedule.UpdateSchedule(_schedule);
            _database.request.RemoveRequest(request);
            

            // Notifier.InformVolunteer(request.Volunteer as Volunteer, shift, InformShiftCommand.Accept);
        }

        public void DenyRequest(Request request)
        {
            _database.request.RemoveRequest(request);
            //Notifier.InformVolunteer(request.Worker as Volunteer, shift, InformShiftCommand.Deny);
        }

        public Shift FindSingleShift(Predicate<Shift> predicate)
        {
            return _schedule.ListOfShifts.Find(predicate);
        }
    }
}

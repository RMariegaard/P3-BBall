using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.UserInterfaceAdmin;
using System.Threading;

namespace VolunteerSystem
{
    public class ScheduleController
    {
        private Schedule _schedule;
        public event UpdateRequestPanelEvent UpdateRequestPanel;

        public ScheduleController(Schedule schedule)
        {
            _schedule = schedule;
        }
        
        public void CreateSchedule()
        {
            throw new NotImplementedException();
        }
        public int ScheduleYear()
        {
            return _schedule.Year;
        }

        public List<Request> GetAllRequests()
        {
            List<Request> requests = new List<Request>();
            _schedule.Shifts.ForEach(x => requests.AddRange(x.Requests));
            return requests;
        }

        public List<AbstractNotification> GetAllRequestsAndNotifications()
        {
            List<AbstractNotification> notifications = new List<AbstractNotification>();
            _schedule.Shifts.ForEach(x => notifications.AddRange(x.Requests));
            Notifier.AllNotifications.ForEach(x => notifications.Add(x));

            return notifications;
        }

        public List<Shift> GetAllShifts()
        {
            return _schedule.Shifts;
        }

        public List<String> GetAllTasks()
        {
            return _schedule.Tasks;
        }

        public void CreateShift(Shift shift)
        {
            _schedule.Shifts.Add(shift);
        }

        public void CreateTask(string task)
        {
            _schedule.Tasks.Add(task);
        }
        public void RemoveTask(string task)
        {
            _schedule.Tasks.Remove(task);
        }

        public void EditShift(Shift oldShift, Shift newShift)
        {
            string[] changes = FindShiftChanges(oldShift, newShift);
            foreach (var volunteer in oldShift.Workers.Where(x => x.GetType() == typeof(Volunteer))){
                Notifier.InformVolunteer(volunteer as Volunteer, oldShift, newShift, changes);
            }
            oldShift.EditShift(newShift);

        }
        public void DeleteShift(Shift shift)
        {
            foreach (var volunteer in shift.Workers.Where(x => x.GetType() == typeof(Volunteer)))
            {
                Notifier.InformVolunteer(volunteer as Volunteer, shift, InformShiftCommand.Delete);
            }
            _schedule.Shifts.Remove(shift);
            UpdateRequestPanel();
        }
        
        private string[] FindShiftChanges(Shift oldShift, Shift newShift)
        {
            List<string> changes = new List<string>();

            if(oldShift.StartTime != newShift.StartTime)
            {
                changes.Add("Starttime");
            }
            if(oldShift.EndTime != newShift.EndTime)
            {
                changes.Add("Endtime");
            }
            if(oldShift.Description != newShift.Description)
            {
                changes.Add("Description");
            }
            if(oldShift.GetNumberOfVolunteers != newShift.GetNumberOfVolunteers)
            {
                changes.Add("Number of volunteers");
            }
            if(oldShift.Task != newShift.Task)
            {
                changes.Add("Task");
            }
            
            return changes.ToArray();
        }

        internal void RemoveTaskAndAssociateShifts(string taskName)
        {
            GetAllShifts().Where(x => x.Task == taskName).ToList().ForEach(y => DeleteShift(y));
            RemoveTask(taskName);
        }

        public void AddWorkerToShift(Shift shift, Worker worker)
        {
            shift?.AddWorker(worker, ScheduleYear());
        }
        
        public void RemoveWorkerFromShift(Worker worker, Shift shift)
        {
            shift.RemoveWorker(worker);
        }

        public void RemoveWorkerFromAllHisShifts(Worker worker)
        {
            GetAllShifts().ForEach(x => x.Workers.RemoveAll(y => y == worker));
            UpdateRequestPanel();
        }
        
        public void RemoveAllRequestsForAWorker(Worker worker)
        {
            foreach (Request request in GetAllRequests())
                if (request.Worker == worker)
                    RemoveRequest(request);
            UpdateRequestPanel();
        }

        public void RemoveRequest(Request request)
        {
            Shift shift = GetAllShifts().Find(x => x.Requests.Contains(request));
            shift.RemoveRequest(request);
        }
        public void ViewShiftInformation()
        {
            throw new NotImplementedException();
        }

        public List<Shift> ViewMyShifts(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }
        
        public void SendRequest()
        {
            throw new NotImplementedException();
        }
        public void ApproveRequest(Request request)
        {
           Shift shift = GetAllShifts().Find(x => x.Requests.Contains(request));
            shift.ApproveRequest(request, ScheduleYear());

            
            Notifier.InformVolunteer(request.Worker as Volunteer, shift, InformShiftCommand.Accept);
        }
        public void DenyRequest(Request request)
        {
            Shift shift = GetAllShifts().Find(x => x.Requests.Contains(request));
            shift.DenieRequest(request);
            Notifier.InformVolunteer(request.Worker as Volunteer, shift, InformShiftCommand.Deny);
        }
        
        public Shift FindSingleShift(Predicate<Shift> predicate)
        {
            return _schedule.Shifts.Find(predicate);
        }
    }
}

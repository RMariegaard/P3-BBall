using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretP3
{
    public class Shift
    {
        public DateTime Time;
        public List<Volunteer> Volunteers;
        public int VolunteersNeeded;
        public string Task;
        public Dictionary<Volunteer, Request> Requests;
        private bool _filled => Volunteers.Count() == VolunteersNeeded;
        public string Description;
        public int LengthInHours;


        public Shift(DateTime time, int volunteersNeeded, string task)
        {
            Volunteers = new List<Volunteer>();
            Requests = new Dictionary<Volunteer, Request>();
            UpdateShift(time);
            UpdateShift(volunteersNeeded);
            UpdateShift(task);
        }

        public string ViewRequests()
        {
            string res = "Requests:\n";
            foreach (var x in Requests.Values)
            {
                res += x.Volunteer.ToString() + "\n";
            }

            return res;
        }


        public void RequestShift(Volunteer volunteer)
        {
            if (!_filled)
                Requests.Add(volunteer, new Request(volunteer));
         //   else 
         //       throw(new ShiftFilledException(this));
        }

        public void AcceptRequest(Request request)
        {
            AddVolunteer(request.Volunteer);
            Requests.Remove(request.Volunteer);
            request.Volunteer.Messages.Add("You have been added to " + this.ToString());
        }


        public bool RemoveVolunteer(Volunteer volunteer) {
            ShiftUpdated -= volunteer.ShiftUpdated;
            return Volunteers.Remove(volunteer);
        }

        public void AddVolunteer(Volunteer volunteer)
        {
            Volunteers.Add(volunteer);
            this.ShiftUpdated += volunteer.ShiftUpdated;
        }


        public void UpdateShift(DateTime time, int volunteersNeeded, string task)
        {
            UpdateShift(time);
            UpdateShift(volunteersNeeded);
            UpdateShift(task);
            ShiftUpdated(this);
            
        }
        public event ShiftUpdated ShiftUpdated;

        public void UpdateShift(DateTime time)
        {
            Time = time;
        }

        public void UpdateShift(int volunteersNeeded)
        {
            VolunteersNeeded = volunteersNeeded;
        }

        public void UpdateShift(string task)
        {
            Task = task;
        }

        public string Title => Task + Time.ToString();

    }
}

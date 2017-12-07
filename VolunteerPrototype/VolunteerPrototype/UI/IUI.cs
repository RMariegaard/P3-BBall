using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem;

namespace VolunteerPrototype.UI
{
    public interface IUI
    {
        string GetCurrentUser { get; }

        ScheduleController ScheduleController();
        WorkerController WorkerController();
        void UpdateSchedulePanel();
        void UpdateMenu();

        int GetFullWidth();
        bool IsLoggedIn();
        void LogIn();
        void LogOut();
    }
}

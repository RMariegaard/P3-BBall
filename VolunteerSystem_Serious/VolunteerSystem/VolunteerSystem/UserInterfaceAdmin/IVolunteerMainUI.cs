using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem.UserInterfaceAdmin
{
    interface IVolunteerMainUI
    {
        void DisplayPopup(string header, string body);
        ScheduleController GetController();
        void DisplayHomepage();
        void DisplayVolunteerOverview();
        void DisplaySettings();
        void DisplayGeneralError(string body);
        Shift DisplayCreateNewShift();
        string DisplayCreateNewTask();
        void Start();
        void UpdateUI();
    }
}

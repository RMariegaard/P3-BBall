using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin
{
    public interface IVolunteerMainUI
    {
        void UpdateSchedule();
        void UpdateAllHomepage();
        void HomepageChangeDay(string day);
        void DisplayPopup(string header, string body);

        void UpdateButtonsLeftSide();
        ScheduleController GetScheduleController();
        WorkerController GetWorkController();
        void DisplayHomepage();
        void DisplayVolunteerOverview();
        void DisplayVolunteerOnHomepage(Volunteer volunteer);
        void DisplayVolunteerInVolunteerOverview(Volunteer volunteer);
        void DisplaySettings();
        void AcceptWorkerRequest(Request request);
        void DenyWorkerRequest(Request request);
        void DisplayGeneralError(string body);
        void DisplayPressedOnShift(Shift shift);
        Size GetFullClientWindowSize();
        void DisplayCreateNewShift();
        void DisplayCreateNewTask();
        void Start();
        void UpdateUI();
        void ScrollToControlOnSchedule(Control control);
    }
}

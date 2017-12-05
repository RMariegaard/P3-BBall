using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolunteerSystem;
using VolunteerSystem.UserInterfaceAdmin;

namespace VolunteerPrototype.UI
{
    class MainUI : Form, IVolunteerMainUI
    {
        private ScheduleController _scheduleController;
        private WorkerController _workerController;
        public Panel MainUIPanel;


        private Size _fullClientWindowSize;

        public MainUI(ScheduleController sc, WorkerController wc)
        {
            _scheduleController = sc;
            _workerController = wc;

            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            

            _fullClientWindowSize = new Size(RectangleToScreen(this.ClientRectangle).Size.Width, RectangleToScreen(this.ClientRectangle).Height);
            MainUIPanel = new Panel()
            {
                Size = _fullClientWindowSize
            };


            Controls.Add(new VolunteerSystem.UserInterfaceAdmin.Homepage.SchedulePanelElements.Schedule(this, ))

            Controls.Add(MainUIPanel);
        }

        public void AcceptWorkerRequest(Request request)
        {
            throw new NotImplementedException();
        }

        public void DenyWorkerRequest(Request request)
        {
            throw new NotImplementedException();
        }

        public void DisplayCreateNewShift()
        {
            throw new NotImplementedException();
        }

        public void DisplayCreateNewTask()
        {
            throw new NotImplementedException();
        }

        public void DisplayGeneralError(string body)
        {
            throw new NotImplementedException();
        }

        public void DisplayHomepage()
        {
            throw new NotImplementedException();
        }

        public void DisplayPopup(string header, string body)
        {
            throw new NotImplementedException();
        }

        public void DisplayPressedOnShift(Shift shift)
        {
          //  throw new NotImplementedException();
        }

        public void DisplaySettings()
        {
            throw new NotImplementedException();
        }

        public void DisplayVolunteerInVolunteerOverview(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }

        public void DisplayVolunteerOnHomepage(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }

        public void DisplayVolunteerOverview()
        {
            throw new NotImplementedException();
        }

        public Size GetFullClientWindowSize()
        {
            throw new NotImplementedException();
        }

        public ScheduleController GetScheduleController()
        {
            return _scheduleController;
        }

        public WorkerController GetWorkController()
        {
            throw new NotImplementedException();
        }

        public void HomepageChangeDay(string day)
        {
            throw new NotImplementedException();
        }

        public void ScrollToControlOnSchedule(Control control)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void UpdateAllHomepage()
        {
           // throw new NotImplementedException();
        }

        public void UpdateButtonsLeftSide()
        {
           // throw new NotImplementedException();
        }

        public void UpdateSchedule()
        {
            throw new NotImplementedException();
        }

        public void UpdateUI()
        {
            throw new NotImplementedException();
        }
    }
}

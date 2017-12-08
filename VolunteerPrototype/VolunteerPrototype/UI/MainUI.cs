﻿using System;
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
    class MainUI : Form, IUI
    {
        private ScheduleController _scheduleController;
        private WorkerController _workerController;
        private ScheduleUI _scheduleUI;
        private DayNavigation _dayNavigation;

        private Panel _schedulePanel;
        private Panel _dayNavigationPanel;
        private MenuUI _menu;
        private MyShiftsPanel _myShiftsPanel;

        private Volunteer _user;

        public Volunteer GetCurrentUser => _user;

        public MainUI(ScheduleController sc, WorkerController wc)
        {
            _scheduleController = sc;
            _workerController = wc;

            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            _dayNavigation = new DayNavigation(this);

            UpdateSchedulePanel();
            _menu = new MenuUI(this, _schedulePanel.Width + _dayNavigationPanel.Width + 54)
            {
                Location = new Point(0, 0),
                Height = 100
            };
            Controls.Add(_menu);

        }

        public int GetFullWidth()
        {
            return Width;
        }

        public ScheduleController ScheduleController()
        {
            return _scheduleController;
        }

        public void UpdateMenu()
        {

        }

        public void UpdateSchedulePanel()
        {
            Controls.Remove(_schedulePanel);
            Controls.Remove(_dayNavigationPanel);
            _dayNavigationPanel = _dayNavigation.GetPanel(new Size(100, this.Height));
            Controls.Add(_dayNavigationPanel);


            _scheduleUI = new ScheduleUI(_dayNavigation._selected.Text, this);

            _schedulePanel = new Panel()
            {
                Location = new Point(_dayNavigationPanel.Width, 100),
                Size = new Size((this.Size.Width / 100) * 70 - 2, 25*24 + 45),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right,
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            _schedulePanel.Controls.Add(_scheduleUI.GetPanel(_schedulePanel));
            Controls.Add(_schedulePanel);
            _myShiftsPanel?.Hide();
        }

        public WorkerController WorkerController()
        {
            return _workerController;
        }

        public bool IsLoggedIn()
        {

            return _user != null;

        }

        public void LogIn(Volunteer volunteer)
        {
            _user = volunteer;
            _myShiftsPanel = new MyShiftsPanel(_user, _schedulePanel.Size)
            {
                Location = _schedulePanel.Location
            };
            Controls.Add(_myShiftsPanel);
        }

        public void LogOut()
        {
            
            _user = null;

        }

        public void ShowMyShifts()
        {
            _myShiftsPanel.Show();
            _schedulePanel.Hide();
            _dayNavigationPanel.Hide();
        }
    }
}

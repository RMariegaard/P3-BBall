﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterface
{
    public partial class TheMainWindow : Form, UserInterfaceAdmin.IVolunteerMainUI
    {
        private Panel _mainPanel;
        private Panel _menuButtonPanel;
       
        Size fullClientWindowSize;

        private UserInterfaceAdmin.Homepage.Homepage _homepage;
        private UserInterfaceAdmin.VolunteerOverview.VolunteerOverview _volunteerOverview;

        enum ShownPage { Homepage, VolunteerOverview, Settings };
        ShownPage shownPage;

        public ScheduleController ScheduleController;
        WorkerController WorkerController;
        public TheMainWindow(ScheduleController scheduleController, WorkerController workerController)
        {
            InitializeComponent();
            Width = 1600;
            Height = 800;

            WorkerController = workerController;
            ScheduleController = scheduleController;

            fullClientWindowSize = RectangleToScreen(this.ClientRectangle).Size;

            int buttonPanelHeight = 50;

            _volunteerOverview = new UserInterfaceAdmin.VolunteerOverview.VolunteerOverview(this, workerController);
            _homepage = new UserInterfaceAdmin.Homepage.Homepage(this);

            shownPage = new ShownPage();

            //Button panel
            _menuButtonPanel = new Panel
            {
                Name = "_menuButtonPanel",
                Size = new Size(fullClientWindowSize.Width - 10, buttonPanelHeight),
                Location = new Point(5, 5),
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left
            };

            //Buttons
            Button _homepageButton = new Button()
            {
                Location = new Point(0, 0),
                Text = "Homepage",
                Size = new Size(_menuButtonPanel.Size.Width/3, _menuButtonPanel.Size.Height),
                FlatStyle = FlatStyle.Flat
            };
            _homepageButton.Click += _homepageButton_Clicked;
            Button _volunteerOverviewButton = new Button()
            {
                Location = new Point(_homepageButton.Location.X + _homepageButton.Size.Width, 0),
                Text = "Volunteer Overview",
                Size = new Size(_menuButtonPanel.Size.Width / 3, _menuButtonPanel.Size.Height),
                FlatStyle = FlatStyle.Flat
            };
            _volunteerOverviewButton.Click += _volunteerOverviewButton_Clicked;
            Button _settingsButton = new Button()
            {
                Location = new Point(_volunteerOverviewButton.Location.X + _volunteerOverviewButton.Size.Width, 0),
                Text = "Settings",
                Size = new Size(_menuButtonPanel.Size.Width / 3, _menuButtonPanel.Size.Height),
                FlatStyle = FlatStyle.Flat
            };
            _settingsButton.Click += _settingsButton_Clicked;

            _menuButtonPanel.Controls.Add(_homepageButton);
            _menuButtonPanel.Controls.Add(_volunteerOverviewButton);
            _menuButtonPanel.Controls.Add(_settingsButton);
            
            //Main panel - schedule
            _mainPanel = new Panel
            {
                Name = "_mainPanel",
                Size = new Size(fullClientWindowSize.Width - 10, fullClientWindowSize.Height - (_menuButtonPanel.Location.Y + _menuButtonPanel.Height) - 10),
                Location = new Point(5, _menuButtonPanel.Location.Y + _menuButtonPanel.Height + 5),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };

            //Start on homepage
            shownPage = ShownPage.Homepage;
            UpdateUI();

            //Adds to the window
            Controls.Add(_mainPanel);
            Controls.Add(_menuButtonPanel);
        }
        
        private void _homepageButton_Clicked(object sender, EventArgs e)
        {
            shownPage = ShownPage.Homepage;
            UpdateUI();
        }
        private void _volunteerOverviewButton_Clicked(object sender, EventArgs e)
        {
            shownPage = ShownPage.VolunteerOverview;
            UpdateUI();
        }
        private void _settingsButton_Clicked(object sender, EventArgs e)
        {
            shownPage = ShownPage.Settings;
            UpdateUI();
        }

        public void Start()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }

        public void DisplayPopup(string Header, string body)
        {
            PopupUI popup = new PopupUI(Header, body);
            popup.Show();
        }

        public void DisplayGeneralError(string errorBody)
        {
            PopupUI popup = new PopupUI("A general error occurred", errorBody);
            popup.Show();
        }

        public void DisplayHomepage()
        {
            _mainPanel.Controls.Add(_homepage.GetHomepagePanel(_mainPanel));
        }
        public void UpdateSchedule()
        {
            _homepage.UpdateSchedulePanel();
        }

        public void DisplaySettings()
        {
            throw new NotImplementedException();
        }

        public void DisplayVolunteerOverview()
        {
            _mainPanel.Controls.Add(_volunteerOverview.GetPanel(_mainPanel.Size));
        }
        
        public void AcceptWorkerRequest(Request request)
        {
            Shift shiftToedit = ScheduleController.FindSingleShift(x => x.Requests.Contains(request));
            //Approve it

            ScheduleController.ApproveRequest(request);
            //_homepage.UpdateShiftPanel(shiftToedit);
            //Update ui




            //Approve it
            // ScheduleController.ApproveRequest(request);

            //Update ui
            //_homepage.UpdateSchedulePanel();
            _homepage.UpdatePendingRequestPanel();
            if (_homepage.ShownVolunteer != null)
                if (_homepage.ShownVolunteer.Name == request.Worker.Name)
                    _homepage.UpdateVolunteerPanel();
        }

        public void DenyWorkerRequest(Request request)
        {
            //Deny it
            ScheduleController.DenyRequest(request);

            //Update ui
            _homepage.UpdatePendingRequestPanel();
            if (_homepage.ShownVolunteer != null)
                if (_homepage.ShownVolunteer.Name == request.Worker.Name)
                    _homepage.UpdateVolunteerPanel();
        }

        public void DisplayCreateNewShift()
        {
            CreateNewShiftUI createNewShiftUIPopup = new CreateNewShiftUI(ScheduleController.GetAllTasks());
            createNewShiftUIPopup.ShowDialog();
            if (createNewShiftUIPopup.DialogResult == DialogResult.OK)
            {
                ScheduleController.CreateShift(createNewShiftUIPopup.Result);
                _homepage.UpdateSchedulePanel();
            }
        }

        public void DisplayCreateNewTask()
        {
            CreateNewTaskUI createNewTaskUIPopup = new CreateNewTaskUI();
            createNewTaskUIPopup.ShowDialog();
            if (createNewTaskUIPopup.DialogResult == DialogResult.OK)
            {
                ScheduleController.CreateTask(createNewTaskUIPopup.Result);
                _homepage.UpdateSchedulePanel();
            }
        }

        public void UpdateUI()
        {
            _mainPanel.Controls.Clear();

            switch (shownPage)
            {
                case ShownPage.Homepage:
                    DisplayHomepage();
                    break;
                case ShownPage.VolunteerOverview:
                    DisplayVolunteerOverview();
                    break;
                case ShownPage.Settings:
                    DisplaySettings();
                    break;
            }
        }

        public ScheduleController GetScheduleController()
        {
            return ScheduleController;
        }

        public void DisplayPressedOnShift(Shift shift)
        {
            UserInterfaceAdmin.PressedOnShiftPopup pressedOnShiftPopup = new UserInterfaceAdmin.PressedOnShiftPopup(shift, this);
            pressedOnShiftPopup.ShowDialog();
        }

        public void DisplayVolunteerOnHomepage(Volunteer volunteer)
        {
            _homepage.ShownVolunteer = volunteer;
            _homepage.UpdateVolunteerPanel();
        }

        public void DisplayVolunteerInVolunteerOverview(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }

        public Size GetFullClientWindowSize()
        {
            return fullClientWindowSize;
        }

        public void ScrollToControlOnSchedule(Control control)
        {
            //_homepage.selectedDay = ;
            _homepage.schedulePanel.ScrollControlIntoView(control);
        }
    }
}

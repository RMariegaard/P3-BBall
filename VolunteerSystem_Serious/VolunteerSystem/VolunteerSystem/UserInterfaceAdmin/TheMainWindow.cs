using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolunteerSystem.UserInterfaceAdmin;
using VolunteerSystem.UserInterfaceAdmin.Homepage;

namespace VolunteerSystem.UserInterface
{
    public partial class TheMainWindow : Form, IVolunteerMainUI
    {
        private Panel _mainPanel;
        private Panel _menuButtonPanel;

        Button _homepageButton;
        Button _volunteerOverviewButton;
        Button _settingsButton;

        Size fullClientWindowSize;
        
        private Homepage _homepage;
        private UserInterfaceAdmin.VolunteerOverview.VolunteerOverview _volunteerOverview;
        private UserInterfaceAdmin.Settings.Settings _settings;

        enum ShownPage { Homepage, VolunteerOverview, Settings };
        ShownPage shownPage;

        public ScheduleController ScheduleController;
        WorkerController WorkerController;
        public TheMainWindow(ScheduleController scheduleController, WorkerController workerController)
        {
            InitializeComponent();
            //Width = 1600;
            //Height = 800;
            //this.MinimumSize = new Size(1200 ,800);

            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            WorkerController = workerController;
            ScheduleController = scheduleController;

            fullClientWindowSize = new Size(RectangleToScreen(this.ClientRectangle).Size.Width, RectangleToScreen(this.ClientRectangle).Height);


            int buttonPanelHeight = 50;

            _volunteerOverview = new UserInterfaceAdmin.VolunteerOverview.VolunteerOverview(this, workerController, ScheduleController);
            _homepage = new UserInterfaceAdmin.Homepage.Homepage(this);
            _settings = new UserInterfaceAdmin.Settings.Settings(this);

            shownPage = new ShownPage();

            //Start on homepage
            shownPage = ShownPage.Homepage;

            //Button panel
            _menuButtonPanel = new Panel
            {
                Name = "_menuButtonPanel",
                Size = new Size(fullClientWindowSize.Width - 10, buttonPanelHeight),
                Location = new Point(5, 5),
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left
            };

            //Buttons
            _homepageButton = new Button()
            {
                Location = new Point(0, 0),
                Text = "Homepage",
                Size = new Size(_menuButtonPanel.Size.Width / 3, _menuButtonPanel.Size.Height),
                FlatStyle = FlatStyle.Flat,
                BackColor = ColorAndStyle.PrimaryColor()
            };
            _homepageButton.Click += _homepageButton_Clicked;
            _volunteerOverviewButton = new Button()
            {
                Location = new Point(_homepageButton.Location.X + _homepageButton.Size.Width, 0),
                Text = "Volunteer Overview",
                Size = new Size(_menuButtonPanel.Size.Width / 3, _menuButtonPanel.Size.Height),
                FlatStyle = FlatStyle.Flat
            };
            _volunteerOverviewButton.Click += _volunteerOverviewButton_Clicked;
            _settingsButton = new Button()
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
            
            //Adds to the window
            Controls.Add(_mainPanel);
            Controls.Add(_menuButtonPanel);

            //event
            scheduleController.UpdateRequestPanel += delegate ()
            {
                _homepage.UpdateTheRequestAndNotificationElements();
            };
            this.ResizeEnd += RezieHomepage;
        }

        private void RezieHomepage(object sender, EventArgs e)
        {
            UpdateSchedule();
        }

        private void _homepageButton_Clicked(object sender, EventArgs e)
        {
            if (shownPage != ShownPage.Homepage)
            {
                shownPage = ShownPage.Homepage;
                UpdateUI();
            }
        }
        private void _volunteerOverviewButton_Clicked(object sender, EventArgs e)
        {
            if (shownPage != ShownPage.VolunteerOverview) {
                shownPage = ShownPage.VolunteerOverview;
                UpdateUI();
            }
        }
        private void _settingsButton_Clicked(object sender, EventArgs e)
        {
            if (shownPage != ShownPage.Settings)
            {
                shownPage = ShownPage.Settings;
                UpdateUI();
            }
        }

        public void Start()
        {
            Application.EnableVisualStyles();

            _mainPanel.Controls.Add(_homepage.GetHomepagePanel(_mainPanel));
            _mainPanel.Controls.Add(_volunteerOverview.GetPanel(_mainPanel.Size));
            _mainPanel.Controls.Add(_settings.GetPanel(_mainPanel.Size));
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
            _volunteerOverview.GetPanel(_mainPanel.Size).Visible = false;
            _settings.GetPanel(_mainPanel.Size).Visible = false;
        }
        public void UpdateSchedule()
        {
            _homepage.UpdateSchedulePanel();
        }
        public void UpdateAllHomepage()
        {
            _homepage.UpdateAllPanels();
        }
        public void DisplaySettings()
        {
            _settings.GetPanel(_mainPanel.Size).BringToFront();
            _settings.GetPanel(_mainPanel.Size).Visible = true;
        }

        public void DisplayVolunteerOverview()
        {
            _volunteerOverview.GetPanel(_mainPanel.Size).BringToFront();
            _volunteerOverview.GetPanel(_mainPanel.Size).Visible = true;

        }
        
        public void AcceptWorkerRequest(Request request)
        {

            ScheduleController.ApproveRequest(request);

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

            _homepageButton.BackColor = Color.White;
            _volunteerOverviewButton.BackColor = Color.White;
            _settingsButton.BackColor = Color.White;

            switch (shownPage)
            {
                case ShownPage.Homepage:
                    DisplayHomepage();
                    _homepageButton.BackColor = ColorAndStyle.PrimaryColor();
                    break;
                case ShownPage.VolunteerOverview:
                    DisplayVolunteerOverview();
                    _volunteerOverviewButton.BackColor = ColorAndStyle.PrimaryColor();
                    break;
                case ShownPage.Settings:
                    DisplaySettings();
                    _settingsButton.BackColor = ColorAndStyle.PrimaryColor();
                    break;
            }
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
            _volunteerOverview.SelectedWorker = volunteer;
            shownPage = ShownPage.VolunteerOverview;
            UpdateUI();
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
        public void HomepageChangeDay(string day)
        {
            _homepage.ChangeButtonSelected(day);
        }

        public ScheduleController GetScheduleController()
        {
            return ScheduleController;
        }


        public WorkerController GetWorkController()
        {
            return WorkerController;
        }

        public void UpdateButtonsLeftSide()
        {
            _homepage.UpdateButtonsLeftSide(0 , 120);
        }
    }
}

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

namespace VolunteerSystem.UserInterface
{
    public partial class TheMainWindow : Form, UserInterfaceAdmin.IVolunteerMainUI
    {
        private Panel _mainPanel;
        private Panel _menuButtonPanel;

        Button _homepageButton;
        Button _volunteerOverviewButton;
        Button _settingsButton;

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
            _homepageButton = new Button()
            {
                Location = new Point(0, 0),
                Text = "Homepage",
                Size = new Size(_menuButtonPanel.Size.Width/3, _menuButtonPanel.Size.Height),
                FlatStyle = FlatStyle.Flat
            };
            _homepageButton.Click += _homepageButton_Clicked;
            _homepageButton.Paint += ColorAndStyle.OnPaintDrawRect;
            _homepageButton.Region = new Region(ColorAndStyle.GetRoundRectGP(new Point(0, 0), _homepageButton.Size, 10));
            _volunteerOverviewButton = new Button()
            {
                Location = new Point(_homepageButton.Location.X + _homepageButton.Size.Width, 0),
                Text = "Volunteer Overview",
                Size = new Size(_menuButtonPanel.Size.Width / 3, _menuButtonPanel.Size.Height),
                FlatStyle = FlatStyle.Flat
            };
            _volunteerOverviewButton.Click += _volunteerOverviewButton_Clicked;
            _volunteerOverviewButton.Paint += ColorAndStyle.OnPaintDrawRect;
            _volunteerOverviewButton.Region = new Region(ColorAndStyle.GetRoundRectGP(new Point(0, 0), _volunteerOverviewButton.Size, 10));
            _settingsButton = new Button()
            {
                Location = new Point(_volunteerOverviewButton.Location.X + _volunteerOverviewButton.Size.Width, 0),
                Text = "Settings",
                Size = new Size(_menuButtonPanel.Size.Width / 3, _menuButtonPanel.Size.Height),
                FlatStyle = FlatStyle.Flat
            };
            _settingsButton.Click += _settingsButton_Clicked;
            _settingsButton.Paint += ColorAndStyle.OnPaintDrawRect;
            _settingsButton.Region = new Region(ColorAndStyle.GetRoundRectGP(new Point(0, 0), _settingsButton.Size, 10));

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
            _homepage.GetHomepagePanel(_mainPanel).BringToFront();
            //_homepage.GetHomepagePanel(_mainPanel).Visible = true;
            //_volunteerOverview.GetPanel(_mainPanel.Size).Visible = false;
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
            //_volunteerOverview.GetPanel(_mainPanel.Size).Visible = true;
            //_homepage.GetHomepagePanel(_mainPanel).Visible = false;
            _volunteerOverview.GetPanel(_mainPanel.Size).BringToFront();

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

            _homepageButton.BackColor = Color.White;
            _volunteerOverviewButton.BackColor = Color.White;
            _settingsButton.BackColor = Color.White;

            switch (shownPage)
            {
                case ShownPage.Homepage:
                    DisplayHomepage();
                    _homepageButton.BackColor = Color.Yellow;
                    break;
                case ShownPage.VolunteerOverview:
                    DisplayVolunteerOverview();
                    _volunteerOverviewButton.BackColor = Color.Yellow;
                    break;
                case ShownPage.Settings:
                    DisplaySettings();
                    _settingsButton.BackColor = Color.Yellow;
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

using System;
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

        public ScheduleController _controller;
        public TheMainWindow(ScheduleController controller)
        {
            InitializeComponent();
            Width = 1600;
            Height = 800;

            _controller = controller;

            fullClientWindowSize = RectangleToScreen(this.ClientRectangle).Size;

            int buttonPanelHeight = 50;

            _homepage = new UserInterfaceAdmin.Homepage.Homepage(this);

            //Button panel
            _menuButtonPanel = new Panel();
            _menuButtonPanel.Size = new Size(fullClientWindowSize.Width - 10, buttonPanelHeight);
            _menuButtonPanel.Location = new Point(5, 5);
            _menuButtonPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;

            //Main panel - schedule
            _mainPanel = new Panel();
            _mainPanel.Size = new Size(fullClientWindowSize.Width - 10, fullClientWindowSize.Height - (_menuButtonPanel.Location.Y + _menuButtonPanel.Height) - 10);
            _mainPanel.Location = new Point(5, _menuButtonPanel.Location.Y + _menuButtonPanel.Height + 5);
            _mainPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            //Start on homepage
            DisplayHomepage();

            //Adds to the window
            Controls.Add(_mainPanel);
            Controls.Add(_menuButtonPanel);
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

        public void DisplaySettings()
        {
            throw new NotImplementedException();
        }

        public void DisplayVolunteerOverview()
        {
            throw new NotImplementedException();
        }
        
        public Shift DisplayCreateNewShift()
        {
            CreateNewShiftUI createNewShiftUIPopup = new CreateNewShiftUI(_controller.GetAllTasks());
            createNewShiftUIPopup.ShowDialog();
            if (createNewShiftUIPopup.DialogResult == DialogResult.OK)
                return createNewShiftUIPopup.Result;
            else
                return null;
        }

        public string DisplayCreateNewTask()
        {
            CreateNewTaskUI createNewTaskUIPopup = new CreateNewTaskUI();
            createNewTaskUIPopup.ShowDialog();
            if (createNewTaskUIPopup.DialogResult == DialogResult.OK)
                return createNewTaskUIPopup.Result;
            else
                return null;
        }

        public void UpdateUI()
        {
            _mainPanel.Controls.Clear();
            DisplayHomepage();
        }

        public ScheduleController GetController()
        {
            return _controller;
        }

        public void DisplayPressedOnShift(Shift shift)
        {
            throw new NotImplementedException();
        }

        public void DisplayVolunteerOnHomepage(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }
    }
}

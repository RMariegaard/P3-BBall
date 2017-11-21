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
    public partial class TheMainWindow : Form
    {
        private Panel _mainPanel;
        private Panel _menuButtonPanel;

        Size fullClientWindowSize;

        Button createNewShiftButton;
        Button createNewTaskbutton;

        private ScheduleController _controller;
        public TheMainWindow(ScheduleController controller)
        {
            InitializeComponent();
            Width = 1600;
            Height = 800;

            _controller = controller;

            fullClientWindowSize = RectangleToScreen(this.ClientRectangle).Size;

            int buttonPanelHeight = 50;

            //Button panel
            _menuButtonPanel = new Panel();
            _menuButtonPanel.Size = new Size(fullClientWindowSize.Width - 10, buttonPanelHeight);
            _menuButtonPanel.Location = new Point(5, 5);
            _menuButtonPanel.BackColor = Color.Green;
            _menuButtonPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;

            //Main panel
            _mainPanel = new Panel();
            _mainPanel.Size = new Size(fullClientWindowSize.Width - 10, fullClientWindowSize.Height - (_menuButtonPanel.Location.Y + _menuButtonPanel.Height) - 10);
            _mainPanel.Location = new Point(5, _menuButtonPanel.Location.Y + _menuButtonPanel.Height + 5);
            _mainPanel.BackColor = Color.Blue;
            _mainPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            //Test Buttons
            createNewShiftButton = new Button();
            createNewShiftButton.Text = "Create Shift";
            createNewShiftButton.Location = new Point(10, 10);
            createNewShiftButton.AutoSize = true;
            createNewShiftButton.Click += createShift_Clicked;

            createNewTaskbutton = new Button();
            createNewTaskbutton.Text = "Create Task";
            createNewTaskbutton.Location = new Point(10, 50);
            createNewTaskbutton.AutoSize = true;
            createNewTaskbutton.Click += createTask_Clicked;
            
            //Adds to main panel
            _mainPanel.Controls.Add(createNewTaskbutton);
            _mainPanel.Controls.Add(createNewShiftButton);
            //Adds to the window
            Controls.Add(_mainPanel);
            Controls.Add(_menuButtonPanel);
        }

        public void createShift_Clicked(object sender, EventArgs e)
        {
            Shift shift = DisplayCreateNewShift();

            if (shift != null)
                _controller.CreateShift(shift);
            else
                DisplayPopup("Error", "Could not create a new shift.. ");
            
        }

        public void createTask_Clicked(object sender, EventArgs e)
        {
            string task = DisplayCreateNewTask();

            if (task != null)
                _controller.CreateTask(task);
            else
                DisplayPopup("Error", "Could not create a new task.. ");
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}

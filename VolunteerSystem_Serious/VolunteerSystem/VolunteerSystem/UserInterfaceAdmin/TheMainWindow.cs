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
        public TheMainWindow()
        {
            InitializeComponent();
            Width = 1600;
            Height = 800;
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
            CreateNewShiftUI createNewShiftUIPopup = new CreateNewShiftUI(null);
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

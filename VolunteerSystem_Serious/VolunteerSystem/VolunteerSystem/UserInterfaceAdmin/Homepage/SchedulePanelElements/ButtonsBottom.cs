using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage.SchedulePanelElements
{
    class ButtonsBottom
    {
        IVolunteerMainUI _mainWindowUI;
        Panel _mainPanel;

        public ButtonsBottom(IVolunteerMainUI mainWindowUI)
        {
            _mainWindowUI = mainWindowUI;
            _mainPanel = new Panel();
        }

        public Panel GetPanel()
        {
            Button createShiftButton = new Button();
            createShiftButton.Location = new Point(0, 0);
            createShiftButton.Text = "Create Shift";
            createShiftButton.AutoSize = true;
            createShiftButton.Click += createShift_Clicked;

            Button createTaskButton = new Button();
            createTaskButton.Location = new Point(createShiftButton.Location.X + createShiftButton.Size.Width, 0);
            createTaskButton.Text = "Create Task";
            createTaskButton.AutoSize = true;
            createTaskButton.Click += createTask_Clicked;

            _mainPanel.Controls.Add(createShiftButton);
            _mainPanel.Controls.Add(createTaskButton);
            return _mainPanel;
        }

        public void createShift_Clicked(object sender, EventArgs e)
        {
            Shift shift = _mainWindowUI.DisplayCreateNewShift();

            if (shift != null)
            {
                _mainWindowUI.GetController().CreateShift(shift);
                _mainWindowUI.UpdateUI();
            }
            else
                _mainWindowUI.DisplayPopup("Error", "Could not create a new shift.. ");

        }

        public void createTask_Clicked(object sender, EventArgs e)
        {
            string task = _mainWindowUI.DisplayCreateNewTask();

            if (task != null)
            {
                _mainWindowUI.GetController().CreateTask(task);
                _mainWindowUI.UpdateUI();
            }
            else
                _mainWindowUI.DisplayPopup("Error", "Could not create a new task.. ");
        }
    }
}

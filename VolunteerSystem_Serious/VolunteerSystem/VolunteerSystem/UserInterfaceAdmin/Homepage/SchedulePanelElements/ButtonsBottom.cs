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
        Panel _buttonsatBottomMainPanel;
        Homepage _homepage;

        public ButtonsBottom(IVolunteerMainUI mainWindowUI, Homepage homepage)
        {
            _mainWindowUI = mainWindowUI;
            _buttonsatBottomMainPanel = new Panel();
            _buttonsatBottomMainPanel.Name = "_buttonsatBottomMainPanel";
            _homepage = homepage;
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

            _buttonsatBottomMainPanel.Controls.Add(createShiftButton);
            _buttonsatBottomMainPanel.Controls.Add(createTaskButton);
            return _buttonsatBottomMainPanel;
        }

        public void createShift_Clicked(object sender, EventArgs e)
        {
            _mainWindowUI.DisplayCreateNewShift();
        }

        public void createTask_Clicked(object sender, EventArgs e)
        {
            _mainWindowUI.DisplayCreateNewTask();
        }
    }
}

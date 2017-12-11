﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage.SchedulePanelElements
{
    public class ButtonsBottom
    {
        IVolunteerMainUI _mainWindowUI;
        Panel _buttonsatBottomMainPanel;
        Homepage _homepage;

        public ButtonsBottom(IVolunteerMainUI mainWindowUI, Homepage homepage)
        {
            _mainWindowUI = mainWindowUI;
            _buttonsatBottomMainPanel = new Panel
            {
                Name = "_buttonsatBottomMainPanel",
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            _homepage = homepage;
        }

        public Panel GetPanel()
        {
            Button createShiftButton = new Button
            {
                Location = new Point(0, 0),
                Text = "Create Shift",
                Size = new Size(70, 40),
                AutoSize = true,
            };
            createShiftButton.Click += CreateShift_Clicked;

            Button createTaskButton = new Button
            {
                Location = new Point(createShiftButton.Location.X + createShiftButton.Size.Width + 10, 0),
                Text = "Create Task",
                Size = new Size(70, 40),
                AutoSize = true
            };
            createTaskButton.Click += CreateTask_Clicked;

            _buttonsatBottomMainPanel.Controls.Add(createShiftButton);
            _buttonsatBottomMainPanel.Controls.Add(createTaskButton);
            return _buttonsatBottomMainPanel;
        }

        public void CreateShift_Clicked(object sender, EventArgs e)
        {
            _mainWindowUI.DisplayCreateNewShift();
        }

        public void CreateTask_Clicked(object sender, EventArgs e)
        {
            _mainWindowUI.DisplayCreateNewTask();
        }
    }
}

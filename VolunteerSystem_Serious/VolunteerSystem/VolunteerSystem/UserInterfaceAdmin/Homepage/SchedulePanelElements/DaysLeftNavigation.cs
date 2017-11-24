using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage.SchedulePanelElements
{
    class DaysLeftNavigation
    {
        Panel _daysLeftNavigationMainPanel;
        IVolunteerMainUI mainWindowUI;
        Homepage _homepage;
        public DaysLeftNavigation(IVolunteerMainUI mainWindowUI, Homepage homepage)
        {
            _homepage = homepage;
            this.mainWindowUI = mainWindowUI;
        }

        public Panel GetPanel(Size forRefferece)
        {
            _daysLeftNavigationMainPanel = new Panel();
            _daysLeftNavigationMainPanel.Name = "_daysLeftNavigationMainPanel";
            int heightOfButtons = 40;
            _daysLeftNavigationMainPanel.Size = forRefferece;

            for (int i = 0; i < _homepage.days.Count; i++)
            {
                Button tempButton = new Button();
                tempButton.TabStop = false;
                tempButton.Text = _homepage.days[i];
                tempButton.FlatStyle = FlatStyle.Flat;
                if (_homepage.selectedDay != null && _homepage.days[i] == _homepage.selectedDay.Text)
                {
                    tempButton.BackColor = Color.Gray;
                    tempButton.Size = new Size(forRefferece.Width, heightOfButtons);
                    tempButton.Location = new Point(0, (i * heightOfButtons) + 2);
                }
                else if (_homepage.selectedDay == null)
                {
                    //Goes inhere if there is no button already picked, this means it picks the first button
                    tempButton.BackColor = Color.Gray;
                    tempButton.Size = new Size(forRefferece.Width, heightOfButtons);
                    tempButton.Location = new Point(0, (i * heightOfButtons) + 2);
                    _homepage.selectedDay = tempButton;

                }
                else
                {
                    tempButton.BackColor = Color.White;
                    tempButton.Size = new Size(forRefferece.Width - 10, heightOfButtons);
                    tempButton.Location = new Point(10, (i * heightOfButtons) + 2);
                }
                tempButton.Click += buttonClicked;
                _daysLeftNavigationMainPanel.Controls.Add(tempButton);
            }

            return _daysLeftNavigationMainPanel;
        }

        private void buttonClicked(object sender, EventArgs e)
        {
            Button pressedButton = (Button)sender;

            //Edit the selected
            _homepage.selectedDay = pressedButton;

            //Update UI
            _homepage.UpdateSchedulePanel();
        }
    }
}

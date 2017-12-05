using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerPrototype.UI
{
    public class DayNavigation
    {
        Panel _daysLeftNavigationMainPanel;
        private IUI _mainUI;
        public Button _selected;

        public DayNavigation(IUI mainUI)
        {
            _mainUI = mainUI;
        }

        public Panel GetPanel(Size forRefferece)
        {
            _daysLeftNavigationMainPanel = new Panel();
            _daysLeftNavigationMainPanel.Name = "_daysLeftNavigationMainPanel";
            _daysLeftNavigationMainPanel.Location = new Point(0, 100);
            int heightOfButtons = 40;
            _daysLeftNavigationMainPanel.Size = forRefferece;
            List<string> days = new List<string>();
            List<string> startdays = _mainUI.ScheduleController().GetAllShifts().Select(x => x.StartTime.ToShortDateString()).ToList();
            List<string> endDays = _mainUI.ScheduleController().GetAllShifts().Select(x => x.StartTime.ToShortDateString()).ToList();
            foreach(string day in startdays)
            {
                if (!days.Contains(day))
                {
                    days.Add(day);
                }
            }
            foreach(string day in endDays)
            {
                if (!days.Contains(day))
                {
                    days.Add(day);
                }
            }


            for (int i = 0; i < days.Count; i++)
            {
                Button tempButton = new Button();
                tempButton.Name = days[i];
                tempButton.TabStop = false;
                tempButton.Text = days[i];
                tempButton.FlatStyle = FlatStyle.Flat;
                if (_selected != null && days[i] == _selected.Text)
                {
                    tempButton.BackColor = Color.Gray;
                    tempButton.Size = new Size(forRefferece.Width, heightOfButtons);
                    tempButton.Location = new Point(0, (i * heightOfButtons) + 2);
                }
                else if (_selected == null)
                {
                    //Goes inhere if there is no button already picked, this means it picks the first button
                    tempButton.BackColor = Color.Gray;
                    tempButton.Size = new Size(forRefferece.Width, heightOfButtons);
                    tempButton.Location = new Point(0, (i * heightOfButtons) + 2);
                    _selected = tempButton;

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
           _selected = pressedButton;

            //Update UI
            _mainUI.UpdateSchedulePanel();
        }
    }
}

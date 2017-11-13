using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecretP3;

namespace VolunteerSystem_ForFun.HomepageUIStuff.SchedulePanelUIStuff
{
    class TheSchedule
    {
        Panel _mainPanel;
        private IVolunteerSystem _system;
        string day;
        int hourHeight;

        public TheSchedule(IVolunteerSystem system, string Day)
        {
            _system = system;
            _mainPanel = new Panel();
            day = Day;

            hourHeight = 50; //schedulePanel.Size.Height / 23;
        }

        public Panel GetPanel(Panel schedulePanel)
        {
            _mainPanel.Size = schedulePanel.Size;
            _mainPanel.Location = new Point(0, 0);

            //Adds Tasks and shifts
            int widthOfTask = 100;
            List<Shift> AllShifts = _system.GetShifts();
            List<string> Tasks = _system.GetTasks();
            for (int i = 0; i < Tasks.Count; i++)
            {
                _mainPanel.Controls.Add(TasksPanels.GetATaskPanel(Tasks[i], AllShifts.Where(x => x.Task == Tasks[i] && x.Time.DayOfWeek.ToString() == day).ToList(), new Size(widthOfTask, schedulePanel.Height), new Point((i * (widthOfTask + 5)) + 50, 0), hourHeight));
            }

            //Alternating color:
            List<Panel> alternatingColor = new List<Panel>();
            for (int i = 0; i < 24; i++)
            {
                Panel temp = new Panel();
                temp.Size = new Size(_mainPanel.Size.Width, hourHeight);
                temp.Location = new Point(0, (hourHeight) * i);
                if (i % 2 == 0)
                    temp.BackColor = Color.FromArgb(200, Color.Gray);
                else
                    temp.BackColor = Color.FromArgb(200, Color.HotPink);
                temp.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;

                
                Label tempLabel = new Label();
                tempLabel.Location = new Point(2, 2);
                tempLabel.Text = i + 1 >= 10 ? $"{i + 1}.00" : $"0{i + 1}.00";
                tempLabel.AutoSize = true;

                temp.Controls.Add(tempLabel);

                alternatingColor.Add(temp);
                _mainPanel.Controls.Add(temp);
            }

            _mainPanel.AutoScroll = true;
            return _mainPanel;
        }
    }
}

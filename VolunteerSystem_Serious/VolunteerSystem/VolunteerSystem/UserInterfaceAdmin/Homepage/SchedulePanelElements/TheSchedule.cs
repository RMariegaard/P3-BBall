using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage.SchedulePanelElements
{
    class TheSchedule
    {
        Panel _mainPanel;
        IVolunteerMainUI _mainWindowUI;
        string day;
        int hourHeight;

        public TheSchedule(IVolunteerMainUI mainWindowUI, string Day)
        {
            _mainWindowUI = mainWindowUI;
            _mainPanel = new Panel();
            day = Day;

            hourHeight = 50; //schedulePanel.Size.Height / 23;
        }

        public Panel GetPanel(Panel schedulePanel)
        {
            _mainPanel.Size = schedulePanel.Size;
            _mainPanel.Location = new Point(0, 0);
            _mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            //Adds Tasks and shifts
            int widthOfTask = 100;
            List<Shift> AllShifts = _mainWindowUI.GetController().GetAllShifts();
            List<string> Tasks = _mainWindowUI.GetController().GetAllTasks();
            for (int i = 0; i < Tasks.Count; i++)
            {
                TasksPanels tempTaskPanel = new TasksPanels();
                
                //Create
                _mainPanel.Controls.Add(tempTaskPanel.GetATaskPanel(Tasks[i], AllShifts.Where(x => x.Task == Tasks[i] && x.StartTime.DayOfWeek.ToString() == day).ToList(), new Size(widthOfTask, schedulePanel.Height), new Point((i * (widthOfTask + 5)) + 50, 0), hourHeight));
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
                    temp.BackColor = Color.FromArgb(200, Color.LightGray);
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

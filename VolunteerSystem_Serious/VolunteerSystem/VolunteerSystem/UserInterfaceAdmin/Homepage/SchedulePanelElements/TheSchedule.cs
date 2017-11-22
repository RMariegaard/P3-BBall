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
        Panel test;

        public TheSchedule(IVolunteerMainUI mainWindowUI, string day)
        {
            _mainWindowUI = mainWindowUI;
            _mainPanel = new Panel();
            _mainPanel.BorderStyle = BorderStyle.FixedSingle;
            test = new Panel();
            this.day = day;
            hourHeight = 50; //schedulePanel.Size.Height / 23

            //Add Numbers to Panel. this needs only to be done once
            for (int i = 0; i < 24; i++)
            {
                Label tempLabel = new Label();
                tempLabel.Location = new Point(0, (i * hourHeight));
                tempLabel.Text = i + 1 >= 10 ? $"{i + 1}.00" : $"0{i + 1}.00";
                tempLabel.AutoSize = true;
                test.Controls.Add(tempLabel);
            }
        }

        public Panel GetPanel(Panel schedulePanel)
        {
            _mainPanel.Size = schedulePanel.Size;
            _mainPanel.Location = new Point(0, 0);
            _mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _mainPanel.AutoScroll = true;
            
            test.Location = new Point(0, 0);
            test.Size = new Size(schedulePanel.Size.Width-20, 24 * hourHeight);
            test.Paint += alternatingColors_Paint;
            
            //Adds Tasks and shifts
            int widthOfTask = 100;
            List<Shift> AllShifts = _mainWindowUI.GetController().GetAllShifts();
            List<string> Tasks = _mainWindowUI.GetController().GetAllTasks();
            for (int i = 0; i < Tasks.Count; i++)
            {
                TasksPanels tempTaskPanel = new TasksPanels(_mainWindowUI);

                //Create
                test.Controls.Add(tempTaskPanel.GetATaskPanel(Tasks[i], AllShifts.Where(x => x.Task == Tasks[i] && x.StartTime.DayOfWeek.ToString() + " " + x.StartTime.ToShortDateString() == day).ToList(), new Size(widthOfTask, test.Height), new Point((i * (widthOfTask + 5)) + 50, 0), hourHeight));
            }

            _mainPanel.Controls.Add(test);
            return _mainPanel;
        }

        private void alternatingColors_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 24; i++)
            {
                Rectangle rc = _mainPanel.ClientRectangle;
                rc.Size = new Size(_mainPanel.Size.Width-10, hourHeight);
                rc.Location = new Point(0, (i*hourHeight));
                Brush brush1 = new SolidBrush(Color.LightGray);
                Brush brush2 = new SolidBrush(Color.Gray);

                if (i%2 == 0)
                    e.Graphics.FillRectangle(brush1, rc);
                else
                    e.Graphics.FillRectangle(brush2, rc);
            }
        }
    }
}

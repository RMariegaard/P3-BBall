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
        Panel _theSchedulemainPanel;
        IVolunteerMainUI _mainWindowUI;
        string day;
        int hourHeight;
        Panel colorAndShiftPanel;

        public TheSchedule(IVolunteerMainUI mainWindowUI, Button day)
        {
            _mainWindowUI = mainWindowUI;
            _theSchedulemainPanel = new Panel();
            _theSchedulemainPanel.Name = "_theSchedulemainPanel";
            _theSchedulemainPanel.BorderStyle = BorderStyle.FixedSingle;
            colorAndShiftPanel = new Panel();
            colorAndShiftPanel.Name = "colorAndShiftPanel";
            this.day = day.Text;
            hourHeight = 50; //schedulePanel.Size.Height / 23
            
            //Add Numbers to Panel. this needs only to be done once
            for (int i = 0; i <= 24; i++)
            {
                Label tempLabel = new Label();
                tempLabel.Location = new Point(0, (i * hourHeight));
                tempLabel.Text = i + 1 >= 10 ? $"{i}.00" : $"0{i}.00";
                tempLabel.AutoSize = true;
                colorAndShiftPanel.Controls.Add(tempLabel);
            }
        }

        public Panel GetPanel(Panel schedulePanel)
        {
            _theSchedulemainPanel.Size = schedulePanel.Size;
            _theSchedulemainPanel.Location = new Point(0, 0);
            _theSchedulemainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _theSchedulemainPanel.AutoScroll = true;
            
            colorAndShiftPanel.Location = new Point(0, 0);
            colorAndShiftPanel.Size = new Size(schedulePanel.Size.Width-20, 24 * hourHeight);
            colorAndShiftPanel.Paint += alternatingColors_Paint;
                
            //Adds Tasks and shifts
            int widthOfTask = 100;
            List<Shift> AllShifts = _mainWindowUI.GetScheduleController().GetAllShifts();
            List<string> Tasks = _mainWindowUI.GetScheduleController().GetAllTasks();
            for (int i = 0; i < Tasks.Count; i++)
            {
                TasksPanels tempTaskPanel = new TasksPanels(_mainWindowUI);

                //Create
                colorAndShiftPanel.Controls.Add(tempTaskPanel.GetATaskPanel(Tasks[i], AllShifts.Where(x => x.Task == Tasks[i] && x.StartTime.DayOfWeek.ToString() + " " + x.StartTime.ToShortDateString() == day).ToList(), new Size(widthOfTask, colorAndShiftPanel.Height), new Point((i * (widthOfTask + 5)) + 50, 0), hourHeight));
            }

            _theSchedulemainPanel.Controls.Add(colorAndShiftPanel);
            return _theSchedulemainPanel;
        }

        private void alternatingColors_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 24; i++)
            {
                Rectangle rc = _theSchedulemainPanel.ClientRectangle;
                rc.Size = new Size(_theSchedulemainPanel.Size.Width-10, hourHeight);
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

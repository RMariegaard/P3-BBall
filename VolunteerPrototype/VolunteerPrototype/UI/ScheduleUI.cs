using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using VolunteerSystem;
using VolunteerSystem.UserInterfaceAdmin.Homepage.SchedulePanelElements;

namespace VolunteerPrototype.UI
{
    public class ScheduleUI
    {
        Panel _theSchedulemainPanel;
        string _day;
        int hourHeight;
        Panel colorAndShiftPanel;
        IUI _mainUI;

        public ScheduleUI(string day, IUI mainUI)
        {
            _mainUI = mainUI;    
            _theSchedulemainPanel = new Panel
            {
                Name = "_theSchedulemainPanel",
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoScroll = true
            };
            colorAndShiftPanel = new Panel
            {
                Name = "colorAndShiftPanel",
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom,
                AutoScroll = true
            };
            if (day != null)
            {
                this._day = day;
            }

            hourHeight = 25;

            //Add Numbers to Panel. this needs only to be done once
            for (int i = 1; i <= 24; i++)
            {
                Label tempLabel = new Label
                {
                    Location = new Point(0, (i * hourHeight)),
                    Text = i > 10 ? $"{i - 1}.00" : $"0{i - 1}.00",
                    AutoSize = true
                };
                colorAndShiftPanel.Controls.Add(tempLabel);
            }

        }

        public Panel GetPanel(Panel schedulePanel)
        {
            _theSchedulemainPanel.Size = schedulePanel.Size;
            _theSchedulemainPanel.Location = schedulePanel.Location;
            _theSchedulemainPanel.AutoScroll = true;
            _theSchedulemainPanel.Dock = DockStyle.Fill;
            //_theSchedulemainPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            
            colorAndShiftPanel.Location = schedulePanel.Location;
            colorAndShiftPanel.Size = new Size(schedulePanel.Size.Width-20, 25 * hourHeight);
            colorAndShiftPanel.Paint += _alternatingColors_Paint;
            colorAndShiftPanel.Dock = DockStyle.Fill;    

            //Adds Tasks and shifts
            List<Shift> AllShifts = _mainUI.ScheduleController().GetAllListOfShifts();
            List<string> Tasks = _mainUI.ScheduleController().GetAllTasks();

            int widthOfTask;
            if (Tasks.Count() != 0)
                widthOfTask = (_theSchedulemainPanel.Size.Width / Tasks.Count()) - 20;
            else
                widthOfTask = 100;

            for (int i = 0; i < Tasks.Count; i++)
            {
                TaskPanel tempTaskPanel = new TaskPanel(_mainUI);

                List<Shift> toBeOnPanel = new List<Shift>();
                foreach(Shift shift in AllShifts)
                {
                    if (shift.Task == Tasks[i])
                    {
                        DateTime tempDateTime = new DateTime(shift.StartTime.Year, shift.StartTime.Month, shift.StartTime.Day, 0, 0, 0);
                        do
                        {
                            if ((tempDateTime == ConvertDayStringFromButtonToDateTime(_day)))
                            {
                                toBeOnPanel.Add(shift);
                                break;
                            }
                            tempDateTime = tempDateTime.AddDays(1);
                        } while (tempDateTime < shift.EndTime);
                    }
                }
                
                //Create
                Panel taskPanel = tempTaskPanel.GetATaskPanel(
                    Tasks[i],
                    toBeOnPanel,
                    new Size(widthOfTask, colorAndShiftPanel.Height),
                    new Point((i * (widthOfTask + 5)) + 50, 0),
                    hourHeight,
                    ConvertDayStringFromButtonToDateTime(_day)
                    );
                
                colorAndShiftPanel.Controls.Add(taskPanel);
            }

            _theSchedulemainPanel.Controls.Add(colorAndShiftPanel);
            return _theSchedulemainPanel;
        }

        private DateTime ConvertDayStringFromButtonToDateTime(string day)
        {
            return DateTime.Parse(day);
        }

        private void _alternatingColors_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 25; i++)
            {
                Rectangle rc = _theSchedulemainPanel.ClientRectangle;
                rc.Size = new Size(_theSchedulemainPanel.Size.Width-10, hourHeight);
                rc.Location = new Point(0, (i*hourHeight));
                Brush brush1 = new SolidBrush(Color.AliceBlue);
                Brush brush2 = new SolidBrush(Color.Gray);

                if (i%2 == 0)
                    e.Graphics.FillRectangle(brush1, rc);
                else
                    e.Graphics.FillRectangle(brush2, rc);
            }
        }
    }
}

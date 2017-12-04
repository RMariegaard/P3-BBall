﻿using System;
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
                this.day = day.Text;
            }

            hourHeight = 25;//schedulePanel.Size.Height / 25;

            //Add Numbers to Panel. this needs only to be done once
            for (int i = 1; i <= 25; i++)
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
            _theSchedulemainPanel.Location = new Point(0, 0);
            _theSchedulemainPanel.AutoScroll = true;
            _theSchedulemainPanel.Dock = DockStyle.Fill;
            //_theSchedulemainPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            
            colorAndShiftPanel.Location = new Point(0, 0);
            colorAndShiftPanel.Size = new Size(schedulePanel.Size.Width-20, 25 * hourHeight);
            colorAndShiftPanel.Paint += _alternatingColors_Paint;
            colorAndShiftPanel.Dock = DockStyle.Fill;    

            //Adds Tasks and shifts
            List<Shift> AllShifts = _mainWindowUI.GetScheduleController().GetAllShifts();
            List<string> Tasks = _mainWindowUI.GetScheduleController().GetAllTasks();

            int widthOfTask;
            if (Tasks.Count() != 0)
                widthOfTask = (_theSchedulemainPanel.Size.Width / Tasks.Count()) - 20;
            else
                widthOfTask = 100;

            for (int i = 0; i < Tasks.Count; i++)
            {
                TasksPanels tempTaskPanel = new TasksPanels(_mainWindowUI);

                List<Shift> toBeOnPanel = new List<Shift>();
                foreach(Shift shift in AllShifts)
                {
                    if (shift.Task == Tasks[i])
                    {
                        DateTime tempDateTime = new DateTime(shift.StartTime.Year, shift.StartTime.Month, shift.StartTime.Day, 0, 0, 0);
                        do
                        {
                            if ((tempDateTime.DayOfWeek.ToString() + " " + tempDateTime.ToShortDateString()) == day)
                            {
                                toBeOnPanel.Add(shift);
                                break;
                            }
                            tempDateTime = tempDateTime.AddDays(1);
                        } while (tempDateTime < shift.EndTime);

                        //if (shift.StartTime.DayOfWeek)
                    }
                }
                //&& x.StartTime.DayOfWeek.ToString() + " " + x.StartTime.ToShortDateString() == day).ToList()
                //Create
                Panel taskPanel = tempTaskPanel.GetATaskPanel(
                    Tasks[i],
                    toBeOnPanel,
                    new Size(widthOfTask, colorAndShiftPanel.Height),
                    new Point((i * (widthOfTask + 5)) + 50, 0),
                    hourHeight,
                    convertDayStringFromButtonToDateTime(day)
                    );
                
                colorAndShiftPanel.Controls.Add(taskPanel);
                //colorAndShiftPanel.Controls.Add(tempTaskPanel.GetATaskPanel(Tasks[i], AllShifts.Where(x => x.Task == Tasks[i] && x.StartTime.DayOfWeek.ToString() + " " + x.StartTime.ToShortDateString() == day).ToList(), new Size(widthOfTask, colorAndShiftPanel.Height), new Point((i * (widthOfTask + 5)) + 50, 0), hourHeight));
            }

            _theSchedulemainPanel.Controls.Add(colorAndShiftPanel);
            return _theSchedulemainPanel;
        }

        private DateTime convertDayStringFromButtonToDateTime(string day)
        {
            List<string> dayElemensts = day.Split(' ').Last().Split('/').ToList();
            return new DateTime(int.Parse(dayElemensts[2]), int.Parse(dayElemensts[0]), int.Parse(dayElemensts[1]));
        }

        private void _alternatingColors_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 25; i++)
            {
                Rectangle rc = _theSchedulemainPanel.ClientRectangle;
                rc.Size = new Size(_theSchedulemainPanel.Size.Width-10, hourHeight);
                rc.Location = new Point(0, (i*hourHeight));
                Brush brush1 = new SolidBrush(ColorAndStyle.AlternatingColorsONE());
                Brush brush2 = new SolidBrush(ColorAndStyle.AlternatingColorsTWO());

                if (i%2 == 0)
                    e.Graphics.FillRectangle(brush1, rc);
                else
                    e.Graphics.FillRectangle(brush2, rc);
            }
        }
    }
}

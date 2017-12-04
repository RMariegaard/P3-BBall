﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage
{
    public class Homepage : AccessibleObject
    {
        public Panel _mainHomepagePanel;

        public Panel schedulePanel;
        public Panel pendingRequestPanel;
        public Panel VolunteerPanel;

        public List<string> days;
        public Button selectedDay;
        
        public Volunteer ShownVolunteer;

        private IVolunteerMainUI _mainWindowUI;

        public Homepage(IVolunteerMainUI mainWindowUI)
        {
            _mainWindowUI = mainWindowUI;
            _mainHomepagePanel = new Panel();

            pendingRequestPanel = new Panel();
            VolunteerPanel = new Panel();
            schedulePanel = new Panel();

            _mainHomepagePanel.Name = "_mainHomepagePanel";
            pendingRequestPanel.Name = "pendingRequestPanel";
            VolunteerPanel.Name = "VolunteerPanel";
            schedulePanel.Name = "schedulePanel";
        }

        public Panel GetHomepagePanel(Panel forRefrence)
        {
            _mainHomepagePanel.Size = forRefrence.Size;
            _mainHomepagePanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            _mainHomepagePanel.Controls.Clear();

            //Updates the panels
            UpdateAllPanels();

            return _mainHomepagePanel;
        }

        public void UpdateAllPanels()
        {
            //_mainHomepagePanel.SuspendLayout();
            UpdateSchedulePanel();
            UpdatePendingRequestPanel();
            UpdateVolunteerPanel();
            //_mainHomepagePanel.PerformLayout();
        }

        private void _createDates()
        {
            days = new List<string>();
            foreach (Shift shift in _mainWindowUI.GetScheduleController().GetAllShifts().OrderBy(x => x.StartTime))
            {
                DateTime tempDateTime = new DateTime(shift.StartTime.Year, shift.StartTime.Month, shift.StartTime.Day, 0, 0, 0);

                do
                {
                    if (!days.Contains(tempDateTime.DayOfWeek.ToString() + " " + tempDateTime.ToShortDateString()))
                    {
                        days.Add(tempDateTime.DayOfWeek.ToString() + " " + tempDateTime.ToShortDateString());
                    }
                    tempDateTime = tempDateTime.AddDays(1);
                } while (tempDateTime < shift.EndTime);
            }

        }

        public void UpdateSchedulePanel()
        {
            _mainHomepagePanel.SuspendLayout();
            _mainHomepagePanel.Controls.Remove(schedulePanel);
            

            //Schedule panel
            schedulePanel.Controls.Clear();
            schedulePanel.Location = new Point(0, 0);
            schedulePanel.Size = new Size((_mainHomepagePanel.Size.Width / 100) * 70 - 2, _mainHomepagePanel.Height);
            schedulePanel.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right;
            schedulePanel.AutoScroll = true;
            _threeSchedulePanels();
            _mainHomepagePanel.Controls.Add(schedulePanel);
            _mainHomepagePanel.ResumeLayout();
        }
        public void UpdatePendingRequestPanel()
        {
            _mainHomepagePanel.Controls.Remove(pendingRequestPanel);
            //Pending request panel
            pendingRequestPanel.Controls.Clear();
            pendingRequestPanel.Location = new Point(schedulePanel.Location.X + schedulePanel.Size.Width + 2, 0);
            pendingRequestPanel.Size = new Size(_mainHomepagePanel.Size.Width - schedulePanel.Size.Width, (_mainHomepagePanel.Size.Height / 100) * 70);
            pendingRequestPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            RequestPanelElements.MainRequestPanelElement requestPanel = new RequestPanelElements.MainRequestPanelElement(_mainWindowUI);
            pendingRequestPanel.Controls.Add(requestPanel.GetRequestPanel(pendingRequestPanel.Size));
            _mainHomepagePanel.Controls.Add(pendingRequestPanel);
        }
        public void UpdateVolunteerPanel()
        {
            _mainHomepagePanel.Controls.Remove(VolunteerPanel);
            //Volunteer panel
            VolunteerPanel.Controls.Clear();
            VolunteerPanel.Location = new Point(schedulePanel.Location.X + schedulePanel.Size.Width + 2, pendingRequestPanel.Location.Y + pendingRequestPanel.Size.Height + 2);
            VolunteerPanel.Size = new Size(_mainHomepagePanel.Size.Width - schedulePanel.Size.Width - 2, _mainHomepagePanel.Size.Height - pendingRequestPanel.Height - 2);
            VolunteerPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            VolunteerSmallOverview.VolunteerHomapageOverview volunteerOverviewSmall = new VolunteerSmallOverview.VolunteerHomapageOverview(_mainWindowUI, this);
            VolunteerPanel.Controls.Add(volunteerOverviewSmall.GetPanel(VolunteerPanel.Size));
            _mainHomepagePanel.Controls.Add(VolunteerPanel);
        }

        private void _threeSchedulePanels()
        {
            
            Panel TheSchedule = new Panel();
            Panel ButtonsBottumPanel = new Panel();
            
          
            TheSchedule.Name = "TheSchedule";
            ButtonsBottumPanel.Name = "ButtonsBottumPanel";

  
            schedulePanel.Controls.Remove(TheSchedule);
            schedulePanel.Controls.Remove(ButtonsBottumPanel);

            int leftButtonsWidth = 120;
            int leftButtonsLocationX = 0;

            schedulePanel.Controls.Add(DaysLeft);
            UpdateButtonsLeftSide(leftButtonsLocationX, leftButtonsWidth);

            TheSchedule.Location = new Point( 120, 0);
            TheSchedule.Size = new Size(schedulePanel.Size.Width - 120, schedulePanel.Size.Height - 50);
            TheSchedule.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            SchedulePanelElements.TheSchedule schedule = new SchedulePanelElements.TheSchedule(_mainWindowUI, selectedDay);
            TheSchedule.Controls.Add(schedule.GetPanel(TheSchedule));
            TheSchedule.AutoScroll = true;
            
            
            ButtonsBottumPanel.Location = new Point(  120, TheSchedule.Location.Y + TheSchedule.Size.Height);
            ButtonsBottumPanel.Size = new Size(schedulePanel.Size.Width - 120, schedulePanel.Size.Height - TheSchedule.Size.Height);
            ButtonsBottumPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            SchedulePanelElements.ButtonsBottom buttons = new SchedulePanelElements.ButtonsBottom(_mainWindowUI, this);
            ButtonsBottumPanel.Controls.Add(buttons.GetPanel());

            
            schedulePanel.Controls.Add(TheSchedule);
            schedulePanel.Controls.Add(ButtonsBottumPanel);
        }

        Panel DaysLeft = new Panel();
        public void UpdateButtonsLeftSide(int x, int width)
        {
            _createDates();
            DaysLeft.Invalidate();
            DaysLeft.Controls.Clear();
            DaysLeft.Name = "DaysLeft";
            DaysLeft.Location = new Point(x, 0);
            DaysLeft.Size = new Size(width, schedulePanel.Size.Height);
            DaysLeft.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            SchedulePanelElements.DaysLeftNavigation daysLeftNavigation = new SchedulePanelElements.DaysLeftNavigation(_mainWindowUI, this);
            DaysLeft.Controls.Add(daysLeftNavigation.GetPanel(DaysLeft.Size));
            
            DaysLeft.Update();
        }

        public void ChangeButtonSelected(string day)
        {
            if(day != selectedDay.Name)
            {
                Control button = schedulePanel.Controls.Find(day, true).FirstOrDefault();

                ((Button)button).PerformClick();
            }

        }
        
        public void UpdateShiftPanel(Shift shift)
        {


            //Form print = new Form();
            //ListBox list = new ListBox();
            //list.BeginUpdate();
            //foreach(var item in controlList)
            //{
            //    list.Items.Add(item.Name);
            //}
            //list.EndUpdate();
            //print.Controls.Add(list);

            //print.Show();


            //Kan godt blive et problem at man søger efter id?
            /*
            Control test = _mainHomepagePanel.Controls.Find("Shift " + shift.ID.ToString(), true).FirstOrDefault();

            if (test != null)
            {
                Control parent = test.Parent;
                test.Dispose();
                SchedulePanelElements.ShiftUIPanel temp = new SchedulePanelElements.ShiftUIPanel(_mainWindowUI, shift);
                parent.Controls.Add(temp.ShiftUI(parent as Panel, 50));
            }
            */
        }


    }
}

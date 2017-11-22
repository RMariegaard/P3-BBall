using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage
{
    class Homepage
    {
        Panel _mainHomepagePanel;

        Panel schedulePanel;
        Panel pendingRequestPanel;
        Panel VolunteerPanel;

        public List<String> days;
        public string selectedDay;
        
        public Volunteer ShownVolunteer;

        private IVolunteerMainUI _mainWindowUI;

        public Homepage(IVolunteerMainUI mainWindowUI)
        {
            _mainWindowUI = mainWindowUI;
            _mainHomepagePanel = new Panel();
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
            UpdateSchedulePanel();
            UpdatePendingRequestPanel();
            UpdateVolunteerPanel();
        }

        public void UpdateSchedulePanel()
        {
            _mainHomepagePanel.Controls.Remove(schedulePanel);
            
            //Finding the dates
            days = new List<string>();
            foreach (Shift shift in _mainWindowUI.GetController().GetAllShifts().OrderBy(x => x.StartTime))
            {
                if (!days.Contains(shift.StartTime.DayOfWeek.ToString() + " " + shift.StartTime.ToShortDateString()))
                {
                    days.Add(shift.StartTime.DayOfWeek.ToString() + " " + shift.StartTime.ToShortDateString());
                }
            }

            //Schedule panel
            schedulePanel = new Panel();
            schedulePanel.Location = new Point(0, 0);
            schedulePanel.Size = new Size((_mainHomepagePanel.Size.Width / 100) * 70 - 2, _mainHomepagePanel.Height);
            schedulePanel.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right;
            schedulePanel.AutoScroll = true;
            threeSchedulePanels();
            _mainHomepagePanel.Controls.Add(schedulePanel);
        }
        public void UpdatePendingRequestPanel()
        {
            _mainHomepagePanel.Controls.Remove(pendingRequestPanel);
            //Pending request panel
            pendingRequestPanel = new Panel();
            pendingRequestPanel.Location = new Point(schedulePanel.Location.X + schedulePanel.Size.Width + 2, 0);
            pendingRequestPanel.Size = new Size(_mainHomepagePanel.Size.Width - schedulePanel.Size.Width, (_mainHomepagePanel.Size.Height / 100) * 70);
            pendingRequestPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            RequestPanelElements.MainRequestPanelElement requestPanel = new RequestPanelElements.MainRequestPanelElement(_mainWindowUI);
            pendingRequestPanel.Controls.Add(requestPanel.GetPanel(pendingRequestPanel.Size));
            _mainHomepagePanel.Controls.Add(pendingRequestPanel);
        }
        public void UpdateVolunteerPanel()
        {
            _mainHomepagePanel.Controls.Remove(VolunteerPanel);
            //Volunteer panel
            VolunteerPanel = new Panel();
            VolunteerPanel.Location = new Point(schedulePanel.Location.X + schedulePanel.Size.Width + 2, pendingRequestPanel.Location.Y + pendingRequestPanel.Size.Height + 2);
            VolunteerPanel.Size = new Size(_mainHomepagePanel.Size.Width - schedulePanel.Size.Width - 2, _mainHomepagePanel.Size.Height - pendingRequestPanel.Height - 2);
            VolunteerPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            VolunteerSmallOverview.VolunteerHomapageOverview volunteerOverviewSmall = new VolunteerSmallOverview.VolunteerHomapageOverview(_mainWindowUI, this);
            VolunteerPanel.Controls.Add(volunteerOverviewSmall.GetPanel(VolunteerPanel.Size));
            _mainHomepagePanel.Controls.Add(VolunteerPanel);
        }

        private void threeSchedulePanels()
        {
            Panel DaysLeft = new Panel();
            Panel TheSchedule = new Panel();
            Panel ButtonsBottumPanel = new Panel();

            schedulePanel.Controls.Remove(DaysLeft);
            schedulePanel.Controls.Remove(TheSchedule);
            schedulePanel.Controls.Remove(ButtonsBottumPanel);
            
            DaysLeft.Location = new Point(0, 0);
            DaysLeft.Size = new Size(120, schedulePanel.Size.Height);
            DaysLeft.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            SchedulePanelElements.DaysLeftNavigation daysLeftNavigation = new SchedulePanelElements.DaysLeftNavigation(_mainWindowUI, this);
            DaysLeft.Controls.Add(daysLeftNavigation.GetPanel(DaysLeft.Size));
            
            TheSchedule.Location = new Point(DaysLeft.Location.X + DaysLeft.Size.Width, 0);
            TheSchedule.Size = new Size(schedulePanel.Size.Width - DaysLeft.Size.Width, schedulePanel.Size.Height - 50);
            TheSchedule.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            SchedulePanelElements.TheSchedule schedule = new SchedulePanelElements.TheSchedule(_mainWindowUI, selectedDay);
            TheSchedule.Controls.Add(schedule.GetPanel(TheSchedule));
            TheSchedule.AutoScroll = true;
            
            ButtonsBottumPanel.Location = new Point(DaysLeft.Location.X + DaysLeft.Size.Width, TheSchedule.Location.Y + TheSchedule.Size.Height);
            ButtonsBottumPanel.Size = new Size(schedulePanel.Size.Width - DaysLeft.Size.Width, schedulePanel.Size.Height - TheSchedule.Size.Height);
            ButtonsBottumPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            SchedulePanelElements.ButtonsBottom buttons = new SchedulePanelElements.ButtonsBottom(_mainWindowUI, this);
            ButtonsBottumPanel.Controls.Add(buttons.GetPanel());

            schedulePanel.Controls.Add(DaysLeft);
            schedulePanel.Controls.Add(TheSchedule);
            schedulePanel.Controls.Add(ButtonsBottumPanel);
        }
    }
}

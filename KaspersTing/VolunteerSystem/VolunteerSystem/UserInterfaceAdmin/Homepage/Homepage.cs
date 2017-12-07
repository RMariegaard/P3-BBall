using System;
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

        Panel TheSchedule;
        Panel ButtonsBottumPanel;
        Panel DaysLeft;

        public List<string> days;
        public Button selectedDay;
        
        public Volunteer ShownVolunteer;

        private IVolunteerMainUI _mainWindowUI;

        public RequestPanelElements.MainRequestPanelElement requestPanelElement;

        public Homepage(IVolunteerMainUI mainWindowUI)
        {
            _mainWindowUI = mainWindowUI;
            requestPanelElement = new RequestPanelElements.MainRequestPanelElement(_mainWindowUI);

            InizializeControls();
            CreatePanels();
        }

        SchedulePanelElements.TheSchedule schedule;
        SchedulePanelElements.DaysLeftNavigation daysLeftNavigation;

        private void InizializeControls()
        {
            _mainHomepagePanel = new Panel();
            schedulePanel = new Panel();
            pendingRequestPanel = new Panel();
            VolunteerPanel = new Panel();


            ButtonsBottumPanel = new Panel();
            TheSchedule = new Panel();
            DaysLeft = new Panel();
            daysLeftNavigation = new SchedulePanelElements.DaysLeftNavigation(_mainWindowUI, this);
            _mainHomepagePanel.Name = "_mainHomepagePanel";
            pendingRequestPanel.Name = "pendingRequestPanel";
            VolunteerPanel.Name = "VolunteerPanel";
            schedulePanel.Name = "schedulePanel";
        }

        private void CreatePanels()
        {
            _mainHomepagePanel.Controls.Add(schedulePanel);
            _mainHomepagePanel.Controls.Add(pendingRequestPanel);
            _mainHomepagePanel.Controls.Add(VolunteerPanel);


            schedulePanel.Controls.Add(DaysLeft);
            schedulePanel.Controls.Add(ButtonsBottumPanel);
            schedulePanel.Controls.Add(TheSchedule);

        }

        public Panel GetHomepagePanel(Panel forRefrence)
        {
            _mainHomepagePanel.Size = forRefrence.Size;
            _mainHomepagePanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            //_mainHomepagePanel.Controls.Clear();

            //Updates the panels
            UpdateAllPanels();
           // UpdateSchedulePanel();
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
            foreach (Shift shift in _mainWindowUI.GetScheduleController().GetAllListOfShifts().OrderBy(x => x.StartTime))
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

            //Schedule panel
            schedulePanel.Invalidate();
            schedulePanel.Location = new Point(0, 0);
            schedulePanel.Size = new Size((_mainHomepagePanel.Size.Width / 100) * 70 - 2, _mainHomepagePanel.Height);
            schedulePanel.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right;
            schedulePanel.AutoScroll = true;
            schedulePanel.BorderStyle = BorderStyle.FixedSingle;


            _threeSchedulePanels();
            schedulePanel.Update();

        }

        public void UpdateTheRequestAndNotificationElements()
        {
            requestPanelElement.UpdateRequestAndNotificationPanels();
        }

        public void UpdatePendingRequestPanel()
        {
            pendingRequestPanel.Invalidate();
            //Pending request panel
            pendingRequestPanel.Controls.Clear();
            pendingRequestPanel.Location = new Point(schedulePanel.Location.X + schedulePanel.Size.Width + 2, 0);
            pendingRequestPanel.Size = new Size(_mainHomepagePanel.Size.Width - schedulePanel.Size.Width, (_mainHomepagePanel.Size.Height / 100) * 70);
            pendingRequestPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            
            pendingRequestPanel.Controls.Add(requestPanelElement.GetRequestPanel(pendingRequestPanel.Size));
            pendingRequestPanel.Update();
        }
        public void UpdateVolunteerPanel()
        {
            VolunteerPanel.Invalidate();
            //Volunteer panel
            VolunteerPanel.Controls.Clear();
            VolunteerPanel.Location = new Point(schedulePanel.Location.X + schedulePanel.Size.Width + 2, pendingRequestPanel.Location.Y + pendingRequestPanel.Size.Height + 2);
            VolunteerPanel.Size = new Size(_mainHomepagePanel.Size.Width - schedulePanel.Size.Width - 2, _mainHomepagePanel.Size.Height - pendingRequestPanel.Height - 2);
            VolunteerPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            VolunteerSmallOverview.VolunteerHomapageOverview volunteerOverviewSmall = new VolunteerSmallOverview.VolunteerHomapageOverview(_mainWindowUI, this);
            VolunteerPanel.Controls.Add(volunteerOverviewSmall.GetPanel(VolunteerPanel.Size));
            VolunteerPanel.Update();
        }
        
        private void _threeSchedulePanels()
        {
            TheSchedule.Name = "TheSchedule";
            ButtonsBottumPanel.Name = "ButtonsBottumPanel";

            int leftButtonsWidth = 120;
            int leftButtonsLocationX = 0;
            
            UpdateButtonsLeftSide(leftButtonsLocationX, leftButtonsWidth);

            TheSchedule.Invalidate();
            TheSchedule.Controls.Clear();
            TheSchedule.Location = new Point( 120, 0);
            TheSchedule.Size = new Size(schedulePanel.Size.Width - 120, schedulePanel.Size.Height - 50);
            TheSchedule.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            schedule = new SchedulePanelElements.TheSchedule(_mainWindowUI, selectedDay, schedulePanel.Height - 50 );
            TheSchedule.Controls.Add(schedule.GetPanel(TheSchedule));
            TheSchedule.AutoScroll = true;
            TheSchedule.Update();

            ButtonsBottumPanel.Invalidate();
            ButtonsBottumPanel.Location = new Point(  120, TheSchedule.Location.Y + TheSchedule.Size.Height);
            ButtonsBottumPanel.Size = new Size(schedulePanel.Size.Width - 120, schedulePanel.Size.Height - TheSchedule.Size.Height + 50);
            ButtonsBottumPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            ButtonsBottumPanel.AutoSize = true;
            SchedulePanelElements.ButtonsBottom buttons = new SchedulePanelElements.ButtonsBottom(_mainWindowUI, this);
            ButtonsBottumPanel.Controls.Add(buttons.GetPanel());
            ButtonsBottumPanel.Update();
        }

       
        public void UpdateButtonsLeftSide(int x, int width)
        {
            _createDates();
            DaysLeft.Invalidate();
            DaysLeft.Controls.Clear();
            DaysLeft.Name = "DaysLeft";
            DaysLeft.Location = new Point(x, 0);
            DaysLeft.Size = new Size(width, schedulePanel.Size.Height);
            DaysLeft.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
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

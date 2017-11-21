﻿using System;
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

            //Schedule panel
            schedulePanel = new Panel();
            schedulePanel.Location = new Point(0, 0);
            schedulePanel.Size = new Size((_mainHomepagePanel.Size.Width / 100) * 70 - 2, _mainHomepagePanel.Height);
            //schedulePanel.BackColor = Color.Yellow;
            schedulePanel.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right;
            schedulePanel.AutoScroll = true;
            threeSchedulePanels();

            //Pending request panel
            pendingRequestPanel = new Panel();
            pendingRequestPanel.Location = new Point(schedulePanel.Location.X + schedulePanel.Size.Width + 2, 0);
            pendingRequestPanel.Size = new Size(_mainHomepagePanel.Size.Width - schedulePanel.Size.Width, (_mainHomepagePanel.Size.Height / 100) * 70);
            //pendingRequestPanel.BackColor = Color.Red;
            pendingRequestPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            //Volunteer panel
            VolunteerPanel = new Panel();
            VolunteerPanel.Location = new Point(schedulePanel.Location.X + schedulePanel.Size.Width + 2, pendingRequestPanel.Location.Y + pendingRequestPanel.Size.Height + 2);
            VolunteerPanel.Size = new Size(_mainHomepagePanel.Size.Width - schedulePanel.Size.Width - 2, _mainHomepagePanel.Size.Height - pendingRequestPanel.Height - 2);
            //VolunteerPanel.BackColor = Color.Beige;
            VolunteerPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            //Adds to main panel
            _mainHomepagePanel.Controls.Add(schedulePanel);
            _mainHomepagePanel.Controls.Add(pendingRequestPanel);
            _mainHomepagePanel.Controls.Add(VolunteerPanel);

            return _mainHomepagePanel;
        }

        private void threeSchedulePanels()
        {
            Panel DaysLeft = new Panel();
            DaysLeft.Location = new Point(0, 0);
            DaysLeft.Size = new Size(80, schedulePanel.Size.Height);
            //DaysLeft.BackColor = Color.Coral;
            DaysLeft.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

            Panel TheSchedule = new Panel();
            TheSchedule.Location = new Point(DaysLeft.Location.X + DaysLeft.Size.Width, 0);
            TheSchedule.Size = new Size(schedulePanel.Size.Width - DaysLeft.Size.Width, schedulePanel.Size.Height - 50);
            //TheSchedule.BackColor = Color.DarkGreen;
            TheSchedule.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            SchedulePanelElements.TheSchedule schedule = new SchedulePanelElements.TheSchedule(_mainWindowUI, "Monday");
            TheSchedule.Controls.Add(schedule.GetPanel(TheSchedule));
            TheSchedule.AutoScroll = true;

            Panel ButtonsBottumPanel = new Panel();
            ButtonsBottumPanel.Location = new Point(DaysLeft.Location.X + DaysLeft.Size.Width, TheSchedule.Location.Y + TheSchedule.Size.Height);
            ButtonsBottumPanel.Size = new Size(schedulePanel.Size.Width - DaysLeft.Size.Width, schedulePanel.Size.Height - TheSchedule.Size.Height);
            //ButtonsBottumPanel.BackColor = Color.Bisque;
            ButtonsBottumPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            SchedulePanelElements.ButtonsBottom buttons = new SchedulePanelElements.ButtonsBottom(_mainWindowUI);
            ButtonsBottumPanel.Controls.Add(buttons.GetPanel());

            schedulePanel.Controls.Add(DaysLeft);
            schedulePanel.Controls.Add(TheSchedule);
            schedulePanel.Controls.Add(ButtonsBottumPanel);
        }
    }
}

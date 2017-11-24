﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage.VolunteerSmallOverview
{
    class VolunteerHomapageOverview
    {
        Panel _volunteerHomapageOverviewMainPanel;
        Volunteer volunteer;
        IVolunteerMainUI volunteerMainUI;
        Label titleTopLabel;
        Homepage _homepage;

        public VolunteerHomapageOverview(IVolunteerMainUI volunteerMainUI, Homepage homepage)
        {
            _volunteerHomapageOverviewMainPanel = new Panel();
            _volunteerHomapageOverviewMainPanel.Name = "_volunteerHomapageOverviewMainPanel";
            this.volunteerMainUI = volunteerMainUI;
            titleTopLabel = new Label();
            _homepage = homepage;
        }

        public Panel GetPanel(Size size)
        {
            _volunteerHomapageOverviewMainPanel.Controls.Clear();
            _volunteerHomapageOverviewMainPanel.Size = size;

            titleTopLabel.Location = new Point(10, 0);
            titleTopLabel.Text = "View a single volunteer";
            titleTopLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            titleTopLabel.AutoSize = true;

            volunteer = _homepage.ShownVolunteer;

            if (volunteer != null)
            {
                _volunteerHomapageOverviewMainPanel.Controls.Add(getVolunteerInformationPanel(new Point(5, 40), new Size((_volunteerHomapageOverviewMainPanel.Width/3) - 5, _volunteerHomapageOverviewMainPanel.Height - 50), volunteer));
                _volunteerHomapageOverviewMainPanel.Controls.Add(getVolunteerShiftPanel(new Point((_volunteerHomapageOverviewMainPanel.Width / 3) +5, 40), new Size((2*_volunteerHomapageOverviewMainPanel.Width / 3) - 10, (_volunteerHomapageOverviewMainPanel.Height/2) - 22), volunteer));
                _volunteerHomapageOverviewMainPanel.Controls.Add(getVolunteerRequestPanel(new Point((_volunteerHomapageOverviewMainPanel.Width / 3) + 5, 40 + (_volunteerHomapageOverviewMainPanel.Height / 2) - 22), new Size((2 * _volunteerHomapageOverviewMainPanel.Width / 3) - 10, (_volunteerHomapageOverviewMainPanel.Height / 2) - 22), volunteer));
            }
            _volunteerHomapageOverviewMainPanel.Controls.Add(titleTopLabel);

            return _volunteerHomapageOverviewMainPanel;
        }

        private Panel getVolunteerInformationPanel(Point location, Size size, Volunteer volunteer)
        {
            Panel volunteerInformationPanel = new Panel();
            volunteerInformationPanel.Name = "volunteerInformationPanel";
            volunteerInformationPanel.Location = location;
            volunteerInformationPanel.Size = size;
            volunteerInformationPanel.BorderStyle = BorderStyle.FixedSingle;
            volunteerInformationPanel.AutoScroll = true;

            Label information = new Label();
            String informationString = 
                $"Name: {volunteer.Name}\n\n" +
                $"Emil: {volunteer.Email}\n\n" +
                $"Team: {volunteer.Assosiation}\n\n" +
                $"Created: {volunteer.DateCreated.ToShortDateString()}\n\n" +
                $"Years Participated: \n";
            foreach(int year in volunteer.YearsWorked)
            {
                informationString += $"  - {year} \n";
            }
            information.Text = informationString;
            information.Location = new Point(2, 10);
            information.MaximumSize = new Size(size.Width - 10,0);
            information.AutoSize = true;

            Button fullProfileButton = new Button();
            fullProfileButton.Click += fullProfileButton_Clicked;
            fullProfileButton.Text = "Full Profile";
            fullProfileButton.AutoSize = true;
            fullProfileButton.Location = new Point((size.Width - fullProfileButton.Size.Width) / 2, information.Bottom + 170/*_mainPanel.Size.Height - 100*/);

            volunteerInformationPanel.Controls.Add(fullProfileButton);
            volunteerInformationPanel.Controls.Add(information);
            return volunteerInformationPanel;
        }

        private void fullProfileButton_Clicked(object sender, EventArgs e)
        {
            volunteerMainUI.DisplayVolunteerInVolunteerOverview(volunteer);
        }

        private Panel getVolunteerShiftPanel(Point location, Size size, Worker worker)
        {
            Panel volunteerShiftPanel = new Panel();
            volunteerShiftPanel.Name = "volunteerShiftPanel";
            volunteerShiftPanel.Location = location;
            volunteerShiftPanel.Size = size;
            volunteerShiftPanel.AutoScroll = true;
            volunteerShiftPanel.BorderStyle = BorderStyle.FixedSingle;

            Label shiftLabel = new Label();
            shiftLabel.Text = "Shifts:";
            shiftLabel.Location = new Point(0, 0);
            shiftLabel.AutoSize = true;

            List<Shift> workerShifts = volunteerMainUI.GetScheduleController().GetAllShifts().Where(x => x.Workers.Exists(y => y.ID == worker.ID)).ToList();
            int widthOfShiftElement = 100;
            for (int i = 0; i < workerShifts.Count; i++)
            {
                volunteerShiftPanel.Controls.Add(getSingleShiftPanel(new Point((i * widthOfShiftElement), shiftLabel.Size.Height-2), new Size(widthOfShiftElement, volunteerShiftPanel.Size.Height - 50), workerShifts[i]));
            }

            volunteerShiftPanel.Controls.Add(shiftLabel);
            return volunteerShiftPanel;
        }
        
        private Panel getVolunteerRequestPanel(Point location, Size size, Worker worker)
        {
            Panel volunteerRequestPanel = new Panel();
            volunteerRequestPanel.Name = "volunteerRequestPanel";
            volunteerRequestPanel.Location = location;
            volunteerRequestPanel.Size = size;
            volunteerRequestPanel.AutoScroll = true;
            volunteerRequestPanel.BorderStyle = BorderStyle.FixedSingle;

            Label shiftLabel = new Label();
            shiftLabel.Text = "Requests:";
            shiftLabel.Location = new Point(0, 0);
            shiftLabel.AutoSize = true;

            List<Shift> workerShifts = volunteerMainUI.GetScheduleController().GetAllShifts().Where(x => x.Requests.Any(y => y.Worker.ID == worker.ID) ).ToList();
            int widthOfShiftElement = 100;
            for (int i = 0; i < workerShifts.Count; i++)
            {
                volunteerRequestPanel.Controls.Add(getSingleShiftPanel(new Point((i * widthOfShiftElement), shiftLabel.Size.Height - 2), new Size(widthOfShiftElement, volunteerRequestPanel.Size.Height - 50), workerShifts[i]));
            }

            volunteerRequestPanel.Controls.Add(shiftLabel);
            return volunteerRequestPanel;
        }

        private Panel getSingleShiftPanel(Point location, Size size, Shift shift)
        {
            Panel singleShiftPanel = new Panel();
            singleShiftPanel.Name = "singleShiftPanel";

            singleShiftPanel.Size = size;
            singleShiftPanel.Location = location;
            singleShiftPanel.BorderStyle = BorderStyle.FixedSingle;

            Label information = new Label();
            information.Location = new Point(2, 2);
            information.MaximumSize = new Size(size.Width - 4, size.Height - 4);
            information.AutoSize = true;
            information.Text =
                $"{shift.Task} \n" +
                $"{shift.StartTime.DayOfWeek}\n" +
                $"{shift.StartTime.Hour}:{shift.StartTime.Minute} - {shift.EndTime.Hour}:{shift.EndTime.Minute}";

            singleShiftPanel.Controls.Add(information);
            return singleShiftPanel;
        }
    }
}

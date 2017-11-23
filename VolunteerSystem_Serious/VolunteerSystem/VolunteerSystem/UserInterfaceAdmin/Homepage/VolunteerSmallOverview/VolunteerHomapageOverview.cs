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
        Panel _mainPanel;
        Volunteer volunteer;
        IVolunteerMainUI volunteerMainUI;
        Label titleTopLabel;
        Homepage _homepage;

        public VolunteerHomapageOverview(IVolunteerMainUI volunteerMainUI, Homepage homepage)
        {
            _mainPanel = new Panel();
            this.volunteerMainUI = volunteerMainUI;
            titleTopLabel = new Label();
            _homepage = homepage;
        }

        public Panel GetPanel(Size size)
        {
            _mainPanel.Controls.Clear();
            _mainPanel.Size = size;

            titleTopLabel.Location = new Point(10, 0);
            titleTopLabel.Text = "View a single volunteer";
            titleTopLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            titleTopLabel.AutoSize = true;

            volunteer = _homepage.ShownVolunteer;

            if (volunteer != null)
            {
                _mainPanel.Controls.Add(getVolunteerInformationPanel(new Point(5, 40), new Size((_mainPanel.Width/3) - 5, _mainPanel.Height - 50), volunteer));
                _mainPanel.Controls.Add(getVolunteerShiftPanel(new Point((_mainPanel.Width / 3) +5, 40), new Size((2*_mainPanel.Width / 3) - 10, (_mainPanel.Height/2) - 22), volunteer));
            }
            _mainPanel.Controls.Add(titleTopLabel);

            return _mainPanel;
        }

        private Panel getVolunteerInformationPanel(Point location, Size size, Volunteer volunteer)
        {
            Panel volunteerInformationPanel = new Panel();
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
            fullProfileButton.Location = new Point((size.Width - fullProfileButton.Size.Width) / 2, information.Bottom + 10/*_mainPanel.Size.Height - 100*/);

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
            volunteerShiftPanel.Location = location;
            volunteerShiftPanel.Size = size;
            volunteerShiftPanel.BorderStyle = BorderStyle.FixedSingle;
            volunteerShiftPanel.AutoScroll = true;

            List<Shift> workerShifts = volunteerMainUI.GetController().GetAllShifts().Where(x => x.Workers.Exists(y => y.Name == worker.Name)).ToList();
            int widthOfShiftElement = 100;
            for (int i = 0; i < workerShifts.Count; i++)
            {
                volunteerShiftPanel.Controls.Add(getSingleShiftPanel(new Point((i * widthOfShiftElement), 0), new Size(widthOfShiftElement, volunteerShiftPanel.Size.Height - 10), workerShifts[i]));
            }

            return volunteerShiftPanel;
        }

        private Panel getSingleShiftPanel(Point location, Size size, Shift shift)
        {
            Panel panel = new Panel();
            panel.Size = size;
            panel.Location = location;
            panel.BorderStyle = BorderStyle.FixedSingle;

            Label information = new Label();
            information.Location = new Point(2, 2);
            information.MaximumSize = new Size(size.Width - 4, size.Height - 4);
            information.AutoSize = true;
            information.Text = 
                $"{shift.Task} \n" +
                $"{shift.StartTime.DayOfWeek}\n" +
                $"{shift.StartTime.TimeOfDay} - {shift.EndTime.TimeOfDay}";

            panel.Controls.Add(information);
            return panel;
        }

        private Panel getVolunteerRequestPanel()
        {
            throw new NotImplementedException();
        }
    }
}

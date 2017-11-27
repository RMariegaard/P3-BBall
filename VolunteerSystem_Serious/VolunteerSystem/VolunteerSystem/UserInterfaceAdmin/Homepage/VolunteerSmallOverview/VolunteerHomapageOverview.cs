using System;
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
            _volunteerHomapageOverviewMainPanel = new Panel
            {
                Name = "_volunteerHomapageOverviewMainPanel"
            };
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
                _volunteerHomapageOverviewMainPanel.Controls.Add(_getVolunteerInformationPanel(new Point(5, 40), new Size((_volunteerHomapageOverviewMainPanel.Width/3) - 5, _volunteerHomapageOverviewMainPanel.Height - 50), volunteer));
                _volunteerHomapageOverviewMainPanel.Controls.Add(_getVolunteerShiftPanel(new Point((_volunteerHomapageOverviewMainPanel.Width / 3) +5, 40), new Size((2*_volunteerHomapageOverviewMainPanel.Width / 3) - 10, (_volunteerHomapageOverviewMainPanel.Height/2) - 22), volunteer));
                _volunteerHomapageOverviewMainPanel.Controls.Add(_getVolunteerRequestPanel(new Point((_volunteerHomapageOverviewMainPanel.Width / 3) + 5, 40 + (_volunteerHomapageOverviewMainPanel.Height / 2) - 22), new Size((2 * _volunteerHomapageOverviewMainPanel.Width / 3) - 10, (_volunteerHomapageOverviewMainPanel.Height / 2) - 22), volunteer));
            }
            _volunteerHomapageOverviewMainPanel.Controls.Add(titleTopLabel);

            return _volunteerHomapageOverviewMainPanel;
        }

        private Panel _getVolunteerInformationPanel(Point location, Size size, Volunteer volunteer)
        {
            Panel volunteerInformationPanel = new Panel
            {
                Name = "volunteerInformationPanel",
                Location = location,
                Size = size,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true
            };

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
            fullProfileButton.Click += _fullProfileButton_Clicked;
            fullProfileButton.Text = "Full Profile";
            fullProfileButton.AutoSize = true;
            fullProfileButton.Location = new Point((size.Width - fullProfileButton.Size.Width) / 2, information.Bottom + 170/*_mainPanel.Size.Height - 100*/);

            volunteerInformationPanel.Controls.Add(fullProfileButton);
            volunteerInformationPanel.Controls.Add(information);
            return volunteerInformationPanel;
        }

        private void _fullProfileButton_Clicked(object sender, EventArgs e)
        {
            volunteerMainUI.DisplayVolunteerInVolunteerOverview(volunteer);
        }

        private Panel _getVolunteerShiftPanel(Point location, Size size, Worker worker)
        {
            Panel volunteerShiftPanel = new Panel
            {
                Name = "volunteerShiftPanel",
                Location = location,
                Size = size,
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label shiftLabel = new Label
            {
                Text = "Shifts:",
                Location = new Point(0, 0),
                AutoSize = true
            };

            List<Shift> workerShifts = volunteerMainUI.GetScheduleController().GetAllShifts().Where(x => x.Workers.Exists(y => y.ID == worker.ID)).ToList();
            int widthOfShiftElement = 100;
            for (int i = 0; i < workerShifts.Count; i++)
            {
                volunteerShiftPanel.Controls.Add(_getSingleShiftPanel(new Point((i * widthOfShiftElement), shiftLabel.Size.Height-2), new Size(widthOfShiftElement, volunteerShiftPanel.Size.Height - 50), workerShifts[i]));
            }

            volunteerShiftPanel.Controls.Add(shiftLabel);
            return volunteerShiftPanel;
        }
        
        private Panel _getVolunteerRequestPanel(Point location, Size size, Worker worker)
        {
            Panel volunteerRequestPanel = new Panel
            {
                Name = "volunteerRequestPanel",
                Location = location,
                Size = size,
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label shiftLabel = new Label
            {
                Text = "Requests:",
                Location = new Point(0, 0),
                AutoSize = true
            };

            List<Shift> workerShifts = volunteerMainUI.GetScheduleController().GetAllShifts().Where(x => x.Requests.Any(y => y.Worker.ID == worker.ID) ).ToList();
            int widthOfShiftElement = 100;
            for (int i = 0; i < workerShifts.Count; i++)
            {
                volunteerRequestPanel.Controls.Add(_getSingleShiftPanel(new Point((i * widthOfShiftElement), shiftLabel.Size.Height - 2), new Size(widthOfShiftElement, volunteerRequestPanel.Size.Height - 50), workerShifts[i]));
            }

            volunteerRequestPanel.Controls.Add(shiftLabel);
            return volunteerRequestPanel;
        }



        private Panel _getSingleShiftPanel(Point location, Size size, Shift shift)
        {
            BindingSource shiftBindingSource = new BindingSource
            {
                DataSource = typeof(Shift)
            };
            shiftBindingSource.Add(shift);

                Panel singleShiftPanel = new Panel
            {
                Name = "singleShiftPanel",

                Size = size,
                Location = location,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label information = new Label
            {
                Location = new Point(2, 2),
                MaximumSize = new Size(size.Width - 4, size.Height - 4),
                AutoSize = true,

            };
            var textBinding = new Binding("Text", shiftBindingSource, "Task");
            textBinding.Format += delegate (object sender, ConvertEventArgs convertEventArgs)
            {
                convertEventArgs.Value = $"{convertEventArgs.Value} \n" +
                $"{shift.StartTime.DayOfWeek}\n" +
                shift.StartTime.ToString("HH:mm") + " - " + shift.EndTime.ToString("HH:mm");
            };
            information.DataBindings.Add(textBinding);

            singleShiftPanel.Controls.Add(information);
            return singleShiftPanel;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.VolunteerOverview.VolunteersSeach
{
    class SeachAndVolunteers 
    {
        Panel _searchAndVolunteerMainPanel;
        WorkerController workerController;
        ScheduleController ScheduleController;
        VolunteerOverview _volunteerOverview;
        TextBox searchTextBox;
        Size _size;
        Panel panelNames;
        ComboBox filterOptions;
        List<Worker> allWorkers;
        ComboBox filterTeam;
        

        public SeachAndVolunteers(ScheduleController scheduleController,WorkerController workerController, VolunteerOverview volunteerOverview)
        {
            this.workerController = workerController;
            this.ScheduleController = scheduleController;
            _volunteerOverview = volunteerOverview;
            _searchAndVolunteerMainPanel = new Panel();
            _searchAndVolunteerMainPanel.Name = "_searchAndVolunteerMainPanel";
            searchTextBox = new TextBox();
            filterOptions = new ComboBox();
            filterTeam = new ComboBox();
            allWorkers = workerController.Workers;
        }

        public Panel GetPanel(Size size)
        {
            _size = size;
            _searchAndVolunteerMainPanel.Size = _size;
            _searchAndVolunteerMainPanel.BorderStyle = BorderStyle.FixedSingle;

            Label searchLabel = new Label()
            {
                Text = "Search for volunteers",
                Location = new Point(5, 5),
                AutoSize = true,
            };

            searchTextBox.Location = new Point(5, searchLabel.Location.Y + searchLabel.Size.Height - 5);
            searchTextBox.Size = new Size(_size.Width - 30, 0);
            searchTextBox.TextChanged += searchTextBox_Changed;

            Label filterOptionLabel = new Label()
            {
                Text = "Select filter options:",
                Location = new Point(5, searchTextBox.Location.Y + searchTextBox.Size.Height + 10),
                AutoSize = true,
            };

            filterOptions.Location = new Point(5, filterOptionLabel.Location.Y + filterOptionLabel.Size.Height - 5);
            filterOptions.Size = new Size(_size.Width -30, 0);
            filterOptions.Items.Add("All");
            filterOptions.Items.Add("Volunteers from last year, who hasn't signed up");
            filterOptions.Items.Add("Volunteers validated for seasonal tickets");
            filterOptions.Items.Add("Volunteers signed up this year");
            filterOptions.Items.Add("All external workers");
            filterOptions.SelectedIndex = 0;
            filterOptions.SelectedIndexChanged += FilterOptions_SelectedIndexChanged;

            Label teamOptionLabel = new Label()
            {
                Text = "Select team:",
                Location = new Point(5, filterOptions.Location.Y + filterOptions.Size.Height + 10),
                AutoSize = true,
            };


            GetTeamFilter(new Point(5, teamOptionLabel.Location.Y + teamOptionLabel.Size.Height - 5));

            UpdateNamesPanel();
            _searchAndVolunteerMainPanel.Controls.Add(searchLabel);
            _searchAndVolunteerMainPanel.Controls.Add(filterOptionLabel);
            _searchAndVolunteerMainPanel.Controls.Add(filterOptions);
            _searchAndVolunteerMainPanel.Controls.Add(teamOptionLabel);
            _searchAndVolunteerMainPanel.Controls.Add(searchTextBox);
            _searchAndVolunteerMainPanel.Controls.Add(filterTeam);
            return _searchAndVolunteerMainPanel;
        }

        private void GetTeamFilter(Point point)
        {
            filterTeam.Location = point;
            filterTeam.Size = new Size(_size.Width - 30, 0);
            filterTeam.Items.Add("All");
            var allTeams = workerController.GetAllTeams();
            foreach(var team in allTeams)
            {
                filterTeam.Items.Add(team);
            }
            filterTeam.SelectedIndex = 0;
            filterTeam.SelectedIndexChanged += FilterTeam_SelectedIndexChanged;
        }

        private void FilterTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateNamesPanel();
        }

        private void FilterOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateNamesPanel();
        }





        private void searchTextBox_Changed(object sender, EventArgs e)
        {
            UpdateNamesPanel();
        }

        public void UpdateNamesPanel()
        {
            _searchAndVolunteerMainPanel.Controls.Remove(panelNames);
            panelNames = namesPanel(new Size(_size.Width -10, _size.Height - searchTextBox.Height - filterTeam.Height), new Point(2, filterTeam.Location.Y + filterTeam.Size.Height));
            _searchAndVolunteerMainPanel.Controls.Add(panelNames);
        }

        private Panel namesPanel(Size size, Point location) 
        {
            Panel namesPanel = new Panel();
            namesPanel.Name = "namesPanel";
            namesPanel.Size = size;
            namesPanel.Location = location;
            namesPanel.AutoScroll = true;

            List<Worker> workersList = FilterWorkers();
             
            for (int i = 0; i < workersList.Count; i++)
            {
                Panel panel = new Panel
                {
                    Location = new Point(namesPanel.Location.X, namesPanel.Location.Y + (i * 32)),
                    Size = new Size(namesPanel.Width - 20, 30),
                    BorderStyle = BorderStyle.FixedSingle,
                    Tag = workersList[i]
                };
                if (_volunteerOverview.SelectedWorker == workersList[i])
                    panel.BackColor = ColorAndStyle.PrimaryColor();
                else if (i % 2 == 0)
                    panel.BackColor = ColorAndStyle.SmallAlternatingColorsONE();
                else
                    panel.BackColor = ColorAndStyle.SmallAlternatingColorsTWO();
                panel.Click += Panel_Click;

                LinkLabel label = new LinkLabel
                {
                    Location = new Point(2, 2),
                    Text = workersList[i].Name,
                    AutoSize = true,
                    LinkBehavior = LinkBehavior.HoverUnderline,
                    LinkColor = Color.Black,
                    Tag = workersList[i]
                };
                label.Click += Panel_Click;

                panel.Controls.Add(label);
                namesPanel.Controls.Add(panel);
            }
            
            return namesPanel;
        }

        private List<Worker> FilterWorkers()
        {
            var res = allWorkers.Where(
                x => x.Name.ToLower().Contains(searchTextBox.Text.ToLower())
                || x.Email.ToLower().Contains(searchTextBox.Text.ToLower())
                || ((x.GetType() == typeof(Volunteer)) ? (((Volunteer)x).PhoneNumber.ToString().Contains(searchTextBox.Text)) : false)
                ).OrderBy(x => x.Name).ToList();
            if (filterTeam.SelectedIndex != 0)
            {
                res = res.Where(x => ((Volunteer)x).Assosiation == filterTeam.SelectedItem.ToString()).ToList();
            }
            if(filterOptions.SelectedIndex != 0)
            {
                if (filterOptions.SelectedIndex == 1)
                {
                    //if x.YearsWorked is thisYear - 1, then that means the volunteer worked last year, but have not yet signed up this year.
                    int thisYear = ScheduleController.ScheduleYear();
                    res = res.Where(x => ((Volunteer)x).YearsWorked.LastOrDefault() == thisYear - 1).ToList();
                }
                else if (filterOptions.SelectedIndex == 2)
                {
                    int thisYear = ScheduleController.ScheduleYear();
                    res = res.Where(x => ((Volunteer)x).IsValidForSeasonTickets(thisYear) == true).ToList();

                }
                else if (filterOptions.SelectedIndex == 3)
                {

                    int thisYear = ScheduleController.ScheduleYear();
                    res = res.Where(x => ((Volunteer)x).YearsWorked.LastOrDefault() == thisYear).ToList();
                }
                else if (filterOptions.SelectedIndex == 4)
                {
                    res = res.Where(x => x is ExternalWorker).ToList();
                }
            }
            return res;
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            Worker worker = (Worker)((Control)sender).Tag;
            if (_volunteerOverview.SelectedWorker != worker)
            {
                _volunteerOverview.SelectedWorker = worker;
                UpdateNamesPanel();
            }

        }
    }
}

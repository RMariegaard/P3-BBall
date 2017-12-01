using System;
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
        

        public SeachAndVolunteers(ScheduleController scheduleController,WorkerController workerController, VolunteerOverview volunteerOverview)
        {
            this.workerController = workerController;
            this.ScheduleController = scheduleController;
            _volunteerOverview = volunteerOverview;
            _searchAndVolunteerMainPanel = new Panel();
            _searchAndVolunteerMainPanel.Name = "_searchAndVolunteerMainPanel";
            searchTextBox = new TextBox();
            filterOptions = new ComboBox();
            allWorkers = workerController.Workers;
        }

        public Panel GetPanel(Size size)
        {
            _size = size;
            _searchAndVolunteerMainPanel.Size = _size;
            _searchAndVolunteerMainPanel.BorderStyle = BorderStyle.FixedSingle;

            searchTextBox.Location = new Point(5, 5);
            searchTextBox.Size = new Size(_size.Width - 30, 0);
            searchTextBox.TextChanged += searchTextBox_Changed;

            filterOptions.Location = new Point(5, searchTextBox.Location.Y + searchTextBox.Size.Height + 5);
            filterOptions.Size = new Size(_size.Width -30, 0);
            filterOptions.Items.Add("All");
            filterOptions.Items.Add("Volunteers from last year, who hasn't signed up");
            filterOptions.Items.Add("Volunteers validated for seasonal tickets");
            filterOptions.Items.Add("Volunteers signed up this year");
            filterOptions.Items.Add("All external workers");
            filterOptions.SelectedIndex = 0;
            filterOptions.SelectedIndexChanged += FilterOptions_SelectedIndexChanged;

            UpdateNamesPanel();
            _searchAndVolunteerMainPanel.Controls.Add(filterOptions);
            _searchAndVolunteerMainPanel.Controls.Add(searchTextBox);
            return _searchAndVolunteerMainPanel;
        }

        private void FilterOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(filterOptions.SelectedIndex == 0)
            {
                allWorkers = workerController.Workers;
            }
            else if (filterOptions.SelectedIndex == 1)
            {
                //if x.YearsWorked is thisYear - 1, then that means the volunteer worked last year, but have not yet signed up this year.
                int thisYear = ScheduleController.ScheduleYear();
                allWorkers = workerController.SearchWorkers(x => ((Volunteer)x).YearsWorked.LastOrDefault() == thisYear - 1);
            }
            else if (filterOptions.SelectedIndex == 2)
            {
                int thisYear = ScheduleController.ScheduleYear();
                allWorkers = workerController.SearchWorkers(x => ((Volunteer)x).IsValidForSeasonTickets(thisYear) == true);

            }
            else if (filterOptions.SelectedIndex == 3)
            {
                
                int thisYear = ScheduleController.ScheduleYear();
                allWorkers = workerController.SearchWorkers(x => ((Volunteer)x).YearsWorked.LastOrDefault() == thisYear);
            }
            else if(filterOptions.SelectedIndex == 4)
            {
                allWorkers = workerController.SearchWorkers(x => x is ExternalWorker);
            }
            UpdateNamesPanel();
        }


        private void searchTextBox_Changed(object sender, EventArgs e)
        {
            UpdateNamesPanel();
        }

        public void UpdateNamesPanel()
        {
            _searchAndVolunteerMainPanel.Controls.Remove(panelNames);
            panelNames = namesPanel(new Size(_size.Width -10, _size.Height - searchTextBox.Height - filterOptions.Height), new Point(2, filterOptions.Location.Y + filterOptions.Size.Height));
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
            return allWorkers.Where(
                x => x.Name.ToLower().Contains(searchTextBox.Text.ToLower())
                || x.Email.ToLower().Contains(searchTextBox.Text.ToLower())
                || ((x.GetType() == typeof(Volunteer)) ? (((Volunteer)x).PhoneNumber.ToString().Contains(searchTextBox.Text)) : false)
                ).OrderBy(x => x.Name).ToList(); 
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

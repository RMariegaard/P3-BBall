using System;
using System.Collections.Generic;
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
        VolunteerOverview _volunteerOverview;
        TextBox searchTextBox;
        Size _size;
        Panel panelNames;

        public SeachAndVolunteers(WorkerController workerController, VolunteerOverview volunteerOverview)
        {
            this.workerController = workerController;
            _volunteerOverview = volunteerOverview;
            _searchAndVolunteerMainPanel = new Panel();
            _searchAndVolunteerMainPanel.Name = "_searchAndVolunteerMainPanel";
            searchTextBox = new TextBox();
        }

        public Panel GetPanel(Size size)
        {
            _size = size;
            _searchAndVolunteerMainPanel.Size = _size;
            _searchAndVolunteerMainPanel.BorderStyle = BorderStyle.FixedSingle;

            searchTextBox.Location = new Point(5, 5);
            searchTextBox.Size = new Size(_size.Width - 10, 0);
            searchTextBox.TextChanged += searchTextBox_Changed;

            UpdateNamesPanel();
            _searchAndVolunteerMainPanel.Controls.Add(searchTextBox);
            return _searchAndVolunteerMainPanel;
        }

        private void searchTextBox_Changed(object sender, EventArgs e)
        {
            UpdateNamesPanel();
        }

        public void UpdateNamesPanel()
        {
            _searchAndVolunteerMainPanel.Controls.Remove(panelNames);
            panelNames = namesPanel(new Size(_size.Width, _size.Height - searchTextBox.Height), new Point(2, searchTextBox.Location.Y + searchTextBox.Size.Height));
            _searchAndVolunteerMainPanel.Controls.Add(panelNames);
        }

        private Panel namesPanel(Size size, Point location)
        {
            Panel namesPanel = new Panel();
            namesPanel.Name = "namesPanel";
            namesPanel.Size = size;
            namesPanel.Location = location;

            List<Worker> workersList = workerController.Workers.Where(x => x.Name.ToLower().Contains(searchTextBox.Text.ToLower())).OrderBy(x => x.Name).ToList();

            for (int i = 0; i < workersList.Count; i++)
            {
                Panel panel = new Panel();
                panel.Location = new Point(namesPanel.Location.X, namesPanel.Location.Y + (i * 32));
                panel.Size = new Size(namesPanel.Width - 10, 30);
                panel.BorderStyle = BorderStyle.FixedSingle;
                if (i % 2 == 0)
                    panel.BackColor = ColorAndStyle.SmallAlternatingColorsONE();
                else
                    panel.BackColor = ColorAndStyle.SmallAlternatingColorsTWO();

                Label label = new Label();
                label.Location = new Point(2, 2);
                label.Text = workersList[i].Name;
                label.AutoSize = true;

                panel.Controls.Add(label);

                namesPanel.Controls.Add(panel);
            }
            
            return namesPanel;
        }
    }
}

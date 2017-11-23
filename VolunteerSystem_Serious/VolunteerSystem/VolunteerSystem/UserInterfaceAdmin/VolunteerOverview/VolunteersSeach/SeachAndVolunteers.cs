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

        TextBox searchTextBox;

        public SeachAndVolunteers(WorkerController workerController)
        {
            this.workerController = workerController;
            _searchAndVolunteerMainPanel = new Panel();
            _searchAndVolunteerMainPanel.Name = "_searchAndVolunteerMainPanel";
        }

        public Panel GetPanel(Size size)
        {
            searchTextBox = new TextBox();
            searchTextBox.Location = new Point(5, 5);
            searchTextBox.Size = new Size(size.Width - 10, 30);
            searchTextBox.TextChanged += searchTextBox_Changed;


            
            _searchAndVolunteerMainPanel.Controls.Add(searchTextBox);
            return _searchAndVolunteerMainPanel;
        }

        private void searchTextBox_Changed(object sender, EventArgs e)
        {

        }

        private Panel namesPanel(Size size, Point location)
        {
            Panel namesPanel = new Panel();
            namesPanel.Name = "namesPanel";
            namesPanel.Size = size;
            namesPanel.Location = location;


            return namesPanel;
        }
    }
}

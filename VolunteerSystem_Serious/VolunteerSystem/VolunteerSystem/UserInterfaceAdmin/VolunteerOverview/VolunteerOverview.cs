using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.VolunteerOverview
{
    class VolunteerOverview
    {
        IVolunteerMainUI volunteerMainUI;
        WorkerController workerController;
        Panel _volunteerOverviewMainPanel;

        Panel volunteersAndSeachPanel;

        public Worker SelectedWorker;

        public VolunteerOverview(IVolunteerMainUI volunteerMainUI, WorkerController workerController)
        {
            this.volunteerMainUI = volunteerMainUI;
            _volunteerOverviewMainPanel = new Panel();
            _volunteerOverviewMainPanel.Name = "_volunteerOverviewMainPanel";

            this.workerController = workerController;
            volunteersAndSeachPanel = new Panel();
            volunteersAndSeachPanel.Name = "volunteersAndSeachPanel";

        }

        public Panel GetPanel(Size size)
        {
            UpdateSeachAndVolunteerElement();
            
            return _volunteerOverviewMainPanel;
        }

        public void UpdateSeachAndVolunteerElement()
        {
            _volunteerOverviewMainPanel.Controls.Remove(volunteersAndSeachPanel);

            volunteersAndSeachPanel.Controls.Clear();
            volunteersAndSeachPanel.Location = new Point(0, 0);
            volunteersAndSeachPanel.Size = new Size(_volunteerOverviewMainPanel.Size.Width / 6, _volunteerOverviewMainPanel.Size.Height);
            volunteersAndSeachPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            VolunteersSeach.SeachAndVolunteers seachAndVolunteers = new VolunteersSeach.SeachAndVolunteers(workerController);
            volunteersAndSeachPanel.Controls.Add(seachAndVolunteers.GetPanel(volunteersAndSeachPanel.Size));
            
            _volunteerOverviewMainPanel.Controls.Add(volunteersAndSeachPanel);
        }
    }
}

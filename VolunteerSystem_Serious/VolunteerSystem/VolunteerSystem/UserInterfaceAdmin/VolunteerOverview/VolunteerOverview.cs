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
        Panel mainPanel;

        Panel volunteersAndSeachPanel;

        public Worker SelectedWorker;

        public VolunteerOverview(IVolunteerMainUI volunteerMainUI, WorkerController workerController)
        {
            this.volunteerMainUI = volunteerMainUI;
            mainPanel = new Panel();

            this.workerController = workerController;
            volunteersAndSeachPanel = new Panel();

        }

        public Panel GetPanel(Size size)
        {
            UpdateSeachAndVolunteerElement();
            
            return mainPanel;
        }

        public void UpdateSeachAndVolunteerElement()
        {
            mainPanel.Controls.Remove(volunteersAndSeachPanel);

            volunteersAndSeachPanel.Controls.Clear();
            volunteersAndSeachPanel.Location = new Point(0, 0);
            volunteersAndSeachPanel.Size = new Size(mainPanel.Size.Width / 6, mainPanel.Size.Height);
            volunteersAndSeachPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            VolunteersSeach.SeachAndVolunteers seachAndVolunteers = new VolunteersSeach.SeachAndVolunteers(workerController);
            volunteersAndSeachPanel.Controls.Add(seachAndVolunteers.GetPanel(volunteersAndSeachPanel.Size));
            
            mainPanel.Controls.Add(volunteersAndSeachPanel);
        }
    }
}

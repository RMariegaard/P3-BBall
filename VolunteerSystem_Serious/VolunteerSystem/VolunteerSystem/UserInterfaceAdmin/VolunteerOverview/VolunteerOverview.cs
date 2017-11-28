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
        ScheduleController scheduleController;
        Panel _volunteerOverviewMainPanel;

        Panel volunteersAndSeachPanel;
        Panel volunteerOverviewPanel;

        private Worker _selectedWorker;
        public Worker SelectedWorker
        {
            get
            {
                return _selectedWorker;
            }
            set
            {
                _selectedWorker = value;
                UpdateVolunteerOverviewElement();
            }
        }
        
        public VolunteerOverview(IVolunteerMainUI volunteerMainUI, WorkerController workerController, ScheduleController scheduleController)
        {
            this.volunteerMainUI = volunteerMainUI;
            _volunteerOverviewMainPanel = new Panel();
            _volunteerOverviewMainPanel.Name = "_volunteerOverviewMainPanel";

            this.workerController = workerController;
            volunteersAndSeachPanel = new Panel();
            volunteersAndSeachPanel.Name = "volunteersAndSeachPanel";
            volunteerOverviewPanel = new Panel();

            this.scheduleController = scheduleController;
        }

        public Panel GetPanel(Size size)
        {
            _volunteerOverviewMainPanel.Size = size;

            UpdateSeachAndVolunteerElement();
            UpdateVolunteerOverviewElement();

            return _volunteerOverviewMainPanel;
        }

        public void UpdateVolunteerOverviewElement()
        {
            _volunteerOverviewMainPanel.Controls.Remove(volunteerOverviewPanel);

            volunteerOverviewPanel = new Panel
            {
                Location = new Point(volunteersAndSeachPanel.Location.X + volunteersAndSeachPanel.Width + 5, 0),
                Size = new Size(((_volunteerOverviewMainPanel.Size.Width / 5) * 3), _volunteerOverviewMainPanel.Size.Height),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left,
                BorderStyle = BorderStyle.FixedSingle
            };
            OverviewPanelStuff.TheOverviewPanel theOverviewPanel = new OverviewPanelStuff.TheOverviewPanel(this, scheduleController);
            volunteerOverviewPanel.Controls.Add(theOverviewPanel.GetPanel(volunteerOverviewPanel.Size));

            _volunteerOverviewMainPanel.Controls.Add(volunteerOverviewPanel);
        }

        public void UpdateSeachAndVolunteerElement()
        {
            _volunteerOverviewMainPanel.Controls.Remove(volunteersAndSeachPanel);

            volunteersAndSeachPanel.Controls.Clear();
            volunteersAndSeachPanel.Location = new Point(0, 0);
            volunteersAndSeachPanel.Size = new Size((_volunteerOverviewMainPanel.Size.Width / 5), _volunteerOverviewMainPanel.Size.Height);
            volunteersAndSeachPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            VolunteersSeach.SeachAndVolunteers seachAndVolunteers = new VolunteersSeach.SeachAndVolunteers(workerController, this);
            volunteersAndSeachPanel.Controls.Add(seachAndVolunteers.GetPanel(volunteersAndSeachPanel.Size));

            _volunteerOverviewMainPanel.Controls.Add(volunteersAndSeachPanel);
        }
    }
}

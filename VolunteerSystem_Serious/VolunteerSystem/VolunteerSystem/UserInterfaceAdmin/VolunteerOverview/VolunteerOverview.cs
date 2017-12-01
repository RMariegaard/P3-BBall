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
        Panel createVolunteerPanel;

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
            createVolunteerPanel = new Panel();

            this.scheduleController = scheduleController;
        }

        public Panel GetPanel(Size size)
        {
            _volunteerOverviewMainPanel.Size = size;

            UpdateSeachAndVolunteerElement();
            UpdateVolunteerOverviewElement();
            UpdateCreateVolunteerElement();

            return _volunteerOverviewMainPanel;
        }
        
        public void UpdateSeachAndVolunteerElement()
        {
            _volunteerOverviewMainPanel.Controls.Remove(volunteersAndSeachPanel);

            volunteersAndSeachPanel.Controls.Clear();
            volunteersAndSeachPanel.Location = new Point(0, 0);
            volunteersAndSeachPanel.Size = new Size((_volunteerOverviewMainPanel.Size.Width / 5), _volunteerOverviewMainPanel.Size.Height);
            volunteersAndSeachPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            VolunteersSeach.SeachAndVolunteers seachAndVolunteers = new VolunteersSeach.SeachAndVolunteers(scheduleController,workerController, this);
            volunteersAndSeachPanel.Controls.Add(seachAndVolunteers.GetPanel(volunteersAndSeachPanel.Size));

            _volunteerOverviewMainPanel.Controls.Add(volunteersAndSeachPanel);
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
            OverviewPanelStuff.TheOverviewPanel theOverviewPanel = new OverviewPanelStuff.TheOverviewPanel(this, scheduleController, workerController);
            volunteerOverviewPanel.Controls.Add(theOverviewPanel.GetPanel(volunteerOverviewPanel.Size));

            _volunteerOverviewMainPanel.Controls.Add(volunteerOverviewPanel);
        }

        public void UpdateCreateVolunteerElement()
        {

            _volunteerOverviewMainPanel.Controls.Remove(createVolunteerPanel);

            createVolunteerPanel = new Panel
            {
                Location = new Point(volunteerOverviewPanel.Location.X + volunteerOverviewPanel.Width + 5, _volunteerOverviewMainPanel.Height/2),
                Size = new Size((((_volunteerOverviewMainPanel.Size.Width / 5) * 1) -20), _volunteerOverviewMainPanel.Size.Height/2),
                BorderStyle = BorderStyle.FixedSingle,
            };
            CreateVolunteerStuff.CreateVolunteerClass createVolunteerClass = new CreateVolunteerStuff.CreateVolunteerClass(workerController, this);
            createVolunteerPanel.Controls.Add(createVolunteerClass.GetPanel(createVolunteerPanel.Size));

            _volunteerOverviewMainPanel.Controls.Add(createVolunteerPanel);
        }
    }
}

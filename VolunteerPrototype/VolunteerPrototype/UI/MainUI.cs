using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolunteerSystem;
using VolunteerSystem.UserInterfaceAdmin;

namespace VolunteerPrototype.UI
{
    class MainUI : Form, IUI
    {
        private ScheduleController _scheduleController;
        private WorkerController _workerController;
        private ScheduleUI _scheduleUI;
        private DayNavigation _dayNavigation;

        private Panel _schedulePanel;
        private Panel _dayNavigationPanel;

        public MainUI(ScheduleController sc, WorkerController wc)
        {
            _scheduleController = sc;
            _workerController = wc;

            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            _dayNavigation = new DayNavigation(this);
            _dayNavigationPanel = _dayNavigation.GetPanel(new Size(100, this.Height));
            Controls.Add(_dayNavigationPanel);
            _scheduleUI = new ScheduleUI(_dayNavigation._selected.Text, this);

            _schedulePanel = new Panel()
            {
                Location = new Point(_dayNavigationPanel.Width, 0),
                Size = new Size((this.Size.Width / 100) * 70 - 2, this.Height),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right,
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
        };
            _schedulePanel.Controls.Add(_scheduleUI.GetPanel(_schedulePanel));
            Controls.Add(_schedulePanel);
        }

        public ScheduleController ScheduleController()
        {
            return _scheduleController;
        }

        public void UpdateSchedulePanel()
        {
            
        }

        public WorkerController WorkerController()
        {
            return _workerController;
        }
    }
}

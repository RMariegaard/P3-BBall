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


        public MainUI(ScheduleController sc, WorkerController wc)
        {
            _scheduleController = sc;
            _workerController = wc;

            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            _dayNavigation = new DayNavigation(this);

            Controls.Add(_dayNavigation.GetPanel(new Size(100,this.Height)));
            _scheduleUI = new ScheduleUI(_dayNavigation._selected.Text, this);

            Panel sPanel = new Panel()
            {
                Location = new Point(200, 50),
                Size = new Size(10,10),// new Size((this.Size.Width / 100) * 70 - 2, this.Height),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right,
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
        };
            Controls.Add(sPanel);
            Controls.Add(_scheduleUI.GetPanel(sPanel));
        }

        public ScheduleController ScheduleController()
        {
            return _scheduleController;
        }

        public void UpdateSchedulePanel()
        {
            throw new NotImplementedException();
        }

        public WorkerController WorkerController()
        {
            return _workerController;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage.SchedulePanelElements
{
    class TasksPanels
    {
        IVolunteerMainUI volunteerMainUI;
        public TasksPanels(IVolunteerMainUI volunteerMainUI)
        {
            this.volunteerMainUI = volunteerMainUI;
        }

        public Panel GetATaskPanel(string headline, List<Shift> shifts, Size size, Point location, int hourHeight)
        {
            Panel taskPanel = new Panel
            {
                Name = headline,
                Location = location,
                Size = size,
                BackColor = ColorAndStyle.SecondaryColor(true),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label Headder = new Label
            {
                Location = new Point(2, 2),
                Text = headline,
                AutoSize = true
            };
            taskPanel.Controls.Add(Headder);

            for (int i = 0; i < shifts.Count(); i++)
            {
                ShiftUIPanel tempShiftUIPanel = new ShiftUIPanel(volunteerMainUI, shifts[i]);
                taskPanel.Controls.Add(tempShiftUIPanel.ShiftUI(taskPanel, hourHeight));
            }

            return taskPanel;
        }
    }
}

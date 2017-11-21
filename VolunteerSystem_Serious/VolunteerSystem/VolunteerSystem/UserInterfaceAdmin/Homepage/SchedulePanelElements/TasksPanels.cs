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
        public TasksPanels() { }

        public Panel GetATaskPanel(string headline, List<Shift> shifts, Size size, Point location, int hourHeight)
        {
            Panel taskPanel = new Panel();
            taskPanel.Location = location;
            taskPanel.Size = size;
            taskPanel.BackColor = Color.LightCyan;
            taskPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;

            Label Headder = new Label();
            Headder.Location = new Point(2, 2);
            Headder.Text = headline;
            Headder.AutoSize = true;
            taskPanel.Controls.Add(Headder);

            for (int i = 0; i < shifts.Count(); i++)
            {
                taskPanel.Controls.Add(ShiftUIPanel.ShiftUI(shifts[i], taskPanel, hourHeight));
            }

            return taskPanel;
        }
    }
}

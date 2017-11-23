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
            Panel taskPanel = new Panel();
            taskPanel.Name = headline;
            taskPanel.Location = location;
            taskPanel.Size = size;
            taskPanel.BackColor = Color.FromArgb(100, Color.LightCyan);
            taskPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;
            taskPanel.BorderStyle = BorderStyle.FixedSingle;

            Label Headder = new Label();
            Headder.Location = new Point(2, 2);
            Headder.Text = headline;
            Headder.AutoSize = true;
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

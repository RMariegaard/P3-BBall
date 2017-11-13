using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecretP3;

namespace VolunteerSystem_ForFun.HomepageUIStuff.SchedulePanelUIStuff
{
    public static class TasksPanels
    {
        
        public static Panel GetATaskPanel(string headline, List<Shift> shifts, Size size, Point location, int hourHeight)
        {
            Panel taskPanel = new Panel();
            taskPanel.Location = location;
            taskPanel.Size = size;
            taskPanel.BackColor = Color.FromArgb(100, Color.LightCyan);

            Label Headder = new Label();
            Headder.Location = new Point(2, 2);
            Headder.Text = headline;
            Headder.AutoSize = true;
            taskPanel.Controls.Add(Headder);

            for(int i = 0; i < shifts.Count(); i++)
            {
                taskPanel.Controls.Add(ShiftUIPanel.ShiftUI(shifts[i], taskPanel, hourHeight));
            }
            
            return taskPanel;
        }
    }
}

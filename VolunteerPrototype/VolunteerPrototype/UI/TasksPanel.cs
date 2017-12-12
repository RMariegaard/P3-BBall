using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using VolunteerSystem;

namespace VolunteerPrototype.UI
{
    public class TaskPanel
    {
        private IUI _mainUI;

        public TaskPanel(IUI mainUI)
        {
            _mainUI = mainUI;
        }
        private string _taskName;
        public Panel GetATaskPanel(string headline, List<Shift> shifts, Size size, Point location, int hourHeight, DateTime date)
        {
            _taskName = headline;
            Panel taskPanel = new Panel
            {
                Name = headline,
                Location = location,
                Size = size,
                BackColor = Color.FromArgb(100, Color.Teal),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label Headder = new Label
            {
                Location = new Point(2, 2),
                Text = headline,
                AutoSize = true,
            };
           
            taskPanel.Controls.Add(Headder);

            for (int i = 0; i < shifts.Count(); i++)
            {
                ShiftPanel tempShiftUIPanel = new ShiftPanel(shifts[i], date, _mainUI);
                taskPanel.Controls.Add(tempShiftUIPanel.ShiftUI(taskPanel, hourHeight));
            }

            return taskPanel;
        }

    }
}

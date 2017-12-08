using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using VolunteerSystem;
using VolunteerSystem.UserInterfaceAdmin;

namespace VolunteerPrototype.UI
{
    public class MyShiftsPanel : Panel
    {
        private Volunteer _volunteer;
        private RichTextBox _shiftsTextBox;
        private RichTextBox _requestsTextBox;
        public MyShiftsPanel(Volunteer volunteer, Size size)
        {
            Size = size;
            _volunteer = volunteer;
            var shifts = volunteer.ListOfShifts?.Select(x => x.Task + "\t" + x.StartTime + " - " + x.EndTime);
            _shiftsTextBox = new RichTextBox()
            {
                Location = new Point(20, 20),
                Size = new Size(400, 250),
                Text = string.Join("\n", shifts),
                ReadOnly = true,
                BackColor = System.Drawing.SystemColors.Window
        };
            _requestsTextBox = new RichTextBox()
            {
                Location = new Point(_shiftsTextBox.Location.X + _shiftsTextBox.Width + 50, _shiftsTextBox.Location.Y),
                Size = _shiftsTextBox.Size,
                Text = string.Join("\n", volunteer.ListOfRequests ?? new List<Request>()),
                ReadOnly = true,
                BackColor = System.Drawing.SystemColors.Window
        };
            Controls.Add(_shiftsTextBox);
            Controls.Add(_requestsTextBox);
        }


    }
}

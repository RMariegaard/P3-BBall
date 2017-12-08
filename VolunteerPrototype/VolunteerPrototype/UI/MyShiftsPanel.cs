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
        private Label _shiftLabel;
        private Label _requeslLabel;

        public MyShiftsPanel(Volunteer volunteer, Size size)
        {
            this.Font = new Font(Font.FontFamily, 12F);
            Size = size;
            _volunteer = volunteer;
            var shifts = volunteer.ListOfShifts?.Select(x => x.Task + "\t" + x.StartTime + " - " + x.EndTime);
            _shiftsTextBox = new RichTextBox()
            {
                Location = new Point(50, 50),
                Size = new Size(550, 250),
                Text = string.Join("\n", shifts),
                ReadOnly = true,
                BackColor = System.Drawing.SystemColors.Window
        };
            _requestsTextBox = new RichTextBox()
            {
                Location = new Point(_shiftsTextBox.Location.X + _shiftsTextBox.Width + 100, _shiftsTextBox.Location.Y),
                Size = _shiftsTextBox.Size,
                Text = string.Join("\n", volunteer.ListOfRequests ?? new List<Request>()),
                ReadOnly = true,
                BackColor = System.Drawing.SystemColors.Window
        };
            _shiftLabel = new Label()
            {
                Location = new Point(_shiftsTextBox.Location.X, _shiftsTextBox.Location.Y - 25),
                Text = "Your Shifts:"
            };
            _requeslLabel = new Label()
            {
                Location = new Point(_requestsTextBox.Location.X, _requestsTextBox.Location.Y - 25),
                Text = "Your Requests:",
                AutoSize = true
            };
            Controls.Add(_shiftLabel);
            Controls.Add(_requeslLabel);
            Controls.Add(_shiftsTextBox);
            Controls.Add(_requestsTextBox);
        }


    }
}

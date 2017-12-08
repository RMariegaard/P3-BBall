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
        private TextBox _shiftsTextBox;
        private TextBox _requestsTextBox;
        public MyShiftsPanel(Volunteer volunteer, Size size)
        {
            Size = size;
            _volunteer = volunteer;
            _shiftsTextBox = new TextBox()
            {
                Location = new Point(20, 20),
                Text = string.Join("\n", volunteer.ListOfShifts)
            };
            _requestsTextBox = new TextBox()
            {
                Location = new Point(_shiftsTextBox.Location.X + 50, _shiftsTextBox.Location.Y),
                Text = string.Join("\n", volunteer.ListOfRequests)
            };
            Controls.Add(_shiftsTextBox);
            Controls.Add(_requestsTextBox);
        }


    }
}

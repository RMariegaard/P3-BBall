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
        private TextBox _shiftTextBox;
        public MyShiftsPanel(Volunteer volunteer)
        {
            _volunteer = volunteer;
            _shiftTextBox = new TextBox()
            {
                Text = volunteer.GetHashCode();
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolunteerSystem.UserInterface;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage
{
    class PressedOnShiftPanel
    {
        IVolunteerMainUI _volunteerMainUI;
        
        public PressedOnShiftPanel(IVolunteerMainUI volunteerMainUI)
        {
            _volunteerMainUI = volunteerMainUI;
        }

        public Panel GetPressedOnShiftPanel(SchedulePanelElements.ShiftUIPanel shiftUIPanel)
        {
            Panel panel = new Panel
            {
                Name = "PressedOnShiftPanel",
                BackColor = Color.FromArgb(100, Color.DarkGray),
                Size = _volunteerMainUI.GetFullClientWindowSize(),
                Location = new Point(0, 0)
            };

            return panel;
        }

    }
}

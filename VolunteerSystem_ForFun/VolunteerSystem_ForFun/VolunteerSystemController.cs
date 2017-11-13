using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem_ForFun
{
    public class VolunteerSystemController
    {
        private readonly IVolunteerSystemUI _volunteerSystemUI;
        private readonly IVolunteerSystem _volunteerSystem;

        public VolunteerSystemController(IVolunteerSystemUI ui, IVolunteerSystem system)
        {
            _volunteerSystemUI = ui;
            _volunteerSystem = system;
            ui.Popup += popup;
        }

        public void popup(string header, string body)
        {
            _volunteerSystemUI.DisplayPopup(header, body);
        }
    }
}

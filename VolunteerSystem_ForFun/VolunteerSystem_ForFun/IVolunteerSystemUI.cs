using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem_ForFun
{
    public interface IVolunteerSystemUI
    {
        void DisplayPopup(string header, string body);
        void DisplayHomepage();
        void DisplayVolunteerOverview();
        void DisplaySettings();
        void DisplayGeneralError();
        void Start();
        event VolunteerPopupEvent Popup;
    }
}

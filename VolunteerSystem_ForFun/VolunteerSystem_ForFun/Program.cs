using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecretP3;

namespace VolunteerSystem_ForFun
{
    class Program
    {
        static void Main(string[] args)
        {
            IVolunteerSystem system = new VolunteerSystem();
            IVolunteerSystemUI ui = new WinFormWindow(system);
            VolunteerSystemController controller = new VolunteerSystemController(ui, system);
            
            ui.Start();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolunteerSystem;

namespace VolunteerPrototype
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var test = new VolunteerSystem.UserInterfaceAdmin.Homepage.Homepage()


            Application.Run(test);
        }
    }
}

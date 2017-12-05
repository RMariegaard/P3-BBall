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

            var btn = new Button()
            {
                Text = DateTime.Now.ToString()
            };
            var UI = new UI.MainUI();
            var test = new VolunteerSystem.UserInterfaceAdmin.Homepage.SchedulePanelElements.TheSchedule(UI, btn);
            var homepage = new VolunteerSystem.UserInterfaceAdmin.Homepage.Homepage(UI);

            var form = new Form()
            {
            };
            form.Controls.Add(homepage.schedulePanel);
            homepage.UpdateSchedulePanel();
            form.Size = new System.Drawing.Size(2000, 750);


            Application.Run(form);
        }
    }
}

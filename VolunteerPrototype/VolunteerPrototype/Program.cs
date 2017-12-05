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




            Schedule schedule = new Schedule(2018);
            ScheduleController scheduleController = new ScheduleController(schedule);
            WorkerController workerController = new WorkerController();

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

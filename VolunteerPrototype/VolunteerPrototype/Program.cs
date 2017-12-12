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


            var db = new VolunteerSystem.Database.FinalDataController(new VolunteerSystem.Database.DatabaseContext(VolunteerSystem.Model.SqlDataConnecter.CnnVal("DatabaseCS")));

            ScheduleController scheduleController = new ScheduleController(db.schedule.GetLatestSchedule(), db);
           //var list = db.shift.GetAll().ToList();
           // var list2 = db.schedule.Get(1).ListOfShifts;

            WorkerController workerController = new WorkerController(db);

            var ui = new UI.MainUI(scheduleController, workerController);

            Application.Run(ui);
        }
    }
}

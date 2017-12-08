using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystem.Database
{
    public class FinalController : IFinalController
    {
        private DatabaseContext _context;

        public FinalController(DatabaseContext context)
        {
            _context = context;
            schedule = new ScheduleDataController(context);
            request = new RequestDataController(context);
            shift = new ShiftDataController(context);
            volunteer = new VolunteerDataController(context);
            externalWorker = new ExternalWorkerController(context);


        }

        public IScheduleController schedule { get; private set; }
        public IRequestController request { get; private set; }

        public IShiftController shift { get; private set; }

        public IVolunteerController volunteer { get; private set; }

        public IExternalWorkerController externalWorker { get; private set; }

        public int Complete()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

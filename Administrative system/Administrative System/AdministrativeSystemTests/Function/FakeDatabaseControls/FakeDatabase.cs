using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystemTests.Function.FakeDatabaseControls;

namespace VolunteerSystemTests.Function
{
    class FakeDatabase : IFinalController
    {
        public FakeDatabase()
        {
            schedule = new FakeScheduleController();
            request = new FakeRequestController();
            shift = new FakeShiftController();
            volunteer = new FakeVolunteerController();
            externalWorker = new FakeExternalWorker();
        }
        public IScheduleController schedule { get; set; }

        public IRequestController request { get; set; }

        public IShiftController shift { get; private set; }

        public IVolunteerController volunteer { get; private set; }

        public IExternalWorkerController externalWorker { get; private set; }

        public int Complete()
        {
            return 0;
        }

        public void Dispose()
        {
            
        }
        

    }
}

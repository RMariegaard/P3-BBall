using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystemTests.Function.FakeDatabaseControls;

namespace VolunteerSystemTests.Function
{
    class FakeDatabase : IFinalController
    {
        public FakeDatabase()
        {
            schedule = new FakeScheduleDataController();
            request = new FakeRequestDataController();
            shift = new FakeShiftDataController();
            volunteer = new FakeVolunteerController();
            externalWorker = new FakeExternalWorker();
            notification = new FakeNotificationDataController();
        }
        public IScheduleController schedule { get; set; }

        public IRequestController request { get; set; }

        public IShiftController shift { get; private set; }

        public IVolunteerController volunteer { get; private set; }

        public IExternalWorkerController externalWorker { get; private set; }

        public INotificationDatabase notification { get; private set; }

        public int Complete()
        {
            return 0;
        }

        public void Dispose()
        {
            
        }
        

    }
}

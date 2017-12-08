using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem.Database.InterfacesDatabase
{
    public interface IFinalController : IDisposable
    {
        IScheduleController schedule { get; }

        IRequestController request { get; }

        IShiftController shift { get; }
        IVolunteerController volunteer { get; }
        IExternalWorkerController externalWorker { get; }

        INotificationDatabase notification { get; }

        int Complete();
    }
}

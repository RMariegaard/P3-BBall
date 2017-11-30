using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystem.Database
{
    public class ExternalWorkerController : DatabaseController<ExternalWorker>, IExternalWorkerController
    {
        public ExternalWorkerController(DatabaseContext context):base(context)
        {

        }
    }
}

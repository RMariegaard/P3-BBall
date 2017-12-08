using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database
{
    public class ExternalWorkerDataController : DatabaseController<ExternalWorker>, IExternalWorkerController
    {
        public ExternalWorkerDataController(DatabaseContext context):base(context)
        {

        }
        public void Complete()
        {
            _context.SaveChanges();
        }
        public List<ExternalWorker> GetExternalListOfWorkers()
        {
            return _context.externalWorker.Include("ListOfShifts").ToList();
        }
    }
}

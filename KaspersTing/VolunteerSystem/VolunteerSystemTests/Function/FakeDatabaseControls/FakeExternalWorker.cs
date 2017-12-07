using System.Collections.Generic;
using VolunteerSystem;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystemTests.Function
{
    internal class FakeExternalWorker : IExternalWorkerController
    {
        public void Add(ExternalWorker entity)
        {
            
        }

        public void AddRange(IEnumerable<ExternalWorker> entities)
        {
            
        }

        public void Complete()
        {
            
        }

        public ExternalWorker Get(int Id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ExternalWorker> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public List<ExternalWorker> GetExternalListOfWorkers()
        {
            throw new System.NotImplementedException();
        }

        public void Remove(ExternalWorker entity)
        {
            
        }

        public void RemoveRange(IEnumerable<ExternalWorker> entities)
        {
            
        }
    }
}
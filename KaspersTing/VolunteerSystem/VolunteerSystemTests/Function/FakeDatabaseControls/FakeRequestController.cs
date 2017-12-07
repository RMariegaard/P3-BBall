using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystemTests.Function.FakeDatabaseControls
{
    class FakeRequestController : IRequestController
    {
        public void Add(Request entity)
        {
            
        }

        public void AddRange(IEnumerable<Request> entities)
        {
            
        }

        public void Complete()
        {
            
        }

        public Request Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Request> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Request> GetAllRequest()
        {
            throw new NotImplementedException();
        }

        public Request GetRequest(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Request entity)
        {
            
        }

        public void RemoveRange(IEnumerable<Request> entities)
        {
            
        }

        public void RemoveRequest(Request request)
        {
           
        }
    }
}

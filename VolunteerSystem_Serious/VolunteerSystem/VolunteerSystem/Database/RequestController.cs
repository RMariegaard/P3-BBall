using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database
{
    public class RequestController : DatabaseController<TestRequest>, IRequestController
    {
        public RequestController(DatabaseContext context):base(context)
        {

        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public TestRequest GetRequest(int id)
        {
            return _context.request.Include("TestVolunteer").Single(x => x.RequestId == id);
        }
    }
}

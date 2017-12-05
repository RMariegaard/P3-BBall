using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database.InterfacesDatabase
{
    public interface IRequestController : IDatabaseController<TestRequest>
    {
        void Complete();
        TestRequest GetRequest(int id);
    }
}

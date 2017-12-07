using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database.InterfacesDatabase
{
    public interface IRequestController : IDatabaseController<Request>
    {
        void Complete();
        Request GetRequest(int id);
        void RemoveRequest(Request request);
        List<Request> GetAllRequest();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystem.Database
{
    public class RequestController : DatabaseController<Request>, IRequestController
    {
        public RequestController(DatabaseContext context):base(context)
        {

        }
    }
}

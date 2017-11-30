using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystem.Database
{
    public class VolunteerController : DatabaseController<Volunteer>, IVolunteerController
    {
        public VolunteerController(DatabaseContext context):base(context)
        {

        }
    }
}

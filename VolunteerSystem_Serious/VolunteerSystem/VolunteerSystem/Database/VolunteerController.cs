using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database
{
    public class VolunteerController : DatabaseController<TestVolunteer>, IVolunteerController
    {
        public VolunteerController(DatabaseContext context):base(context)
        {

        }

        public List<TestVolunteer> getthembitches()
        {
            return _context.volunteer.Include("testShift").ToList();
        }
    }
}

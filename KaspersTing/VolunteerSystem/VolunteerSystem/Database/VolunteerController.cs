using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database
{
    public class VolunteerController : DatabaseController<Volunteer>, IVolunteerController
    {
        public VolunteerController(DatabaseContext context):base(context)
        {

        }


        public List<Volunteer> getthembitches()
        {
            return _context.volunteer.Include("ListOfShifts").Include("ListOfRequests").ToList();
        }
        public void Complete()
        {
            _context.SaveChanges();
        }
        
    }
}

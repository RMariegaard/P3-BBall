using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database
{
    public class VolunteerDataController : DatabaseController<Volunteer>, IVolunteerController
    {
        public VolunteerDataController(DatabaseContext context):base(context)
        {

        }


        public List<Volunteer> GetVolunteersFromDatabase()
        {
            return _context.volunteer.Include("ListOfShifts").Include("ListOfRequests").ToList();
        }
        public void Complete()
        {
            //_context.SaveChanges();
        }
        
    }
}

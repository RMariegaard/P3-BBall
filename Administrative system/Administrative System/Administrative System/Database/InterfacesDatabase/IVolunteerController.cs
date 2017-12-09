using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database.InterfacesDatabase
{
    public interface IVolunteerController : IDatabaseController<Volunteer>
    {
        List<Volunteer> GetVolunteersFromDatabase();
        void Complete();

        void Update(Volunteer volunteer);
    }
    
}

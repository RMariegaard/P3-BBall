using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database
{
    public class ShiftController : DatabaseController<TestShift>, IShiftController
    {
        public ShiftController(DatabaseContext context):base(context)
        {

        }
    }
}

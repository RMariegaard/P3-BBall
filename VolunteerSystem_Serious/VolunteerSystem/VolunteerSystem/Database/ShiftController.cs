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

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void UpdateShift(TestShift shift)
        {
            _context.Entry(shift).State = System.Data.Entity.EntityState.Modified;
            
        }
    }
}

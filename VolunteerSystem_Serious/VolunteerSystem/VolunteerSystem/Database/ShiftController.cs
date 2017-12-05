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
            //_context.Entry(shift.ListOfRequest).State = System.Data.Entity.EntityState.Modified;
            _context.Entry(shift.ListOfWorkers).State = System.Data.Entity.EntityState.Modified;

        }
        public List<TestShift> getthembitches()
        {
            return _context.shift.Include("ListOfRequest").Include("ListOfWorkers").Include("ListOfRequest.TestVolunteer").ToList();
           
        }
        public void RemoveRequest(TestRequest request)
        {
            _context.request.Remove(request);
        }

        public TestShift GetShift(int id)
        {
            return _context.shift.Include("ListOfRequest").Include("ListOfWorkers").Single(x => x.ShiftId == id);
        }
    }
}

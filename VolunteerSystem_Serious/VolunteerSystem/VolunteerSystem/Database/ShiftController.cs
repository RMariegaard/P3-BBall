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

        public void UpdateShift(TestShift shift, TestWorker worker)
        {
            //_context.Entry(shift.ListOfRequest).State = System.Data.Entity.EntityState.Modified;
           // shift.AddWorker(worker, 2);
            _context.Entry(shift).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

        }
        public List<TestShift> getthembitches()
        {
            return _context.shift.Include("ListOfRequest").Include("ListOfWorkers").Include("ListOfRequest.TestVolunteer").ToList();
           
        }

        public TestShift GetShift(int id)
        {
            return _context.shift.Include("ListOfRequest").Include("ListOfWorkers").Single(x => x.ShiftId == id);
        }
        public void RemoveRequest(TestRequest request)
        {
            _context.request.Attach(request);
            _context.request.Remove(request);
            _context.SaveChanges();
        }
    }
}

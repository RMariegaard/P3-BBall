using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database
{
    public class ShiftController : DatabaseController<Shift>, IShiftController
    {
        public ShiftController(DatabaseContext context):base(context)
        {

        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void UpdateShift(Shift shift, Worker worker)
        {
            //_context.Entry(shift.ListOfRequests).State = System.Data.Entity.EntityState.Modified;
           // shift.AddWorker(worker, 2);
            _context.Entry(shift).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();

        }
        public List<Shift> getthembitches()
        {
            return _context.shift.Include("ListOfRequests").Include("ListOfWorkers").Include("ListOfRequests.Volunteer").ToList();
           
        }

        public Shift GetShift(int id)
        {
            return _context.shift.Include("ListOfRequests").Include("ListOfWorkers").Single(x => x.ShiftId == id);
        }
        public void RemoveRequest(Request request)
        {
            _context.request.Attach(request);
            _context.request.Remove(request);
            _context.SaveChanges();
        }
    }
}

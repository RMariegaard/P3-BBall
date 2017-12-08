using System.Collections.Generic;
using VolunteerSystem;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystemTests.Function
{
    internal class FakeShiftController : IShiftController
    {
        public FakeShiftController()
        {
        }

        public void Add(Shift entity)
        {
            
        }

        public void AddRange(IEnumerable<Shift> entities)
        {
            
        }

        public void Complete()
        {
            
        }

        public Shift Get(int Id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Shift> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Shift GetShift(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Shift> getthembitches()
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Shift entity)
        {
            
        }

        public void RemoveRange(IEnumerable<Shift> entities)
        {
           
        }

        public void RemoveRequest(Request request)
        {
            
        }

        public void UpdateShift(Shift shift, Worker worker)
        {
            
        }
    }
}
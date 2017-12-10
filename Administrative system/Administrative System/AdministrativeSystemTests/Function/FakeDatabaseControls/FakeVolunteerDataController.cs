using System.Collections.Generic;
using VolunteerSystem;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystemTests.Function
{
    internal class FakeVolunteerController : IVolunteerController
    {
        public FakeVolunteerController()
        {
        }

        public void Add(Volunteer entity)
        {
            
        }

        public void AddRange(IEnumerable<Volunteer> entities)
        {
            
        }

        public void Complete()
        {
          
        }

        public Volunteer Get(int Id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Volunteer> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public List<Volunteer> GetVolunteersFromDatabase()
        {
            return new List<Volunteer>();
        }

        public void Remove(Volunteer entity)
        {
            
        }

        public void RemoveRange(IEnumerable<Volunteer> entities)
        {
            
        }

        public void Update(Volunteer volunteer)
        {
            
        }
    }
}
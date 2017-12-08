using System.Collections.Generic;
using VolunteerSystem;
using VolunteerSystem.Database.InterfacesDatabase;

namespace VolunteerSystemTests.Function
{
    internal class FakeNotificationDataController : INotificationDatabase
    {
        public void Add(Notification entity)
        {
            
        }

        public void AddRange(IEnumerable<Notification> entities)
        {
            
        }

        public void Complete()
        {
           
        }

        public Notification Get(int Id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Notification> GetAll()
        {
            IEnumerable<Notification> temp = new List<Notification>();
            return temp;
        }

        public void Remove(Notification entity)
        {
            
        }

        public void RemoveRange(IEnumerable<Notification> entities)
        {
           
        }
    }
}
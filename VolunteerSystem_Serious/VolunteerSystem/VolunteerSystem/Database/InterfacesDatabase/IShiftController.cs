using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database.InterfacesDatabase
{
    public interface IShiftController : IDatabaseController<TestShift>
    {
        void Complete();
        void UpdateShift(TestShift shift);
        List<TestShift> getthembitches();
        void RemoveRequest(TestRequest request);
        TestShift GetShift(int id);
    }
}

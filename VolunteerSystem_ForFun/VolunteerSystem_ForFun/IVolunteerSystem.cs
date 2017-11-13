using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretP3;

namespace VolunteerSystem_ForFun
{
    public interface IVolunteerSystem
    {
        List<Shift> GetShifts();
        List<String> GetTasks();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database.InterfacesDatabase
{
    public interface IShiftController : IDatabaseController<Shift>
    {
        void Complete();
        void UpdateShift(Shift shift, Worker worker);
        List<Shift> getthembitches();
        void RemoveRequest(Request request);
        Shift GetShift(int id);
    }
}

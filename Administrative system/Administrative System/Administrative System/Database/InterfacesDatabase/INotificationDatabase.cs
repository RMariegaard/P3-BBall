﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem.Database.InterfacesDatabase
{
    public interface INotificationDatabase : IDatabaseController<Notification>
    {
        void Complete();
    }
}

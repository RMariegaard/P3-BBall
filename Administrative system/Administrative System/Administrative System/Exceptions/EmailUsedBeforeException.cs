﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSystem.Exceptions
{
    class EmailUsedBeforeException : Exception
    {
        public EmailUsedBeforeException(string message) : base(message)
        {
        }
    }
}

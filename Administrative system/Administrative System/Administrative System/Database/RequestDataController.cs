﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem.Database.InterfacesDatabase;
using VolunteerSystem.Model;

namespace VolunteerSystem.Database
{
    public class RequestDataController : DatabaseController<Request>, IRequestController
    {
        public RequestDataController(DatabaseContext context):base(context)
        {

        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public Request GetRequest(int id)
        {
            return _context.request.Include("Volunteer").Single(x => x.RequestId == id);
        }
        public List<Request> GetAllRequest()
        {
            return _context.request.Include("Volunteer").Include("Shift").ToList();
        }
        public void RemoveRequest(Request request)
        {
            _context.request.Attach(request);
            _context.request.Remove(request);
            _context.SaveChanges();
        }
    }
}

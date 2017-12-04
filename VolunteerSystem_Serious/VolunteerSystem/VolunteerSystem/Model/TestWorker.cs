using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VolunteerSystem.Model
{
    public abstract class TestWorker
    {
        public TestWorker(string _name, string _email)
        {
            this.email = _email;
            this.name = _name;
        }
        public TestWorker()
        {

        }
        [Key]
        public int workerId { get; set; }
        public string email { get; private set; }
        public string name { get; private set; }
    }
}

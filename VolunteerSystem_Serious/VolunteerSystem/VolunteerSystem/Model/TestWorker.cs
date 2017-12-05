using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerSystem.Model
{
    public abstract class TestWorker
    {
        public TestWorker(string name, string email)
        {
            this.Email = email;
            this.Name = name;
            ListofShifts = new List<TestShift>();
        }
        public TestWorker()
        {
            ListofShifts = new List<TestShift>();
        }
        [Key]
        public int WorkerId { get; set; }
        public string Email { get; private set; }
        public string Name { get; private set; }

        public List<TestShift> ListofShifts {get;set;}


        //public string Email
        //{
        //    get { return _email; }
        //    set
        //    {
        //        if (_checkEmailValidation(value))
        //            _email = value;
        //        else
        //            throw new Exception();
        //    }
        //}



        public string GetInformation()
        {
            return Name + "\t" + Email;
        }

        public override string ToString()
        {
            return Name + " " + Email;
        }

        //private bool _checkEmailValidation(string value)
        //{
        //    try
        //    {
        //        if (!value.Any(c => c == '@'))
        //            return false;

        //        string localPart = value.Substring(0, value.IndexOf('@'));
        //        string domain = value.Substring(value.IndexOf('@') + 1);


        //        if (!localPart.All(c => char.IsLetterOrDigit(c) || c == '.' || c == '_' || c == '-') || localPart.Count() < 1)
        //            return false;



        //        string domainFirstAndLast = domain.Substring(0, 1) + domain.Substring(domain.Count() - 1);
        //        string domainMid = domain.Substring(1, domain.Count() - 2);

        //        if (domainMid.All(c => char.IsLetterOrDigit(c) || c == '.' || c == '-')
        //            && domainFirstAndLast.All(c => char.IsLetterOrDigit(c))
        //            && domain.Any(c => c == '.'))
        //        {
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //    catch (ArgumentOutOfRangeException)
        //    {
        //        return false;
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VolunteerSystem
{
    public abstract class Worker
    {
        protected string _name;
        protected string _email;

        public string Email {
            get { return _email; }
            set
            {
                if (_checkEmailValidation(value))
                    _email = value;
                else
                    throw new Exception();
            }
        }
        
        public string Name { get { return _name; } }
        
        public Worker(string name, string email)
        {
            this._name = name;
            this.Email = email;
        }

        [Key]
        public int ID { get; set; }


        public string GetInformation()
        {
            return Name + "\t" + Email;
        }

        public override string ToString()
        {
            return Name + " " + Email;
        }

        private bool _checkEmailValidation(string value)
        {
            try
            {
                if (!value.Any(c => c == '@'))
                    return false;

                string localPart = value.Substring(0, value.IndexOf('@'));
                string domain = value.Substring(value.IndexOf('@') + 1);


                if (!localPart.All(c => char.IsLetterOrDigit(c) || c == '.' || c == '_' || c == '-') || localPart.Count() < 1)
                    return false;



                string domainFirstAndLast = domain.Substring(0, 1) + domain.Substring(domain.Count() - 1);
                string domainMid = domain.Substring(1, domain.Count() - 2);

                if (domainMid.All(c => char.IsLetterOrDigit(c) || c == '.' || c == '-')
                    && domainFirstAndLast.All(c => char.IsLetterOrDigit(c))
                    && domain.Any(c => c == '.'))
                {
                    return true;
                }
                else
                    return false;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        //private bool ValidateEmail(string email)
        //{
        //    string[] splittedEmail = email.Split('@');

        //    if (splittedEmail.Length == 2)
        //        return EmailLocalPartValidation(splittedEmail[0]) && EmailDomainValidation(splittedEmail[1]);
        //    else
        //        return false;
        //}

        //private bool EmailLocalPartValidation(string localPart)
        //{
        //    const string validCharacters = "abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVXYZ0123456789._-";
        //    return localPart.All(x => validCharacters.Contains(x));
        //}

        //private bool EmailDomainValidation(string domain)
        //{
        //    const string validCharacters = "abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVXYZ0123456789.-";
        //    if (domain.First() == '/' || domain.Last() == '/' || !domain.Contains('.'))
        //        return false;
        //    return domain.All(x => validCharacters.Contains(x));
        //}
    }
}

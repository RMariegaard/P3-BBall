using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretP3
{
    public class Volunteer
    {
        public ContactInfo ContactInformation;
        int _phone => ContactInformation.phone;
        public bool ValidForTickets;
        public List<string> Messages;


        public Volunteer(ContactInfo info)
        {
            Messages = new List<string>();
            this.ContactInformation = info;
            //If volunteer exist in old system => ValidForTickets = true;
        }

        public void ShiftUpdated(Shift shift)
        {
            Messages.Add("Changes happend to " + shift.ToString());

        }

        public override string ToString()
        {
            return "Volunteer: " + ContactInformation.fname + " " + ContactInformation.lname;
        }

    }

    public class ContactInfo
    {
        public string fname;
        public string lname;
        public int phone;
        public string email;
        public int age;
        public string gender;
        public string team;

        public override string ToString()
        {
            return fname + ", " + fname + ", " + age + ", " + gender + ", " + email + ", " + phone + ", " + team + ", " + "hej";
        }

        public ContactInfo(string name, string gender, int age, string email, int phone, string team)
        {
            this.fname = name;
            this.age = age;
            this.email = email;
            this.phone = phone;
            this.gender = gender;
            this.team = team;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerPrototype.UI
{
    public partial class RegisterOrLoginPopUp : Form
    {
        public RegisterOrLoginPopUp()
        {
            
            InitializeComponent();
            label1.Text = "You have to be logged in before you can request a shift.\nIf you do not have a login, you can register as a volunteer.";

            this.Text = "Login Or Register";
            button1.Text = "Log In";
            button1.DialogResult = DialogResult.Yes;
            button2.Text = "Register";
            button2.DialogResult = DialogResult.No;
            StartPosition = FormStartPosition.CenterParent;
        }


    }
}

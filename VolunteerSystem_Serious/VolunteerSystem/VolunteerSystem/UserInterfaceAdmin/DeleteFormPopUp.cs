using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin
{
    public partial class DeleteFormPopUp : Form
    {
        string _text;
        public DeleteFormPopUp(string text)
        {
            _text = text;
            InitializeComponent();
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Abort;
        }


    }
}

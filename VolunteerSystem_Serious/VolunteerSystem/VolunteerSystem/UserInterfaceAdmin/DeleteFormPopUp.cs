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
        public DeleteFormPopUp()
        {
            InitializeComponent();
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Abort;
        }


    }
}

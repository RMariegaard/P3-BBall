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

namespace VolunteerSystem.UserInterfaceAdmin
{
    public partial class DeleteFormPopUp : Form
    {
        string _text;
        public DeleteFormPopUp(string text)
        {
            _text = text;
            
            InitializeComponent();

            this.Icon = new Icon(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\TwoMen.ico");
            this.Text = "Volunteer Manegement System";

            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Abort;
            StartPosition = FormStartPosition.CenterParent;
        }


    }
}

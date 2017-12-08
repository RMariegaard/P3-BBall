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

namespace VolunteerSystem.UserInterface
{
    public partial class PopupUI : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        Label Header;
        Label Message;
        Button OkButton;

        public PopupUI(string headerText, string messageText)
        {
            InitializeComponent();

            this.Icon = new Icon(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\TwoMen.ico");
            this.Text = "Volunteer Manegement System";

            this.FormBorderStyle = FormBorderStyle.None;
            Width = 400;
            Height = 100;
            this.BackColor = Color.DarkGray;
            this.MouseDown += PopupUI_MouseDown;

            int spacing = 10;

            Header = new Label();
            Header.Text = headerText;
            Header.AutoSize = true;
            Header.Location = new Point(spacing, 10);

            Message = new Label();
            Message.Text = messageText;
            Message.AutoSize = true;
            Message.Location = new Point(spacing, 50);

            OkButton = new Button();
            OkButton.Text = "OK";
            OkButton.AutoSize = true;
            OkButton.Location = new Point(Width - OkButton.Size.Width - spacing, Height - OkButton.Size.Height - spacing);
            OkButton.Click += OkButton_Click;

            Controls.Add(Header);
            Controls.Add(Message);
            Controls.Add(OkButton);
        }

        private void PopupUI_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

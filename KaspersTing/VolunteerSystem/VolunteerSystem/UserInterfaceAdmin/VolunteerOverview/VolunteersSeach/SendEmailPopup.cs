using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VolunteerSystem.UserInterfaceAdmin.VolunteerOverview.VolunteersSeach
{
    public class SendEmailPopup : Form
    {
        private string _adressString;

        private RichTextBox _adressTextBox;
        private TextBox _subjectTextBox;
        private RichTextBox _messageTextBox;

        private Button _sendButton;

        private List<Worker> _wokers;

        public SendEmailPopup(List<Worker> ListOfWorkers)
        {
            this.Text = "Email to ListOfWorkers";
            this._wokers = ListOfWorkers;
            Height = 500;
            Width = 700;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            SetAdresses();
            _adressTextBox = new RichTextBox()
            {
                Text = _adressString,
                Location = new Point(5, 5),
                Width = 600,
                Height = 70,
                AutoSize = true
            };
            _subjectTextBox = new TextBox()
            {
                Text = "Subject",
                Location = new Point(5, _adressTextBox.Location.Y + _adressTextBox.Height + 10),
                Width = 600
            };
            _messageTextBox = new RichTextBox()
            {
                Text = "Message",
                Location = new Point(5, _subjectTextBox.Location.Y + _subjectTextBox.Height + 20),
                Width = 600,
                Height = 300,

            };
            _sendButton = new Button();
            _sendButton.Text = "Send";
            _sendButton.Location = new Point(5, _messageTextBox.Location.Y + _messageTextBox.Height + 5);
            _sendButton.Click += SendEmail;
            

            Controls.Add(_adressTextBox);
            Controls.Add(_subjectTextBox);
            Controls.Add(_messageTextBox);
            Controls.Add(_sendButton);
        }

        private void SendEmail(object sender, EventArgs e)
        {
            Notifier.SendEmail(_adressTextBox.Text, _subjectTextBox.Text, _messageTextBox.Text);
            this.Close();
        }

        private void SetAdresses()
        {
            _adressString = string.Join(", ", _wokers.Select(x => x.Email));

        }
    }
}

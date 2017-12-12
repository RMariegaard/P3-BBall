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
    public partial class InformVolunteerEmailBeforeSendUI : Form
    {
        TextBox messageTextbox;
        Button sendEmailButton;
        Button doItButDontSendEmailButton;
        Label emailToLabel;
        WorkerShiftPair[] workersWithshiftsArray;
        string initialMessage;

        public InformVolunteerEmailBeforeSendUI(string message, params WorkerShiftPair[] workersWithshifts)
        {
            InitializeComponent();
            this.Text = "Email";
            this.Icon = new Icon(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\TwoMen.ico");

            workersWithshiftsArray = workersWithshifts;
            initialMessage = message;
            createUI();
        }

        public InformVolunteerEmailBeforeSendUI(string message, params Request[] requests)
        {
            InitializeComponent();
            initialMessage = message;
            WorkerShiftPair[] workerShiftPairs = new WorkerShiftPair[requests.Count()];

            for(int i = 0; i < requests.Count(); i++)
                workerShiftPairs[i] = new WorkerShiftPair(requests[i].Volunteer, requests[i].Shift);
            workersWithshiftsArray = workerShiftPairs;
            
            createUI();
        }

        private void createUI()
        {
            StartPosition = FormStartPosition.CenterParent;

            Height = 320;
            Width = 500;

            emailToLabel = new Label
            {
                Location = new Point(5, 5),
                MaximumSize = new Size(ClientRectangle.Width - 10, 100),
                AutoSize = true
            };
            emailToLabel.Text = "Sending email to: \n";
            foreach (WorkerShiftPair pair in workersWithshiftsArray)
            {
                if (pair.Worker is Volunteer)
                {
                    emailToLabel.Text += $"{pair.Worker.Email}, ";
                }
            }

            messageTextbox = new TextBox()
            {
                Text = initialMessage,
                Multiline = true,
                Location = new Point(5, emailToLabel.Location.Y + emailToLabel.MaximumSize.Height + 5),
                Size = new Size(ClientRectangle.Width - 10, 100)
            };

            sendEmailButton = new Button
            {
                Size = new Size(110, 50),
                Location = new Point((ClientRectangle.Width / 2) - 120, messageTextbox.Location.Y + messageTextbox.Height + 5),
                Text = "Send Mail",
                DialogResult = DialogResult.OK
            };
            sendEmailButton.Click += SendEmailButton_Click;

            doItButDontSendEmailButton = new Button()
            {
                Size = new Size(110, 50),
                Location = new Point((ClientRectangle.Width / 2) + 10, messageTextbox.Location.Y + messageTextbox.Height + 5),
                Text = "Don't send email\nBut do the action",
                DialogResult = DialogResult.OK
            };

            Controls.Add(sendEmailButton);
            Controls.Add(doItButDontSendEmailButton);
            Controls.Add(emailToLabel);
            Controls.Add(messageTextbox);
        }
        
        private void SendEmailButton_Click(object sender, EventArgs e)
        {
            string message = messageTextbox.Text;

            foreach (WorkerShiftPair pair in workersWithshiftsArray)
            {
                if (pair.Worker is Volunteer)
                {
                    message.Replace("[Volunteer]", pair.Worker.Name)
                            .Replace("[Task]", pair.Shift.Task)
                            .Replace("[Time]", pair.Shift.StartTime.ToString("dddd D. dd/MM/yyyy kl. hh:mm"))
                            .Replace("[StartTime]", pair.Shift.StartTime.ToString("dddd D. dd/MM/yyyy kl. hh:mm"))
                            .Replace("[Endtime]", pair.Shift.EndTime.ToString("dddd D. dd/MM/yyyy kl. hh:mm"));

                    Notifier.InformVolunteer((Volunteer)pair.Worker, messageTextbox.Text);
                }
            }
        }
    }
}
/*
    [Volunteer]
    [Task]
    [Time]
    [StartTime]
    [EndTime]
*/

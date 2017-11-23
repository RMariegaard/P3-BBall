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
    public partial class PressedOnShiftPopup : Form
    {
        Shift shift;
        Panel mainPanel;
        Size fullClientSize;

        public PressedOnShiftPopup(Shift shift)
        {
            InitializeComponent();
            Width = 800;
            Height = 400;

            this.shift = shift;
            fullClientSize = RectangleToScreen(this.ClientRectangle).Size;

            mainPanel = new Panel();
            mainPanel.Size = fullClientSize;
            mainPanel.Location = new Point(0, 0);

            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.AutoSize = true;
            cancelButton.Location = new Point(5, mainPanel.Size.Height - cancelButton.Size.Height - 5);
            cancelButton.DialogResult = DialogResult.Cancel;

            Label textLabel = new Label();
            textLabel.Location = new Point();
            textLabel.MaximumSize = new Size(300, 0);
            textLabel.Text = $"You have now pressed on a shift. This window is under constucktion so please dont mind all the mess. " +
                $"This will be adressed within the near future, so don't panic, and don't come running to Casper all the time and ask if he has created this window yet." +
                $"He Is On IT ;) \n\n" +
                $"The shift you have pressed on is this:\n" +
                $"{shift.Task}\n" +
                $"{shift.StartTime.ToShortDateString()} {shift.StartTime.ToShortTimeString()}\n" +
                $"{shift.EndTime.ToShortDateString()} {shift.EndTime.ToShortTimeString()}\n" +
                $"Desciption: {shift.Description}\n\n" +
                $"Volunteers added to the shift: \n";
            foreach (Worker worker in shift.Workers)
                textLabel.Text += $" - {worker.Name}\n";
            textLabel.Text += "\n\nAnd all the requests: \n";
            foreach (Request request in shift.Requests)
                textLabel.Text += $" - {request.Worker.Name}\n";
            textLabel.AutoSize = true;

            mainPanel.Controls.Add(textLabel);
            mainPanel.Controls.Add(cancelButton);
            Controls.Add(mainPanel);
        }
    }
}

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
        Panel _pressedOnShiftPopupMainPanel;
        Size fullClientSize;

        public PressedOnShiftPopup(Shift shift)
        {
            InitializeComponent();
            Width = 400;
            Height = 400;
            

            this.shift = shift;
            fullClientSize = RectangleToScreen(this.ClientRectangle).Size;

            _pressedOnShiftPopupMainPanel = new Panel();
            _pressedOnShiftPopupMainPanel.Name = "_pressedOnShiftPopupMainPanel";
            _pressedOnShiftPopupMainPanel.Size = fullClientSize;
            _pressedOnShiftPopupMainPanel.Location = new Point(0, 0);

            Label shiftInfo = new Label();
            shiftInfo.Text = $" Task: {shift.Task}\n Starts: {shift.StartTime}\n Ends: {shift.EndTime}\n Volunteers Needed: {shift.VolunteersNeeded}\n Description: {shift.Description}\n";
            shiftInfo.AutoSize = true;
            shiftInfo.Location = new Point();
            


            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.AutoSize = true;
            cancelButton.Location = new Point(5, _pressedOnShiftPopupMainPanel.Size.Height - cancelButton.Size.Height - 5);
            cancelButton.DialogResult = DialogResult.Cancel;


            Button editShiftButton = new Button();
            editShiftButton.Text = "Edit Shift";
            editShiftButton.AutoSize = true;
            editShiftButton.Location = new Point(cancelButton.Width + 5, _pressedOnShiftPopupMainPanel.Size.Height - editShiftButton.Size.Height - 5);





            Label workerLabel = new Label();
            workerLabel.Location = new Point(5, shiftInfo.Location.Y + shiftInfo.PreferredHeight);
            workerLabel.MaximumSize = new Size(300, 0);
            workerLabel.Text = "Workers:";
            workerLabel.AutoSize = true;

            ListBox workersList = new ListBox();
            workersList.Text = "Workers";
            workersList.Size = new Size(300, 100);
            workersList.Location = new Point(5,workerLabel.Location.Y + workerLabel.GetPreferredSize(Size.Empty).Height);
            workersList.BorderStyle = BorderStyle.FixedSingle;
            workersList.BeginUpdate();
            foreach (var w in shift.Workers)
            {
                workersList.Items.Add($"{w.Name} - {w.Email}");
            }
            workersList.EndUpdate();
            workersList.AutoSize = true;

            Label requestLabel = new Label();
            requestLabel.Location = new Point(0, workersList.Location.Y + workersList.Height);
            requestLabel.MaximumSize = new Size(300, 0);
            requestLabel.Text = "Requests:";
            requestLabel.AutoSize = true;

            ListBox requestsList = new ListBox();
            requestsList.Text = "Requests";
            requestsList.Size = new Size(300, 100);
            requestsList.Location = new Point(5, requestLabel.Location.Y + requestLabel.PreferredSize.Height);
            requestsList.BorderStyle = BorderStyle.FixedSingle;
            requestsList.BeginUpdate();
            foreach (var r in shift.Requests)
            {
               requestsList.Items.Add($"{r.TimeSent} - {r.Worker.Name} - {r.Worker.Email}");
            }
            requestsList.EndUpdate();
            requestsList.AutoSize = true;


            _pressedOnShiftPopupMainPanel.Controls.Add(workerLabel);
            _pressedOnShiftPopupMainPanel.Controls.Add(cancelButton);
            //hvorfor virker det kun når jeg tilføger den til this og ikke mainPanel???
           this.Controls.Add(workersList);
            this.Controls.Add(requestsList);
            this.Controls.Add(requestLabel);
            this.Controls.Add(shiftInfo);
            this.Controls.Add(editShiftButton);
            Controls.Add(_pressedOnShiftPopupMainPanel);
        }

    }
}

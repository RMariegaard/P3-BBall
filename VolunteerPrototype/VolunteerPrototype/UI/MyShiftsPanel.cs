using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using VolunteerSystem;
using VolunteerSystem.UserInterfaceAdmin;

namespace VolunteerPrototype.UI
{
    public class MyShiftsPanel : Panel
    {
        private Volunteer _volunteer;
        private ListBox _shiftsTextBox;
        private ListBox _requestsTextBox;
        private Label _shiftLabel;
        private Label _requeslLabel;
        private Button _removeShiftButton;
        private Button _removeRequestButton;

        private IUI _mainUI;


        public MyShiftsPanel(Volunteer volunteer, Size size, IUI mainUI)
        {
            _mainUI = mainUI;
            this.Font = new Font(Font.FontFamily, 12F);
            Size = size;
            _volunteer = volunteer;
            var shifts = volunteer.ListOfShifts?.Select(x => x.Task + "\t" + x.StartTime + " - " + x.EndTime);
            var requests = volunteer.ListOfRequests?.Select(x => x.Shift).Select(x => x.Task + "\t" + x.StartTime + " - " + x.EndTime);
            _shiftsTextBox = new ListBox()
            {
                Location = new Point(50, 50),
                Size = new Size(550, 250),
                BackColor = System.Drawing.SystemColors.Window,
                
        };
            _shiftsTextBox.Leave += UnselectShiftBox;
            
            _shiftsTextBox.Items.AddRange(volunteer.ListOfShifts?.ToArray());
            _shiftsTextBox.Format += FormatShift;
            _shiftsTextBox.FormattingEnabled = true;

            _requestsTextBox = new ListBox()
            {
                Location = new Point(_shiftsTextBox.Location.X + _shiftsTextBox.Width + 100, _shiftsTextBox.Location.Y),
                Size = _shiftsTextBox.Size,
                BackColor = System.Drawing.SystemColors.Window
            };
            _requestsTextBox.Items.AddRange(volunteer.ListOfRequests?.ToArray());
            _requestsTextBox.Leave += UnselectRequestBox;
            _requestsTextBox.Format += FormatRequest;
            _requestsTextBox.FormattingEnabled = true;


            _shiftLabel = new Label()
            {
                Location = new Point(_shiftsTextBox.Location.X, _shiftsTextBox.Location.Y - 25),
                Text = "Your Shifts:",
                AutoSize = true 
            };
            _requeslLabel = new Label()
            {
                Location = new Point(_requestsTextBox.Location.X, _requestsTextBox.Location.Y - 25),
                Text = "Your Requests:",
                AutoSize = true
            };

            _removeShiftButton = new Button()
            {
                Location = new Point(_shiftsTextBox.Location.X, _shiftsTextBox.Location.Y + _shiftsTextBox.Height + 25),
                Text = "Remove me from this shift",
                Enabled = false,
                AutoSize = true
            };
            _removeShiftButton.Click += removeShift_click;
            _shiftsTextBox.SelectedIndexChanged += UpdateShiftButton;

            _removeRequestButton = new Button()
            {
                Location = new Point(_requestsTextBox.Location.X, _requestsTextBox.Location.Y + _requestsTextBox.Height + 25),
                Text = "Cancel this request",
                Enabled = false,
                AutoSize = true
            };
            _removeRequestButton.Click += removeRequest;
            _requestsTextBox.SelectedIndexChanged += UpdateRequestButton;

            Controls.Add(_shiftLabel);
            Controls.Add(_requeslLabel);
            Controls.Add(_shiftsTextBox);
            Controls.Add(_requestsTextBox);
            Controls.Add(_removeRequestButton);
            Controls.Add(_removeShiftButton);
        }

        private void removeRequest(object sender, EventArgs e)
        {
            string message = "Are you sure that you want to cancel your request from this shift?";

            var result = MessageBox.Show(message, "Cancel Request",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.Yes)
            {
                _mainUI.ScheduleController().RemoveRequest((Request)_requestsTextBox.Items[_requestsTextBox.SelectedIndex]);
                _mainUI.ShowMyShifts();
            }
        }

        private void FormatShift(object sender, ListControlConvertEventArgs e)
        {
            var shift = e.ListItem as Shift;
            e.Value = shift.Task + "\t" + shift.StartTime + " - " + shift.EndTime;
        }

        private void FormatRequest(object sender, ListControlConvertEventArgs e)
        {
            Request request = e.ListItem as Request;
            var shift = request.Shift;
            if (shift != null)
            {
                e.Value = shift.Task + "\t" + shift.StartTime + " - " + shift.EndTime;
            }
        }

        private void removeShift_click(object sender, EventArgs e)
        {
            string message = "Are you sure that you want to remove youself from this shift?\nThe Administrator will be notified of this.";

            var result = MessageBox.Show(message, "Removing yourself from a shift",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.Yes)
            {

                var shift = (Shift)_shiftsTextBox.SelectedItem;
                _mainUI.ScheduleController().RemoveWorkerFromShift(_volunteer, shift);
                _mainUI.ScheduleController().CreateNotification(new Notification("Volunteer Removed From Shift", $"{_volunteer.Name} has removed themselves from {shift.Task} - {shift.StartTime}", NotificationImportance.HighImportance));
                _mainUI.ShowMyShifts();
            }
        }

        private void UpdateRequestButton(object sender, EventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox != null && listbox.SelectedIndex > -1)
            {
                _removeRequestButton.Enabled = true;
            }
        }
        private void UpdateShiftButton(object sender, EventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox != null && listbox.SelectedIndex > -1)
            {
                _removeShiftButton.Enabled = true;
            }
        }

        private void UnselectRequestBox(object sender, EventArgs e)
        {
            var listbox = sender as ListBox;
            if(listbox != null && _shiftsTextBox.ContainsFocus)
            {
                listbox.SelectedIndex = -1;
                _removeRequestButton.Enabled = false;
            }
        }
        private void UnselectShiftBox(object sender, EventArgs e)
        {
            var listbox = sender as ListBox;
            if (listbox != null && _requestsTextBox.ContainsFocus)
            {
                listbox.SelectedIndex = -1;
                _removeShiftButton.Enabled = false;
            }
        }
    }
}

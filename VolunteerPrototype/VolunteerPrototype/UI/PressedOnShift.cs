using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolunteerSystem;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace VolunteerPrototype.UI
{
    public class PressedOnShift : Form
    {
        Shift shift;
        Panel _pressedOnShiftPopupMainPanel;
        Size fullClientSize;
        IUI _mainUI;
        ListBox ListOfWorkersList;
        ListBox ListOfRequestsList;
        private Button _requestButton;
        private Button _removeButton;

        BindingSource bindingSource = new BindingSource();

        public PressedOnShift(Shift shift, IUI mainUI)
        {


            this.Icon = new Icon(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\TwoMen.ico");
            this.Text = $"{shift.Task} - {shift.StartTime}";

            Width = 400;
            Height = 400;
            this.StartPosition = FormStartPosition.CenterParent;
            this._mainUI = mainUI;
            this.shift = shift;
            fullClientSize = RectangleToScreen(this.ClientRectangle).Size;

            _pressedOnShiftPopupMainPanel = new Panel
            {
                Name = "_pressedOnShiftPopupMainPanel",
                Size = fullClientSize,
                Location = new Point(0, 0)
            };

            Label shiftInfo = new Label
            {
                AutoSize = true,
                Text = $" Task: {shift.Task}\n Starts: {shift.StartTime}\n Ends: {shift.EndTime}\n Volunteers Needed: {shift.VolunteersNeeded}\n Description: {shift.Description}\n",
                Location = new Point()
            };
            var shiftBindingSource = new BindingSource()
            {
                DataSource = typeof(Shift)
            };
            shiftBindingSource.Add(shift);

            var shiftInfoBinding = new Binding("Text", shiftBindingSource, "Task");
            shiftInfoBinding.Format += delegate (object sender, ConvertEventArgs e)
            {
                e.Value = $" Task: {shift.Task}\n Starts: {shift.StartTime.ToString("dd/MM/yyyy HH:mm")}\n Ends: {shift.EndTime.ToString("dd/MM/yyyy HH:mm")}\n Volunteers Needed: {shift.VolunteersNeeded}\n Description: {shift.Description}\n";
            };
            shiftInfo.DataBindings.Add(shiftInfoBinding);

            Button cancelButton = new Button
            {
                Text = "Cancel",
                AutoSize = true
            };
            cancelButton.Location = new Point(5, _pressedOnShiftPopupMainPanel.Size.Height - cancelButton.Size.Height - 5);
            cancelButton.DialogResult = DialogResult.Cancel;

            Label workerLabel = new Label
            {
                Location = new Point(5, shiftInfo.Location.Y + shiftInfo.PreferredHeight),
                MaximumSize = new Size(300, 0),
                Text = "List Of Workers:",
                AutoSize = true
            };

            ListOfWorkersList = new ListBox
            {
                Text = "ListOfWorkers",
                Size = new Size(300, 100),
                Location = new Point(5, workerLabel.Location.Y + workerLabel.GetPreferredSize(Size.Empty).Height),
                BorderStyle = BorderStyle.FixedSingle
            };
            ListOfWorkersList.AutoSize = true;
            ListOfWorkersList.ScrollAlwaysVisible = true;

            Label requestLabel = new Label
            {
                Location = new Point(0, ListOfWorkersList.Location.Y + ListOfWorkersList.Height),
                MaximumSize = new Size(300, 0),
                Text = "ListOfRequests:",
                AutoSize = true
            };

            ListOfRequestsList = new ListBox
            {
                Text = "ListOfRequests",
                AutoSize = true,
                ScrollAlwaysVisible = true,
                Size = new Size(300, 100),
                Location = new Point(5, requestLabel.Location.Y + requestLabel.PreferredSize.Height),
                BorderStyle = BorderStyle.FixedSingle
            };


            _requestButton = new Button()
            {
                Text = "Request this shift",
                AutoSize = true,
                Location = new Point(cancelButton.Location.X + cancelButton.Width + 5, cancelButton.Location.Y)
            };
            _requestButton.Click += RequestShift;
            if (shift.ListOfRequests.Exists(x => x.Volunteer == _mainUI.GetCurrentUser))
            {
                HasBeenRequested();
            }
            else if (shift.ListOfWorkers.Contains(_mainUI.GetCurrentUser))
            {
                IsOnShift();
            }
            Controls.Add(_requestButton);





            _pressedOnShiftPopupMainPanel.Controls.Add(workerLabel);
            _pressedOnShiftPopupMainPanel.Controls.Add(cancelButton);
            this.Controls.Add(ListOfWorkersList);
            this.Controls.Add(ListOfRequestsList);
            this.Controls.Add(requestLabel);
            this.Controls.Add(shiftInfo);
            Controls.Add(_pressedOnShiftPopupMainPanel);

            UpdateWorkersAndRequests();
        }

        private void IsOnShift()
        {
            MakeRemoveMeFromShiftButton();
            _requestButton.Text = "You are on this shift";
            _requestButton.Enabled = false;
        }

        private void MakeRemoveMeFromShiftButton()
        {
            _removeButton = new Button()
            {
                Text = "Remove Me From This Shift",
                AutoSize = true,
                Location = new Point(this.Width - 170, _requestButton.Location.Y)
            };
            _removeButton.Click += RemoveFromShiftClick;
            Controls.Add(_removeButton);
        }

        private void RemoveFromShiftClick(object sender, EventArgs e)
        {
            string message = "Are you sure that you want to remove youself from this shift?\nThe Administrator will be notified of this.";

            var result = MessageBox.Show(message, "Removing yourself from a shift",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.Yes)
            {
                _mainUI.ScheduleController().RemoveWorkerFromShift(_mainUI.GetCurrentUser, shift);
                _mainUI.ScheduleController().CreateNotification(new Notification("Volunteer Removed From Shift", $"{_mainUI.GetCurrentUser.Name} has removed themselves from {shift.Task} - {shift.StartTime}", NotificationImportance.HighImportance));
                ListOfWorkersList.Items.Remove(_mainUI.GetCurrentUser.Association + " - " + _mainUI.GetCurrentUser.Name);

                _requestButton.Text = "Request this shift";
                _requestButton.Enabled = true;
                Controls.Remove(_removeButton);

            }


        }

        private void UpdateWorkersAndRequests()
        {
            var workerlist = shift.ListOfWorkers;
            foreach (var volunteer in workerlist.Select(x => x as Volunteer))
            {
                if (volunteer != null)
                {
                    if (_mainUI.IsLoggedIn())
                        ListOfWorkersList.Items.Add($"{volunteer.Association} - {volunteer.Name}");
                    else
                        ListOfWorkersList.Items.Add($"{volunteer.Association}");
                }
            }
            foreach (var worker in workerlist.Select(x => x as ExternalWorker))
            {
                if (worker != null)
                    ListOfWorkersList.Items.Add($"{worker.Name}");
            }

            ListOfRequestsList.BeginUpdate();
            foreach (var r in shift.ListOfRequests)
            {
                if (_mainUI.IsLoggedIn())
                    ListOfRequestsList.Items.Add(r.Volunteer.Association + " - " + r.Volunteer.Name);
                else
                    ListOfRequestsList.Items.Add(r.Volunteer.Association);
            }

            ListOfRequestsList.EndUpdate();
            this._pressedOnShiftPopupMainPanel.Update();

        }


        private void HasBeenRequested()
        {
            _requestButton.Text = "You have requested this shift";
            _requestButton.Enabled = false;

            _removeButton = new Button()
            {
                Text = "Cancel Request",
                AutoSize = true,
                Location = new Point(this.Width - 115, _requestButton.Location.Y)
            };
            _removeButton.Click += CancelRequestClick;
            Controls.Add(_removeButton);
            
        }

        private void CancelRequestClick(object sender, EventArgs e)
        {
            string message = "Are you sure that you want to cancel your request from this shift?";

            var result = MessageBox.Show(message, "Cancel Request",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                _mainUI.ScheduleController().RemoveRequest(_mainUI.GetCurrentUser.ListOfRequests.First(x => x.Shift == shift));

                ListOfRequestsList.Items.Remove(_mainUI.GetCurrentUser.Association + " - " + _mainUI.GetCurrentUser.Name);

                _requestButton.Text = "Request this shift";
                _requestButton.Enabled = true;
                Controls.Remove(_removeButton);
            }

        }

        private void RequestShift(object sender, EventArgs e)
        {
            string message = "Are you sure you want to request this shift?\nThe Administrator has to accept your request before you actually are going to work on the shift.\nYou will be notified via email when this happens.";
            DialogResult result = DialogResult.No; //Standard - has to be assigned

            if (_mainUI.IsLoggedIn())
            {
                var res = MessageBox.Show(message, "Request Shift",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    _mainUI.ScheduleController().SendRequest(shift, _mainUI.GetCurrentUser);
                    HasBeenRequested();
                    _mainUI.UpdateSchedulePanel();
                    _mainUI.UpdateMenu();
                }
            }
            else
            {
                var popUp = new RegisterOrLoginPopUp();
                popUp.ShowDialog();
                //Login
                if (popUp.DialogResult == DialogResult.Yes) {
                    var login = new LogIn.LogInForm(_mainUI);
                    login.ShowDialog();
                    if (login.DialogResult == DialogResult.Yes)
                    {
                        result = MessageBox.Show(message, "Request Shift",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                    }
                }
                //register
                else if(popUp.DialogResult == DialogResult.No)
                {
                    var register = new LogIn.RegisterForm(_mainUI);
                    register.ShowDialog();
                    if(register.DialogResult == DialogResult.Yes)
                    {
                        result = MessageBox.Show(message, "Request Shift",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                    }

                }
                //If either registered or logged in
                    if (result == DialogResult.Yes)
                    {
                        if (!shift.ListOfRequests.Exists(x => x.Volunteer == _mainUI.GetCurrentUser) && !shift.ListOfWorkers.Contains(_mainUI.GetCurrentUser))
                        { 
                            _mainUI.ScheduleController().SendRequest(shift, _mainUI.GetCurrentUser);
                            HasBeenRequested();
                            _mainUI.UpdateSchedulePanel();
                            _mainUI.UpdateMenu();


                        ListOfRequestsList.Items.Add(_mainUI.GetCurrentUser.Association + " - " + _mainUI.GetCurrentUser.Name);
                        HasBeenRequested();

                    }
                }
                }

        }
    }

}

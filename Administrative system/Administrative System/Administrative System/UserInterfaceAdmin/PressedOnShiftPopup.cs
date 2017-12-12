using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolunteerSystem.UserInterface;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;

namespace VolunteerSystem.UserInterfaceAdmin
{
    public partial class PressedOnShiftPopup : Form
    {
        Shift shift;
        Panel _pressedOnShiftPopupMainPanel;
        Size fullClientSize;
        IVolunteerMainUI volunteerMainUI;
        ListBox ListOfWorkersList;
        ListBox ListOfRequestsList;

        BindingSource bindingSource = new BindingSource();

        public PressedOnShiftPopup(Shift shift, IVolunteerMainUI volunteerMainUI)
        {
            InitializeComponent();

            this.Icon = new Icon(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\TwoMen.ico");
            this.Text = "Volunteer Manegement System";

            Width = 400;
            Height = 400;
            this.StartPosition = FormStartPosition.CenterParent;
            this.volunteerMainUI = volunteerMainUI;
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


            Button editShiftButton = new Button
            {
                Text = "Edit Shift",
                AutoSize = true
            };
            editShiftButton.Location = new Point(cancelButton.Width + 5, _pressedOnShiftPopupMainPanel.Size.Height - editShiftButton.Size.Height - 5);
            editShiftButton.Click += EditShift_Clicked;

            Button deleteButton = new Button
            {
                Text = "Delete Shift",
                AutoSize = true,
                
            };
            deleteButton.Location = new Point(_pressedOnShiftPopupMainPanel.Size.Width - deleteButton.Size.Width - 5, _pressedOnShiftPopupMainPanel.Size.Height - deleteButton.Size.Height - 5);
            deleteButton.Click += DeleteButton_clicked;

            Label workerLabel = new Label
            {
                Location = new Point(5, shiftInfo.Location.Y + shiftInfo.PreferredHeight),
                MaximumSize = new Size(300, 0),
                Text = "Workers:",
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

            

            bindingSource.DataSource = shift.ListOfWorkers;
            ListOfWorkersList.BeginUpdate();

            ListOfWorkersList.DataSource = bindingSource;


            ListOfWorkersList.SelectedIndexChanged += ListOfWorkersList_SelectedIndexChanged;
            


            ListOfWorkersList.EndUpdate();

            Label requestLabel = new Label
            {
                Location = new Point(0, ListOfWorkersList.Location.Y + ListOfWorkersList.Height),
                MaximumSize = new Size(300, 0),
                Text = "Requests:",
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
            
            ListOfRequestsList.BeginUpdate();
            foreach (var r in shift.ListOfRequests)
            {
               ListOfRequestsList.Items.Add(r);
            }
            ListOfRequestsList.SelectedIndexChanged += ListOfRequestsList_SelectedIndexChanged;

            ListOfRequestsList.EndUpdate();

            Button addWorkerButton = new Button()
            {

                Location = new Point(ListOfWorkersList.Location.X + ListOfWorkersList.Width + 5, ListOfWorkersList.Location.Y + ListOfWorkersList.Height - 60 - 5),
                Image = ResizeImage(System.Drawing.Image.FromFile(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\addVolunteerImage.PNG"), 25, 25),
                Size = new Size(35, 35),
            };
            addWorkerButton.Click += _addWorkerButton_Clicked;


            _pressedOnShiftPopupMainPanel.Controls.Add(workerLabel);
            _pressedOnShiftPopupMainPanel.Controls.Add(cancelButton);
            _pressedOnShiftPopupMainPanel.Controls.Add(addWorkerButton);
            //hvorfor virker det kun når jeg tilføger den til this og ikke mainPanel???
           this.Controls.Add(ListOfWorkersList);
            this.Controls.Add(ListOfRequestsList);
            this.Controls.Add(requestLabel);
            this.Controls.Add(shiftInfo);
            this.Controls.Add(editShiftButton);
            this.Controls.Add(deleteButton);
            Controls.Add(_pressedOnShiftPopupMainPanel);
        }

        private void ListOfRequestsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Request request = (Request)ListOfRequestsList.SelectedItem;
            if(request != null)
                volunteerMainUI.UpdateSmallVolunteerOverview(request.Volunteer as Volunteer);
        }

        private void ListOfWorkersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ListOfWorkersList.SelectedItem is Volunteer)
                volunteerMainUI.UpdateSmallVolunteerOverview((Volunteer)ListOfWorkersList.SelectedItem);
        }

        private void _addWorkerButton_Clicked(object sender, EventArgs e)
        {
            AddWorkerManuallyButtonPopUp workerForm = new AddWorkerManuallyButtonPopUp(volunteerMainUI, shift);
            workerForm.ShowDialog();

            bindingSource.DataSource = null;
            bindingSource.DataSource = shift.ListOfWorkers;
        }

        private void DeleteButton_clicked(object sender, EventArgs e)
        {
            DeleteFormPopUp deletePopup = new DeleteFormPopUp("Are you sure you want to delete this shift?");
            deletePopup.ShowDialog();
            if (deletePopup.DialogResult == DialogResult.OK)
            {
                //View the email preview window
                List<WorkerShiftPair> list = new List<WorkerShiftPair>();
                shift.ListOfWorkers.ForEach(x => list.Add(new WorkerShiftPair(x, shift)));
                string StandardMessage = Notifier.GetStandardVolunteerDeletedShiftMessage;
                InformVolunteerEmailBeforeSendUI emailPopup = new InformVolunteerEmailBeforeSendUI(StandardMessage, list.ToArray());
                if (list.Count != 0)
                    emailPopup.ShowDialog();
                if (emailPopup.DialogResult == DialogResult.OK || list.Count == 0)
                {
                    //View the email preview window
                    string StandardMessageRequest = Notifier.GetStandardVolunteerDeniedShiftMessage;
                    List<Request> listOfRequests = shift.ListOfRequests;
                    InformVolunteerEmailBeforeSendUI emailPopupRequest = new InformVolunteerEmailBeforeSendUI(StandardMessageRequest, listOfRequests.ToArray());
                    if (listOfRequests.Count != 0)
                        emailPopupRequest.ShowDialog();
                    if (emailPopupRequest.DialogResult == DialogResult.OK || listOfRequests.Count == 0)
                    {
                        foreach (Request request in listOfRequests)
                            volunteerMainUI.GetScheduleController().DenyRequest(request);
                        volunteerMainUI.GetScheduleController().DeleteShift(shift);
                        volunteerMainUI.UpdateAllHomepage();
                        this.Close();
                    }
                    else
                    {
                        DeleteFormPopUp emailNotsentPopup = new DeleteFormPopUp("The shift is not removed and no emails has been sent\nIf you want to delete the shift,\nthen you must press \"Send email\"");
                        emailNotsentPopup.ShowDialog();
                    }
                }
                else
                {
                    DeleteFormPopUp emailNotsentPopup = new DeleteFormPopUp("The shift is not removed and no emails has been sent\nIf you want to delete the shift,\nthen you must press \"Send email\"");
                    emailNotsentPopup.ShowDialog();
                }
            }
        }

        private void EditShift_Clicked(object sender, EventArgs e)
        {
            List<string> list = volunteerMainUI.GetScheduleController().GetAllTasks();

            EditShiftUI editShiftUI = new EditShiftUI(list, shift);
            editShiftUI.ShowDialog();

            //Det fucking langsomt men what ever.
            string task = shift.Task;
            int time = shift.StartTime.Day;
            if (editShiftUI.DialogResult == DialogResult.OK)
            {
                volunteerMainUI.GetScheduleController().EditShift(shift, editShiftUI.Result);
                this.Close();
            }
            if (task != shift.Task || time != shift.StartTime.Day)
            {
                volunteerMainUI.UpdateSchedule();
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}

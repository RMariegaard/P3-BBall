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

namespace VolunteerSystem.UserInterfaceAdmin
{
    public partial class PressedOnShiftPopup : Form
    {
        Shift shift;
        Panel _pressedOnShiftPopupMainPanel;
        Size fullClientSize;
        IVolunteerMainUI volunteerMainUI;


        public PressedOnShiftPopup(Shift shift, IVolunteerMainUI volunteerMainUI)
        {
            InitializeComponent();
            Width = 400;
            Height = 400;

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
                e.Value = $" Task: {shift.Task}\n Starts: {shift.StartTime.ToString("dd/MM/yyyy hh:mm")}\n Ends: {shift.EndTime.ToString("dd/MM/yyyy hh:mm")}\n Volunteers Needed: {shift.VolunteersNeeded}\n Description: {shift.Description}\n";
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

            ListBox workersList = new ListBox
            {
                Text = "Workers",
                Size = new Size(300, 100),
                Location = new Point(5, workerLabel.Location.Y + workerLabel.GetPreferredSize(Size.Empty).Height),
                BorderStyle = BorderStyle.FixedSingle
            };
            workersList.BeginUpdate();
            foreach (var w in shift.Workers)
            {
                if( w is Volunteer)
                {
                    workersList.Items.Add($"{((Volunteer)w).Assosiation} - {w.Name} - {w.Email}");
                }

            }
            workersList.EndUpdate();
            workersList.AutoSize = true;

            Label requestLabel = new Label
            {
                Location = new Point(0, workersList.Location.Y + workersList.Height),
                MaximumSize = new Size(300, 0),
                Text = "Requests:",
                AutoSize = true
            };

            ListBox requestsList = new ListBox
            {
                Text = "Requests",
                Size = new Size(300, 100),
                Location = new Point(5, requestLabel.Location.Y + requestLabel.PreferredSize.Height),
                BorderStyle = BorderStyle.FixedSingle
            };
            requestsList.BeginUpdate();
            foreach (var r in shift.Requests)
            {
               requestsList.Items.Add($"{r.TimeSent.ToString("dd/MM/yyyy hh:mm")} - {r.Worker.Name} - {r.Worker.Email}");
            }
            requestsList.EndUpdate();
            requestsList.AutoSize = true;

            Button addWorkerButton = new Button()
            {

                Location = new Point(workersList.Location.X + workersList.Width + 5, workersList.Location.Y + workersList.Height - 60 - 5),
                Image = ResizeImage(System.Drawing.Image.FromFile(Directory.GetCurrentDirectory() + "\\addVolunteerImage.PNG"), 25, 25),
                Size = new Size(35, 35),
            };



            _pressedOnShiftPopupMainPanel.Controls.Add(workerLabel);
            _pressedOnShiftPopupMainPanel.Controls.Add(cancelButton);
            _pressedOnShiftPopupMainPanel.Controls.Add(addWorkerButton);
            //hvorfor virker det kun når jeg tilføger den til this og ikke mainPanel???
           this.Controls.Add(workersList);
            this.Controls.Add(requestsList);
            this.Controls.Add(requestLabel);
            this.Controls.Add(shiftInfo);
            this.Controls.Add(editShiftButton);
            this.Controls.Add(deleteButton);
            Controls.Add(_pressedOnShiftPopupMainPanel);
        }

        private void DeleteButton_clicked(object sender, EventArgs e)
        {
            DeleteFormPopUp deletePopup = new DeleteFormPopUp("Are you sure you want to delete this shift?");
            deletePopup.ShowDialog();
            if(deletePopup.DialogResult == DialogResult.OK)
            {
                this.Close();
                volunteerMainUI.GetScheduleController().DeleteShift(shift.ID);
                volunteerMainUI.UpdateSchedule();
   
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
                volunteerMainUI.GetScheduleController().EditShift(shift.ID, editShiftUI.Result);
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

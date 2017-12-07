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
    public partial class EditShiftUI : CreateNewShiftUI
    {
        private Shift shift;
        public EditShiftUI(List<string> tasks, Shift shift) : base(tasks)
        {
            InitializeComponent();
            this.shift = shift;

            this.Icon = new Icon(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\TwoMen.ico");
            this.Text = "Volunteer Manegement System: Edit shift";

            StartPosition = FormStartPosition.CenterParent;
            desciptionTextBox.Text = shift.Description;
            startHourMinutTextBox.Text = shift.StartTime.Hour + ":" + shift.StartTime.Minute;
            endHourMinutTextBox.Text = shift.EndTime.Hour + ":" + shift.EndTime.Minute;
            numberOfVolunteersTextBox.Text = shift.VolunteersNeeded.ToString();
            TasksComboBox.SelectedItem = shift.Task;
            startDateTimePicker.Value = shift.StartTime;
            endDateTimePicker.Value = shift.EndTime;
            createShiftButton.Text = "Save";


        }
        protected override void createShift_Clicked(object sender, EventArgs e)
        {
            if (correctInformation())
            {
                DateTime startDateTime = new DateTime(startDateTimePicker.Value.Year, startDateTimePicker.Value.Month, startDateTimePicker.Value.Day, startHour, startMinut, 0);
                DateTime endDateTime = new DateTime(endDateTimePicker.Value.Year, endDateTimePicker.Value.Month, endDateTimePicker.Value.Day, endHour, endMinut, 0);
                if (startDateTime < endDateTime)
                {
                    Result = new Shift(startDateTime, endDateTime, TasksComboBox.SelectedItem.ToString(), int.Parse(numberOfVolunteersTextBox.Text), desciptionTextBox.Text);
                    DialogResult = DialogResult.OK;
 
                }
                else
                {

                    PopupUI popup = new PopupUI("Invalid information", "The start of the shift has to be before the end of shift");
                    popup.Show();
                }
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            desciptionTextBox.Text = shift.Description;
            startHourMinutTextBox.Text = shift.StartTime.Hour + ":" + shift.StartTime.Minute;
            endHourMinutTextBox.Text = shift.EndTime.Hour + ":" + shift.EndTime.Minute;
            numberOfVolunteersTextBox.Text = shift.VolunteersNeeded.ToString();
            TasksComboBox.SelectedItem = shift.Task;
            DateTimePicker.Value = shift.StartTime;
            createShiftButton.Text = "Save";


        }

      

    }
}

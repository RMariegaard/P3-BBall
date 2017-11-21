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
    public partial class CreateNewShiftUI : Form
    {
        private Label desciptionLabel;
        private Label endTimeLabel;
        private Label startTimeLabel;
        private Label dateLabel;
        private Label TaskLabel;
        private Label numberOfVolunteersLabel;

        private TextBox desciptionTextBox;
        private TextBox numberOfVolunteersTextBox;
        private TextBox startHourMinutTextBox;
        private TextBox endHourMinutTextBox;
        private ComboBox TasksComboBox;
        private DateTimePicker DateTimePicker;

        private Button createShiftButton;
        private Button regretButton;

        int startHour;
        int startMinut;

        int endHour;
        int endMinut;

        public Shift Result;

        public CreateNewShiftUI(List<String> tasks)
        {
            InitializeComponent();

            Height = 300;
            Width = 500;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            FormBorderStyle = FormBorderStyle.FixedDialog;

            int heightBewtweenElements = 30;
            int YForLabels = 10;
            int YForTextBoxandStuff = 160;

            int numberOfVolunteerPosistion = 1;
            int taskPosition = 2;
            int startTimePosition = 3;
            int endTimePosition = 4;
            int datePickerPosition = 5;
            int desciptionPosition = 6;
            int buttonPosition = 7;

            //Desciption - label
            desciptionLabel = new Label();
            desciptionLabel.Text = "Desiption: ";
            desciptionLabel.Location = new Point(YForLabels, (heightBewtweenElements * desciptionPosition) + 10);
            desciptionLabel.AutoSize = true;

            //End Time  - Label
            endTimeLabel = new Label();
            endTimeLabel.Text = "End time: (F.eks.: '12:00')";
            endTimeLabel.Location = new Point(YForLabels, (heightBewtweenElements * endTimePosition) + 10);
            endTimeLabel.AutoSize = true;

            //Start Time  - Label
            startTimeLabel = new Label();
            startTimeLabel.Text = "Time of start: (F.eks.: '8:30')";
            startTimeLabel.Location = new Point(YForLabels, (heightBewtweenElements * startTimePosition) + 10);
            startTimeLabel.AutoSize = true;

            //Task  - Label
            TaskLabel = new Label();
            TaskLabel.Text = "Task: ";
            TaskLabel.Location = new Point(YForLabels, (heightBewtweenElements * taskPosition) + 10);
            TaskLabel.AutoSize = true;

            //Date  - label
            dateLabel = new Label();
            dateLabel.Text = "Select the date: ";
            dateLabel.Location = new Point(YForLabels, (heightBewtweenElements * datePickerPosition) + 10);
            dateLabel.AutoSize = true;

            //Number of volunteer  - Label
            numberOfVolunteersLabel = new Label();
            numberOfVolunteersLabel.Text = "Max number of volunteers: ";
            numberOfVolunteersLabel.Location = new Point(YForLabels, (heightBewtweenElements * numberOfVolunteerPosistion) + 10);
            numberOfVolunteersLabel.AutoSize = true;
            
            //Desciption - Textbox
            desciptionTextBox = new TextBox();
            desciptionTextBox.Location = new Point(YForTextBoxandStuff, (heightBewtweenElements * desciptionPosition) + 10);
            desciptionTextBox.Size = new Size(200, desciptionTextBox.Size.Height);

            //Start time - textbox
            startHourMinutTextBox = new TextBox();
            startHourMinutTextBox.Location = new Point(YForTextBoxandStuff, (heightBewtweenElements * startTimePosition) + 10);
            startHourMinutTextBox.Size = new Size(200, desciptionTextBox.Size.Height);

            //End time - textbox
            endHourMinutTextBox = new TextBox();
            endHourMinutTextBox.Location = new Point(YForTextBoxandStuff, (heightBewtweenElements * endTimePosition) + 10);
            endHourMinutTextBox.Size = new Size(200, desciptionTextBox.Size.Height);

            //Number of volunteers  - Textbox
            numberOfVolunteersTextBox = new TextBox();
            numberOfVolunteersTextBox.Location = new Point(YForTextBoxandStuff, (heightBewtweenElements * numberOfVolunteerPosistion) + 10);
            numberOfVolunteersTextBox.Size = new Size(200, desciptionTextBox.Size.Height);

            //Task  - comboBox
            TasksComboBox = new ComboBox();
            TasksComboBox.Location = new Point(YForTextBoxandStuff, (heightBewtweenElements * taskPosition) + 10);
            foreach (string task in tasks)
                TasksComboBox.Items.Add(task);
            TasksComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TasksComboBox.Size = new Size(200, desciptionTextBox.Size.Height);

            //Date  - DateTimePicker
            DateTimePicker = new DateTimePicker();
            DateTimePicker.Location = new Point(YForTextBoxandStuff, (heightBewtweenElements * datePickerPosition) + 10);
            
            //Regret  - Button
            regretButton = new Button();
            regretButton.Location = new Point(YForLabels, (heightBewtweenElements * buttonPosition) + 10);
            regretButton.Text = "Cancel";
            regretButton.AutoSize = true;
            regretButton.DialogResult = DialogResult.Cancel;

            //Create Shift - Button
            createShiftButton = new Button();
            createShiftButton.Location = new Point(regretButton.Location.Y + regretButton.Size.Width + 10, (heightBewtweenElements * buttonPosition) + 10);
            createShiftButton.Text = "Create Shift";
            createShiftButton.AutoSize = true;
            createShiftButton.Click += createShift_Clicked;

            //Adds to the window
            Controls.Add(desciptionLabel);
            Controls.Add(dateLabel);
            Controls.Add(endTimeLabel);
            Controls.Add(startTimeLabel);
            Controls.Add(TaskLabel);
            Controls.Add(numberOfVolunteersLabel);

            Controls.Add(desciptionTextBox);
            Controls.Add(DateTimePicker);
            Controls.Add(numberOfVolunteersTextBox);
            Controls.Add(TasksComboBox);
            Controls.Add(endHourMinutTextBox);
            Controls.Add(startHourMinutTextBox);

            Controls.Add(createShiftButton);
            Controls.Add(regretButton);
        }

        private void createShift_Clicked(object sender, EventArgs e)
        {
            if (correctInformation())
            {
                DateTime startDateTime = new DateTime(DateTimePicker.Value.Year, DateTimePicker.Value.Month, DateTimePicker.Value.Day, startHour, startMinut, 0);
                DateTime endDateTime = new DateTime(DateTimePicker.Value.Year, DateTimePicker.Value.Month, DateTimePicker.Value.Day, endHour, endMinut, 0);

                Result = new Shift(startDateTime, endDateTime, TasksComboBox.SelectedItem.ToString(), int.Parse(numberOfVolunteersTextBox.Text), desciptionTextBox.Text);
                DialogResult = DialogResult.OK;
            }
        }
        

        private bool correctInformation()
        {
            //Check tasks comboBox
            if (TasksComboBox.SelectedItem == null)
            {
                PopupUI popup = new PopupUI("Uncorrect information", "Please select the task");
                popup.Show();
                return false;
            }

            //Check number of volunteers
            if (!numberOfVolunteersTextBox.Text.All(x => char.IsNumber(x)) || numberOfVolunteersTextBox.Text == "" || numberOfVolunteersTextBox == null)
            {
                PopupUI popup = new PopupUI("Uncorrect information", "Number of volunteers field, must be a number, and not start with '0'.");
                popup.Show();
                return false;
            }

            //Check start Time
            string[] startTextBoxSplittet= startHourMinutTextBox.Text.Split(':');
            if (startTextBoxSplittet.Count() == 2)
            {
                if (!int.TryParse(startTextBoxSplittet[0], out startHour))
                {
                    PopupUI popup = new PopupUI("Uncorrect information", "The hours in start time must be numbers");
                    popup.Show();
                    return false;
                }
                if (!int.TryParse(startTextBoxSplittet[1], out startMinut))
                {
                    PopupUI popup = new PopupUI("Uncorrect information", "The minuts in start time must be numbers");
                    popup.Show();
                    return false;
                }
            }
            else
            {
                PopupUI popup = new PopupUI("Uncorrect information", "Start time must be of the format xx:xx, and only contain numbers, apart from the kolon that seperates the hours from the minuts");
                popup.Show();
                return false;
            }

            //Check end Time
            string[] endTextBoxSplittet = endHourMinutTextBox.Text.Split(':');
            if (endTextBoxSplittet.Count() == 2)
            {
                if (!int.TryParse(endTextBoxSplittet[0], out endHour))
                {
                    PopupUI popup = new PopupUI("Uncorrect information", "The hours in end time must be numbers");
                    popup.Show();
                    return false;
                }
                if (!int.TryParse(endTextBoxSplittet[1], out endMinut))
                {
                    PopupUI popup = new PopupUI("Uncorrect information", "The minuts in end time must be numbers");
                    popup.Show();
                    return false;
                }
            }
            else
            {
                PopupUI popup = new PopupUI("Uncorrect information", "End time must be of the format xx:xx, and only contain numbers, apart from the kolon that seperates the hours from the minuts");
                popup.Show();
                return false;
            }

            return true;
        }
    }
}

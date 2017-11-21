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
        private Label timeLabel;
        private Label TaskLabel;
        private Label numberOfVolunteersLabel;

        private TextBox desciptionTextBox;
        private TextBox numberOfVolunteersTextBox;
        private ComboBox TasksComboBox;
        private DateTimePicker startTimeTimePicker;
        private DateTimePicker endTimeTimePicker;

        private Button createShiftButton;
        private Button regretButton;

        public Shift Result;

        public CreateNewShiftUI(List<String> Tasks)
        {
            InitializeComponent();

            Height = 300;
            Width = 400;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            FormBorderStyle = FormBorderStyle.FixedDialog;

            int heightBewtweenElements = 30;
            int YForLabels = 10;
            int YForTextBoxandStuff = 160;

            desciptionLabel = new Label();
            desciptionLabel.Text = "Desiption: ";
            desciptionLabel.Location = new Point(YForLabels, (heightBewtweenElements * 1) + 10);
            desciptionLabel.AutoSize = true;

            endTimeLabel = new Label();
            endTimeLabel.Text = "End time: ";
            endTimeLabel.Location = new Point(YForLabels, (heightBewtweenElements * 3) + 10);
            endTimeLabel.AutoSize = true;

            timeLabel = new Label();
            timeLabel.Text = "Time of start: ";
            timeLabel.Location = new Point(YForLabels, (heightBewtweenElements * 2) + 10);
            timeLabel.AutoSize = true;

            TaskLabel = new Label();
            TaskLabel.Text = "Task: ";
            TaskLabel.Location = new Point(YForLabels, (heightBewtweenElements * 4) + 10);
            TaskLabel.AutoSize = true;

            numberOfVolunteersLabel = new Label();
            numberOfVolunteersLabel.Text = "Max number of volunteers: ";
            numberOfVolunteersLabel.Location = new Point(YForLabels, (heightBewtweenElements * 5) + 10);
            numberOfVolunteersLabel.AutoSize = true;

            desciptionTextBox = new TextBox();
            desciptionTextBox.Location = new Point(YForTextBoxandStuff, (heightBewtweenElements * 1) + 10);
            desciptionTextBox.Size = new Size(200, desciptionTextBox.Size.Height);
            
            numberOfVolunteersTextBox = new TextBox();
            numberOfVolunteersTextBox.Location = new Point(YForTextBoxandStuff, (heightBewtweenElements * 5) + 10);
            numberOfVolunteersTextBox.Size = new Size(200, desciptionTextBox.Size.Height);

            TasksComboBox = new ComboBox();
            TasksComboBox.Location = new Point(YForTextBoxandStuff, (heightBewtweenElements * 4) + 10);
            foreach (string task in Tasks)
                TasksComboBox.Items.Add(task);
            TasksComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            TasksComboBox.Size = new Size(200, desciptionTextBox.Size.Height);

            startTimeTimePicker = new DateTimePicker();
            startTimeTimePicker.Location = new Point(YForTextBoxandStuff, (heightBewtweenElements * 2) + 10);

            endTimeTimePicker = new DateTimePicker();
            endTimeTimePicker.Location = new Point(YForTextBoxandStuff, (heightBewtweenElements * 3) + 10);

            regretButton = new Button();
            regretButton.Location = new Point(YForLabels, (heightBewtweenElements * 6) + 10);
            regretButton.Text = "Cancel";
            regretButton.AutoSize = true;
            regretButton.DialogResult = DialogResult.Cancel;

            createShiftButton = new Button();
            createShiftButton.Location = new Point(regretButton.Location.Y + regretButton.Size.Width + 10, (heightBewtweenElements * 6) + 10);
            createShiftButton.Text = "Create Shift";
            createShiftButton.AutoSize = true;
            createShiftButton.Click += createShift_Clicked;

            Controls.Add(desciptionLabel);
            Controls.Add(endTimeLabel);
            Controls.Add(timeLabel);
            Controls.Add(TaskLabel);
            Controls.Add(numberOfVolunteersLabel);
            Controls.Add(desciptionTextBox);
            Controls.Add(endTimeTimePicker);
            Controls.Add(numberOfVolunteersTextBox);
            Controls.Add(TasksComboBox);
            Controls.Add(startTimeTimePicker);
            Controls.Add(createShiftButton);
            Controls.Add(regretButton);
        }

        private void createShift_Clicked(object sender, EventArgs e)
        {
            if (correctInformation())
            {
                Result = new Shift(new DateTime(2017, 5, 15, 3, 0, 0), new DateTime(2017, 5, 15, 6, 0, 0), TasksComboBox.SelectedItem.ToString(), int.Parse(numberOfVolunteersTextBox.Text), desciptionTextBox.Text);
                //DateTime test = endTimeTimePicker;
                DialogResult = DialogResult.OK;
            }
            else
            {
                PopupUI popup = new PopupUI("Uncorrect information", "One or more fields is incorretly filled. Please adress this.");
                popup.Show();
            }
        }

        private bool correctInformation()
        {
            //return true;
            if (TasksComboBox.SelectedItem == null)
                return false;
            if (!numberOfVolunteersTextBox.Text.All(x => char.IsNumber(x)) || numberOfVolunteersTextBox.Text == "" || numberOfVolunteersTextBox == null)
                return false;

            return true;
        }
    }
}

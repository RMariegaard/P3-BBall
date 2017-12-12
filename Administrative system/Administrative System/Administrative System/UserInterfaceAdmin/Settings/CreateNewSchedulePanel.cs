using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.Settings
{
    public class CreateNewSchedulePanel : Panel
    {

        private Button _createScheduleButton;
        private Label _yearLabel;
        private TextBox _yearInputTextBox;
        private IVolunteerMainUI _mainUI;

        public CreateNewSchedulePanel(IVolunteerMainUI mainUI)
        {
            _mainUI = mainUI;
            

            _yearLabel = new Label()
            {
                Text = "New Schedule Year:",
                Location = new System.Drawing.Point(5, 20),
                AutoSize = true
            };
            _yearInputTextBox = new TextBox()
            {
                Location = new System.Drawing.Point(_yearLabel.Location.X + _yearLabel.Width + 10, _yearLabel.Location.Y)
            };
            _createScheduleButton = new Button()
            {
                Text = "Create New Schedule",
                Location = new System.Drawing.Point(_yearInputTextBox.Location.X, _yearInputTextBox.Location.Y + _yearInputTextBox.Height + 5),
                AutoSize = true
            };
            _createScheduleButton.Click += CreateSchedule;
            this.Controls.Add(_yearLabel);
            this.Controls.Add(_yearInputTextBox);
            this.Controls.Add(_createScheduleButton);
        }

        private void CreateSchedule(object sender, EventArgs e)
        {
            int year;
            if (int.TryParse(_yearInputTextBox.Text, out year))
            {

                WrongEmailWarning information = new WrongEmailWarning("You have to open and close the program\nbefore the new schedule shows up!");
                information.ShowDialog();
                _mainUI.GetScheduleController().CreateSchedule(year);

            }
            else
            {
                WrongEmailWarning inValidInput = new WrongEmailWarning("You entered an invalid year");
                inValidInput.ShowDialog();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace VolunteerSystem.UserInterfaceAdmin.VolunteerOverview.CreateVolunteerStuff
{
    class CreateVolunteerClass : INotifyPropertyChanged
    {
        Panel createVolunteerMainPanel;
        WorkerController workerController;
        List<LabelAndTextBox> labelAndTextBoxList;
        CheckBox externalCheckBox;
        VolunteerOverview volunteerOverview;

        public event PropertyChangedEventHandler PropertyChanged;

        public CreateVolunteerClass(WorkerController workerController, VolunteerOverview volunteerOverview)
        {
            this.workerController = workerController;
            this.volunteerOverview = volunteerOverview;
            createVolunteerMainPanel = new Panel();
        }

        public Panel GetPanel(Size size)
        {
            return getThePanels(size);
        }

        private Panel getThePanels(Size size)
        {
            Panel panel = new Panel()
            {
                Location = new Point(0, 0),
                BorderStyle = BorderStyle.FixedSingle,
                Size = size
            };


            Label externalWorkerLabel = new Label
            {
                Location = new Point(5, 5),
                Text = "Check This If It Is An External Worker: ",
                AutoSize = true,
            };
            panel.Controls.Add(externalWorkerLabel);

            externalCheckBox = new CheckBox()
            {
                Location = new Point(externalWorkerLabel.Location.X + externalWorkerLabel.Width + 5, 5),
                AutoSize = true,
            };
            
            externalCheckBox.Click += ExternalCheckBox_Click;
            panel.Controls.Add(externalCheckBox);

            //The labels and textboxes
            labelAndTextBoxList = new List<LabelAndTextBox>();
            int labelandTextBoxHeight = 40;
            LabelAndTextBox nameLabelandTextBox = new LabelAndTextBox("Name");
            panel.Controls.Add(nameLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 1) + 2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(nameLabelandTextBox);

            LabelAndTextBox emailLabelandTextBox = new LabelAndTextBox("Email");
            panel.Controls.Add(emailLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 2) + 2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(emailLabelandTextBox);

            BindingSource teamBinding = new BindingSource { DataSource = externalCheckBox };


            LabelAndTextBox teamLabelandTextBox = new LabelAndTextBox("Team");
            var binding = new Binding("Enabled", teamBinding, "Checked");
            binding.Format += delegate (object s, ConvertEventArgs e)
            {
                e.Value = !(bool)e.Value;
            };
            binding.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
            panel.Controls.Add(teamLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 3) + 2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            teamLabelandTextBox.TextBox.DataBindings.Add(binding);
            labelAndTextBoxList.Add(teamLabelandTextBox);
            
            
            LabelAndTextBox ageLabelandTextBox = new LabelAndTextBox("Age");
            var ageBinding = new Binding("Enabled", teamBinding, "Checked");
            ageBinding.Format += delegate (object s, ConvertEventArgs e)
            {
                e.Value = !(bool)e.Value;
            };
            panel.Controls.Add(ageLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 4)+2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            ageLabelandTextBox.TextBox.DataBindings.Add(ageBinding);
            labelAndTextBoxList.Add(ageLabelandTextBox);

            LabelAndTextBox phoneLabelandTextBox = new LabelAndTextBox("Phone Number");
            var phoneBinding = new Binding("Enabled", teamBinding, "Checked");
            phoneBinding.Format += delegate (object s, ConvertEventArgs e)
            {
                e.Value = !(bool)e.Value;
            };
            panel.Controls.Add(phoneLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 5)+2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            phoneLabelandTextBox.TextBox.DataBindings.Add(phoneBinding);
            labelAndTextBoxList.Add(phoneLabelandTextBox);
            
            LabelAndTextBox passwordLabelandTextBox = new LabelAndTextBox("Password");
            var passwordBinding = new Binding("Enabled", teamBinding, "Checked");
            passwordBinding.Format += delegate (object s, ConvertEventArgs e)
            {
                e.Value = !(bool)e.Value;
            };
            panel.Controls.Add(passwordLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 6)+2), new Point(2, 2), new Point((panel.Width /10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            passwordLabelandTextBox.TextBox.DataBindings.Add(passwordBinding);
            labelAndTextBoxList.Add(passwordLabelandTextBox);
            

            //Button
            Button createButton = new Button
            {
                Size = new Size(panel.Size.Width-10, 40),
                Location = new Point(5, panel.Height - 45),
                Text = "Create Volunteer"
            };
            createButton.Click += CreateButton_Click;
            panel.Controls.Add(createButton);

            return panel;
        }

        private void ExternalCheckBox_Click(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(externalCheckBox.Checked, new PropertyChangedEventArgs("GetNumberOfVolunteers"));
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (externalCheckBox.Checked)
            {
                string name = labelAndTextBoxList.First(x => x.Label.Text == "Name").TextBox.Text;
                string email = labelAndTextBoxList.First(x => x.Label.Text == "Email").TextBox.Text;
                ExternalWorker externalWorker = new ExternalWorker(name, email)
                {

                };

                workerController.CreateWorker(externalWorker);
                volunteerOverview.UpdateCreateVolunteerElement();
                volunteerOverview.UpdateSeachAndVolunteerElement();
            }
            else
            {
                string name = labelAndTextBoxList.First(x => x.Label.Text == "Name").TextBox.Text;
                string email = labelAndTextBoxList.First(x => x.Label.Text == "Email").TextBox.Text;
                string team = labelAndTextBoxList.First(x => x.Label.Text == "Team").TextBox.Text;

                Volunteer volunteer = new Volunteer(name, email, team)
                {  

                };

                workerController.CreateWorker(volunteer);
                volunteerOverview.UpdateCreateVolunteerElement();
                volunteerOverview.UpdateSeachAndVolunteerElement();
            }
        }
    }

    class LabelAndTextBox
    {
        private string labelText;
        public Label Label;
        public TextBox TextBox;

        public LabelAndTextBox(string labelText)
        {
            this.labelText = labelText;
        }

        public Panel GetLabelAndTextPanel(Point panelLocation, Point labelLocation, Point TextBoxLocation, Size sizeTotal)
        {
            Panel labelandTextBoxPanel = new Panel()
            {
                Location = panelLocation,
                Size = sizeTotal
            };

            this.Label = new Label
            {
                Text = labelText,
                Location = labelLocation,
                AutoSize = true,
            };

            this.TextBox = new TextBox
            {
                Location = TextBoxLocation,
                Size = new Size(sizeTotal.Width - TextBoxLocation.X - 10, sizeTotal.Height),
            };

            labelandTextBoxPanel.Controls.Add(Label);
            labelandTextBoxPanel.Controls.Add(TextBox);

            return labelandTextBoxPanel;
        }
    }
}

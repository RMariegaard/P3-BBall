﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.VolunteerOverview.CreateVolunteerStuff
{
    class CreateVolunteerClass
    {
        Panel createVolunteerMainPanel;
        WorkerController workerController;
        List<LabelAndTextBox> labelAndTextBoxList;
        CheckBox externalCheckBox;
        VolunteerOverview volunteerOverview;
        Panel thePanel;

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
        Panel panel;
        private Panel getThePanels(Size size)
        {
            panel = new Panel()
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
            externalCheckBox.CheckedChanged += ExternalCheckBox_CheckedChanged;
            panel.Controls.Add(externalCheckBox);

            //Textbox and labels
            updateThePanel();

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
        
        private void updateThePanel()
        {
            panel.Controls.Remove(thePanel);
            thePanel = new Panel()
            {
                Location = new Point(0, externalCheckBox.Location.Y + externalCheckBox.Size.Height + 5),
                Size = new Size(panel.Width, panel.Height - externalCheckBox.Location.Y - externalCheckBox.Height - 50),
            };
            thePanel.Controls.Add(AddTextBoxAndLabels(thePanel.Size));

            panel.Controls.Add(thePanel);

        }

        private Panel AddTextBoxAndLabels(Size size)
        {
            Panel labelTextBoxPanel = new Panel()
            {
                Size = size,
                Location = new Point(0, 0)
            };

            //The labels and textboxes
            labelAndTextBoxList = new List<LabelAndTextBox>();
            int labelandTextBoxHeight = 40;
            LabelAndTextBox nameLabelandTextBox = new LabelAndTextBox("Name");
            labelTextBoxPanel.Controls.Add(nameLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 0) + 2), new Point(2, 2), new Point((labelTextBoxPanel.Width / 10) * 3, 2), new Size(labelTextBoxPanel.Width, labelandTextBoxHeight), true, externalCheckBox.Checked));
            labelAndTextBoxList.Add(nameLabelandTextBox);

            LabelAndTextBox emailLabelandTextBox = new LabelAndTextBox("Email");
            labelTextBoxPanel.Controls.Add(emailLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 1) + 2), new Point(2, 2), new Point((labelTextBoxPanel.Width / 10) * 3, 2), new Size(labelTextBoxPanel.Width, labelandTextBoxHeight), true, externalCheckBox.Checked));
            labelAndTextBoxList.Add(emailLabelandTextBox);

            LabelAndTextBox teamLabelandTextBox = new LabelAndTextBox("Team");
            labelTextBoxPanel.Controls.Add(teamLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 2) + 2), new Point(2, 2), new Point((labelTextBoxPanel.Width / 10) * 3, 2), new Size(labelTextBoxPanel.Width, labelandTextBoxHeight), false, externalCheckBox.Checked));
            labelAndTextBoxList.Add(teamLabelandTextBox);
            
            LabelAndTextBox ageLabelandTextBox = new LabelAndTextBox("Age");
            labelTextBoxPanel.Controls.Add(ageLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 3)+2), new Point(2, 2), new Point((labelTextBoxPanel.Width / 10) * 3, 2), new Size(labelTextBoxPanel.Width, labelandTextBoxHeight), false, externalCheckBox.Checked));
            labelAndTextBoxList.Add(ageLabelandTextBox);

            LabelAndTextBox phoneLabelandTextBox = new LabelAndTextBox("Phone Number");
            labelTextBoxPanel.Controls.Add(phoneLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 4)+2), new Point(2, 2), new Point((labelTextBoxPanel.Width / 10) * 3, 2), new Size(labelTextBoxPanel.Width, labelandTextBoxHeight), false, externalCheckBox.Checked));
            labelAndTextBoxList.Add(phoneLabelandTextBox);
            
            LabelAndTextBox passwordLabelandTextBox = new LabelAndTextBox("Password");
            labelTextBoxPanel.Controls.Add(passwordLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 5)+2), new Point(2, 2), new Point((labelTextBoxPanel.Width /10) * 3, 2), new Size(labelTextBoxPanel.Width, labelandTextBoxHeight), false, externalCheckBox.Checked));
            labelAndTextBoxList.Add(passwordLabelandTextBox);
            
            return labelTextBoxPanel;
        }

        private void ExternalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            updateThePanel();
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

        public Panel GetLabelAndTextPanel(Point panelLocation, Point labelLocation, Point TextBoxLocation, Size sizeTotal, bool alsoExternalWorker, bool checkState)
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
                Enabled = true,
            };
            if (!alsoExternalWorker && checkState)
                TextBox.Enabled = false;

            labelandTextBoxPanel.Controls.Add(Label);
            labelandTextBoxPanel.Controls.Add(TextBox);

            return labelandTextBoxPanel;
        }
    }
}

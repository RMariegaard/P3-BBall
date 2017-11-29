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

            LabelAndTextBox teamLabelandTextBox = new LabelAndTextBox("Team");
            panel.Controls.Add(teamLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 3) + 2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(teamLabelandTextBox);
            
            /*
            LabelAndTextBox ageLabelandTextBox = new LabelAndTextBox("Age");
            panel.Controls.Add(ageLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 4)+2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(ageLabelandTextBox);

            LabelAndTextBox phoneLabelandTextBox = new LabelAndTextBox("Phone Number");
            panel.Controls.Add(phoneLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 5)+2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(phoneLabelandTextBox);
            
            LabelAndTextBox passwordLabelandTextBox = new LabelAndTextBox("Password");
            panel.Controls.Add(passwordLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 6)+2), new Point(2, 2), new Point((panel.Width /10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(passwordLabelandTextBox);
            */

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

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (externalCheckBox.Checked)
            {

            }
            else
            {
                string name = labelAndTextBoxList.First(x => x.Label.Text == "Name").TextBox.Text;
                string email = labelAndTextBoxList.First(x => x.Label.Text == "Email").TextBox.Text;
                string team = labelAndTextBoxList.First(x => x.Label.Text == "Team").TextBox.Text;

                Volunteer volunteer = new Volunteer(name, email, team)
                {
                    
                };

                if (volunteer == null)
                {
                    throw new Exception("Noooo");
                }
                else
                {
                    workerController.CreateWorker(volunteer);
                }
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem;
using System.Windows.Forms;
using System.Drawing;
using VolunteerSystem.UserInterfaceAdmin;

namespace VolunteerPrototype.UI
{
    public class AccountSettings
    {
        Panel createVolunteerMainPanel;
        List<LabelAndTextBox> labelAndTextBoxList;
        private IUI _mainUI;
        private Volunteer _volunteer;


        public AccountSettings(IUI mainUI)
        {
            _mainUI = mainUI;
            _volunteer = _mainUI.GetCurrentUser;
            createVolunteerMainPanel = new Panel();
        }

        public Panel GetPanel(Size size)
        {
            return GetThePanels(size);
        }

        private Panel GetThePanels(Size size)
        {
            Panel panel = new Panel()
            {
                Location = new Point(0, 0),
                BorderStyle = BorderStyle.FixedSingle,
                Size = size
            };



            //The labels and textboxes
            labelAndTextBoxList = new List<LabelAndTextBox>();
            int labelandTextBoxHeight = 40;
            LabelAndTextBox nameLabelandTextBox = new LabelAndTextBox("Name");
            
            panel.Controls.Add(nameLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 1) + 2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(nameLabelandTextBox);
            nameLabelandTextBox.TextBox.Text = _volunteer.Name;

            LabelAndTextBox emailLabelandTextBox = new LabelAndTextBox("Email");
            
            panel.Controls.Add(emailLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 2) + 2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(emailLabelandTextBox);
            emailLabelandTextBox.TextBox.Text = _volunteer.Email;

            LabelAndTextBox teamLabelandTextBox = new LabelAndTextBox("Team");
            
            panel.Controls.Add(teamLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 3) + 2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(teamLabelandTextBox);
            teamLabelandTextBox.TextBox.Text = _volunteer.Association;

            LabelAndTextBox phoneLabelandTextBox = new LabelAndTextBox("Phone Number");
            
            panel.Controls.Add(phoneLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 5) + 2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
  
            labelAndTextBoxList.Add(phoneLabelandTextBox);
            phoneLabelandTextBox.TextBox.Text = _volunteer.Phonenumber.ToString();

            LabelAndTextBox passwordLabelandTextBox = new LabelAndTextBox("New Password");
            panel.Controls.Add(passwordLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 6) + 2), new Point(2, 2), new Point((panel.Width / 10) * 3, 2), new Size(panel.Width, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(passwordLabelandTextBox);


            //Button
            Button updateButton = new Button
            {
                Size = new Size(panel.Size.Width - 10, 40),
                Location = new Point(5, panel.Height - 45),
                Text = "Update"
            };
            updateButton.Click += UpdateButton_Click;
            panel.Controls.Add(updateButton);

            return panel;
        }


        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {

                    string name = labelAndTextBoxList.First(x => x.Label.Text == "Name").TextBox.Text;
                    string email = labelAndTextBoxList.First(x => x.Label.Text == "Email").TextBox.Text;
                    string team = labelAndTextBoxList.First(x => x.Label.Text == "Team").TextBox.Text;
                    int phoneNumber = int.Parse(labelAndTextBoxList.First(x => x.Label.Text == "Phone Number").TextBox.Text);
                    string temppassword = labelAndTextBoxList.First(x => x.Label.Text == "New Password").TextBox.Text;
                string password = temppassword == "" || temppassword == null ? _volunteer.HashPassworkd : temppassword;

                _mainUI.WorkerController().UpdateVolunteer(_mainUI.GetCurrentUser, name, email, team, phoneNumber, password);

            }
            catch (FormatException)
            {
                WrongEmailWarning message = new WrongEmailWarning("Phonenumber is not legal");
                message.StartPosition = FormStartPosition.CenterParent;
                message.ShowDialog();
            }
            catch (Exception)
            {
                WrongEmailWarning message = new WrongEmailWarning("The Email format is not legal, enter a correct email adress.");
                message.StartPosition = FormStartPosition.CenterParent;
                message.ShowDialog();


            }
        }
    }

    public class LabelAndTextBox
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

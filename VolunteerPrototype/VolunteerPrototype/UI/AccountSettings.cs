using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSystem;
using System.Windows.Forms;
using System.Drawing;
using VolunteerSystem.UserInterfaceAdmin;
using VolunteerSystem.Exceptions;

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
            int labelandTextBoxHeight = 50;
            LabelAndTextBox nameLabelandTextBox = new LabelAndTextBox("Name");
            
            panel.Controls.Add(nameLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 1) + 2), new Point(2, 2), new Point((panel.Width / 10) * 1, 2), new Size(panel.Width / 3, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(nameLabelandTextBox);
            nameLabelandTextBox.TextBox.Text = _volunteer.Name;

            LabelAndTextBox emailLabelandTextBox = new LabelAndTextBox("Email");
            
            panel.Controls.Add(emailLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 2) + 2), new Point(2, 2), new Point((panel.Width / 10) * 1, 2), new Size(panel.Width / 3, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(emailLabelandTextBox);
            emailLabelandTextBox.TextBox.Text = _volunteer.Email;

            LabelAndTextBox teamLabelandTextBox = new LabelAndTextBox("Team");
            
            panel.Controls.Add(teamLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 3) + 2), new Point(2, 2), new Point((panel.Width / 10) * 1, 2), new Size(panel.Width / 3, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(teamLabelandTextBox);
            teamLabelandTextBox.TextBox.Text = _volunteer.Association;

            LabelAndTextBox phoneLabelandTextBox = new LabelAndTextBox("Phone Number");
            
            panel.Controls.Add(phoneLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 5) + 2), new Point(2, 2), new Point((panel.Width / 10) * 1, 2), new Size(panel.Width / 3, labelandTextBoxHeight)));
  
            labelAndTextBoxList.Add(phoneLabelandTextBox);
            phoneLabelandTextBox.TextBox.Text = _volunteer.Phonenumber.ToString();

            LabelAndTextBox oldPasswordLabelandTextBox = new LabelAndTextBox("Old Password");
            panel.Controls.Add(oldPasswordLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 6) + 2), new Point(2, 2), new Point((panel.Width / 10) * 1, 2), new Size(panel.Width / 3, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(oldPasswordLabelandTextBox);
            oldPasswordLabelandTextBox.TextBox.UseSystemPasswordChar = true;

            LabelAndTextBox passwordLabelandTextBox = new LabelAndTextBox("New Password");
            
            panel.Controls.Add(passwordLabelandTextBox.GetLabelAndTextPanel(new Point(0, (labelandTextBoxHeight * 7) + 2), new Point(2, 2), new Point((panel.Width / 10) * 1, 2), new Size(panel.Width / 3, labelandTextBoxHeight)));
            labelAndTextBoxList.Add(passwordLabelandTextBox);
            passwordLabelandTextBox.TextBox.UseSystemPasswordChar = true;

            //Button
            Button updateButton = new Button
            {
                Size = new Size(panel.Width / 3, labelandTextBoxHeight),
                Location = new Point(10, panel.Height - 220),
                Text = "Update",
                AutoSize = true
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

                if (WorkerController.GetHash(labelAndTextBoxList.First(x => x.Label.Text == "Old Password").TextBox.Text).ToString() == _volunteer.HashPassworkd)
                {
                    if (temppassword == null || temppassword == "")
                        _mainUI.WorkerController().UpdateVolunteer(_mainUI.GetCurrentUser, name, email, team, phoneNumber);
                    else
                    {
                        _mainUI.WorkerController().UpdateVolunteer(_mainUI.GetCurrentUser, name, email, team, phoneNumber, temppassword);
                    }

                    WrongEmailWarning message = new WrongEmailWarning("Your account information has been updated");
                    message.StartPosition = FormStartPosition.CenterParent;
                    message.ShowDialog();
                    labelAndTextBoxList.First(x => x.Label.Text == "Old Password").TextBox.Text = "";
                    labelAndTextBoxList.First(x => x.Label.Text == "New Password").TextBox.Text = "";
                }
                else
                {
                    WrongEmailWarning message = new WrongEmailWarning("Old password was incorrect");
                    message.StartPosition = FormStartPosition.CenterParent;
                    message.ShowDialog();
                }
            }
            catch (EmailNotValidException)
            {
                WrongEmailWarning message = new WrongEmailWarning("The Email format is not legal, enter a correct email adress.");
                message.StartPosition = FormStartPosition.CenterParent;
                message.ShowDialog();
            }
            catch (EmailUsedBeforeException)
            {
                WrongEmailWarning message = new WrongEmailWarning("The Email is used with another account");
                message.StartPosition = FormStartPosition.CenterParent;
                message.ShowDialog();
            }
            catch (FormatException)
            {
                WrongEmailWarning message = new WrongEmailWarning("Phonenumber is not legal");
                message.StartPosition = FormStartPosition.CenterParent;
                message.ShowDialog();
            }
            catch (Exception)
            {
                WrongEmailWarning message = new WrongEmailWarning("some information is not legal");
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

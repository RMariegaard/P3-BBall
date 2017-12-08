using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolunteerPrototype.UI;
using VolunteerSystem;

namespace VolunteerPrototype.LogIn
{
    public class LogInForm : Form
    {
        private Label _emailLabel;
        private Label _passwordLabel;
        private TextBox _emailInputBox;
        private TextBox _passwordInputBox;

        private Button _logInButton;

        public VolunteerSystem.Volunteer _volunteer;
        private IUI _mainUI;

        public LogInForm(IUI mainUI)
        {
            _mainUI = mainUI;
            Size = new System.Drawing.Size(300, 175);
            _emailLabel = new Label()
            {
                Text = "Email:",
                AutoSize = true,
                Location = new System.Drawing.Point(25, 25)
            };
            _emailInputBox = new TextBox()
            {
                Location = new System.Drawing.Point(_emailLabel.Location.X + _emailLabel.Width + 5, _emailLabel.Location.Y),
                Text = ""
            };


            _passwordLabel = new Label()
            {
                Text = "Password:",
                AutoSize = true,
                Location = new System.Drawing.Point(_emailLabel.Location.X, _emailInputBox.Location.Y + _emailInputBox.Height + 5)
            };

            _passwordInputBox = new TextBox()
            {
                Location = new System.Drawing.Point(_passwordLabel.Location.X + _passwordLabel.Width + 5, _passwordLabel.Location.Y),
                Text = "",
                UseSystemPasswordChar = true
            };
            _passwordInputBox.KeyPress += EnterKeyHit;

            _logInButton = new Button()
            {
                Text = "Log In",
                Location = new System.Drawing.Point(_passwordInputBox.Location.X, _passwordInputBox.Location.Y + _passwordInputBox.Height + 20)
            };
            _logInButton.Click += ValidateLogIn;
            Controls.Add(_emailLabel);
            Controls.Add(_emailInputBox);
            Controls.Add(_passwordLabel);
            Controls.Add(_passwordInputBox);
            Controls.Add(_logInButton);
        }

        private void EnterKeyHit(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ValidateLogIn(sender, e);
            }
        }

        private void ValidateLogIn(object sender, EventArgs e)
        {
            try
            {
                if (Check(_emailInputBox.Text, _passwordInputBox.Text))
                {

                    DialogResult = DialogResult.Yes;
                    Close();
                }
                else
                {
                    LogInFailed();
                }
            }
            catch (Exception)
            {
                LogInFailed();
            }
            
        }

        private void LogInFailed()
        {
            var popup = new VolunteerSystem.UserInterface.PopupUI("Login Failed", "The entered email or password was incorrect")
            {
                StartPosition = FormStartPosition.CenterParent
            };
            popup.ShowDialog();
            _passwordInputBox.SelectAll();
        }

        private bool Check(string login, string pwd)
        {
            try
            {
                var db = new VolunteerSystem.Database.FinalController(new VolunteerSystem.Database.DatabaseContext(VolunteerSystem.Model.SqlDataConnecter.CnnVal("DatabaseCS")));
                var volunteer = _mainUI.WorkerController().ListOfWorkers.Select(x => x as Volunteer).First(y => (y.Email == login && y.HashPassworkd == pwd));
                
                if(volunteer != null)
                {
                    _volunteer = volunteer;
                }
                return volunteer != null;
            }
            catch (Exception)
            {
                throw new Exception("Username or password was wrong");
            }
        }
    }
}

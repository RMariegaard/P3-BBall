using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerPrototype.LogIn
{
    public class LogInForm : Form
    {
        private Label _emailLabel;
        private Label _passwordLabel;
        private TextBox _emailInputBox;
        private TextBox _passwordInputBox;

        private Button _logInButton;


        public LogInForm()
        {
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
            
                if (CredentialsSource.Check(_emailInputBox.Text, _passwordInputBox.Text))
                {

                    DialogResult = DialogResult.Yes;
                    Close(); }
                else
                {
                var popup = new VolunteerSystem.UserInterface.PopupUI("Login Failed", "The entered email or password was incorrect")
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                popup.ShowDialog();
                _passwordInputBox.SelectAll();
                }
                
            
        }
    }
}

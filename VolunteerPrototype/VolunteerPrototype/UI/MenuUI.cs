using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerPrototype.UI
{
    public class MenuUI : Panel
    {
        public bool IsLoggedIn { get { return _mainUI.IsLoggedIn(); } }

        private Button _scheduleButton;
        private Button _loginButton;
        private Button _logOutButton;
        private Button _myShiftButton;
        private Button _accountSettingsButton;

        private Label _userInforLabel;

        private IUI _mainUI;

        private Form _logInPopUp;

        public MenuUI(IUI mainUI, int width)
        {
            Width = width;
            _mainUI = mainUI;
            _scheduleButton = new Button()
            {
                Text = "Schedule",
                Location = new System.Drawing.Point(100, 70),
                Size = new System.Drawing.Size(width/3 - 45,30),
            };
            _scheduleButton.Click += ShowSchedule;
            Controls.Add(_scheduleButton);
            _userInforLabel = new Label()
            {
                Location = new System.Drawing.Point(100, 30)
            };
            Controls.Add(_userInforLabel);

            if (IsLoggedIn)
            {
                LoggedIn();
            }
            else
            {
                Default();
            }
        }

        private void ShowSchedule(object sender, EventArgs e)
        {
            _mainUI.UpdateSchedulePanel();
        }

        private void Default()
        {
             _loginButton = new Button()
            {
                Text = "Log In",
                 Location = new System.Drawing.Point(100, 5)
             };
            _loginButton.Click += Login;
            Controls.Add(_loginButton);
            _userInforLabel.Text = "You are not logged in";
       

        }

        private void Login(object sender, EventArgs e)
        {
            _logInPopUp = new LogIn.LogInForm()
            {
                StartPosition = FormStartPosition.CenterParent
            };
            
            _logInPopUp.ShowDialog();
            if (_logInPopUp.DialogResult == DialogResult.Yes)
            {
                Controls.Remove(_loginButton);
                _mainUI.LogIn();
                LoggedIn();
            }
            else
            {
                //do nothing
            }
            
            _mainUI.UpdateMenu();
        }

        private void LoggedIn()
        {
           
            _logOutButton = new Button()
            {
                Text = "Log Out",
                Location = new System.Drawing.Point(100, 5)
            };
            Controls.Add(_logOutButton);
            _logOutButton.Click += LogOut;
            _myShiftButton = new Button()
            {
                Text = "My Shifts",
                Location = new System.Drawing.Point(_scheduleButton.Width + _scheduleButton.Location.X, _scheduleButton.Location.Y),
                Size = _scheduleButton.Size
            };
            _myShiftButton.Click += MyShiftClicked;
            Controls.Add(_myShiftButton);
            
            _accountSettingsButton = new Button()
            {
                Text = "Account Settings",
                Location = new System.Drawing.Point(_myShiftButton.Width + _myShiftButton.Location.X, _myShiftButton.Location.Y),
                Size = _scheduleButton.Size
            };
            Controls.Add(_accountSettingsButton);

            _userInforLabel.Text = $"Logged in as {_mainUI.GetCurrentUser}";
        }

        private void MyShiftClicked(object sender, EventArgs e)
        {
            _mainUI.ShowMyShifts();
        }

        private void LogOut(object sender, EventArgs e)
        {
            Controls.Remove(_logOutButton);
            
            Default();
            _mainUI.LogOut();
            _mainUI.UpdateMenu();
        }
    }
}

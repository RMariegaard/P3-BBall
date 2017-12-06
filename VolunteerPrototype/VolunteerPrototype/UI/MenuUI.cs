using System;
using System.Collections.Generic;
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

        private IUI _mainUI;

        private Form _logInPopUp;

        public MenuUI(IUI mainUI, int width)
        {
            Width = width;
            _mainUI = mainUI;
            _scheduleButton = new Button()
            {
                Text = "Schedule",
                Location = new System.Drawing.Point(100, 70)
            };
            _scheduleButton.Click += ShowSchedule;
            Controls.Add(_scheduleButton);

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
                Location = new System.Drawing.Point(_scheduleButton.Width + _scheduleButton.Location.X, _scheduleButton.Location.Y)
            };
            Controls.Add(_myShiftButton);
            _accountSettingsButton = new Button()
            {
                Text = "Account Settings",
                Location = new System.Drawing.Point(_myShiftButton.Width + _myShiftButton.Location.X, _myShiftButton.Location.Y)
            };
            Controls.Add(_accountSettingsButton);
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

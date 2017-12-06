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
        private bool _isLoggedIn;
        public bool IsLoggedIn { get { return _isLoggedIn; } }

        //test
        Button test1;
        Button test2;

        private IUI _mainUI;

        private Form _logInPopUp;

        public MenuUI(IUI mainUI, bool isLoggedIn, int width)
        {
            Width = width;
            _mainUI = mainUI;
            _isLoggedIn = isLoggedIn;

            if (IsLoggedIn)
            {
                LoggedIn();
            }
            else
            {
                Default();
            }
        }

        private void Default()
        {
             test1 = new Button()
            {
                Text = "Log In",
                 Location = new System.Drawing.Point(this.Width - 110)
             };
            test1.Click += Login;
            Controls.Add(test1);
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
                Controls.Remove(test1);
                _isLoggedIn = true;
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
           
            test2 = new Button()
            {
                Text = "Log Out",
                Location = new System.Drawing.Point(this.Width - 110)
            };
            Controls.Add(test2);
            test2.Click += LogOut;
        }

        private void LogOut(object sender, EventArgs e)
        {
            Controls.Remove(test2);
            _isLoggedIn = false;
            Default();
            _mainUI.UpdateMenu();
        }
    }
}

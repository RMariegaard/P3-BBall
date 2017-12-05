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

        public MenuUI(IUI mainUI, bool isLoggedIn)
        {
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
                Text = "Log In"
            };
            test1.Click += Login;
            Controls.Add(test1);
        }

        private void Login(object sender, EventArgs e)
        {
            Controls.Remove(test1);
            _isLoggedIn = true;
            LoggedIn();
            _mainUI.UpdateMenu();
        }

        private void LoggedIn()
        {

            test2 = new Button()
            {
                Text = "Log Out"
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

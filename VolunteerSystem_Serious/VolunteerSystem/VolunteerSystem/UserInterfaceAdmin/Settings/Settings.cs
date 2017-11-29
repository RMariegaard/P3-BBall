using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.Settings
{
    class Settings
    {
        IVolunteerMainUI _settingsMainUI;
        private Panel _settingsMainPanel;
        private Label _titleLabel;

        public Settings(IVolunteerMainUI mainUI)
        {
            this._settingsMainUI = mainUI;

            _settingsMainPanel = new Panel()
            {
                Name = "_settingsMainPanel"
            };

            _titleLabel = new Label()
            {
                Location = new Point(5, 5),
                Text = "Settings",
                AutoSize = true
                
            };

            this._settingsMainPanel.Controls.Add(_titleLabel);
    }

        public Panel GetPanel(Size size)
        {
            this._settingsMainPanel.Size = size;
            return this._settingsMainPanel;
        }

        
    }
}

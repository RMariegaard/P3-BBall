using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.Settings
{
    public class Settings
    {
        IVolunteerMainUI _settingsMainUI;
        private Panel _settingsMainPanel;
        private Label _titleLabel;
        private ListBox _settingsList;
        private Panel _settingViewPanel;
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

            _settingsList = new ListBox()
            {
                Location = new Point(5, _titleLabel.Location.Y + _titleLabel.Height + 5),
                Font = new Font(_settingsMainPanel.Font.Name, 14F),
                AutoSize = true
            };
            _settingsList.Items.Add("Email ");
            _settingsList.SelectedIndex = 0;

            _settingViewPanel = new EmailSetting()
            {
                Location = new Point(_settingsList.Location.X + _settingsList.Width + 10, _settingsList.Location.Y),

                BorderStyle = BorderStyle.FixedSingle,
                AutoSize = true
            };

            _settingsMainPanel.Controls.Add(_settingViewPanel);

            this._settingsMainPanel.Controls.Add(_settingsList);

            this._settingsMainPanel.Controls.Add(_titleLabel);
    }

        public Panel GetPanel(Size size)
        {
            this._settingsMainPanel.Size = size;
            return this._settingsMainPanel;
        }

        
    }
}

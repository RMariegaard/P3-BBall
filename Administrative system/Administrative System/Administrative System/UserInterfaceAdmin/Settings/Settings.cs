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
            _settingsList.Items.Add("Email");
            _settingsList.Items.Add("Create new schedule");
            _settingsList.SelectedIndexChanged += ChangeSetting;

            
            this._settingsMainPanel.Controls.Add(_settingsList);

            this._settingsMainPanel.Controls.Add(_titleLabel);
    }

        private void ChangeSetting(object sender, EventArgs e)
        {
            _settingsMainPanel.Controls.Remove(_settingViewPanel);
            if (_settingsList.SelectedIndex == 0)
            {
                _settingViewPanel = new EmailSetting()
                {
                    Location = new Point(_settingsList.Location.X + _settingsList.Width + 10, _settingsList.Location.Y),

                    BorderStyle = BorderStyle.FixedSingle,
                    AutoSize = true
                };

                _settingsMainPanel.Controls.Add(_settingViewPanel);
            }
            if (_settingsList.SelectedIndex == 1)
            {
                _settingViewPanel = new CreateNewSchedulePanel(this._settingsMainUI)
                {
                    Location = new Point(_settingsList.Location.X + _settingsList.Width + 10, _settingsList.Location.Y),

                    BorderStyle = BorderStyle.FixedSingle,
                    AutoSize = true
                };

                _settingsMainPanel.Controls.Add(_settingViewPanel);

            }

        }

        public Panel GetPanel(Size size)
        {
            this._settingsMainPanel.Size = size;
            return this._settingsMainPanel;
        }

        
    }
}

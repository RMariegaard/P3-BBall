using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.Settings
{
    public class EmailSetting : Panel
    {
        private string _path;
        private TextBox _messageBox;
        private ListBox _messageList;
        private Button _saveButton;

        public EmailSetting() : base()
        {
            this._path = (Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory))) + @"\Messages";
            _messageList = new ListBox()
            {
                AutoSize = true,
                Location = new System.Drawing.Point(5, 5)
            };
            _messageBox = new TextBox()
            {
                AutoSize = true,
                Location = new System.Drawing.Point(5, _messageList.Location.Y + _messageList.Height + 5),
                Height = 200,
                Width = 200,
            };
            _saveButton = new Button()
            {
                Text = "Save",
                Location = new System.Drawing.Point(_messageBox.Location.X, _messageBox.Location.Y + _messageBox.Height + 5)
            };
            _saveButton.Click += SaveChanges;
            _messageList.Items.AddRange(System.IO.Directory.EnumerateFiles(_path).ToArray());
            _messageList.SelectedIndexChanged += ChangeMessageBox;
            this.Controls.Add(_messageList);
            this.Controls.Add(_messageBox);
            this.Controls.Add(_saveButton);
        }

        private void SaveChanges(object sender, EventArgs e)
        {
            File.WriteAllText(_messageList.SelectedItem.ToString(), _messageBox.Text);
        }

        private void ChangeMessageBox(object sender, EventArgs e)
        {
            _messageBox.Text = new StreamReader(_messageList.SelectedItem.ToString()).ReadToEnd();

        }
    }
}

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
        private RichTextBox _messageBox;
        private ListBox _messageList;
        private Button _saveButton;

        public EmailSetting() : base()
        {
            this._path = (Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory))) + @"\Messages";
            _messageList = new ListBox()
            {
                AutoSize = true,
                Location = new System.Drawing.Point(5, 5),
                Width = 200,
                DisplayMember = "Key"
                
            };
            _messageBox = new RichTextBox()
            {
                AutoSize = true,
                Location = new System.Drawing.Point(5, _messageList.Location.Y + _messageList.Height + 5),
                Width = 400,
                Height = 400,
                
                
            };
            _saveButton = new Button()
            {
                Text = "Save",
                Location = new System.Drawing.Point(_messageBox.Location.X, _messageBox.Location.Y + _messageBox.Height + 5)
            };
            _saveButton.Click += SaveChanges;

            string[] text = { "Removed volunteer message", "Request accepted message", "Request denied message", "Shift changed message", "Shift deleted message"};

            for(int i = 0; i < Directory.GetFiles(_path).Count(); i++)
            {

                if (text[i] != null)
                    _messageList.Items.Add(new KeyValuePair<string, string>(text[i], System.IO.Directory.GetFiles(_path)[i]));
                else
                {
                    string fileName = Path.GetFileName(System.IO.Directory.GetFiles(_path)[i]);
                    _messageList.Items.Add(new KeyValuePair<string, string>(fileName, System.IO.Directory.GetFiles(_path)[i]));
                }
            }
            //_messageList.Items.AddRange(System.IO.Directory.GetFiles(_path));
            
            _messageList.SelectedIndexChanged += ChangeMessageBox;
            if(_messageList.Items.Count > 0)
            {
                _messageList.SelectedIndex = 0;
            }
            this.Controls.Add(_messageList);
            this.Controls.Add(_messageBox);
            this.Controls.Add(_saveButton);
        }

        private void SaveChanges(object sender, EventArgs e)
        {
            if(_messageList.SelectedItem != null)
            File.WriteAllText(((KeyValuePair<string, string>)_messageList.SelectedItem).Value.ToString(), _messageBox.Text);
        }

        private void ChangeMessageBox(object sender, EventArgs e)
        {
            var file = new StreamReader(((KeyValuePair<string, string>)_messageList.SelectedItem).Value.ToString());
            _messageBox.Text = file.ReadToEnd();
            file.Close();

        }
    }


}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterface
{
    public partial class CreateNewTaskUI : Form
    {
        Button createTaskButton;
        Button abortButton;

        TextBox nameOfTaskTextBox;
        Label nameOfTaskLabel;

        public string Result;

        public CreateNewTaskUI()
        {
            InitializeComponent();

            Height = 300;
            Width = 400;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.Icon = new Icon(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\TwoMen.ico");
            this.Text = "Volunteer Manegement System: Create new task";

            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            nameOfTaskLabel = new Label();
            nameOfTaskLabel.Location = new Point(10, 10);
            nameOfTaskLabel.Text = "Name of the new task:";
            nameOfTaskLabel.AutoSize = true;

            nameOfTaskTextBox = new TextBox();
            nameOfTaskTextBox.Location = new Point(nameOfTaskLabel.Location.X + nameOfTaskLabel.Size.Width + 50, 10);
            nameOfTaskTextBox.Size = new Size(200, nameOfTaskTextBox.Size.Height);

            abortButton = new Button();
            abortButton.Text = "Cancel";
            abortButton.AutoSize = true;
            abortButton.Location = new Point(10, 50);
            abortButton.DialogResult = DialogResult.Cancel;

            createTaskButton = new Button();
            createTaskButton.Text = "Create";
            createTaskButton.AutoSize = true;
            createTaskButton.Location = new Point(Width - createTaskButton.Size.Width - 30, 50);
            createTaskButton.Click += createTaskButton_Clicked;

            Controls.Add(nameOfTaskLabel);
            Controls.Add(nameOfTaskTextBox);
            Controls.Add(abortButton);
            Controls.Add(createTaskButton);
        }

        private void createTaskButton_Clicked(object sender, EventArgs e)
        {
            if (correctInformation())
            {
                Result = nameOfTaskTextBox.Text;
                DialogResult = DialogResult.OK;
            }
            else
            {
                PopupUI NotGoodEnugh = new PopupUI("Information incomplete", "The given information is either incomplete og wrong, plz fix bro");
                NotGoodEnugh.Show();
            }
        }
        private bool correctInformation()
        {
            return true;
        }
    }
}

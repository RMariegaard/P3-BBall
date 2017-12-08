using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage.SchedulePanelElements
{
    public class TasksPanels
    {
        IVolunteerMainUI volunteerMainUI;
        public TasksPanels(IVolunteerMainUI volunteerMainUI)
        {
            this.volunteerMainUI = volunteerMainUI;
        }
        private string _taskName;
        public Panel GetATaskPanel(string headline, List<Shift> ListOfShifts, Size size, Point location, int hourHeight, DateTime date)
        {
            _taskName = headline;
            Panel taskPanel = new Panel
            {
                Name = headline,
                Location = location,
                Size = size,
                BackColor = ColorAndStyle.SecondaryColor(true),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label Headder = new Label
            {
                Location = new Point(2, 2),
                Text = headline,
                AutoSize = true,
            };
           
            taskPanel.Controls.Add(Headder);

            PictureBox button = new PictureBox()
            {
                Image = PressedOnShiftPopup.ResizeImage(Image.FromFile(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\CrossIcon.png"), 15, 15),
                Location = new Point(taskPanel.Width - 15, 0),
                Cursor = Cursors.Hand,
                Size = new Size(15, 15),
                BackColor = Color.FromArgb(0, Color.Gray),
            };
            button.Click += headder_clicked;
            taskPanel.Controls.Add(button);
            for (int i = 0; i < ListOfShifts.Count(); i++)
            {
                ShiftUIPanel tempShiftUIPanel = new ShiftUIPanel(volunteerMainUI, ListOfShifts[i], date);
                taskPanel.Controls.Add(tempShiftUIPanel.ShiftUI(taskPanel, hourHeight));
            }

            return taskPanel;
        }

        private void headder_clicked(object sender, EventArgs e)
        {
            DeleteFormPopUp deletePopup = new DeleteFormPopUp($"Are you sure you want to delete the task {_taskName}?\n (This will remove the task for all days and shifts and request for this task)");
            deletePopup.StartPosition = FormStartPosition.CenterParent;
            deletePopup.ShowDialog();
            if (deletePopup.DialogResult == DialogResult.OK)
            {
                volunteerMainUI.RemoveTaskAndAssociateShifts(_taskName);
            }
        }
    }
}

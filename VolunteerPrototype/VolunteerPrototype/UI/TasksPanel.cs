using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using VolunteerSystem.UserInterfaceAdmin;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage.SchedulePanelElements
{
    public class TasksPanel
    {
        IVolunteerMainUI volunteerMainUI;
        public TasksPanel(IVolunteerMainUI volunteerMainUI)
        {
            this.volunteerMainUI = volunteerMainUI;
        }
        private string _taskName;
        public Panel GetATaskPanel(string headline, List<Shift> shifts, Size size, Point location, int hourHeight, DateTime date)
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
                Image = PressedOnShiftPopup.ResizeImage(SystemIcons.Error.ToBitmap(), 15, 15),
                Location = new Point(taskPanel.Width - 15, 0),
                Cursor = Cursors.Hand,
                Size = new Size(15, 15)
            };
            button.Click += headder_clicked;
            taskPanel.Controls.Add(button);
            for (int i = 0; i < shifts.Count(); i++)
            {
                ShiftPanel tempShiftUIPanel = new ShiftPanel(volunteerMainUI, shifts[i], date);
                taskPanel.Controls.Add(tempShiftUIPanel.ShiftUI(taskPanel, hourHeight));
            }

            return taskPanel;
        }

        private void headder_clicked(object sender, EventArgs e)
        {
            DeleteFormPopUp deletePopup = new DeleteFormPopUp($"Are you sure you want to delete the task {_taskName}?\n (This will remove the task for all days)");
            deletePopup.StartPosition = FormStartPosition.CenterParent;
            deletePopup.ShowDialog();
            
        }
    }
}

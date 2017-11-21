using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage.SchedulePanelElements
{
    class ShiftUIPanel
    {
        IVolunteerMainUI volunteerMainUI;
        Shift shift;

        public ShiftUIPanel(IVolunteerMainUI volunteerMainUI, Shift shift)
        {
            this.volunteerMainUI = volunteerMainUI;
            this.shift = shift;
        }

        public Panel ShiftUI(Panel forRefence, int hourHeight)
        {
            float LengthInHours = 0;
            TimeSpan timeSpan = shift.EndTime - shift.StartTime;

            Panel shiftPanel = new Panel();
            shiftPanel.Location = new Point(0, (shift.StartTime.Hour - 1) * hourHeight);
            shiftPanel.Size = new Size(forRefence.Size.Width, (int)(LengthInHours * hourHeight));
            shiftPanel.BackColor = Color.AliceBlue;
            shiftPanel.BorderStyle = BorderStyle.FixedSingle;
            shiftPanel.Click += panel_clicked;

            Label headder = new Label();
            headder.Text = $"{shift.NumberOfVolunteers().ToString()}/{shift.VolunteersNeeded}";
            headder.Location = new Point(0, 0);
            headder.AutoSize = true;

            Label Time = new Label();
            Time.Location = new Point(0, headder.Location.Y + headder.Size.Height + 1);
            Time.Text = $"{shift.StartTime.Hour}.{shift.StartTime.Minute} - {shift.EndTime.Hour}.{shift.EndTime.Minute}";
            Time.AutoSize = true;

            Label Desciption = new Label();
            Desciption.Location = new Point(0, Time.Location.Y + Time.Size.Height + 1);
            Desciption.Text = shift.Description;
            Desciption.MaximumSize = new Size(100, 0);
            Desciption.AutoSize = true;

            shiftPanel.Controls.Add(headder);
            shiftPanel.Controls.Add(Time);
            shiftPanel.Controls.Add(Desciption);

            return shiftPanel;
        }

        private void panel_clicked(object sender, EventArgs e)
        {
            volunteerMainUI.DisplayPressedOnShift(shift);
        }
    }
}

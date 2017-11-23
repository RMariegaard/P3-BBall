using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage.SchedulePanelElements
{
    public class ShiftUIPanel
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
            TimeSpan timeSpan = shift.EndTime - shift.StartTime;
            int LengthInminuts = (int)timeSpan.TotalMinutes;
            
            Panel shiftPanel = new Panel();
            shiftPanel.Location = new Point(0, (int)(((shift.StartTime.Hour*60) + shift.StartTime.Minute) * ((double)hourHeight/60)));
            shiftPanel.Size = new Size(forRefence.Size.Width, (int)(LengthInminuts * ((double)hourHeight/60)));
            shiftPanel.BackColor = Color.AliceBlue;
            shiftPanel.BorderStyle = BorderStyle.FixedSingle;
            shiftPanel.Click += panel_clicked;

            Label headder = new Label();
            headder.Text = $"{shift.NumberOfVolunteers().ToString()}/{shift.VolunteersNeeded}";
            headder.Location = new Point(0, 0);
            headder.AutoSize = true;

            Label Time = new Label();
            Time.Location = new Point(0, headder.Location.Y + headder.Size.Height + 1);
            string text = shift.StartTime.Hour + "." + shift.StartTime.Minute + "-" + shift.EndTime.Hour + "." + shift.EndTime.Minute;
            Time.Text = text;
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

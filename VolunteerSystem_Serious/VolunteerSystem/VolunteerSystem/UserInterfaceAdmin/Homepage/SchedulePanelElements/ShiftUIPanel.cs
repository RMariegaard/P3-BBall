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
        public static Panel ShiftUI(Shift shift, Panel forRefence, int hourHeight)
        {
            float LengthInHours = 0;
            if (shift.StartTime.Minute != 0)
            {
                LengthInHours += (1 / 60) * (60 - shift.StartTime.Minute);
                LengthInHours -= 1;
            }
            if (shift.EndTime.Minute != 0)
                LengthInHours += (1 / 60) * shift.EndTime.Minute;

            LengthInHours += (shift.EndTime.Hour - shift.StartTime.Hour);
            

            Panel shiftPanel = new Panel();
            shiftPanel.Location = new Point(0, (shift.StartTime.Hour - 1) * hourHeight);
            shiftPanel.Size = new Size(forRefence.Size.Width, (int)(LengthInHours * hourHeight));
            shiftPanel.BackColor = Color.AliceBlue;

            Label headder = new Label();
            headder.Text = $"{shift.NumberOfVolunteers().ToString()}/{shift.VolunteersNeeded}";
            headder.Location = new Point(0, 0);
            headder.AutoSize = true;

            Label Time = new Label();
            Time.Location = new Point(0, headder.Location.Y + headder.Size.Height + 1);
            Time.Text = $"{shift.StartTime.Hour}.00 - {shift.StartTime.Hour + LengthInHours}.00";
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
    }
}

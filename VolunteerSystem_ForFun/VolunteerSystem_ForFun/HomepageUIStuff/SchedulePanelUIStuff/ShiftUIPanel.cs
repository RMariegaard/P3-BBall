using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecretP3;

namespace VolunteerSystem_ForFun.HomepageUIStuff.SchedulePanelUIStuff
{
    static class ShiftUIPanel
    {
        public static Panel ShiftUI(Shift shift, Panel forRefence, int hourHeight)
        {
            Panel shiftPanel = new Panel();
            shiftPanel.Location = new Point(0, (shift.Time.Hour-1) * hourHeight);
            shiftPanel.Size = new Size(forRefence.Size.Width, shift.LengthInHours * hourHeight);
            shiftPanel.BackColor = Color.AliceBlue;

            Label headder = new Label();
            headder.Text = shift.Title;
            headder.Location = new Point(0, 0);
            headder.AutoSize = true;

            Label Time = new Label();
            Time.Location = new Point(0, headder.Location.Y + headder.Size.Height + 1);
            Time.Text = $"{shift.Time.Hour}.00 - {shift.Time.Hour + shift.LengthInHours}.00";
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

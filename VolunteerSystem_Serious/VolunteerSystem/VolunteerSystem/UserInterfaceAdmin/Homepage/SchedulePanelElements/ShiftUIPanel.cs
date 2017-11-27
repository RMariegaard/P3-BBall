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

        

        public Control ShiftUI(Panel forRefence, int hourHeight)
        {
            BindingSource bs = new BindingSource
            {
                DataSource = typeof(Shift)
            };
            bs.Add(shift);

            //////////////////ShiftPanel////////////////////////

            Panel shiftPanel = new Panel
            {
                Name = "Shift " + shift.ID.ToString(),
                BackColor = Color.AliceBlue,
                BorderStyle = BorderStyle.FixedSingle
            };
            shiftPanel.Click += _panel_clicked;

            //Binds the location to starttime, so whenever this is edited, the position of the shift will be changed in the schedule
            var locationBinding = new Binding("Location", bs, "StartTime");
            locationBinding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            {
                convertEventArgs.Value = new Point(0, (int)(((shift.StartTime.Hour * 60) + shift.StartTime.Minute) * ((double)hourHeight / 60))); 
            };

            var SizeBinding = new Binding("Size", bs, "EndTime");
            SizeBinding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            {
                TimeSpan timeSpan = shift.EndTime - shift.StartTime;
                int LengthInminuts = (int)timeSpan.TotalMinutes;

                convertEventArgs.Value = new Size(forRefence.Size.Width, (int)(LengthInminuts * ((double)hourHeight / 60)));
            };


            shiftPanel.DataBindings.Add(locationBinding);
            shiftPanel.DataBindings.Add(SizeBinding);



            //////////////////NumberOfVolunteerLabel////////////////////////
            var numberOfVolunteerBinding = new Binding("Text", bs, "GetNumberOfVolunteers");
            //Ikke længere nødvendig
            //binding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            //{
            //    convertEventArgs.Value =  convertEventArgs.Value + "/"+shift.VolunteersNeeded;
            //};

            Label headder = new Label();
            headder.DataBindings.Add(numberOfVolunteerBinding);
            headder.Location = new Point(0, 0);
            headder.AutoSize = true;

            //////////////////TimeLabel////////////////////////
            var timeBinding = new Binding("Text", bs, "TimeInterval");
            Label Time = new Label
            {
                Location = new Point(0, headder.Location.Y + headder.Size.Height + 1)
            };
            Time.DataBindings.Add(timeBinding);
            Time.AutoSize = true;

            //////////////////DescriptionLabel////////////////////////
            Label Desciption = new Label
            {
                Location = new Point(0, Time.Location.Y + Time.Size.Height + 1),
                Text = shift.Description,
                MaximumSize = new Size(100, 0),
                AutoSize = true
            };

            shiftPanel.Controls.Add(headder);
            shiftPanel.Controls.Add(Time);
            shiftPanel.Controls.Add(Desciption);

            return shiftPanel;
        }

        private void _panel_clicked(object sender, EventArgs e)
        {
            volunteerMainUI.DisplayPressedOnShift(shift);
        }
    }
}

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
        Panel shiftPanel;

        public ShiftUIPanel(IVolunteerMainUI volunteerMainUI, Shift shift)
        {
            this.volunteerMainUI = volunteerMainUI;
            this.shift = shift;
        }

        

        public Control ShiftUI(Panel forRefence, int hourHeight)
        {
            BindingSource shiftBindingSource = new BindingSource
            {
                DataSource = typeof(Shift)
            };
            shiftBindingSource.Add(shift);

            //////////////////ShiftPanel////////////////////////

            shiftPanel = new Panel
            {
                Name = "Shift " + shift.ID.ToString(),
                BackColor = Color.AliceBlue,
                BorderStyle = BorderStyle.FixedSingle
            };
            shiftPanel.Click += _panel_clicked;
            
            //Binds the location to starttime, so whenever this is edited, the position of the shift will be changed in the schedule
            var locationBinding = new Binding("Location", shiftBindingSource, "StartTime");
            locationBinding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            {
                convertEventArgs.Value = new Point(0, (int)(((shift.StartTime.Hour * 60) + shift.StartTime.Minute) * ((double)hourHeight / 60))); 
            };

            var SizeBinding = new Binding("Size", shiftBindingSource, "EndTime");
            SizeBinding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            {
                TimeSpan timeSpan = shift.EndTime - shift.StartTime;
                int LengthInminuts = (int)timeSpan.TotalMinutes;

                convertEventArgs.Value = new Size(forRefence.Size.Width, (int)(LengthInminuts * ((double)hourHeight / 60)));
            };


            shiftPanel.DataBindings.Add(locationBinding);
            shiftPanel.DataBindings.Add(SizeBinding);


            //////////////////TopColorPanel////////////////////////
            var topColorBinding = new Binding("BackColor", shiftBindingSource, "GetNumberOfVolunteers");
            topColorBinding.Format += delegate (object sender, ConvertEventArgs e)
            {
                if (shift.Requests.Count() + shift.Workers.Count() < shift.VolunteersNeeded)
                    e.Value = ColorAndStyle.ShiftColorCompletelyFree();
                else if ((shift.Workers.Count() < shift.VolunteersNeeded) && (shift.Workers.Count() + shift.Requests.Count() > shift.VolunteersNeeded))
                    e.Value = ColorAndStyle.ShiftColorEnoughRequests();
                else if (shift.Workers.Count() >= shift.VolunteersNeeded)
                    e.Value = ColorAndStyle.ShiftColorWhenFull();
            };

            Panel topColor = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(shiftPanel.Width, 5)
            };
            topColor.DataBindings.Add(topColorBinding);
            //////////////////NumberOfVolunteerLabel////////////////////////
            var numberOfVolunteerBinding = new Binding("Text", shiftBindingSource, "GetNumberOfVolunteers");
            //Ikke længere nødvendig
            //binding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            //{
            //    convertEventArgs.Value =  convertEventArgs.Value + "/"+shift.VolunteersNeeded;
            //};

            Label headder = new Label();
            headder.DataBindings.Add(numberOfVolunteerBinding);
            headder.Location = new Point(0 , 8);
            headder.AutoSize = true;

            //////////////////TimeLabel////////////////////////
            var timeBinding = new Binding("Text", shiftBindingSource, "TimeInterval");
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
                MaximumSize = new Size(100, 0),
                AutoSize = true
            };
            var descriptionBinding = new Binding("Text", shiftBindingSource, "Description");
            Desciption.DataBindings.Add(descriptionBinding);

            shiftPanel.Controls.Add(topColor);
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

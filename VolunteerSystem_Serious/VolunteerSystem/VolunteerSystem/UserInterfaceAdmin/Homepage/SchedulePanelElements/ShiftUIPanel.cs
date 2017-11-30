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
        DateTime date;
        public ShiftUIPanel(IVolunteerMainUI volunteerMainUI, Shift shift, DateTime date)
        {
            this.volunteerMainUI = volunteerMainUI;
            this.shift = shift;
            this.date = date;
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
            int startTimeHour = 0;
            int endTimeHour = 0;
            int startTimeMinut = 0;
            int endTimeMinut = 0;

            DateTime startShiftShotDateTime = new DateTime(shift.StartTime.Year, shift.StartTime.Month, shift.StartTime.Day);
            DateTime endShiftShotDateTime = new DateTime(shift.EndTime.Year, shift.EndTime.Month, shift.EndTime.Day);

            if (startShiftShotDateTime < date)
            {
                startTimeHour = 0;
                startTimeMinut = 0;
            }
            else if (startShiftShotDateTime == date)
            {
                startTimeHour = shift.StartTime.Hour;
                startTimeMinut = shift.StartTime.Minute;
            }
            if (endShiftShotDateTime == date)
            {
                endTimeHour = shift.EndTime.Hour;
                endTimeMinut = shift.EndTime.Minute;
            }
            else if (endShiftShotDateTime > date)
            {
                endTimeHour = 23;
                endTimeMinut = 23;
            }



            var locationBinding = new Binding("Location", shiftBindingSource, "StartTime");
            locationBinding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            {
                convertEventArgs.Value = new Point(0, (int)(((startTimeHour * 60 + 60) + startTimeMinut) * ((double)hourHeight / 60))); 
            };

            var SizeBinding = new Binding("Size", shiftBindingSource, "EndTime");
            SizeBinding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            {
                TimeSpan timeSpan = new TimeSpan(endTimeHour, endTimeMinut, 0) - new TimeSpan(startTimeHour, startTimeMinut, 0); //shift.EndTime - shift.StartTime;
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
            headder.Click += _panel_clicked;

            //////////////////TimeLabel////////////////////////
            var timeBinding = new Binding("Text", shiftBindingSource, "TimeInterval");
            Label Time = new Label
            {
                Location = new Point(0, headder.Location.Y + headder.Size.Height + 1)
            };
            Time.DataBindings.Add(timeBinding);
            Time.AutoSize = true;
            Time.Click += _panel_clicked;
            //////////////////DescriptionLabel////////////////////////
            Label Desciption = new Label
            {
                Location = new Point(0, Time.Location.Y + Time.Size.Height + 1),
                MaximumSize = new Size(100, 0),
                AutoSize = true
            };
            var descriptionBinding = new Binding("Text", shiftBindingSource, "Description");
            Desciption.DataBindings.Add(descriptionBinding);
            Desciption.Click += _panel_clicked;


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

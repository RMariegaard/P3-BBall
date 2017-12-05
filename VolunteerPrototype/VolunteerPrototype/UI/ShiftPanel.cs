using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using VolunteerSystem;

namespace VolunteerPrototype.UI
{
    public class ShiftPanel
    {
        Shift shift;
        Panel shiftPanel;
        DateTime date;
        public ShiftPanel(Shift shift, DateTime date)
        {
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

            var locationBinding = new Binding("Location", shiftBindingSource, "StartTime");
            locationBinding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            {
                if (date.Date == shift.StartTime.Date)
                    convertEventArgs.Value = new Point(0, (int)(((shift.StartTime.Hour * 60 + 60) + shift.StartTime.Minute) * ((double)hourHeight / 60)));
                else
                    convertEventArgs.Value = new Point(0, 25); //25 svare til hourhight inde i theSchedule...
            };

            DateTime test = shift.EndTime;
            var SizeBinding = new Binding("Size", shiftBindingSource, "EndTime");
            SizeBinding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            {

                if (date.Date == shift.EndTime.Date)
                {
                    TimeSpan timeSpan = new TimeSpan(shift.EndTime.Hour, shift.EndTime.Minute, 0) - new TimeSpan(shift.StartTime.Hour, shift.StartTime.Minute, 0);
                    int LengthInminuts = (int)timeSpan.TotalMinutes;

                    convertEventArgs.Value = new Size(forRefence.Size.Width, (int)(LengthInminuts * ((double)hourHeight / 60)));
                }
                else if (date.Date == shift.StartTime.Date)
                {
                    TimeSpan timespan = new TimeSpan(23, 60, 0) - new TimeSpan(shift.StartTime.Hour, shift.StartTime.Minute, 0); //
                    int LengthInminuts = (int)timespan.TotalMinutes;

                    convertEventArgs.Value = new Size(forRefence.Size.Width, (int)(LengthInminuts * ((double)hourHeight / 60)));
                }
                else
                {
                    TimeSpan timespan = new TimeSpan(23, 60, 0) - new TimeSpan(0, 0, 0); //
                    int LengthInminuts = (int)timespan.TotalMinutes;

                    convertEventArgs.Value = new Size(forRefence.Size.Width, (int)(LengthInminuts * ((double)hourHeight / 60)));
                }

                if (test != shift.EndTime)
                {
                    //vgv 
                }
            };

            shiftPanel.DataBindings.Add(locationBinding);
            shiftPanel.DataBindings.Add(SizeBinding);


            //////////////////TopColorPanel////////////////////////
            var topColorBinding = new Binding("BackColor", shiftBindingSource, "GetNumberOfVolunteers");
            topColorBinding.Format += delegate (object sender, ConvertEventArgs e)
            {
                if (shift.Requests.Count() + shift.Workers.Count() < shift.VolunteersNeeded)
                    e.Value = Color.Green;
                else if ((shift.Workers.Count() < shift.VolunteersNeeded) && (shift.Workers.Count() + shift.Requests.Count() > shift.VolunteersNeeded))
                    e.Value = Color.Yellow;
                else if (shift.Workers.Count() >= shift.VolunteersNeeded)
                    e.Value = Color.Red;
            };

            Panel topColor = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(shiftPanel.Width, 5)
            };
            topColor.DataBindings.Add(topColorBinding);
            //////////////////NumberOfVolunteerLabel////////////////////////
            var numberOfVolunteerBinding = new Binding("Text", shiftBindingSource, "GetNumberOfVolunteers");

            numberOfVolunteerBinding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            {
               convertEventArgs.Value =  "Volunteers:     "+convertEventArgs.Value;
            };

            Label headder = new Label();
            headder.DataBindings.Add(numberOfVolunteerBinding);
            headder.Location = new Point(0 , 8);
            headder.AutoSize = true;

            //////////////////TimeLabel////////////////////////
            var timeBinding = new Binding("Text", shiftBindingSource, "TimeInterval");
            Label Time = new Label
            {
                Location = new Point(0, headder.Location.Y + headder.Font.Height + 2)
            };
            timeBinding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            {
                convertEventArgs.Value = "Time:     " + convertEventArgs.Value;
            };
            Time.DataBindings.Add(timeBinding);
            Time.AutoSize = true;
            //////////////////DescriptionLabel////////////////////////
            Label Desciption = new Label
            {
                Location = new Point(0, Time.Location.Y + Time.Font.Height  + 2),
                MaximumSize = new Size(100, 0),
                AutoSize = true
            };
            var descriptionBinding = new Binding("Text", shiftBindingSource, "Description");
            descriptionBinding.Format += delegate (object sentFrom, ConvertEventArgs convertEventArgs)
            {
                convertEventArgs.Value = "Description:\n" + convertEventArgs.Value;
            };
            Desciption.DataBindings.Add(descriptionBinding);


            shiftPanel.Controls.Add(topColor);
            shiftPanel.Controls.Add(headder);
            shiftPanel.Controls.Add(Time);
            shiftPanel.Controls.Add(Desciption);

            return shiftPanel;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage.RequestPanelElements
{
    
    class MainRequestPanelElement
    {
        private Panel _mainRequestPanel;
        private IVolunteerMainUI _volunteerMainUI;
        private Label titleTopLabel;

        public MainRequestPanelElement(IVolunteerMainUI volunteerMainUI)
        {
            _volunteerMainUI = volunteerMainUI;
            _mainRequestPanel = new Panel
            {
                Name = "_mainRequestPanel",
                AutoScroll = true
            };
            titleTopLabel = new Label();
        }

        private Panel _getTopBarPanel(Size size)
        {
            Panel TopBarPanel = new Panel
            {
                Name = "TopBarPanel",
                Location = new Point(0, 40),
                Size = size
            };

            Label nameLabel = new Label
            {
                Text = "Name",
                AutoSize = true,
                Location = new Point((_mainRequestPanel.Size.Width / 5) * 0 + 2, 2)
            };

            Label dateSentLabel = new Label
            {
                Text = "Request Sent",
                AutoSize = true,
                Location = new Point((_mainRequestPanel.Size.Width / 5) * 1 + 2, 2)
            };

            Label shiftInfoLabel = new Label
            {
                Text = "Shift Info",
                AutoSize = true,
                Location = new Point((_mainRequestPanel.Size.Width / 5) * 2 + 2, 2)
            };

            Label acceptButtonLabel = new Label
            {
                Text = "Accept",
                AutoSize = true,
                Location = new Point((_mainRequestPanel.Size.Width / 5) * 3 + 2, 2)
            };

            Label denyButtonLabel = new Label
            {
                Text = "Deny",
                AutoSize = true,
                Location = new Point((_mainRequestPanel.Size.Width / 5) * 4 + 2, 2)
            };


            TopBarPanel.Controls.Add(nameLabel);
            TopBarPanel.Controls.Add(dateSentLabel);
            TopBarPanel.Controls.Add(shiftInfoLabel);
            TopBarPanel.Controls.Add(acceptButtonLabel);
            TopBarPanel.Controls.Add(denyButtonLabel);
            return TopBarPanel;
        }

        public Panel GetRequestPanel(Size forRefference)
        {
            int panelHeight = 40;

            _mainRequestPanel.Controls.Clear();
            _mainRequestPanel.Size = forRefference;
            titleTopLabel.Location = new Point(10, 0);
            titleTopLabel.Text = "Pending Requests";
            titleTopLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            titleTopLabel.AutoSize = true;

            Panel theRequestsPanel = new Panel();
            theRequestsPanel.Location = new Point(_mainRequestPanel.Location.X, panelHeight + 15);
            theRequestsPanel.Size = new Size(_mainRequestPanel.Width, _mainRequestPanel.Height - theRequestsPanel.Location.Y);
            theRequestsPanel.AutoScroll = true;
            theRequestsPanel.BorderStyle = BorderStyle.FixedSingle;

            int i = 0;
            foreach (Request request in _volunteerMainUI.GetScheduleController().GetAllRequests())
            {
                Panel requestPanel = new Panel
                {
                    Name = "requestPanel",
                    Location = new Point(0, (i * panelHeight)),
                    Size = new Size(_mainRequestPanel.Width - 20, panelHeight),
                    BorderStyle = BorderStyle.FixedSingle
                };
                if (i % 2 != 0)
                    requestPanel.BackColor = ColorAndStyle.SmallAlternatingColorsONE();
                else
                    requestPanel.BackColor = ColorAndStyle.SmallAlternatingColorsTWO();

                requestPanel.Controls.Add(_getVolunteerName(new Point((_mainRequestPanel.Size.Width / 5) * 0 + 2, 2), request, request.Worker));
                requestPanel.Controls.Add(_getDateItWasSent(new Point((_mainRequestPanel.Size.Width / 5) * 1 + 2, 2), request));
                requestPanel.Controls.Add(_getShiftInformation(new Point((_mainRequestPanel.Size.Width / 5) * 2 + 2, 2), request));
                requestPanel.Controls.Add(_getAccept(new Point((_mainRequestPanel.Size.Width / 5) * 3 + 2, 2), request));
                requestPanel.Controls.Add(_getDeny(new Point((_mainRequestPanel.Size.Width / 5) * 4 + 2, 2), request));

                theRequestsPanel.Controls.Add(requestPanel);
                i++;
            }

            _mainRequestPanel.Controls.Add(theRequestsPanel);
            _mainRequestPanel.Controls.Add(_getTopBarPanel(new Size(_mainRequestPanel.Size.Width - 20, panelHeight)));
            _mainRequestPanel.Controls.Add(titleTopLabel);

            return _mainRequestPanel;
        }

        private LinkLabel _getVolunteerName(Point location, Request request, Worker worker)
        {
            LinkLabel volunteerNameLabel = new LinkLabel
            {
                Location = location,
                Text = request.Worker.Name,
                AutoSize = false,
                Tag = worker
            };
            volunteerNameLabel.Click += VolunteerLabel_clicked;
            volunteerNameLabel.LinkColor = Color.Black;
            volunteerNameLabel.LinkBehavior = LinkBehavior.HoverUnderline;
            return volunteerNameLabel;
        }
        public void VolunteerLabel_clicked(object sender, EventArgs e)
        {
            Volunteer volunteer = (Volunteer)((Label)sender).Tag;
            _volunteerMainUI.DisplayVolunteerOnHomepage(volunteer);
        }

        private Label _getDateItWasSent(Point location, Request request)
        {
            Label dateSent = new Label
            {
                Location = location,
                Text = request.TimeSent.ToShortDateString(),
                AutoSize = true
            };

            return dateSent;
        }

        private Shift pressedShift;
        private LinkLabel _getShiftInformation(Point location, Request request)
        {

            pressedShift = _volunteerMainUI.GetScheduleController().FindSingleShift(x => x.Requests.Contains(request));
            LinkLabel shiftInformationLabel = new LinkLabel
            {
                Location = location,


                Text = pressedShift.Task + " \n" +
                                         pressedShift.StartTime.DayOfWeek + " " +
                                         pressedShift.StartTime.TimeOfDay.ToString("hh\\:mm"),
                AutoSize = true
            };
            //Hvis du bare bruger presseddShift, så vil den altid vise den sidste shift der er tilføjet.
            shiftInformationLabel.Click += delegate (object sender, EventArgs e) { _getShiftInformation_Clicked(sender, _volunteerMainUI.GetScheduleController().FindSingleShift(x => x.Requests.Contains(request))); };

            shiftInformationLabel.LinkColor = Color.Black;

            shiftInformationLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;

            //Mangler at skrive dato og klokslet på. Ikke længere
            return shiftInformationLabel;
        }

        private void _getShiftInformation_Clicked(object sender, Shift shift)
        {
            //Kan ikke få den til at vise shiften i skemaet...
            //Control temp = _volunteerMainUI.FindSpecificControl("Shift " + shift.ID.ToString());
            //if(temp != null)
            //    _volunteerMainUI.ScrollToControlOnSchedule(temp);
            _volunteerMainUI.DisplayPressedOnShift(shift);
        }

        private Button _getAccept(Point location, Request request)
        {
            Button acceptButton = new Button
            {
                Location = location,
                Text = "Accept",
                AutoSize = true,
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.System
            };
            acceptButton.Click += _acceptButton_Clicked;
            acceptButton.Tag = request;
            acceptButton.TabStop = false;
            
            return acceptButton;
        }

        private void _acceptButton_Clicked(object sender, EventArgs e)
        {
            Request request = (Request)((Button)sender).Tag;
            _volunteerMainUI.AcceptWorkerRequest(request);
        }

        private Button _getDeny(Point location, Request request)
        {
            Button denyButton = new Button
            {
                Location = location,
                Text = "Deny",
                AutoSize = true,
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.System
            };
            denyButton.Click += _denyButton_Clicked;
            denyButton.Tag = request;
            denyButton.TabStop = false;

            return denyButton;
        }

        private void _denyButton_Clicked(object sender, EventArgs e)
        {
            Request request = (Request)((Button)sender).Tag;
            _volunteerMainUI.DenyWorkerRequest(request);
        }
    }
}

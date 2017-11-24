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
            _mainRequestPanel = new Panel();
            _mainRequestPanel.Name = "_mainRequestPanel";
            _mainRequestPanel.AutoScroll = true;
            titleTopLabel = new Label();
        }

        private Panel getTopBarPanel(Size size)
        {
            Panel TopBarPanel = new Panel();
            TopBarPanel.Name = "TopBarPanel";
            TopBarPanel.Location = new Point(0, 40);
            TopBarPanel.Size = size;

            Label nameLabel = new Label();
            nameLabel.Text = "Name";
            nameLabel.Location = new Point((_mainRequestPanel.Size.Width / 5) * 0 + 2, 2);

            Label dateSentLabel = new Label();
            dateSentLabel.Text = "Request Sent";
            dateSentLabel.Location = new Point((_mainRequestPanel.Size.Width / 5) * 1 + 2, 2);

            Label shiftInfoLabel = new Label();
            shiftInfoLabel.Text = "Shift Info";
            shiftInfoLabel.Location = new Point((_mainRequestPanel.Size.Width / 5) * 2 + 2, 2);

            Label acceptButtonLabel = new Label();
            acceptButtonLabel.Text = "Accept";
            acceptButtonLabel.Location = new Point((_mainRequestPanel.Size.Width / 5) * 3 + 2, 2);

            Label denyButtonLabel = new Label();
            denyButtonLabel.Text = "Deny";
            denyButtonLabel.Location = new Point((_mainRequestPanel.Size.Width / 5) * 4 + 2, 2);


            TopBarPanel.Controls.Add(nameLabel);
            TopBarPanel.Controls.Add(dateSentLabel);
            TopBarPanel.Controls.Add(shiftInfoLabel);
            TopBarPanel.Controls.Add(acceptButtonLabel);
            TopBarPanel.Controls.Add(denyButtonLabel);
            return TopBarPanel;
        }

        public Panel GetRequestPanel(Size forRefference)
        {
            _mainRequestPanel.Controls.Clear();
            _mainRequestPanel.Size = forRefference;
            titleTopLabel.Location = new Point(10, 0);
            titleTopLabel.Text = "Pending Requests";
            titleTopLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            titleTopLabel.AutoSize = true;
            
            int i = 1;
            int panelHeight = 40;
            foreach (Request request in _volunteerMainUI.GetScheduleController().GetAllRequests())
            {
                Panel requestPanel = new Panel();
                requestPanel.Name = "requestPanel";
                requestPanel.Location = new Point(0, (i * panelHeight) + 20);
                requestPanel.Size = new Size(_mainRequestPanel.Width-20, panelHeight);
                requestPanel.BorderStyle = BorderStyle.FixedSingle;
                if (i % 2 == 0)
                    requestPanel.BackColor = Color.LightGray;

                requestPanel.Controls.Add(getVolunteerName(new Point((_mainRequestPanel.Size.Width / 5) * 0 + 2, 2), request, request.Worker));
                requestPanel.Controls.Add(getDateItWasSent(new Point((_mainRequestPanel.Size.Width / 5) * 1 + 2, 2), request));
                requestPanel.Controls.Add(getShiftInformation(new Point((_mainRequestPanel.Size.Width / 5) * 2 + 2, 2), request));
                requestPanel.Controls.Add(getAccept(new Point((_mainRequestPanel.Size.Width / 5) * 3 + 2, 2), request));
                requestPanel.Controls.Add(getDeny(new Point((_mainRequestPanel.Size.Width / 5) * 4 + 2, 2), request));

                _mainRequestPanel.Controls.Add(requestPanel);
                i++;
            }

            _mainRequestPanel.Controls.Add(getTopBarPanel(new Size(_mainRequestPanel.Size.Width - 20, panelHeight)));
            _mainRequestPanel.Controls.Add(titleTopLabel);

            return _mainRequestPanel;
        }

        private Label getVolunteerName(Point location, Request request, Worker worker)
        {
            Label volunteerNameLabel = new Label();
            volunteerNameLabel.Location = location;
            volunteerNameLabel.Text = request.Worker.Name;
            volunteerNameLabel.ForeColor = Color.Blue;
            volunteerNameLabel.Font = new Font(volunteerNameLabel.Font,FontStyle.Underline);

            volunteerNameLabel.Tag = worker;
            volunteerNameLabel.Click += volunteerLabel_clicked;
            volunteerNameLabel.AutoSize = true;

            return volunteerNameLabel;
        }
        public void volunteerLabel_clicked(object sender, EventArgs e)
        {
            Volunteer volunteer = (Volunteer)((Label)sender).Tag;
            _volunteerMainUI.DisplayVolunteerOnHomepage(volunteer);
        }

        private Label getDateItWasSent(Point location, Request request)
        {
            Label dateSent = new Label();
            dateSent.Location = location;
            dateSent.Text = request.TimeSent.ToShortDateString();
            dateSent.AutoSize = true;

            return dateSent;
        }

        private Shift pressedShift;
        private Label getShiftInformation(Point location, Request request)
        {

            pressedShift = _volunteerMainUI.GetScheduleController().FindSingleShift(x => x.Requests.Contains(request));
            Label shiftInformationLabel = new Label();
            shiftInformationLabel.Location = location;

            shiftInformationLabel.Text = pressedShift.Task + " \n" + 
                                         pressedShift.StartTime.DayOfWeek +  " " +
                                         pressedShift.StartTime.TimeOfDay.ToString("hh\\:mm");
            shiftInformationLabel.AutoSize = true;
            shiftInformationLabel.Click += getShiftInformation_Clicked;

            //Mangler at skrive dato og klokslet på
            return shiftInformationLabel;
        }
        private void getShiftInformation_Clicked(object sender, EventArgs e)
        {
            //_volunteerMainUI.ScrollToControlOnSchedule(pressedShift.);
        }

        private Button getAccept(Point location, Request request)
        {
            Button acceptButton = new Button();
            acceptButton.Location = location;
            acceptButton.Text = "Accept";
            acceptButton.AutoSize = true;
            acceptButton.Click += acceptButton_Clicked;
            acceptButton.Tag = request;
            acceptButton.TabStop = false;
            
            return acceptButton;
        }

        private void acceptButton_Clicked(object sender, EventArgs e)
        {
            Request request = (Request)((Button)sender).Tag;
            _volunteerMainUI.AcceptWorkerRequest(request);
        }

        private Button getDeny(Point location, Request request)
        {
            Button denyButton = new Button();
            denyButton.Location = location;
            denyButton.Text = "Deny";
            denyButton.AutoSize = true;
            denyButton.Click += denyButton_Clicked;
            denyButton.Tag = request;
            denyButton.TabStop = false;

            return denyButton;
        }

        private void denyButton_Clicked(object sender, EventArgs e)
        {
            Request request = (Request)((Button)sender).Tag;
            _volunteerMainUI.DenyWorkerRequest(request);
        }
    }
}

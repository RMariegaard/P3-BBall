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
        private Panel _mainPanel;
        private IVolunteerMainUI _volunteerMainUI;
        private Label titleTopLabel;

        public MainRequestPanelElement(IVolunteerMainUI volunteerMainUI)
        {
            _volunteerMainUI = volunteerMainUI;
            _mainPanel = new Panel();
            _mainPanel.AutoScroll = true;
            titleTopLabel = new Label();
        }

        private Panel getTopBarPanel(Size size)
        {
            Panel panel = new Panel();
            panel.Location = new Point(0, 40);
            panel.Size = size;

            Label nameLabel = new Label();
            nameLabel.Text = "Name";
            nameLabel.Location = new Point((_mainPanel.Size.Width / 5) * 0 + 2, 2);

            Label dateSentLabel = new Label();
            dateSentLabel.Text = "Request Sent";
            dateSentLabel.Location = new Point((_mainPanel.Size.Width / 5) * 1 + 2, 2);

            Label shiftInfoLabel = new Label();
            shiftInfoLabel.Text = "Shift Info";
            shiftInfoLabel.Location = new Point((_mainPanel.Size.Width / 5) * 2 + 2, 2);

            Label acceptButtonLabel = new Label();
            acceptButtonLabel.Text = "Accept";
            acceptButtonLabel.Location = new Point((_mainPanel.Size.Width / 5) * 3 + 2, 2);

            Label denyButtonLabel = new Label();
            denyButtonLabel.Text = "Deny";
            denyButtonLabel.Location = new Point((_mainPanel.Size.Width / 5) * 4 + 2, 2);


            panel.Controls.Add(nameLabel);
            panel.Controls.Add(dateSentLabel);
            panel.Controls.Add(shiftInfoLabel);
            panel.Controls.Add(acceptButtonLabel);
            panel.Controls.Add(denyButtonLabel);
            return panel;
        }

        public Panel GetPanel(Size forRefference)
        {
            _mainPanel.Controls.Clear();
            _mainPanel.Size = forRefference;
            titleTopLabel.Location = new Point(10, 0);
            titleTopLabel.Text = "Pending Requests";
            titleTopLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            titleTopLabel.AutoSize = true;
            
            int i = 1;
            int panelHeight = 40;
            foreach (Request request in _volunteerMainUI.GetController().GetAllRequests())
            {
                Panel tempPanel = new Panel();
                tempPanel.Location = new Point(0, (i * panelHeight) + 20);
                tempPanel.Size = new Size(_mainPanel.Width-20, panelHeight);
                tempPanel.BorderStyle = BorderStyle.FixedSingle;
                if (i % 2 == 0)
                    tempPanel.BackColor = Color.LightGray;

                tempPanel.Controls.Add(getVolunteerName(new Point((_mainPanel.Size.Width / 5) * 0 + 2, 2), request, request.Worker));
                tempPanel.Controls.Add(getDateItWasSent(new Point((_mainPanel.Size.Width / 5) * 1 + 2, 2), request));
                tempPanel.Controls.Add(getShiftInformation(new Point((_mainPanel.Size.Width / 5) * 2 + 2, 2), request));
                tempPanel.Controls.Add(getAccept(new Point((_mainPanel.Size.Width / 5) * 3 + 2, 2), request));
                tempPanel.Controls.Add(getDeny(new Point((_mainPanel.Size.Width / 5) * 4 + 2, 2), request));

                _mainPanel.Controls.Add(tempPanel);
                i++;
            }

            _mainPanel.Controls.Add(getTopBarPanel(new Size(_mainPanel.Size.Width - 20, panelHeight)));
            _mainPanel.Controls.Add(titleTopLabel);

            return _mainPanel;
        }

        private Label getVolunteerName(Point location, Request request, Worker worker)
        {
            Label volunteerNameLabel = new Label();
            volunteerNameLabel.Location = location;
            volunteerNameLabel.Text = request.Worker.Name;
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

        private Label getShiftInformation(Point location, Request request)
        {
            Label shiftInformationLabel = new Label();
            shiftInformationLabel.Location = location;
            shiftInformationLabel.Text = request.Shift.Task;
            shiftInformationLabel.AutoSize = true;

            //Mangler at skrive dato og klokslet på
            return shiftInformationLabel;
        }
        private Button getAccept(Point location, Request request)
        {
            Button acceptButton = new Button();
            acceptButton.Location = location;
            acceptButton.Text = "Accept";
            acceptButton.AutoSize = true;
            acceptButton.Click += acceptButton_Clicked;
            acceptButton.Tag = request;
            
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

            return denyButton;
        }

        private void denyButton_Clicked(object sender, EventArgs e)
        {
            Request request = (Request)((Button)sender).Tag;
            _volunteerMainUI.DenyWorkerRequest(request);
        }
    }
}

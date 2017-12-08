using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.Homepage.RequestPanelElements
{
    
    public class MainRequestPanelElement
    {
        private Panel _mainRequestPanel;
        private IVolunteerMainUI _volunteerMainUI;
        private Label titleTopLabel;
        private CheckBox NotificationCheckBox;
        private bool isChecked;
        int panelHeight = 40;
        Panel requestAndNotificationPanel;

        public MainRequestPanelElement(IVolunteerMainUI volunteerMainUI)
        {
            _volunteerMainUI = volunteerMainUI;
            _mainRequestPanel = new Panel
            {
                Name = "_mainRequestPanel",
                AutoScroll = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom
            };
            titleTopLabel = new Label();
            isChecked = true;
        }

        private Panel _getTopBarPanel(Size size)
        {
            Panel TopBarPanel = new Panel
            {
                Name = "TopBarPanel",
                Location = new Point(0, panelHeight),
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
                Text = "Date Sent",
                AutoSize = true,
                Location = new Point((_mainRequestPanel.Size.Width / 5) * 1 + 2, 2)
            };

            Label shiftInfoLabel = new Label
            {
                Text = "Shift Info",
                AutoSize = true,
                Location = new Point((_mainRequestPanel.Size.Width / 5) * 2 + 2, 2)
            };



            TopBarPanel.Controls.Add(nameLabel);
            TopBarPanel.Controls.Add(dateSentLabel);
            TopBarPanel.Controls.Add(shiftInfoLabel);
            return TopBarPanel;
        }

        public Panel GetRequestPanel(Size forRefference)
        {
            _mainRequestPanel.Controls.Clear();
            _mainRequestPanel.Size = forRefference;
            titleTopLabel.Location = new Point(10, 0);
            titleTopLabel.Text = "Pending ListOfRequests";
            titleTopLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            titleTopLabel.AutoSize = true;

            _mainRequestPanel.Controls.Add(titleTopLabel);

            Label notificationLabel = new Label
            {
                Text = "Show Notifications: ",
                Location = new Point(titleTopLabel.Location.X + titleTopLabel.Size.Width + 10, 15),
                AutoSize = true
            };
            NotificationCheckBox = new CheckBox();
            NotificationCheckBox.Checked = isChecked ? true : false;
            NotificationCheckBox.Location = new Point(notificationLabel.Location.X + notificationLabel.Size.Width + 10, 15);
            NotificationCheckBox.AutoSize = true;
            NotificationCheckBox.Click += checkBox_clicked;

            _mainRequestPanel.Controls.Add(notificationLabel);
            _mainRequestPanel.Controls.Add(NotificationCheckBox);

            UpdateRequestAndNotificationPanels();
            _mainRequestPanel.Controls.Add(_getTopBarPanel(new Size(_mainRequestPanel.Size.Width - 20, panelHeight)));

            return _mainRequestPanel;
        }

        private Panel theRequestPanel()
        {
            Panel theListOfRequestsPanel = new Panel
            {
                Location = new Point(_mainRequestPanel.Location.X, panelHeight + 15),
                Size = new Size(_mainRequestPanel.Width, _mainRequestPanel.Height - panelHeight - 15),
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };

            List<AbstractNotification> showedNotifications = new List<AbstractNotification>();
            if (NotificationCheckBox.Checked == false)
                _volunteerMainUI.GetScheduleController().GetAllListOfRequests().ForEach(x => showedNotifications.Add(x));
            else
                showedNotifications = _volunteerMainUI.GetScheduleController().GetAllRequestsAndNotifications();

            int i = 0;
            foreach (AbstractNotification abNotification in showedNotifications.OrderBy(x => x.Importance).ThenBy(x => x.DateCreated))
            {
                Panel notificationPanel = new Panel
                {
                    Name = "requestPanel",
                    Location = new Point(0, (i * panelHeight)),
                    Size = new Size(_mainRequestPanel.Width - 20, panelHeight),
                    BorderStyle = BorderStyle.FixedSingle
                };
                if (i % 2 != 0)
                    notificationPanel.BackColor = ColorAndStyle.SmallAlternatingColorsONE();
                else
                    notificationPanel.BackColor = ColorAndStyle.SmallAlternatingColorsTWO();

                if (abNotification.GetType() == typeof(Request))
                {
                    Request request = (Request)abNotification;

                    notificationPanel.Controls.Add(new Panel() { Size = new Size(5, notificationPanel.Height), Location = new Point(0, 0), BackColor = ColorAndStyle.RequestColor() });
                    notificationPanel.Controls.Add(_getVolunteerName(new Point((notificationPanel.Size.Width / 5) * 0 + 6, 2), request, request.Volunteer));
                    notificationPanel.Controls.Add(_getDateItWasSent(new Point((notificationPanel.Size.Width / 5) * 1 + 2, 2), request));
                    notificationPanel.Controls.Add(_getShiftInformation(new Point((notificationPanel.Size.Width / 5) * 2 + 2, 2), request));
                    notificationPanel.Controls.Add(_getAccept(new Point((notificationPanel.Size.Width / 5) * 3 + 2, 2), request));
                    notificationPanel.Controls.Add(_getDeny(new Point((notificationPanel.Size.Width / 5) * 4 + 2, 2), request));
                }
                else if (abNotification.GetType() == typeof(Notification))
                {
                    Notification notification = (Notification)abNotification;
                    Color notificationColor;
                    switch (notification.Importance)
                    {
                        case NotificationImportance.HighImportance:
                            notificationColor = ColorAndStyle.NotificationColorHighImportance();
                            break;
                        case NotificationImportance.MediumImportance:
                            notificationColor = ColorAndStyle.NotificationColorMediumImportance();
                            break;
                        case NotificationImportance.LowImportance:
                            notificationColor = ColorAndStyle.NotificationColorLowImportance();
                            break;
                        default:
                            notificationColor = ColorAndStyle.PrimaryColor();
                            break;
                    }

                    notificationPanel.Controls.Add(new Panel() { Size = new Size(5, notificationPanel.Height), Location = new Point(0, 0), BackColor = notificationColor });
                    notificationPanel.Controls.Add(notificationHeadder(new Point((notificationPanel.Size.Width / 5) * 0 + 6, 2), (notificationPanel.Size.Width / 5) + 2, notification.Headder));
                    notificationPanel.Controls.Add(notificationBody(new Point((notificationPanel.Size.Width / 5) * 1 + 5, 2), notificationPanel.Width - (((notificationPanel.Size.Width / 5) * 2) + 5), notification.Body));
                    notificationPanel.Controls.Add(removeNotificationButton(new Point((notificationPanel.Size.Width / 5) * 4 + 5, 2), notification));
                }

                theListOfRequestsPanel.Controls.Add(notificationPanel);
                i++;
            }

            return theListOfRequestsPanel;
        }

        private void checkBox_clicked(object sender, EventArgs e)
        {
            isChecked = !isChecked;
            UpdateRequestAndNotificationPanels();

        }

        public void UpdateRequestAndNotificationPanels()
        {
            _mainRequestPanel.Controls.Remove(requestAndNotificationPanel);
            requestAndNotificationPanel = theRequestPanel();
            _mainRequestPanel.Controls.Add(requestAndNotificationPanel);
            requestAndNotificationPanel.BringToFront();
        }

        private Label notificationHeadder(Point location, int maxWidth, string headder)
        {
            Label headderLabel = new Label()
            {
                Text = headder,
                Location = location,
                MaximumSize = new Size(maxWidth, 0),
                AutoSize = true
            };
            
            return headderLabel;
        }
        private Label notificationBody(Point location, int maxWidth, string body)
        {
            Label bodyLabel = new Label()
            {
                Text = body,
                Location = location,
                MaximumSize = new Size(maxWidth, 0),
                AutoSize = true
            };

            return bodyLabel;
        }
        private Button removeNotificationButton(Point location, Notification notification)
        {
            Button removeNotificationButton = new Button
            {
                AutoSize = true,
                Text = "Remove",
                Location = location,
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.System
            };

            removeNotificationButton.Click += delegate (object sender, EventArgs e)
            {
                _volunteerMainUI.GetScheduleController().RemoveNotification(notification);
                UpdateRequestAndNotificationPanels();
            };

            return removeNotificationButton;
        }

        private LinkLabel _getVolunteerName(Point location, Request request, Worker worker)
        {
            LinkLabel volunteerNameLabel = new LinkLabel
            {
                Location = location,
                Text = request.Volunteer.Name,
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

            pressedShift = _volunteerMainUI.GetScheduleController().FindSingleShift(x => x.ListOfRequests.Contains(request));
            LinkLabel shiftInformationLabel = new LinkLabel
            {
                Location = location,
                Text = pressedShift.Task + " \n" +
                                         pressedShift.StartTime.DayOfWeek + " " +
                                         pressedShift.StartTime.TimeOfDay.ToString("hh\\:mm"),
                AutoSize = true
            };
            //Hvis du bare bruger presseddShift, så vil den altid vise den sidste shift der er tilføjet.
            shiftInformationLabel.Click += delegate (object sender, EventArgs e) { _getShiftInformation_Clicked(sender, _volunteerMainUI.GetScheduleController().FindSingleShift(x => x.ListOfRequests.Contains(request))); };

            shiftInformationLabel.LinkColor = Color.Black;

            shiftInformationLabel.LinkBehavior = LinkBehavior.HoverUnderline;
            
            return shiftInformationLabel;
        }

        private void _getShiftInformation_Clicked(object sender, Shift shift)
        {
            _volunteerMainUI.HomepageChangeDay(shift.StartTime.DayOfWeek.ToString() + " " + shift.StartTime.ToShortDateString());
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

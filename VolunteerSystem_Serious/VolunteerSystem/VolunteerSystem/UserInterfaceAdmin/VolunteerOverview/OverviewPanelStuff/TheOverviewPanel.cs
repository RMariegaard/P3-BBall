using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin.VolunteerOverview.OverviewPanelStuff
{
    class TheOverviewPanel
    {
        Panel overviewPanel;

        Panel volunteerInformationPanel;
        Panel shiftInformaitonPanel;
        Panel requestInforationPanel;

        VolunteerOverview volunteerOverview;
        ScheduleController scheduleController;
        WorkerController workerController;

        public TheOverviewPanel(VolunteerOverview volunteerOverview, ScheduleController scheduleController, WorkerController workerController)
        {
            this.workerController = workerController;
            this.volunteerOverview = volunteerOverview;
            this.scheduleController = scheduleController;
        }

        public Panel GetPanel(Size size)
        {
            overviewPanel = new Panel
            {
                Size = size,
                Location = new Point(0, 0)
            };

            UpdatePanels();

            return overviewPanel;
        }

        public void UpdatePanels()
        {
            updateVolunteerInformationPanel();
            updateShiftInformationPanel();
            updateRequestInformaitonPanel();
        }

        private void updateVolunteerInformationPanel()
        {
            overviewPanel.Controls.Remove(volunteerInformationPanel);
            overviewPanel.Controls.Add(getVolunteerInformationPanel(new Point(5, 5), new Size((overviewPanel.Width / 3) - 10, overviewPanel.Height - 10)));
        }

        private void updateShiftInformationPanel()
        {
            overviewPanel.Controls.Remove(shiftInformaitonPanel);
            overviewPanel.Controls.Add(getShiftInformationPanel(new Point(volunteerInformationPanel.Location.X + volunteerInformationPanel.Width + 5, 5), new Size((overviewPanel.Width / 3) - 10, overviewPanel.Height - 10)));
        }
        private void updateRequestInformaitonPanel()
        {
            overviewPanel.Controls.Remove(requestInforationPanel);
            overviewPanel.Controls.Add(getRequestInformaitonPanel(new Point(shiftInformaitonPanel.Location.X + shiftInformaitonPanel.Width + 5, 5), new Size((overviewPanel.Width / 3) - 10, overviewPanel.Height - 10)));
        }

        private Panel getVolunteerInformationPanel(Point location, Size size)
        {
            volunteerInformationPanel = new Panel
            {
                Location = location,
                Size = size,
                BorderStyle = BorderStyle.FixedSingle,
            };

            Label titleTopLabel = new Label
            {
                Location = new Point(2, 2),
                Text = "Volunteer Information",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true
            };
            
            String informationString = "";
            if (volunteerOverview.SelectedWorker != null)
            {
                if (volunteerOverview.SelectedWorker.GetType() == typeof(Volunteer))
                {
                    Volunteer volunteer = (Volunteer)volunteerOverview.SelectedWorker;
                    informationString =
                        $"Name: {volunteer.Name}\n\n" +
                        $"Emil: {volunteer.Email}\n\n" +
                        $"Team: {volunteer.Assosiation}\n\n" +
                        $"Created: {volunteer.DateCreated.ToShortDateString()}\n\n" +
                        $"Years Participated: \n";
                    foreach (int year in volunteer.YearsWorked)
                    {
                        informationString += $"  - {year} \n";
                    }
                }
                else
                {
                    ExternalWorker externalWorker = (ExternalWorker)volunteerOverview.SelectedWorker;
                    informationString =
                        $"This is an external worker!\n\n" +
                        $"Name: {externalWorker.Name}\n\n" +
                        $"Emil: {externalWorker.Email}\n\n";
                }
            }
            else
            {
                informationString = "select a volunteer on the left to see his/her information here";
            }
            Label informationLabel = new Label
            {
                Text = informationString,
                Location = new Point(2, 50),
                MaximumSize = new Size(size.Width - 10, 0),
                AutoSize = true
            };

            Button removeWorkerButton = new Button()
            {
                Location = new Point(5, volunteerInformationPanel.Height - 25),
                Size = new Size(volunteerInformationPanel.Width - 10, 20),
                Text = "Remove Volunteer"
            };
            removeWorkerButton.Click += RemoveVolunteerButton_Click;

            volunteerInformationPanel.Controls.Add(removeWorkerButton);
            volunteerInformationPanel.Controls.Add(titleTopLabel);
            volunteerInformationPanel.Controls.Add(informationLabel);
            return volunteerInformationPanel;
        }

        private void RemoveVolunteerButton_Click(object sender, EventArgs e)
        {
            DeleteFormPopUp deleteFormPopUp = new DeleteFormPopUp("Are you sure you want to delete this volunteer and all requests from given volunteer?");
            deleteFormPopUp.StartPosition = FormStartPosition.CenterParent;
            deleteFormPopUp.ShowDialog();
            if (deleteFormPopUp.DialogResult == DialogResult.OK)
            {
                //Removes
                scheduleController.RemoveAllRequestsForAWorker(volunteerOverview.SelectedWorker);
                scheduleController.RemoveWorkerFromAllHisShifts(volunteerOverview.SelectedWorker);
                workerController.RemoveWorker(volunteerOverview.SelectedWorker);

                //Updates
                volunteerOverview.UpdateSeachAndVolunteerElement();
                volunteerOverview.SelectedWorker = null;
                volunteerOverview.UpdateVolunteerOverviewElement();
            }
        }

        private Panel getShiftInformationPanel(Point location, Size size)
        {
            shiftInformaitonPanel = new Panel
            {
                Location = location,
                Size = size,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true
            };

            Label titleTopLabel = new Label
            {
                Location = new Point(2, 2),
                Text = "Shift Information",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true
            };

            Worker worker = volunteerOverview.SelectedWorker;
            if (worker != null)
            {
                List<Shift> workerShifts = new List<Shift>();
                foreach (Shift shift in scheduleController.GetAllShifts())
                {
                    foreach(Worker work in shift.Workers.Where(x => x.Name == worker.Name))
                    {
                        if (worker.ID == work.ID)
                            workerShifts.Add(shift);
                    }
                }
                
                int panelHeight = 100;
                for (int i = 0; i < workerShifts.Count(); i++)
                {
                    Panel panel = new Panel
                    {
                        Size = new Size(size.Width - 30, panelHeight),
                        Location = new Point(5, (panelHeight * i) + 40),
                        BorderStyle = BorderStyle.FixedSingle,
                    };

                    Shift shift = workerShifts[i];

                    Panel colorPanel = new Panel()
                    {
                        Location = new Point(0, 0),
                        Size = new Size(panel.Size.Width, 10),
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    if (shift.Requests.Count() + shift.Workers.Count() < shift.VolunteersNeeded)
                        colorPanel.BackColor = ColorAndStyle.ShiftColorCompletelyFree();
                    else if ((shift.Workers.Count() < shift.VolunteersNeeded) && (shift.Workers.Count() + shift.Requests.Count() > shift.VolunteersNeeded))
                        colorPanel.BackColor = ColorAndStyle.ShiftColorEnoughRequests();
                    else if (shift.Workers.Count() >= shift.VolunteersNeeded)
                        colorPanel.BackColor = ColorAndStyle.ShiftColorWhenFull();
                    panel.Controls.Add(colorPanel);

                    string informationString =
                        $"{shift.StartTime.DayOfWeek}\n" +
                        $"{shift.Task}\n" +
                        $"{shift.StartTime.ToShortTimeString()} - {shift.EndTime.ToShortTimeString()}";

                    Label label = new Label
                    {
                        Text = informationString,
                        Location = new Point(5, 15),
                        MaximumSize = new Size(panel.Size.Width - 10, 0),
                        AutoSize = true
                    };
                    panel.Controls.Add(label);

                    Button button = new Button
                    {
                        Text = "Remove",
                        Size = new Size(panel.Size.Width - 10, 40),
                        Location = new Point(5, panel.Size.Height - 42),
                        Tag = workerShifts[i]
                    };
                    button.Click += delegate (object sender, EventArgs e)
                    {
                        scheduleController.RemoveWorkerFromShift(volunteerOverview.SelectedWorker, (Shift)((Control)sender).Tag);
                        updateShiftInformationPanel();
                    };
                    panel.Controls.Add(button);

                    shiftInformaitonPanel.Controls.Add(panel);
                }
            }

            shiftInformaitonPanel.Controls.Add(titleTopLabel);
            return shiftInformaitonPanel;
        }
        
        private Panel getRequestInformaitonPanel(Point location, Size size)
        {
            requestInforationPanel = new Panel
            {
                Location = location,
                Size = size,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true
            };

            Label titleTopLabel = new Label
            {
                Location = new Point(2, 2),
                Text = "Request Information",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true
            };

            Worker worker = volunteerOverview.SelectedWorker;
            int panelHeight = 150;
            if (worker != null)
            {
                List<Shift> requestedShifts = new List<Shift>();
                foreach (Shift shift in scheduleController.GetAllShifts())
                {
                    foreach (Request request in shift.Requests)
                    {
                        if (worker.ID == request.Worker.ID)
                            requestedShifts.Add(shift);
                    }
                }

                for (int i = 0; i < requestedShifts.Count(); i++)
                {
                    Panel panel = new Panel
                    {
                        Size = new Size(size.Width - 30, panelHeight),
                        Location = new Point(5, (panelHeight * i +2) + 40),
                        BorderStyle = BorderStyle.FixedSingle,
                    };

                    Shift shift = requestedShifts[i];

                    Panel colorPanel = new Panel()
                    {
                        Location = new Point(0, 0),
                        Size = new Size(panel.Size.Width, 10),
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    if (shift.Requests.Count() + shift.Workers.Count() < shift.VolunteersNeeded)
                        colorPanel.BackColor = ColorAndStyle.ShiftColorCompletelyFree();
                    else if ((shift.Workers.Count() < shift.VolunteersNeeded) && (shift.Workers.Count() + shift.Requests.Count() > shift.VolunteersNeeded))
                        colorPanel.BackColor = ColorAndStyle.ShiftColorEnoughRequests();
                    else if (shift.Workers.Count() >= shift.VolunteersNeeded)
                        colorPanel.BackColor = ColorAndStyle.ShiftColorWhenFull();
                    panel.Controls.Add(colorPanel);

                    string informationString =
                        $"{shift.StartTime.DayOfWeek}\n" +
                        $"{shift.Task}\n" +
                        $"{shift.StartTime.ToShortTimeString()} - {shift.EndTime.ToShortTimeString()}";
                    Label label = new Label
                    {
                        Text = informationString,
                        Location = new Point(5, 15),
                        MaximumSize = new Size(panel.Size.Width - 10, 0),
                        AutoSize = true
                    };
                    panel.Controls.Add(label);

                    int buttonHeight = 40;
                    Button acceptButton = new Button()
                    {
                        Text = "Accept",
                        Size = new Size(panel.Size.Width - 10, buttonHeight),
                        Location = new Point(5, panel.Size.Height - (2 * buttonHeight + 2)),
                        Tag = requestedShifts[i]
                    };
                    acceptButton.Click += delegate (object sender, EventArgs e)
                    {
                        scheduleController.ApproveRequest(((Shift)((Control)sender).Tag).Requests.First(x => x.Worker == volunteerOverview.SelectedWorker));
                        updateShiftInformationPanel();
                        updateRequestInformaitonPanel();
                    };
                    panel.Controls.Add(acceptButton);

                    Button denyButton = new Button()
                    {
                        Text = "Deny",
                        Size = new Size(panel.Size.Width - 10, buttonHeight),
                        Location = new Point(5, panel.Size.Height - (1 * buttonHeight + 2)),
                        Tag = requestedShifts[i]
                    };
                    denyButton.Click += delegate (object sender, EventArgs e)
                    {
                        scheduleController.DenyRequest(((Shift)((Control)sender).Tag).Requests.First(x => x.Worker == volunteerOverview.SelectedWorker));
                        updateShiftInformationPanel();
                        updateRequestInformaitonPanel();
                    };
                    panel.Controls.Add(denyButton);

                    requestInforationPanel.Controls.Add(panel);
                }
            }

            requestInforationPanel.Controls.Add(titleTopLabel);
            return requestInforationPanel;
        }
    }
}

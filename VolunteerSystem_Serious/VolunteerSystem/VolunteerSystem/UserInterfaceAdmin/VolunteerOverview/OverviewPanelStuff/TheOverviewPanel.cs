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

        public TheOverviewPanel(VolunteerOverview volunteerOverview, ScheduleController scheduleController)
        {
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
                informationString = "select a volunteer on the left to see his/her information here";
            }
            Label informationLabel = new Label
            {
                Text = informationString,
                Location = new Point(2, 50),
                MaximumSize = new Size(size.Width - 10, 0),
                AutoSize = true
            };

            volunteerInformationPanel.Controls.Add(titleTopLabel);
            volunteerInformationPanel.Controls.Add(informationLabel);
            return volunteerInformationPanel;
        }

        private Panel getShiftInformationPanel(Point location, Size size)
        {
            shiftInformaitonPanel = new Panel
            {
                Location = location,
                Size = size,
                BorderStyle = BorderStyle.FixedSingle
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
                List<Shift> workerShifts = scheduleController.GetAllShifts().Where(x => x.Workers.Exists(y => y.ID == worker.ID)).ToList();
                int panelHeight = 100;
                for (int i = 0; i < workerShifts.Count(); i++)
                {
                    Panel panel = new Panel
                    {
                        Size = new Size(size.Width - 10, panelHeight),
                        Location = new Point(5, (panelHeight * i) + 40),
                        BorderStyle = BorderStyle.FixedSingle,
                    };

                    Shift shift = workerShifts[i];
                    string informationString =
                        $"{shift.StartTime.DayOfWeek}\n" +
                        $"{shift.Task}\n" +
                        $"{shift.StartTime.ToShortTimeString()} - {shift.EndTime.ToShortTimeString()}";

                    Label label = new Label
                    {
                        Text = informationString,
                        Location = new Point(5, 5),
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
                BorderStyle = BorderStyle.FixedSingle
            };

            Label titleTopLabel = new Label
            {
                Location = new Point(2, 2),
                Text = "Request Information",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true
            };

            requestInforationPanel.Controls.Add(titleTopLabel);
            return requestInforationPanel;
        }
    }
}

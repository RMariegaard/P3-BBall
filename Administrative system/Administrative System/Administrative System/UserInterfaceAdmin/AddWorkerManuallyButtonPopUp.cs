using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem.UserInterfaceAdmin
{
    public partial class AddWorkerManuallyButtonPopUp : Form
    {
        private WorkerController workController;
        private IVolunteerMainUI volunteerMainUI;
        List<Worker> ListOfWorkersList;
        Shift shift;

        public AddWorkerManuallyButtonPopUp(IVolunteerMainUI volunteerMainUI, Shift shift)
        {
            StartPosition = FormStartPosition.CenterParent;
            workController = volunteerMainUI.GetWorkController();
            
            this.volunteerMainUI = volunteerMainUI;
            this.shift = shift;
            InitializeComponent();

            this.Icon = new Icon(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\TwoMen.ico");
            this.Text = "Volunteer Manegement System : Add Worker";

            ListOfWorkersList = workController.ListOfWorkers;
            addListOfWorkersToList();
        }

        private void SearchForWorker_TextChanged(object sender, EventArgs e)
        {
            updateWorkerList();
        }
 

        private void updateWorkerList()
        {
            
             ListOfWorkersList = workController.ListOfWorkers.Where(x => x.Name.ToLower().Contains(SearchForWorker.Text.ToLower())).OrderBy(x => x.Name).ToList();

            addListOfWorkersToList();

        }
        private void addListOfWorkersToList()
        {
            ListOfWorkersBox.Items.Clear();
            ListOfWorkersBox.BeginUpdate();
            foreach (var worker in ListOfWorkersList)
            {
                ListOfWorkersBox.Items.Add(worker.Name);
            }
            ListOfWorkersBox.EndUpdate();
        }

        private void addWorkerButton_Click(object sender, EventArgs e)
        {
            volunteerMainUI.GetScheduleController().AddWorkerToShift(shift, ListOfWorkersList.Where(x => x.Name == (string)ListOfWorkersBox.SelectedItem).FirstOrDefault());
            this.Close();
        }

        private void CancelAddWorker_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

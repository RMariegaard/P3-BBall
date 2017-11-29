using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        List<Worker> workersList;
        Shift shift;

        public AddWorkerManuallyButtonPopUp(IVolunteerMainUI volunteerMainUI, Shift shift)
        {
            workController = volunteerMainUI.GetWorkController();
            this.volunteerMainUI = volunteerMainUI;
            this.shift = shift;
            InitializeComponent();

            workersList = workController.Workers;
            addWorkersToList();
        }

        private void SearchForWorker_TextChanged(object sender, EventArgs e)
        {
            updateWorkerList();
        }
 

        private void updateWorkerList()
        {
            
             workersList = workController.Workers.Where(x => x.Name.ToLower().Contains(SearchForWorker.Text.ToLower())).OrderBy(x => x.Name).ToList();

            addWorkersToList();

        }
        private void addWorkersToList()
        {
            listOfWorkersBox.Items.Clear();
            listOfWorkersBox.BeginUpdate();
            foreach (var worker in workersList)
            {
                listOfWorkersBox.Items.Add(worker.Name);
            }
            listOfWorkersBox.EndUpdate();
        }

        private void addWorkerButton_Click(object sender, EventArgs e)
        {
            volunteerMainUI.GetScheduleController().AddWorkerToShift(shift, workersList.Where(x => x.Name == (string)listOfWorkersBox.SelectedItem).FirstOrDefault());
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolunteerPrototype.UI;
using VolunteerSystem.Exceptions;
using VolunteerSystem.UserInterfaceAdmin;
using VolunteerSystem;

namespace VolunteerPrototype.LogIn
{
    public partial class RegisterForm : Form
    {
        private IUI _mainUI;
        public RegisterForm(IUI mainUI)
        {
            _mainUI = mainUI;
            InitializeComponent();


        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                Volunteer volunteer;
                if (textBoxPassword.Text == textBoxConfirmPassword.Text)
                {
                    if(textBoxPhonenumber.Text == "")
                        volunteer = new Volunteer(textBoxName.Text, textBoxEmail.Text, textBoxAssociation.Text, textBoxPassword.Text);
                    else
                        volunteer = new Volunteer(textBoxName.Text, textBoxEmail.Text, textBoxAssociation.Text, int.Parse(textBoxPhonenumber.Text), textBoxPassword.Text);
                   
                    _mainUI.WorkerController().CreateWorker(volunteer);
                    DialogResult = DialogResult.Yes;
                    _mainUI.LogIn(volunteer);
                    Close();
                }
                else
                {
                    WrongEmailWarning message = new WrongEmailWarning("Password does not match, pls try again");
                    message.StartPosition = FormStartPosition.CenterParent;
                    message.ShowDialog();
                }
            }
            catch (FormatException)
            {
                WrongEmailWarning message = new WrongEmailWarning("Phonenumber is not legal");
                message.StartPosition = FormStartPosition.CenterParent;
                message.ShowDialog();
            }
            catch (EmailUsedBeforeException excpetion)
            {
                WrongEmailWarning message = new WrongEmailWarning(excpetion.Message);
                message.StartPosition = FormStartPosition.CenterParent;
                message.ShowDialog();
            }
            catch (EmailNotValidException)
            {
                WrongEmailWarning message = new WrongEmailWarning("The Email format is not legal, enter a correct email adress.");
                message.StartPosition = FormStartPosition.CenterParent;
                message.ShowDialog();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

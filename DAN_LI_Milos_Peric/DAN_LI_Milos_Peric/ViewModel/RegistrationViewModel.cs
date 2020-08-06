using DAN_LI_Milos_Peric.Command;
using DAN_LI_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_LI_Milos_Peric.ViewModel
{
    class RegistrationViewModel
    {
        RegistrationView registration;

        public RegistrationViewModel(RegistrationView registrationMenu)
        {
            registration = registrationMenu;
        }

        private ICommand addDoctorCommand;
        public ICommand AddDoctorCommand
        {
            get
            {
                if (addDoctorCommand == null)
                {
                    addDoctorCommand = new RelayCommand(param => AddNewDoctorExecute());
                }
                return addDoctorCommand;
            }
        }

        private void AddNewDoctorExecute()
        {
            try
            {
                AddDoctorView addDoctor = new AddDoctorView();
                addDoctor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private ICommand addPatientCommand;
        public ICommand AddPatientCommand
        {
            get
            {
                if (addPatientCommand == null)
                {
                    addPatientCommand = new RelayCommand(param => AddNewPatientExecute());
                }
                return addPatientCommand;
            }
        }

        private void AddNewPatientExecute()
        {
            try
            {
                AddPatientView addPatient = new AddPatientView();
                addPatient.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private ICommand logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                if (logoutCommand == null)
                {
                    logoutCommand = new RelayCommand(param => Logout());
                    return logoutCommand;
                }
                return logoutCommand;
            }
        }

        public void Logout()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButton.OKCancel);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        LoginView loginView = new LoginView();
                        Thread.Sleep(1000);
                        registration.Close();
                        loginView.Show();
                        return;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

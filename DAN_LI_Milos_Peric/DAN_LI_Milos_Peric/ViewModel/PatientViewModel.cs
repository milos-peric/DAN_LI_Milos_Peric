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
    class PatientViewModel : ViewModelBase
    {
        PatientView patientView;
        public PatientViewModel(PatientView patientViewOpen, tblPatient patientInstance)
        {
            patientView = patientViewOpen;
            Patient = patientInstance;
        }

        private tblPatient patient;
        public tblPatient Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
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
                        patientView.Close();
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

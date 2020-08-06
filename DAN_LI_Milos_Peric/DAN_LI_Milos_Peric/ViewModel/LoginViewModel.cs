using DAN_LI_Milos_Peric.Command;
using DAN_LI_Milos_Peric.Helper;
using DAN_LI_Milos_Peric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_LI_Milos_Peric.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        LoginView login;
        DataBaseService dataBaseService = new DataBaseService();
        public LoginViewModel(LoginView viewLogin)
        {
            login = viewLogin;
            doctor = new tblDoctor();
        }

        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private tblDoctor doctor;
        public tblDoctor Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
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

        private ICommand registerCommand;
        public ICommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                {
                    registerCommand = new RelayCommand(param => RegisterExecute());
                }
                return registerCommand;
            }
        }

        private void RegisterExecute()
        {
            try
            {
                RegistrationView registerView = new RegistrationView();
                login.Close();
                registerView.ShowDialog();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private ICommand signInCommand;
        public ICommand SignInCommand
        {
            get
            {
                if (signInCommand == null)
                {
                    signInCommand = new RelayCommand(SignInCommandExecute, param => CanSignInCommandExecute());
                }
                return signInCommand;
            }
        }

        private void SignInCommandExecute(object obj)
        {
            try
            {
                string password = (obj as PasswordBox).Password;
                doctor = dataBaseService.FindDoctorCredentials(UserName, password);                
                patient = dataBaseService.FindPatientCredentials(UserName, password);                
                if (doctor != null)
                {
                    DoctorView doctorView = new DoctorView(doctor);
                    login.Close();
                    doctorView.Show();
                    return;
                }
                else if (patient != null)
                {
                    PatientView patientView = new PatientView(patient);
                    login.Close();
                    patientView.Show();
                    return;
                }
                else
                {
                    MessageBox.Show("Wrong usename or password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSignInCommandExecute()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

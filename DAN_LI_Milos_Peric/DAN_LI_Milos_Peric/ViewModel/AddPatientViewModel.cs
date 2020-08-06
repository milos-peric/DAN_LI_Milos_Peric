using DAN_LI_Milos_Peric.Command;
using DAN_LI_Milos_Peric.Helper;
using DAN_LI_Milos_Peric.Validation;
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
    class AddPatientViewModel : ViewModelBase
    {
        AddPatientView addPatient;
        DataBaseService dataBaseService = new DataBaseService();

        public AddPatientViewModel(AddPatientView patientView)
        {
            addPatient = patientView;
            patient = new tblPatient();
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

        private ICommand addPatientCommand;
        public ICommand AddPatientCommand
        {
            get
            {
                if (addPatientCommand == null)
                {
                    addPatientCommand = new RelayCommand(AddPatientCommandExecute, param => CanAddPatientCommandExecute());
                }
                return addPatientCommand;
            }
        }

        private void AddPatientCommandExecute(object obj)
        {
            try
            {
                if (EntryValidation.ValidateName(Patient.FirstName) == false)
                {
                    MessageBox.Show("First Name can only contain letters. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateName(Patient.LastName) == false)
                {
                    MessageBox.Show("Last Name can only contain letters. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateJmbg(Patient.JMBG) == false)
                {
                    MessageBox.Show("JMBG you entered is not valid. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateMedicalInsuranceNumber(Patient.MedicalInsuranceNumber) == false)
                {
                    MessageBox.Show("Medical insurance number entered is not valid, must contain exactly 11 numbers. Please try again", "Invalid input");
                    return;
                }
                string password = (obj as PasswordBox).Password;
                string encryptPassword = EncryptionHelper.Encrypt(password);
                patient.Password = encryptPassword;
                dataBaseService.AddPatient(patient);
                MessageBox.Show("New patient registered successfully!", "Info");
                addPatient.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddPatientCommandExecute()
        {
            if (string.IsNullOrEmpty(patient.FirstName) || string.IsNullOrEmpty(patient.LastName) ||
              string.IsNullOrEmpty(patient.JMBG) || string.IsNullOrEmpty(patient.MedicalInsuranceNumber) || string.IsNullOrEmpty(patient.UserName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(param => CancelCommandExecute());
                }
                return cancelCommand;
            }
        }

        private void CancelCommandExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to close window?", "Close Window", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        addPatient.Close();
                        break;
                    case MessageBoxResult.No:
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

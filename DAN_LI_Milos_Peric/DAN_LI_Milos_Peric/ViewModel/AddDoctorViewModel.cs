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
    class AddDoctorViewModel : ViewModelBase
    {
        AddDoctorView addDoctor;
        DataBaseService dataBaseService = new DataBaseService();
        public AddDoctorViewModel(AddDoctorView doctorView)
        {
            addDoctor = doctorView;
            doctor = new tblDoctor();
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

        private ICommand addDoctorCommand;
        public ICommand AddDoctorCommand
        {
            get
            {
                if (addDoctorCommand == null)
                {
                    addDoctorCommand = new RelayCommand(AddDoctorCommandExecute, param => CanAddDoctorCommandExecute());
                }
                return addDoctorCommand;
            }
        }

        private void AddDoctorCommandExecute(object obj)
        {
            try
            {
                if (EntryValidation.ValidateName(Doctor.FirstName) == false)
                {
                    MessageBox.Show("First Name can only contain letters. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateName(Doctor.LastName) == false)
                {
                    MessageBox.Show("Last Name can only contain letters. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateJmbg(Doctor.JMBG) == false)
                {
                    MessageBox.Show("JMBG you entered is not valid. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateBankAccountNumber(Doctor.BankAccountNumber) == false)
                {
                    MessageBox.Show("Bank account nubmer you entered is not valid, must contain exactly 18 numbers. Please try again", "Invalid input");
                    return;
                }
                string password = (obj as PasswordBox).Password;
                string encryptPassword = EncryptionHelper.Encrypt(password);
                doctor.Password = encryptPassword;
                dataBaseService.AddDoctor(doctor);
                MessageBox.Show("New doctor registered successfully!", "Info");
                addDoctor.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddDoctorCommandExecute()
        {
            if (string.IsNullOrEmpty(doctor.FirstName) || string.IsNullOrEmpty(doctor.LastName) ||
              string.IsNullOrEmpty(doctor.JMBG) || string.IsNullOrEmpty(doctor.BankAccountNumber) || string.IsNullOrEmpty(doctor.UserName))
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
                        addDoctor.Close();
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

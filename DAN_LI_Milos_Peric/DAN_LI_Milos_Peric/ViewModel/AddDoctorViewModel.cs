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

        //AddItem addItem;
        //DataBaseService dataBaseService = new DataBaseService();
        //ActionEvent actionEventObject;

        //public AddItemViewModel(AddItem item)
        //{
        //    addItem = item;
        //    warehouseItem = new vwProduct();
        //    WarehouseItemList = dataBaseService.GetAllWarehouseItems().ToList();
        //    actionEventObject = new ActionEvent();
        //    actionEventObject.ActionPerformed += ActionPerformed;
        //}

        //private vwProduct warehouseItem;
        //public vwProduct WarehouseItem
        //{
        //    get { return warehouseItem; }
        //    set
        //    {
        //        warehouseItem = value;
        //        OnPropertyChanged("WarehouseItem");
        //    }
        //}

        //private List<vwProduct> warehouseItemList;
        //public List<vwProduct> WarehouseItemList
        //{
        //    get { return warehouseItemList; }
        //    set
        //    {
        //        warehouseItemList = value;
        //        OnPropertyChanged("WarehouseItemList");
        //    }
        //}

        //private ICommand addCommand;
        //public ICommand AddCommand
        //{
        //    get
        //    {
        //        if (addCommand == null)
        //        {
        //            addCommand = new RelayCommand(param => AddCommandExecute(), param => CanAddCommandExecute());
        //        }
        //        return addCommand;
        //    }
        //}

        //private void AddCommandExecute()
        //{
        //    try
        //    {
        //        if (EntryValidation.ValidateProductName(WarehouseItem.ProductName) == false)
        //        {
        //            MessageBox.Show("Product name can only contain letters and numbers. Please try again", "Invalid input");
        //            return;
        //        }
        //        if (EntryValidation.ValidateProductNumber(WarehouseItem.ProductNumber) == false)
        //        {
        //            MessageBox.Show("Product number can only contain numbers. Please try again", "Invalid input");
        //            return;
        //        }
        //        if (EntryValidation.ValidateAmount((int)WarehouseItem.Amount) == false)
        //        {
        //            MessageBox.Show("Amount must be greated than 0 and less or equat to 100. Please try again", "Invalid input");
        //            return;
        //        }
        //        if (EntryValidation.ValidatePriceFormat(WarehouseItem.Price) == false)
        //        {
        //            MessageBox.Show("Price can contain only decimal numbers (numbers, comma and dot is allowed). Please try again", "Invalid input");
        //            return;
        //        }
        //        if (EntryValidation.ValidatePriceAmount(WarehouseItem.Price) == false)
        //        {
        //            MessageBox.Show("Price must be a positive number. Please try again", "Invalid input");
        //            return;
        //        }
        //        WarehouseItem.InStock = false;
        //        dataBaseService.AddWarehouseItem(WarehouseItem);
        //        IsUpdateWarehouseItem = true;
        //        string logMessage = string.Format("Warehouse Item: {0}, Item number: {1}, Item amount: {2}, Item price: {3} was added to database.", WarehouseItem.ProductName,
        //        WarehouseItem.ProductNumber, WarehouseItem.Amount, WarehouseItem.Price);
        //        actionEventObject.OnActionPerformed(logMessage);
        //        MessageBox.Show("New Warehouse Item Added Successfully!", "Info");
        //        addItem.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        //private bool CanAddCommandExecute()
        //{
        //    if (string.IsNullOrEmpty(warehouseItem.ProductName) || string.IsNullOrEmpty(warehouseItem.ProductNumber) ||
        //        string.IsNullOrEmpty(warehouseItem.Amount.ToString()) || string.IsNullOrEmpty(warehouseItem.Price))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        //private ICommand cancelCommand;
        //public ICommand CancelCommand
        //{
        //    get
        //    {
        //        if (cancelCommand == null)
        //        {
        //            cancelCommand = new RelayCommand(param => CancelCommandExecute());
        //        }
        //        return cancelCommand;
        //    }
        //}

        //private void CancelCommandExecute()
        //{
        //    try
        //    {
        //        MessageBoxResult result = MessageBox.Show("Are you sure you want to close window?", "Close Window", MessageBoxButton.YesNo);
        //        switch (result)
        //        {
        //            case MessageBoxResult.Yes:
        //                addItem.Close();
        //                break;
        //            case MessageBoxResult.No:
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAN_LI_Milos_Peric.Helper;
using System.Threading.Tasks;

namespace DAN_LI_Milos_Peric
{
    class DataBaseService
    {
        internal tblDoctor AddDoctor(tblDoctor doctor)
        {
            try
            {
                using (MedicalServiceEntities context = new MedicalServiceEntities())
                {
                    tblDoctor newDoctor = new tblDoctor();
                    newDoctor.FirstName = doctor.FirstName;
                    newDoctor.LastName = doctor.LastName;
                    newDoctor.JMBG = doctor.JMBG;
                    newDoctor.BankAccountNumber = doctor.BankAccountNumber;
                    newDoctor.JMBG = doctor.JMBG;
                    newDoctor.UserName = doctor.UserName;
                    newDoctor.Password = doctor.Password;
                    context.tblDoctors.Add(newDoctor);
                    context.SaveChanges();
                    doctor.ID = newDoctor.ID;
                    return doctor;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        internal tblPatient AddPatient(tblPatient patient)
        {
            try
            {
                using (MedicalServiceEntities context = new MedicalServiceEntities())
                {
                    tblPatient newPatient = new tblPatient();
                    newPatient.FirstName = patient.FirstName;
                    newPatient.LastName = patient.LastName;
                    newPatient.JMBG = patient.JMBG;
                    newPatient.MedicalInsuranceNumber = patient.MedicalInsuranceNumber;
                    newPatient.JMBG = patient.JMBG;
                    newPatient.UserName = patient.UserName;
                    newPatient.Password = patient.Password;
                    context.tblPatients.Add(newPatient);
                    context.SaveChanges();
                    patient.ID = newPatient.ID;
                    return patient;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        //internal vwProduct EditWarehouseItem(vwProduct item)
        //{
        //    try
        //    {
        //        using (WarehouseDataBaseEntities context = new WarehouseDataBaseEntities())
        //        {
        //            tblProduct itemToEdit = (from i in context.tblProducts where i.ID == item.ID select i).First();
        //            itemToEdit.ProductName = item.ProductName;
        //            itemToEdit.ProductNumber = item.ProductNumber;
        //            itemToEdit.Amount = item.Amount;
        //            itemToEdit.Price = item.Price;
        //            context.SaveChanges();
        //            return item;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
        //        return null;
        //    }
        //}

        internal tblPatient FindPatientCredentials(string userName, string password)
        {
            try
            {
                using (MedicalServiceEntities context = new MedicalServiceEntities())
                {
                    string encryptedPassword = EncryptionHelper.Encrypt(password);
                    tblPatient patientToFind = (from d in context.tblPatients where d.UserName == userName && d.Password == encryptedPassword select d).First();
                    return patientToFind;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Doctor not found!" + ex.Message.ToString());
                return null;
            }
        }

        internal tblDoctor FindDoctorCredentials(string userName, string password)
        {
            try
            {
                using (MedicalServiceEntities context = new MedicalServiceEntities())
                {
                    string encryptedPassword = EncryptionHelper.Encrypt(password);
                    tblDoctor doctorToFind = (from d in context.tblDoctors where d.UserName == userName && d.Password == encryptedPassword select d).First();
                    return doctorToFind;
                }
            }
            catch (Exception ex)
            {                
                System.Diagnostics.Debug.WriteLine("Doctor not found!" + ex.Message.ToString());
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAN_LI_Milos_Peric.Validation
{
    public class EntryValidation
    {
        public static bool ValidateJmbg(string jmbg)
        {
            if (jmbg.Length != 13)
            {
                return false;
            }
            bool isNumber = Int64.TryParse(jmbg, out long result);
            if (isNumber == false)
            {
                return false;
            }
            string datePartOfJmbg = jmbg.Substring(0, 7);
            StringBuilder dateStringBuilder = new StringBuilder(10);
            if (Convert.ToInt32(datePartOfJmbg.Substring(4, 1)) == 0)
            {
                dateStringBuilder.Append(datePartOfJmbg.Substring(0, 2) + "." + datePartOfJmbg.Substring(2, 2) + ".2" + datePartOfJmbg.Substring(4, 3));
            }
            else
            {
                dateStringBuilder.Append(datePartOfJmbg.Substring(0, 2) + "." + datePartOfJmbg.Substring(2, 2) + ".1" + datePartOfJmbg.Substring(4, 3));
            }
            bool dateIsValid = DateTime.TryParse(Convert.ToString(dateStringBuilder), out DateTime enteredDate);
            if (dateIsValid == true)
            {
                if (enteredDate.Year < 1900)
                {
                    return false;
                }
                DateTime presentDate = DateTime.Today;
                int compareDate = DateTime.Compare(enteredDate, presentDate);
                if (compareDate >= 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ValidateName(string nameSurname)
        {
            bool isOnlyLetters = Regex.IsMatch(nameSurname, @"^[a-zA-Z]+$");
            return isOnlyLetters;
        }

        public static bool ValidatePhone(string nameSurname)
        {
            bool isValidPhoneEntry = Regex.IsMatch(nameSurname, @"^[0-9*#+ /-]+$");
            return isValidPhoneEntry;
        }

        public static bool ValidateBankAccountNumber(string bankAccountNumber)
        {
            if (bankAccountNumber.Length != 18)
                return false;
            if (ulong.TryParse(bankAccountNumber, out _))
                return true;
            else
                return false;
        }

        public static bool ValidateMedicalInsuranceNumber(string medicalInsuranceNumber)
        {
            if (medicalInsuranceNumber.Length != 11)
                return false;
            if (ulong.TryParse(medicalInsuranceNumber, out _))
                return true;
            else
                return false;
        }
    }
}

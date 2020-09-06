using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Validation
{
    static class UserValidation
    {
        public static bool Validate(User user)
        {
            if (user.FirstName.Length < 1 || !user.FirstName.All(Char.IsLetter))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect first name, try again.", "Notification");
                return false;
            }
            if (user.LastName.Length < 1 || !user.LastName.All(char.IsLetter))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect last name, try again.", "Notification");
                return false;
            }
            if (user.DateOfBirth.ToShortDateString() == "1/1/0001")
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect date of birth, try again.", "Notification");
                return false;
            }
            if (int.Parse(user.DateOfBirth.ToString("yyyy")) > int.Parse(DateTime.Now.ToString("yyyy")))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Date of birth suggests that you are born in the future, please try again.", "Notification");
                return false;
            }
            if (int.Parse(DateTime.Now.ToString("yyyy")) - int.Parse(user.DateOfBirth.ToString("yyyy")) > 100)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Your date of birth suggests that you lived longer than 100 years, please try again.", "Notification");
                return false;
            }
            if ((int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(user.DateOfBirth.ToString("yyyyMMdd"))) / 10000 < 18)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Your date of birth suggests are under aged (less than 18 y/o), please try again.", "Notification");
                return false;
            }
            if (user.Gender == "" || user.Gender == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect gender, try again.", "Notification");
                return false;
            }
            if (user.Email.Length < 1)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect email, try again.", "Notification");
                return false;
            }
            if (user.PhoneNumber.Length < 10 || !user.PhoneNumber.All(Char.IsDigit))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect phone number (10 digits), try again.", "Notification");
                return false;
            }
            if (user.Username == "" || user.Username == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Username is incorrect, please try again.", "Notification");
                return false;
            }
            else
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    var cmd = new SqlCommand(@"select Username from tblUser where Username = @Username", conn);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    conn.Open();
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    while (reader1.Read())
                    {
                        if (reader1[0].ToString() == user.Username)
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Username already exists in database, try again.", "Notification");
                            return false;
                        }
                    }
                    reader1.Close();
                    conn.Close();
                }
            }
            if (user.Password == "" || user.Password == null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Password is incorrect, please try again.", "Notification");
                return false;
            }
            if (user.Password.Length < 8 || user.Password.Where(char.IsUpper).Count() == 0 || user.Password.Where(char.IsLower).Count() == 0 ||
                user.Password.Where(char.IsDigit).Count() == 0 || !CheckSpecialCharacters(user.Password))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Password must contain at least 8 characters:\n\none lowercase letter\n" +
                            "one uppercase letter\none number\none special character\n\nplease try again.", "Notification");
                return false;
            }

            return true;
        }

        public static bool CheckSpecialCharacters(string password)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (password.Contains(item))
                    return true;
            }

            return false;
        }
    }
}

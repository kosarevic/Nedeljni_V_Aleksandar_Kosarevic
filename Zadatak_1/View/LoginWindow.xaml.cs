using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zadatak_1.Model;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public static User CurrentUser = new User();

        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            CurrentUser = null;
            //Inserted value in password field is being converted into enrypted verson for latter matching with database version.
            byte[] data = System.Text.Encoding.ASCII.GetBytes(txtPassword.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);

            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            //User is extracted from the database matching inserted paramaters Username and Password.
            SqlCommand query = new SqlCommand("SELECT * FROM tblUser WHERE Username=@Username AND Password=@Password", sqlCon);
            query.CommandType = CommandType.Text;
            query.Parameters.AddWithValue("@Username", txtUsername.Text);
            query.Parameters.AddWithValue("@Password", hash);
            sqlCon.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                CurrentUser = new User
                {
                    Id = int.Parse(row[0].ToString()),
                    FirstName = row[1].ToString(),
                    LastName = row[2].ToString(),
                    DateOfBirth = DateTime.Parse(row[3].ToString()),
                    Gender = row[4].ToString(),
                    Email = row[5].ToString(),
                    PhoneNumber = row[6].ToString(),
                    Username = row[7].ToString(),
                    Password = row[8].ToString()
                };
            }

            if(CurrentUser != null)
            {
                NoticesWindow window = new NoticesWindow();
                window.Show();
                Close();
                return;
            }

            sqlCon.Close();

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Incorrect login credentials, please try again.", "Notification");
        }

        private void BtnRegister(object sender, RoutedEventArgs e)
        {
            RegistrationWindow window = new RegistrationWindow();
            window.Show();
            Close();
        }
    }
}

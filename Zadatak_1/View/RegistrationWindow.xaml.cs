using System;
using System.Collections.Generic;
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
using Zadatak_1.Validation;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        RegisterUserViewModel ruvm = new RegisterUserViewModel();

        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = ruvm;
            ruvm.User.FirstName = "";
            ruvm.User.LastName = "";
            ruvm.User.Gender = "";
            ruvm.User.Email = "";
            ruvm.User.PhoneNumber = "";
            ruvm.User.Username = "";
            ruvm.User.Password = "";
        }

        private void Btn_Confirm(object sender, RoutedEventArgs e)
        {
            ruvm.User.Password = PswBox.Password;
            ruvm.User.DateOfBirth = DateTime.Parse(Date.ToString());

            if (UserValidation.Validate(ruvm.User))
            {
                ruvm.AddUser();
                LoginWindow window = new LoginWindow();
                window.Show();
                Close();
            }
        }

        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.Show();
            Close();
        }
    }
}

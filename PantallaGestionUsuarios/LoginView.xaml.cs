using PantallaGestionUsuarios.Utils;
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

namespace PantallaGestionUsuarios.Views
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {

        public LoginView()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
        }

        public async void Login(object sender, RoutedEventArgs e)
        {

            if (userBox.Text.Length == 0 || passwordBox.Text.Length == 0) {

                MessageBox.Show("Escriba su nombre de usuario y contraseña");
                return;
            }

            bool result = await UserProcessor.SingIn(userBox.Text, passwordBox.Text);

            if (result)
            {
                Utilities.GoToUsers(sender, e);
                this.Close();
            }
        }
    }
}

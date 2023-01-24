using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PantallaGestionUsuarios
{
    /// <summary>
    /// Lógica de interacción para RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        public async void RegisterUser(object sender, RoutedEventArgs e)
        {
            if(nombreBox.textBox.Text.Length == 0 ||apellidosBox.textBox.Text.Length == 0 ||emailBox.textBox.Text.Length == 0 || passwordBox.passwordBox.Password.Length == 0 || repeatPasswordBox.passwordBox.Password.Length == 0)
            {
                MessageBox.Show("Debes rellenar todos los campos");
                return;
            }

            if(passwordBox.passwordBox.Password != repeatPasswordBox.passwordBox.Password)
            {
                MessageBox.Show("La contraseña debe ser igual");
                return;
            }

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    nombre = nombreBox.textBox.Text,
                    email = emailBox.textBox.Text,
                    password = passwordBox.passwordBox.Password,
                    apellidos = apellidosBox.textBox.Text,
                    rol = "admin"
                }),
                Encoding.UTF8,
                "application/json");

            var res = await UserProcessor.SignUp(jsonContent);
            if (res)
            {
                Utils.Utilities.GoToUsers(sender, e);
                this.Close();
            }
        }

    }
}

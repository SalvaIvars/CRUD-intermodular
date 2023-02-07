using PantallaGestionUsuarios.Utils;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

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

            if (emailBox.Text.Length == 0 || passwordBox.Password.Length == 0) {
                MessageBox.Show("Escriba su nombre de usuario y contraseña");
                return;
            }


            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    email = emailBox.Text.TrimEnd(),
                    password = passwordBox.Password.TrimEnd(),
                }),
                Encoding.UTF8,
                "application/json");

            bool result = await UserProcessor.SingIn(jsonContent);

            if (result) {
                ApiHelper.addTokens();
                UserModel user = await UserProcessor.LoadUser(emailBox.Text);
                Application.Current.Properties["user"] = user;
                Utilities.GoToUsers(sender, e);
                this.Close();
            }
        }

        public void showPassword(object sender, RoutedEventArgs e)
        {

        }
    }
}

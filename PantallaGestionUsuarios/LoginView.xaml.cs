using PantallaGestionUsuarios.Utils;
using PantallaGestionUsuarios.Views.Error;
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
                new CustomError("Escriba su nombre de usuario y contraseña").ShowDialog();
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

        public void exitApp(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

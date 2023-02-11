using Microsoft.Win32;
using PantallaGestionUsuarios.Utils;
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

namespace PantallaGestionUsuarios.Views
{
    /// <summary>
    /// Lógica de interacción para PostUser.xaml
    /// </summary>
    public partial class PostUser : Window
    {
        public PostUser()
        {
            InitializeComponent();

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void minimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void CreateUser(object sender, RoutedEventArgs e)
        {

            bool canUpdate = true;
            if (nombreBox.textBox.Text.Length == 0)
            {
                canUpdate = false;
                nombreBox.textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                nombreBox.textBox.BorderThickness = new Thickness(1.5f);

            }
            if (emailBox.textBox.Text.Length == 0)
            {
                canUpdate = false;
                emailBox.textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                emailBox.textBox.BorderThickness = new Thickness(1.5f);
            }
            if(passwordBox.passwordBox.Password.Length == 0)
            {
                canUpdate=false;
                passwordBox.passwordBox.BorderBrush = Brushes.Red;
                passwordBox.passwordBox.BorderThickness = new Thickness(1.5f);
            }
            if (canUpdate)
            {

                using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    password = passwordBox.passwordBox.Password,
                    name = nombreBox.textBox.Text,
                    email = emailBox.textBox.Text,
                    lastname = apellidosBox.textBox.Text,
                    date = fechaBox.textBox.Text,
                    nick = nickBox.textBox.Text,
                    rol = rolBox.Text,
                }),
                Encoding.UTF8,
                "application/json");

                await UserProcessor.PostUser(jsonContent);

                if (!profilePicture.Source.ToString().Contains("defaulProfilePicture.png"))
                {
                    await UserProcessor.PostPhoto(profilePicture, profilePicture.Source.ToString(), emailBox.textBox.Text);
                }

                Utils.Utilities.GoToUsers(sender, e);
                this.Close();
            }
        }

        private void nombreBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if (nombreBox.textBox.Text.Length > 0)
            {
                nombreBox.textBox.BorderThickness = new Thickness(0);
            }
        }

        private void emailBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if (emailBox.textBox.Text.Length > 0)
            {
                emailBox.textBox.BorderThickness = new Thickness(0);
            }
        }

        private void passwordBoxTextChange(Object sender, RoutedEventArgs argss)
        {
            if (passwordBox.passwordBox.Password.Length > 0)
            {
                passwordBox.passwordBox.BorderThickness = new Thickness(0);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                profilePicture.Source = new BitmapImage(new Uri(op.FileName));
            }

        }
    }
}

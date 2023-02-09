using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace PantallaGestionUsuarios.Views
{
    /// <summary>
    /// Lógica de interacción para PutFormUsers.xaml
    /// </summary>
    public partial class PutFormUsers : Window
    {
        private UserModel user { get; set; }
        public PutFormUsers(UserModel user)
        {
            InitializeComponent();
            this.user = user;
        }

        public async void LoadUserPhoto()
        {
            if (user.photo != null && user.photo.Length > 0)
            {
                profilePictureImage.Source = new BitmapImage(new Uri("http://localhost:8080/profilePicture/" + user.photo));
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void FillData(object sender, RoutedEventArgs e)
        {
            nombreBox.textBox.Text = user.name;
            apellidosBox.textBox.Text = user.lastname;
            fechaBox.textBox.Text = user.date;
            LoadUserPhoto();
        }

        public async void SendUser(object sender, RoutedEventArgs e)
        {
            bool canUpdate = true;
            if (nombreBox.textBox.Text.Length == 0)
            {
                canUpdate = false;
                nombreBox.textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                nombreBox.textBox.BorderThickness = new Thickness(1.5f);


                if (canUpdate)
                {
                    nombreBox.textBox.BorderThickness = new Thickness(0);
                    using StringContent jsonContent = new(
                        JsonSerializer.Serialize(new
                        {
                            nombre = nombreBox.textBox.Text,
                            apellidos = apellidosBox.textBox.Text,
                            fecha = fechaBox.textBox.Text,
                        }),
                        Encoding.UTF8,
                        "application/json");
                    await UserProcessor.UpdateUser(user._id, jsonContent);
                    Utils.Utilities.GoToUsers(sender, e);
                    this.Close();
                }
            }
        }

        void nombreBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if (nombreBox.textBox.Text.Length > 0)
            {
                nombreBox.textBox.BorderThickness = new Thickness(0);
            }
        }
    }
}

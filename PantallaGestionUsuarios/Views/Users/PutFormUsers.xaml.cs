using PantallaGestionUsuarios.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para PutFormUsers.xaml
    /// </summary>
    public partial class PutFormUsers : Window
    {
        private UserModel user {  get; set; }
        public PutFormUsers(UserModel user)
        {
            InitializeComponent();
            this.user = user;
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
            nombreBox.textBox.Text = user.nombre;
            apellidosBox.textBox.Text = user.apellidos;
            emailBox.textBox.Text = user.email;
            fechaBox.textBox.Text = user.fecha;
            nickBox.textBox.Text = user.nick;
            rolBox.textBox.Text = user.rol;
            Base64ToImage(user.foto);
        }

        public void Base64ToImage(string base64String)
        {
            if(base64String == null)
            {
                return;
            }
            byte[] binaryData = Convert.FromBase64String(base64String);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);
            bi.EndInit();

            profilePictureImage.Source = bi;
        }

        public async void SendUser(object sender, RoutedEventArgs e)
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
            if(canUpdate)
            {
                nombreBox.textBox.BorderThickness = new Thickness(0);
                emailBox.textBox.BorderThickness = new Thickness(0);
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(new
                    {
                        nombre = nombreBox.textBox.Text,
                        email = emailBox.textBox.Text,
                        apellidos = apellidosBox.textBox.Text,
                        fecha = fechaBox.textBox.Text,
                        nick = nickBox.textBox.Text,
                        rol = rolBox.textBox.Text,
                    }),
                    Encoding.UTF8,
                    "application/json");
                await UserProcessor.UpdateUser(user._id, jsonContent);
                Utils.Utilities.GoToUsers(sender, e);
                this.Close();
            }
        }
        
        private void nombreBoxTextChange(object sender, TextChangedEventArgs args)
        {
            if(nombreBox.textBox.Text.Length > 0)
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


    }
}

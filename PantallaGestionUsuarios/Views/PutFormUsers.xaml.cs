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
    /// Lógica de interacción para PutFormUsers.xaml
    /// </summary>
    public partial class PutFormUsers : Window
    {
        private UserModel user {  get; set; }
        public PutFormUsers(UserModel user)
        {
            InitializeComponent();
            this.user = user;

            publicationsButton.Click += GoToPublications;
            commentsButton.Click += GoToComments;
            usersButton.Click += GoToUsers;
        }

        private void GoToPublications(object sender, RoutedEventArgs e)
        {
            Utilities.GoToPublications(sender, e);
            this.Close();
        }
        private void GoToComments(object sender, RoutedEventArgs e)
        {
            Utilities.GoToComments(sender, e);
            this.Close();
        }
        private void GoToUsers(object sender, RoutedEventArgs e)
        {
            Utilities.GoToUsers(sender, e);
            this.Close();
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
            fotoBox.textBox.Text = user.foto;
            webBox.textBox.Text = user.web;
            rolBox.textBox.Text = user.rol;
        }

        public async void SendUser(object sender, RoutedEventArgs e)
        {

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    nombre = nombreBox.textBox.Text,
                    email = emailBox.textBox.Text,
                    apellidos = apellidosBox.textBox.Text,
                    fecha = fechaBox.textBox.Text,
                    web = webBox.textBox.Text,
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
}

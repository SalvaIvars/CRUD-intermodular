﻿using PantallaGestionUsuarios.Utils;
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

            publicationsButton.Click += GoToPublications;
            commentsButton.Click += GoToComments;
            usersButton.Click += GoToUsers;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
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
        private async void CreateUser(object sender, RoutedEventArgs e)
        {
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    id_usuario = "2221", // Prueba
                    password = passwordBox.passwordBox.Password,
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

            await UserProcessor.PostUser(jsonContent);
            Utils.Utilities.GoToUsers(sender, e);
            this.Close();

        }
    }
}

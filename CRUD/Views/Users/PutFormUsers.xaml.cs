﻿using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System;
using Microsoft.Win32;

namespace CRUD.Views
{
    /// <summary>
    /// Lógica de interacción para PutFormUsers.xaml
    /// </summary>
    public partial class PutFormUsers : Window
    {
        bool imageChanged = false;
        private UserModel user { get; set; }
        public PutFormUsers(UserModel user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void minimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            }

            if (canUpdate)
            {
                var imagen = user.photo;
                if (!profilePictureImage.Source.ToString().ToUpper().Contains("DEFAULTPROFILEPICTURE.PNG") && imageChanged)
                {
                    imagen = profilePictureImage.Source.ToString();
                    await UserProcessor.PostPhoto(profilePictureImage, profilePictureImage.Source.ToString(), user.email);
                }
                if(user.photo!= null)
                {
                    var contador = imagen.IndexOf('.');
                    string ext = imagen.Substring(contador, imagen.Length - contador);
                    imagen = user.email + ext;
                }

                if (user.photo == null)
                {
                    //MessageBox.Show(user.photo);
                    
                }
                else
                {
                    var contador = imagen.IndexOf('.');
                    string ext = imagen.Substring(contador, profilePictureImage.Source.ToString().Length - contador);
                    imagen = user.email + ext;
                }

                nombreBox.textBox.BorderThickness = new Thickness(0);
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(new
                    {
                        name = nombreBox.textBox.Text,
                        lastname = apellidosBox.textBox.Text,
                        date = fechaBox.textBox.Text,
                        email = user.email,
                        nick = user.nick,
                        password = user.password,
                        following = user.following,
                        photo = imagen,
                        rol = user.rol,
                        fav_routes = user.fav_routes,
                        description = user.description,
                    }),
                    Encoding.UTF8,
                    "application/json");
                await UserProcessor.UpdateUser(jsonContent);
                await menu.Refresh();
            }
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imageChanged = true;
                profilePictureImage.Source = new BitmapImage(new Uri(op.FileName));
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

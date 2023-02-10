﻿using PantallaGestionUsuarios.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PantallaGestionUsuarios.Controls
{
    /// <summary>
    /// Lógica de interacción para PersonalMenu.xaml
    /// </summary>
    public partial class PersonalMenu : UserControl
    {
        public PersonalMenu()
        {
            InitializeComponent();
            publicationsButton.Click += GoToPublications;
             commentsButton.Click += GoToComments;
             usersButton.Click += GoToUsers;

        }
        /*
        public async void LoadUserPhoto(object sender, RoutedEventArgs e)
        {
            UserModel userPhoto = (UserModel)Application.Current.Properties["user"];
            if(userPhoto.photo != null && userPhoto.photo.Length>0)
                {
                profilePicture.ImageSource = new BitmapImage(new Uri("http://localhost:8080/profilePicture/"+ userPhoto.photo));
            }
        }*/

        private void GoToPublications(object sender, RoutedEventArgs e)
        {
            Utilities.GoToPublications(sender, e);
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
        private void GoToComments(object sender, RoutedEventArgs e)
        {
            Utilities.GoToComments(sender, e);
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
        private void GoToUsers(object sender, RoutedEventArgs e)
        {
            Utilities.GoToUsers(sender, e);
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }

        private void logOut(object sender, RoutedEventArgs e)
        {
            Utilities.GoToLogin(sender, e);
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
    }
}
